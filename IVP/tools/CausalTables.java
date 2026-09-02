import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main() throws IOException {
    // For each namespace and assembly: collect element root drivers (from "(root; " marks) and subordinated drivers.
    record Info(Set<String> roots, Set<String> subs, int elements) {}
    Map<String,Info> byNs = new TreeMap<>();
    Map<String,Info> byAsm = new TreeMap<>();
    Pattern cdP = Pattern.compile("CD-\\d{2}");
    List<Path> files = new ArrayList<>();
    for (String r: new String[]{"src","tests"}) try(var s=Files.walk(Paths.get(r))) {
        s.filter(p->p.toString().endsWith(".cs")).filter(p->!p.toString().contains("/obj/")&&!p.toString().contains("/bin/")).forEach(files::add);
    }
    for (Path f: files) {
        String asm = assemblyOf(f.toString());
        String c = Files.readString(f);
        Matcher nm = Pattern.compile("namespace\\s+([A-Za-z0-9_.]+)").matcher(c);
        String ns=""; while(nm.find()) ns=nm.group(1);
        if (ns.isEmpty()) continue;
        for (String ln : c.split("\n",-1)) {
            if (!ln.contains("Change drivers:") || ln.contains("Injected dependencies")) continue;
            if (!ln.contains("(root; ")) continue;
            Matcher cm = cdP.matcher(ln);
            List<String> codes = new ArrayList<>(); while(cm.find()) codes.add(cm.group());
            if (codes.isEmpty()) continue;
            // root = the code right after "(root; "
            String root = null; Set<String> subs = new TreeSet<>();
            int idx = ln.indexOf("(root; ");
            String after = ln.substring(idx);
            Matcher rm = Pattern.compile("CD-\\d{2}").matcher(after);
            if (rm.find()) root = rm.group();
            for (String code : codes) if (!code.equals(root)) subs.add(code);
            if (root==null) continue;
            byNs.computeIfAbsent(ns, k->new Info(new TreeSet<>(), new TreeSet<>(), 0));
            byNs.get(ns).roots().add(root); byNs.get(ns).subs().addAll(subs);
            byAsm.computeIfAbsent(asm, k->new Info(new TreeSet<>(), new TreeSet<>(), 0));
            byAsm.get(asm).roots().add(root); byAsm.get(asm).subs().addAll(subs);
        }
    }
    System.out.println("=== NAMESPACE causal table ===");
    for (var e: byNs.entrySet())
        System.out.println(e.getKey() + " | roots: " + String.join(",", e.getValue().roots()) + " | subordinated: " + String.join(",", e.getValue().subs()));
    System.out.println();
    System.out.println("=== ASSEMBLY causal table ===");
    for (var e: byAsm.entrySet())
        System.out.println(e.getKey() + " | roots: " + String.join(",", e.getValue().roots()) + " | subordinated: " + String.join(",", e.getValue().subs()));
}
String assemblyOf(String p) {
    if (p.startsWith("src/Application")) return "CTF.Application";
    if (p.startsWith("src/Host")) return "CTF.Host";
    if (p.startsWith("src/Persistence/Persistence.InMemory")) return "Persistence.InMemory";
    if (p.startsWith("src/Persistence/Persistence.MariaDB")) return "Persistence.MariaDB";
    if (p.startsWith("src/Persistence/Persistence.SQLite")) return "Persistence.SQLite";
    if (p.startsWith("tests/Application.Tests")) return "CTF.Application.Tests";
    if (p.startsWith("tests/Persistence.Tests")) return "Persistence.Tests";
    return "?";
}
