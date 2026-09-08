namespace CTF.Application.GameRules.PlayerNsNs5;

/// <summary>
/// Positions and configures the player at spawn using the current map's spawn locations.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): mapInfoService -> CD-11; mapRotationService -> CD-12. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Map, ChangeDriver.MapRotation, ChangeDriver.Player)]
public class PlayerSpawnSystem(
    MapInfoService mapInfoService,
    MapRotationService mapRotationService) : ISystem
{
    /// <summary>Applies the spawn position and player configuration on spawn.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Map, ChangeDriver.MapRotation, ChangeDriver.Player)]
    [Event]
    public void OnPlayerSpawn(Player player)
    {
        CurrentMap currentMap = mapInfoService.CurrentMap;
        PlayerInfo playerInfo = player.GetRequiredInfo();
        SpawnLocation spawnLocation = currentMap.GetRandomSpawnLocation(playerInfo.Appearance.Team.Id);
        player.Position = spawnLocation.Position;
        player.Angle = spawnLocation.Angle;
        player.Interior = currentMap.Interior;
        player.Color = playerInfo.Appearance.Team.ColorHex;
        player.Team = (int)playerInfo.Appearance.Team.Id;
        player.Skin = (int)playerInfo.Appearance.Team.SkinId;
        if (playerInfo.HasSkin())
        {
            player.Skin = playerInfo.Appearance.SkinId;
        }
        if (mapRotationService.IsMapLoading)
        {
            player.ToggleSpectating(true);
        }
    }
}
