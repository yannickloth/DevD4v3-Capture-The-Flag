namespace CTF.Application.Tests.Players.Accounts;

/// <summary>Tests for PlayerInfo flag counters.</summary>
/// <remarks>Change drivers: CD-10 (player-statistics/rank model), CD-29 (code-under-test: PlayerInfo flag counters), CD-26 (NUnit test-framework contract), CD-27 (FluentAssertions contract)</remarks>
public class FlagCounterTests
{
    [Test]
    public void AddBroughtFlags_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var player = new PlayerInfo();
        int expected = 2;

        // Act
        player.AddBroughtFlags();
        player.AddBroughtFlags();

        // Assert
        player.BroughtFlags.Should().Be(expected);
    }

    [Test]
    public void AddCapturedFlags_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var player = new PlayerInfo();
        int expected = 2;

        // Act
        player.AddCapturedFlags();
        player.AddCapturedFlags();

        // Assert
        player.CapturedFlags.Should().Be(expected);
    }

    [Test]
    public void AddDroppedFlags_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var player = new PlayerInfo();
        int expected = 2;

        // Act
        player.AddDroppedFlags();
        player.AddDroppedFlags();

        // Assert
        player.DroppedFlags.Should().Be(expected);
    }

    [Test]
    public void AddReturnedFlags_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var player = new PlayerInfo();
        int expected = 2;

        // Act
        player.AddReturnedFlags();
        player.AddReturnedFlags();

        // Assert
        player.ReturnedFlags.Should().Be(expected);
    }
}
