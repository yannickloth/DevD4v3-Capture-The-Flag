namespace CTF.Composition.HostingNsNs3;

[ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Hosting)]
public static class ApplicationServicesExtensions
{
    [ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Hosting)]
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddPlayerServices()
            .AddMapServices(GameModePaths.Maps)
            .AddTeamServices()
            .AddGameRulesServices()
            .AddGunGameServices();

        return services;
    }
}
