namespace CTF.Application.Players.Combos;

/// <remarks>Change drivers: CD-05 (combo definitions), CD-21 (DI container/composition)</remarks>
public static class ComboServicesExtensions
{
    /// <remarks>Change drivers: CD-05 (combo definitions), CD-21 (DI container/composition)</remarks>
    public static IServiceCollection AddComboServices(this IServiceCollection services)
    {
        services
            .AddSingleton<ComboSettings>()
            .AddCombo<FlamethrowerVitality>()
            .AddCombo<GrenadesVitality>()
            .AddCombo<MolotovVitality>()
            .AddCombo<RocketLauncherVitality>()
            .AddCombo<SatchelChargesVitality>()
            .AddCombo<TearGasVitality>();

        return services;
    }

    /// <remarks>Change drivers: CD-05 (combo definitions), CD-21 (DI container/composition)</remarks>
    private static IServiceCollection AddCombo<T>(this IServiceCollection services)
        where T : class, ICombo
    {
        services.AddSingleton<ICombo, T>();
        return services;
    }
}
