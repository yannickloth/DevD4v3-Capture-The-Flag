namespace CTF.Application.WeaponCatalogs;

/// \u003csummary\u003e
/// Validates a weapon catalog type value independently of runtime configuration.
/// \u003c/summary\u003e
[ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
public static class WeaponCatalogTypeValidator
{
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    public static void EnsureValidCatalog(WeaponCatalogType type)
    {
        if (!Enum.IsDefined(type))
            throw new ArgumentOutOfRangeException(
                paramName: nameof(type),
                actualValue: type,
                message: "The weapon catalog type is invalid.");
    }
}
