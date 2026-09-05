namespace CTF.Application.Chat.ChatSystem;

/// <summary>
/// Routes player chat messages to the matching private chat tier based on its prefix identifier.
/// </summary>
/// <remarks>Injected dependencies: chats (FrozenDictionary&lt;char, IChatMessage&gt;) -> CD-13. Driven by the chat-handler registry contract + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
public class ChatSystem(FrozenDictionary<char, IChatMessage> chats) : ISystem
{
    /// <summary>
    /// This callback is called when a player sends a message in chat.
    /// </summary>
    /// <param name="player">The player who sent the message.</param>
    /// <param name="text">	The content of the message that the player sent.</param>
    /// <remarks>
    /// See <see href="https://www.open.mp/docs/scripting/callbacks/OnPlayerText"/>
    /// </remarks>
    /// <returns>
    /// By default, this callback sends a message containing the content of the message and the player's name. 
    /// <para/>
    /// Returning 0 will ignore this default behaviour.
    /// </returns>
    [ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Player, ChangeDriver.ClientMessage)]
    [Event]
    public bool OnPlayerText(Player player, string text)
    {
        if (player.IsInClassSelection())
        {
            player.SendClientMessage(Color.Red, Messages.ChatDisabled);
            return false;
        }

        char identifier = text[0];
        if (chats.TryGetValue(identifier, out IChatMessage chatMessage)) 
        {
            PlayerInfo sender = player.GetRequiredInfo();
            ChatText.ReplaceFirstCharacter(text, newCharacter: ' ');
            bool sendMessageByDefault = !chatMessage.SendToAllPlayers(sender, text);
            ChatText.ReplaceFirstCharacter(text, newCharacter: identifier);
            return sendMessageByDefault;
        }
        return true;
    }
}
