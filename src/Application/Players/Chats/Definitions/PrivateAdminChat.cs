namespace CTF.Application.Players.Chats.Definitions;

/// <summary>
/// Represents the admin private chat tier, routed by the '#' prefix.
/// </summary>
/// <remarks>Change drivers: CD-09 (authorization policy); CD-13 (chat rules); CD-01 (open.mp/SampSharp platform API)</remarks>
/// <remarks>Injected dependencies: entityManager -> CD-01. Driven by the IEntityManager (platform) contract + CD-21 (DI wiring).</remarks>
public class PrivateAdminChat(IEntityManager entityManager) : IChatMessage
{
    /// <summary>Gets the chat prefix identifier.</summary>
    /// <remarks>Change drivers: CD-13 (chat rules)</remarks>
    public char Id => '#';

    /// <summary>Sends the message to all players of the required admin role.</summary>
    /// <remarks>Change drivers: CD-09 (authorization policy); CD-13 (chat rules); CD-01 (open.mp/SampSharp platform API)</remarks>
    public bool SendToAllPlayers(PlayerInfo sender, string message)
    {
        if (sender.HasLowerRoleThan(RoleId.Admin))
            return false;

        var players = entityManager.GetComponents<Player>();
        foreach (Player player in players)
        {
            if (player.IsInClassSelection())
                continue;

            PlayerInfo playerInfo = player.GetRequiredInfo();
            if (playerInfo.HasLowerRoleThan(RoleId.Admin))
                continue;

            player.SendClientMessage(new Color(0x33FF33AA), $"[Admin Chat] {sender.Name}: {message}");
        }
        return true;
    }
}
