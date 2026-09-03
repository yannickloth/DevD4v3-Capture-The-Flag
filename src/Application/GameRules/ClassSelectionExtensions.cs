namespace CTF.Application.GameRules;

/// <summary>
/// Provides extension methods for the class-selection player state.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection flow); CD-01 (open.mp/SampSharp platform API: player ECS) → CD-02</remarks>
public static class ClassSelectionExtensions
{
    /// <summary>Checks whether the player is in class selection.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection flow)</remarks>
    public static bool IsInClassSelection(this Player player)
        => player.GetComponent<ClassSelectionComponent>().IsInClassSelection;

    /// <summary>Checks whether the player is not in class selection.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection flow)</remarks>
    public static bool IsNotInClassSelection(this Player player)
        => !player.IsInClassSelection();

    /// <summary>Checks whether the player has a forced class selection after death.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: death/respawn class selection)</remarks>
    public static bool HasForcedClassSelectionAfterDeath(this Player player)
        => !player.IsInClassSelection();

    /// <summary>Enables class selection for the player.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection flow)</remarks>
    public static void EnableClassSelection(this Player player)
        => player.GetComponent<ClassSelectionComponent>().IsInClassSelection = true;

    /// <summary>Disables class selection for the player.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection flow)</remarks>
    public static void DisableClassSelection(this Player player)
        => player.GetComponent<ClassSelectionComponent>().IsInClassSelection = false;

    /// <summary>Redirects the player to the class selection screen.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection flow); CD-01 (open.mp/SampSharp platform API: spectating) → CD-02</remarks>
    public static void RedirectToClassSelection(this Player player)
    {
        player.EnableClassSelection();
        player.ForceClassSelection();
        player.ToggleSpectating(true);
        player.ToggleSpectating(false);
    }
}
