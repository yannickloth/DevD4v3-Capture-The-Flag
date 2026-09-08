namespace CTF.Application.Commands.ClientMessageNsNs8;

/// <summary>
/// Admin commands that act on a target player: teleport to them (goto), bring them to the admin (get), and ban them.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): worldService -> CD-36. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.ClientMessage)]
public class TargetPlayerCommands(IWorldService worldService) : ISystem
{
    /// <summary>Teleports the admin to a target player's position.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.ClientMessage)]
    [PlayerCommand("goto")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void GoToPlayerPosition(
        Player currentPlayer, 
        [CommandParameter(Name = "playerId")]Player targetPlayer)
    {
        if (currentPlayer == targetPlayer)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerIsEqualsToTargetPlayer);
            return;
        }

        currentPlayer.Position = targetPlayer.Position;
    }

    /// <summary>Brings a target player to the admin's position.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.ClientMessage)]
    [PlayerCommand("get")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void BringPlayerToMyPosition(
        Player currentPlayer,
        [CommandParameter(Name = "playerId")]Player targetPlayer)
    {
        if (currentPlayer == targetPlayer)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerIsEqualsToTargetPlayer);
            return;
        }

        targetPlayer.Position = currentPlayer.Position;
    }

    /// <summary>Bans a target player.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.ClientMessage)]
    [PlayerCommand("ban")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void BanPlayer(
        Player currentPlayer,
        [CommandParameter(Name = "playerId")]Player targetPlayer,
        string reason)
    {
        if (currentPlayer == targetPlayer)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerIsEqualsToTargetPlayer);
            return;
        }

        if (targetPlayer.IsServerOwner())
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.CannotPerformActionOnServerOwner);
            return;
        }

        const int MaxLength = 50;
        if (reason.Length > MaxLength)
        {
            var message = Smart.Format(Messages.BanReason, new { Length = MaxLength });
            currentPlayer.SendClientMessage(Color.Red, message);
            return;
        }

        {
            var message = Smart.Format(Messages.SuccessfullyBanned, new
            {
                CurrentPlayer = currentPlayer.Name,
                TargetPlayer = targetPlayer.Name,
                Reason = reason
            });

            worldService.SendClientMessage(Color.Red, message);
        }

        targetPlayer.Ban(reason);
    }
}
