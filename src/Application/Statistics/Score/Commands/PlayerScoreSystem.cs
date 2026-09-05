namespace CTF.Application.Statistics.Score.Commands;

/// <remarks>Injected dependencies (change drivers of these elements): entityManager -> CD-32; worldService -> CD-36. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure)]
public class PlayerScoreSystem(
    IEntityManager entityManager,
    IWorldService worldService) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.Player, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
    [PlayerCommand("setscore")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void SetScoreToPlayer(
        Player currentPlayer, 
        [CommandParameter(Name = "playerId")]Player targetPlayer,
        int score)
    {
        if (score < 0)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.ValueCannotBeNegative);
            return;
        }

        targetPlayer.SetScore(score);

        {
            var message = Smart.Format(Messages.SetScoreToPlayer, new
            {
                PlayerName = targetPlayer.Name,
                Score = score
            });
            currentPlayer.SendClientMessage(Color.Yellow, message);
        }

        {
            var message = Smart.Format(Messages.ReceiveScoreFromPlayer, new
            {
                PlayerName = currentPlayer.Name,
                Score = score
            });
            targetPlayer.SendClientMessage(Color.Yellow, message);
        }
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.Player, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
    [PlayerCommand("addscore")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void AddScoreToPlayer(
        Player currentPlayer, 
        [CommandParameter(Name = "playerId")]Player targetPlayer,
        int score)
    {
        if (score < 0)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.ValueCannotBeNegative);
            return;
        }

        targetPlayer.AddScore(score);

        {
            var message = Smart.Format(Messages.AddScoreToPlayer, new
            {
                PlayerName = targetPlayer.Name,
                Score = score
            });
            currentPlayer.SendClientMessage(Color.Yellow, message);
        }

        {
            var message = Smart.Format(Messages.ReceiveScoreFromPlayer, new
            {
                PlayerName = currentPlayer.Name,
                Score = score
            });
            targetPlayer.SendClientMessage(Color.Yellow, message);
        }
    }

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
