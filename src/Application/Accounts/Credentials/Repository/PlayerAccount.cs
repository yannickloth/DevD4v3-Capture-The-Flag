namespace CTF.Application.Accounts.Credentials.Repository;

/// <summary>
/// Represents the persisted account of a player: identity and credentials.
/// </summary>
/// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy); CD-18 (database schema/player data model) → CD-08; CD-20 (outbound repository contract) → CD-08</remarks>
public partial class PlayerAccount
{
    /// <summary>
    /// It is a sentinel value that indicates the player has no account in the database.
    /// </summary>
    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy: no-account sentinel); CD-18 (database schema/player data model) → CD-08; CD-20 (outbound repository contract) → CD-08</remarks>
    private const int NoAccount = -1;

    /// <summary>
    /// It is generated automatically by the database provider.
    /// </summary>
    /// <remarks>
    /// It is a permanent identifier that is generated when the player's account is created in the database.
    /// </remarks>
    /// <remarks>Change drivers: CD-18 (database schema/player data model) ‖ CD-20 (outbound repository contract); both → CD-08 (account)</remarks>
    public int AccountId { get; private set; } = NoAccount;

    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy); CD-18 (database schema/player data model: name column); CD-20 (outbound repository contract) → CD-08</remarks>
    public string Name { get; private set; } = "DefaultUser";

    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy); CD-18 (database schema/player data model: password column); CD-20 (outbound repository contract) → CD-08</remarks>
    public string Password { get; private set; } = "DefaultPassword";

    /// <remarks>Change drivers: CD-18 (database schema/player data model) ‖ CD-20 (outbound repository contract); both → CD-08 (account)</remarks>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy: name validation); CD-18 (database schema/player data model: name column) → CD-08; CD-20 (outbound repository contract) → CD-08 — inherited against dependency direction from the Name column this method writes</remarks>
    public Result SetName(string value)
    {
        Result policyResult = PlayerNamePolicy.ValidateName(value);
        if (!policyResult.IsSuccess)
            return policyResult;

        Name = value;
        return Result.Success();
    }

    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy: password validation); CD-18 (database schema/player data model: password column) → CD-08; CD-20 (outbound repository contract) → CD-08 — inherited against dependency direction from the Password column this method writes</remarks>
    public Result SetPassword(string value)
    {
        Result policyResult = PlayerNamePolicy.ValidatePassword(value);
        if (!policyResult.IsSuccess)
            return policyResult;

        Password = value;
        return Result.Success();
    }
}
