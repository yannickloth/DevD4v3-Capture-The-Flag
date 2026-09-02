namespace CTF.Application.Tests.Players.TopPlayers;

/// <summary>Tests for MaxTopPlayers.</summary>
/// <remarks>Change drivers: CD-10 (player-statistics/rank model); CD-17 (game configuration/.env schema); CD-29 (code-under-test: MaxTopPlayers); CD-26 (NUnit test-framework contract) → CD-29; CD-27 (FluentAssertions contract) → CD-29</remarks>
public class MaxTopPlayersTests
{
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(10)]
    [TestCase(15)]
    public void Create_WhenCalledWithValidValue_ShouldReturnSuccessResult(int value)
    {
        // Arrange

        // Act
        Result<MaxTopPlayers> result = MaxTopPlayers.Create(value);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(value);
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(4)]
    [TestCase(16)]
    [TestCase(20)]
    public void Create_WhenCalledWithInvalidValue_ShouldReturnFailureResult(int value)
    {
        // Arrange
        var expectedMessage = Messages.InvalidMaxTopPlayers;

        // Act
        Result<MaxTopPlayers> result = MaxTopPlayers.Create(value);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }
}
