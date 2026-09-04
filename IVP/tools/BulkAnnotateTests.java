import java.nio.file.*;
import java.util.*;
import java.util.regex.*;

void main() throws IOException {
    Pattern memberP = Pattern.compile("^\\s{4}(?:(?:public|private|internal|protected)\\s+)?(?:(?:static|readonly|const|sealed|override|virtual|async|partial|new)\\s+)*[\\w<>,?.\\[\\]]+\\s+(\\w+)\\s*([({=;]|=>)");
    Pattern typeP = Pattern.compile("^\\s{0,4}(?:public|internal|private)?\\s*(?:static |abstract |partial |sealed |readonly )*(class|interface|enum|struct|record)\\b.*");
    Pattern driverP = Pattern.compile("Change drivers:\\s*(.*?)(?:</remarks>)?$");

    List<Path> files = new ArrayList<>();
    try (var s = Files.walk(Paths.get("tests"))) {
        s.filter(p -> p.toString().endsWith(".cs"))
         .filter(p -> !p.toString().contains("/obj/") && !p.toString().contains("/bin/"))
         .filter(p -> !p.getFileName().toString().equals("Usings.cs"))
         .forEach(files::add);
    }

    int annotated = 0;
    for (Path f : files) {
        List<String> lines = new ArrayList<>(Files.readAllLines(f));
        String classDriver = null;
        for (String line : lines) {
            Matcher m = driverP.matcher(line);
            if (m.find()) {
                classDriver = m.group(1).trim().replace("</remarks>", "").trim();
                break;
            }
        }
        if (classDriver == null) continue;

        boolean usesNSubstitute = lines.stream().anyMatch(l -> l.contains("Substitute.") || l.contains("NSubstitute"));
        String rootCd = firstCd(classDriver);

        List<String> out = new ArrayList<>();
        for (int i = 0; i < lines.size(); i++) {
            String line = lines.get(i);
            String trimmed = line.trim();
            boolean isMember = !trimmed.startsWith("///") && !trimmed.startsWith("//")
                    && !typeP.matcher(line).matches()
                    && memberP.matcher(line).find();
            boolean hasDriver = false;
            if (isMember) {
                for (int j = i - 1; j >= 0 && j >= i - 8; j--) {
                    if (lines.get(j).contains("Change drivers:")) { hasDriver = true; break; }
                }
            }
            if (isMember && !hasDriver) {
                String indent = line.substring(0, line.length() - line.stripLeading().length());
                String driver = classDriver;
                if (usesNSubstitute && !driver.contains("CD-28") && rootCd != null) {
                    driver = driver + "; CD-28 (NSubstitute mock contract) → " + rootCd;
                }
                out.add(indent + "/// <remarks>Change drivers: " + driver + "</remarks>");
                annotated++;
            }
            out.add(line);
        }
        if (out.size() > lines.size()) {
            Files.write(f, out);
        }
    }
    System.out.println("annotated member-like lines=" + annotated);
}

String firstCd(String s) {
    Matcher m = Pattern.compile("CD-\\d{2}").matcher(s);
    return m.find() ? m.group() : null;
}
