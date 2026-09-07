namespace CTF.Application.GameRules.Core;

/// <summary>
/// Registers game-rules services with the DI container.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules)]
public static class GameRulesServicesExtensions
{
    /// <summary>Registers the flag event handlers and supporting game-rules services.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public static IServiceCollection AddGameRulesServices(this IServiceCollection services)
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
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    private static IServiceCollection AddFlagEvent<T>(this IServiceCollection services)
        where T : class, IFlagEvent
    {
        services.AddSingleton<IFlagEvent, T>();
        return services;
    }
}
