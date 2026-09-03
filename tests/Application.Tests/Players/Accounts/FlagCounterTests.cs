namespace CTF.Application.Tests.Players.Accounts;

/// <summary>Tests for PlayerInfo flag counters.</summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: PlayerInfo flag counters); CD-26 (NUnit test-framework contract) → CD-10; CD-27 (FluentAssertions contract) → CD-10</remarks>
public class FlagCounterTests
{
    [Test]
    public void AddBroughtFlags_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var player = new PlayerInfo();
        int expected = 2;

        // Act
        player.Stats.AddBroughtFlags();
        player.Stats.AddBroughtFlags();

        // Assert
        player.Stats.BroughtFlags.Should().Be(expected);
    }

    [Test]
    public void AddCapturedFlags_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var player = new PlayerInfo();
        int expected = 2;

        // Act
        player.Stats.AddCapturedFlags();
        player.Stats.AddCapturedFlags();

        // Assert
        player.Stats.CapturedFlags.Should().Be(expected);
    }

    [Test]
    public void AddDroppedFlags_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var player = new PlayerInfo();
        int expected = 2;

        // Act
        player.Stats.AddDroppedFlags();
        player.Stats.AddDroppedFlags();

        // Assert
        player.Stats.DroppedFlags.Should().Be(expected);
    }

    [Test]
    public void AddReturnedFlags_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var player = new PlayerInfo();
        int expected = 2;

        // Act
        player.Stats.AddReturnedFlags();
        player.Stats.AddReturnedFlags();

        // Assert
        player.Stats.ReturnedFlags.Should().Be(expected);
    }
}
