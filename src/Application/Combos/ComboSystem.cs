namespace CTF.Application.Combos;

/// <remarks>Change drivers: CD-05 (root; combo definitions); CD-15 (command set) → CD-05; CD-06 (coin economy) → CD-05; CD-07 (GunGame mode rules) → CD-05; CD-31 (player key-state event); CD-33 (dialog); CD-34 (stats textdraw); CD-35 (GameText); CD-36 (client messages) → CD-05; CD-10 (player-statistics/rank model) → CD-05</remarks>
public class ComboSystem : ISystem
{
    /// <remarks>Change drivers: CD-33 (root; dialog API)</remarks>
    private readonly IDialogService _dialogService;

    /// <remarks>Change drivers: CD-05 (root; combo definitions: combos dialog); CD-33 (dialog) → CD-05</remarks>
    private readonly TablistDialog _tablistDialog;

    /// <remarks>Change drivers: CD-36 (root; client-message API via IWorldService)</remarks>
    private readonly IWorldService _worldService;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: stats textdraw renderer)</remarks>
    private readonly PlayerStatsRenderer _playerStatsRenderer;

    /// <remarks>Change drivers: CD-05 (root; combo definitions: available combos)</remarks>
    private readonly IEnumerable<ICombo> _combos;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules: combo availability)</remarks>
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
    /// <remarks>Change drivers: CD-05 (root; combo definitions); CD-07 (GunGame mode rules) → CD-05; CD-31 (OnPlayerKeyStateChange) → CD-05</remarks>
    public async Task OnPlayerKeyStateChange(Player player, Keys newKeys, Keys oldKeys)
    {
        if (_gunGameMode.IsEnabled)
            return;

        if (KeyUtils.HasPressed(newKeys, oldKeys, Keys.AnalogLeft))
            await ShowCombos(player);
    }

    [PlayerCommand("combos")]
    /// <remarks>Change drivers: CD-05 (root; combo definitions); CD-06 (coin economy) → CD-05; CD-07 (GunGame mode rules) → CD-05; CD-33 (dialog); CD-36 (client messages) → CD-05; CD-15 (command set) → CD-05</remarks>
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
        PlayerStatsPerRound playerStats = player.GetRequiredInfo().Stats.PerRound;
        if (playerStats.HasInsufficientCoins(selectedCombo.RequiredCoins))
        {
            player.SendClientMessage(Color.Red, Messages.InsufficientCoins);
            await ShowCombos(player);
            return;
        }
        await GiveComboToPlayer(player, selectedCombo);
    }

    /// <remarks>Change drivers: CD-05 (root; combo definitions); CD-06 (coin economy) → CD-05; CD-35 (GameText); CD-36 (client messages); CD-34 (stats textdraw) → CD-05</remarks>
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
