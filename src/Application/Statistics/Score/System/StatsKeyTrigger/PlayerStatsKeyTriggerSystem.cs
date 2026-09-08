
namespace CTF.Application.Statistics.DialogNsNs2;

/// <summary>
/// Dispatches the my-stats dialog command when the player presses the analog-right key.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): showStatsCommands -> CD-21 (DI wiring). Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.Dialog)]
public class PlayerStatsKeyTriggerSystem(
    PlayerStatsDialogCommands showStatsCommands) : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.Dialog)]
    public void OnPlayerKeyStateChange(Player player, Keys newKeys, Keys oldKeys)
    {
        if (KeyUtils.HasPressed(newKeys, oldKeys, Keys.AnalogRight))
        {
            showStatsCommands.ShowStats(player);
        }
    }
}
