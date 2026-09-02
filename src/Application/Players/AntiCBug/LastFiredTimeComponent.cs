namespace CTF.Application.Players.AntiCBug;

/// <summary>
/// Represents a component that stores the last shot time and 
/// is used to detect rapid shooting techniques such as C-Bug.
/// </summary>
/// <remarks>Change drivers: CD-14 (anti-cheat policy); CD-01 (open.mp/SampSharp platform API)</remarks>
public class LastFiredTimeComponent : Component
{
    /// <summary>Gets or sets the last shot time.</summary>
    /// <remarks>Change drivers: CD-14 (anti-cheat policy); CD-01 (open.mp/SampSharp platform API)</remarks>
    public long Value { get; set; }
}
