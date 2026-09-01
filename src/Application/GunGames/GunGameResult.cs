namespace CTF.Application.GunGames;

/// <summary>
/// Represents the possible results produced after processing a kill
/// according to the GunGame rules.
/// </summary>
/// <remarks>Change drivers: CD-07 (GunGame mode rules)</remarks>
public enum GunGameResult
{
    /// <summary>
    /// No progression-related action occurred.
    /// </summary>
    /// <remarks>Change drivers: CD-07 (GunGame mode rules)</remarks>
    None,

    /// <summary>
    /// The killer advanced to the next weapon level.
    /// </summary>
    /// <remarks>Change drivers: CD-07 (GunGame mode rules)</remarks>
    LeveledUp,

    /// <summary>
    /// The victim was demoted to the previous weapon level.
    /// </summary>
    /// <remarks>Change drivers: CD-07 (GunGame mode rules)</remarks>
    LeveledDown,

    /// <summary>
    /// The killer reached the final weapon level.
    /// </summary>
    /// <remarks>Change drivers: CD-07 (GunGame mode rules)</remarks>
    ReachedFinalLevel,

    /// <summary>
    /// The killer scored a kill while already at the final weapon level.
    /// </summary>
    /// <remarks>Change drivers: CD-07 (GunGame mode rules)</remarks>
    ScoredFinalKill
}
