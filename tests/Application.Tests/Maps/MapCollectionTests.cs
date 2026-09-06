namespace CTF.Application.Tests.Maps;

/// <summary>Tests for MapCollection.</summary>
[ChangeDriversAttribute(ChangeDriver.Map, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
public class MapCollectionTests
{
    static readonly int[] InvalidMapCases = [-1, 1000];
    private MapCollection _maps;

    [SetUp]
    public void SetUp()
    {
        _maps = new MapCollection(TestPaths.Maps);
    }

    [TestCase("de")]
    [TestCase("DE")]
    [TestCase("dE")]
    [TestCase("De")]
    [ChangeDriversAttribute(ChangeDriver.Map, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void GetAll_WhenAllMapsAreObtainedWithFindBy_ShouldReturnEnumerable(string findBy)
    {
        // Arrange
        string[] expectedMaps =
        [
            "de_aztec",
            "de_dust2",
            "de_dust2_small",
            "de_dust2x1",
            "de_dust2x2",
            "de_dust2x3",
            "de_dust2x4",
            "de_dust2x5",
            "de_dust5",
            "DesertGlory"
        ];

        // Act
        IEnumerable<IMap> maps = _maps.GetAll(findBy);
        string[] actual = maps.Select(map => map.Name).ToArray();

        // Assert
        actual.Should().BeEquivalentTo(expectedMaps);
    }

    [TestCaseSource(nameof(InvalidMapCases))]
    [ChangeDriversAttribute(ChangeDriver.Map, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void GetById_WhenMapIdIsInvalid_ShouldReturnFailureResult(int mapId)
    {
        // Arrange
        string expectedMessage = Messages.InvalidMap;

        // Act
        Result<IMap> result = _maps.GetById(mapId);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Map, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void GetById_WhenMapIdEqualsCount_ShouldReturnFailureResult()
    {
        // Arrange
        string expectedMessage = Messages.InvalidMap;

        // Act
        Result<IMap> result = _maps.GetById(_maps.Count);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [ChangeDriversAttribute(ChangeDriver.Map, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void GetById_WhenMapIdIsValid_ShouldReturnSuccessResult(int mapId)
    {
        // Act
        Result<IMap> result = _maps.GetById(mapId);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(mapId);
        result.Value.Name.Should().NotBeNullOrEmpty();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Map, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void GetByName_WhenMapNameIsNotFound_ShouldReturnFailureResult()
    {
        // Arrange
        string mapName = "NotFound";
        string expectedMessage = Messages.MapNotFound;

        // Act
        Result<IMap> result = _maps.GetByName(mapName);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
    }

    [TestCase("de_aztec")]
    [TestCase("DE_AZTEC")]
    [TestCase("De_Aztec")]
    [ChangeDriversAttribute(ChangeDriver.Map, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void GetByName_WhenMapNameIsFound_ShouldReturnSuccessResult(string mapName)
    {
        // Arrange
        string expectedMapName = "de_aztec";

        // Act
        Result<IMap> result = _maps.GetByName(mapName);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be(expectedMapName);
    }
}
