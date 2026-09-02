namespace CTF.Application.Teams.Flags;

/// <summary>
/// Registers flag services with the DI container.
/// </summary>
/// <remarks>Change drivers: CD-21 (root; DI container/composition)</remarks>
public static class FlagServicesExtensions
{
    /// <summary>Registers the flag event handlers and supporting services.</summary>
    /// <remarks>Change drivers: CD-21 (root; DI container/composition)</remarks>
    public static IServiceCollection AddFlagServices(this IServiceCollection services)
    {
        services
            .AddFlagEvent<OnFlagAtBasePosition>()
            .AddFlagEvent<OnFlagCaptured>()
            .AddFlagEvent<OnFlagReturned>()
            .AddFlagEvent<OnFlagDropped>()
            .AddFlagEvent<OnFlagScore>()
            .AddFlagEvent<OnFlagTaken>()
            .AddSingleton(sp =>
            {
                var flagEvents = sp.GetRequiredService<IEnumerable<IFlagEvent>>();
                return flagEvents.ToFrozenDictionary(f => f.FlagStatus);
            });

        services
            .AddSingleton<FlagAutoReturnTimer>()
            .AddSingleton<FlagStateResetter>();

        return services;
    }

    /// <summary>Registers a flag event implementation as a singleton.</summary>
    /// <remarks>Change drivers: CD-21 (root; DI container/composition)</remarks>
    private static IServiceCollection AddFlagEvent<T>(this IServiceCollection services)
        where T : class, IFlagEvent
    {
        services.AddSingleton<IFlagEvent, T>();
        return services;
    }
}
