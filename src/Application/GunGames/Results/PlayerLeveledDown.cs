namespace CTF.Application.GunGames.Results;

/// <summary>
/// Handles the <see cref="GunGameResult.LeveledDown"/> result.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules); CD-03 (combat/weapon-rules specification) → CD-07; CD-01 (open.mp/SampSharp platform API) → CD-07</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-01; weaponProgression -> CD-07. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class PlayerLeveledDown(
    IWorldService worldService,
    ActiveWeaponProgression weaponProgression) : IGunGameResultHandler
{
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public GunGameResult Result => GunGameResult.LeveledDown;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules); CD-03 (combat/weapon-rules specification) → CD-07; CD-01 (open.mp/SampSharp platform API) → CD-07</remarks>
    public void Handle(KillContext context)
    {
        var victimProgression = context.Victim.GetComponent<PlayerProgression>();
        IWeapon newWeapon = weaponProgression.GetWeapon(victimProgression.WeaponLevel);
        context.Victim.ResetWeapons(); 
        context.Victim.GiveWeapon(Weapon.Knife, 1); 
        context.Victim.GiveWeapon(newWeapon.Id, IWeapon.UnlimitedAmmo);

        var message = Smart.Format(GunGameMessages.PlayerLeveledDown, new
        {
            Killer = context.Killer.Name,
            Victim = context.Victim.Name,
            Level  = victimProgression.WeaponLevel,
            Weapon = newWeapon.Name
        });

        worldService.SendClientMessage(Color.Yellow, message);
    }
}
