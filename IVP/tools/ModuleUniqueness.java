import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

/**
 * Uniqueness census: every chain must be carried by EXACTLY ONE module —
 * (a) exactly one top-level type (flag chains with >1 type, even in one namespace),
 * (b) exactly one namespace marker (flag duplicate marker chains),
 * (c) no marker-only namespaces (a namespace whose only module is the marker).
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
    Map<String, List<String>> typesByChain = new TreeMap<>();   // chain -> ns::type
    Map<String, List<String>> markersByChain = new TreeMap<>(); // chain -> namespaces
    Set<String> nsWithType = new HashSet<>();
    Pattern nsP = Pattern.compile("^namespace\\s+([\\w.]+)");
    Pattern typeP = Pattern.compile("(?<![\\w.@])(class|struct|record|interface)\\s+([A-Za-z_][A-Za-z0-9_]*)\\b");
    for (Path f : files) {
        String ns = null;
        List<String> pending = null;
        boolean markerFile = f.getFileName().toString().equals("ChangeDrivers.cs");
        for (String raw : Files.readAllLines(f)) {
            String line = raw.strip(); if (line.startsWith("\uFEFF")) line = line.substring(1).strip();
            if (ns == null) { Matcher nm = nsP.matcher(line); if (nm.find()) { ns = nm.group(1); continue; } }
            if (ns == null) continue;
            boolean atCol0 = raw.length() > 0 && !Character.isWhitespace(raw.charAt(0));
            if (line.startsWith("[ChangeDriversAttribute(")) {
                Matcher m = Pattern.compile("\\[ChangeDriversAttribute\\(([^)]*)\\)\\]").matcher(line);
                if (!m.find()) continue;
                List<String> chain = new ArrayList<>();
                Matcher cm = Pattern.compile("ChangeDriver\\.([A-Za-z_]+)").matcher(m.group(1));
                while (cm.find()) chain.add(cm.group(1));
                if (markerFile) markersByChain.computeIfAbsent(String.join(" > ", chain), k -> new ArrayList<>()).add(ns);
                else if (atCol0) pending = chain;
                continue;
            }
            if (atCol0) {
                Matcher tm = typeP.matcher(line);
                if (tm.find()) {
                    if (pending != null) {
                        typesByChain.computeIfAbsent(String.join(" > ", pending), k -> new ArrayList<>())
                                    .add(ns + " :: " + tm.group(2));
                        nsWithType.add(ns);
                    }
                    pending = null;
                }
            }
        }
    }
    int dupTypes = 0, dupMarkers = 0, markerOnly = 0;
    System.out.println("## (a) chains carried by more than one type:");
    for (var e : typesByChain.entrySet()) if (e.getValue().size() > 1) {
        dupTypes++;
        System.out.println("{" + e.getKey() + "}  x" + e.getValue().size());
        for (String t : e.getValue()) System.out.println("     " + t);
    }
    System.out.println("## (b) chains carried by more than one namespace marker:");
    for (var e : markersByChain.entrySet()) if (e.getValue().size() > 1) {
        dupMarkers++;
        System.out.println("{" + e.getKey() + "}  x" + e.getValue().size());
        for (String n : e.getValue()) System.out.println("     " + n);
    }
    System.out.println("## (c) namespaces with a marker but no types:");
    for (var e : markersByChain.entrySet())
        for (String n : e.getValue())
            if (!nsWithType.contains(n)) { markerOnly++; System.out.println("     " + n + " {" + e.getKey() + "}"); }
    System.out.println("## duplicate-type chains=" + dupTypes + " duplicate-marker chains=" + dupMarkers + " marker-only namespaces=" + markerOnly);
}
