namespace Persistence.Tests.Common.PasswordHasher;

/// <summary>A no-op password hasher that stores plaintext, used to exercise repositories without a real BCrypt cost.</summary>
[ChangeDriversAttribute(ChangeDriver.BCrypt)]
public class FakePasswordHasher : IPasswordHasher
{
    /// <summary>Returns the input unchanged.</summary>
    [ChangeDriversAttribute(ChangeDriver.BCrypt)]
    public string HashPassword(string text) => text;
    /// <summary>Compares plaintext equality.</summary>
    [ChangeDriversAttribute(ChangeDriver.BCrypt)]
    public bool Verify(string text, string passwordHash) => text == passwordHash;
}
