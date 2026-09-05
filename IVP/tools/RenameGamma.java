import java.io.*;
import java.nio.file.*;
import java.nio.charset.StandardCharsets;
import java.util.*;
import java.util.regex.*;

/** IVP helper: rename Gamma_* namespace dirs to curated semantic leaf names.
 *  Usage: java IVP/tools/RenameGamma.java mapping.tsv
 *  mapping.tsv rows (tab-separated):
 *      <repoRootRelativeDirOfGamma>   <newLeafName>
 *  For each: git mv the dir to parent/<newLeafName>, then repo-wide replace the
 *  exact old full namespace string -> new full namespace string across *.cs files.
 *  Old full namespace is derived from the dir path.
 */
void main(String[] args) throws Exception {
    if (args.length != 1) { System.err.println("usage: RenameGamma.java mapping.tsv"); return; }
    // Read rows first
    List<String[]> rows = new ArrayList<>();
    for (String raw : Files.readAllLines(Paths.get(args[0]), StandardCharsets.UTF_8)) {
        if (raw.isBlank() || raw.startsWith("#")) continue;
        String[] p = raw.split("\t", -1);
        if (p.length < 2) { System.err.println("bad row: " + raw); continue; }
        rows.add(new String[]{p[0].trim(), p[1].trim()});
    }
    for (String[] row : rows) {
        Path dir = Paths.get(row[0]);
        String newLeaf = row[1];
        String oldNs = dirNs(dir.toString());
        Path parent = dir.getParent();
        Path destDir = parent.resolve(newLeaf);
        if (!Files.isDirectory(dir)) { System.err.println("missing dir: " + dir); continue; }
        exec("git", "mv", dir.toString(), destDir.toString());
        String newNs = oldNs.substring(0, oldNs.lastIndexOf('.')) + "." + newLeaf;
        System.out.println("moved: " + dir + " -> " + destDir + "   (" + oldNs + " -> " + newNs + ")");
        replaceNs(oldNs, newNs);
    }
}
static String dirNs(String p) {
    // ./src/Application/Accounts/Authentication/Gamma_X -> CTF.Application.Accounts.Authentication.Gamma_X
    String q = p;
    if (q.startsWith("./")) q = q.substring(2);
    // decide project root prefix
    if (q.startsWith("src/Host/")) return "CTF.Host." + q.substring("src/Host/".length()).replace('/','.');
    if (q.startsWith("src/Application/")) return "CTF.Application." + q.substring("src/Application/".length()).replace('/','.');
    if (q.startsWith("tests/Application.Tests/")) return "CTF.Application.Tests." + q.substring("tests/Application.Tests/".length()).replace('/','.');
    throw new RuntimeException("cannot derive ns from " + p);
}
static void replaceNs(String oldNs, String newNs) throws Exception {
    Charset UTF8 = StandardCharsets.UTF_8;
    Path root = Paths.get(".");
    List<Path> files = new ArrayList<>();
    for (String r : new String[]{"src","tests"}) {
        try (var s = Files.walk(root.resolve(r))) {
            s.filter(p -> p.toString().endsWith(".cs"))
             .filter(p -> !p.toString().contains("/obj/") && !p.toString().contains("/bin/"))
             .forEach(files::add);
        }
    }
    int changed = 0;
    String rx = Pattern.quote(oldNs) + "(?![A-Za-z0-9_])";
    Pattern p = Pattern.compile(rx);
    for (Path f : files) {
        String c = Files.readString(f, UTF8);
        Matcher m = p.matcher(c);
        if (m.find()) {
            Files.writeString(f, m.replaceAll(Matcher.quoteReplacement(newNs)), UTF8);
            changed++;
        }
    }
    System.out.println("   updated " + changed + " files for " + oldNs);
}
static void exec(String... cmd) throws Exception {
    Process p = new ProcessBuilder(cmd).inheritIO().start();
    if (p.waitFor() != 0) throw new IOException("failed: " + String.join(" ", cmd));
}
