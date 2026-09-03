namespace CTF.Application.Platform;

/// <summary>
/// Provides player skin query extension methods.
/// </summary>
/// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API)</remarks>
public static class PlayerSkinExtensions
{
    /// <summary>Determines whether the player has a skin assigned.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API)</remarks>
    public static bool HasSkin(this PlayerInfo playerInfo)
        => playerInfo.SkinId != -1;
}
