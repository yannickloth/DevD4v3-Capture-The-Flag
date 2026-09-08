namespace CTF.Hosting;

[ChangeDriversAttribute(ChangeDriver.Hosting)]
public static class GameModePaths
{
    [ChangeDriversAttribute(ChangeDriver.Hosting)]
    public static string Maps =>
        Path.Combine(
            Root,
            "Maps",
            "Files");

    [ChangeDriversAttribute(ChangeDriver.Hosting)]
    public static string Sql =>
        Path.Combine(
            Root,
            "yesql");

    [ChangeDriversAttribute(ChangeDriver.Hosting)]
    private static string Root => 
        Path.Combine(
            Directory.GetCurrentDirectory(), 
            "gamemode");
}
