import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main(String[] args) throws IOException {
    record Elem(String ns, String name, Set<String> drivers) {}
    List<Elem> elems = new ArrayList<>();
    Pattern nsP = Pattern.compile("namespace\\s+([A-Za-z0-9_.]+)");
    Pattern typeP = Pattern.compile("(?m)^\\s*(?:(?:public|internal|private|protected)\\s+)?(?:(?:static|sealed|abstract|partial|readonly|ref)\\s+)*(class|interface|enum|record|struct)\\s+(\\w+)");
    Pattern cdP = Pattern.compile("CD-\\d{2}");

    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src","tests"}) {
        try (var s = Files.walk(Paths.get(r))) {
            s.filter(p -> p.toString().endsWith(".cs"))
             .filter(p -> !p.toString().contains("/obj/") && !p.toString().contains("/bin/"))
             .filter(p -> !p.getFileName().toString().equals("Usings.cs"))
             .forEach(files::add);
        }
    }
    for (Path f : files) {
        String c = Files.readString(f);
        Matcher nm = nsP.matcher(c); String ns=""; while(nm.find()) ns=nm.group(1);
        if(ns.isEmpty()) continue;
        String[] lines = c.split("\n",-1);
        Matcher tm = typeP.matcher(c);
        while(tm.find()){
            String tn=tm.group(2); int pos=tm.start(); int li=0,acc=0;
            for(int i=0;i<lines.length;i++){acc+=lines[i].length()+1; if(acc>pos){li=i;break;}}
            Set<String> drv=new TreeSet<>();
            for(int i=li;i>=0&&i>=li-12;i--){Matcher cm=cdP.matcher(lines[i]);while(cm.find())drv.add(cm.group()); if(lines[i].contains("Change drivers:"))break;}
            if(!drv.isEmpty()) elems.add(new Elem(ns,tn,drv));
        }
    }

    Map<String,String> label = new LinkedHashMap<>();
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
      {"CD-29","code-under-test contract"},{"CD-30","SQLite SQL dialect"}
    };
    for (var l : L) label.put(l[0], l[1]);

    StringBuilder sb = new StringBuilder();
    sb.append("# Capture-The-Flag — Change-Driver & Causal-Cohesion Metrics\n\n");
    Set<String> nsSet = new HashSet<>(); for (Elem e : elems) nsSet.add(e.ns());
    sb.append("> Regenerated from the fixed driver model (CD-19 = MariaDB SQL dialect, CD-30 = SQLite SQL dialect, tests included). ").append(elems.size()).append(" types across ").append(nsSet.size()).append(" namespaces, 30 change drivers.\n\n");

    // 1. driver activation table
    Map<String,Set<String>> elemByDriver = new TreeMap<>();
    Map<String,Set<String>> nsByDriver = new TreeMap<>();
    for(Elem e:elems) for(String d:e.drivers()){
        elemByDriver.computeIfAbsent(d,k->new TreeSet<>()).add(e.name());
        nsByDriver.computeIfAbsent(d,k->new TreeSet<>()).add(e.ns());
    }
    sb.append("## 1. Driver activation\n\n| Driver | Elements | Modules (namespaces) | Scatter ratio (elem/ns) |\n|---|---|---|---|\n");
    for(String d: label.keySet()){
        int e = elemByDriver.getOrDefault(d,new TreeSet<>()).size();
        int n = nsByDriver.getOrDefault(d,new TreeSet<>()).size();
        sb.append(String.format("| %s (%s) | %d | %d | %.2f |%n", d, label.get(d), e, n, e/(double)n));
    }
    sb.append("\n");

    // 2. global statistics
    List<Integer> dpc = new ArrayList<>(); for(Elem e:elems) dpc.add(e.drivers().size());
    Collections.sort(dpc);
    double meanDPC = dpc.stream().mapToInt(Integer::intValue).average().orElse(0);
    Map<Integer,Integer> hist = new TreeMap<>(); for(int d:dpc) hist.merge(d,1,Integer::sum);

    Map<String,Set<String>> nsDrv = new TreeMap<>();
    for(Elem e:elems) for(String d:e.drivers()) nsDrv.computeIfAbsent(e.ns(),k->new TreeSet<>()).add(d);
    List<Integer> nsc = new ArrayList<>(); for(var v:nsDrv.values()) nsc.add(v.size());
    Collections.sort(nsc);

    sb.append("## 2. Global statistics\n\n");
    sb.append("| Statistic | Value |\n|---|---|\n");
    sb.append("| Types with a change-driver annotation | ").append(elems.size()).append(" |\n");
    sb.append("| Namespaces | ").append(nsDrv.size()).append(" |\n");
    sb.append("| Mean change drivers per class | ").append(String.format("%.2f", meanDPC)).append(" |\n");
    sb.append("| Median change drivers per class | ").append(median(dpc)).append(" |\n");
    sb.append("| Mean change drivers per namespace | ").append(String.format("%.2f", nsc.stream().mapToInt(Integer::intValue).average().orElse(0))).append(" |\n");
    sb.append("| Median change drivers per namespace | ").append(median(nsc)).append(" |\n\n");
    sb.append("Drivers per class histogram: ").append(hist).append("\n\n");

    // 3. contamination: distinct driver SETS per namespace
    Map<String,Set<String>> nsSets = new TreeMap<>();
    for(Elem e:elems) nsSets.computeIfAbsent(e.ns(),k->new TreeSet<>()).add(String.join("+",e.drivers()));
    long single = nsSets.values().stream().filter(s->s.size()==1).count();
    long composite = nsSets.size()-single;
    sb.append("## 3. Namespace contamination (multi-change-driver-set mixes)\n\n");
    sb.append("| namespace | classes | distinct tokens | distinct sets | single-set? |\n|---|---|---|---|---|\n");
    Map<String,List<Elem>> byNs = new TreeMap<>();
    for(Elem e:elems) byNs.computeIfAbsent(e.ns(),k->new ArrayList<>()).add(e);
    for(var en: byNs.entrySet()){
        Set<String> toks=new TreeSet<>(); for(Elem e:en.getValue()) toks.addAll(e.drivers());
        Set<String> sets=new TreeSet<>(); for(Elem e:en.getValue()) sets.add(String.join("+",e.drivers()));
        sb.append(String.format("| %s | %d | %d | %d | %s |%n", en.getKey(), en.getValue().size(), toks.size(), sets.size(), sets.size()==1?"yes":"no"));
    }
    sb.append("\n").append(composite).append(" of ").append(nsSets.size()).append(" namespaces are composite; ").append(single).append(" are single-set.\n\n");

    // 4. causal cohesion per namespace (module): purity = 1/|sets|, completeness = min over A of |M ∩ [A]| / |[A]|
    sb.append("## 4. Causal cohesion per namespace\n\n");
    sb.append("Module M = namespace. purity(M) = 1 / (#distinct driver sets in M). completeness(M) = min over each driver-set A in M of |M ∩ [A]| / |[A]|, where [A] = system-wide elements with driver-set A.\n\n");
    sb.append("| namespace | classes | purity | completeness |\n|---|---|---|---|\n");
    // precompute [A] system-wide element count per driver-set
    Map<String,Integer> classCardinality = new HashMap<>();
    for(Elem e:elems) classCardinality.merge(String.join("+",e.drivers()),1,Integer::sum);
    for(var en: byNs.entrySet()){
        String ns = en.getKey(); List<Elem> l = en.getValue();
        Set<String> sets=new TreeSet<>(); for(Elem e:l) sets.add(String.join("+",e.drivers()));
        double purity = 1.0/sets.size();
        double completeness = 1.0;
        for(String A: sets){
            int inModule = 0; for(Elem e:l) if(String.join("+",e.drivers()).equals(A)) inModule++;
            int total = classCardinality.getOrDefault(A,1);
            double frac = (double)inModule/total;
            completeness = Math.min(completeness, frac);
        }
        sb.append(String.format("| %s | %d | %.3f | %.3f |%n", ns, l.size(), purity, completeness));
    }
    sb.append("\n");

    // 5. per-class drivers (full listing, compact)
    sb.append("## 5. Per-class driver assignment (multiset summary)\n\n");
    sb.append("Full per-element assignment lives in the source `<remarks>`; here the driver-set frequency per class count.\n\n");
    sb.append(hist).append("\n");

    String out = (args.length > 0) ? args[0] : "IVP/before/metrics.md";
    Files.writeString(Paths.get(out), sb.toString());
    System.out.println("wrote " + out + ", types=" + elems.size());
}

static String median(List<Integer> l){ int n=l.size(); if(n==0)return"0"; return n%2==1 ? ""+l.get(n/2) : ""+(l.get(n/2-1)+l.get(n/2))/2.0; }
