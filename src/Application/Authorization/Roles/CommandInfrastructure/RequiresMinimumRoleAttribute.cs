namespace CTF.Application.Authorization.Roles.CommandInfrastructure;

[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure)]
[AttributeUsage(AttributeTargets.Method)]
public class RequiresMinimumRoleAttribute(RoleId role) : CommandTagAttribute("role", role.ToString());
