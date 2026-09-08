namespace CTF.Application.Commands.PlayerNsNs2;

/// <remarks>No injected services. Adds the warnings component when a player connects.</remarks>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Player)]
public class WarnConnectSystem : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Player)]
    public void OnPlayerConnect(Player player)
    {
        player.AddComponent<WarningsComponent>();
    }
}
