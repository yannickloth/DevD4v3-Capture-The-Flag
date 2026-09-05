namespace CTF.Application.GameRules.Flag.FlagState;

/// <summary>
/// Represents a team flag with its state, carrier, and identity, following the CTF flag rules.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.MapIcon, ChangeDriver.Model)]
public class Flag
{
    /// <summary>
    /// Gets the 3D model associated with the flag.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Model)]
    public required FlagModel Model { get; init; }

    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.MapIcon)]
    public required FlagIcon Icon { get; init; }

    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public required Color ColorHex { get; init; }

    /// <summary>
    /// Gets the display name of the flag.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public required string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the current status of the flag.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public FlagStatus Status { get; private set; } = FlagStatus.BasePosition;

    /// <summary>
    /// Gets the player currently carrying the flag.
    /// </summary>
    /// <remarks>
    /// Returns <c>null</c> when the flag has no carrier.
    /// </remarks>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public FlagCarrier? Carrier { get; private set; }

    /// <summary>
    /// Checks if the flag has been captured by a player.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public bool HasCarrier => Carrier is not null;

    /// <summary>
    /// Determines whether the specified player is carrying this flag.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns>
    /// <see langword="true"/> if the player is carrying this flag;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
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
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
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
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
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
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public void Drop()
    {
        RemoveCarrier();
        Status = FlagStatus.Dropped;
    }

    /// <summary>
    /// Returns the flag to its base state and removes its current carrier.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public void ReturnToBase()
    {
        RemoveCarrier();
        Status = FlagStatus.BasePosition;
    }

    /// <summary>
    /// Resets the flag to its initial state.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public void Reset()
    {
        RemoveCarrier();
        Status = FlagStatus.BasePosition;
    }

    /// <summary>
    /// Sets the player who holds the flag.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    private void SetCarrier(Player player)
    {
        Carrier = new FlagCarrier(player);
        CarrierAttachment.Attach(player, Model, ColorHex);
    }

    /// <summary>
    /// Removes the flag that the player is holding.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    private void RemoveCarrier()
    {
        if (Carrier is not null)
        {
            CarrierAttachment.Detach(Carrier.Player);
            Carrier = null;
        }
    }

    /// <summary>
    /// Renders the flag on the carrier via an attached object.
    /// It isolates the platform rendering details of the carrier-attachment rule.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.AttachedObject)]
    private static class CarrierAttachment
    {
        [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.AttachedObject)]
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

        [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.AttachedObject)]
        internal static void Detach(Player player)
        {
            player.RemoveAttachedObject(0);
        }
    }
}
