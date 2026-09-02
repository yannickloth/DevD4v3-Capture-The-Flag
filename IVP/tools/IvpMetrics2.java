import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main() throws IOException {
    record Elem(String ns, String name, Set<String> drivers) {}
    List<Elem> elems = new ArrayList<>();
    Pattern nsP = Pattern.compile("namespace\\s+([A-Za-z0-9_.]+)");
    // only match a type declaration on a CODE line: optional modifiers then keyword then name then a declaration terminator
    Pattern typeP = Pattern.compile("(?m)^\\s*(?:(?:public|internal|private|protected)\\s+)?(?:(?:static|sealed|abstract|partial|readonly|ref)\\s+)*(class|interface|enum|record|struct)\\s+(\\w+)");
    Pattern cdP = Pattern.compile("CD-\\d{2}");

    List<Path> files = new ArrayList<>();
    try (var s = Files.walk(Paths.get("src"))) {
        s.filter(p -> p.toString().endsWith(".cs"))
         .filter(p -> !p.toString().contains("/obj/") && !p.toString().contains("/bin/"))
         .forEach(files::add);
    }
    for (Path f : files) {
        String c = Files.readString(f);
        Matcher nm = nsP.matcher(c); String ns = "";
        while (nm.find()) ns = nm.group(1);
        if (ns.isEmpty()) continue;
        String[] lines = c.split("\n", -1);
        Matcher tm = typeP.matcher(c);
        while (tm.find()) {
            String tname = tm.group(2);
            int pos = tm.start();
            int lineIdx = 0, accum = 0;
            for (int i=0;i<lines.length;i++){ accum += lines[i].length()+1; if (accum > pos){ lineIdx=i; break; } }
            Set<String> drv = new TreeSet<>();
            for (int i=lineIdx; i>=0 && i>=lineIdx-10; i--) {
                Matcher cm = cdP.matcher(lines[i]);
                while (cm.find()) drv.add(cm.group());
                if (lines[i].contains("Change drivers:")) break;
            }
            if (!drv.isEmpty()) elems.add(new Elem(ns, tname, drv));
        }
    }

    Map<String, Set<String>> elemByDriver = new TreeMap<>();
    Map<String, Set<String>> nsByDriver = new TreeMap<>();
    Map<String, Map<String, Set<String>>> elemNsByDriver = new TreeMap<>();
    for (Elem e : elems) for (String d : e.drivers()) {
        elemByDriver.computeIfAbsent(d, k->new TreeSet<>()).add(e.name());
        nsByDriver.computeIfAbsent(d, k->new TreeSet<>()).add(e.ns());
        elemNsByDriver.computeIfAbsent(d, k->new TreeMap<>())
            .computeIfAbsent(e.ns(), k->new TreeSet<>()).add(e.name());
    }
    Map<String,String> label = new LinkedHashMap<>();
    label.put("CD-01","open.mp/SampSharp platform API"); label.put("CD-02","CTF game-rules specification");
    label.put("CD-03","combat/weapon-rules specification"); label.put("CD-04","weapon-catalog configuration");
    label.put("CD-05","combo definitions"); label.put("CD-06","coin economy"); label.put("CD-07","GunGame mode rules");
    label.put("CD-08","account & authentication policy"); label.put("CD-09","authorization policy");
    label.put("CD-10","player-statistics/rank model"); label.put("CD-11","map configuration"); label.put("CD-12","map-rotation rules");
    label.put("CD-13","chat rules"); label.put("CD-14","anti-cheat policy"); label.put("CD-15","command set");
    label.put("CD-16","RCON security policy"); label.put("CD-17","game configuration/.env schema");
    label.put("CD-18","database schema/player data model"); label.put("CD-19","SQL dialect/DBMS");
    label.put("CD-20","outbound repository contract"); label.put("CD-21","DI container/composition");
    label.put("CD-22","hosting/deployment spec"); label.put("CD-23","Serilog logging"); label.put("CD-24","Discord webhook contract");
    label.put("CD-25","BCrypt password-hashing contract");

    System.out.println("## driver-activation-table");
    System.out.println();
    System.out.println("| Driver | Elements | Modules (namespaces) |");
    System.out.println("|---|---|---|");
    for (String d : elemByDriver.keySet())
        System.out.printf("| %s (%s) | %d | %d |%n", d, label.getOrDefault(d,""), elemByDriver.get(d).size(), nsByDriver.get(d).size());

    System.out.println();
    System.out.println("## per-driver-namespace-membership");
    System.out.println();
    for (String d : elemByDriver.keySet()) {
        System.out.println("### " + d + " — " + label.getOrDefault(d,""));
        System.out.println("Elements: " + elemByDriver.get(d).size() + ", Modules touched: " + nsByDriver.get(d).size());
        for (var en : elemNsByDriver.get(d).entrySet())
            System.out.println("- " + en.getKey() + " (" + en.getValue().size() + "): " + String.join(", ", en.getValue()));
        System.out.println();
    }
}
