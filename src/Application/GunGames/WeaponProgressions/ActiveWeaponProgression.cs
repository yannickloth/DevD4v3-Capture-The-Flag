namespace CTF.Application.GunGames.WeaponProgressions;

/// <summary>
/// Represents the active weapon progression for the current GunGame session.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
/// <remarks>
/// Consumers do not need to know which weapon progression is active.
/// This class always exposes the progression selected for the current session.
/// </remarks>
/// <remarks>Injected dependencies (change drivers of these elements): gunGameSession -> CD-29+CD-07; progressions (FrozenDictionary&lt;WeaponProgressionType, WeaponProgression&gt;) -> CD-07. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class ActiveWeaponProgression(
    GunGameSession gunGameSession,
    FrozenDictionary<WeaponProgressionType, WeaponProgression> progressions)
{
    /// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
    private WeaponProgression Current
        => progressions[gunGameSession.WeaponProgressionType];

    /// <inheritdoc cref="WeaponProgression.GetWeapon"/>
    /// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
    public IWeapon GetWeapon(WeaponLevel level)
        => Current.GetWeapon(level);

    /// <inheritdoc cref="WeaponProgression.IsFinalLevel"/>
    /// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
    public bool IsFinalLevel(WeaponLevel level)
        => Current.IsFinalLevel(level);
    
    /// <inheritdoc cref="WeaponProgression.MaxLevel"/>
    /// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
    public MaxWeaponLevel MaxLevel 
        => Current.MaxLevel;
}
