namespace CTF.Host.Extensions;

/// <remarks>Change drivers: CD-22 (hosting/deployment spec), CD-21 (DI container/composition)</remarks>
public static class ApplicationServicesExtensions
{
    /// <remarks>Change drivers: CD-22 (hosting/deployment spec), CD-21 (DI container/composition)</remarks>
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
