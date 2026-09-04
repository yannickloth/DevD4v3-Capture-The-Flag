namespace CTF.Application.AntiCheat;

/// <summary>
/// Prevents players from abusing the GTA: San Andreas crouch bug (C-Bug).
/// </summary>
/// <remarks>Change drivers: CD-14 (root; anti-cheat policy); CD-17 (game configuration/.env schema) → CD-14; CD-31 (player events); CD-32 (ECS runtime); CD-35 (GameText API) → CD-14</remarks>
/// <remarks>
/// C-Bug is a bug in GTA: San Andreas that allows players to manipulate the
/// reload animation of certain weapons, particularly the Desert Eagle, to fire
/// much faster than the game's normal mechanics would allow.
/// </remarks>
/// <remarks>Injected dependencies (change drivers of these elements): unixTimeSeconds -> CD-41; antiCBugSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class AntiCBugSystem(
    UnixTimeSeconds unixTimeSeconds,
    AntiCBugSettings antiCBugSettings) : ISystem
{
    /// <summary>Adds the last-fired-time component when a player connects.</summary>
    /// <remarks>Change drivers: CD-14 (root; anti-cheat policy); CD-31 (OnPlayerConnect) → CD-14</remarks>
    [Event]
    public void OnPlayerConnect(Player player)
    {
        player.AddComponent<LastFiredTimeComponent>();
    }

    /// <summary>Detects the C-Bug on key state changes.</summary>
    /// <remarks>Change drivers: CD-14 (root; anti-cheat policy); CD-17 (game configuration/.env schema) → CD-14; CD-31 (OnPlayerKeyStateChange); CD-32 (ECS runtime); CD-35 (GameText API) → CD-14</remarks>
    [Event]
    public void OnPlayerKeyStateChange(Player player, Keys newKeys, Keys oldKeys)
    {
        if (antiCBugSettings.Disabled)
            return;

        if (player.State != PlayerState.OnFoot)
            return;

        var lastFiredTimeComponent = player.GetComponent<LastFiredTimeComponent>();
        if (player.SpecialAction != SpecialAction.Duck && 
            KeyUtils.HasPressed(newKeys, oldKeys, Keys.Fire))
        {
            lastFiredTimeComponent.Value = player.Weapon switch
            {
                Weapon.Deagle => unixTimeSeconds.Value,
                _ => default
            };
        }
        else if (KeyUtils.HasPressed(newKeys, oldKeys, Keys.Crouch))
        {
            long currentTime = unixTimeSeconds.Value;
            long elapsedTime = currentTime - lastFiredTimeComponent.Value;
            if (elapsedTime < 1)
            {
                player.GameText("~r~~h~DON'T C-BUG!", TimeSpan.FromSeconds(3), GameTextStyle.Style4);
                player.ApplyAnimation(
                    animationLibrary: "PED", 
                    animationName: "getup", 
                    fDelta: 4.1f, 
                    loop: false, 
                    lockX: false, 
                    lockY: false, 
                    freeze: false, 
                    time: TimeSpan.FromSeconds(0),
                    syncType: PlayerAnimationSyncType.NoSync
                );
            }
        }
    }
}
