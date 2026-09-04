import java.nio.file.*;
import java.util.*;
import java.util.regex.*;
import java.util.stream.*;

/** Flat module-purity evaluation under the strict single-set rule, with
 *  per-using-function dependency transmission.
 *
 *  Model:
 *  - Elements: direct members of a class (4-space indent; nested classes are
 *    modules on their own and are excluded from the parent).
 *  - Change drivers are inherited against dependency direction: each member that
 *    *uses* an injected dependency inherits that dependency's contract driver(s)
 *    (from the class-level "Injected dependencies ... name -> CD-xx" meta remark;
 *    CD-21 is wiring, not a driver). Members that never touch the dependency do
 *    not inherit it.
 *  - Module gamma = union of the direct members' effective sets.
 *  - SINGLE-SET iff all effective member sets are identical (and non-empty).
 *
 *  Run from repo root: java IVP/tools/ModulePurity.java [rootDir]
 */
void main(String[] args) throws IOException {
    String root = args.length > 0 ? args[0] : ".";
    record Viol(String cls, String file, int members, Map<String, Set<String>> effective, Set<String> injected) {}
    List<Viol> viols = new ArrayList<>();
    int checked = 0, pure = 0, skipped = 0;

    Pattern memberP = Pattern.compile("^\\s{4}(?:public|private|internal|protected)\\s.*");
    Pattern cdP = Pattern.compile("CD-\\d+");
    Pattern depP = Pattern.compile("(\\w+)\\s*->\\s*(CD-\\d+)");
    Pattern classP = Pattern.compile("^(?:public|internal|private|protected)?[\\w<>]*\\s*(?:sealed |static |abstract |partial )*class\\s+(\\w+)");

    List<Path> files = new ArrayList<>();
    try (var s = Files.walk(Paths.get(root, "src")); var s2 = Files.walk(Paths.get(root, "tests"))) {
        s.filter(p -> p.toString().endsWith(".cs")).forEach(files::add);
        s2.filter(p -> p.toString().endsWith(".cs")).forEach(files::add);
    }
    files.removeIf(p -> p.toString().contains("/obj/") || p.toString().contains("/bin/")
            || p.getFileName().toString().equals("Usings.cs") || p.getFileName().toString().endsWith("Designer.cs"));

    for (Path f : files) {
        String[] lines = Files.readString(f).split("\n", -1);
        int depth = 0;
        String cls = null;
        int clsDepth = 0;
        Map<String, Set<String>> injected = new LinkedHashMap<>(); // field -> contract drivers
        String pendingRemark = "";

        // pass 1: find the first class declaration; collect injected contracts from its remarks
        int start = 0;
        for (int i = 0; i < lines.length; i++) {
            String ls = lines[i].strip();
            if (ls.startsWith("///")) { pendingRemark += "\n" + lines[i]; continue; }
            if (ls.startsWith("namespace") || ls.isEmpty() || ls.startsWith("[")) continue;
            Matcher cm = classP.matcher(ls);
            if (cm.find() && !ls.contains(" where ")) {
                cls = cm.group(1);
                clsDepth = depth;
                for (String rl : pendingRemark.split("\n")) {
                    if (rl.contains("Injected dependencies")) {
                        Matcher dm = depP.matcher(rl);
                        while (dm.find()) {
                            if (!dm.group(2).equals("CD-21"))
                                injected.computeIfAbsent(dm.group(1), k -> new TreeSet<>()).add(dm.group(2));
                        }
                    }
                }
                start = i + 1;
            }
            pendingRemark = "";
        }
        if (cls == null) continue;

        // pass 2: walk class body; attribute each member: own remark set + contracts of injected fields it uses
        String curMember = null;
        Map<String, Set<String>> effective = new LinkedHashMap<>(); // member -> effective set
        Map<String, String> memberBody = new LinkedHashMap<>();     // member -> body text (heuristic)
        StringBuilder body = new StringBuilder();
        pendingRemark = "";
        int memberDepth = -1;
        for (int i = start; i < lines.length; i++) {
            String l = lines[i];
            String ls = l.strip();
            boolean isMember = depth == clsDepth + 1 && memberP.matcher(l).find();
            if (ls.startsWith("///")) { pendingRemark += "\n" + l; continue; }
            if (ls.isEmpty() || ls.startsWith("[")) continue;

            if (isMember) {
                if (curMember != null) memberBody.put(curMember, body.toString());
                body = new StringBuilder();
                String name = memberName(l);
                curMember = name != null ? name : "member#" + i;
                memberDepth = depth;
                Set<String> g = new TreeSet<>();
                for (String rl : pendingRemark.split("\n")) {
                    if (rl.contains("Change drivers") && !rl.contains("Injected dependencies") && !rl.contains("-> CD-")) {
                        Matcher m3 = cdP.matcher(rl);
                        while (m3.find()) g.add(m3.group());
                    }
                }
                effective.put(curMember, g);
                pendingRemark = "";
            }
            if (curMember != null) body.append(l).append('\n');
            depth += count(l, '{') - count(l, '}');
            if (depth <= clsDepth) { if (curMember != null) memberBody.put(curMember, body.toString()); break; }
        }
        if (curMember != null) memberBody.put(curMember, body.toString());

        // transmission: each using function inherits the used dependency's contract drivers
        for (var e : memberBody.entrySet()) {
            Set<String> eff = effective.get(e.getKey());
            if (eff == null) continue;
            for (var dep : injected.entrySet()) {
                if (Pattern.compile("\\b" + Pattern.quote(dep.getKey()) + "\\b").matcher(e.getValue()).find())
                    eff.addAll(dep.getValue());
            }
        }
        effective.values().removeIf(Set::isEmpty);
        if (effective.isEmpty()) { skipped++; continue; }
        checked++;
        Set<Set<String>> distinct = new HashSet<>(effective.values());
        if (distinct.size() <= 1) { pure++; continue; }
        Map<String, Set<String>> effCopy = new LinkedHashMap<>();
        effective.forEach((k, v) -> effCopy.put(k, new TreeSet<>(v)));
        viols.add(new Viol(cls, f.toString(), effective.size(), effCopy,
                injected.values().stream().flatMap(Set::stream).collect(Collectors.toCollection(TreeSet::new))));
    }

    System.out.println("modules with transmissions evaluated=" + checked + " single-set=" + pure + " violating=" + viols.size() + " skipped=" + skipped);
    System.out.println();
    for (Viol v : viols.stream().sorted(Comparator.comparingInt((Viol x) -> -x.effective().size())).toList()) {
        System.out.println("== " + v.cls() + "  [" + v.file() + "]");
        v.effective().forEach((m, g) -> System.out.println("   " + m + " : " + g));
    }
}

static String memberName(String l) {
    Matcher m = Pattern.compile("(?:public|private|internal|protected)\\s+(?:static\\s+|readonly\\s+|sealed\\s+|override\\s+|virtual\\s+|async\\s+)*[\\w<>,.?\\[\\]\\\\]+\\s+(_?\\w+)\\s*[(;={]").matcher(l);
    return m.find() ? m.group(1) : null;
}
static int count(String s, char c) { int n = 0; for (char x : s.toCharArray()) if (x == c) n++; return n; }
