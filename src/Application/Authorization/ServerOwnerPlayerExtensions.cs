namespace CTF.Application.Authorization;

/// <summary>
/// Provides server-owner authorization extension methods over the player entity.
/// </summary>
/// <remarks>Change drivers: CD-09 (root; authorization policy); CD-17 (game configuration/.env schema: server-owner name) → CD-09</remarks>
public static class ServerOwnerPlayerExtensions
{
    /// <summary>
    /// Determines whether the specified player is the server owner.
    /// </summary>
    /// <param name="player">
    /// The player to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the player is the server owner;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>Change drivers: CD-09 (root; authorization policy); CD-17 (game configuration/.env schema: server-owner name) → CD-09</remarks>
    public static bool IsServerOwner(this Player player)
    {
        var envReader = new EnvReader();
        var ownerName = envReader["ServerOwner__Name"];
        return player.Name.Equals(
            ownerName,
            StringComparison.OrdinalIgnoreCase);
    }
}
