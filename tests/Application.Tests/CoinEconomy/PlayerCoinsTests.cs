namespace CTF.Application.Tests.CoinEconomy;

/// <summary>Tests for PlayerCoins.</summary>
[ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
public class PlayerCoinsTests
{
    [TestCase(10)]
    [TestCase(9)]
    [TestCase(8)]
    public void HasSufficientCoins_WhenPlayerHasSufficientCoins_ShouldReturnTrue(int amount)
    {
        // Arrange
        var coins = new PlayerCoins();
        coins.AddCoins(10);

        // Act
        bool actual = coins.HasSufficientCoins(amount);

        // Assert
        actual.Should().BeTrue();
    }

    [TestCase(11)]
    [TestCase(12)]
    public void HasSufficientCoins_WhenPlayerHasInsufficientCoins_ShouldReturnFalse(int amount)
    {
        // Arrange
        var coins = new PlayerCoins();
        coins.AddCoins(10);

        // Act
        bool actual = coins.HasSufficientCoins(amount);

        // Assert
        actual.Should().BeFalse();
    }

    [TestCase(10)]
    [TestCase(9)]
    [TestCase(8)]
    public void HasInsufficientCoins_WhenPlayerHasSufficientCoins_ShouldReturnFalse(int amount)
    {
        // Arrange
        var coins = new PlayerCoins();
        coins.AddCoins(10);

        // Act
        bool actual = coins.HasInsufficientCoins(amount);

        // Assert
        actual.Should().BeFalse();
    }

    [TestCase(11)]
    [TestCase(12)]
    public void HasInsufficientCoins_WhenPlayerHasInsufficientCoins_ShouldReturnTrue(int amount)
    {
        // Arrange
        var coins = new PlayerCoins();
        coins.AddCoins(10);

        // Act
        bool actual = coins.HasInsufficientCoins(amount);

        // Assert
        actual.Should().BeTrue();
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(101)]
    public void AddCoins_WhenCoinsAreNotBetween1To100_ShouldReturnFailureResult(int value)
    {
        // Arrange
        var coins = new PlayerCoins();
        var expectedMessage = Messages.InvalidAddCoins;

        // Act
        Result result = coins.AddCoins(value);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(99)]
    [TestCase(100)]
    public void AddCoins_WhenCoinsAreBetween1To100_ShouldReturnSuccessResult(int value)
    {
        // Arrange
        var coins = new PlayerCoins();

        // Act
        Result result = coins.AddCoins(value);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        coins.Balance.Should().Be(value);
    }

    [TestCase(21)]
    [TestCase(22)]
    [TestCase(23)]
    [TestCase(100)]
    public void AddCoins_WhenSumOfCoinsExceedsValueOf100_ShouldSetValueTo100(int value)
    {
        // Arrange
        var coins = new PlayerCoins();
        int expectedBalance = 100;
        coins.AddCoins(80);

        // Act
        Result result = coins.AddCoins(value);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        coins.Balance.Should().Be(expectedBalance);
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(-101)]
    public void SubtractCoins_WhenCoinsAreNotInSpecifiedRange_ShouldReturnFailureResult(int value)
    {
        // Arrange
        var coins = new PlayerCoins();
        // Should be in the range of -1 to -100.
        var expectedMessage = Messages.InvalidSubtractCoins;

        // Act
        Result result = coins.SubtractCoins(value);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [TestCase(-1)]
    [TestCase(-2)]
    [TestCase(-99)]
    [TestCase(-100)]
    public void SubtractCoins_WhenCoinsAreInSpecifiedRange_ShouldReturnSuccessResult(int value)
    {
        // Arrange
        var coins = new PlayerCoins();
        int expectedBalance = 0;

        // Act
        Result result = coins.SubtractCoins(value);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        coins.Balance.Should().Be(expectedBalance);
    }

    [TestCase(-11)]
    [TestCase(-12)]
    [TestCase(-13)]
    [TestCase(-100)]
    public void SubtractCoins_WhenSubtractionOfCoinsGivesNegativeResult_ShouldSetValueToZero(int value)
    {
        // Arrange
        var coins = new PlayerCoins();
        int expectedBalance = 0;
        coins.AddCoins(10);

        // Act
        Result result = coins.SubtractCoins(value);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        coins.Balance.Should().Be(expectedBalance);
    }
}
