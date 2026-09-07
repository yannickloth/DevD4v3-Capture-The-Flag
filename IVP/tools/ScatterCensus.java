import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

/**
 * Completeness census (scatter): groups top-level types by their exact causal chain and
 * reports every chain hosted in more than one namespace — a set living in several modules.
 */
void main(String[] args) throws IOException {
    String root = args.length > 0 ? args[0] : ".";
    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src", "tests"}) try (var s = Files.walk(Paths.get(root, r))) {
        s.filter(p -> p.toString().endsWith(".cs"))
         .filter(p -> !p.toString().contains("/obj/") && !p.toString().contains("/bin/"))
         .filter(p -> !p.getFileName().toString().equals("Usings.cs"))
         .filter(p -> !p.getFileName().toString().endsWith(".Designer.cs"))
         .filter(p -> !p.getFileName().toString().equals("ChangeDrivers.cs"))
         .forEach(files::add);
    }
    record Entry(String ns, String type, String chain) {}
    Map<String, List<Entry>> byChain = new TreeMap<>();
    Pattern nsP = Pattern.compile("^namespace\\s+([\\w.]+)");
    Pattern typeP = Pattern.compile("(?<![\\w.@])(class|struct|record|interface)\\s+([A-Za-z_][A-Za-z0-9_]*)\\b");
    for (Path f : files) {
        String ns = null;
        List<String> pendingChain = null;
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
                if (atCol0) pendingChain = String.join(",", chain) == null ? null : chain;
                continue;
            }
            if (atCol0) {
                Matcher tm = typeP.matcher(line);
                if (tm.find()) {
                    if (pendingChain != null) {
                        String key = String.join(" > ", pendingChain);
                        byChain.computeIfAbsent(key, k -> new ArrayList<>())
                               .add(new Entry(ns, tm.group(2), key));
                    }
                    pendingChain = null;
                }
            }
        }
    }
    int scattered = 0, unique = 0;
    for (var e : byChain.entrySet()) {
        Set<String> nss = new TreeSet<>();
        for (Entry en : e.getValue()) nss.add(en.ns());
        if (nss.size() >= 2) {
            scattered++;
            System.out.println("{" + e.getKey() + "}  spans " + nss.size() + " namespaces, " + e.getValue().size() + " types:");
            for (Entry en : e.getValue()) System.out.println("     " + en.ns() + " :: " + en.type());
        } else unique++;
    }
    System.out.println("## distinct chains=" + byChain.size() + " complete(1 ns)=" + unique + " scattered(>1 ns)=" + scattered);
}
