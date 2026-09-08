
namespace CTF.Application.CoinEconomy.PlayerDomain;

/// <remarks>No injected services. Adds the give-me-coins WaitTimeComponent when a player connects.</remarks>
[ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Player)]
public class CoinCooldownConnectSystem : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Player)]
    public void OnPlayerConnect(Player player)
        => player.AddComponent<WaitTimeComponent>();
}
