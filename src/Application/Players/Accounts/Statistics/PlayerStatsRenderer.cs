namespace CTF.Application.Players.Accounts.Statistics;

/// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API), CD-10 (player-statistics/rank model)</remarks>
/// <remarks>Injected dependencies: worldService -> CD-01. Driven by the IWorldService (platform) contract + CD-21 (DI wiring).</remarks>
public class PlayerStatsRenderer(IWorldService worldService)
{
    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API)</remarks>
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

    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API), CD-10 (player-statistics/rank model)</remarks>
    public void UpdateTextDraw(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        PlayerStatsTextDraw playerStatsTextDraw = GetTextDrawOrThrow(player);
        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerStatsTextDraw.Value.Text = playerInfo.GetStatsAsText();
        playerStatsTextDraw.Value.Show();
    }

    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API)</remarks>
    public void ShowTextDraw(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        PlayerStatsTextDraw playerStatsTextDraw = GetTextDrawOrThrow(player);
        playerStatsTextDraw.Value.Show();
    }

    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API)</remarks>
    public void HideTextDraw(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        PlayerStatsTextDraw playerStatsTextDraw = GetTextDrawOrThrow(player);
        playerStatsTextDraw.Value.Hide();
    }

    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API)</remarks>
    private PlayerStatsTextDraw GetTextDrawOrThrow(Player player)
    {
        return player.GetComponent<PlayerStatsTextDraw>()
             ?? throw new InvalidOperationException($"The '{nameof(PlayerStatsTextDraw)}' component is not attached to the player");
    }

    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API)</remarks>
    private class PlayerStatsTextDraw : Component
    {
        /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API)</remarks>
        public PlayerTextDraw Value { get; }

        /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API)</remarks>
        public PlayerStatsTextDraw(PlayerTextDraw value)
        {
            ArgumentNullException.ThrowIfNull(value);
            Value = value;
        }
    }
}
