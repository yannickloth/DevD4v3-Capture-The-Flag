namespace CTF.Application.GameRules;

/// <summary>
/// Provides extension methods for working with the player's paused state.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification); CD-01 (open.mp/SampSharp platform API) → CD-02</remarks>
public static class PlayerPauseExtensions
{
    /// <summary>
    /// Determines whether the specified player is currently paused.
    /// </summary>
    /// <param name="player">
    /// The player whose paused state should be checked.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the player is currently paused;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification); CD-01 (open.mp/SampSharp platform API) → CD-02</remarks>
    public static bool IsPaused(this Player player)
        => player.GetComponent<PlayerDataComponent>().IsPaused;
}
