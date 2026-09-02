namespace CTF.Application.Players;

/// <summary>
/// Handles the command-text callback, invoking the registered command handlers.
/// </summary>
/// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API); CD-21 (DI container/composition) → CD-01</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): playerCommandService -> CD-01; serviceProvider -> CD-21. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class PlayerCommandTextSystem(
    IPlayerCommandService playerCommandService,
    IServiceProvider serviceProvider) : ISystem
{
    /// <summary>
    /// This callback is called when a player enters a command into the client chat window. 
    /// Commands are anything that start with a forward slash, e.g. /help.
    /// </summary>
    /// <param name="player">
    /// The player that entered a command.
    /// </param>
    /// <param name="text">
    /// The command that was entered (including the forward slash).
    /// </param>
    /// <returns>
    /// <c>true</c> if the command was processed, otherwise <c>false</c>; If the command was not found both in 
    /// filterscripts and in gamemode, the player will be received a message: 'SERVER: Unknown command'.
    /// </returns>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API); CD-21 (DI container/composition) → CD-01</remarks>
    [Event]
    public bool OnPlayerCommandText(Player player, string text)
    {
        bool invokeResult = playerCommandService.Invoke(serviceProvider, player, text);
        if (!invokeResult)
            player.SendClientMessage(Color.Red, Messages.CommandNotFound);

        return true;
    }
}
