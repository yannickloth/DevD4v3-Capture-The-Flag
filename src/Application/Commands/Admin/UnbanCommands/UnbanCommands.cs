namespace CTF.Application.Commands.ClientMessageNsNs2;

/// <summary>
/// Unbans a player IP address.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): serverService -> CD-42. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.ServerService, ChangeDriver.ClientMessage)]
public class UnbanCommands(IServerService serverService) : ISystem
{
    /// <summary>Unbans a player IP address.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.ServerService, ChangeDriver.ClientMessage)]
    [PlayerCommand("unban")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void UnbanPlayer(Player currentPlayer, string ip)
    {
        var message = Smart.Format(Messages.SuccessfullyUnbanned, new { Ip = ip });
        currentPlayer.SendClientMessage(Color.Yellow, message);
        serverService.SendRconCommand($"unbanip {ip}");
    }
}
