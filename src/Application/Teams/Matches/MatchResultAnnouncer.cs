namespace CTF.Application.Teams.Matches;

/// <summary>
/// Announces the result of a match to all players.
/// </summary>
/// <remarks>Change drivers: CD-02 (CTF game-rules specification: match end conditions), CD-01 (open.mp/SampSharp platform API: client messages, GameText).</remarks>
public class MatchResultAnnouncer(IWorldService worldService)
{
    /// <summary>Announces the match result.</summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: match end conditions), CD-01 (open.mp/SampSharp platform API: client messages, GameText).</remarks>
    public void Announce()
    {
        MatchResult result = MatchResult.Create(Team.Alpha, Team.Beta);

        string resultMessage = result.IsTie ? 
            Messages.TiedTeams : 
            Smart.Format(
                Messages.TeamIsWinner,
                new { result.Winner.Name });

        string resultSummary = result.IsTie ?
            Messages.Tie :
            Smart.Format(
                Messages.Winner,
                new
                {
                    result.Winner.GameTextColor,
                    TeamName = result.Winner.Name
                });

        worldService.SendClientMessage(Color.Yellow, resultMessage);
        worldService.GameText(resultSummary, TimeSpan.FromSeconds(2), GameTextStyle.Style3);
    }
}
