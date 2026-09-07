namespace CTF.Application.GunGames;

/// <summary>
/// The GunGame victory reward table: the reward amounts and the weapon reward options,
/// governed solely by the GunGame-mode rules (CD-07).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public static class GunGameRewardTable
{
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public const int WinnerEarnedHealth = 100;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public const int WinnerEarnedArmour = 100;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public const int WinnerEarnedCoins  = 100;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public const int TeamEarnedHealth   = 50;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public const int TeamEarnedArmour   = 50;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public const int TeamEarnedCoins    = 15;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public const int TeamEarnedScore    = 3;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public readonly record struct WeaponReward(IWeapon Weapon, int Ammo);

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public static readonly WeaponReward[] WeaponRewards =
    [
        new(WeaponDefinitions.Grenade,       Ammo: 5),
        new(WeaponDefinitions.Molotov,       Ammo: 5),
        new(WeaponDefinitions.SatchelCharge, Ammo: 5),
        new(WeaponDefinitions.Flamethrower,  Ammo: 1000)
    ];
}
