namespace CTF.Application.MapRotation;

/// <summary>
/// Represents the time left on the current map.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.MapRotation)]
public class TimeLeft
{
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    private const int MaxRoundTime = 3600;
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    private const int DefaultRoundTime = 900;

    /// <summary>
    /// Represents the interval in seconds.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    private int _interval = DefaultRoundTime;

    // This property can never be mutable.
    // If this property is modified from the outside, it may cause buffer overflow.
    /// <summary>
    /// Represents the time left in a text draw.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public string TextDraw { get; } = "00:00";

    /// <remarks>Change drivers: CD-12 (root; map-rotation rules)</remarks>
    public TimeLeft() => UpdateTextDraw();

    /// <summary>
    /// Checks if the countdown has ended.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public bool IsCompleted() => _interval == 0;

    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public Result SetInterval(Minutes minutes)
    {
        if (minutes.Value < 0 || minutes.Value > (MaxRoundTime / 60))
        {
            var message = Smart.Format(Messages.InvalidInterval, new { Max = MaxRoundTime / 60 });
            return Result.Failure(message);
        }

        _interval = minutes.Value * 60;
        UpdateTextDraw();
        return Result.Success();
    }

    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public Result SetInterval(Seconds seconds)
    {
        if (seconds.Value < 0 || seconds.Value > MaxRoundTime)
        {
            var message = Smart.Format(Messages.InvalidInterval, new { Max = MaxRoundTime });
            return Result.Failure(message);
        }

        _interval = seconds.Value;
        UpdateTextDraw();
        return Result.Success();
    }

    /// <summary>
    /// Reduces the time remaining until it reaches zero.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public void Decrease()
    {
        if (_interval == 0)
            return;

        _interval--;
        UpdateTextDraw();
    }

    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public void Reset()
    {
        _interval = DefaultRoundTime;
        UpdateTextDraw();
    }

    /// <summary>
    /// The purpose is to manipulate the buffer directly with pointers 
    /// to avoid memory reallocations caused by string interpolation.
    /// </summary>
    /// <remarks>
    /// This decision was made because the text will be updated every 1s by a timer.
    /// </remarks>
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    private unsafe void UpdateTextDraw()
    {
        int minutes = _interval / 60;
        int seconds = _interval % 60;

        int digit1 = minutes % 10;
        int digit0 = minutes / 10 % 10;

        int digit4 = seconds % 10;
        int digit3 = seconds / 10 % 10;

        fixed (char* text = TextDraw)
        {
            text[0] = (char)(digit0 + '0');
            text[1] = (char)(digit1 + '0');
            text[3] = (char)(digit3 + '0');
            text[4] = (char)(digit4 + '0');
        }
    }
}

[ChangeDriversAttribute(ChangeDriver.MapRotation)]
public readonly ref struct Minutes
{
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public int Value { get; }
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules)</remarks>
    public Minutes(int value) => Value = value;
}

[ChangeDriversAttribute(ChangeDriver.MapRotation)]
public readonly ref struct Seconds
{
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public int Value { get; }
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules)</remarks>
    public Seconds(int value) => Value = value;
}
