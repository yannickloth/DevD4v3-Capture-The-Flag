import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main() throws IOException {
    // extract co-occurring driver pairs (unordered) from flat remarks
    Pattern cdP = Pattern.compile("CD-\\d{2}");
    Map<String,Integer> pairCount = new TreeMap<>();
    List<Path> files = new ArrayList<>();
    for (String r: new String[]{"src","tests"}) try(var s=Files.walk(Paths.get(r))) {
        s.filter(p->p.toString().endsWith(".cs")).filter(p->!p.toString().contains("/obj/")&&!p.toString().contains("/bin/")).forEach(files::add);
    }
    for (Path f: files) {
        for (String ln : Files.readString(f).split("\n",-1)) {
            if (!ln.contains("Change drivers:")) continue;
            Matcher m = cdP.matcher(ln);
            Set<String> s = new TreeSet<>(); while(m.find()) s.add(m.group());
            List<String> l = new ArrayList<>(s);
            for (int i=0;i<l.size();i++) for (int j=i+1;j<l.size();j++) {
                String k = l.get(i)+"‖"+l.get(j); pairCount.merge(k,1,Integer::sum);
            }
        }
    }
    // print pairs sorted by co-occurrence count desc
    pairCount.entrySet().stream().sorted((a,b)->Integer.compare(b.getValue(),a.getValue()))
        .limit(60)
        .forEach(e->System.out.println(e.getValue()+"  "+e.getKey()));
}
