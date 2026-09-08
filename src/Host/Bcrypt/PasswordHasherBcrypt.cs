namespace CTF.BCrypt;

[ChangeDriversAttribute(ChangeDriver.BCrypt)]
public class PasswordHasherBcrypt : IPasswordHasher
{
    [ChangeDriversAttribute(ChangeDriver.BCrypt)]
    public string HashPassword(string text)
        => global::BCrypt.Net.BCrypt.HashPassword(text);

    [ChangeDriversAttribute(ChangeDriver.BCrypt)]
    public bool Verify(string text, string passwordHash)
        => global::BCrypt.Net.BCrypt.Verify(text, passwordHash);
}