namespace CTF.Application.Commands.ClientMessageNsNs6;

/// <summary>
/// Eliminates the player's character for respawn purposes, subject to a minimum-health rule.
/// </summary>
/// <remarks>No injected services.</remarks>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.ClientMessage)]
public class KillSystem : ISystem
{
    /// <summary>Eliminates the player's character for respawn purposes, subject to a minimum-health rule.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.ClientMessage)]
    [PlayerCommand("kill")]
    public void Kill(Player player)
    {
        const float minimumHealthToUseKillCommand = 15f;
        PlayerInfo playerInfo = player.GetRequiredInfo();

        if (playerInfo.Appearance.Team == Team.None)
        {
            player.SendClientMessage(Color.Red, Messages.NoTeam);
            return;
        }

        if (player.Health < minimumHealthToUseKillCommand)
        {
            player.SendClientMessage(Color.Red, Messages.NotEnoughHealth);
            return;
        }

        player.Health = 0;
    }
}
