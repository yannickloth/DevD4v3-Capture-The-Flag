namespace CTF.Application.Tests.Statistics;

/// <summary>Tests for Rank.</summary>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
public class RankTests
{
    [Test]
    public void IsMax_WhenRankIsMaximum_ShouldReturnTrue()
    {
        // Arrange
        RankId rankId = RankId.Legendary;
        Result<Rank> result = RankCollection.GetById(rankId);
        Rank rank = result.Value;

        // Act
        bool actual = rank.IsMax();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void IsMax_WhenRankIsNotMaximum_ShouldReturnFalse()
    {
        // Arrange
        RankId rankId = RankId.Junior;
        Result<Rank> result = RankCollection.GetById(rankId);
        Rank rank = result.Value;

        // Act
        bool actual = rank.IsMax();

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void IsNotMax_WhenRankIsNotMaximum_ShouldReturnTrue() 
    {
        // Arrange
        RankId rankId = RankId.Junior;
        Result<Rank> result = RankCollection.GetById(rankId);
        Rank rank = result.Value;

        // Act
        bool actual = rank.IsNotMax();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void IsNotMax_WhenRankIsMaximum_ShouldReturnFalse()
    {
        // Arrange
        RankId rankId = RankId.Legendary;
        Result<Rank> result = RankCollection.GetById(rankId);
        Rank rank = result.Value;

        // Act
        bool actual = rank.IsNotMax();

        // Assert
        actual.Should().BeFalse();
    }
}
