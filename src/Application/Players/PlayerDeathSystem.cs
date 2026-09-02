namespace CTF.Application.Players;

/// <summary>
/// Sends death messages to reflect player connect, disconnect, and death events.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification); CD-01 (open.mp/SampSharp platform API) → CD-02</remarks>
/// <remarks>Injected dependencies: worldService -> CD-01. Driven by the IWorldService (platform) contract + CD-21 (DI wiring).</remarks>
public class PlayerDeathSystem(IWorldService worldService) : ISystem
{
    /// <summary>Sends a death message when a player connects.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification); CD-01 (open.mp/SampSharp platform API) → CD-02</remarks>
    [Event]
    public void OnPlayerConnect(Player player)
    {
        worldService.SendDeathMessage(killer: null, player, Weapon.Connect);
    }

    /// <summary>Sends a death message when a player disconnects.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification); CD-01 (open.mp/SampSharp platform API) → CD-02</remarks>
    [Event]
    public void OnPlayerDisconnect(Player player, DisconnectReason reason) 
    {
        worldService.SendDeathMessage(killer: null, player, Weapon.Disconnect);
    }

    /// <summary>Sends a death message when a player dies.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification); CD-01 (open.mp/SampSharp platform API) → CD-02</remarks>
    [Event]
    public void OnPlayerDeath(Player victim, Player killer, Weapon reason)
    {
        worldService.SendDeathMessage(killer, victim, reason);
    }
}
