import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

/** Groups annotated types by exact full change-driver set per namespace. */
void main(String[] args) throws IOException {
    String root = (args.length > 0) ? args[0] : ".";
    record Elem(String ns, String name, Set<String> drivers) {}
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
            Set<String> d = new TreeSet<>();
            for (int j=li-1; j>=0 && j>=li-12; j--) {
                if (lines[j].contains("Change drivers:")) {
                    Matcher cm = cdP.matcher(lines[j]); while (cm.find()) d.add(cm.group());
                    break;
                }
            }
            if (!d.isEmpty()) elems.add(new Elem(ns, name, d));
        }
    }

    Map<String, Map<String, List<String>>> groups = new TreeMap<>();
    for (Elem e : elems) {
        groups.computeIfAbsent(e.ns(), k -> new LinkedHashMap<>())
              .computeIfAbsent(String.join("+", e.drivers()), k -> new ArrayList<>())
              .add(e.name());
    }

    int composite = 0, total = 0;
    for (var en : groups.entrySet()) {
        total++;
        if (en.getValue().size() > 1) composite++;
    }
    System.out.println("namespaces=" + total + " composite-by-full-set=" + composite + " single=" + (total-composite));

    for (var en : groups.entrySet()) {
        if (en.getValue().size() <= 1) continue;
        System.out.println("\n" + en.getKey() + " (" + en.getValue().size() + " sets):");
        for (var setEn : en.getValue().entrySet()) {
            System.out.println("  [" + setEn.getKey() + "] " + setEn.getValue().size() + " type(s): " + String.join(", ", setEn.getValue()));
        }
    }
}
