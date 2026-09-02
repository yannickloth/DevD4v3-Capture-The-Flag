namespace CTF.Application.Teams.Flags.AutoReturn;

/// <summary>
/// Represents settings for automatic flag return.
/// </summary>
/// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag auto-return rule), CD-17 (game configuration/.env schema: FlagAutoReturn__*)</remarks>
public class FlagAutoReturnSettings
{
    /// <summary>
    /// Gets the delay, in seconds, before a dropped flag is returned automatically.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag auto-return rule), CD-17 (game configuration/.env schema: FlagAutoReturn__Delay)</remarks>
    public int Delay { get; init; } = 120;
}
