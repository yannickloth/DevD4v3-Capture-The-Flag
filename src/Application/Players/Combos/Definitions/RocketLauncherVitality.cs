namespace CTF.Application.Players.Combos.Definitions;

/// <remarks>Change drivers: CD-05 (root; combo definitions); CD-06 (coin economy) → CD-05; CD-17 (game configuration/.env schema) → CD-05</remarks>
/// <remarks>Injected dependencies: comboSettings -> CD-17. Driven by the ComboSettings (config) contract + CD-21 (DI wiring).</remarks>
public class RocketLauncherVitality(ComboSettings comboSettings) : ICombo
{
    private const int Health = 100;
    private const int RocketLauncherAmmo = 2;

    /// <remarks>Change drivers: CD-05 (root; root; combo definitions)</remarks>
    public string Name => $"{Health} Health and Rocket launcher(RPG)";
    /// <remarks>Change drivers: CD-06 (root; root; coin economy)</remarks>
    public int RequiredCoins => 100;

    /// <remarks>Change drivers: CD-05 (root; combo definitions); CD-17 (game configuration/.env schema) → CD-05</remarks>
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
        playerInfo.StatsPerRound.ResetCoins();
        return Result.Success();
    }
}
