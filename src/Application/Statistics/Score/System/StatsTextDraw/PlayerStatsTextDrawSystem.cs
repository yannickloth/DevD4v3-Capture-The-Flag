namespace CTF.Application.Statistics.Score.System.StatsTextDraw;

/// <summary>
/// Creates and updates the per-player statistics textdraw.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): playerStatsRenderer -> CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.TextDraw)]
public class PlayerStatsTextDrawSystem(
    PlayerStatsRenderer playerStatsRenderer) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.TextDraw)]
    [Event]
    public void OnPlayerConnect(Player player)
    {
        playerStatsRenderer.CreateTextDraw(player);
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.TextDraw)]
    [Event]
    public void OnPlayerSpawn(Player player)
    {
        playerStatsRenderer.UpdateTextDraw(player);
    }
}
