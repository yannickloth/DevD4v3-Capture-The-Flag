namespace CTF.Application.Tests;

/// <summary>Tests for map test-data paths.</summary>
[ChangeDriversAttribute(ChangeDriver.Hosting, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.Map)]
public class TestPaths
{
    public static string Maps =>
        Path.Combine(
            Directory.GetCurrentDirectory(),
            "Maps",
            "Files");
}
