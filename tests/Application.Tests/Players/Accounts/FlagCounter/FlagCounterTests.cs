namespace CTF.Application.Tests.Statistics;

/// <summary>Tests for PlayerInfo flag counters.</summary>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
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
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
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
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
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
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
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
