namespace CTF.Application.GameRules.ConfigurationNsNs2;

/// <summary>
/// Represents settings for automatic flag return.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Configuration)]
public class FlagAutoReturnSettings
{
    /// <summary>
    /// Gets the delay, in seconds, before a dropped flag is returned automatically.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Configuration)]
    public int Delay { get; init; } = 120;
}
