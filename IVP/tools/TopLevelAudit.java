import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

/** Top-level namespace/change-driver audit: for each namespace, group top-level types by
 *  exact change-driver set and report namespaces whose top-level types span >1 set. */
void main(String[] args) throws IOException {
    String root = (args.length > 0) ? args[0] : ".";
    record T(String ns, String name, String set) {}
    List<T> types = new ArrayList<>();
    Pattern cdP = Pattern.compile("CD-\\d{2}");
    Pattern typeP = Pattern.compile("(?m)^\\s*(public|internal|private|protected)\\s+(static\\s+|sealed\\s+|abstract\\s+|partial\\s+|readonly\\s+|ref\\s+)*(class|interface|enum|struct|record)\\s+([A-Za-z_][A-Za-z0-9_]*)\\b");

    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src","tests"}) try (var s = Files.walk(Paths.get(root, r))) {
        s.filter(p -> p.toString().endsWith(".cs"))
         .filter(p -> !p.toString().contains("/obj/") && !p.toString().contains("/bin/"))
         .filter(p -> !p.getFileName().toString().equals("Usings.cs"))
         .forEach(files::add);
    }
    for (Path f : files) {
        String c = Files.readString(f);
        String ns = "";
        for (String line : c.split("\n")) {
            if (line.matches("\\s*namespace\\s+[A-Za-z0-9_.]+\\s*;")) {
                ns = line.replaceAll(".*namespace\\s+([A-Za-z0-9_.]+)\\s*;.*", "$1").trim();
            }
        }
        // file-scoped namespace only
        Matcher nsFile = Pattern.compile("namespace\\s+([A-Za-z0-9_.]+)\\s*;").matcher(c);
        if (!nsFile.find()) continue;
        ns = nsFile.group(1);
        String[] lines = c.split("\n", -1);
        int depth = 0;
        // determine base depth: after namespace line, top-level types are at some base indentation.
        // Find first '{' that opens a class; simpler: a type is top-level if it is declared
        // before we have entered any class body (depth counted only by braces after the file starts).
        int[] braceDepth = {0};
        java.util.function.BiConsumer<String,Integer> scan = null;
        int curDepth = 0;
        boolean seenNs = false;
        int nsLineIdx = -1;
        for (int i=0;i<lines.length;i++){ if (lines[i].startsWith("namespace ")){nsLineIdx=i; break;} }
        // Count braces only between namespace and first top-level type is hard; use heuristic:
        // top-level types are those declared while braceDepth==0 counting from after the namespace decl.
        int depthCounter = 0;
        for (int i=0;i<lines.length;i++){
            String line = lines[i];
            if (i==0 && line.isBlank()) continue;
            // skip namespace line
            if (nsLineIdx>=0 && i==nsLineIdx) continue;
            Matcher tm = typeP.matcher(line);
            int open = count(line,'{'), close = count(line,'}');
            boolean isType = tm.find();
            if (isType && depthCounter==0) {
                String name = tm.group(4);
                // find Change drivers above
                String set = "";
                for (int j=i-1;j>=0 && j>=i-6;j--){
                    if (lines[j].contains("Change drivers:")){ set=drivers(lines[j],cdP); break; }
                }
                types.add(new T(ns, name, set.isEmpty()?name:set));
            }
            depthCounter += open - close;
            if (depthCounter<0) depthCounter=0;
        }
    }

    Map<String, Map<String, List<String>>> byNs = new TreeMap<>();
    for (T t : types) byNs.computeIfAbsent(t.ns(), k-> new LinkedHashMap<>()).computeIfAbsent(t.set(), k-> new ArrayList<>()).add(t.name());

    int composite=0, single=0, total=0;
    for (var en : byNs.entrySet()) { total++; if (en.getValue().size()>1) composite++; else single++; }
    System.out.println("namespaces="+total+" top-level-composite="+composite+" single="+single);
    for (var en : byNs.entrySet()) {
        if (en.getValue().size()>1){
            System.out.println("\n"+en.getKey()+"  ("+en.getValue().size()+" top-level sets):");
            for (var e2:en.getValue().entrySet()) System.out.println("  ["+e2.getKey()+"] "+e2.getValue());
        }
    }
}
int count(String s, char ch){ int n=0; for(int i=0;i<s.length();i++) if(s.charAt(i)==ch) n++; return n; }
String drivers(String line, Pattern cdP){ TreeSet<String> s=new TreeSet<>(); Matcher m=cdP.matcher(line); while(m.find()) s.add(m.group()); return String.join("+",s); }
