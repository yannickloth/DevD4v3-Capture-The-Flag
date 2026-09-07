namespace CTF.Application.Combat.Connection;

/// <remarks>No injected services. Adds the health and armour cooldown components when a player connects.</remarks>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player)]
public class RestoreVitalityConnectSystem : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player)]
    public void OnPlayerConnect(Player player)
    {
        player.AddComponent<HealthCooldownComponent>();
        player.AddComponent<ArmourCooldownComponent>();
    }
}
