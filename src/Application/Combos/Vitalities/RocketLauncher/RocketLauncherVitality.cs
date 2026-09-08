
namespace CTF.Application.Combos.CoinDomain;

/// <remarks>Injected dependencies: comboSettings -> CD-05. Driven by the ComboSettings contract + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.Coin)]
public class RocketLauncherVitality(ComboSettings comboSettings) : ICombo
{
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private const int Health = 100;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private const int RocketLauncherAmmo = 2;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public string Name => $"{Health} Health and Rocket launcher(RPG)";
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public int RequiredCoins => 100;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public Result Give(Player player)
    {
        if (comboSettings.IsRocketLauncherDisabled)
        {
            player.SendClientMessage(Color.Red, Messages.RocketLauncherDisabled);
            return Result.Failure();
        }

        PlayerInfo playerInfo = player.GetRequiredInfo();
        player.Health = Health;
        player.GiveWeapon(Weapon.RocketLauncher, RocketLauncherAmmo);
        playerInfo.Coins.Reset();
        return Result.Success();
    }
}
