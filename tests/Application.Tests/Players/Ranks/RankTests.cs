namespace CTF.Application.Tests.Players.Ranks;

/// <summary>Tests for IRank.</summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: IRank); CD-26 (NUnit test-framework contract) → CD-10; CD-27 (FluentAssertions contract) → CD-10</remarks>
public class RankTests
{
    [Test]
    public void IsMax_WhenRankIsMaximum_ShouldReturnTrue()
    {
        // Arrange
        RankId rankId = RankId.Legendary;
        Result<IRank> result = RankCollection.GetById(rankId);
        IRank rank = result.Value;

        // Act
        bool actual = rank.IsMax();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void IsMax_WhenRankIsNotMaximum_ShouldReturnFalse()
    {
        // Arrange
        RankId rankId = RankId.Junior;
        Result<IRank> result = RankCollection.GetById(rankId);
        IRank rank = result.Value;

        // Act
        bool actual = rank.IsMax();

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void IsNotMax_WhenRankIsNotMaximum_ShouldReturnTrue() 
    {
        // Arrange
        RankId rankId = RankId.Junior;
        Result<IRank> result = RankCollection.GetById(rankId);
        IRank rank = result.Value;

        // Act
        bool actual = rank.IsNotMax();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void IsNotMax_WhenRankIsMaximum_ShouldReturnFalse()
    {
        // Arrange
        RankId rankId = RankId.Legendary;
        Result<IRank> result = RankCollection.GetById(rankId);
        IRank rank = result.Value;

        // Act
        bool actual = rank.IsNotMax();

        // Assert
        actual.Should().BeFalse();
    }
}
