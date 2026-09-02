import java.nio.file.*;
import java.util.regex.*;

void main() throws IOException {
    // for each production .cs (src), count member declarations vs 'Change drivers' remarks;
    // a member = a line with "public|private|internal|protected ... method/prop" pattern. Approximate via method-like decls.
    int files=0, underAnnotated=0;
    var member = Pattern.compile("(?m)^\\s{4}(?:public|private|internal|protected)\\s+(?:static\\s+|readonly\\s+|sealed\\s+|override\\s+|virtual\\s+|async\\s+)*(?![\\w\\s]*class\\b)[\\w<>,?\\[\\]]+\\s+\\w+\\s*[({=]");
    try (var s = Files.walk(Paths.get("src"))) {
        for (var p : (Iterable<Path>) s.filter(x->x.toString().endsWith(".cs")&&!x.toString().contains("/obj/")&&!x.toString().contains("/bin/")&&!x.getFileName().toString().equals("Usings.cs"))::iterator) {
            String c = Files.readString(p);
            String[] lines = c.split("\n");
            int members=0, remarks=0;
            for (String l : lines) {
                if (member.matcher(l).find()) members++;
                if (l.contains("Change drivers:")) remarks++;
            }
            files++;
            if (members > remarks) { underAnnotated++; System.out.println("UNDER-ANNOTATED " + p + ": members~" + members + " remarks=" + remarks); }
        }
    }
    System.out.println("files=" + files + " underAnnotated=" + underAnnotated);
}
