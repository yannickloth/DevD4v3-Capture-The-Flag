namespace CTF.Application.Commands.ClientMessageNsNs9;

/// <summary>
/// Issues a warning to a target player, kicking after the third warning.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): worldService -> CD-36. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
public class WarnCommands(IWorldService worldService) : ISystem
{
    /// <summary>Issues a warning to a target player, kicking after the third warning.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
    [PlayerCommand("warn")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void Warn(
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

        var warningsComponent = targetPlayer.GetComponent<WarningsComponent>();
        warningsComponent.Value++;

        var message = Smart.Format(Messages.WarningSuccessfullyGiven, new
        {
            CurrentPlayer = currentPlayer.Name,
            TargetPlayer = targetPlayer.Name,
            WarningsNumber = warningsComponent.Value,
            Reason = reason
        });

        worldService.SendClientMessage(Color.Yellow, message);

        if (warningsComponent.Value == 3)
        {
            targetPlayer.Kick();
        }
    }
}
