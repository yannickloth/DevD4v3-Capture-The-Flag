namespace CTF.Application.Combos;

/// <remarks>Change drivers: CD-05 (root; combo definitions); CD-06 (coin economy) → CD-05</remarks>
public class MolotovVitality : ICombo
{
    private const int Health = 100;
    private const int Armour = 100;
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
        playerInfo.StatsPerRound.ResetCoins();
        return Result.Success();
    }
}
