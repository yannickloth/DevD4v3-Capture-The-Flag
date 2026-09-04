namespace CTF.Application.CommandInfrastructure;

/// <summary>
/// Middleware executed before <c>OnPlayerCommandText</c> to prevent command execution
/// when the player does not meet the required conditions.
/// </summary>
/// <remarks>Change drivers: CD-43 (root; command infrastructure); CD-32 (ECS middleware); CD-08 (account & authentication policy) → CD-43; CD-02 (CTF game-rules specification) → CD-43; CD-12 (map-rotation rules: command lock during map load) → CD-43</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): entityManager -> CD-32; next -> CD-32; mapRotationService -> CD-12. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class PlayerCommandLockMiddleware(
    IEntityManager entityManager,
    EventDelegate next,
    MapRotationService mapRotationService)
{
    /// <summary>
    /// Invokes the middleware logic to lock player commands if certain conditions are met.
    /// </summary>
    /// <param name="context">Contains context information about the fired event.</param>
    /// <returns>
    /// <see langword="true"/> if any condition is met to block the command.
    /// Otherwise, it proceeds to the next middleware or action.
    /// </returns>
/// <remarks>Change drivers: CD-43 (root; command infrastructure); CD-32 (ECS middleware); CD-08 (account & authentication policy) → CD-43; CD-02 (CTF game-rules specification) → CD-43; CD-12 (map-rotation rules: command lock during map load) → CD-43</remarks>
    public object Invoke(EventContext context)
    {
        EntityId playerId = (EntityId)context.Arguments[0];
        Player player = entityManager.GetComponent<Player>(playerId);

        if (player.IsUnauthenticated())
        {
            player.SendClientMessage(Color.Red, Messages.LoginOrRegisterToContinue);
            return true;
        }

        if (player.IsInClassSelection())
        {
            player.SendClientMessage(Color.Red, Messages.CommandLockClassSelection);
            return true;
        }

        if (mapRotationService.IsMapLoading)
        {
            player.SendClientMessage(Color.Red, Messages.CommandLockMapLoading);
            return true;
        }

        return next(context);
    }
}
