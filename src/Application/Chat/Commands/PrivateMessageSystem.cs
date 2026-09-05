namespace CTF.Application.Chat.Commands;

/// <summary>
/// Provides private-message commands (PM, block, unblock) and manages the per-player PM state.
/// </summary>
/// <remarks>Injected dependencies: entityManager -> CD-32. Driven by the IEntityManager (platform) contract + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure)]
public class PrivateMessageSystem(IEntityManager entityManager) : ISystem
{
    /// <summary>Sends a private message to a player and relays it to the staff.</summary>
    [ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Ecs)]
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
    [ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.CommandSet, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Ecs)]
    [PlayerCommand("blockpm")]
    public void Block(Player player)
    {
        var privateMessageComponent = player.GetComponent<PrivateMessageComponent>();
        privateMessageComponent.IsBlocked = true;
        player.SendClientMessage(Color.Yellow, Messages.PrivateMessagesDisabled);
        player.PlaySound(1139);
    }

    /// <summary>Unblocks private messages for the player.</summary>
    [ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.CommandSet, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Ecs)]
    [PlayerCommand("unblockpm")]
    public void Unblock(Player player)
    {
        var privateMessageComponent = player.GetComponent<PrivateMessageComponent>();
        privateMessageComponent.IsBlocked = false;
        player.SendClientMessage(Color.Yellow, Messages.PrivateMessagesEnabled);
        player.PlaySound(1139);
    }

    /// <summary>Adds the private-message component when a player connects.</summary>
    [ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Player)]
    [Event]
    public void OnPlayerConnect(Player player) 
    {
        player.AddComponent<PrivateMessageComponent>();
    }

    [ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Ecs)]
    private class PrivateMessageComponent : Component
    {
        [ChangeDriversAttribute(ChangeDriver.Chat)]
        public bool IsBlocked { get; set; }
    }
}
