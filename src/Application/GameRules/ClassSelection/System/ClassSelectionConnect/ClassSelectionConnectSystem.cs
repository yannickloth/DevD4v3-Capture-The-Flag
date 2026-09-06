using CTF.Application.GameRules.ClassSelection.Components;

namespace CTF.Application.GameRules.ClassSelection.System.ClassSelectionConnect;

/// <summary>
/// Handles player connect for the class-selection flow.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): classSelectionTextDrawRenderer -> CD-34; classSelectionSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.Audio, ChangeDriver.Player)]
public class ClassSelectionConnectSystem(
    ClassSelectionTextDrawRenderer classSelectionTextDrawRenderer,
    ClassSelectionSettings classSelectionSettings) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.Audio, ChangeDriver.Player)]
    [Event]
    public void OnPlayerConnect(Player player)
    {
        player.Color = Team.None.ColorHex;
        player.AddComponent<ClassSelectionComponent>();
        player.RemoveAttachedObject(0);
        player.PlayAudioStream(classSelectionSettings.IntroAudioUrl);
        classSelectionTextDrawRenderer.Show(player);
    }
}
