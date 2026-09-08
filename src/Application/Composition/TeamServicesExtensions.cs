namespace CTF.Composition;

/// <summary>
/// Registers team-related services with the DI container.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Composition)]
public static class TeamServicesExtensions
{
    /// <summary>Registers the team services.</summary>
    [ChangeDriversAttribute(ChangeDriver.Composition)]
    public static IServiceCollection AddTeamServices(this IServiceCollection services)
    {
        services
            .AddSingleton<TeamPickupService>()
            .AddSingleton<TeamIconService>()
            .AddSingleton<TeamTextDrawRenderer>()
            .AddSingleton<TeamBalancer>()
            .AddSingleton<MatchResultAnnouncer>()
            .AddSingleton<ClassSelectionTextDrawRenderer>();

        return services;
    }
}
