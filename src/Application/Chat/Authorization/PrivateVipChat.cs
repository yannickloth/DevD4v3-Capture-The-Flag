namespace CTF.Application.Chat.Authorization;

/// <summary>
/// Represents the VIP private chat tier, routed by the '$' prefix.
/// </summary>
/// <remarks>Injected dependencies: entityManager -> CD-32. Driven by the IEntityManager (platform) contract + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Authorization, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
public class PrivateVipChat(IEntityManager entityManager) : IChatMessage
{
    /// <summary>Gets the chat prefix identifier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Authorization, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
    public char Id => '$';

    /// <summary>Sends the message to all players of the required VIP role.</summary>
    [ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Authorization, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
    public bool SendToAllPlayers(PlayerInfo sender, string message)
    {
        if (sender.HasLowerRoleThan(RoleId.VIP))
            return false;

        var players = entityManager.GetComponents<Player>();
        foreach (Player player in players)
        {
            if (player.IsInClassSelection())
                continue;

            PlayerInfo playerInfo = player.GetRequiredInfo();
            if (playerInfo.HasLowerRoleThan(RoleId.VIP))
                continue;

            player.SendClientMessage($"{{8b0000}}[Vip Chat] {sender.Account.Name}: {message}");
        }
        return true;
    }
}
