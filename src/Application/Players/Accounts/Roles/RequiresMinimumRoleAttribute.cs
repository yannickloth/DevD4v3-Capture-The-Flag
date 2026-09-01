namespace CTF.Application.Players.Accounts.Roles;

/// <remarks>Change drivers: CD-09 (authorization policy), CD-01 (open.mp/SampSharp platform API)</remarks>
[AttributeUsage(AttributeTargets.Method)]
public class RequiresMinimumRoleAttribute(RoleId role) : CommandTagAttribute("role", role.ToString());
