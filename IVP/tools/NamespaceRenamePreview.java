import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

/** Preview semantic names for Gamma_* namespaces: use first causal subordinate driver(s)
 *  until unique within the parent namespace. Excludes arrow-target drivers from the set.
 */
void main(String[] args) throws IOException {
    String root = (args.length > 0) ? args[0] : ".";
    Map<String,String> names = new LinkedHashMap<>();
    for (var e : new String[][]{
        {"CD-06","Coins"},{"CD-07","GunGame"},{"CD-08","Accounts"},{"CD-09","Authorization"},
        {"CD-11","Maps"},{"CD-12","Rotation"},{"CD-13","Chat"},{"CD-14","AntiCheat"},
        {"CD-15","Commands"},{"CD-16","Rcon"},{"CD-17","Settings"},{"CD-18","Database"},
        {"CD-19","MariaDB"},{"CD-20","Repository"},{"CD-21","Composition"},{"CD-22","Deployment"},
        {"CD-23","Logging"},{"CD-24","Discord"},{"CD-25","BCrypt"},{"CD-26","NUnit"},
        {"CD-27","FluentAssertions"},{"CD-28","NSubstitute"},{"CD-30","SQLite"},
        {"CD-31","PlayerEvents"},{"CD-32","ECS"},{"CD-33","Dialogs"},{"CD-34","TextDraws"},
        {"CD-35","GameText"},{"CD-36","ClientMessages"},{"CD-37","Pickups"},{"CD-38","MapIcons"},
        {"CD-39","AttachedObjects"},{"CD-40","Audio"},{"CD-41","Timers"},{"CD-42","ServerService"},
        {"CD-43","CommandInfrastructure"},{"CD-44","Models"}
    }) names.put(e[0], e[1]);

    record Elem(String ns, String name, List<String> sources, List<String> roots, List<String> subs) {}
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
        if (ns.isEmpty() || !ns.contains("Gamma_")) continue;
        String[] lines = c.split("\n",-1); Matcher tm = typeP.matcher(c);
        while (tm.find()) {
            String name = tm.group(2); int pos = tm.start(); int li=0, acc=0;
            for (int i=0;i<lines.length;i++){acc+=lines[i].length()+1; if(acc>pos){li=i;break;}}
            String cdLine = null;
            for (int j=li-1; j>=0 && j>=li-12; j--) {
                if (lines[j].contains("Change drivers:")) { cdLine = lines[j]; break; }
            }
            if (cdLine == null) continue;

            // Extract source drivers in order, excluding arrow targets.
            List<String> sources = new ArrayList<>();
            List<String> roots = new ArrayList<>();
            List<String> subs = new ArrayList<>();
            Matcher cm = cdP.matcher(cdLine);
            List<int[]> spans = new ArrayList<>();
            while (cm.find()) spans.add(new int[]{cm.start(), cm.end(), Integer.parseInt(cm.group().substring(3))});
            for (int i = 0; i < spans.size(); i++) {
                int prevEnd = i > 0 ? spans.get(i - 1)[1] : 0;
                int start = spans.get(i)[0];
                int nextStart = (i + 1 < spans.size()) ? spans.get(i + 1)[0] : cdLine.length();
                String before = cdLine.substring(prevEnd, start);
                String segment = cdLine.substring(start, nextStart);
                String id = String.format("CD-%02d", spans.get(i)[2]);
                if (before.contains("→")) continue; // arrow target, not a source
                sources.add(id);
                if (segment.contains("(root")) roots.add(id);
                else subs.add(id);
            }
            if (!sources.isEmpty()) elems.add(new Elem(ns, name, sources, roots, subs));
        }
    }

    Map<String, Set<String>> nsSets = new TreeMap<>();
    Map<String, Elem> nsSample = new TreeMap<>();
    for (Elem e : elems) {
        nsSets.computeIfAbsent(e.ns(), k-> new TreeSet<>()).add(String.join("+", e.sources()));
        nsSample.putIfAbsent(e.ns(), e);
    }

    Map<String, List<String>> byParent = new TreeMap<>();
    for (String ns : nsSets.keySet()) {
        int dot = ns.lastIndexOf('.');
        String parent = dot > 0 ? ns.substring(0, dot) : "";
        byParent.computeIfAbsent(parent, k-> new ArrayList<>()).add(ns);
    }

    for (var pen : byParent.entrySet()) {
        String parent = pen.getKey();
        List<String> children = pen.getValue();
        Set<String> used = new HashSet<>();
        Map<String,String> proposed = new LinkedHashMap<>();
        for (int len=1; ; len++) {
            Map<String,String> round = new LinkedHashMap<>();
            for (String ns : children) {
                if (proposed.containsKey(ns)) continue;
                Elem e = nsSample.get(ns);
                List<String> parts = new ArrayList<>();
                int count = 0;
                for (String id : e.sources()) {
                    if (e.roots().contains(id)) continue;
                    parts.add(names.getOrDefault(id, id.replace("-","")));
                    count++;
                    if (count >= len) break;
                }
                String nm = parts.isEmpty() ? "Core" : String.join("", parts);
                round.put(ns, nm);
            }
            for (var en : round.entrySet()) {
                String nm = en.getValue();
                if (!used.contains(nm) && Collections.frequency(round.values(), nm) == 1) {
                    proposed.put(en.getKey(), nm);
                    used.add(nm);
                }
            }
            if (proposed.size() == children.size()) break;
            if (len > 10) {
                for (String ns : children) if (!proposed.containsKey(ns)) proposed.put(ns, "Gamma" + ns.substring(ns.lastIndexOf('_')));
                break;
            }
        }
        for (String ns : children) {
            System.out.println(ns + " \u2192 " + parent + "." + proposed.get(ns) + "  (set=" + nsSets.get(ns).iterator().next() + ")");
        }
    }
}
