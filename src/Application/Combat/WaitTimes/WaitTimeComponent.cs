namespace CTF.Application.Combat.WaitTimes;

/// <summary>
/// Records the unix-time instant after which a cooldown-gated
/// self-restore command may be used again.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Configuration, ChangeDriver.Ecs)]
public class WaitTimeComponent : Component
{
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Ecs)]
    public long Value { get; set; }
}
