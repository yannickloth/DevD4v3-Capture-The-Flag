namespace CTF.Application.Combos;

/// <remarks>Change drivers: CD-05 (root; combo definitions); CD-06 (coin economy) → CD-05</remarks>
/// <remarks>Injected dependencies: comboSettings -> CD-05. Driven by the ComboSettings contract + CD-21 (DI wiring).</remarks>
public class RocketLauncherVitality(ComboSettings comboSettings) : ICombo
{
    /// <remarks>Change drivers: CD-05 (root; combo definitions: reward health); CD-03 (combat/weapon-rules specification: health) → CD-05</remarks>
    private const int Health = 100;

    /// <remarks>Change drivers: CD-05 (root; combo definitions: rocket launcher ammo)</remarks>
    private const int RocketLauncherAmmo = 2;

    /// <remarks>Change drivers: CD-05 (root; combo definitions)</remarks>
    public string Name => $"{Health} Health and Rocket launcher(RPG)";
    /// <remarks>Change drivers: CD-06 (root; coin economy)</remarks>
    public int RequiredCoins => 100;

    /// <remarks>Change drivers: CD-05 (root; combo definitions)</remarks>
    public Result Give(Player player)
    {
        if (comboSettings.IsRocketLauncherDisabled)
        {
            player.SendClientMessage(Color.Red, Messages.RocketLauncherDisabled);
            return Result.Failure();
        }

        PlayerInfo playerInfo = player.GetRequiredInfo();
        player.Health = Health;
        player.GiveWeapon(Weapon.RocketLauncher, RocketLauncherAmmo);
        playerInfo.Stats.PerRound.ResetCoins();
        return Result.Success();
    }
}
