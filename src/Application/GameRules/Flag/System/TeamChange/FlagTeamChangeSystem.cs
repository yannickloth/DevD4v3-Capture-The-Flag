namespace CTF.Application.GameRules.Flag.System.TeamChange;

/// <summary>
/// Drops the flag when a carrying player changes teams.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): flagEvents (FrozenDictionary&lt;FlagStatus, IFlagEvent&gt;) -> CD-02. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules)]
public class FlagTeamChangeSystem(
    FrozenDictionary<FlagStatus, IFlagEvent> flagEvents) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    [Event]
    public void OnTeamChange(Player player, Team selectedTeam)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();

        if (!playerInfo.Appearance.Team.RivalTeam.Flag.IsCarriedBy(player))
            return;

        IFlagEvent flagDropped = flagEvents[FlagStatus.Dropped];
        flagDropped.Handle(selectedTeam, player);
    }
}
