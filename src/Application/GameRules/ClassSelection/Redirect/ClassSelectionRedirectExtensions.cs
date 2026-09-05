namespace CTF.Application.GameRules.ClassSelection.Redirect;

/// <summary>
/// Provides the class-selection redirect extension, which also drives player spectating state.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
public static class ClassSelectionRedirectExtensions
{
    /// <summary>Redirects the player to the class selection screen.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    public static void RedirectToClassSelection(this Player player)
    {
        player.EnableClassSelection();
        player.ForceClassSelection();
        player.ToggleSpectating(true);
        player.ToggleSpectating(false);
    }
}
