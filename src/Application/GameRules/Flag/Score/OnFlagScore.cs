namespace CTF.Application.GameRules.Flag.Score;

/// <summary>
/// This event occurs when a player has captured the opposing team's flag and brought it back to their own base.
/// Uniform flow: the CTF flag rules (CD-02) plus the engaged contracts of the score
/// handling (coin CD-06, statistics CD-10, repository CD-20, player CD-31, textdraw CD-34,
/// pickup CD-37, audio CD-40, GameText CD-35, client-message CD-36).
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; worldService -> CD-36; teamPickupService -> CD-37; teamTextDrawRenderer -> CD-34; playerStatsRenderer -> CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Repository)]
public class OnFlagScore(
    IPlayerRepository playerRepository,
    IWorldService worldService,
    TeamPickupService teamPickupService,
    TeamTextDrawRenderer teamTextDrawRenderer,
    PlayerStatsRenderer playerStatsRenderer) : IFlagEvent
{
    /// <summary>Gets the flag status handled by this event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Repository)]
    public FlagStatus FlagStatus => FlagStatus.Brought;

    /// <summary>Handles the flag-score event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Repository)]
    public void Handle(Team team, Player player)
    {
        teamPickupService.CreateFlagFromBasePosition(team.RivalTeam);
        teamPickupService.DestroyExteriorMarker(team.RivalTeam);
        team.Sounds.PlayTeamScoresSound();
        teamTextDrawRenderer.UpdateTeamScore(team);

        var message = Smart.Format(Messages.OnFlagScore, new
        {
            PlayerName = player.Name,
            TeamName = team.Name,
            team.RivalTeam.ColorName
        });
        worldService.SendClientMessage(team.ColorHex, message);
        worldService.GameText($"~n~~n~~n~{team.GameTextColor}{team.ColorName} team scores!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);

        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerInfo.Coins.AddCoins(FlagScoreRewards.CarrierEarnedCoins);
        playerInfo.Stats.AddBroughtFlags();
        player.AddScore(FlagScoreRewards.CarrierEarnedScore);
        player.HideOnRadarMap();
        playerRepository.UpdateBroughtFlags(playerInfo);
        GiveRewards(team);
    }

    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Repository)]
    private void GiveRewards(Team team)
    {
        TeamMembers teamMembers = team.Members;
        foreach (Player player in teamMembers)
        {
            PlayerInfo playerInfo = player.GetRequiredInfo();
            playerInfo.Coins.AddCoins(FlagScoreRewards.TeamEarnedCoins);
            player.AddHealth(FlagScoreRewards.TeamEarnedHealth);
            player.AddScore(FlagScoreRewards.TeamEarnedScore);
            playerStatsRenderer.UpdateTextDraw(player);
        }
    }
}
