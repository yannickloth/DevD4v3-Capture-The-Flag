using SampSharp.OpenMp.Core.Std.Chrono;

namespace CTF.Application.GunGames.Systems.Enforcement;

/// <summary>
/// Ensures that players can only use the weapon assigned to their current
/// GunGame level while the mode is active.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): gunGameMode -> CD-07; weaponProgression -> CD-07. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Ecs)]
public class GunGameWeaponEnforcer(
    IGunGameMode gunGameMode,
    ActiveWeaponProgression weaponProgression) : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Ecs)]
    public void OnPlayerUpdate(Player player, TimePoint _)
    {
        if (!gunGameMode.IsEnabled)
            return;

        Weapon currentWeapon = player.Weapon;
        if (currentWeapon == Weapon.None  || 
            currentWeapon == Weapon.Knife || 
            currentWeapon == Weapon.Parachute)
            return;

        var playerProgression = player.GetComponent<PlayerProgression>();
        Weapon expectedWeapon = weaponProgression.GetWeapon(playerProgression.WeaponLevel).Id;

        if (currentWeapon == expectedWeapon)
            return;

        player.ResetWeapons();
        player.GiveWeapon(Weapon.Knife, 1);
        player.GiveWeapon(expectedWeapon, IWeapon.UnlimitedAmmo);
    }
}
