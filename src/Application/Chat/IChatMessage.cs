namespace CTF.Application.Chat;

/// <summary>
/// Represents a chat message in the messaging system.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Authorization, ChangeDriver.ClientMessage)]
public interface IChatMessage
{
    /// <summary>
    /// Gets the unique identifier of the chat message.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Authorization, ChangeDriver.ClientMessage)]
    char Id { get; }

    /// <summary>
    /// Sends a message to all players in the chat and returns a status indicating success or failure.
    /// </summary>
    /// <param name="sender">The player who is sending the message.</param>
    /// <param name="message">The content of the message to be sent.</param>
    /// <returns>
    /// <c>true</c> if the message was successfully sent; otherwise, <c>false</c>.
    /// </returns>
    [ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Authorization, ChangeDriver.ClientMessage)]
    bool SendToAllPlayers(PlayerInfo sender, string message);
}
