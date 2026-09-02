namespace CTF.Application.Players.Combos.Definitions;

/// <remarks>Change drivers: CD-05 (combo definitions); CD-06 (coin economy)</remarks>
public class FlamethrowerVitality : ICombo
{
    private const int Health = 100;
    private const int Armour = 100;
    // 1000 (shows in game as 50-50).
    private const int FlamethrowerAmmo = 1000;

    /// <remarks>Change drivers: CD-05 (combo definitions)</remarks>
    public string Name => $"{Health} Health, {Armour} Armour and FlameThrower";
    /// <remarks>Change drivers: CD-06 (coin economy)</remarks>
    public int RequiredCoins => 100;

    /// <remarks>Change drivers: CD-05 (combo definitions)</remarks>
    public Result Give(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        player.Health = Health;
        player.Armour = Armour;
        player.GiveWeapon(Weapon.FlameThrower, FlamethrowerAmmo);
        playerInfo.StatsPerRound.ResetCoins();
        return Result.Success();
    }
}
