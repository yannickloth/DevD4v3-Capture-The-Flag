namespace CTF.Application.Combat.Connection;

/// <remarks>No injected services. Adds the weapon-selection component when a player connects.</remarks>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player)]
public class WeaponSelectionConnectSystem : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player)]
    public void OnPlayerConnect(Player player)
    {
        player.AddComponent<WeaponSelectionComponent>();
    }
}
