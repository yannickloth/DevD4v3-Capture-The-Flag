namespace CTF.Application.GameRules;

/// <summary>
/// Sends the welcome messages to a player upon connection.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification); CD-01 (open.mp/SampSharp platform API) → CD-02</remarks>
public class PlayerWelcomeSystem : ISystem
{
    /// <summary>Sends the welcome messages when a player connects.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification); CD-01 (open.mp/SampSharp platform API) → CD-02</remarks>
    [Event]
    public void OnPlayerConnect(Player player)
    {
        player.SendClientMessage(Color.Yellow, Messages.Welcome1);
        player.SendClientMessage(Color.Red, Messages.Welcome2);
        player.SendClientMessage(Color.Yellow, Messages.Welcome3);
    }
}
