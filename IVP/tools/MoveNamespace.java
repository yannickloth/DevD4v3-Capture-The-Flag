import java.io.*;
import java.nio.file.*;
import java.nio.charset.Charset;
import java.util.*;
import java.util.regex.*;

/** Mechanical IVP helper: moves each listed .cs into a sub-folder matching a new namespace
 *  and rewrites its single namespace declaration.
 *  Usage: java IVP/tools/MoveNamespace.java mapping.tsv
 *  mapping.tsv: one "<tab>-separated" row per file:
 *      <repoRootRelativeFilePath>\t<newFullNamespace>
 *  Uses `git mv` to preserve history. Rewrites the first "namespace X ...;" occurrence.
 */
void main(String[] args) throws IOException {
    if (args.length != 1) { System.err.println("usage: MoveNamespace.java mapping.tsv"); return; }
    Charset UTF8 = StandardCharsets.UTF_8;
    List<String> lines = Files.readAllLines(Paths.get(args[0]), UTF8);
    Pattern nsP = Pattern.compile("namespace\\s+[A-Za-z0-9_.]+\\s*;");
    for (String raw : lines) {
        if (raw.isBlank() || raw.startsWith("#")) continue;
        String[] p = raw.split("\t", -1);
        if (p.length < 2) { System.err.println("bad row: " + raw); continue; }
        Path file = Paths.get(p[0].trim());
        String newNs = p[1].trim();
        Path parent = file.getParent();
        String subDir = newNs.substring(newNs.lastIndexOf('.') + 1);
        Path destDir = parent.resolve(subDir);
        Files.createDirectories(destDir);
        Path dest = destDir.resolve(file.getFileName());
        // git mv
        exec("git", "mv", file.toString(), dest.toString());
        String c = Files.readString(dest, UTF8);
        Matcher m = nsP.matcher(c);
        if (!m.find()) { System.err.println("no namespace decl in " + dest); continue; }
        String replaced = c.substring(0, m.start()) + "namespace " + newNs + ";" + c.substring(m.end());
        Files.writeString(dest, replaced, UTF8);
        System.out.println("moved+rewrote " + file + " -> " + dest + "  [" + newNs + "]");
    }
}

static void exec(String... cmd) throws IOException {
    Process p = new ProcessBuilder(cmd).inheritIO().start();
    try {
        int code = p.waitFor();
        if (code != 0) throw new IOException("git mv failed (" + code + ") for " + cmd[cmd.length - 1]);
    } catch (InterruptedException e) { throw new IOException(e); }
}
