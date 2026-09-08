namespace CTF.Application.Authorization.CommandInfrastructureNsNs2;

[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure)]
[AttributeUsage(AttributeTargets.Method)]
public class RequiresMinimumRoleAttribute(RoleId role) : CommandTagAttribute("role", role.ToString());
