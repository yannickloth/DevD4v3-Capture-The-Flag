namespace CTF.Application.GameRules.ClientMessageNsNs3;

/// <summary>
/// Announces the result of a match to all players.
/// </summary>
/// <remarks>Injected dependencies: worldService -> CD-36. Driven by the IWorldService (platform) contract + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.GameText, ChangeDriver.ClientMessage)]
public class MatchResultAnnouncer(IWorldService worldService)
{
    /// <summary>Announces the match result.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.GameText, ChangeDriver.ClientMessage)]
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
