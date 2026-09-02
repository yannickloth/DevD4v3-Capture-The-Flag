namespace CTF.Host;

/// <remarks>Change drivers: CD-17 (game configuration/.env schema), CD-24 (Discord webhook contract), CD-01 (open.mp/SampSharp platform API), CD-21 (DI container/composition), CD-23 (Serilog logging)</remarks>
public class Startup : IEcsStartup
{
    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API)</remarks>
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

    /// <remarks>Change drivers: CD-17 (game configuration/.env schema), CD-24 (Discord webhook contract), CD-01 (open.mp/SampSharp platform API), CD-21 (DI container/composition), CD-23 (Serilog logging)</remarks>
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

    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API), CD-21 (DI container/composition)</remarks>
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
