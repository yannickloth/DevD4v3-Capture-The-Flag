import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main() throws IOException {
    // TRUE contradiction: driver marked as "(root; X" AND appears on the LEFT of its own arrow ("CD-X (…) → Y").
    List<Path> files = new ArrayList<>();
    for (String r: new String[]{"src","tests"}) try(var s=Files.walk(Paths.get(r))) {
        s.filter(p->p.toString().endsWith(".cs")).filter(p->!p.toString().contains("/obj/")&&!p.toString().contains("/bin/")).forEach(files::add);
    }
    int bad=0;
    for (Path f: files) {
        int ln=0;
        for (String line : Files.readString(f).split("\n",-1)) { ln++;
            if (!line.contains("Change drivers:") || line.contains("Injected dependencies")) continue;
            Matcher um = Pattern.compile("(CD-\\d{2}) \\((root; )?[^)]*\\)(?: → (CD-\\d{2}))?").matcher(line);
            // simpler: find each "CD-XX (root; ...)" unit; then check if the same code appears as "CD-XX (...) → "
            Set<String> roots = new TreeSet<>();
            Matcher rm = Pattern.compile("(CD-\\d{2}) \\(root;").matcher(line);
            while (rm.find()) roots.add(rm.group(1));
            // left-side check: "CD-XX (label) → Y" where XX in roots
            Matcher lm = Pattern.compile("(CD-\\d{2}) \\([^)]*\\) → ").matcher(line);
            while (lm.find()) {
                if (roots.contains(lm.group(1))) {
                    System.out.println(f + ":" + ln + " TRUE contradiction: " + lm.group(1) + " both root and subordinated");
                    bad++;
                }
            }
        }
    }
    System.out.println("true-contradictions=" + bad);
}
