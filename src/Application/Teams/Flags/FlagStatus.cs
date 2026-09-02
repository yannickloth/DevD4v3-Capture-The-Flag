namespace CTF.Application.Teams.Flags;

/// <summary>
/// Represents the states a flag can be in during the match.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; root; CTF game-rules specification: flag state machine)</remarks>
public enum FlagStatus
{
    /// <summary>
    /// Indicates that the flag is at the base position, where it is defended to prevent enemy capture.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; root; CTF game-rules specification: flag state machine)</remarks>
    BasePosition,

    /// <summary>
    /// Indicates that a player has captured the opposing team's flag from their base.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; root; CTF game-rules specification: flag state machine)</remarks>
    Captured,

    /// <summary>
    /// Indicates that a player has returned the flag to their team's base.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; root; CTF game-rules specification: flag state machine)</remarks>
    Returned,

    /// <summary>
    /// Indicates that a player has taken the flag from a position other than the base.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; root; CTF game-rules specification: flag state machine)</remarks>
    Taken,

    /// <summary>
    /// Indicates that a player has captured the opposing team's flag and brought it back to their own base.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; root; CTF game-rules specification: flag state machine)</remarks>
    Brought,

    /// <summary>
    /// Indicates that a player has dropped the opposing team's flag.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; root; CTF game-rules specification: flag state machine)</remarks>
    Dropped
}
