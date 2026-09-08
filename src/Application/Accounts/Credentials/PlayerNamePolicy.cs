namespace CTF.Application.Accounts;

/// <summary>
/// Validates player name and password credentials against the account policy.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account)]
public static partial class PlayerNamePolicy
{
    /// <summary>
    /// It is a validation pattern for player names.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Account)]
    private const string PlayerNamePattern = @"^[0-9a-zA-Z\[\]\(\)\$\@._=]+$";

    [ChangeDriversAttribute(ChangeDriver.Account)]
    [GeneratedRegex(PlayerNamePattern)]
    private static partial Regex PlayerNameRegex();

    /// <summary>
    /// Validates the player name against the account policy.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Account)]
    public static Result ValidateName(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure(Messages.NameCannotBeEmpty);

        if (value.Length < 3 || value.Length > 20)
            return Result.Failure(Messages.PlayerNameLength);

        if (!PlayerNameRegex().IsMatch(value))
            return Result.Failure(Messages.InvalidNickName);

        return Result.Success();
    }

    /// <summary>
    /// Validates the player password against the account policy.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Account)]
    public static Result ValidatePassword(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure(Messages.PasswordCannotBeEmpty);

        if (value.Length < 5 || value.Length > 20)
            return Result.Failure(Messages.PasswordLength);

        return Result.Success();
    }
}
