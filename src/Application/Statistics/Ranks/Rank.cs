namespace CTF.Application.Statistics.Ranks;

/// <summary>
/// Represents a rank tier in the player rank model.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Statistics)]
public readonly record struct Rank(RankId Id, string Name, int RequiredKills)
{
    /// <summary>Initializes a rank from its identifier and required kills; the name is derived from the identifier.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public Rank(RankId id, int requiredKills) : this(id, id.ToString(), requiredKills) { }

    /// <summary>Gets a sentinel rank representing no rank.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public static readonly Rank None = new((RankId)(-1), "None", 0);

    /// <summary>Determines whether this is the maximum rank.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public bool IsMax() => RankCollection.Count == (int)Id + 1;

    /// <summary>Determines whether this is not the maximum rank.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public bool IsNotMax() => !IsMax();
}
