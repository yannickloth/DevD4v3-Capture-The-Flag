namespace CTF.Application.Teams.Ids;

/// <summary>
/// Represents the identifiers of the teams in the CTF gamemode.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Player, ChangeDriver.GameRules)]
public enum TeamId
{
    /// <summary>The Alpha team.</summary>
    [ChangeDriversAttribute(ChangeDriver.Player)]
    Alpha,
    /// <summary>The Beta team.</summary>
    [ChangeDriversAttribute(ChangeDriver.Player)]
    Beta,
    /// <summary>The NoTeam state.</summary>
    [ChangeDriversAttribute(ChangeDriver.Player, ChangeDriver.GameRules)]
    NoTeam = 0xFF
}
