namespace CTF.Application.GameRules.Flag.Score;

/// <summary>
/// This event occurs when a player has captured the opposing team's flag and brought it back to their own base.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; worldService -> CD-36; teamPickupService -> CD-37; teamTextDrawRenderer -> CD-34; playerStatsRenderer -> CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Repository)]
public class OnFlagScore(
    IPlayerRepository playerRepository,
    IWorldService worldService,
    TeamPickupService teamPickupService,
    TeamTextDrawRenderer teamTextDrawRenderer,
    PlayerStatsRenderer playerStatsRenderer) : IFlagEvent
{
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    private const int CarrierEarnedCoins = 8;

    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    private const int CarrierEarnedScore = 4;

    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    private const int TeamEarnedCoins    = 5;

    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    private const int TeamEarnedHealth   = 10;

    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    private const int TeamEarnedScore    = 1;

    /// <summary>Gets the flag status handled by this event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public FlagStatus FlagStatus => FlagStatus.Brought;

    /// <summary>Handles the flag-score event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Repository)]
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
        playerInfo.Coins.AddCoins(CarrierEarnedCoins);
        playerInfo.Stats.AddBroughtFlags();
        player.AddScore(CarrierEarnedScore);
        player.HideOnRadarMap();
        playerRepository.UpdateBroughtFlags(playerInfo);
        GiveRewards(team);
    }

    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.TextDraw)]
    private void GiveRewards(Team team)
    {
        TeamMembers teamMembers = team.Members;
        foreach (Player player in teamMembers)
        {
            PlayerInfo playerInfo = player.GetRequiredInfo();
            playerInfo.Coins.AddCoins(TeamEarnedCoins);
            player.AddHealth(TeamEarnedHealth);
            player.AddScore(TeamEarnedScore);
            playerStatsRenderer.UpdateTextDraw(player);
        }
    }
}
