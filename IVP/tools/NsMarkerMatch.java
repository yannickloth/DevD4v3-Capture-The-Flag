import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

/** Reports namespaces whose ChangeDrivers marker chain != the exact chain of their top-level type(s). */
void main(String[] args) throws IOException {
    String root = args.length > 0 ? args[0] : ".";
    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src", "tests"}) try (var s = Files.walk(Paths.get(root, r))) {
        s.filter(p -> p.toString().endsWith(".cs"))
         .filter(p -> !p.toString().contains("/obj/") && !p.toString().contains("/bin/"))
         .filter(p -> !p.getFileName().toString().equals("Usings.cs"))
         .filter(p -> !p.getFileName().toString().endsWith(".Designer.cs"))
         .forEach(files::add);
    }
    Map<String, List<String>> marker = new HashMap<>();
    record Entry(String ns, String type, List<String> chain) {}
    List<Entry> tops = new ArrayList<>();
    Pattern nsP = Pattern.compile("^namespace\\s+([\\w.]+)");
    Pattern typeP = Pattern.compile("(?<![\\w.@])(class|struct|record|interface)\\s+([A-Za-z_][A-Za-z0-9_]*)\\b");
    for (Path f : files) {
        String ns = null;
        List<String> pending = null;
        boolean markerFile = f.getFileName().toString().equals("ChangeDrivers.cs");
        for (String raw : Files.readAllLines(f)) {
            String line = raw.strip();
            if (ns == null) { Matcher nm = nsP.matcher(line); if (nm.find()) { ns = nm.group(1); continue; } }
            if (ns == null) continue;
            boolean atCol0 = raw.length() > 0 && !Character.isWhitespace(raw.charAt(0));
            if (line.startsWith("[ChangeDriversAttribute(")) {
                Matcher m = Pattern.compile("\\[ChangeDriversAttribute\\(([^)]*)\\)\\]").matcher(line);
                if (!m.find()) continue;
                List<String> chain = new ArrayList<>();
                Matcher cm = Pattern.compile("ChangeDriver\\.([A-Za-z_]+)").matcher(m.group(1));
                while (cm.find()) chain.add(cm.group(1));
                if (markerFile) marker.put(ns, chain);
                else if (atCol0) pending = chain;
                continue;
            }
            if (atCol0) {
                Matcher tm = typeP.matcher(line);
                if (tm.find()) { if (pending != null) tops.add(new Entry(ns, tm.group(2), pending)); pending = null; }
            }
        }
    }
    // group types per namespace; a namespace must have exactly one distinct chain and it must equal the marker
    Map<String, List<Entry>> byNs = new TreeMap<>();
    for (Entry e : tops) byNs.computeIfAbsent(e.ns(), k -> new ArrayList<>()).add(e);
    int mismatches = 0;
    for (var en : byNs.entrySet()) {
        Set<String> chains = new LinkedHashSet<>();
        for (Entry e : en.getValue()) chains.add(String.join(",", e.chain()));
        List<String> m = marker.get(en.getKey());
        String ms = m == null ? "<no marker>" : String.join(",", m);
        boolean nsHasMarkerOnlyType = false;
        for (String c : chains) if (!c.equals(ms)) {
            mismatches++;
            System.out.println(en.getKey() + "\n     marker={" + ms + "}");
            for (Entry e : en.getValue()) if (String.join(",", e.chain()).equals(c))
                System.out.println("     " + e.type() + " {" + c + "}");
            break;
        }
    }
    System.out.println("## namespaces=" + byNs.size() + " marker/type mismatches=" + mismatches);
}
