namespace CTF.Application.Statistics;

/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: player score); CD-15 (command set) → CD-10; CD-09 (authorization policy) → CD-10; CD-01 (open.mp/SampSharp platform API) → CD-10</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): entityManager -> CD-01; worldService -> CD-01. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class PlayerScoreSystem(
    IEntityManager entityManager,
    IWorldService worldService) : ISystem
{
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: player score); CD-09 (authorization policy) → CD-10; CD-01 (open.mp/SampSharp platform API) → CD-10</remarks>
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

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: player score); CD-09 (authorization policy) → CD-10; CD-01 (open.mp/SampSharp platform API) → CD-10</remarks>
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

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: player score); CD-09 (authorization policy) → CD-10; CD-01 (open.mp/SampSharp platform API) → CD-10</remarks>
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
