namespace CTF.Application.GameRules;

/// <summary>
/// An ECS component tracking whether the player is in class selection.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection flow); CD-32 (ECS component) → CD-02</remarks>
public class ClassSelectionComponent : Component
{
    /// <summary>Gets or sets whether the player is in class selection.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection flow)</remarks>
    public bool IsInClassSelection { get; set; } = true;
}
