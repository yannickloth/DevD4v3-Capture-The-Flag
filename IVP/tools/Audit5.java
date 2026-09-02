import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main() throws IOException {
    // For each remark: every 'X → Y' must have Y present as 'Y (' in the same remark.
    List<Path> files = new ArrayList<>();
    for (String r: new String[]{"src","tests"}) try(var s=Files.walk(Paths.get(r))) {
        s.filter(p->p.toString().endsWith(".cs")).filter(p->!p.toString().contains("/obj/")&&!p.toString().contains("/bin/")).forEach(files::add);
    }
    int bad=0;
    for (Path f: files) {
        int ln=0;
        for (String line : Files.readString(f).split("\n",-1)) { ln++;
            if (!line.contains("Change drivers:") || line.contains("Injected dependencies")) continue;
            Matcher t = Pattern.compile("→ (CD-\\d{2})").matcher(line);
            while (t.find()) {
                String target = t.group(1);
                if (!line.contains(target + " (")) {
                    System.out.println(f + ":" + ln + " dangling → " + target);
                    bad++;
                }
            }
        }
    }
    System.out.println("dangling-subordination=" + bad);
}
