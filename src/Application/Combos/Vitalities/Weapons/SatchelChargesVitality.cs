namespace CTF.Application.Combos.Vitalities.Weapons;

[ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.Coin, ChangeDriver.Combat)]
public class SatchelChargesVitality : ICombo
{
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private const int Health = 100;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private const int Armour = 100;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private const int SatchelAmmo = 6;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public string Name => $"{Health} Health, {Armour} Armour and Satchel charges";
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public int RequiredCoins => 100;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public Result Give(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        player.Health = Health;
        player.Armour = Armour;
        player.GiveWeapon(Weapon.SatchelCharge, SatchelAmmo);
        player.GiveWeapon(Weapon.Detonator, 1);
        playerInfo.Stats.PerRound.ResetCoins();
        return Result.Success();
    }
}
