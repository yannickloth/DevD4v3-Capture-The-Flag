namespace CTF.Application.GameRules.EcsNsNs2;

/// <summary>
/// Provides extension methods for the class-selection player state.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Ecs)]
public static class ClassSelectionExtensions
{
    /// <summary>Checks whether the player is in class selection.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public static bool IsInClassSelection(this Player player)
        => player.GetComponent<ClassSelectionComponent>().IsInClassSelection;

    /// <summary>Checks whether the player is not in class selection.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public static bool IsNotInClassSelection(this Player player)
        => !player.IsInClassSelection();

    /// <summary>Checks whether the player has a forced class selection after death.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public static bool HasForcedClassSelectionAfterDeath(this Player player)
        => !player.IsInClassSelection();

    /// <summary>Enables class selection for the player.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public static void EnableClassSelection(this Player player)
        => player.GetComponent<ClassSelectionComponent>().IsInClassSelection = true;

    /// <summary>Disables class selection for the player.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public static void DisableClassSelection(this Player player)
        => player.GetComponent<ClassSelectionComponent>().IsInClassSelection = false;
}
