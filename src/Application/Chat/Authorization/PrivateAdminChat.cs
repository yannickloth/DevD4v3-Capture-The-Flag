namespace CTF.Application.Chat.ClientMessageNsNs3;

/// <summary>
/// Represents the admin private chat tier, routed by the '#' prefix.
/// </summary>
/// <remarks>Injected dependencies: entityManager -> CD-32. Driven by the IEntityManager (platform) contract + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Authorization, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
public class PrivateAdminChat(IEntityManager entityManager) : IChatMessage
{
    /// <summary>Gets the chat prefix identifier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Authorization, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
    public char Id => '#';

    /// <summary>Sends the message to all players of the required admin role.</summary>
    [ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Authorization, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
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

            player.SendClientMessage(new Color(0x33FF33AA), $"[Admin Chat] {sender.Account.Name}: {message}");
        }
        return true;
    }
}
