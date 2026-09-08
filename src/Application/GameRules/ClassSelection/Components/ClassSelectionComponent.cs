namespace CTF.Application.GameRules.EcsNsNs2;

/// <summary>
/// An ECS component tracking whether the player is in class selection.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Ecs)]
public class ClassSelectionComponent : Component
{
    /// <summary>Gets or sets whether the player is in class selection.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public bool IsInClassSelection { get; set; } = true;
}
