namespace Persistence.Tests.Common;

/// <summary>A no-op password hasher that stores plaintext, used to exercise repositories without a real BCrypt cost.</summary>
/// <remarks>Change drivers: CD-25 (root; BCrypt password-hashing contract: mimics the <c>IPasswordHasher</c> seam)</remarks>
public class FakePasswordHasher : IPasswordHasher
{
    /// <summary>Returns the input unchanged.</summary>
    /// <remarks>Change drivers: CD-25 (root; BCrypt password-hashing contract: IPasswordHasher contract)</remarks>
    public string HashPassword(string text) => text;
    /// <summary>Compares plaintext equality.</summary>
    /// <remarks>Change drivers: CD-25 (root; BCrypt password-hashing contract: IPasswordHasher contract)</remarks>
    public bool Verify(string text, string passwordHash) => text == passwordHash;
}
