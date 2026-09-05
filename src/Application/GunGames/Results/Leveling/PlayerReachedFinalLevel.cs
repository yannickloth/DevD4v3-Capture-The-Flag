namespace CTF.Application.GunGames.Results.Leveling;

/// <summary>
/// Handles the <see cref="GunGameResult.ReachedFinalLevel"/> result.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; weaponProgression -> CD-07. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
public class PlayerReachedFinalLevel(
    IWorldService worldService,
    ActiveWeaponProgression weaponProgression) : IGunGameResultHandler
{
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public GunGameResult Result => GunGameResult.ReachedFinalLevel;
    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
    public void Handle(KillContext context)
    {
        var killerProgression = context.Killer.GetComponent<PlayerProgression>();
        IWeapon newWeapon = weaponProgression.GetWeapon(killerProgression.WeaponLevel);
        context.Killer.RemoveWeapon(context.Reason);
        context.Killer.GiveWeapon(newWeapon.Id, IWeapon.UnlimitedAmmo);

        var message = Smart.Format(GunGameMessages.PlayerReachedFinalLevel, new
        {
            Killer = context.Killer.Name,
            Weapon = newWeapon.Name
        });

        worldService.SendClientMessage(Color.Yellow, message);
    }
}
