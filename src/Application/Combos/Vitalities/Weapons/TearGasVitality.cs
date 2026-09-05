namespace CTF.Application.Combos.Vitalities.Weapons;

[ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.Coin, ChangeDriver.Combat)]
public class TearGasVitality : ICombo
{
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private const int Health = 100;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private const int Armour = 100;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private const int TearGasAmmo = 30;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public string Name => $"{Health} Health, {Armour} Armour and Tear gas";
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public int RequiredCoins => 100;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public Result Give(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        player.Health = Health;
        player.Armour = Armour;
        player.GiveWeapon(Weapon.Teargas, TearGasAmmo);
        playerInfo.Stats.PerRound.ResetCoins();
        return Result.Success();
    }
}
