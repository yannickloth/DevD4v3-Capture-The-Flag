using CTF.Application.Statistics.TeamStats.Scoreboard.ScoreboardCommands;

namespace CTF.Application.Statistics.TeamStats.Scoreboard.ScoreboardKeyTrigger;

/// <summary>
/// Dispatches the team-scoreboard dialog command from a key press.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): scoreboardCommands -> CD-21 (DI wiring). Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Player)]
public class ScoreboardKeyTriggerSystem(
    TeamScoreboardCommands scoreboardCommands) : ISystem
{
    /// <summary>Shows the scoreboard when the player presses the No key.</summary>
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Player)]
    public void OnPlayerKeyStateChange(Player player, Keys newKeys, Keys oldKeys)
    {
        if (KeyUtils.HasPressed(newKeys, oldKeys, Keys.No))
        {
            scoreboardCommands.ShowPlayers(player);
        }
    }
}
