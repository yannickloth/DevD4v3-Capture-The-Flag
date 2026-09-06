namespace CTF.Host.Composition.EcsBuilder;

[ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Ecs)]
public static class HostEcsBuilderExtensions
{
    [ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Ecs)]
    public static IEcsBuilder RegisterPauseEventHandlers(this IEcsBuilder builder) 
    {
        var playerPauseSystem = builder.Services.GetRequiredService<PlayerPauseSystem>();
        var flagCarrierPauseSystem = builder.Services.GetRequiredService<FlagCarrierPauseSystem>();
        playerPauseSystem.PauseEvent += flagCarrierPauseSystem.OnPlayerPauseStateChange;
        return builder;
    }

    [ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Ecs)]
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

    [ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Ecs)]
    public static IEcsBuilder RegisterTeamEventHandlers(this IEcsBuilder builder)
    {
        var teamSelectionSystem = builder.Services.GetRequiredService<TeamSelectionSystem>();
        var flagTeamChangeSystem = builder.Services.GetRequiredService<FlagTeamChangeSystem>();
        teamSelectionSystem.TeamChangeEvent += flagTeamChangeSystem.OnTeamChange;
        return builder;
    }

    [ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Ecs)]
    public static IEcsBuilder RegisterMiddlewares(this IEcsBuilder builder)
    {
        builder
            .UseMiddleware<PlayerCommandLockMiddleware>(name: "OnPlayerCommandText")
            .UseMiddleware<PlayerSpawnLockMiddleware>(name: "OnPlayerRequestSpawn");

        return builder;
    }
}
