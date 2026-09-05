namespace CTF.Application.Commands.Basic;

/// <summary>
/// Provides the basic (public) command set.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): entityManager -> CD-32; dialogService -> CD-33. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.ClientMessage)]
public class BasicCommands(
    IEntityManager entityManager,
    IDialogService dialogService) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.GameRules)]
    private const float MinimumHealthToUseKillCommand = 15f;

    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.GameRules)]
    private const float MinimumHealthToUseSpectatorCommand = 85f;

    /// <summary>Shows the first page of public commands.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Dialog)]
    [PlayerCommand("cmds")]
    public async Task ShowFirstCommandsPage(Player player)
    {
        var content = Smart.Format(DetailedCommandInfo.Public1, new
        {
            Color1 = Color.Yellow,
            Color2 = Color.White
        });

        var dialog = new MessageDialog(
            caption: "Commands [1/2]",
            content,
            button1: "Next",
            button2: "Close"
        );

        MessageDialogResponse response = await dialogService.ShowAsync(player, dialog);

        if (response.Response == DialogResponse.LeftButton)
            await ShowSecondCommandsPage(player);
    }

    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Dialog)]
    private async Task ShowSecondCommandsPage(Player player)
    {
        var content = Smart.Format(DetailedCommandInfo.Public2, new
        {
            Color1 = Color.Yellow,
            Color2 = Color.White
        });

        var dialog = new MessageDialog(
            caption: "Commands [2/2]",
            content,
            button1: "Previous",
            button2: "Close"
        );

        MessageDialogResponse response = await dialogService.ShowAsync(player, dialog);

        if (response.Response == DialogResponse.LeftButton)
            await ShowFirstCommandsPage(player);
    }

    /// <summary>Shows the help dialog.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Dialog)]
    [PlayerCommand("help")]
    public void ShowHelp(Player player)
    {
        var content = Smart.Format(DetailedCommandInfo.Help, new
        {
            Color1 = Color.Yellow,
            Color2 = Color.White
        });

        var dialog = new MessageDialog(
            caption: "Help", 
            content, 
            button1: "Close"
        );

        dialogService.ShowAsync(player, dialog);
    }

    /// <summary>Shows the credits dialog.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Dialog)]
    [PlayerCommand("credits")]
    public void ShowCredits(Player player)
    {
        var content = Smart.Format(DetailedCommandInfo.Credits, new
        {
            Color1 = Color.Yellow,
            Color2 = Color.White
        });

        var dialog = new MessageDialog(
            caption: "Credits",
            content,
            button1: "Close"
        );

        dialogService.ShowAsync(player, dialog);
    }

    /// <summary>Eliminates the player's character for respawn purposes, subject to a minimum-health rule.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.ClientMessage)]
    [PlayerCommand("kill")]
    public void Kill(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();

        if (playerInfo.Appearance.Team == Team.None)
        {
            player.SendClientMessage(Color.Red, Messages.NoTeam);
            return;
        }

        if (player.Health < MinimumHealthToUseKillCommand)
        {
            player.SendClientMessage(Color.Red, Messages.NotEnoughHealth);
            return;
        }

        player.Health = 0;
    }

    /// <summary>Reports a target player to the moderators/admins.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
    [PlayerCommand("report")]
    public void ReportPlayer(
        Player currentPlayer,
        [CommandParameter(Name = "playerId")]Player targetPlayer,
        string reason)
    {
        if (currentPlayer == targetPlayer)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerIsEqualsToTargetPlayer);
            return;
        }

        IEnumerable<Player> admins = entityManager
            .GetComponents<Player>()
            .Where(player => player.GetRequiredInfo().Role.Id >= RoleId.Moderator);

        if (!admins.Any())
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.NoAdminsConnected);
            return;
        }

        var message = Smart.Format(Messages.ReportToAnotherPlayer, new
        {
            CurrentPlayer = currentPlayer.Name,
            TargetPlayer = targetPlayer.Name,
            Reason = reason
        });

        foreach (Player admin in admins)
        {
            admin.SendClientMessage(Color.Red, message);
        }

        currentPlayer.SendClientMessage(Color.Yellow, Messages.ReportSuccessfullySent);
        currentPlayer.PlaySound(1058);
    }

    /// <summary>Enables spectator mode on a target player, subject to a minimum-health rule.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.ClientMessage)]
    [PlayerCommand("spec")]
    public void EnableSpectatorMode(
        Player currentPlayer,
        [CommandParameter(Name = "playerId")]Player targetPlayer,
        TeamTextDrawRenderer teamTextDrawRenderer)
    {
        if (currentPlayer == targetPlayer)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerIsEqualsToTargetPlayer);
            return;
        }

        if (targetPlayer.IsInClassSelection())
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerIsInClassSelection);
            return;
        }

        if (currentPlayer.GetRequiredInfo().Appearance.Team.RivalTeam.Flag.IsCarriedBy(currentPlayer))
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.HasCapturedFlag);
            return;
        }

        if (currentPlayer.Health < MinimumHealthToUseSpectatorCommand)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.NotEnoughHealth);
            return;
        }

        Team removedTeam = currentPlayer.RemoveFromCurrentTeam();
        teamTextDrawRenderer.UpdateTeamMembers(removedTeam);
        currentPlayer.Interior = targetPlayer.Interior;
        currentPlayer.VirtualWorld = targetPlayer.VirtualWorld;
        currentPlayer.ToggleSpectating(true);
        currentPlayer.SpectatePlayer(targetPlayer);
        currentPlayer.SendClientMessage(Color.Yellow, Messages.ExitSpectatorMode);
    }
}
