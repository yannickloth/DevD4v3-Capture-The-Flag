namespace CTF.Application.PlayerResources;

/// <summary>
/// Represents the player skin ids assigned to teams.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Model)]
public enum SkinTeamId
{
    /// <summary>The NoTeam skin.</summary>
    [ChangeDriversAttribute(ChangeDriver.Model)]
    NoTeam = 0,
    /// <summary>The Alpha team skin.</summary>
    [ChangeDriversAttribute(ChangeDriver.Model)]
    Alpha = 170,
    /// <summary>The Beta team skin.</summary>
    [ChangeDriversAttribute(ChangeDriver.Model)]
    Beta = 177
}
