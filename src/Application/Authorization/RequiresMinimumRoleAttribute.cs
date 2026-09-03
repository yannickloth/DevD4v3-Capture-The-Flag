namespace CTF.Application.Authorization;

/// <remarks>Change drivers: CD-09 (root; authorization policy); CD-01 (open.mp/SampSharp platform API) → CD-09</remarks>
[AttributeUsage(AttributeTargets.Method)]
public class RequiresMinimumRoleAttribute(RoleId role) : CommandTagAttribute("role", role.ToString());
