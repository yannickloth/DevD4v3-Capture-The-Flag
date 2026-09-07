namespace CTF.Application.GunGames.Composition;

[ChangeDriversAttribute(ChangeDriver.GunGame)]
public static class GunGameExtensions
{
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public static IServiceCollection AddGunGameServices(this IServiceCollection services)
    {
        services
            .AddSingleton<GunGameRewardGrant>()
            .AddSingleton<GunGameSession>()
            .AddSingleton<ActiveWeaponProgression>()
            .AddSingleton<IGunGameMode>(sp => sp.GetRequiredService<GunGameSystem>());

        services
            .AddWeaponProgression<ClassicWeaponProgression>()
            .AddWeaponProgression<HardcoreWeaponProgression>()
            .AddWeaponProgression<PistolsWeaponProgression>()
            .AddWeaponProgression<ReverseClassicWeaponProgression>()
            .AddWeaponProgression<RiflesWeaponProgression>()
            .AddWeaponProgression<ShotgunsWeaponProgression>()
            .AddWeaponProgression<SmgsWeaponProgression>()
            .AddWeaponProgression<PowerfulWeaponProgression>()
            .AddSingleton(sp =>
            {
                var progressions = sp.GetRequiredService<IEnumerable<WeaponProgression>>();
                return progressions.ToFrozenDictionary(w => w.Type);
            });

        services
            .AddGunGameResultHandler<PlayerLeveledDown>()
            .AddGunGameResultHandler<PlayerLeveledUp>()
            .AddGunGameResultHandler<PlayerReachedFinalLevel>()
            .AddGunGameResultHandler<PlayerScoredFinalKill>()
            .AddSingleton(sp =>
            {
                var handlers = sp.GetRequiredService<IEnumerable<IGunGameResultHandler>>();
                return handlers.ToFrozenDictionary(h => h.Result);
            });

        return services;
    }

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    private static IServiceCollection AddWeaponProgression<T>(this IServiceCollection services)
        where T : WeaponProgression
    {
        services.AddSingleton<WeaponProgression, T>();
        return services;
    }

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    private static IServiceCollection AddGunGameResultHandler<T>(this IServiceCollection services)
        where T : class, IGunGameResultHandler
    {
        services.AddSingleton<IGunGameResultHandler, T>();
        return services;
    }
}
