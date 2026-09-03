namespace CTF.Application.Authorization;

/// <summary>
/// Provides player role query extension methods.
/// </summary>
/// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
public static class PlayerRoleExtensions
{
    /// <summary>Determines whether the player has the specified role.</summary>
    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public static bool HasRole(this PlayerInfo playerInfo, RoleId id)
        => playerInfo.Role.Id == id;

    /// <summary>Determines whether the player's role is lower than the specified role.</summary>
    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public static bool HasLowerRoleThan(this PlayerInfo playerInfo, RoleId id)
        => playerInfo.Role.Id < id;

    /// <summary>Determines whether the player has VIP role.</summary>
    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public static bool IsVIP(this PlayerInfo playerInfo)
        => playerInfo.HasRole(RoleId.VIP);

    /// <summary>Determines whether the player has moderator role.</summary>
    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public static bool IsModerator(this PlayerInfo playerInfo)
        => playerInfo.HasRole(RoleId.Moderator);

    /// <summary>Determines whether the player has admin role.</summary>
    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public static bool IsAdmin(this PlayerInfo playerInfo)
        => playerInfo.HasRole(RoleId.Admin);

    /// <summary>Determines whether the player does not have VIP role.</summary>
    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public static bool IsNotVIP(this PlayerInfo playerInfo)
        => !playerInfo.IsVIP();

    /// <summary>Determines whether the player does not have moderator role.</summary>
    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public static bool IsNotModerator(this PlayerInfo playerInfo)
        => !playerInfo.IsModerator();

    /// <summary>Determines whether the player does not have admin role.</summary>
    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public static bool IsNotAdmin(this PlayerInfo playerInfo)
        => !playerInfo.IsAdmin();
}
