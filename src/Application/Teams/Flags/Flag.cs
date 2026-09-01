namespace CTF.Application.Teams.Flags;

/// <summary>
/// Represents a team flag with its state, carrier, and identity, following the CTF flag rules.
/// </summary>
/// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag state model and capture/score rules), CD-01 (open.mp/SampSharp platform API: attached-object rendering), CD-11 (map configuration: flag model/icon identity).</remarks>
public class Flag
{
    /// <summary>
    /// Gets the 3D model associated with the flag.
    /// </summary>
    /// <remarks>Change drivers: CD-11 (map configuration: flag model identity), CD-01 (open.mp/SampSharp platform API: pickup/textdraw model ids).</remarks>
    public required FlagModel Model { get; init; }

    /// <summary>
    /// Gets the map icon associated with the flag.
    /// </summary>
    /// <remarks>Change drivers: CD-11 (map configuration: flag icon identity), CD-01 (open.mp/SampSharp platform API: map-icon ids).</remarks>
    public required FlagIcon Icon { get; init; }

    /// <summary>
    /// Gets the display name of the flag.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag display name).</remarks>
    public required string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the primary color associated with the flag.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: team color), CD-01 (open.mp/SampSharp platform API: attached-object material color).</remarks>
    public required Color ColorHex { get; init; }

    /// <summary>
    /// Gets the current status of the flag.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag state machine).</remarks>
    public FlagStatus Status { get; private set; } = FlagStatus.BasePosition;

    /// <summary>
    /// Gets the player currently carrying the flag.
    /// </summary>
    /// <remarks>
    /// Returns <c>null</c> when the flag has no carrier.
    /// </remarks>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: one-flag-per-player carrier rule).</remarks>
    public Player Carrier { get; private set; }

    /// <summary>
    /// Checks if the flag has been captured by a player.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag carrier state).</remarks>
    public bool HasCarrier => Carrier is not null;

    /// <summary>
    /// Gets the name of the player who captured the flag.
    /// </summary>
    /// <remarks>
    /// If the flag is not captured, returns <c>None</c>.
    /// </remarks>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: carrier display), CD-10 (player-statistics/rank model: carrier stats).</remarks>
    public string CarrierName => HasCarrier ? Carrier.Name : "None";

    /// <summary>
    /// Marks the flag as captured by the specified player.
    /// </summary>
    /// <param name="player">
    /// The player who captured the flag.
    /// </param>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: capture-from-base rule).</remarks>
    public void Capture(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        RemoveCarrier();
        SetCarrier(player);
        Status = FlagStatus.Captured;
    }

    /// <summary>
    /// Marks the flag as taken from a dropped state by the specified player.
    /// </summary>
    /// <param name="player">
    /// The player who picked up the flag.
    /// </param>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: take-from-non-base rule).</remarks>
    public void Take(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        RemoveCarrier();
        SetCarrier(player);
        Status = FlagStatus.Taken;
    }

    /// <summary>
    /// Drops the flag and removes its current carrier.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag carrier death/disconnect drop rule).</remarks>
    public void Drop()
    {
        RemoveCarrier();
        Status = FlagStatus.Dropped;
    }

    /// <summary>
    /// Returns the flag to its base state and removes its current carrier.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag auto-return and return rule).</remarks>
    public void ReturnToBase()
    {
        RemoveCarrier();
        Status = FlagStatus.BasePosition;
    }

    /// <summary>
    /// Resets the flag to its initial state.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: round/flag reset rule).</remarks>
    public void Reset()
    {
        RemoveCarrier();
        Status = FlagStatus.BasePosition;
    }

    /// <summary>
    /// Sets the player who holds the flag.
    /// </summary>
    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API: attached-object rendering), CD-02 (CTF game-rules specification: carrier attachment).</remarks>
    private void SetCarrier(Player player)
    {
        Carrier = player;
        player.SetAttachedObject(
            index: 0, 
            modelId: (int)Model, 
            bone: Bone.Spine, 
            offset: new Vector3(-0.057000f, -0.108999f, 0.075000f), 
            rotation: new Vector3(171.500030f, 66.200012f, -4.100002f), 
            scale: new Vector3(1.0f, 1.0f, 1.0f), 
            materialColor1: ColorHex,
            materialColor2: ColorHex
        );
    }

    /// <summary>
    /// Removes the flag that the player is holding.
    /// </summary>
    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API: attached-object removal).</remarks>
    private void RemoveCarrier()
    {
        if (Carrier is not null)
        {
            Carrier.RemoveAttachedObject(0);
            Carrier = default;
        }
    }
}
