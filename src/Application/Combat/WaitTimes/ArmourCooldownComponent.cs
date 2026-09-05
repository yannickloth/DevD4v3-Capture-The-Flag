namespace CTF.Application.Combat.WaitTimes;

/// <summary>
/// Records the unix-time instant after which the armour-restore
/// command may be used again.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Ecs)]
public class ArmourCooldownComponent : Component
{
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Ecs)]
    public long Value { get; set; }
}
