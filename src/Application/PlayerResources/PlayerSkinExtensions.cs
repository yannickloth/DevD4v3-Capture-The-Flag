namespace CTF.Application.PlayerResources;

/// <summary>
/// Provides player skin query extension methods.
/// </summary>
/// <remarks>Change drivers: CD-44 (root; skin id resources)</remarks>
public static class PlayerSkinExtensions
{
    /// <summary>Determines whether the player has a skin assigned.</summary>
    /// <remarks>Change drivers: CD-44 (root; skin id resources)</remarks>
    public static bool HasSkin(this PlayerInfo playerInfo)
        => playerInfo.Appearance.SkinId != -1;
}
