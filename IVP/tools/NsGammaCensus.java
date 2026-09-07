import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

/**
 * Namespace-level Gamma purity: for each namespace, the distinct change-driver sets declared by
 * its top-level types (col-0 class-level [ChangeDrivers] attributes) AND its ChangeDrivers
 * namespace marker. A namespace is pure iff exactly one distinct set appears.
 */
void main(String[] args) throws IOException {
    String root = args.length > 0 ? args[0] : ".";
    Pattern cdP = Pattern.compile("ChangeDriver\\.([A-Za-z_]+)");
    Pattern nsP = Pattern.compile("^namespace\\s+([\\w.]+)");
    Map<String, Set<String>> nsSets = new TreeMap<>();
    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src", "tests"}) try (var s = Files.walk(Paths.get(root, r))) {
        s.filter(p -> p.toString().endsWith(".cs"))
         .filter(p -> !p.toString().contains("/obj/") && !p.toString().contains("/bin/"))
         .filter(p -> !p.getFileName().toString().equals("Usings.cs"))
         .filter(p -> !p.getFileName().toString().endsWith(".Designer.cs"))
         .forEach(files::add);
    }
    for (Path f : files) {
        String ns = null;
        String firstTopSet = null;
        for (String raw : Files.readAllLines(f)) {
            String line = raw.strip();
            if (ns == null) {
                Matcher nm = nsP.matcher(line);
                if (nm.find()) { ns = nm.group(1); continue; }
            }
            if (ns != null && firstTopSet == null && line.startsWith("[ChangeDriversAttribute(")) {
                Matcher m = Pattern.compile("\\[ChangeDriversAttribute\\(([^)]*)\\)\\]").matcher(line);
                if (m.find()) {
                    TreeSet<String> set = new TreeSet<>();
                    Matcher cm = cdP.matcher(m.group(1));
                    while (cm.find()) set.add(cm.group(1));
                    firstTopSet = String.join("+", set);
                }
            }
        }
        if (ns != null && firstTopSet != null) {
            nsSets.computeIfAbsent(ns, k -> new TreeSet<>()).add(firstTopSet);
        }
    }
    int composite = 0, single = 0;
    for (var e : nsSets.entrySet()) {
        if (e.getValue().size() >= 2) {
            composite++;
            System.out.println(e.getKey() + "  sets=" + e.getValue().size());
            for (String s : e.getValue()) System.out.println("     {" + s + "}");
        } else single++;
    }
    System.out.println("## namespaces=" + (composite + single) + " single-set=" + single + " composite=" + composite);
}
