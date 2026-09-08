namespace CTF.Application.Chat.EcsNsNs4;

/// <summary>
/// Blocks or unblocks private messages for the player.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.CommandSet, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Ecs)]
public class PrivateMessageToggleCommands : ISystem
{
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
}
