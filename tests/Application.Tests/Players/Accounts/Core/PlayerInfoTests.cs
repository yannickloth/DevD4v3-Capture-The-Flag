namespace CTF.Application.Tests.Statistics.Model;

/// <summary>Tests for PlayerInfo.</summary>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
public class PlayerInfoTests
{
    static readonly int[] InvalidRankCases = [-1, -2, RankCollection.Count];
    static readonly int[] InvalidSkinCases = [-1, -2, 312];

    [TestCaseSource(nameof(InvalidRankCases))]
    public void SetRank_WhenRankIdIsInvalid_ShouldReturnFailureResult(int value)
    {
        // Arrange
        var player = new PlayerInfo();
        RankId rankId = (RankId)value;
        var expectedMessage = Messages.InvalidRank;

        // Act
        Result result = player.Stats.SetRank(rankId);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
        player.Stats.RankId.Should().NotBe(rankId);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void SetRank_WhenRankIdIsValid_ShouldReturnSuccessResult()
    {
        // Arrange
        var player = new PlayerInfo();
        RankId rankId = RankId.Maniac;

        // Act
        Result result = player.Stats.SetRank(rankId);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        player.Stats.RankId.Should().Be(rankId);
    }

    [TestCaseSource(nameof(InvalidSkinCases))]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void SetSkin_WhenSkinIdIsInvalid_ShouldReturnFailureResult(int skinId)
    {
        // Arrange
        var player = new PlayerInfo();
        var expectedMessage = Messages.InvalidSkin;

        // Act
        Result result = player.Appearance.SetSkin(skinId);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void SetSkin_WhenSkinIdIsValid_ShouldReturnSuccessResult()
    {
        // Arrange
        var player = new PlayerInfo();
        // See https://www.open.mp/docs/scripting/resources/skins
        // Skin valid between 0 to 311.
        int skinId = 311;

        // Act
        Result result = player.Appearance.SetSkin(skinId);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        player.Appearance.SkinId.Should().Be(skinId);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void SetTotalKills_WhenArgumentIsNegative_ShouldReturnFailureResult()
    {
        // Arrange
        var player = new PlayerInfo();
        int kills = -1;
        var expectedMessage = Messages.ValueCannotBeNegative;

        // Act
        Result result = player.Stats.SetTotalKills(kills);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
        player.Stats.TotalKills.Should().NotBe(kills);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void SetTotalKills_WhenArgumentIsPositive_ShouldReturnSuccessResult()
    {
        // Arrange
        var player = new PlayerInfo();
        int kills = 10;

        // Act
        Result result = player.Stats.SetTotalKills(kills);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        player.Stats.TotalKills.Should().Be(kills);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void AddTotalKills_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var player = new PlayerInfo();
        int expectedKills = 2;

        // Act
        player.Stats.AddTotalKills();
        player.Stats.AddTotalKills();

        // Assert
        player.Stats.TotalKills.Should().Be(expectedKills);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void SetTotalDeaths_WhenArgumentIsNegative_ShouldReturnFailureResult()
    {
        // Arrange
        var player = new PlayerInfo();
        int deaths = -1;
        var expectedMessage = Messages.ValueCannotBeNegative;

        // Act
        Result result = player.Stats.SetTotalDeaths(deaths);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
        player.Stats.TotalDeaths.Should().NotBe(deaths);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void SetTotalDeaths_WhenArgumentIsPositive_ShouldReturnSuccessResult()
    {
        // Arrange
        var player = new PlayerInfo();
        int deaths = 10;

        // Act
        Result result = player.Stats.SetTotalDeaths(deaths);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        player.Stats.TotalDeaths.Should().Be(deaths);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void AddTotalDeaths_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var player = new PlayerInfo();
        int expectedDeaths = 2;

        // Act
        player.Stats.AddTotalDeaths();
        player.Stats.AddTotalDeaths();

        // Assert
        player.Stats.TotalDeaths.Should().Be(expectedDeaths);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void AddHeadShots_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var player = new PlayerInfo();
        int expectedHeadShots = 2;

        // Act
        player.Stats.AddHeadShots();
        player.Stats.AddHeadShots();

        // Assert
        player.Stats.HeadShots.Should().Be(expectedHeadShots);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void AddGunGameWins_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var player = new PlayerInfo();
        int expectedGunGameWins = 2;

        // Act
        player.Stats.AddGunGameWins();
        player.Stats.AddGunGameWins();

        // Assert
        player.Stats.GunGameWins.Should().Be(expectedGunGameWins);
    }
}
