namespace CTF.Application.Teams;

/// <summary>
/// Registers team-related services with the DI container.
/// </summary>
/// <remarks>Change drivers: CD-21 (DI container/composition).</remarks>
public static class TeamServicesExtensions
{
    /// <summary>Registers the team services and flag services.</summary>
    /// <remarks>Change drivers: CD-21 (DI container/composition).</remarks>
    public static IServiceCollection AddTeamServices(this IServiceCollection services)
    {
        services
            .AddSingleton<TeamPickupService>()
            .AddSingleton<TeamIconService>()
            .AddSingleton<TeamTextDrawRenderer>()
            .AddSingleton<TeamBalancer>()
            .AddSingleton<MatchResultAnnouncer>()
            .AddSingleton<ClassSelectionTextDrawRenderer>()
            .AddFlagServices();

        return services;
    }
}
