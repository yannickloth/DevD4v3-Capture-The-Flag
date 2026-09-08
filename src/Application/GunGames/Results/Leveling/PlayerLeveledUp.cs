namespace CTF.Application.GunGames.ClientMessageNsNs2;

/// <summary>
/// Handles the <see cref="GunGameResult.LeveledUp"/> result.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; weaponProgression -> CD-07. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
public class PlayerLeveledUp(
    IWorldService worldService,
    ActiveWeaponProgression weaponProgression) : IGunGameResultHandler
{
    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
    public GunGameResult Result => GunGameResult.LeveledUp;

    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
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
