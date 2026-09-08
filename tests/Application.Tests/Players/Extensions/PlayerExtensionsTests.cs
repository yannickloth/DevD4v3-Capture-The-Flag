namespace CTF.Application.Tests.Accounts.Ecs;

/// <summary>Tests for PlayerExtensions.</summary>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.Ecs)]
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
    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.Ecs)]
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
    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.Ecs)]
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
    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.Ecs)]
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
    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.Ecs)]
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
