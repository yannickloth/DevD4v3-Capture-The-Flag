namespace CTF.Application.GameRules.Players.Welcome;

/// <summary>
/// Sends the welcome messages to a player upon connection.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.ClientMessage)]
public class PlayerWelcomeSystem : ISystem
{
    /// <summary>Sends the welcome messages when a player connects.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.ClientMessage)]
    [Event]
    public void OnPlayerConnect(Player player)
    {
        player.SendClientMessage(Color.Yellow, Messages.Welcome1);
        player.SendClientMessage(Color.Red, Messages.Welcome2);
        player.SendClientMessage(Color.Yellow, Messages.Welcome3);
    }
}
