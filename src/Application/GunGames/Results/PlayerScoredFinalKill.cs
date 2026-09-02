namespace CTF.Application.GunGames.Results;

/// <summary>
/// Handles the <see cref="GunGameResult.ScoredFinalKill"/> result.
/// </summary>
/// <remarks>Change drivers: CD-10 (player-statistics/rank model); CD-07 (GunGame mode rules); CD-20 (outbound repository contract)</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-01; playerRepository -> CD-20. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class PlayerScoredFinalKill(
    IWorldService worldService,
    IPlayerRepository playerRepository) : IGunGameResultHandler
{
    /// <remarks>Change drivers: CD-07 (GunGame mode rules)</remarks>
    public GunGameResult Result => GunGameResult.ScoredFinalKill;

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model); CD-07 (GunGame mode rules); CD-20 (outbound repository contract); CD-01 (open.mp/SampSharp platform API)</remarks>
    public void Handle(KillContext context)
    {
        PlayerInfo killerInfo = context.Killer.GetRequiredInfo();
        killerInfo.AddGunGameWins();
        playerRepository.UpdateGunGameWins(killerInfo);

        var message = Smart.Format(GunGameMessages.PlayerScoredFinalKill, new
        {
            Killer = context.Killer.Name
        });

        worldService.SendClientMessage(Color.Gold, message);
    }
}
