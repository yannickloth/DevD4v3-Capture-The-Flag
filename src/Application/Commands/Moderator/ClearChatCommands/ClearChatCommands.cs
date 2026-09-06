namespace CTF.Application.Commands.Moderator.ClearChatCommands;

/// <summary>
/// Clears the chat for all players.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): worldService -> CD-36. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.ClientMessage)]
public class ClearChatCommands(IWorldService worldService) : ISystem
{
    /// <summary>Clears the chat for all players.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.ClientMessage)]
    [PlayerCommand("clearallchat")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void ClearAllChat(Player currentPlayer)
    {
        for (int i = 0; i < 200; i++)
        {
            worldService.SendClientMessage(" ");
        }
    }
}
