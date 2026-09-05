namespace CTF.Application.Statistics.Ranks;

/// <summary>
/// Identifies the rank tiers in the player rank model.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Statistics)]
public enum RankId
{
    /// <summary>The initial rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    Noob,

    /// <summary>A rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    Medium,

    /// <summary>A rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    Junior,

    /// <summary>A rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    SemiAdvance,

    /// <summary>A rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    Advanced,

    /// <summary>A rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    Hitman,

    /// <summary>A rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    Extreme,

    /// <summary>A rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    Annihilator,

    /// <summary>A rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    Maniac,

    /// <summary>A rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    Invincible,

    /// <summary>A rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    Senior,

    /// <summary>A rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    GameMaster,

    /// <summary>A rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    Professional,

    /// <summary>A rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    SuperPro,

    /// <summary>The highest rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    Legendary
}
