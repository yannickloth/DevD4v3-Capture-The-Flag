import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main() throws IOException {
    Pattern nsP = Pattern.compile("namespace\\s+([A-Za-z0-9_.]+)");
    Pattern typeP = Pattern.compile("(?m)^\\s*(?:(?:public|internal|private|protected)\\s+)?(?:(?:static|sealed|abstract|partial|readonly|ref)\\s+)*(?:record\\s+)?(class|interface|enum|struct)\\s+([A-Za-z0-9_]+)");
    Pattern cdP = Pattern.compile("CD-\\d{2}");
    Map<String,Set<String>> setNs = new TreeMap<>(); // gamma-set -> namespaces
    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src","tests"}) try(var s=Files.walk(Paths.get(r))) {
        s.filter(p->p.toString().endsWith(".cs")).filter(p->!p.toString().contains("/obj/")&&!p.toString().contains("/bin/")).filter(p->!p.getFileName().toString().equals("Usings.cs")).forEach(files::add);
    }
    for(Path f: files){ String c=Files.readString(f); Matcher nm=nsP.matcher(c);String ns="";while(nm.find())ns=nm.group(1); if(ns.isEmpty())continue;
        String[] lines=c.split("\n",-1); Matcher tm=typeP.matcher(c);
        while(tm.find()){ int p=tm.start(); int li=0,acc=0; for(int i=0;i<lines.length;i++){acc+=lines[i].length()+1;if(acc>p){li=i;break;}}
            Set<String> d=new TreeSet<>(); for(int j=li;j>=0&&j>=li-8;j--){Matcher cm=cdP.matcher(lines[j]);while(cm.find())d.add(cm.group()); if(lines[j].contains("Change drivers:"))break;}
            if(!d.isEmpty()) setNs.computeIfAbsent(String.join("+",d),k->new TreeSet<>()).add(ns);
        }
    }
    int scattered=0; for(var e:setNs.entrySet()) if(e.getValue().size()>1) scattered++;
    System.out.println("distinct gamma-sets=" + setNs.size());
    System.out.println("scattered (span >1 namespace)=" + scattered);
}
