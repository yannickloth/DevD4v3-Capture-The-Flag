namespace CTF.Application.GameRules.ModelDomain;

/// <summary>
/// Adds player classes for team skins on game mode init.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Model)]
public class ClassSelectionInitSystem : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Model)]
    [Event]
    public void OnGameModeInit(IServerService serverService)
    {
        serverService.AddPlayerClass((int)Team.Alpha.SkinId, new Vector3(0f, 0f, 0f), 0);
        serverService.AddPlayerClass((int)Team.Beta.SkinId, new Vector3(0f, 0f, 0f), 0);
    }
}
