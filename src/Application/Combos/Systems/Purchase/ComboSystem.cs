namespace CTF.Application.Combos.Systems.Purchase;

[ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.CommandSet, ChangeDriver.Coin, ChangeDriver.GunGame, ChangeDriver.Player, ChangeDriver.Dialog, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.ClientMessage, ChangeDriver.Statistics)]
public class ComboSystem : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Dialog)]
    private readonly IDialogService _dialogService;

    [ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.Dialog)]
    private readonly TablistDialog _tablistDialog;

    [ChangeDriversAttribute(ChangeDriver.ClientMessage)]
    private readonly IWorldService _worldService;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    private readonly PlayerStatsRenderer _playerStatsRenderer;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private readonly IEnumerable<ICombo> _combos;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    private readonly IGunGameMode _gunGameMode;

    /// <remarks>Change drivers: CD-05 (root; combo definitions); CD-06 (coin economy) → CD-05; CD-33 (dialog) → CD-05</remarks>
    public ComboSystem(
        IDialogService dialogService,
        IWorldService worldService,
        PlayerStatsRenderer playerStatsRenderer,
        IEnumerable<ICombo> combos,
        IGunGameMode gunGameMode)
    {
        _dialogService = dialogService;
        _worldService = worldService;
        _playerStatsRenderer = playerStatsRenderer;
        _combos = combos;
        _gunGameMode = gunGameMode;

        var columnHeaders = new[]
        {
            "Combo",
            "Required Coins"
        };

        _tablistDialog = new TablistDialog(
            caption: "Combos",
            button1: "Select",
            button2: "Close",
            columnHeaders);

        foreach (ICombo combo in combos)
            _tablistDialog.Add(combo.Name, combo.RequiredCoins.ToString());
    }

    [Event]
    [ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.GunGame, ChangeDriver.Player)]
    public async Task OnPlayerKeyStateChange(Player player, Keys newKeys, Keys oldKeys)
    {
        if (_gunGameMode.IsEnabled)
            return;

        if (KeyUtils.HasPressed(newKeys, oldKeys, Keys.AnalogLeft))
            await ShowCombos(player);
    }

    [PlayerCommand("combos")]
    [ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.Coin, ChangeDriver.GunGame, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
    public async Task ShowCombos(Player player)
    {
        if (_gunGameMode.IsEnabled)
        {
            player.SendClientMessage(Color.Red, Messages.CombosUnavailable);
            return;
        }

        TablistDialogResponse response = await _dialogService.ShowAsync(player, _tablistDialog);
        if (response.IsRightButtonOrDisconnected())
            return;

        if (_gunGameMode.IsEnabled)
        {
            player.SendClientMessage(Color.Red, Messages.CombosUnavailable);
            return;
        }

        string selectedItemName = response.Item.Columns[0];
        ICombo selectedCombo = _combos.First(combo => combo.Name == selectedItemName);
        PlayerInfo playerInfo = player.GetRequiredInfo();
        if (playerInfo.Coins.HasInsufficientCoins(selectedCombo.RequiredCoins))
        {
            player.SendClientMessage(Color.Red, Messages.InsufficientCoins);
            await ShowCombos(player);
            return;
        }
        await GiveComboToPlayer(player, selectedCombo);
    }

    [ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.Coin, ChangeDriver.GameText, ChangeDriver.ClientMessage, ChangeDriver.TextDraw)]
    private async Task GiveComboToPlayer(Player player, ICombo selectedCombo)
    {
        Result result = selectedCombo.Give(player);
        if (result.IsFailed)
        {
            await ShowCombos(player);
            return;
        }

        var message = Smart.Format(Messages.RedeemedCoins, new
        {
            PlayerName = player.Name,
            ComboName = selectedCombo.Name
        });
        _worldService.SendClientMessage(Color.Yellow, message);
        _worldService.GameText(Messages.ComboUsage, TimeSpan.FromSeconds(5), GameTextStyle.Style3);
        _playerStatsRenderer.UpdateTextDraw(player);
    }
}
