namespace CTF.Application.GameRules;

/// <summary>
/// Represents a team flag with its state, carrier, and identity, following the CTF flag rules.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag state model and capture/score rules); CD-31 (player entity) → CD-02; CD-38 (map-icon id resources) → CD-02; CD-39 (attached-object API) → CD-02; CD-44 (object model id resources) → CD-02</remarks>
public class Flag
{
    /// <summary>
    /// Gets the 3D model associated with the flag.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag visual identity); CD-38 (map-icon id resources); CD-39 (attached-object material color); CD-44 (object model id resources) → CD-02</remarks>
    public required FlagIdentity Identity { get; init; }

    /// <summary>
    /// Gets the display name of the flag.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag display name)</remarks>
    public required string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the current status of the flag.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag state machine)</remarks>
    public FlagStatus Status { get; private set; } = FlagStatus.BasePosition;

    /// <summary>
    /// Gets the player currently carrying the flag.
    /// </summary>
    /// <remarks>
    /// Returns <c>null</c> when the flag has no carrier.
    /// </remarks>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: one-flag-per-player carrier rule)</remarks>
    public FlagCarrier? Carrier { get; private set; }

    /// <summary>
    /// Checks if the flag has been captured by a player.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag carrier state)</remarks>
    public bool HasCarrier => Carrier is not null;

    /// <summary>
    /// Determines whether the specified player is carrying this flag.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns>
    /// <see langword="true"/> if the player is carrying this flag;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag carrier state)</remarks>
    public bool IsCarriedBy(Player player)
    {
        if (!HasCarrier)
            return false;

        return Carrier.Is(player);
    }

    /// <summary>
    /// Marks the flag as captured by the specified player.
    /// </summary>
    /// <param name="player">
    /// The player who captured the flag.
    /// </param>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: capture-from-base rule)</remarks>
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
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: take-from-non-base rule)</remarks>
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
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag carrier death/disconnect drop rule)</remarks>
    public void Drop()
    {
        RemoveCarrier();
        Status = FlagStatus.Dropped;
    }

    /// <summary>
    /// Returns the flag to its base state and removes its current carrier.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag auto-return and return rule)</remarks>
    public void ReturnToBase()
    {
        RemoveCarrier();
        Status = FlagStatus.BasePosition;
    }

    /// <summary>
    /// Resets the flag to its initial state.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: round/flag reset rule)</remarks>
    public void Reset()
    {
        RemoveCarrier();
        Status = FlagStatus.BasePosition;
    }

    /// <summary>
    /// Sets the player who holds the flag.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: carrier attachment)</remarks>
    private void SetCarrier(Player player)
    {
        Carrier = new FlagCarrier(player);
        CarrierAttachment.Attach(player, Identity.Model, Identity.ColorHex);
    }

    /// <summary>
    /// Removes the flag that the player is holding.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: carrier attachment)</remarks>
    private void RemoveCarrier()
    {
        if (Carrier is not null)
        {
            CarrierAttachment.Detach(Carrier.Player);
            Carrier = null;
        }
    }

    /// <summary>
    /// Represents the visual identity of the flag: model, map icon, and color.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag visual identity); CD-44 (model/icon id spaces, color type) → CD-02</remarks>
    public sealed record FlagIdentity
    {
        /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag visual identity); CD-38 (map-icon id resources); CD-39 (attached-object material color); CD-44 (object model id resources) → CD-02</remarks>
        public required FlagModel Model { get; init; }

        /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag visual identity); CD-38 (map-icon id) → CD-02</remarks>
        public required FlagIcon Icon { get; init; }

        /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team color); CD-39 (attached-object material color) → CD-02</remarks>
        public required Color ColorHex { get; init; }
    }

    /// <summary>
    /// Renders the flag on the carrier via an attached object.
    /// It isolates the platform rendering details of the carrier-attachment rule.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: carrier attachment); CD-39 (attached-object rendering: index/bone/offset/rotation/scale/material color) → CD-02</remarks>
    private static class CarrierAttachment
    {
        /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: carrier attachment); CD-39 (attached-object rendering: index/bone/offset/rotation/scale/material color) → CD-02</remarks>
        internal static void Attach(Player player, FlagModel model, Color color)
        {
            player.SetAttachedObject(
                index: 0,
                modelId: (int)model,
                bone: Bone.Spine,
                offset: new Vector3(-0.057000f, -0.108999f, 0.075000f),
                rotation: new Vector3(171.500030f, 66.200012f, -4.100002f),
                scale: new Vector3(1.0f, 1.0f, 1.0f),
                materialColor1: color,
                materialColor2: color
            );
        }

        /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: carrier attachment); CD-39 (attached-object removal: attachment index) → CD-02</remarks>
        internal static void Detach(Player player)
        {
            player.RemoveAttachedObject(0);
        }
    }
}
