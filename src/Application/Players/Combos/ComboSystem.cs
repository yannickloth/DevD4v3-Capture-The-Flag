namespace CTF.Application.Players.Combos;

/// <remarks>Change drivers: CD-05 (root; combo definitions); CD-06 (coin economy) → CD-05; CD-07 (GunGame mode rules) → CD-05; CD-01 (open.mp/SampSharp platform API) → CD-05</remarks>
public class ComboSystem : ISystem
{
    private readonly IDialogService _dialogService;
    private readonly TablistDialog _tablistDialog;
    private readonly IWorldService _worldService;
    private readonly PlayerStatsRenderer _playerStatsRenderer;
    private readonly IEnumerable<ICombo> _combos;
    private readonly IGunGameMode _gunGameMode;

    /// <remarks>Change drivers: CD-05 (root; combo definitions); CD-06 (coin economy) → CD-05; CD-01 (open.mp/SampSharp platform API) → CD-05</remarks>
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
    /// <remarks>Change drivers: CD-05 (root; combo definitions); CD-07 (GunGame mode rules) → CD-05; CD-01 (open.mp/SampSharp platform API) → CD-05</remarks>
    public async Task OnPlayerKeyStateChange(Player player, Keys newKeys, Keys oldKeys)
    {
        if (_gunGameMode.IsEnabled)
            return;

        if (KeyUtils.HasPressed(newKeys, oldKeys, Keys.AnalogLeft))
            await ShowCombos(player);
    }

    [PlayerCommand("combos")]
    /// <remarks>Change drivers: CD-05 (root; combo definitions); CD-06 (coin economy) → CD-05; CD-07 (GunGame mode rules) → CD-05; CD-01 (open.mp/SampSharp platform API) → CD-05; CD-15 (command set) → CD-05</remarks>
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
        PlayerStatsPerRound playerStats = player.GetRequiredInfo().StatsPerRound;
        if (playerStats.HasInsufficientCoins(selectedCombo.RequiredCoins))
        {
            player.SendClientMessage(Color.Red, Messages.InsufficientCoins);
            await ShowCombos(player);
            return;
        }
        await GiveComboToPlayer(player, selectedCombo);
    }

    /// <remarks>Change drivers: CD-05 (root; combo definitions); CD-06 (coin economy) → CD-05; CD-01 (open.mp/SampSharp platform API) → CD-05</remarks>
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
