namespace CTF.Application.WeaponCatalogs.Catalogs;

/// \u003csummary\u003e
/// Validates a weapon catalog type value independently of runtime configuration.
/// \u003c/summary\u003e
/// \u003cremarks\u003eChange drivers: CD-04 (root; weapon-catalog configuration: valid enum values)\u003c/remarks\u003e
public static class WeaponCatalogTypeValidator
{
    /// \u003cremarks\u003eChange drivers: CD-04 (root; weapon-catalog configuration: valid enum values)\u003c/remarks\u003e
    public static void EnsureValidCatalog(WeaponCatalogType type)
    {
        if (!Enum.IsDefined(type))
            throw new ArgumentOutOfRangeException(
                paramName: nameof(type),
                actualValue: type,
                message: "The weapon catalog type is invalid.");
    }
}
