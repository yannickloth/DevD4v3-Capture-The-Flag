namespace CTF.Application.Tests;

/// <summary>Tests for map test-data paths.</summary>
/// <remarks>Change drivers: CD-22 (root; hosting/deployment spec: map test-data paths); CD-26 (NUnit test-framework contract) → CD-22; CD-27 (FluentAssertions contract) → CD-22; CD-11 (map configuration) → CD-22</remarks>
public class TestPaths
{
    public static string Maps =>
        Path.Combine(
            Directory.GetCurrentDirectory(),
            "Maps",
            "Files");
}
