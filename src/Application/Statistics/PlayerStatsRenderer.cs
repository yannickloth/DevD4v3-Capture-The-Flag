namespace CTF.Application.Statistics;

/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-31 (player events); CD-32 (ECS runtime); CD-34 (Textdraw API) → CD-10</remarks>
/// <remarks>Injected dependencies: worldService -> CD-36. Driven by the IWorldService (platform) contract + CD-21 (DI wiring).</remarks>
public class PlayerStatsRenderer(IWorldService worldService)
{
    /// <remarks>Change drivers: CD-34 (root; textdraw API)</remarks>
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

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-32 (ECS runtime); CD-34 (Textdraw API) → CD-10</remarks>
    public void UpdateTextDraw(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        PlayerStatsTextDraw playerStatsTextDraw = GetTextDrawOrThrow(player);
        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerStatsTextDraw.Value.Text = GetStatsAsText(playerInfo);
        playerStatsTextDraw.Value.Show();
    }

    /// <summary>Formats the player's statistics as a textdraw-compatible string.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public static string GetStatsAsText(PlayerInfo playerInfo)
    {
        Result<Rank> rankResult = RankCollection.GetById(playerInfo.Stats.RankId);
        var stats = new
        {
            playerInfo.Stats.PerRound.Kills,
            playerInfo.Stats.PerRound.Deaths,
            playerInfo.Stats.PerRound.KillingSpree,
            playerInfo.Stats.PerRound.Coins,
            MaxRank = RankCollection.Count,
            Level = (int)playerInfo.Stats.RankId + 1,
            RankName = rankResult.Value.Name
        };
        const string message =
            "~w~KILLS: ~y~{Kills} ~w~DEATHS: ~y~{Deaths} ~w~SPREE: ~y~{KillingSpree} " +
            "~w~COINS: ~y~{Coins}/100 ~w~LEVEL: ~y~{Level}/{MaxRank} ~w~RANK: ~y~{RankName}";
        return Smart.Format(message, stats);
    }

    /// <remarks>Change drivers: CD-34 (root; textdraw API)</remarks>
    public void ShowTextDraw(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        PlayerStatsTextDraw playerStatsTextDraw = GetTextDrawOrThrow(player);
        playerStatsTextDraw.Value.Show();
    }

    /// <remarks>Change drivers: CD-34 (root; textdraw API)</remarks>
    public void HideTextDraw(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        PlayerStatsTextDraw playerStatsTextDraw = GetTextDrawOrThrow(player);
        playerStatsTextDraw.Value.Hide();
    }

    /// <remarks>Change drivers: CD-34 (root; textdraw API)</remarks>
    private PlayerStatsTextDraw GetTextDrawOrThrow(Player player)
    {
        return player.GetComponent<PlayerStatsTextDraw>()
             ?? throw new InvalidOperationException($"The '{nameof(PlayerStatsTextDraw)}' component is not attached to the player");
    }

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-32 (ECS runtime); CD-34 (Textdraw API) → CD-10</remarks>
    private class PlayerStatsTextDraw : Component
    {
        /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-34 (Textdraw API) → CD-10</remarks>
        public PlayerTextDraw Value { get; }

        /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-34 (Textdraw API) → CD-10</remarks>
        public PlayerStatsTextDraw(PlayerTextDraw value)
        {
            ArgumentNullException.ThrowIfNull(value);
            Value = value;
        }
    }
}
