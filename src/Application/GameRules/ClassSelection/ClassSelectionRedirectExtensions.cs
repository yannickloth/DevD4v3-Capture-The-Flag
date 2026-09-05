namespace CTF.Application.GameRules.ClassSelection;

/// <summary>
/// Provides the class-selection redirect extension, which also drives player spectating state.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection flow); CD-31 (player entity & lifecycle events: spectating) → CD-02</remarks>
public static class ClassSelectionRedirectExtensions
{
    /// <summary>Redirects the player to the class selection screen.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection flow); CD-31 (spectating) → CD-02</remarks>
    public static void RedirectToClassSelection(this Player player)
    {
        player.EnableClassSelection();
        player.ForceClassSelection();
        player.ToggleSpectating(true);
        player.ToggleSpectating(false);
    }
}
