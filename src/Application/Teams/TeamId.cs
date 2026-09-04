namespace CTF.Application.Teams;

/// <summary>
/// Represents the identifiers of the teams in the CTF gamemode.
/// </summary>
/// <remarks>Change drivers: CD-31 (root; player team id) ‖ CD-02 (root; CTF game-rules specification: team identity)</remarks>
public enum TeamId
{
    /// <summary>The Alpha team.</summary>
    /// <remarks>Change drivers: CD-31 (root; player team id)</remarks>
    Alpha,
    /// <summary>The Beta team.</summary>
    /// <remarks>Change drivers: CD-31 (root; player team id)</remarks>
    Beta,
    /// <summary>The NoTeam state.</summary>
    /// <remarks>Change drivers: CD-31 (root; player team id) ‖ CD-02 (root; CTF game-rules specification: no-team state)</remarks>
    NoTeam = 0xFF
}
