namespace CTF.Application.Combos.Vitalities.Weapons;

[ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.Coin, ChangeDriver.Combat)]
public class FlamethrowerVitality : ICombo
{
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private const int Health = 100;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private const int Armour = 100;

    // 1000 (shows in game as 50-50).
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private const int FlamethrowerAmmo = 1000;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public string Name => $"{Health} Health, {Armour} Armour and FlameThrower";
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public int RequiredCoins => 100;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public Result Give(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        player.Health = Health;
        player.Armour = Armour;
        player.GiveWeapon(Weapon.FlameThrower, FlamethrowerAmmo);
        playerInfo.Coins.Reset();
        return Result.Success();
    }
}
