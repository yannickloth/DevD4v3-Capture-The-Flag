import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

/** Root-causal namespace purity measurement.
 *  A namespace is single-root-set when all its annotated top-level types share the same
 *  set of root change drivers (subordinate drivers, marked by "->" or by absence of
 *  "(root", are ignored). Matches the causal-order model used by the after-state refactor.
 */
void main(String[] args) throws IOException {
    String root = (args.length > 0) ? args[0] : ".";
    record Elem(String ns, String name, Set<String> rootDrivers) {}
    List<Elem> elems = new ArrayList<>();
    Pattern nsP = Pattern.compile("namespace\\s+([A-Za-z0-9_.]+)");
    Pattern typeP = Pattern.compile("(?m)^\\s*(?:(?:public|internal|private|protected)\\s+)?(?:(?:static|sealed|abstract|partial|readonly|ref)\\s+)*(class|interface|enum|struct|record)\\s+([A-Za-z_][A-Za-z0-9_]*)");
    Pattern cdP = Pattern.compile("CD-\\d{2}");

    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src","tests"}) try (var s = Files.walk(Paths.get(root, r))) {
        s.filter(p -> p.toString().endsWith(".cs"))
         .filter(p -> !p.toString().contains("/obj/") && !p.toString().contains("/bin/"))
         .filter(p -> !p.getFileName().toString().equals("Usings.cs"))
         .forEach(files::add);
    }

    for (Path f : files) {
        String c = Files.readString(f); Matcher nm = nsP.matcher(c); String ns=""; while (nm.find()) ns = nm.group(1);
        if (ns.isEmpty()) continue;
        String[] lines = c.split("\n",-1); Matcher tm = typeP.matcher(c);
        while (tm.find()) {
            String name = tm.group(2); int pos = tm.start(); int li=0, acc=0;
            for (int i=0;i<lines.length;i++){acc+=lines[i].length()+1; if(acc>pos){li=i;break;}}
            Set<String> roots = new TreeSet<>();
            for (int j=li-1; j>=0 && j>=li-12; j--) {
                if (lines[j].contains("Change drivers:")) {
                    String line = lines[j];
                    Matcher cm = cdP.matcher(line);
                    List<int[]> spans = new ArrayList<>();
                    while (cm.find()) spans.add(new int[]{cm.start(), cm.end(), Integer.parseInt(cm.group().substring(3))});
                    boolean hasRootMarker = line.contains("(root");
                    for (int i = 0; i < spans.size(); i++) {
                        int start = spans.get(i)[0];
                        int end = (i + 1 < spans.size()) ? spans.get(i + 1)[0] : line.length();
                        String tail = line.substring(spans.get(i)[1], end);
                        String segment = line.substring(start, end);
                        // A driver is subordinate if it is explicitly fed into another driver.
                        if (tail.contains("→")) continue;
                        // If at least one driver in the line is explicitly marked as root,
                        // only those marked drivers count as roots; unmarked peers are subordinates.
                        if (hasRootMarker && !segment.contains("(root")) continue;
                        roots.add(String.format("CD-%02d", spans.get(i)[2]));
                    }
                    break;
                }
            }
            if (!roots.isEmpty()) elems.add(new Elem(ns, name, roots));
        }
    }

    Map<String,Set<String>> nsRootSets = new TreeMap<>();
    Map<String,Integer> nsCount = new TreeMap<>();
    for (Elem e : elems) {
        nsCount.merge(e.ns(), 1, Integer::sum);
        nsRootSets.computeIfAbsent(e.ns(), k -> new TreeSet<>()).add(String.join("+", e.rootDrivers()));
    }

    List<String> composite = new ArrayList<>();
    List<String> single = new ArrayList<>();
    for (var en : nsRootSets.entrySet()) {
        (en.getValue().size() == 1 ? single : composite).add(en.getKey());
    }

    System.out.println("## root-causal-census");
    System.out.println("namespaces=" + nsRootSets.size() + " single-root-set=" + single.size() + " composite-root-set=" + composite.size());
    System.out.println("## single-root-set");
    for (String s : single) System.out.println(s + " | " + nsRootSets.get(s).iterator().next() + " | " + nsCount.get(s));
    System.out.println("## composite-root-set");
    for (String s : composite) {
        System.out.println(s + " | types=" + nsCount.get(s) + " | root-sets=" + nsRootSets.get(s).size());
        for (String set : nsRootSets.get(s)) {
            long cnt = elems.stream().filter(e -> e.ns().equals(s) && String.join("+", e.rootDrivers()).equals(set)).count();
            System.out.println("  " + set + " -> " + cnt + " type(s)");
        }
    }
}
