namespace CTF.Application.Chat;

/// <summary>
/// Provides private-message commands (PM, block, unblock) and manages the per-player PM state.
/// </summary>
/// <remarks>Change drivers: CD-13 (root; chat rules); CD-15 (command set) → CD-13; CD-09 (authorization policy) → CD-13; CD-31 (player events); CD-32 (ECS runtime); CD-36 (client messages); CD-43 (command infrastructure) → CD-13</remarks>
/// <remarks>Injected dependencies: entityManager -> CD-32. Driven by the IEntityManager (platform) contract + CD-21 (DI wiring).</remarks>
public class PrivateMessageSystem(IEntityManager entityManager) : ISystem
{
    /// <summary>Sends a private message to a player and relays it to the staff.</summary>
    /// <remarks>Change drivers: CD-13 (root; chat rules); CD-15 (command set) → CD-13; CD-09 (authorization policy) → CD-13; CD-43 (command infrastructure); CD-36 (client messages); CD-32 (ECS runtime) → CD-13</remarks>
    [PlayerCommand("pm")]
    public void SendMessageToPlayer(
        Player sender,
        [CommandParameter(Name = "playerId")]Player receiver,
        string message)
    {
        if (sender == receiver)
        {
            sender.SendClientMessage(Color.Red, Messages.PlayerIsEqualsToTargetPlayer);
            return;
        }

        var privateMessageComponent = receiver.GetComponent<PrivateMessageComponent>();
        if (privateMessageComponent.IsBlocked) 
        {
            sender.SendClientMessage(Color.Red, Messages.PrivateMessagesBlocked);
            return;
        }

        int senderId = sender.Id;
        int receiverId = receiver.Id;
        sender.SendClientMessage(Color.Yellow, $"PM to {receiver.Name}({receiverId}): {message}");
        sender.PlaySound(1058);
        receiver.SendClientMessage(Color.Yellow, $"PM from {sender.Name}({senderId}): {message}");
        receiver.PlaySound(1058);

        // Send private message to the STAFF.
        var players = entityManager.GetComponents<Player>();
        foreach (Player player in players)
        {
            PlayerInfo playerInfo = player.GetRequiredInfo();
            if (playerInfo.HasLowerRoleThan(RoleId.Moderator))
                continue;

            // This prevents double messaging.
            if (player == sender || player == receiver)
                continue;

            var messageForStaff = $"[PM] {sender.Name} writes to {receiver.Name}: {message}";
            player.SendClientMessage(Color.Yellow, messageForStaff);
        }
    }

    /// <summary>Blocks private messages for the player.</summary>
    /// <remarks>Change drivers: CD-13 (root; chat rules); CD-15 (command set) → CD-13; CD-43 (command infrastructure); CD-36 (client messages); CD-32 (ECS runtime) → CD-13</remarks>
    [PlayerCommand("blockpm")]
    public void Block(Player player)
    {
        var privateMessageComponent = player.GetComponent<PrivateMessageComponent>();
        privateMessageComponent.IsBlocked = true;
        player.SendClientMessage(Color.Yellow, Messages.PrivateMessagesDisabled);
        player.PlaySound(1139);
    }

    /// <summary>Unblocks private messages for the player.</summary>
    /// <remarks>Change drivers: CD-13 (root; chat rules); CD-15 (command set) → CD-13; CD-43 (command infrastructure); CD-36 (client messages); CD-32 (ECS runtime) → CD-13</remarks>
    [PlayerCommand("unblockpm")]
    public void Unblock(Player player)
    {
        var privateMessageComponent = player.GetComponent<PrivateMessageComponent>();
        privateMessageComponent.IsBlocked = false;
        player.SendClientMessage(Color.Yellow, Messages.PrivateMessagesEnabled);
        player.PlaySound(1139);
    }

    /// <summary>Adds the private-message component when a player connects.</summary>
    /// <remarks>Change drivers: CD-13 (root; chat rules); CD-31 (OnPlayerConnect) → CD-13</remarks>
    [Event]
    public void OnPlayerConnect(Player player) 
    {
        player.AddComponent<PrivateMessageComponent>();
    }

    /// <remarks>Change drivers: CD-13 (root; chat rules); CD-32 (component storage) → CD-13</remarks>
    private class PrivateMessageComponent : Component
    {
        /// <remarks>Change drivers: CD-13 (root; chat rules: PM block state)</remarks>
        public bool IsBlocked { get; set; }
    }
}
