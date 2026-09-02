namespace CTF.Application.Players.Chats;

/// <summary>
/// Provides dependency-injection extension methods for the chat subsystem.
/// </summary>
/// <remarks>Change drivers: CD-21 (root; DI container/composition)</remarks>
public static class ChatServicesExtensions
{
    /// <summary>Registers the chat subsystem services.</summary>
    /// <remarks>Change drivers: CD-21 (root; DI container/composition)</remarks>
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

    private static IServiceCollection AddChatMessage<T>(this IServiceCollection services)
        where T : class, IChatMessage
    {
        services.AddSingleton<IChatMessage, T>();
        return services;
    }
}
