namespace CTF.Application.GameRules.ClassSelection.Middleware;

/// <summary>
/// Middleware executed before <c>OnPlayerRequestSpawn</c> to prevent players
/// from spawning when the required conditions are not met.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): entityManager -> CD-32; next -> CD-32; mapRotationService -> CD-12. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Ecs, ChangeDriver.Account, ChangeDriver.MapRotation)]
public class PlayerSpawnLockMiddleware(
    IEntityManager entityManager,
    EventDelegate next,
    MapRotationService mapRotationService)
{
    /// <summary>
    /// Invokes the middleware logic to block spawn requests from class selection
    /// when the required conditions are not met.
    /// </summary>
    /// <param name="context">Contains context information about the fired event.</param>
    /// <returns>
    /// <see langword="false"/> if spawning from class selection is blocked;
    /// otherwise, proceeds to the next middleware or action.
    /// </returns>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Account, ChangeDriver.MapRotation)]
    public object Invoke(EventContext context)
    {
        EntityId playerId = (EntityId)context.Arguments[0];
        Player player = entityManager.GetComponent<Player>(playerId);
        if (player.IsUnauthenticated())
        {
            player.SendClientMessage(Color.Red, Messages.LoginOrRegisterToContinue);
            return false;
        }

        if (mapRotationService.IsMapLoading)
        {
            player.SendClientMessage(Color.Red, Messages.MapIsLoading);
            return false;
        }

        Team selectedTeam = player.Team == (int)TeamId.Alpha ? Team.Alpha : Team.Beta;
        if (selectedTeam.IsFull())
        {
            string gameText = selectedTeam.GetAvailabilityMessage();
            player.GameText(gameText, TimeSpan.FromMilliseconds(999999999), GameTextStyle.Style3);
            return false;
        }

        return next(context);
    }
}
