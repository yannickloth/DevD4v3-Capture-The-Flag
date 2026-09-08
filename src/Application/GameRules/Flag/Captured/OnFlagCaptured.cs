namespace CTF.Application.GameRules.ConfigurationNsNs7;

/// <summary>
/// This event occurs when a player has captured the opposing team's flag from their base.
/// Uniform flow: the CTF flag rules (CD-02) plus the engaged contracts of the capture
/// handling (coin CD-06, statistics CD-10, repository CD-20, pickup CD-37, map icon CD-38,
/// audio CD-40, GameText CD-35, client-message CD-36, configuration CD-17).
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
    /// <summary>Gets the flag status handled by this event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.Audio, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Repository, ChangeDriver.Configuration)]
    public FlagStatus FlagStatus => FlagStatus.Captured;

    /// <summary>Handles the flag-captured event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.Audio, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Repository, ChangeDriver.Configuration)]
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
        playerInfo.Coins.AddCoins(FlagCapturedRewards.EarnedCoins);
        playerInfo.Stats.AddCapturedFlags();
        player.AddScore(FlagCapturedRewards.EarnedScore);
        if (flagCarrierSettings.ShowOnRadarMap)
        {
            player.ShowOnRadarMap();
        }
        playerRepository.UpdateCapturedFlags(playerInfo);
        playerStatsRenderer.UpdateTextDraw(player);
    }
}
