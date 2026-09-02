namespace CTF.Application.Players;

/// <summary>
/// Positions and configures the player at spawn using the current map's spawn locations.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification); CD-11 (map configuration) → CD-02; CD-12 (map-rotation rules) → CD-02; CD-01 (open.mp/SampSharp platform API) → CD-02</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): mapInfoService -> CD-11; mapRotationService -> CD-12. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class PlayerSpawnSystem(
    MapInfoService mapInfoService,
    MapRotationService mapRotationService) : ISystem
{
    /// <summary>Applies the spawn position and player configuration on spawn.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification); CD-11 (map configuration) → CD-02; CD-12 (map-rotation rules) → CD-02; CD-01 (open.mp/SampSharp platform API) → CD-02</remarks>
    [Event]
    public void OnPlayerSpawn(Player player)
    {
        CurrentMap currentMap = mapInfoService.CurrentMap;
        PlayerInfo playerInfo = player.GetRequiredInfo();
        SpawnLocation spawnLocation = currentMap.GetRandomSpawnLocation(playerInfo.Team.Id);
        player.Position = spawnLocation.Position;
        player.Angle = spawnLocation.Angle;
        player.Interior = currentMap.Interior;
        player.Color = playerInfo.Team.ColorHex;
        player.Team = (int)playerInfo.Team.Id;
        player.Skin = (int)playerInfo.Team.SkinId;
        if (playerInfo.HasSkin())
        {
            player.Skin = playerInfo.SkinId;
        }
        if (mapRotationService.IsMapLoading)
        {
            player.ToggleSpectating(true);
        }
    }
}
