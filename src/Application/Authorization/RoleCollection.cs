namespace CTF.Application.Authorization;

/// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
public class RoleCollection
{
    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    private RoleCollection() { }

    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    private static readonly RoleId[] s_roles = Enum.GetValues<RoleId>();

    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public static IReadOnlyList<RoleId> GetAll() => s_roles;

    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public static int Count => s_roles.Length;
}
