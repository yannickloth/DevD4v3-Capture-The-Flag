namespace CTF.Application.Commands.Moderator.WarnConnect;

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
