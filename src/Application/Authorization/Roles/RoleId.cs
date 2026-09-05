namespace CTF.Application.Authorization.Roles;

[ChangeDriversAttribute(ChangeDriver.Authorization)]
public enum RoleId
{
    [ChangeDriversAttribute(ChangeDriver.Authorization)]
    Basic,

    [ChangeDriversAttribute(ChangeDriver.Authorization)]
    VIP,

    [ChangeDriversAttribute(ChangeDriver.Authorization)]
    Moderator,

    [ChangeDriversAttribute(ChangeDriver.Authorization)]
    Admin
}
