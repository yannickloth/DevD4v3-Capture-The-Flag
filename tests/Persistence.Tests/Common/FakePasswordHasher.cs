namespace Persistence.Tests.Common;

/// <summary>A no-op password hasher that stores plaintext, used to exercise repositories without a real BCrypt cost.</summary>
/// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract it must satisfy), CD-29 (code-under-test: mimics the <c>IPasswordHasher</c> seam)</remarks>
public class FakePasswordHasher : IPasswordHasher
{
    /// <summary>Returns the input unchanged.</summary>
    /// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract), CD-29 (code-under-test: IPasswordHasher contract)</remarks>
    public string HashPassword(string text) => text;
    /// <summary>Compares plaintext equality.</summary>
    /// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract), CD-29 (code-under-test: IPasswordHasher contract)</remarks>
    public bool Verify(string text, string passwordHash) => text == passwordHash;
}
