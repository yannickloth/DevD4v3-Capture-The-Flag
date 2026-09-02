namespace CTF.Application.GunGames.Results;

/// <summary>
/// Handles the <see cref="GunGameResult.LeveledUp"/> result.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules); CD-03 (combat/weapon-rules specification) → CD-07; CD-01 (open.mp/SampSharp platform API) → CD-07</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-01; weaponProgression -> CD-29+CD-07. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class PlayerLeveledUp(
    IWorldService worldService,
    ActiveWeaponProgression weaponProgression) : IGunGameResultHandler
{
    /// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
    public GunGameResult Result => GunGameResult.LeveledUp;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules); CD-03 (combat/weapon-rules specification) → CD-07; CD-01 (open.mp/SampSharp platform API) → CD-07</remarks>
    public void Handle(KillContext context)
    {
        var killerProgression = context.Killer.GetComponent<PlayerProgression>();
        IWeapon newWeapon = weaponProgression.GetWeapon(killerProgression.WeaponLevel);
        context.Killer.RemoveWeapon(context.Reason);
        context.Killer.GiveWeapon(newWeapon.Id, IWeapon.UnlimitedAmmo);

        var message = Smart.Format(GunGameMessages.PlayerLeveledUp, new
        {
            Killer = context.Killer.Name,
            Level  = killerProgression.WeaponLevel,
            Weapon = newWeapon.Name
        });

        worldService.SendClientMessage(Color.Yellow, message);
    }
}
