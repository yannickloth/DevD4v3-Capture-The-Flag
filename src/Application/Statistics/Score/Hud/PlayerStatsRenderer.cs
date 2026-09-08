namespace CTF.Application.Statistics.TextDrawNsNs2;

/// <remarks>Injected dependencies: worldService -> CD-36. Driven by the IWorldService (platform) contract + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.TextDraw)]
public class PlayerStatsRenderer(IWorldService worldService)
{
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.TextDraw)]
    public void CreateTextDraw(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        bool isTextDrawCreated = player.GetComponent<PlayerStatsTextDraw>() is not null;
        if (isTextDrawCreated)
            return;

        PlayerTextDraw playerTextDraw = worldService.CreatePlayerTextDraw(
            player, 
            position: new Vector2(319.000000f, 433.000000f), 
            string.Empty
        );
        playerTextDraw.Font = TextDrawFont.Slim;
        playerTextDraw.LetterSize = new Vector2(0.279166f, 1.350000f);
        playerTextDraw.TextSize = new Vector2(12.000000f, 640.000000f);
        playerTextDraw.Outline = 1;
        playerTextDraw.Shadow = 0;
        playerTextDraw.Alignment = TextDrawAlignment.Center;
        playerTextDraw.ForeColor = new Color(-1);
        playerTextDraw.BackColor = new Color(255);
        playerTextDraw.BoxColor = new Color(101);
        playerTextDraw.UseBox = true;
        playerTextDraw.Proportional = true;
        playerTextDraw.Selectable = false;
        player.AddComponent<PlayerStatsTextDraw>(playerTextDraw);
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.TextDraw)]
    public void UpdateTextDraw(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        PlayerStatsTextDraw playerStatsTextDraw = GetTextDrawOrThrow(player);
        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerStatsTextDraw.Value.Text = GetStatsAsText(playerInfo);
        playerStatsTextDraw.Value.Show();
    }

    /// <summary>Formats the player's statistics as a textdraw-compatible string.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.TextDraw)]
    public static string GetStatsAsText(PlayerInfo playerInfo)
    {
        Result<Rank> rankResult = RankCollection.GetById(playerInfo.Stats.RankId);
        var stats = new
        {
            playerInfo.Stats.PerRound.Kills,
            playerInfo.Stats.PerRound.Deaths,
            playerInfo.Stats.PerRound.KillingSpree,
            Coins = playerInfo.Coins.Balance,
            MaxRank = RankCollection.Count,
            Level = (int)playerInfo.Stats.RankId + 1,
            RankName = rankResult.Value.Name
        };
        const string message =
            "~w~KILLS: ~y~{Kills} ~w~DEATHS: ~y~{Deaths} ~w~SPREE: ~y~{KillingSpree} " +
            "~w~COINS: ~y~{Coins}/100 ~w~LEVEL: ~y~{Level}/{MaxRank} ~w~RANK: ~y~{RankName}";
        return Smart.Format(message, stats);
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.TextDraw)]
    public void ShowTextDraw(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        PlayerStatsTextDraw playerStatsTextDraw = GetTextDrawOrThrow(player);
        playerStatsTextDraw.Value.Show();
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.TextDraw)]
    public void HideTextDraw(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        PlayerStatsTextDraw playerStatsTextDraw = GetTextDrawOrThrow(player);
        playerStatsTextDraw.Value.Hide();
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.TextDraw)]
    private PlayerStatsTextDraw GetTextDrawOrThrow(Player player)
    {
        return player.GetComponent<PlayerStatsTextDraw>()
             ?? throw new InvalidOperationException($"The '{nameof(PlayerStatsTextDraw)}' component is not attached to the player");
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.TextDraw)]
    private class PlayerStatsTextDraw : Component
    {
        [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.TextDraw)]
        public PlayerTextDraw Value { get; }

        /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-34 (Textdraw API) → CD-10</remarks>
        public PlayerStatsTextDraw(PlayerTextDraw value)
        {
            ArgumentNullException.ThrowIfNull(value);
            Value = value;
        }
    }
}
