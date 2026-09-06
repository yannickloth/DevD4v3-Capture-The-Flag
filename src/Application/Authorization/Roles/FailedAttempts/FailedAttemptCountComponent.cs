namespace CTF.Application.Authorization.Roles.FailedAttempts;

/// <summary>
/// Tracks the number of consecutive wrong secret-key attempts made by
/// a player while running the give-me-admin command.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Ecs)]
public class FailedAttemptCountComponent : Component
{
    [ChangeDriversAttribute(ChangeDriver.Authorization)]
    public int Value { get; set; } = 0;
}
