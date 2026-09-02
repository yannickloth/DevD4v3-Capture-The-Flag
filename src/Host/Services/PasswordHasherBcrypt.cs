namespace CTF.Host.Services;

/// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract), CD-21 (DI container/composition)</remarks>
public class PasswordHasherBcrypt : IPasswordHasher
{
    /// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract)</remarks>
    public string HashPassword(string text)
        => BCrypt.Net.BCrypt.HashPassword(text);

    /// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract)</remarks>
    public bool Verify(string text, string passwordHash)
        => BCrypt.Net.BCrypt.Verify(text, passwordHash);
}