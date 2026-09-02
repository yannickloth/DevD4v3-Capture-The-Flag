import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main() throws IOException {
    Pattern nsP = Pattern.compile("namespace\\s+([A-Za-z0-9_.]+)");
    Pattern decl = Pattern.compile("(?m)^[ \\t]*(?:(?:public|internal|private|protected|static|sealed|abstract|partial|readonly|ref|unsafe)[ \\t]+)*(?:record[ \\t]+)?(class|interface|enum|struct)[ \\t]+([A-Za-z_][A-Za-z0-9_]*)\\b");
    int prod=0, test=0;
    List<Path> files = new ArrayList<>();
    for (String r: new String[]{"src","tests"}) try(var s=Files.walk(Paths.get(r))) {
        s.filter(p->p.toString().endsWith(".cs")).filter(p->!p.toString().contains("/obj/")&&!p.toString().contains("/bin/")).filter(p->!p.getFileName().toString().equals("Usings.cs")).forEach(files::add);
    }
    for (Path f: files) {
        String c=Files.readString(f); Matcher nm=nsP.matcher(c); String ns=""; while(nm.find()) ns=nm.group(1);
        if(ns.isEmpty()) continue;
        String[] lines=c.split("\n",-1); Matcher m=decl.matcher(c);
        boolean isTest = f.toString().startsWith("tests/");
        while(m.find()){ int p=m.start(); int li=0,acc=0; for(int i=0;i<lines.length;i++){acc+=lines[i].length()+1;if(acc>p){li=i;break;}}
            for(int j=li;j>=0&&j>=li-8;j--){ if(lines[j].contains("Change drivers")){ if(isTest) test++; else prod++; break; } }
        }
    }
    System.out.println("production annotated types=" + prod + ", test annotated types=" + test + ", total=" + (prod+test));
}
