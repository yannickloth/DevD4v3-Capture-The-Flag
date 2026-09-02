import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main() throws IOException {
    record Elem(String ns, String name, Set<String> drv){}
    List<Elem> elems = new ArrayList<>();
    Pattern nsP = Pattern.compile("namespace\\s+([A-Za-z0-9_.]+)");
    Pattern decl = Pattern.compile("(?m)^[ \\t]*(?:(?:public|internal|private|protected|static|sealed|abstract|partial|readonly|ref|unsafe)[ \\t]+)*(?:record[ \\t]+)?(class|interface|enum|struct)[ \\t]+([A-Za-z_][A-Za-z0-9_]*)\\b");
    Pattern cdP = Pattern.compile("CD-\\d{2}");
    List<Path> files = new ArrayList<>();
    for (String r: new String[]{"src","tests"}) try(var s=Files.walk(Paths.get(r))) {
        s.filter(p->p.toString().endsWith(".cs")).filter(p->!p.toString().contains("/obj/")&&!p.toString().contains("/bin/")).filter(p->!p.getFileName().toString().equals("Usings.cs")).forEach(files::add);
    }
    for (Path f: files) {
        String c=Files.readString(f); Matcher nm=nsP.matcher(c); String ns=""; while(nm.find()) ns=nm.group(1); if(ns.isEmpty()) continue;
        String[] lines=c.split("\n",-1); Matcher m=decl.matcher(c);
        while(m.find()){ int p=m.start(); int li=0,acc=0; for(int i=0;i<lines.length;i++){acc+=lines[i].length()+1;if(acc>p){li=i;break;}}
            Set<String> d=new TreeSet<>(); for(int j=li;j>=0&&j>=li-8;j--){Matcher cm=cdP.matcher(lines[j]);while(cm.find())d.add(cm.group()); if(lines[j].contains("Change drivers"))break;}
            if(!d.isEmpty()) elems.add(new Elem(ns, m.group(2), d));
        }
    }
    // system-wide type-set cardinality
    Map<String,Integer> setCard = new HashMap<>();
    for (Elem e: elems) setCard.merge(String.join("+", e.drv()),1,Integer::sum);
    // group by namespace
    Map<String,List<Elem>> byNs = new TreeMap<>();
    for (Elem e: elems) byNs.computeIfAbsent(e.ns(), k->new ArrayList<>()).add(e);

    List<String[]> rows = new ArrayList<>();
    for (var en: byNs.entrySet()) {
        String ns = en.getKey(); List<Elem> l = en.getValue();
        Set<String> toks = new TreeSet<>(); for (Elem e: l) toks.addAll(e.drv());
        Set<String> sets = new TreeSet<>(); for (Elem e: l) sets.add(String.join("+",e.drv()));
        double purity = 1.0/sets.size();
        double completeness = 1.0;
        for (String A: sets) { int in=0; for(Elem e:l) if(String.join("+",e.drv()).equals(A)) in++; completeness=Math.min(completeness, (double)in/setCard.getOrDefault(A,1)); }
        rows.add(new String[]{ns, String.valueOf(l.size()), String.valueOf(toks.size()), String.valueOf(sets.size()), String.format("%.3f",purity), String.format("%.3f",completeness)});
    }
    rows.sort((a,b)->Double.compare(Double.parseDouble(a[4]), Double.parseDouble(b[4])));
    System.out.println("| namespace | classes | tokens | sets | purity | completeness |");
    System.out.println("|---|---|---|---|---|---|");
    for (String[] r: rows) System.out.println("| " + r[0] + " | " + r[1] + " | " + r[2] + " | " + r[3] + " | " + r[4] + " | " + r[5] + " |");
}
