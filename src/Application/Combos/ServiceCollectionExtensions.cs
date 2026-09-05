using CTF.Application.Combos.Systems;

namespace CTF.Application.Combos;

[ChangeDriversAttribute(ChangeDriver.Combo)]
public static class ComboServicesExtensions
{
    [ChangeDriversAttribute(ChangeDriver.Combo)]
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

    [ChangeDriversAttribute(ChangeDriver.Combo)]
    private static IServiceCollection AddCombo<T>(this IServiceCollection services)
        where T : class, ICombo
    {
        services.AddSingleton<ICombo, T>();
        return services;
    }
}
