namespace CTF.Application.Commands.Moderation;

/// <summary>
/// Gives a jetpack to all connected players.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): entityManager -> CD-32; worldService -> CD-36. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
public class JetpackCommands(
    IEntityManager entityManager,
    IWorldService worldService) : ISystem
{
    /// <summary>Gives a jetpack to all connected players.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
    [PlayerCommand("jetall")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void GiveJetpackToPlayers(Player currentPlayer)
    {
        var players = entityManager.GetComponents<Player>();

        foreach (Player player in players)
        {
            player.SpecialAction = SpecialAction.UseJetpack;
        }

        var message = Smart.Format(Messages.GiveJetpackToPlayers, new 
        { 
            PlayerName = currentPlayer.Name 
        });

        worldService.SendClientMessage(Color.Yellow, message);
    }
}
