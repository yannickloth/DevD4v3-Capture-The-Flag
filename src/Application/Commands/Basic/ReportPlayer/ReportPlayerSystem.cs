namespace CTF.Application.Commands.ClientMessageNsNs5;

/// <summary>
/// Reports a target player to the moderators/admins.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): entityManager -> CD-32. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
public class ReportPlayerSystem(IEntityManager entityManager) : ISystem
{
    /// <summary>Reports a target player to the moderators/admins.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
    [PlayerCommand("report")]
    public void ReportPlayer(
        Player currentPlayer,
        [CommandParameter(Name = "playerId")]Player targetPlayer,
        string reason)
    {
        if (currentPlayer == targetPlayer)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerIsEqualsToTargetPlayer);
            return;
        }

        IEnumerable<Player> admins = entityManager
            .GetComponents<Player>()
            .Where(player => player.GetRequiredInfo().Role.Id >= RoleId.Moderator);

        if (!admins.Any())
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.NoAdminsConnected);
            return;
        }

        var message = Smart.Format(Messages.ReportToAnotherPlayer, new
        {
            CurrentPlayer = currentPlayer.Name,
            TargetPlayer = targetPlayer.Name,
            Reason = reason
        });

        foreach (Player admin in admins)
        {
            admin.SendClientMessage(Color.Red, message);
        }

        currentPlayer.SendClientMessage(Color.Yellow, Messages.ReportSuccessfullySent);
        currentPlayer.PlaySound(1058);
    }
}
