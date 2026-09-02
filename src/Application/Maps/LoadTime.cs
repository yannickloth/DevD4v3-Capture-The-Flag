namespace CTF.Application.Maps;

/// <summary>
/// Represents the total wait time for the new map to load.
/// </summary>
/// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-01 (open.mp/SampSharp platform API) → CD-12</remarks>
public class LoadTime
{
    private readonly Action _onLoadingMap;
    private readonly Action _onLoadedMap;
    private int _interval = MaxLoadTime;
    /// <remarks>Change drivers: CD-12 (root; root; map-rotation rules)</remarks>
    public const int MaxLoadTime = 10;

    /// <summary>
    /// Displays the load time in the game.
    /// </summary>
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-01 (open.mp/SampSharp platform API) → CD-12</remarks>
    public string GameText { get; private set; } = string.Empty;

    /// <summary>
    /// Represents the interval in seconds.
    /// </summary>
    /// <remarks>Change drivers: CD-12 (root; root; map-rotation rules)</remarks>
    public int Interval => _interval;

    /// <remarks>Change drivers: CD-12 (root; root; map-rotation rules)</remarks>
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
    /// <remarks>Change drivers: CD-12 (root; root; map-rotation rules)</remarks>
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

    /// <remarks>Change drivers: CD-12 (root; root; map-rotation rules)</remarks>
    private void UpdateGameText() => GameText = $"Loading map... ({_interval})";
    /// <remarks>Change drivers: CD-12 (root; root; map-rotation rules)</remarks>
    private void Reset()
    {
        _interval = MaxLoadTime;
        GameText = string.Empty;
    }
}
