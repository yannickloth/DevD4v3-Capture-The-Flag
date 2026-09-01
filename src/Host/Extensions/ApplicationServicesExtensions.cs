namespace CTF.Host.Extensions;

/// <remarks>Change drivers: CD-21 (DI container/composition), CD-22 (hosting/deployment spec)</remarks>
public static class ApplicationServicesExtensions
{
    /// <remarks>Change drivers: CD-21 (DI container/composition), CD-22 (hosting/deployment spec)</remarks>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddPlayerServices()
            .AddMapServices(GameModePaths.Maps)
            .AddTeamServices()
            .AddGunGameServices();

        return services;
    }
}
