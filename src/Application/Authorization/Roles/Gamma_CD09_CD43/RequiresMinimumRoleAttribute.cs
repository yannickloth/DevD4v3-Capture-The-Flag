namespace CTF.Application.Authorization.Roles.Gamma_CD09_CD43;

/// <remarks>Change drivers: CD-09 (root; authorization policy); CD-43 (command infrastructure) → CD-09</remarks>
[AttributeUsage(AttributeTargets.Method)]
public class RequiresMinimumRoleAttribute(RoleId role) : CommandTagAttribute("role", role.ToString());
