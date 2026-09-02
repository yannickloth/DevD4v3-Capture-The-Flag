namespace CTF.Application.Players.Chats.Definitions;

/// <summary>
/// Represents the team private chat tier, routed by the '!' prefix.
/// </summary>
/// <remarks>Change drivers: CD-13 (root; chat rules); CD-01 (open.mp/SampSharp platform API) → CD-13</remarks>
public class PrivateTeamChat : IChatMessage
{
    /// <summary>Gets the chat prefix identifier.</summary>
    /// <remarks>Change drivers: CD-13 (root; root; chat rules)</remarks>
    public char Id => '!';

    /// <summary>Sends the message to all players of the sender's team.</summary>
    /// <remarks>Change drivers: CD-13 (root; chat rules); CD-01 (open.mp/SampSharp platform API) → CD-13</remarks>
    public bool SendToAllPlayers(PlayerInfo sender, string message)
    {
        if (sender.Team == Team.None)
            return false;

        Team currentTeam = sender.Team;
        TeamMembers players = currentTeam.Members;
        foreach (Player player in players) 
        {
            player.SendClientMessage(currentTeam.ColorHex, $"[Team Chat] {sender.Name}: {message}");
        }
        return true;
    }
}
