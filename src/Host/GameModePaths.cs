namespace CTF.Host;

/// <remarks>Change drivers: CD-22 (root; hosting/deployment spec)</remarks>
public static class GameModePaths
{
    /// <remarks>Change drivers: CD-22 (root; hosting/deployment spec); CD-11 (map configuration) → CD-22</remarks>
    public static string Maps =>
        Path.Combine(
            Root,
            "Maps",
            "Files");

    /// <remarks>Change drivers: CD-22 (root; hosting/deployment spec)</remarks>
    public static string Sql =>
        Path.Combine(
            Root,
            "yesql");

    /// <remarks>Change drivers: CD-22 (root; hosting/deployment spec)</remarks>
    private static string Root => 
        Path.Combine(
            Directory.GetCurrentDirectory(), 
            "gamemode");
}
