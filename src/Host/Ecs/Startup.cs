namespace CTF.Host.Ecs;

/// <remarks>Change drivers: CD-32 (root; ECS runtime: IEcsStartup lifecycle); CD-17 (game configuration/.env schema) → CD-32; CD-21 (DI container/composition) → CD-32; CD-23 (Serilog logging) → CD-32; CD-24 (Discord webhook contract) → CD-32</remarks>
public class Startup : IEcsStartup
{
    /// <remarks>Change drivers: CD-32 (root; ECS runtime)</remarks>
    public void Initialize(IStartupContext context)
    {
        context.UseEntities()
            .UseConsoleCommands()
            .UsePlayerCommands(opts => 
            {
                opts.UsageMessageColor = Color.Red;
                opts.PermissionDeniedMessage = default;
            });
    }

    /// <remarks>Change drivers: CD-21 (root; DI container/composition: adapter/singleton registrations); CD-32 (IEcsStartup contract) → CD-21; CD-17 (game configuration/.env schema) → CD-21; CD-23 (Serilog logging) → CD-21; CD-24 (Discord webhook contract) → CD-21</remarks>
    public void ConfigureServices(IServiceCollection services, IConfiguration _)
    {
        new EnvLoader()
            #if DEBUG
            .EnableFileNotFoundException()
            #endif
            .AddEnvFile(".env")
            .Load();

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        services.ChooseDatabaseProvider(configuration);
        services
            .AddSerilog()
            .AddApplicationServices()
            .AddSettings(configuration)
            .AddSingleton<IPasswordHasher, PasswordHasherBcrypt>()
            .AddSingleton(configuration)
            .AddSingleton(TimeProvider.System)
            .AddSingleton<UnixTimeSeconds>()
            .AddStreamer()
            .AddMapObjects();

        services.RemoveAll<ICommandTextFormatter>();
        services.RemoveAll<IPermissionChecker>();
        services.AddSingleton<ICommandTextFormatter, CommandUsageFormatter>();
        services.AddSingleton<IPermissionChecker, PlayerRoleChecker>();

        services.AddHttpClient<IDiscordWebhookClient, DiscordWebhookClient>(httpClient =>
        {
            httpClient.Timeout = TimeSpan.FromSeconds(5);
        });

        // Add systems to the services collection
        services
            .AddSystemsInAssembly(typeof(CurrentMap).Assembly)
            .AddSystemsInAssembly(typeof(Startup).Assembly);
    }

    /// <remarks>Change drivers: CD-32 (root; ECS runtime)</remarks>
    public void Configure(IEcsBuilder builder)
    {
        // TODO: Enable desired ECS system features
        builder
            .RegisterMiddlewares()
            .RegisterPauseEventHandlers()
            .RegisterMapEventHandlers()
            .RegisterTeamEventHandlers()
            .EnableStreamerEvents();
    }
}
