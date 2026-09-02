import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main() throws IOException {
    List<Path> files = new ArrayList<>();
    for (String r: new String[]{"src","tests"}) try(var s=Files.walk(Paths.get(r))) {
        s.filter(p->p.toString().endsWith(".cs")).filter(p->!p.toString().contains("/obj/")&&!p.toString().contains("/bin/")).forEach(files::add);
    }
    int bad=0;
    for (Path f: files) {
        int ln=0;
        for (String line : Files.readString(f).split("\n",-1)) { ln++;
            if (!line.contains("‖")) continue;
            // each side of ‖ should be a root-marked unit "CD-XX (root; ...)"
            String[] segs = line.split("‖");
            for (String seg : segs) {
                seg = seg.trim();
                if (seg.startsWith("CD-") && !seg.contains("(root;") && !seg.startsWith("CD-0") ) continue;
                Matcher m = Pattern.compile("^(CD-\\d{2}) \\(").matcher(seg);
                if (m.find() && !seg.contains("(root;")) {
                    // segment begins with a driver unit lacking root mark
                    System.out.println(f + ":" + ln + " sibling without root mark: " + seg.substring(0, Math.min(40, seg.length())));
                    bad++;
                }
            }
        }
    }
    System.out.println("sibling-without-root=" + bad);
}
