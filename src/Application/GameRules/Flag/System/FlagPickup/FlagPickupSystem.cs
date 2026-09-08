namespace CTF.Application.GameRules.PlayerNsNs3;

/// <summary>
/// Handles flag pickup interactions.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): flagEvents (FrozenDictionary&lt;FlagStatus, IFlagEvent&gt;) -> CD-02. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Pickup, ChangeDriver.Player)]
public class FlagPickupSystem(
    FrozenDictionary<FlagStatus, IFlagEvent> flagEvents) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Pickup, ChangeDriver.Player)]
    [Event]
    public void OnPlayerPickUpPickup(Player player, Pickup pickup)
    {
        if (pickup.Model == (int)FlagModel.Red)
        {
            FlagStatus flagStatus = Team.Alpha.HandleFlagInteraction(flagPicker: player);
            IFlagEvent flagEvent = flagEvents[flagStatus];
            flagEvent.Handle(Team.Alpha, player);
        }
        else if (pickup.Model == (int)FlagModel.Blue)
        {
            FlagStatus flagStatus = Team.Beta.HandleFlagInteraction(flagPicker: player);
            IFlagEvent flagEvent = flagEvents[flagStatus];
            flagEvent.Handle(Team.Beta, player);
        }
        else if (pickup.Model == (int)ExteriorMarker.Red)
        {
            if (player.Team == (int)TeamId.Alpha)
                player.GameText(Messages.RedFlagIsNotAtBasePosition, TimeSpan.FromSeconds(5), GameTextStyle.Style3);
        }
        else if (pickup.Model == (int)ExteriorMarker.Blue)
        {
            if (player.Team == (int)TeamId.Beta)
                player.GameText(Messages.BlueFlagIsNotAtBasePosition, TimeSpan.FromSeconds(5), GameTextStyle.Style3);
        }
    }
}
