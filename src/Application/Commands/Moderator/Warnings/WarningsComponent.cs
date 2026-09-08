namespace CTF.Application.Commands.EcsDomain;

/// <summary>
/// Records how many times a player has been warned; kicks after the third warning.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Ecs)]
public class WarningsComponent : Component
{
    [ChangeDriversAttribute(ChangeDriver.CommandSet)]
    public int Value { get; set; }
}
