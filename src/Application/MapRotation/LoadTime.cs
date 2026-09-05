namespace CTF.Application.MapRotation;

/// <summary>
/// Represents the total wait time for the new map to load.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.MapRotation)]
public class LoadTime
{
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    private readonly Action _onLoadingMap;

    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    private readonly Action _onLoadedMap;

    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    private int _interval = MaxLoadTime;

    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public const int MaxLoadTime = 10;

    /// <summary>
    /// Displays the load time in the game.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public string GameText { get; private set; } = string.Empty;

    /// <summary>
    /// Represents the interval in seconds.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public int Interval => _interval;

    /// <remarks>Change drivers: CD-12 (root; map-rotation rules)</remarks>
    public LoadTime(Action onLoadingMap, Action onLoadedMap)
    {
        ArgumentNullException.ThrowIfNull(onLoadingMap);
        ArgumentNullException.ThrowIfNull(onLoadedMap);
        _onLoadingMap = onLoadingMap;
        _onLoadedMap = onLoadedMap;
    }

    /// <summary>
    /// Reduces the load time until it reaches zero.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public void Decrease()
    {
        if (_interval == 0)
        {
            Reset();
            _onLoadedMap();
            return;
        }

        if (_interval == MaxLoadTime)
        {
            _onLoadingMap();
        }

        _interval--;
        UpdateGameText();
    }

    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    private void UpdateGameText() => GameText = $"Loading map... ({_interval})";
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    private void Reset()
    {
        _interval = MaxLoadTime;
        GameText = string.Empty;
    }
}
