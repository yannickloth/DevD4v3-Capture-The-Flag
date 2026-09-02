namespace CTF.Application.Players.Accounts.Roles;

/// <remarks>Change drivers: CD-09 (authorization policy), CD-17 (game configuration/.env schema)</remarks>
public class ServerOwnerSettings
{
    /// <remarks>Change drivers: CD-09 (authorization policy), CD-17 (game configuration/.env schema)</remarks>
    public string Name { get; init; } = string.Empty;

    /// <remarks>Change drivers: CD-09 (authorization policy), CD-17 (game configuration/.env schema)</remarks>
    public string SecretKey { get; init; } = string.Empty;
}
