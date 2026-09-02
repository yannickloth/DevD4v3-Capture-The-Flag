namespace CTF.Application.Players;

/// <summary>
/// Provides dependency-injection extension methods for the players subsystem.
/// </summary>
/// <remarks>Change drivers: CD-21 (root; DI container/composition)</remarks>
public static class PlayerServicesExtensions
{
    /// <summary>Registers the players subsystem services.</summary>
    /// <remarks>Change drivers: CD-21 (root; DI container/composition)</remarks>
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
