namespace CTF.Application.Players.Weapons;

/// <remarks>Change drivers: CD-04 (weapon-catalog configuration), CD-21 (DI container/composition)</remarks>
public static class WeaponServicesExtensions
{
    /// <remarks>Change drivers: CD-04 (weapon-catalog configuration), CD-21 (DI container/composition)</remarks>
    public static IServiceCollection AddWeaponServices(this IServiceCollection services)
    {
        services
            .AddWeaponCatalog<RunWeaponCatalog>()
            .AddWeaponCatalog<WalkingWeaponCatalog>()
            .AddWeaponCatalog<MixedWeaponCatalog>()
            .AddWeaponCatalog<RifleOnlyWeaponCatalog>()
            .AddWeaponCatalog<WarWeaponCatalog>()
            .AddWeaponCatalog<HeavyWeaponCatalog>()
            .AddWeaponCatalog<MeleeWeaponCatalog>()
            .AddSingleton<ActiveWeaponCatalog>()
            .AddSingleton(sp =>
            {
                var catalogs = sp.GetRequiredService<IEnumerable<WeaponCatalog>>();
                return catalogs.ToFrozenDictionary(w => w.Type);
            });

        return services;
    }

    /// <remarks>Change drivers: CD-04 (weapon-catalog configuration), CD-21 (DI container/composition)</remarks>
    private static IServiceCollection AddWeaponCatalog<T>(this IServiceCollection services)
        where T : WeaponCatalog
    {
        services.AddSingleton<WeaponCatalog, T>();
        return services;
    }
}
