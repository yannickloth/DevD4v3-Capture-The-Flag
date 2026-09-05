namespace CTF.Application.Combos.Vitalities.Weapons;

[ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.Coin, ChangeDriver.Combat)]
public class MolotovVitality : ICombo
{
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private const int Health = 100;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private const int Armour = 100;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private const int MolotovAmmo = 6;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public string Name => $"{Health} Health, {Armour} Armour and Molotov cocktail";
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public int RequiredCoins => 100;

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public Result Give(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        player.Health = Health;
        player.Armour = Armour;
        player.GiveWeapon(Weapon.Moltov, MolotovAmmo);
        playerInfo.Stats.PerRound.ResetCoins();
        return Result.Success();
    }
}
