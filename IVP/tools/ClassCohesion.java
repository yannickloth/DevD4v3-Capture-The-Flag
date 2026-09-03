import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main() throws IOException {
    // For each class in src+tests, treat its MEMBERS as elements; driver set per member = CD codes on the member's own
    // nearest preceding 'Change drivers:' remark line within 6 lines (that line only; 'Injected dependencies' lines never contribute).
    // purity(class) = 1 / #distinct member-driver-sets; completeness = min over set A of (members with A / system-wide members with A);
    // 1-HHI = 1 - sum over sets A of (share_A^2).
    // NOTE: system-wide [A] = count of MEMBERS (not types) with driver set A.
    record SetCount() {}
    // We'll aggregate at member level across the whole codebase, then per class.
    record Attrib(String cls, String member, Set<String> drivers) {}
    List<Attrib> memberAttrs = new ArrayList<>();
    Pattern typeP = Pattern.compile("(?m)^\\s*(?:(?:public|internal|private|protected)\\s+)?(?:(?:static|sealed|abstract|partial|readonly|ref)\\s+)*(class|interface|enum|record|struct)\\s+(\\w+)");
    Pattern memberP = Pattern.compile("(?m)^\\s{4}(?:public|private|internal|protected)\\s+(?:static\\s+|readonly\\s+|sealed\\s+|override\\s+|virtual\\s+|async\\s+)*[\\w<>,?\\[\\]]+\\s+(\\w+)\\s*[({=]");
    Pattern cdP = Pattern.compile("CD-\\d{2}");

    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src","tests"}) try (var s=Files.walk(Paths.get(r))) {
        s.filter(p->p.toString().endsWith(".cs")).filter(p->!p.toString().contains("/obj/")&&!p.toString().contains("/bin/")).filter(p->!p.getFileName().toString().equals("Usings.cs")).forEach(files::add);
    }
    for (Path f : files) {
        String c = Files.readString(f);
        String[] lines = c.split("\n",-1);
        // determine current class per line
        String curCls = "";
        List<String> clsStack = new ArrayList<>();
        Matcher tm = typeP.matcher(c);
        // simpler: track class by scanning lines; member remark is nearest preceding 'Change drivers' line
        for (int i=0;i<lines.length;i++) {
            String ln = lines[i];
            // update current class (top-level-ish)
            Matcher cm = typeP.matcher(ln);
            if (cm.find() && (ln.contains("class")||ln.contains("interface")||ln.contains("enum")||ln.contains("record")||ln.contains("struct"))) {
                curCls = cm.group(2);
            }
            if (memberP.matcher(ln).find()) {
                // STRICT: nearest preceding 'Change drivers:' remark (within 6 lines) — that line only
                Set<String> drv = new TreeSet<>();
                for (int j=i-1;j>=0 && j>=i-6;j--) {
                    if (lines[j].contains("Change drivers:")) {
                        Matcher dm = cdP.matcher(lines[j]);
                        while (dm.find()) drv.add(dm.group());
                        break;
                    }
                }
                if (!drv.isEmpty()) memberAttrs.add(new Attrib(curCls, curCls+"."+(i+1), drv));
            }
        }
    }
    // system-wide member count per driver set
    Map<String,Integer> setCard = new HashMap<>();
    Map<String, List<Attrib>> byCls = new TreeMap<>();
    for (Attrib a : memberAttrs) {
        setCard.merge(String.join("+",a.drivers()),1,Integer::sum);
        byCls.computeIfAbsent(a.cls(),k->new ArrayList<>()).add(a);
    }
    System.out.println("total member-level attributions=" + memberAttrs.size());
    System.out.println("classes with member attributions=" + byCls.size());
    // per class cohesion
    System.out.println("CLASS | members | purity | completeness | extent(1-HHI)");
    List<String[]> rows = new ArrayList<>();
    for (var e : byCls.entrySet()) {
        String cls = e.getKey(); List<Attrib> mems = e.getValue();
        Set<String> sets = new TreeSet<>(); for (Attrib a:mems) sets.add(String.join("+",a.drivers()));
        double purity = 1.0/sets.size();
        double completeness = 1.0;
        double hhi = 0.0;
        for (String A: sets) {
            int inClass=0; for(Attrib a:mems) if(String.join("+",a.drivers()).equals(A)) inClass++;
            double frac=(double)inClass/setCard.getOrDefault(A,1);
            completeness=Math.min(completeness, frac);
            double p=(double)inClass/mems.size();
            hhi += p*p;
        }
        double extent = 1.0 - hhi;
        rows.add(new String[]{cls, String.valueOf(mems.size()), String.format("%.3f",purity), String.format("%.3f",completeness), String.format("%.3f",extent)});
    }
    // sort by purity asc (most contaminated first)
    rows.sort((a,b)->Double.compare(Double.parseDouble(a[2]), Double.parseDouble(b[2])));
    for (String[] r : rows) System.out.println(r[0]+" | "+r[1]+" | "+r[2]+" | "+r[3]+" | "+r[4]);
}
