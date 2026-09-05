import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

/** Top-level-only namespace/change-driver audit (robust vs file-scoped & block-scoped ns, BOM).
 *  A .cs type is top-level if its declaration sits inside the namespace body but NOT inside
 *  any other type body. We simulate brace depth, treating the namespace's own brace (block form)
 *  as the base. */
void main(String[] args) throws IOException {
    String root = (args.length > 0) ? args[0] : ".";
    record T(String ns, String name, String set, String file) {}
    List<T> types = new ArrayList<>();
    Pattern cdP = Pattern.compile("CD-\\d{2}");
    // Handle: class/interface/enum/struct/record, and 'record struct'/'record class'.
    // Capture group 2 = type name (last identifier before '(' or '{' or ':' or space).
    Pattern typeP = Pattern.compile(
        "\\b(public|internal|private|protected)\\s+(static\\s+|sealed\\s+|abstract\\s+|partial\\s+|readonly\\s+|ref\\s+|new\\s+)*" +
        "(class|interface|enum|struct|record)(\\s+(struct|class))?\\s+([A-Za-z_][A-Za-z0-9_]*)\\b");

    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src","tests"}) try (var s = Files.walk(Paths.get(root, r))) {
        s.filter(p -> p.toString().endsWith(".cs"))
         .filter(p -> !p.toString().contains("/obj/") && !p.toString().contains("/bin/"))
         .filter(p -> !p.getFileName().toString().equals("Usings.cs"))
         .forEach(files::add);
    }
    for (Path f : files) {
        List<String> raw;
        try { raw = Files.readAllLines(f); } catch (IOException e) { continue; }
        // strip BOM from first line
        List<String> lines = new ArrayList<>(raw);
        if (!lines.isEmpty() && lines.get(0).startsWith("\uFEFF")) lines.set(0, lines.get(0).substring(1));

        String ns = null;
        // Find namespace declaration (file-scoped 'namespace X;' or block 'namespace X {')
        int nsDepthBase = 0;
        int startIdx = -1;
        for (int i=0;i<lines.size();i++){
            Matcher m = Pattern.compile("\\s*namespace\\s+([A-Za-z0-9_.]+)\\s*(;|\\{)?").matcher(lines.get(i));
            // Require it to actually be a namespace declaration statement
            if (lines.get(i).contains("namespace ") && !lines.get(i).contains("using ")) {
                Matcher fm = Pattern.compile("namespace\\s+([A-Za-z0-9_.]+)\\s*;").matcher(lines.get(i));
                if (fm.find()) { ns = fm.group(1); startIdx = i; nsDepthBase = 0; break; }
                Matcher bm = Pattern.compile("namespace\\s+([A-Za-z0-9_.]+)\\s*\\{").matcher(lines.get(i));
                if (bm.find()) { ns = bm.group(1); startIdx = i; nsDepthBase = 1; break; }
            }
        }
        if (ns == null || startIdx < 0) continue;

        // Count braces from startIdx+1 onward (namespace line's own brace handled by nsDepthBase).
        int depth = nsDepthBase;
        String lastSet = "";
        for (int i=startIdx+1;i<lines.size();i++){
            String line = lines.get(i);
            if (line.contains("Change drivers:")) lastSet = drivers(line, cdP);
            // Type line? Must be declared at namespace-body depth (depth==nsDepthBase for file-scoped(0),
            // and for block-scoped depth==1 means directly in namespace, since nested types are at depth>=2).
            Matcher tm = typeP.matcher(line);
            if (tm.find() && depth == nsDepthBase) {
                String name = tm.group(6);
                // ensure not matched inside a doc comment string like '/// public class' - XML comments start with //
                String trimmed = line.trim();
                if (trimmed.startsWith("///")) { /* skip */ }
                else {
                    String set = "";
                    for (int j=i-1;j>=0 && j>=i-12;j--){
                        if (lines.get(j).contains("Change drivers:")){ set=drivers(lines.get(j),cdP); break; }
                    }
                    types.add(new T(ns, name, set.isEmpty()?name:set, f.toString()));
                }
            }
            depth += count(line,'{') - count(line,'}');
        }
    }

    Map<String, Map<String, List<T>>> byNs = new TreeMap<>();
    for (T t : types) byNs.computeIfAbsent(t.ns(), k-> new LinkedHashMap<>())
        .computeIfAbsent(t.set(), k-> new ArrayList<>()).add(t);

    System.out.println("TOTAL top-level types="+types.size());
    int composite=0, single=0;
    for (var en : byNs.entrySet()) { if (en.getValue().size()>1) composite++; else single++; }
    System.out.println("namespaces="+byNs.size()+" top-level-composite="+composite+" single="+single);
    for (var en : byNs.entrySet()) {
        if (en.getValue().size()>1){
            System.out.println("\n== "+en.getKey()+"  ("+en.getValue().size()+" top-level sets) ==");
            for (var e2:en.getValue().entrySet()){
                System.out.println("   ["+e2.getKey()+"]");
                for (T t: e2.getValue()) System.out.println("        "+t.name()+"   <- "+t.file());
            }
        }
    }
}
int count(String s, char ch){ int n=0; for(int i=0;i<s.length();i++) if(s.charAt(i)==ch) n++; return n; }
String drivers(String line, Pattern cdP){ TreeSet<String> s=new TreeSet<>(); Matcher m=cdP.matcher(line); while(m.find()) s.add(m.group()); return String.join("+",s); }
