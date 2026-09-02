namespace CTF.Application.Players.Chats;

/// <summary>
/// Represents a chat message in the messaging system.
/// </summary>
/// <remarks>Change drivers: CD-13 (root; chat rules)</remarks>
public interface IChatMessage
{
    /// <summary>
    /// Gets the unique identifier of the chat message.
    /// </summary>
    /// <remarks>Change drivers: CD-13 (root; chat rules)</remarks>
    char Id { get; }

    /// <summary>
    /// Sends a message to all players in the chat and returns a status indicating success or failure.
    /// </summary>
    /// <param name="sender">The player who is sending the message.</param>
    /// <param name="message">The content of the message to be sent.</param>
    /// <returns>
    /// <c>true</c> if the message was successfully sent; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>Change drivers: CD-13 (root; chat rules); CD-09 (authorization policy) → CD-13; CD-01 (open.mp/SampSharp platform API) → CD-13</remarks>
    bool SendToAllPlayers(PlayerInfo sender, string message);
}
