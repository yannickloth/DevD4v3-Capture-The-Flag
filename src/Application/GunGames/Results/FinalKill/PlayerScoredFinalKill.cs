namespace CTF.Application.GunGames.Results.FinalKill;

/// <summary>
/// Handles the <see cref="GunGameResult.ScoredFinalKill"/> result.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; playerRepository -> CD-20. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Statistics, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
public class PlayerScoredFinalKill(
    IWorldService worldService,
    IPlayerRepository playerRepository) : IGunGameResultHandler
{
    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Statistics, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
    public GunGameResult Result => GunGameResult.ScoredFinalKill;

    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Statistics, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
    public void Handle(KillContext context)
    {
        PlayerInfo killerInfo = context.Killer.GetRequiredInfo();
        killerInfo.Stats.AddGunGameWins();
        playerRepository.UpdateGunGameWins(killerInfo);

        var message = Smart.Format(GunGameMessages.PlayerScoredFinalKill, new
        {
            Killer = context.Killer.Name
        });

        worldService.SendClientMessage(Color.Gold, message);
    }
}
