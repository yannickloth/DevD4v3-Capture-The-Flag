namespace CTF.Application.GunGames;

/// <summary>
/// Represents the active weapon progression for the current GunGame session.
/// </summary>
/// <remarks>
/// Consumers do not need to know which weapon progression is active.
/// This class always exposes the progression selected for the current session.
/// </remarks>
/// <remarks>Injected dependencies (change drivers of these elements): gunGameSession -> CD-07; progressions (FrozenDictionary&lt;WeaponProgressionType, WeaponProgression&gt;) -> CD-07. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public class ActiveWeaponProgression(
    GunGameSession gunGameSession,
    FrozenDictionary<WeaponProgressionType, WeaponProgression> progressions)
{
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    private WeaponProgression Current
        => progressions[gunGameSession.WeaponProgressionType];

    /// <inheritdoc cref="WeaponProgression.GetWeapon"/>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public IWeapon GetWeapon(WeaponLevel level)
        => Current.GetWeapon(level);

    /// <inheritdoc cref="WeaponProgression.IsFinalLevel"/>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public bool IsFinalLevel(WeaponLevel level)
        => Current.IsFinalLevel(level);
    
    /// <inheritdoc cref="WeaponProgression.MaxLevel"/>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public MaxWeaponLevel MaxLevel 
        => Current.MaxLevel;
}
