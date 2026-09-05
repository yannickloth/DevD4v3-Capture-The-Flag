namespace CTF.Application.GunGames.Systems;

/// <summary>
/// Represents the possible results produced after processing a kill
/// according to the GunGame rules.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public enum GunGameResult
{
    /// <summary>
    /// No progression-related action occurred.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    None,

    /// <summary>
    /// The killer advanced to the next weapon level.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    LeveledUp,

    /// <summary>
    /// The victim was demoted to the previous weapon level.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    LeveledDown,

    /// <summary>
    /// The killer reached the final weapon level.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    ReachedFinalLevel,

    /// <summary>
    /// The killer scored a kill while already at the final weapon level.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    ScoredFinalKill
}
