namespace CTF.Application.GameRules.Flag.Returned;

/// <summary>
/// This event occurs when a player has returned the flag to their team's base.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; worldService -> CD-36; teamPickupService -> CD-37; playerStatsRenderer -> CD-10; flagAutoReturnTimer -> CD-02. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Repository)]
public class OnFlagReturned(
    IPlayerRepository playerRepository,
    IWorldService worldService,
    TeamPickupService teamPickupService,
    PlayerStatsRenderer playerStatsRenderer,
    FlagAutoReturnTimer flagAutoReturnTimer) : IFlagEvent
{
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Coin)]
    private const int EarnedCoins = 5;

    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Statistics)]
    private const int EarnedScore = 2;

    /// <summary>Gets the flag status handled by this event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public FlagStatus FlagStatus => FlagStatus.Returned;

    /// <summary>Handles the flag-returned event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Repository)]
    public void Handle(Team team, Player player)
    {
        teamPickupService.CreateFlagFromBasePosition(team);
        teamPickupService.DestroyExteriorMarker(team);
        team.Sounds.PlayFlagReturnedSound();
        flagAutoReturnTimer.Stop(team);
        var message = Smart.Format(Messages.OnFlagReturned, new
        {
            PlayerName = player.Name,
            TeamName = team.Name,
            team.ColorName
        });
        worldService.SendClientMessage(team.ColorHex, message);
        worldService.GameText($"~n~~n~~n~{team.GameTextColor}{team.ColorName} flag returned!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);

        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerInfo.Stats.PerRound.AddCoins(EarnedCoins);
        playerInfo.Stats.AddReturnedFlags();
        player.AddScore(EarnedScore);
        playerRepository.UpdateReturnedFlags(playerInfo);
        playerStatsRenderer.UpdateTextDraw(player);
    }
}
