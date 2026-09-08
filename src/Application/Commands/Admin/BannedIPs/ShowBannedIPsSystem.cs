namespace CTF.Application.Commands.ClientMessageNsNs3;

/// <summary>
/// Shows the list of banned IP addresses.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): dialogService -> CD-33. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Dialog, ChangeDriver.ClientMessage)]
public class ShowBannedIPsSystem(IDialogService dialogService) : ISystem
{
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
