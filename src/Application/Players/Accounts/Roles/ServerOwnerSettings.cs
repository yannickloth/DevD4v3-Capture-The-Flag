namespace CTF.Application.Players.Accounts.Roles;

/// <remarks>Change drivers: CD-09 (root; authorization policy); CD-17 (game configuration/.env schema) → CD-09</remarks>
public class ServerOwnerSettings
{
    /// <remarks>Change drivers: CD-09 (root; authorization policy); CD-17 (game configuration/.env schema) → CD-09</remarks>
    public string Name { get; init; } = string.Empty;

    /// <remarks>Change drivers: CD-09 (root; authorization policy); CD-17 (game configuration/.env schema) → CD-09</remarks>
    public string SecretKey { get; init; } = string.Empty;
}
