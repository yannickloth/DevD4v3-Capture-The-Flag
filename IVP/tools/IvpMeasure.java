import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

/** Canonical IVP measurement tool.
 *  Semantics (pinned — do not change without re-baselining):
 *  - element = every class/interface/enum/struct/record declaration in src/ + tests/ (excl. Usings.cs, obj/, bin/)
 *  - Gamma-set = CD codes on the NEAREST "Change drivers:" remark line within 12 lines above the declaration (that line only;
 *    "Injected dependencies" remarks are supplementary and never contribute to the element's set)
 *  - run from the repo root: java IVP/tools/IvpMeasure.java [rootDir]   (default ".")
 */
void main(String[] args) throws IOException {
    String root = (args.length > 0) ? args[0] : ".";
    record Elem(String ns, String name, String kind, Set<String> drivers) {}
    List<Elem> elems = new ArrayList<>();
    Pattern nsP = Pattern.compile("namespace\\s+([A-Za-z0-9_.]+)");
    Pattern typeP = Pattern.compile("(?m)^\\s*(?:(?:public|internal|private|protected)\\s+)?(?:(?:static|sealed|abstract|partial|readonly|ref)\\s+)*(class|interface|enum|struct|record)\\s+([A-Za-z_][A-Za-z0-9_]*)");
    Pattern cdP = Pattern.compile("CD-\\d{2}");

    String[][] L = {
      {"CD-01","open.mp/SampSharp platform API"},{"CD-02","CTF game-rules specification"},
      {"CD-03","combat/weapon-rules specification"},{"CD-04","weapon-catalog configuration"},
      {"CD-05","combo definitions"},{"CD-06","coin economy"},{"CD-07","GunGame mode rules"},
      {"CD-08","account & authentication policy"},{"CD-09","authorization policy"},
      {"CD-10","player-statistics/rank model"},{"CD-11","map configuration"},{"CD-12","map-rotation rules"},
      {"CD-13","chat rules"},{"CD-14","anti-cheat policy"},{"CD-15","command set"},
      {"CD-16","RCON security policy"},{"CD-17","game configuration/.env schema"},
      {"CD-18","database schema/player data model"},{"CD-19","MariaDB SQL dialect"},
      {"CD-20","outbound repository contract"},{"CD-21","DI container/composition"},
      {"CD-22","hosting/deployment spec"},{"CD-23","Serilog logging"},{"CD-24","Discord webhook contract"},
      {"CD-25","BCrypt password-hashing contract"},{"CD-26","NUnit test-framework contract"},
      {"CD-27","FluentAssertions contract"},{"CD-28","NSubstitute mock contract"},
      {"CD-30","SQLite SQL dialect"}
    };
    Map<String,String> label = new LinkedHashMap<>(); for (var l : L) label.put(l[0], l[1]);

    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src","tests"}) try (var s = Files.walk(Paths.get(root, r))) {
        s.filter(p -> p.toString().endsWith(".cs")).filter(p -> !p.toString().contains("/obj/") && !p.toString().contains("/bin/")).filter(p -> !p.getFileName().toString().equals("Usings.cs")).forEach(files::add);
    }
    for (Path f : files) {
        String c = Files.readString(f); Matcher nm = nsP.matcher(c); String ns=""; while (nm.find()) ns = nm.group(1);
        if (ns.isEmpty()) continue;
        String[] lines = c.split("\n",-1); Matcher tm = typeP.matcher(c);
        while (tm.find()) {
            String kind = tm.group(1), name = tm.group(2); int pos = tm.start(); int li=0, acc=0;
            for (int i=0;i<lines.length;i++){acc+=lines[i].length()+1; if(acc>pos){li=i;break;}}
            Set<String> d = new TreeSet<>();
            for (int j=li-1; j>=0 && j>=li-12; j--) {
                if (lines[j].contains("Change drivers:")) {
                    Matcher cm = cdP.matcher(lines[j]); while (cm.find()) d.add(cm.group());
                    break;
                }
            }
            if (!d.isEmpty()) elems.add(new Elem(ns, name, kind, d));
        }
    }

    Set<String> nsSet = new TreeSet<>(); for (Elem e : elems) nsSet.add(e.ns());
    Map<String,String> setKey = new HashMap<>();
    Map<String,Integer> setCard = new TreeMap<>();
    for (Elem e : elems) setCard.merge(String.join("+", e.drivers()), 1, Integer::sum);
    Map<String,Set<String>> nsOfSet = new TreeMap<>();
    for (Elem e : elems) nsOfSet.computeIfAbsent(String.join("+",e.drivers()), k->new TreeSet<>()).add(e.ns());
    long scattered = nsOfSet.values().stream().filter(s -> s.size() > 1).count();

    System.out.println("## census");
    Map<String,Integer> byKind = new TreeMap<>(); for (Elem e : elems) byKind.merge(e.kind(),1,Integer::sum);
    System.out.println("types=" + elems.size() + " by-kind=" + byKind + " namespaces=" + nsSet.size() + " distinct-sets=" + setCard.size() + " scattered-sets=" + scattered);

    System.out.println("## activation");
    Map<String,Set<String>> elemByDriver = new TreeMap<>(), nsByDriver = new TreeMap<>();
    for (Elem e : elems) for (String d : e.drivers()) {
        elemByDriver.computeIfAbsent(d, k->new TreeSet<>()).add(e.name());
        nsByDriver.computeIfAbsent(d, k->new TreeSet<>()).add(e.ns());
    }
    for (String d : label.keySet()) {
        int en = elemByDriver.getOrDefault(d, new TreeSet<>()).size();
        int nn = nsByDriver.getOrDefault(d, new TreeSet<>()).size();
        System.out.printf("| %s (%s) | %d | %d | %.2f |%n", d, label.get(d), en, nn, nn>0 ? en/(double)nn : 0);
    }

    System.out.println("## global");
    List<Integer> dpc = new ArrayList<>(); for (Elem e : elems) dpc.add(e.drivers().size());
    Collections.sort(dpc);
    Map<Integer,Integer> hist = new TreeMap<>(); for (int d : dpc) hist.merge(d,1,Integer::sum);
    Map<String,Set<String>> nsDrv = new TreeMap<>();
    for (Elem e : elems) nsDrv.computeIfAbsent(e.ns(), k->new TreeSet<>()).addAll(e.drivers());
    List<Integer> nsc = new ArrayList<>(); for (var v : nsDrv.values()) nsc.add(v.size());
    Collections.sort(nsc);
    System.out.printf("mean-dpc=%.2f median-dpc=%s mean-dpns=%.2f median-dpns=%s hist=%s%n",
        dpc.stream().mapToInt(Integer::intValue).average().orElse(0), median(dpc),
        nsc.stream().mapToInt(Integer::intValue).average().orElse(0), median(nsc), hist);

    System.out.println("## namespace-sets");
    Map<String,Set<String>> nsSets = new TreeMap<>();
    for (Elem e : elems) nsSets.computeIfAbsent(e.ns(), k->new TreeSet<>()).add(String.join("+",e.drivers()));
    long single = nsSets.values().stream().filter(s -> s.size()==1).count();
    for (var en : nsSets.entrySet()) {
        int classes = (int) elems.stream().filter(e -> e.ns().equals(en.getKey())).count();
        int toks = (int) nsDrv.get(en.getKey()).size();
        System.out.printf("| %s | %d | %d | %d |%n", en.getKey(), classes, toks, en.getValue().size());
    }
    System.out.println("composite=" + (nsSets.size()-single) + " single=" + single);

    System.out.println("## prod-test-split");
    long prod = elems.stream().filter(e -> !e.ns().startsWith("CTF.Application.Tests") && !e.ns().startsWith("Persistence.Tests")).count();
    System.out.println("production=" + prod + " test=" + (elems.size()-prod));
}
static String median(List<Integer> l){ int n=l.size(); if(n==0)return"0"; return n%2==1 ? ""+l.get(n/2) : ""+(l.get(n/2-1)+l.get(n/2))/2.0; }
