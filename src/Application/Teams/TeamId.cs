namespace CTF.Application.Teams;

/// <summary>
/// Represents the identifiers of the teams in the CTF gamemode.
/// </summary>
/// <remarks>Change drivers: CD-02 (CTF game-rules specification: team identity), CD-01 (open.mp/SampSharp platform API: player team id)</remarks>
public enum TeamId
{
    /// <summary>The Alpha team.</summary>
    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API: team id).</remarks>
    Alpha,
    /// <summary>The Beta team.</summary>
    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API: team id).</remarks>
    Beta,
    /// <summary>The NoTeam state.</summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: no-team state), CD-01 (open.mp/SampSharp platform API: no-team id)</remarks>
    NoTeam = 0xFF
}
