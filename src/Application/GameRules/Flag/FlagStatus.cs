namespace CTF.Application.GameRules.Flag;

/// <summary>
/// Represents the states a flag can be in during the match.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules)]
public enum FlagStatus
{
    /// <summary>
    /// Indicates that the flag is at the base position, where it is defended to prevent enemy capture.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    BasePosition,

    /// <summary>
    /// Indicates that a player has captured the opposing team's flag from their base.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    Captured,

    /// <summary>
    /// Indicates that a player has returned the flag to their team's base.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    Returned,

    /// <summary>
    /// Indicates that a player has taken the flag from a position other than the base.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    Taken,

    /// <summary>
    /// Indicates that a player has captured the opposing team's flag and brought it back to their own base.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    Brought,

    /// <summary>
    /// Indicates that a player has dropped the opposing team's flag.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    Dropped
}
