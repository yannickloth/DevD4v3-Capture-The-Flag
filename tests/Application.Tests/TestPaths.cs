namespace CTF.Application.Tests;

/// <summary>Tests for map test-data paths.</summary>
/// <remarks>Change drivers: CD-29 (root; code-under-test: map test-data paths); CD-26 (NUnit test-framework contract) → CD-29; CD-27 (FluentAssertions contract) → CD-29; CD-11 (map configuration) → CD-29; CD-22 (hosting/deployment spec) → CD-29</remarks>
public class TestPaths
{
    public static string Maps =>
        Path.Combine(
            Directory.GetCurrentDirectory(),
            "Maps",
            "Files");
}
