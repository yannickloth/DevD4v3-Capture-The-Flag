namespace CTF.Application.Statistics.TeamStats.System.Death;

/// <summary>
/// Updates team kills and deaths on player death.
/// </summary>
/// <remarks>No injected dependencies.</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player)]
public class TeamDeathStatsSystem : ISystem
{
    /// <summary>Updates team kills and deaths on player death.</summary>
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player)]
    public void OnPlayerDeath(Player victim, Player killer, Weapon reason)
    {
        PlayerInfo victimInfo = victim.GetRequiredInfo();
        victimInfo.Appearance.Team.StatsPerRound.AddDeaths();

        if (killer.IsInvalidPlayer())
            return;

        PlayerInfo killerInfo = killer.GetRequiredInfo();
        killerInfo.Appearance.Team.StatsPerRound.AddKills();
    }
}
