namespace CTF.Application.Chat.EcsNsNs3;

/// <summary>
/// Sends a private message to a player and relays it to the staff.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): entityManager -> CD-32. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Ecs)]
public class SendPrivateMessageCommands(IEntityManager entityManager) : ISystem
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
}
