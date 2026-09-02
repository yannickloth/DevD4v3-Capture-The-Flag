namespace CTF.Application.Teams.ClassSelection;

/// <summary>
/// An ECS component tracking whether the player is in class selection.
/// </summary>
/// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API: ECS component), CD-02 (CTF game-rules specification: class-selection flow).</remarks>
public class ClassSelectionComponent : Component
{
    /// <summary>Gets or sets whether the player is in class selection.</summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: class-selection flow).</remarks>
    public bool IsInClassSelection { get; set; } = true;
}
