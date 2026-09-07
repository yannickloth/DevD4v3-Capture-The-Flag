import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

/**
 * Namespace-level causal purity: every top-level type's causal chain must EXTEND its
 * namespace marker's chain (marker = prefix, root-first order preserved; equal is fine).
 * Reports namespaces where a top-level type violates the extension property.
 */
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
        List<String> pendingChain = null;   // col-0 attr awaiting its top-level type decl
        boolean markerFile = f.getFileName().toString().equals("ChangeDrivers.cs");
        for (String raw : Files.readAllLines(f)) {
            String line = raw.strip();
            if (ns == null) {
                Matcher nm = nsP.matcher(line);
                if (nm.find()) { ns = nm.group(1); continue; }
            }
            if (ns == null) continue;
            boolean atCol0 = raw.length() > 0 && !Character.isWhitespace(raw.charAt(0));
            if (line.startsWith("[ChangeDriversAttribute(")) {
                Matcher m = Pattern.compile("\\[ChangeDriversAttribute\\(([^)]*)\\)\\]").matcher(line);
                if (!m.find()) continue;
                List<String> chain = new ArrayList<>();
                Matcher cm = Pattern.compile("ChangeDriver\\.([A-Za-z_]+)").matcher(m.group(1));
                while (cm.find()) chain.add(cm.group(1));
                if (markerFile) marker.put(ns, chain);
                else if (atCol0) pendingChain = chain;
                continue;
            }
            if (atCol0) {
                Matcher tm = typeP.matcher(line);
                if (tm.find()) {
                    if (pendingChain != null) tops.add(new Entry(ns, tm.group(2), pendingChain));
                    pendingChain = null;
                }
            }
        }
    }
    int ok = 0;
    record Bad(String ns, String type, List<String> chain) {}
    List<Bad> bad = new ArrayList<>();
    for (Entry e : tops) {
        List<String> mchain = marker.get(e.ns());
        boolean ext = mchain != null && e.chain().size() >= mchain.size();
        if (ext) for (int i = 0; i < mchain.size(); i++)
            if (!mchain.get(i).equals(e.chain().get(i))) { ext = false; break; }
        if (ext) ok++;
        else bad.add(new Bad(e.ns(), e.type(), e.chain()));
    }
    Map<String, List<Bad>> byNs = new TreeMap<>();
    for (Bad b : bad) byNs.computeIfAbsent(b.ns(), k -> new ArrayList<>()).add(b);
    for (var en : byNs.entrySet()) {
        System.out.println(en.getKey() + "  marker={" + String.join(",", marker.getOrDefault(en.getKey(), List.of("?"))) + "}");
        for (Bad b : en.getValue()) System.out.println("     " + b.type() + " {" + String.join(",", b.chain()) + "}");
    }
    System.out.println("## top-level types=" + tops.size() + " extension-consistent=" + ok + " violating=" + bad.size() + " namespaces-affected=" + byNs.size());
}
