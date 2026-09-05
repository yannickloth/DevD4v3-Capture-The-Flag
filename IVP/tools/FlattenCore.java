import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

/** Flatten the CTF.Application .Core namespace level into its parent and update usings/references. */
void main(String[] args) throws IOException {
    Path root = Paths.get(args.length > 0 ? args[0] : ".");

    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src","tests"}) try (var s = Files.walk(root.resolve(r))) {
        s.filter(p -> p.toString().endsWith(".cs"))
         .filter(p -> !p.toString().contains("/obj/") && !p.toString().contains("/bin/"))
         .forEach(files::add);
    }

    Pattern nsFile = Pattern.compile("namespace (CTF\\.Application[A-Za-z0-9_.]*)\\.Core;");
    Pattern nsBlock = Pattern.compile("namespace (CTF\\.Application[A-Za-z0-9_.]*)\\.Core\\s*\\{");
    Pattern usingCore = Pattern.compile("(global\\s+using\\s+|using\\s+)(CTF\\.Application[A-Za-z0-9_.]*)\\.Core;");
    Pattern qualified = Pattern.compile("(CTF\\.Application[A-Za-z0-9_.]*)\\.Core\\.");

    int nsChanges = 0, usingChanges = 0, qChanges = 0;
    for (Path f : files) {
        String c = Files.readString(f);
        String out = c;
        Matcher m;
        boolean changed = false;
        m = nsFile.matcher(out); if (m.find()) { out = m.replaceAll("namespace $1;"); changed = true; }
        m = nsBlock.matcher(out); if (m.find()) { out = m.replaceAll("namespace $1 {"); changed = true; }
        m = usingCore.matcher(out); if (m.find()) { out = m.replaceAll("$1$2;"); changed = true; }
        m = qualified.matcher(out); if (m.find()) { out = m.replaceAll("$1."); changed = true; }
        if (changed && !out.equals(c)) {
            Files.writeString(f, out);
            nsChanges += (nsFile.matcher(c).find()?1:0) + (nsBlock.matcher(c).find()?1:0);
            Matcher u = usingCore.matcher(c); while (u.find()) usingChanges++;
            Matcher q = qualified.matcher(c); while (q.find()) qChanges++;
        }
    }
    System.out.println("namespaces=" + nsChanges + " usings=" + usingChanges + " qualified=" + qChanges);

    // Deduplicate identical lines in every Usings.cs (remove later duplicates, keep order).
    for (String r : new String[]{"src","tests"}) try (var s = Files.walk(root.resolve(r))) {
        s.filter(p -> p.toString().endsWith("Usings.cs")).forEach(p -> {
            try {
                List<String> lines = new ArrayList<>(Files.readAllLines(p));
                List<String> seen = new ArrayList<>();
                boolean deduped = false;
                List<String> out = new ArrayList<>();
                for (String line : lines) {
                    if (line.startsWith("global using ") && !line.contains(" = ") && seen.contains(line)) { deduped = true; continue; }
                    if (line.startsWith("global using ") && !line.contains(" = ")) seen.add(line);
                    out.add(line);
                }
                if (deduped) Files.write(p, out);
            } catch (IOException ex) { throw new RuntimeException(ex); }
        });
    }
    System.out.println("Done.");
}
