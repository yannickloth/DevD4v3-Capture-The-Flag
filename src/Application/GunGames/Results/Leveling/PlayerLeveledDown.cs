namespace CTF.Application.GunGames.ClientMessageNsNs2;

/// <summary>
/// Handles the <see cref="GunGameResult.LeveledDown"/> result.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; weaponProgression -> CD-07. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
public class PlayerLeveledDown(
    IWorldService worldService,
    ActiveWeaponProgression weaponProgression) : IGunGameResultHandler
{
    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
    public GunGameResult Result => GunGameResult.LeveledDown;

    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
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
