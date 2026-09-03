namespace CTF.Application.GameRules;

/// <summary>
/// Represents the settings for the flag carrier.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: carrier pause/radar rules); CD-17 (game configuration/.env schema: FlagCarrier__*) → CD-02</remarks>
public class FlagCarrierSettings
{
    /// <summary>
    /// Gets the maximum duration (in seconds) that the flag carrier can be idle (AFK) while holding the flag.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: carrier-pause flag return rule); CD-17 (game configuration/.env schema: FlagCarrier__PauseTime) → CD-02</remarks>
    public int PauseTime { get; init; } = 30;

    /// <summary>
    /// Gets a value indicating whether the flag carrier should be shown on the radar map.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: carrier radar rule); CD-17 (game configuration/.env schema: FlagCarrier__ShowOnRadarMap) → CD-02</remarks>
    public bool ShowOnRadarMap { get; set; } = true;
}
