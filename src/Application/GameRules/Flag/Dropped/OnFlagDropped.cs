namespace CTF.Application.GameRules.Flag.Dropped;

/// <summary>
/// This event occurs when a player has dropped the opposing team's flag.
/// Uniform flow: the CTF flag rules (CD-02) plus the engaged contracts of the drop
/// handling (statistics CD-10, repository CD-20, pickup CD-37, map icon CD-38,
/// audio CD-40, GameText CD-35, client-message CD-36).
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; worldService -> CD-36; teamPickupService -> CD-37; flagAutoReturnTimer -> CD-02. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.Audio, ChangeDriver.Statistics, ChangeDriver.Repository)]
public class OnFlagDropped(
    IPlayerRepository playerRepository,
    IWorldService worldService,
    TeamPickupService teamPickupService,
    FlagAutoReturnTimer flagAutoReturnTimer) : IFlagEvent
{
    /// <summary>Gets the flag status handled by this event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.Audio, ChangeDriver.Statistics, ChangeDriver.Repository)]
    public FlagStatus FlagStatus => FlagStatus.Dropped;

    /// <summary>Handles the flag-dropped event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.Audio, ChangeDriver.Statistics, ChangeDriver.Repository)]
    public void Handle(Team team, Player player)
    {
        teamPickupService.CreateFlagFromVector3(team, player.Position);
        team.Sounds.PlayFlagDroppedSound();
        flagAutoReturnTimer.Start(team);
        team.Flag.Drop();
        var message = Smart.Format(Messages.OnFlagDropped, new
        {
            PlayerName = player.Name,
            TeamName = team.Name,
            team.ColorName
        });
        worldService.SendClientMessage(team.ColorHex, message);
        worldService.GameText($"~n~~n~~n~{team.GameTextColor}{team.ColorName} flag dropped!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);

        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerInfo.Stats.AddDroppedFlags();
        player.HideOnRadarMap();
        playerRepository.UpdateDroppedFlags(playerInfo);
    }
}
