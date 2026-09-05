namespace CTF.Application.Accounts.Credentials.Repository;

/// <summary>
/// Represents the persisted account of a player: identity and credentials.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.DatabaseSchema, ChangeDriver.Repository)]
public partial class PlayerAccount
{
    /// <summary>
    /// It is a sentinel value that indicates the player has no account in the database.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.DatabaseSchema, ChangeDriver.Repository)]
    private const int NoAccount = -1;

    /// <summary>
    /// It is generated automatically by the database provider.
    /// </summary>
    /// <remarks>
    /// It is a permanent identifier that is generated when the player's account is created in the database.
    /// </remarks>
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.Repository, ChangeDriver.Account)]
    public int AccountId { get; private set; } = NoAccount;

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.DatabaseSchema, ChangeDriver.Repository)]
    public string Name { get; private set; } = "DefaultUser";

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.DatabaseSchema, ChangeDriver.Repository)]
    public string Password { get; private set; } = "DefaultPassword";

    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.Repository, ChangeDriver.Account)]
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.DatabaseSchema, ChangeDriver.Repository)]
    public Result SetName(string value)
    {
        Result policyResult = PlayerNamePolicy.ValidateName(value);
        if (!policyResult.IsSuccess)
            return policyResult;

        Name = value;
        return Result.Success();
    }

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.DatabaseSchema, ChangeDriver.Repository)]
    public Result SetPassword(string value)
    {
        Result policyResult = PlayerNamePolicy.ValidatePassword(value);
        if (!policyResult.IsSuccess)
            return policyResult;

        Password = value;
        return Result.Success();
    }
}
