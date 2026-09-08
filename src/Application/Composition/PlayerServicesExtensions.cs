namespace CTF.Composition;

/// <summary>
/// Provides dependency-injection extension methods for the players subsystem.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Composition)]
public static class PlayerServicesExtensions
{
    /// <summary>Registers the players subsystem services.</summary>
    [ChangeDriversAttribute(ChangeDriver.Composition)]
    public static IServiceCollection AddPlayerServices(this IServiceCollection services)
    {
        services
            .AddSingleton<PlayerRankUpdater>()
            .AddSingleton<PlayerKillingSpreeUpdater>()
            .AddSingleton<PlayerStatsRenderer>()
            .AddSingleton<AuthenticationDialog>()
            .AddSingleton<AccountAuthenticator>()
            .AddComboServices()
            .AddChatServices()
            .AddWeaponServices();

        return services;
    }
}
