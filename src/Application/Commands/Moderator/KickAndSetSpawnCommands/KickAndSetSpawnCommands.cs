namespace CTF.Application.Commands.Moderator.KickAndSetSpawnCommands;

/// <summary>
/// Moderator commands that act on a target player: kick them or respawn them at their spawn point.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): worldService -> CD-36. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.ClientMessage)]
public class KickAndSetSpawnCommands(IWorldService worldService) : ISystem
{
    /// <summary>Kicks a target player.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.ClientMessage)]
    [PlayerCommand("kick")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void Kick(
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

        var message = Smart.Format(Messages.SuccessfullyKicked, new
        {
            CurrentPlayer = currentPlayer.Name,
            TargetPlayer = targetPlayer.Name,
            Reason = reason
        });

        worldService.SendClientMessage(Color.Red, message);
        targetPlayer.Kick();
    }

    /// <summary>Respawns a target player at their spawn point.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.ClientMessage)]
    [PlayerCommand("setspawn")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void SetSpawn(
        Player currentPlayer,
        [CommandParameter(Name = "playerId")]Player targetPlayer)
    {
        if (targetPlayer.IsUnauthenticated())
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.UnauthenticatedPlayer);
            return;
        }

        if (targetPlayer.IsInClassSelection())
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerIsInClassSelection);
            return;
        }

        if (targetPlayer.State == PlayerState.Spectating)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerInSpectatorMode);
            return;
        }

        var message = Smart.Format(Messages.SetSpawnToPlayer, new { PlayerName = targetPlayer.Name });
        currentPlayer.SendClientMessage(Color.Yellow, message);
        targetPlayer.Spawn();
    }
}
