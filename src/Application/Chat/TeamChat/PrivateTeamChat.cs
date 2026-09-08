namespace CTF.Application.Chat.ClientMessageNsNs5;

/// <summary>
/// Represents the team private chat tier, routed by the '!' prefix.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.ClientMessage)]
public class PrivateTeamChat : IChatMessage
{
    /// <summary>Gets the chat prefix identifier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Chat)]
    public char Id => '!';

    /// <summary>Sends the message to all players of the sender's team.</summary>
    [ChangeDriversAttribute(ChangeDriver.Chat)]
    public bool SendToAllPlayers(PlayerInfo sender, string message)
    {
        if (sender.Appearance.Team == Team.None)
            return false;

        Team currentTeam = sender.Appearance.Team;
        TeamMembers players = currentTeam.Members;
        foreach (Player player in players) 
        {
            player.SendClientMessage(currentTeam.ColorHex, $"[Team Chat] {sender.Account.Name}: {message}");
        }
        return true;
    }
}
