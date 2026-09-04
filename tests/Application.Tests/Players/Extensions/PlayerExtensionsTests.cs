namespace CTF.Application.Tests.Players.Extensions;

/// <summary>Tests for PlayerExtensions.</summary>
/// <remarks>Change drivers: CD-08 (root; account & authentication policy: PlayerExtensions); CD-26 (NUnit test-framework contract) → CD-08; CD-27 (FluentAssertions contract) → CD-08; CD-32 (ECS runtime) → CD-08</remarks>
public class PlayerExtensionsTests
{
    [Test]
    public void GetRequiredInfo_WhenNoAccountComponent_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var fakePlayer = new FakePlayer2();

        // Act
        Action act = () => fakePlayer.GetRequiredInfo();

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void GetRequiredInfo_WhenAccountComponentIsAssigned_ShouldNotThrowInvalidOperationException()
    {
        // Arrange
        var fakePlayer = new FakePlayer3();

        // Act
        Action act = () => fakePlayer.GetRequiredInfo();

        // Assert
        act.Should().NotThrow<InvalidOperationException>();
    }

    [Test]
    public void IsUnauthenticated_WhenPlayerIsUnauthenticated_ShouldReturnTrue()
    {
        // Arrange
        var fakePlayer = new FakePlayer3()
        {
            IsAuthenticated = false
        };

        // Act
        bool actual = fakePlayer.IsUnauthenticated();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void IsUnauthenticated_WhenPlayerIsAuthenticated_ShouldReturnFalse()
    {
        // Arrange
        var fakePlayer = new FakePlayer3()
        {
            IsAuthenticated = true
        };

        // Act
        bool actual = fakePlayer.IsUnauthenticated();

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void IsUnauthenticated_WhenNoAccountComponent_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var fakePlayer = new FakePlayer2();

        // Act
        Action act = () => fakePlayer.IsUnauthenticated();

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
}
