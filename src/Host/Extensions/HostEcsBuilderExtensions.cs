namespace CTF.Host.Extensions;

/// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API), CD-21 (DI container/composition)</remarks>
public static class HostEcsBuilderExtensions
{
    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API), CD-21 (DI container/composition)</remarks>
    public static IEcsBuilder RegisterPauseEventHandlers(this IEcsBuilder builder) 
    {
        var playerPauseSystem = builder.Services.GetRequiredService<PlayerPauseSystem>();
        var flagCarrierPauseSystem = builder.Services.GetRequiredService<FlagCarrierPauseSystem>();
        playerPauseSystem.PauseEvent += flagCarrierPauseSystem.OnPlayerPauseStateChange;
        return builder;
    }

    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API), CD-21 (DI container/composition)</remarks>
    public static IEcsBuilder RegisterMapEventHandlers(this IEcsBuilder builder)
    {
        var mapRotationService = builder.Services.GetRequiredService<MapRotationService>();
        var rocketLauncherSystem = builder.Services.GetRequiredService<RocketLauncherSystem>();
        var gunGameSystem = builder.Services.GetRequiredService<GunGameSystem>();
        var matchResultAnnouncer = builder.Services.GetRequiredService<MatchResultAnnouncer>();
        mapRotationService.LoadingMapEvent += rocketLauncherSystem.OnLoadingMap;
        mapRotationService.LoadingMapEvent += gunGameSystem.OnLoadingMap;
        mapRotationService.LoadingMapEvent += matchResultAnnouncer.Announce;
        return builder;
    }

    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API), CD-21 (DI container/composition)</remarks>
    public static IEcsBuilder RegisterTeamEventHandlers(this IEcsBuilder builder)
    {
        var teamSelectionSystem = builder.Services.GetRequiredService<TeamSelectionSystem>();
        var flagSystem = builder.Services.GetRequiredService<FlagSystem>();
        teamSelectionSystem.TeamChangeEvent += flagSystem.OnTeamChange;
        return builder;
    }

    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API), CD-21 (DI container/composition)</remarks>
    public static IEcsBuilder RegisterMiddlewares(this IEcsBuilder builder)
    {
        builder
            .UseMiddleware<PlayerCommandLockMiddleware>(name: "OnPlayerCommandText")
            .UseMiddleware<PlayerSpawnLockMiddleware>(name: "OnPlayerRequestSpawn");

        return builder;
    }
}
