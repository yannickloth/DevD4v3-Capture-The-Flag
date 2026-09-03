namespace CTF.Application.Tests.Players.Accounts;

/// <summary>Tests for PlayerInfo.SetPassword.</summary>
/// <remarks>Change drivers: CD-08 (root; account & authentication policy: PlayerInfo.SetPassword); CD-26 (NUnit test-framework contract) → CD-08; CD-27 (FluentAssertions contract) → CD-08</remarks>
public class PasswordTests
{
    [Test]
    public void SetPassword_WhenArgumentIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var player = new PlayerInfo();
        string password = default;

        // Act
        Action act = () => player.Account.SetPassword(password);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("   ")]
    public void SetPassword_WhenPasswordIsEmpty_ShouldReturnFailureResult(string password)
    {
        // Arrange
        var player = new PlayerInfo();
        var expectedMessage = Messages.PasswordCannotBeEmpty;

        // Act
        Result result = player.Account.SetPassword(password);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
        player.Account.Password.Should().NotBe(password);
    }

    [TestCase("aaaa")]
    [TestCase("aaaaaaaaaaaaaaaaaaaaa")]
    public void SetPassword_WhenPasswordLengthIsInvalid_ShouldReturnFailureResult(string password)
    {
        // Arrange
        var player = new PlayerInfo();
        var expectedMessage = Messages.PasswordLength;

        // Act
        Result result = player.Account.SetPassword(password);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
        player.Account.Password.Should().NotBe(password);
    }

    [TestCase("12345")]
    [TestCase("bbbbbbbbbbbbbbbbbbbb")]
    public void SetPassword_WhenPasswordIsValid_ShouldReturnSuccessResult(string password)
    {
        // Arrange
        var player = new PlayerInfo();

        // Act
        Result result = player.Account.SetPassword(password);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        player.Account.Password.Should().Be(password);
    }
}
