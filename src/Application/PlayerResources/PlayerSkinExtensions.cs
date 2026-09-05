namespace CTF.Application.PlayerResources;

/// <summary>
/// Provides player skin query extension methods.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Model)]
public static class PlayerSkinExtensions
{
    /// <summary>Determines whether the player has a skin assigned.</summary>
    [ChangeDriversAttribute(ChangeDriver.Model)]
    public static bool HasSkin(this PlayerInfo playerInfo)
        => playerInfo.Appearance.SkinId != -1;
}
