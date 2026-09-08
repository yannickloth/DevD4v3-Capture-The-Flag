namespace CTF.Application.GameRules.PlayerNsNs2;

/// <summary>
/// Sends death messages to reflect player connect, disconnect, and death events.
/// </summary>
/// <remarks>Injected dependencies: worldService -> CD-36. Driven by the IWorldService (platform) contract + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
public class PlayerDeathSystem(IWorldService worldService) : ISystem
{
    /// <summary>Sends a death message when a player connects.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    [Event]
    public void OnPlayerConnect(Player player)
    {
        worldService.SendDeathMessage(killer: null, player, Weapon.Connect);
    }

    /// <summary>Sends a death message when a player disconnects.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    [Event]
    public void OnPlayerDisconnect(Player player, DisconnectReason reason) 
    {
        worldService.SendDeathMessage(killer: null, player, Weapon.Disconnect);
    }

    /// <summary>Sends a death message when a player dies.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    [Event]
    public void OnPlayerDeath(Player victim, Player killer, Weapon reason)
    {
        worldService.SendDeathMessage(killer, victim, reason);
    }
}
