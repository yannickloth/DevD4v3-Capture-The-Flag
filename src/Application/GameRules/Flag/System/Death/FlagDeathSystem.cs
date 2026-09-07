namespace CTF.Application.GameRules.Flag.System.Death;

/// <summary>
/// Handles flag drop and rewards when a carrying player dies.
/// Uniform flow: the CTF flag rules (CD-02) plus the engaged contracts of the death
/// handling (player CD-31, combat CD-03, coin CD-06, statistics CD-10).
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): flagEvents (FrozenDictionary&lt;FlagStatus, IFlagEvent&gt;) -> CD-02; playerStatsRenderer -> CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Combat, ChangeDriver.Coin, ChangeDriver.Statistics)]
public class FlagDeathSystem(
    FrozenDictionary<FlagStatus, IFlagEvent> flagEvents,
    PlayerStatsRenderer playerStatsRenderer) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Combat, ChangeDriver.Coin, ChangeDriver.Statistics)]
    [Event]
    public void OnPlayerDeath(Player victim, Player killer, Weapon reason)
    {
        PlayerInfo victimInfo = victim.GetRequiredInfo();
        if (victimInfo.Appearance.Team.RivalTeam.Flag.IsCarriedBy(victim))
        {
            Team currentTeam = victimInfo.Appearance.Team;
            IFlagEvent flagDropped = flagEvents[FlagStatus.Dropped];
            flagDropped.Handle(currentTeam.RivalTeam, victim);
            if (killer is not null)
            {
                PlayerInfo killerInfo = killer.GetRequiredInfo();
                killerInfo.Coins.AddCoins(FlagDeathRewards.CarrierKillEarnedCoins);
                killer.AddHealth(FlagDeathRewards.CarrierKillEarnedHealth);
                killer.AddScore(FlagDeathRewards.CarrierKillEarnedScore);
                playerStatsRenderer.UpdateTextDraw(killer);
            }
        }
    }
}
