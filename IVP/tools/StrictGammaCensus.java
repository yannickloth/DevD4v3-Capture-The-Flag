import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

/**
 * Strict per-type member Gamma-set census.
 * A type is reported iff its DIRECT members carry >=2 distinct (order-insensitive) Gamma-sets.
 * Direct members = fields, consts, methods, properties, events, ctors, delegates, nested record
 * structs that carry a [ChangeDriversAttribute] immediately above them inside the type body.
 * Namespace ChangeDrivers markers and Designer files are excluded.
 * Nested classes count as their own modules (excluded from the parent).
 */
void main(String[] args) throws IOException {
    String root = args.length > 0 ? args[0] : ".";
    Pattern cdP = Pattern.compile("ChangeDriver\\.([A-Za-z_]+)");
    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src"}) try (var s = Files.walk(Paths.get(root, r))) {
        s.filter(p -> p.toString().endsWith(".cs"))
         .filter(p -> !p.toString().contains("/obj/") && !p.toString().contains("/bin/"))
         .filter(p -> !p.getFileName().toString().equals("Usings.cs"))
         .filter(p -> !p.getFileName().toString().endsWith(".Designer.cs"))
         .filter(p -> !p.getFileName().toString().equals("ChangeDrivers.cs"))
         .forEach(files::add);
    }
    int flagged = 0;
    for (Path f : files) {
        List<String> lines;
        try { lines = Files.readAllLines(f); } catch (Exception e) { continue; }
        for (Type t : parse(lines, cdP)) if (t.memberSets.size() >= 2) {
            flagged++;
            System.out.println(rel(f, root) + " :: " + t.kind + " " + t.name + "  sets=" + t.memberSets.size());
            for (String s : t.memberSets) System.out.println("     {" + s + "}");
        }
    }
    System.out.println("## strict impure types: " + flagged);
}
static String rel(Path f, String root) {
    Path r = Paths.get(root).toAbsolutePath().normalize();
    Path a = f.toAbsolutePath().normalize();
    return r.relativize(a).toString();
}
List<Type> parse(List<String> lines, Pattern cdP) {
    List<Type> all = new ArrayList<>();
    // scope stack: each entry = a type scope; tracks brace balance at which its body opened,
    // and whether the body has opened yet (a primary-constructor decl spans several delta-0
    // lines before its '{' — the scope must NOT pop during those).
    List<Type> stack = new ArrayList<>();
    List<Integer> balAtOpen = new ArrayList<>();
    List<Boolean> bodySeen = new ArrayList<>();
    int bal = 0;
    String pending = null;                 // attribute set awaiting its declaration
    Pattern declP = Pattern.compile("(?<![\\w.@])(class|struct|record|interface)\\s+([A-Za-z_][A-Za-z0-9_]*)\\b");
    for (String raw : lines) {
        String line = raw.strip();
        if (line.isEmpty() || line.startsWith("//")) continue;
        if (line.startsWith("[ChangeDriversAttribute(")) {
            pending = attrSet(line, cdP);
            continue;
        }
        Matcher dm = declP.matcher(line);
        if (dm.find()) {
            // bodyless declaration (e.g. 'record struct X(...);') — no members to collect
            boolean bodyless = line.contains(";") && delta(line) <= 0;
            pending = null;                 // attribute above a type decl is the type's own; not a member
            if (bodyless) continue;
            Type t = new Type(dm.group(1), dm.group(2));
            all.add(t);
            stack.add(t);
            balAtOpen.add(bal + delta(line));
            bodySeen.add(delta(line) > 0);
            continue;
        }
        if (pending != null && !stack.isEmpty()) {
            stack.get(stack.size() - 1).memberSets.add(pending);
            pending = null;
        }
        bal += delta(line);
        if (!stack.isEmpty() && bal > balAtOpen.get(balAtOpen.size() - 1)) {
            bodySeen.set(bodySeen.size() - 1, true);
        }
        // pop scopes whose body has opened and whose braces returned to the opening balance
        while (!stack.isEmpty() && bodySeen.get(bodySeen.size() - 1)
               && bal <= balAtOpen.get(balAtOpen.size() - 1)) {
            stack.remove(stack.size() - 1);
            balAtOpen.remove(balAtOpen.size() - 1);
            bodySeen.remove(bodySeen.size() - 1);
        }
    }
    return all;
}
String attrSet(String line, Pattern cdP){ TreeSet<String> s=new TreeSet<>(); Matcher m=cdP.matcher(line); while(m.find()) s.add(m.group(1)); return String.join("+", s); }
int delta(String line){ int n=0; for(int i=0;i<line.length();i++){char c=line.charAt(i); if(c=='{')n++; else if(c=='}')n--;} return n; }
static final class Type { String kind,name; Set<String> memberSets=new TreeSet<>(); Type(String k,String n){kind=k;name=n;} }
