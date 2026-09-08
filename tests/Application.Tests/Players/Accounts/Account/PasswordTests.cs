namespace CTF.Application.Tests.Accounts;

/// <summary>Tests for PlayerInfo.SetPassword.</summary>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
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
    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
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
    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
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
    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
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
