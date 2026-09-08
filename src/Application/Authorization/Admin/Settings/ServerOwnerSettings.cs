namespace CTF.Application.Authorization.ConfigurationDomain;

[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Configuration)]
public class ServerOwnerSettings
{
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Configuration)]
    public string Name { get; init; } = string.Empty;

    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Configuration)]
    public string SecretKey { get; init; } = string.Empty;
}
