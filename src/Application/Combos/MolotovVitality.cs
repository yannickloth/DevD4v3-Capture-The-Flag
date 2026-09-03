namespace CTF.Application.Combos;

/// <remarks>Change drivers: CD-05 (root; combo definitions); CD-06 (coin economy) → CD-05</remarks>
public class MolotovVitality : ICombo
{
    /// <remarks>Change drivers: CD-05 (root; combo definitions: reward health); CD-03 (combat/weapon-rules specification: health) → CD-05</remarks>
    private const int Health = 100;

    /// <remarks>Change drivers: CD-05 (root; combo definitions: reward armour); CD-03 (combat/weapon-rules specification: armour) → CD-05</remarks>
    private const int Armour = 100;

    /// <remarks>Change drivers: CD-05 (root; combo definitions: molotov ammo)</remarks>
    private const int MolotovAmmo = 6;

    /// <remarks>Change drivers: CD-05 (root; combo definitions)</remarks>
    public string Name => $"{Health} Health, {Armour} Armour and Molotov cocktail";
    /// <remarks>Change drivers: CD-06 (root; coin economy)</remarks>
    public int RequiredCoins => 100;

    /// <remarks>Change drivers: CD-05 (root; combo definitions)</remarks>
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
