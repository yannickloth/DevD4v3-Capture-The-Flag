namespace CTF.Application.AntiCheat;

/// <summary>
/// Represents a component that stores the last shot time and 
/// is used to detect rapid shooting techniques such as C-Bug.
/// </summary>
/// <remarks>Change drivers: CD-14 (root; anti-cheat policy); CD-32 (component storage) → CD-14</remarks>
public class LastFiredTimeComponent : Component
{
    /// <summary>Gets or sets the last shot time.</summary>
    /// <remarks>Change drivers: CD-14 (root; anti-cheat policy); CD-32 (component storage) → CD-14</remarks>
    public long Value { get; set; }
}
