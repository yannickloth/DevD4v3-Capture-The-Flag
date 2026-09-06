namespace CTF.Application.GameRules.Flag.Captured;

/// <summary>
/// This event occurs when a player has captured the opposing team's flag from their base.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; worldService -> CD-36; teamPickupService -> CD-37; playerStatsRenderer -> CD-10; flagCarrierSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.Audio, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Repository, ChangeDriver.Configuration)]
public class OnFlagCaptured(
    IPlayerRepository playerRepository,
    IWorldService worldService,
    TeamPickupService teamPickupService,
    PlayerStatsRenderer playerStatsRenderer,
    FlagCarrierSettings flagCarrierSettings) : IFlagEvent
{
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Coin)]
    private const int EarnedCoins = 5;

    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Statistics)]
    private const int EarnedScore = 2;

    /// <summary>Gets the flag status handled by this event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public FlagStatus FlagStatus => FlagStatus.Captured;

    /// <summary>Handles the flag-captured event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Repository)]
    public void Handle(Team team, Player player)
    {
        teamPickupService.CreateExteriorMarker(team);
        teamPickupService.DestroyFlag(team);
        team.Sounds.PlayFlagTakenSound();
        var message = Smart.Format(Messages.OnFlagCaptured, new
        {
            PlayerName = player.Name,
            TeamName = team.Name,
            team.ColorName
        });
        worldService.SendClientMessage(team.ColorHex, message);
        worldService.GameText($"~n~~n~~n~{team.GameTextColor}{team.ColorName} flag captured!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);

        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerInfo.Coins.AddCoins(EarnedCoins);
        playerInfo.Stats.AddCapturedFlags();
        player.AddScore(EarnedScore);
        if (flagCarrierSettings.ShowOnRadarMap)
        {
            player.ShowOnRadarMap();
        }
        playerRepository.UpdateCapturedFlags(playerInfo);
        playerStatsRenderer.UpdateTextDraw(player);
    }
}
