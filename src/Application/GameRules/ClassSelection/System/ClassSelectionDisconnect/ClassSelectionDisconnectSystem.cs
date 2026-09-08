namespace CTF.Application.GameRules.AccountDomain;

/// <summary>
/// Handles player disconnect by removing them from their team.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): teamTextDrawRenderer -> CD-34. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Account)]
public class ClassSelectionDisconnectSystem(
    TeamTextDrawRenderer teamTextDrawRenderer) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Account)]
    [Event]
    public void OnPlayerDisconnect(Player player, DisconnectReason reason)
    {
        if (player.IsUnauthenticated())
            return;

        PlayerInfo playerInfo = player.GetRequiredInfo();
        if (playerInfo.Appearance.Team == Team.None)
            return;

        playerInfo.Appearance.Team.Members.Remove(player);
        teamTextDrawRenderer.UpdateTeamMembers(playerInfo.Appearance.Team);
    }
}
