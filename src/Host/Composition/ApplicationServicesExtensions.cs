namespace CTF.Host.Composition;

/// <remarks>Change drivers: CD-21 (root; DI container/composition); CD-22 (hosting/deployment spec) → CD-21</remarks>
public static class ApplicationServicesExtensions
{
    /// <remarks>Change drivers: CD-21 (root; DI container/composition); CD-22 (hosting/deployment spec) → CD-21</remarks>
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
