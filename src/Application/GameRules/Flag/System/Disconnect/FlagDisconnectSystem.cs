namespace CTF.Application.GameRules.Flag.System.Disconnect;

/// <summary>
/// Handles flag drop when a carrying player disconnects.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): flagEvents (FrozenDictionary&lt;FlagStatus, IFlagEvent&gt;) -> CD-02. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
public class FlagDisconnectSystem(
    FrozenDictionary<FlagStatus, IFlagEvent> flagEvents) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    [Event]
    public void OnPlayerDisconnect(Player player, DisconnectReason reason)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        if (playerInfo.Appearance.Team.RivalTeam.Flag.IsCarriedBy(player))
        {
            Team currentTeam = playerInfo.Appearance.Team;
            IFlagEvent flagDropped = flagEvents[FlagStatus.Dropped];
            flagDropped.Handle(currentTeam.RivalTeam, player);
        }
    }
}
