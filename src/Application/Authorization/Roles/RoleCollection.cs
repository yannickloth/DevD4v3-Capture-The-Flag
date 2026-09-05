namespace CTF.Application.Authorization.Roles;

[ChangeDriversAttribute(ChangeDriver.Authorization)]
public class RoleCollection
{
    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    private RoleCollection() { }

    [ChangeDriversAttribute(ChangeDriver.Authorization)]
    private static readonly RoleId[] s_roles = Enum.GetValues<RoleId>();

    [ChangeDriversAttribute(ChangeDriver.Authorization)]
    public static IReadOnlyList<RoleId> GetAll() => s_roles;

    [ChangeDriversAttribute(ChangeDriver.Authorization)]
    public static int Count => s_roles.Length;
}
