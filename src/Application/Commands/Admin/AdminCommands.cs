namespace CTF.Application.Commands.Admin;

/// <summary>
/// Provides the admin-role command set.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): entityManager -> CD-32; serverService -> CD-42; worldService -> CD-36; dialogService -> CD-33. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.ServerService)]
public class AdminCommands(
    IEntityManager entityManager,
    IServerService serverService,
    IWorldService worldService,
    IDialogService dialogService) : ISystem
{
    /// <summary>Shows the admin commands dialog.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Dialog)]
    [PlayerCommand("cmdsadmin")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void ShowAdminCommands(Player player)
    {
        var content = Smart.Format(DetailedCommandInfo.Admin, new
        {
            Color1 = Color.Yellow,
            Color2 = Color.White
        });

        var dialog = new MessageDialog(
            caption: "Admin Commands", 
            content, 
            button1: "Close"
        );

        dialogService.ShowAsync(player, dialog);
    }

    /// <summary>Gives a jetpack to all connected players.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
    [PlayerCommand("jetall")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void GiveJetpackToPlayers(Player currentPlayer)
    {
        var players = entityManager.GetComponents<Player>();

        foreach (Player player in players)
        {
            player.SpecialAction = SpecialAction.UseJetpack;
        }

        var message = Smart.Format(Messages.GiveJetpackToPlayers, new 
        { 
            PlayerName = currentPlayer.Name 
        });

        worldService.SendClientMessage(Color.Yellow, message);
    }

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

    /// <summary>Unbans a player IP address.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.ServerService, ChangeDriver.ClientMessage)]
    [PlayerCommand("unban")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void UnbanPlayer(Player currentPlayer, string ip)
    {
        var message = Smart.Format(Messages.SuccessfullyUnbanned, new { Ip = ip });
        currentPlayer.SendClientMessage(Color.Yellow, message);
        serverService.SendRconCommand($"unbanip {ip}");
    }

    /// <summary>Shows the list of banned IP addresses.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Dialog, ChangeDriver.ClientMessage)]
    [PlayerCommand("bannedips")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void ShowBannedIPs(Player currentPlayer)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "bans.json");
        var content = File.ReadAllText(path);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var bannedPlayers = JsonSerializer.Deserialize<BannedPlayer[]>(content, options);

        if (bannedPlayers.Length == 0)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.NoMatchFound);
            return;
        }

        var dialog = new ListDialog(
            caption: $"Banned Players: {bannedPlayers.Length}", 
            button1: "Close"
        );

        foreach (BannedPlayer bannedPlayer in bannedPlayers)
        {
            dialog.Add(bannedPlayer.ToString());
        }

        dialogService.ShowAsync(currentPlayer, dialog);
    }

    [ChangeDriversAttribute(ChangeDriver.CommandSet)]
    private class BannedPlayer
    {
        [ChangeDriversAttribute(ChangeDriver.CommandSet)]
        public string Address { get; set; } = string.Empty;
        [ChangeDriversAttribute(ChangeDriver.CommandSet)]
        public string Player { get; set; } = string.Empty;
        [ChangeDriversAttribute(ChangeDriver.CommandSet)]
        public string Reason { get; set; } = string.Empty;
        [ChangeDriversAttribute(ChangeDriver.CommandSet)]
        public string Time { get; set; } = "2023-12-07T16:05:21-0500";
        [ChangeDriversAttribute(ChangeDriver.CommandSet)]
        public override string ToString()
        {
            var dt = DateTimeOffset.Parse(Time).DateTime;
            var date = dt.ToString("yyyy/MM/dd");
            var time = dt.ToString("HH:mm:ss");
            return $"{Address} [{date} | {time}] {Player} - {Reason}";
        }
    }
}
