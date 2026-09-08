namespace CTF.Application.AntiCheat.EcsDomain;

/// <summary>
/// Represents a component that stores the last shot time and 
/// is used to detect rapid shooting techniques such as C-Bug.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.AntiCheat, ChangeDriver.Ecs)]
public class LastFiredTimeComponent : Component
{
    /// <summary>Gets or sets the last shot time.</summary>
    [ChangeDriversAttribute(ChangeDriver.AntiCheat, ChangeDriver.Ecs)]
    public long Value { get; set; }
}
