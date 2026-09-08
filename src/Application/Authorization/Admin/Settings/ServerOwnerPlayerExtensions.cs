namespace CTF.Application.Authorization.ConfigurationDomain;

/// <summary>
/// Provides server-owner authorization extension methods over the player entity.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Configuration)]
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
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Configuration)]
    public static bool IsServerOwner(this Player player)
    {
        var envReader = new EnvReader();
        var ownerName = envReader["ServerOwner__Name"];
        return player.Name.Equals(
            ownerName,
            StringComparison.OrdinalIgnoreCase);
    }
}
