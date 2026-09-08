namespace CTF.Application.CoinEconomy.EcsDomain;

/// <summary>
/// Records the unix-time instant after which the give-me-coins
/// command may be used again.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Configuration, ChangeDriver.Ecs)]
public class WaitTimeComponent : Component
{
    [ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Configuration)]
    public long Value { get; set; }
}
