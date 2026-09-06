namespace CTF.Application.AntiCheat.SystemConnect;

/// <summary>Adds the last-fired-time component when a player connects.</summary>
[ChangeDriversAttribute(ChangeDriver.AntiCheat, ChangeDriver.Player)]
public class AntiCBugConnectSystem : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.AntiCheat, ChangeDriver.Player)]
    public void OnPlayerConnect(Player player)
    {
        player.AddComponent<LastFiredTimeComponent>();
    }
}
