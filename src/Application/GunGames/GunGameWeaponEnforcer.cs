using SampSharp.OpenMp.Core.Std.Chrono;

namespace CTF.Application.GunGames;

/// <summary>
/// Ensures that players can only use the weapon assigned to their current
/// GunGame level while the mode is active.
/// </summary>
/// <remarks>Change drivers: CD-07 (GunGame mode rules), CD-03 (combat/weapon-rules specification), CD-01 (open.mp/SampSharp platform API)</remarks>
public class GunGameWeaponEnforcer(
    IGunGameMode gunGameMode,
    ActiveWeaponProgression weaponProgression) : ISystem
{
    [Event]
    /// <remarks>Change drivers: CD-07 (GunGame mode rules), CD-03 (combat/weapon-rules specification), CD-01 (open.mp/SampSharp platform API)</remarks>
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
