namespace CTF.Application.Statistics.CommandSetNsNs3;

/// <summary>
/// Adds score to all connected players.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): entityManager -> CD-32; worldService -> CD-36. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
public class AddScoreToAllCommands(
    IEntityManager entityManager,
    IWorldService worldService) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
    [PlayerCommand("addallscore")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void AddScoreToAllPlayers(Player currentPlayer, int score)
    {
        if (score < 0)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.ValueCannotBeNegative);
            return;
        }

        IEnumerable<Player> players = entityManager.GetComponents<Player>();
        foreach (Player targetPlayer in players)
        {
            targetPlayer.AddScore(score);
        }

        var message = Smart.Format(Messages.AddScoreToAllPlayers, new
        {
            PlayerName = currentPlayer.Name,
            Score = score
        });

        worldService.SendClientMessage(Color.Yellow, message);
    }
}
