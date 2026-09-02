import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main() throws IOException {
    record T(String ns, Set<String> drv){}
    Map<String,Set<String>> byNs = new TreeMap<>();
    Map<String,Integer> typeSetCount = new TreeMap<>();
    int types=0;
    Pattern nsP = Pattern.compile("namespace\\s+([A-Za-z0-9_.]+)");
    Pattern typeP = Pattern.compile("(?m)^\\s*(?:(?:public|internal|private|protected)\\s+)?(?:(?:static|sealed|abstract|partial|readonly|ref)\\s+)*(?:record\\s+)?(class|interface|enum|struct)\\s+([A-Za-z0-9_]+)");
    Pattern cdP = Pattern.compile("CD-\\d{2}");
    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src","tests"}) try(var s=Files.walk(Paths.get(r))) {
        s.filter(p->p.toString().endsWith(".cs")).filter(p->!p.toString().contains("/obj/")&&!p.toString().contains("/bin/")).filter(p->!p.getFileName().toString().equals("Usings.cs")).forEach(files::add);
    }
    for(Path f: files){ String c=Files.readString(f); Matcher nm=nsP.matcher(c);String ns="";while(nm.find())ns=nm.group(1); if(ns.isEmpty())continue;
        String[] lines=c.split("\n",-1); Matcher tm=typeP.matcher(c);
        for(int[] pos={0}; tm.find(); ) {
            String name=tm.group(2); int p=tm.start();
            // find line idx
            int li=0,acc=0; for(int i=0;i<lines.length;i++){acc+=lines[i].length()+1; if(acc>p){li=i;break;}}
            Set<String> d=new TreeSet<>(); for(int j=li;j>=0&&j>=li-8;j--){Matcher cm=cdP.matcher(lines[j]);while(cm.find())d.add(cm.group()); if(lines[j].contains("Change drivers:"))break;}
            if(!d.isEmpty()){ types++; byNs.computeIfAbsent(ns,k->new TreeSet<>()); typeSetCount.merge(String.join("+",d),1,Integer::sum); }
        }
    }
    System.out.println("classes(elements)=" + types);
    System.out.println("namespaces(modules, actual)=" + byNs.size());
    System.out.println("distinct change-driver SETS (Gamma-equivalence classes E/gamma)=" + typeSetCount.size());
}
