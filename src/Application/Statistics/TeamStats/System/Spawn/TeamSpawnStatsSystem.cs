namespace CTF.Application.Players.TextDrawDomain;

/// <summary>
/// Shows the team textdraws when a player spawns.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): teamTextDrawRenderer -> CD-34. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Player, ChangeDriver.TextDraw)]
public class TeamSpawnStatsSystem(
    TeamTextDrawRenderer teamTextDrawRenderer) : ISystem
{
    /// <summary>Shows team textdraws when the player spawns.</summary>
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Player, ChangeDriver.TextDraw)]
    public void OnPlayerSpawn(Player player)
    {
        teamTextDrawRenderer.Show(player);
    }
}
