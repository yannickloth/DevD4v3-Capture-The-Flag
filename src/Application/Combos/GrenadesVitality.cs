namespace CTF.Application.Combos;

/// <remarks>Change drivers: CD-05 (root; combo definitions); CD-06 (coin economy) → CD-05; CD-03 (combat/weapon-rules specification) → CD-05</remarks>
public class GrenadesVitality : ICombo
{
    /// <remarks>Change drivers: CD-05 (root; combo definitions: reward health)</remarks>
    private const int Health = 100;

    /// <remarks>Change drivers: CD-05 (root; combo definitions: reward armour)</remarks>
    private const int Armour = 100;

    /// <remarks>Change drivers: CD-05 (root; combo definitions: grenade ammo)</remarks>
    private const int GrenadeAmmo = 6;

    /// <remarks>Change drivers: CD-05 (root; combo definitions)</remarks>
    public string Name => $"{Health} Health, {Armour} Armour and Grenades";
    /// <remarks>Change drivers: CD-05 (root; combo definitions: coin cost)</remarks>
    public int RequiredCoins => 100;

    /// <remarks>Change drivers: CD-05 (root; combo definitions)</remarks>
    public Result Give(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        player.Health = Health;
        player.Armour = Armour;
        player.GiveWeapon(Weapon.Grenade, GrenadeAmmo);
        playerInfo.Stats.PerRound.ResetCoins();
        return Result.Success();
    }
}
