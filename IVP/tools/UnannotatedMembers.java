import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main() throws IOException {
    // A member = indented line matching "    (public|private|internal|protected|static|const|readonly|override|virtual|async|partial)... 
    // that is NOT a type declaration, and has NO 'Change drivers' within 8 lines above.
    Pattern member = Pattern.compile("^\\s{4}(?:(?:public|private|internal|protected)\\s+)?(?:(?:static|readonly|const|sealed|override|virtual|async|partial|new)\\s+)*[\\w<>,?.\\[\\]]+\\s+(\\w+)\\s*([({=;]|=>)");
    List<Path> files = new ArrayList<>();
    for (String r: new String[]{"src","tests"}) try(var s=Files.walk(Paths.get(r))) {
        s.filter(p->p.toString().endsWith(".cs")).filter(p->!p.toString().contains("/obj/")&&!p.toString().contains("/bin/")).filter(p->!p.getFileName().toString().equals("Usings.cs")).forEach(files::add);
    }
    int miss=0, total=0;
    Map<String,Integer> missByFile = new TreeMap<>();
    for (Path f: files) {
        String c=Files.readString(f);
        String[] lines=c.split("\n",-1);
        for (int i=0;i<lines.length;i++) {
            String ln=lines[i];
            if (ln.trim().startsWith("//") || ln.trim().startsWith("///")) continue;
            if (ln.contains("(class|interface|enum|struct)") || ln.matches("^\\s{0,4}(public|internal)?\\s*(sealed |abstract |partial |static |readonly )*(class|interface|enum|struct)\\b.*")) continue;
            Matcher m=member.matcher(ln);
            if (!m.find()) continue;
            total++;
            boolean has=false; for(int j=i;j>=0&&j>=i-8;j--){ if(lines[j].contains("Change drivers")){has=true;break;} }
            if (!has) { miss++; missByFile.merge(f.getFileName().toString(),1,Integer::sum); }
        }
    }
    System.out.println("member-like lines=" + total + ", unannotated=" + miss);
    for (var e : missByFile.entrySet()) if (e.getValue()>=2) System.out.println("  " + e.getKey() + ": " + e.getValue());
}
