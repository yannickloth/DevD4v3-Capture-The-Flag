namespace CTF.Application.Players.Chats.Definitions;

/// <summary>
/// Represents the moderator private chat tier, routed by the '&' prefix.
/// </summary>
/// <remarks>Change drivers: CD-13 (root; chat rules); CD-09 (authorization policy) → CD-13; CD-01 (open.mp/SampSharp platform API) → CD-13</remarks>
/// <remarks>Injected dependencies: entityManager -> CD-01. Driven by the IEntityManager (platform) contract + CD-21 (DI wiring).</remarks>
public class PrivateModeratorChat(IEntityManager entityManager) : IChatMessage
{
    /// <summary>Gets the chat prefix identifier.</summary>
    /// <remarks>Change drivers: CD-13 (root; root; chat rules)</remarks>
    public char Id => '&';

    /// <summary>Sends the message to all players of the required moderator role.</summary>
    /// <remarks>Change drivers: CD-13 (root; chat rules); CD-09 (authorization policy) → CD-13; CD-01 (open.mp/SampSharp platform API) → CD-13</remarks>
    public bool SendToAllPlayers(PlayerInfo sender, string message)
    {
        if (sender.HasLowerRoleThan(RoleId.Moderator))
            return false;

        var players = entityManager.GetComponents<Player>();
        foreach (Player player in players)
        {
            if (player.IsInClassSelection())
                continue;

            PlayerInfo playerInfo = player.GetRequiredInfo();
            if (playerInfo.HasLowerRoleThan(RoleId.Moderator))
                continue;

            player.SendClientMessage(Color.Yellow, $"[Moderator Chat] {sender.Name}: {message}");
        }
        return true;
    }
}
