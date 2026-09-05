namespace CTF.Application.Composition;

/// <summary>
/// Provides dependency-injection extension methods for the chat subsystem.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Chat)]
public static class ChatServicesExtensions
{
    /// <summary>Registers the chat subsystem services.</summary>
    [ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Chat)]
    public static IServiceCollection AddChatServices(this IServiceCollection services)
    {
        services
            .AddChatMessage<PrivateAdminChat>()
            .AddChatMessage<PrivateModeratorChat>()
            .AddChatMessage<PrivateTeamChat>()
            .AddChatMessage<PrivateVipChat>()
            .AddSingleton(sp =>
            {
                var chats = sp.GetRequiredService<IEnumerable<IChatMessage>>();
                return chats.ToFrozenDictionary(c => c.Id);
            });

        return services;
    }

    [ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Chat)]
    private static IServiceCollection AddChatMessage<T>(this IServiceCollection services)
        where T : class, IChatMessage
    {
        services.AddSingleton<IChatMessage, T>();
        return services;
    }
}
