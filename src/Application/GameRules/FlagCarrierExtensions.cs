namespace CTF.Application.GameRules;

/// <summary>
/// Provides flag-carrier query extension methods over the player entity.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag carrier state); CD-01 (open.mp/SampSharp platform API: player/flag entities) → CD-02</remarks>
public static class FlagCarrierExtensions
{
    /// <summary>
    /// Determines whether the specified player is carrying the enemy team's flag.
    /// </summary>
    /// <param name="playerInfo">
    /// The player information to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the player is carrying the enemy flag;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag carrier state)</remarks>
    public static bool IsCarryingEnemyFlag(this PlayerInfo playerInfo)
    {
        if (playerInfo.Team == Team.None)
            return false;

        Flag rivalTeamFlag = playerInfo.Team.RivalTeam.Flag;
        if (rivalTeamFlag.HasCarrier)
        {
            Player carrier = rivalTeamFlag.Carrier;
            return carrier.Name.Equals(
                playerInfo.Name,
                StringComparison.OrdinalIgnoreCase);
        }
        return false;
    }
}
