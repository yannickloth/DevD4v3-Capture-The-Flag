namespace CTF.Application.Chat.PlayerDomain;

/// <remarks>No injected services. Adds the private-message component when a player connects.</remarks>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Player)]
public class PrivateMessageConnectSystem : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Player)]
    public void OnPlayerConnect(Player player) 
    {
        player.AddComponent<PrivateMessageComponent>();
    }
}
