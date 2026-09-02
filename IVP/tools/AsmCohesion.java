import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main() throws IOException {
    Pattern nsP = Pattern.compile("namespace\\s+([A-Za-z0-9_.]+)");
    Pattern decl = Pattern.compile("(?m)^[ \\t]*(?:(?:public|internal|private|protected|static|sealed|abstract|partial|readonly|ref|unsafe)[ \\t]+)*(?:record[ \\t]+)?(class|interface|enum|struct)[ \\t]+([A-Za-z_][A-Za-z0-9_]*)\\b");
    Pattern cdP = Pattern.compile("CD-\\d{2}");
    // assembly = project dir: src/Application, src/Host, src/Persistence/Persistence.InMemory, ... tests/Application.Tests, tests/Persistence.Tests
    Map<String,Set<String>> asmSets = new TreeMap<>(); Map<String,Integer> asmCount = new TreeMap<>();
    List<Path> files = new ArrayList<>();
    for (String r: new String[]{"src","tests"}) try(var s=Files.walk(Paths.get(r))) {
        s.filter(p->p.toString().endsWith(".cs")).filter(p->!p.toString().contains("/obj/")&&!p.toString().contains("/bin/")).filter(p->!p.getFileName().toString().equals("Usings.cs")).forEach(files::add);
    }
    for (Path f: files) {
        String asm = assemblyOf(f.toString());
        String c=Files.readString(f); Matcher nm=nsP.matcher(c); String ns=""; while(nm.find()) ns=nm.group(1); if(ns.isEmpty()) continue;
        String[] lines=c.split("\n",-1); Matcher m=decl.matcher(c);
        while(m.find()){ int p=m.start(); int li=0,acc=0; for(int i=0;i<lines.length;i++){acc+=lines[i].length()+1;if(acc>p){li=i;break;}}
            Set<String> d=new TreeSet<>(); for(int j=li;j>=0&&j>=li-8;j--){Matcher cm=cdP.matcher(lines[j]);while(cm.find())d.add(cm.group()); if(lines[j].contains("Change drivers"))break;}
            if(!d.isEmpty()){ asmSets.computeIfAbsent(asm,k->new TreeSet<>()).add(String.join("+",d)); asmCount.merge(asm,1,Integer::sum); }
        }
    }
    System.out.println("| assembly | classes | distinct type-driver-sets | purity |");
    System.out.println("|---|---|---|---|");
    for (var e: asmSets.entrySet()) System.out.println("| " + e.getKey() + " | " + asmCount.get(e.getKey()) + " | " + e.getValue().size() + " | " + String.format("%.3f",1.0/e.getValue().size()) + " |");
}
String assemblyOf(String p) {
    // src/Application -> CTF.Application ; src/Persistence/Persistence.InMemory -> Persistence.InMemory ; tests/Application.Tests -> CTF.Application.Tests ; tests/Persistence.Tests -> Persistence.Tests
    if (p.startsWith("src/Application")) return "src/Application";
    if (p.startsWith("src/Host")) return "src/Host";
    if (p.startsWith("src/Persistence/Persistence.InMemory")) return "Persistence.InMemory";
    if (p.startsWith("src/Persistence/Persistence.MariaDB")) return "Persistence.MariaDB";
    if (p.startsWith("src/Persistence/Persistence.SQLite")) return "Persistence.SQLite";
    if (p.startsWith("tests/Application.Tests")) return "tests/Application.Tests";
    if (p.startsWith("tests/Persistence.Tests")) return "tests/Persistence.Tests";
    return "OTHER-" + p;
}
