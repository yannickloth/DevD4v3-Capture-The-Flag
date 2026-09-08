namespace CTF.Application.Authorization.CommandSetNsNs2;

/// <summary>
/// Sets the role of a target player.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.GameText, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure, ChangeDriver.CommandSet)]
public class SetRoleCommands(
    IPlayerRepository playerRepository) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.GameText, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure, ChangeDriver.CommandSet)]
    [PlayerCommand("setrole")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void SetRole(
        Player currentPlayer, 
        [CommandParameter(Name = "playerId")]Player targetPlayer, 
        int roleId)
    {
        if (currentPlayer == targetPlayer)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerIsEqualsToTargetPlayer);
            return;
        }

        if (targetPlayer.IsUnauthenticated())
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.UnauthenticatedPlayer);
            return;
        }

        if (targetPlayer.IsServerOwner())
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.CannotPerformActionOnServerOwner);
            return;
        }

        PlayerInfo targetPlayerInfo = targetPlayer.GetRequiredInfo();
        RoleId newRoleId = (RoleId)roleId;
        RoleId oldRoleId = targetPlayerInfo.Role.Id;
        Result result = targetPlayerInfo.Role.Set(newRoleId);
        if (result.IsFailed)
        {
            currentPlayer.SendClientMessage(Color.Red, result.Message);
            return;
        }

        if (oldRoleId == newRoleId)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerAlreadyHasThatRole);
            return;
        }

        var gameText = newRoleId > oldRoleId ?
            Smart.Format(Messages.PromotedToRole, new { RoleName = newRoleId }) :
            Smart.Format(Messages.DemotedToRole,  new { RoleName = newRoleId });

        var message = Smart.Format(Messages.RoleSuccessfullyChanged, new
        {
            RoleName = newRoleId,
            PlayerName = targetPlayer.Name
        });

        playerRepository.UpdateRole(targetPlayerInfo);
        targetPlayer.GameText(gameText, TimeSpan.FromSeconds(4), GameTextStyle.Style3);
        currentPlayer.SendClientMessage(Color.Yellow, message);
    }
}
