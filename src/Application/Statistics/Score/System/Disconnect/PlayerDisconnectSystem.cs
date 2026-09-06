namespace CTF.Application.Statistics.Score.System.Disconnect;

/// <summary>
/// Persists the last-connection time when an authenticated player disconnects.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): playerRepository -> CD-20. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Account, ChangeDriver.Repository)]
public class PlayerDisconnectSystem(
    IPlayerRepository playerRepository) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Account, ChangeDriver.Repository)]
    [Event]
    public void OnPlayerDisconnect(Player player, DisconnectReason reason)
    {
        if (player.IsUnauthenticated())
            return;

        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerInfo.Stats.SetLastConnection();
        playerRepository.UpdateLastConnection(playerInfo);
    }
}
