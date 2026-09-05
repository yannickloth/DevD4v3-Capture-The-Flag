namespace CTF.Host.Bcrypt;

[ChangeDriversAttribute(ChangeDriver.BCrypt)]
public class PasswordHasherBcrypt : IPasswordHasher
{
    [ChangeDriversAttribute(ChangeDriver.BCrypt)]
    public string HashPassword(string text)
        => BCrypt.Net.BCrypt.HashPassword(text);

    [ChangeDriversAttribute(ChangeDriver.BCrypt)]
    public bool Verify(string text, string passwordHash)
        => BCrypt.Net.BCrypt.Verify(text, passwordHash);
}