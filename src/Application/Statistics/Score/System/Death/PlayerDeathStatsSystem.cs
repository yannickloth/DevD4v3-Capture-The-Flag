namespace CTF.Application.Statistics.Score.System.Death;

/// <summary>
/// Updates and persists player kill/death statistics on a player death.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; killingSpreeUpdater -> CD-10; playerRankUpdater -> CD-10; playerStatsRenderer -> CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.Repository)]
public class PlayerDeathStatsSystem(
    IPlayerRepository playerRepository,
    PlayerKillingSpreeUpdater killingSpreeUpdater,
    PlayerRankUpdater playerRankUpdater,
    PlayerStatsRenderer playerStatsRenderer) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.Repository)]
    [Event]
    public void OnPlayerDeath(Player victim, Player killer, Weapon reason)
    {
        PlayerInfo victimInfo = victim.GetRequiredInfo();
        victimInfo.Stats.PerRound.AddDeaths();
        victimInfo.Stats.PerRound.ResetKillingSpree();
        victimInfo.Stats.AddTotalDeaths();
        playerRepository.UpdateTotalDeaths(victimInfo);

        if (killer.IsInvalidPlayer())
            return;

        PlayerInfo killerInfo = killer.GetRequiredInfo();
        killerInfo.Stats.PerRound.AddKills();
        killerInfo.Stats.AddTotalKills();
        killer.AddScore();
        playerRepository.UpdateTotalKills(killerInfo);
        killingSpreeUpdater.Update(killer);
        playerRankUpdater.Update(killer);
        playerStatsRenderer.UpdateTextDraw(killer);
    }
}
