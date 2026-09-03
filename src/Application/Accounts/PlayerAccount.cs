namespace CTF.Application.Accounts;

/// <summary>
/// Represents the persisted account of a player: identity and credentials.
/// </summary>
/// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy)</remarks>
public partial class PlayerAccount
{
    /// <summary>
    /// It is a sentinel value that indicates the player has no account in the database.
    /// </summary>
    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy: no-account sentinel)</remarks>
    private const int NoAccount = -1;

    /// <summary>
    /// It is a validation pattern for player names.
    /// </summary>
    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy: player name validation pattern)</remarks>
    private const string PlayerNamePattern = @"^[0-9a-zA-Z\[\]\(\)\$\@._=]+$";

    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy: player name validation regex)</remarks>
    [GeneratedRegex(PlayerNamePattern)]
    private static partial Regex PlayerNameRegex();

    /// <summary>
    /// It is generated automatically by the database provider.
    /// </summary>
    /// <remarks>
    /// It is a permanent identifier that is generated when the player's account is created in the database.
    /// </remarks>
    /// <remarks>Change drivers: CD-18 (database schema/player data model) ‖ CD-20 (outbound repository contract); both → CD-08 (account)</remarks>
    public int AccountId { get; private set; } = NoAccount;

    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy); CD-20 (outbound repository contract) → CD-08</remarks>
    public string Name { get; private set; } = "DefaultUser";

    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy); CD-20 (outbound repository contract) → CD-08</remarks>
    public string Password { get; private set; } = "DefaultPassword";

    /// <remarks>Change drivers: CD-18 (database schema/player data model) ‖ CD-20 (outbound repository contract); both → CD-08 (account)</remarks>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy)</remarks>
    public Result SetName(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure(Messages.NameCannotBeEmpty);

        if (value.Length < 3 || value.Length > 20)
            return Result.Failure(Messages.PlayerNameLength);

        if (!PlayerNameRegex().IsMatch(value))
            return Result.Failure(Messages.InvalidNickName);

        Name = value;
        return Result.Success();
    }

    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy)</remarks>
    public Result SetPassword(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure(Messages.PasswordCannotBeEmpty);

        if (value.Length < 5 || value.Length > 20)
            return Result.Failure(Messages.PasswordLength);

        Password = value;
        return Result.Success();
    }
}
