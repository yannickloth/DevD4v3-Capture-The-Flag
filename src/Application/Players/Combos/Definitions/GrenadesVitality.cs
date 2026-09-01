namespace CTF.Application.Players.Combos.Definitions;

/// <remarks>Change drivers: CD-05 (combo definitions), CD-06 (coin economy)</remarks>
public class GrenadesVitality : ICombo
{
    private const int Health = 100;
    private const int Armour = 100;
    private const int GrenadeAmmo = 6;

    /// <remarks>Change drivers: CD-05 (combo definitions)</remarks>
    public string Name => $"{Health} Health, {Armour} Armour and Grenades";
    /// <remarks>Change drivers: CD-06 (coin economy)</remarks>
    public int RequiredCoins => 100;

    /// <remarks>Change drivers: CD-05 (combo definitions)</remarks>
    public Result Give(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        player.Health = Health;
        player.Armour = Armour;
        player.GiveWeapon(Weapon.Grenade, GrenadeAmmo);
        playerInfo.StatsPerRound.ResetCoins();
        return Result.Success();
    }
}
