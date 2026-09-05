namespace CTF.Application.Tests.Players.Ranks;

/// <summary>Tests for RankCollection.</summary>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
public class RankCollectionTests
{
    static readonly int[] InvalidRankCases = [-1, 1000, RankCollection.Count];

    [TestCaseSource(nameof(InvalidRankCases))]
    public void GetById_WhenRankIsInvalid_ShouldReturnFailureResult(int value)
    {
        // Arrange
        RankId rankId = (RankId)value;
        string expectedMessage = Messages.InvalidRank;

        // Act
        Result<Rank> result = RankCollection.GetById(rankId);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void GetById_WhenRankIsValid_ShouldReturnSuccessResult()
    {
        // Arrange
        RankId rankId = RankId.Noob;
        string expectedRank = rankId.ToString();

        // Act
        Result<Rank> result = RankCollection.GetById(rankId);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be(expectedRank);
        result.Message.Should().BeEmpty();
    }

    [TestCaseSource(nameof(InvalidRankCases))]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void GetNextRank_WhenRankIsInvalid_ShouldReturnFailureResult(int value)
    {
        // Arrange
        RankId rankId = (RankId)value;
        string expectedMessage = Messages.InvalidRank;

        // Act
        Result<Rank> result = RankCollection.GetNextRank(rankId);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void GetNextRank_WhenRankIsValid_ShouldReturnSuccessResult()
    {
        // Arrange
        RankId previousRank = RankId.Noob;
        string expectedNextRank = RankId.Medium.ToString();

        // Act
        Result<Rank> result = RankCollection.GetNextRank(previousRank);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be(expectedNextRank);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void GetNextRank_WhenThereIsNoNextRank_ShouldNotReturnsAnyRank()
    {
        // Arrange
        RankId previousRank = RankId.Legendary;
        string expectedNextRank = "None";

        // Act
        Result<Rank> result = RankCollection.GetNextRank(previousRank);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be(expectedNextRank);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void GetByRequiredKills_WhenKillsIsNegative_ShouldReturnFailureResult()
    {
        // Arrange
        var expectedMessage = Messages.ValueCannotBeNegative;
        int kills = -1;

        // Act
        Result<Rank> result = RankCollection.GetByRequiredKills(kills);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [TestCaseSource(typeof(GetRankByRequiredKillsTestCases))]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void GetByRequiredKills_WhenRankIsObtainedByKills_ShouldReturnSuccessResult((RankId ExpectedRankId, int Kills) rank)
    {
        // Arrange

        // Act
        Result<Rank> result = RankCollection.GetByRequiredKills(rank.Kills);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(rank.ExpectedRankId);
    }
}
