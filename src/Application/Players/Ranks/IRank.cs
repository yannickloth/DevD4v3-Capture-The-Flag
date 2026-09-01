namespace CTF.Application.Players.Ranks;

/// <summary>
/// Represents a rank tier in the player rank model.
/// </summary>
/// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
public interface IRank
{
    /// <summary>Gets the rank identifier.</summary>
    /// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
    RankId Id { get; }

    /// <summary>Gets the rank name.</summary>
    /// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
    string Name { get; }

    /// <summary>Gets the total kills required to reach this rank.</summary>
    /// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
    int RequiredKills { get; }

    /// <summary>Determines whether this is the maximum rank.</summary>
    /// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
    bool IsMax();

    /// <summary>Determines whether this is not the maximum rank.</summary>
    /// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
    bool IsNotMax();
}
