namespace CTF.Application.Statistics.Score.Commands.SetScoreCommands;

/// <summary>
/// Sets or adds score to a single target player.
/// </summary>
/// <remarks>No injected dependencies.</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.Player, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
public class SetScoreCommands : ISystem
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
}
