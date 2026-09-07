import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

/** Finds classes whose methods carry two+ distinct ChangeDrivers chains (unambiguous single-module violations). */
void main(String[] args) throws IOException {
    String root = args.length > 0 ? args[0] : ".";
    Pattern cdP = Pattern.compile("ChangeDriver\\.([A-Za-z]+)");
    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src"}) try (var s = Files.walk(Paths.get(root, r))) {
        s.filter(p -> p.toString().endsWith(".cs"))
         .filter(p -> !p.toString().contains("/obj/") && !p.toString().contains("/bin/"))
         .filter(p -> !p.getFileName().toString().equals("Usings.cs"))
         .forEach(files::add);
    }
    for (Path f : files) {
        List<String> lines;
        try { lines = Files.readAllLines(f); } catch (Exception e) { continue; }
        // find top-level class declarations (not nested), collecting their method-level attribute sets
        int classDepth = 0; String curClass = null;
        Map<String, Set<String>> classMemberChains = new LinkedHashMap<>(); // class simple name -> sets seen on methods
        // crude brace-depth tracking
        int depth = 0;
        for (int i = 0; i < lines.size(); i++) {
            String line = lines.get(i);
            Matcher classM = Pattern.compile("(public|internal|private)\\s+(static\\s+|sealed\\s+|abstract\\s+|partial\\s+)*(class|struct|record|interface)\\s+([A-Za-z_][A-Za-z0-9_]*)").matcher(line);
            if (classM.find() && depth == 0) {
                String name = classM.group(4);
                curClass = name;
                classMemberChains.put(name, new TreeSet<>());
            }
            // method attribute (line is the [ChangeDriversAttribute...] directly above a method at class depth)
            if (line.trim().startsWith("[ChangeDriversAttribute") && curClass != null) {
                // next non-blank line tells if it's a method (has ( ) and ; absent) — approximate: method decl has '(' and not '(' only params
                classMemberChains.get(curClass).add(extract(line, cdP));
            }
            depth += count(line,'{') - count(line,'}');
        }
        for (var en : classMemberChains.entrySet()) {
            if (en.getValue().size() >= 2) {
                System.out.println(f + " :: " + en.getKey() + "  distinct-member-chains=" + en.getValue().size());
                for (String c : en.getValue()) System.out.println("     " + c);
            }
        }
    }
}
String extract(String line, Pattern cdP) {
    List<String> ids = new ArrayList<>();
    Matcher m = cdP.matcher(line); while (m.find()) ids.add(m.group(1));
    return String.join("+", ids);
}
int count(String s, char ch){ int n=0; for(int i=0;i<s.length();i++) if(s.charAt(i)==ch)n++; return n; }
