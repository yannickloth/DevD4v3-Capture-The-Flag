namespace CTF.Application.Players.GeneralCommands;

/// <summary>
/// Provides the moderator-role command set.
/// </summary>
/// <remarks>Change drivers: CD-09 (authorization policy); CD-15 (command set); CD-01 (open.mp/SampSharp platform API)</remarks>
/// <remarks>Injected dependencies: worldService -> CD-01. Driven by the IWorldService (platform) contract + CD-21 (DI wiring).</remarks>
public class ModeratorCommands(IWorldService worldService) : ISystem
{
    /// <summary>Shows the moderator commands dialog.</summary>
    /// <remarks>Change drivers: CD-09 (authorization policy); CD-15 (command set); CD-01 (open.mp/SampSharp platform API)</remarks>
    [PlayerCommand("cmdsmoderator")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void ShowModeratorCommands(Player player, IDialogService dialogService)
    {
        var content = Smart.Format(DetailedCommandInfo.Moderator, new
        {
            Color1 = Color.Yellow,
            Color2 = Color.White
        });

        var dialog = new MessageDialog(
            caption: "Moderator Commands",
            content,
            button1: "Close"
        );

        dialogService.ShowAsync(player, dialog);
    }

    /// <summary>Kicks a target player.</summary>
    /// <remarks>Change drivers: CD-09 (authorization policy); CD-15 (command set); CD-01 (open.mp/SampSharp platform API)</remarks>
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
    /// <remarks>Change drivers: CD-09 (authorization policy); CD-15 (command set); CD-01 (open.mp/SampSharp platform API)</remarks>
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

    /// <summary>Clears the chat for all players.</summary>
    /// <remarks>Change drivers: CD-09 (authorization policy); CD-15 (command set); CD-01 (open.mp/SampSharp platform API)</remarks>
    [PlayerCommand("clearallchat")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void ClearAllChat(Player currentPlayer)
    {
        for (int i = 0; i < 200; i++)
        {
            worldService.SendClientMessage(" ");
        }
    }

    /// <summary>Issues a warning to a target player, kicking after the third warning.</summary>
    /// <remarks>Change drivers: CD-09 (authorization policy); CD-15 (command set); CD-01 (open.mp/SampSharp platform API)</remarks>
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

    /// <summary>Adds the warnings component when a player connects.</summary>
    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API)</remarks>
    [Event]
    public void OnPlayerConnect(Player player)
    {
        player.AddComponent<WarningsComponent>();
    }

    private class WarningsComponent : Component
    {
        public int Value { get; set; }
    }
}
