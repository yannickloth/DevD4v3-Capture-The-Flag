namespace CTF.Application.Commands.ClientMessageNsNs7;

/// <summary>
/// Enables spectator mode on a target player, subject to a minimum-health rule.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.ClientMessage)]
public class EnableSpectatorModeSystem : ISystem
{
    /// <summary>Enables spectator mode on a target player, subject to a minimum-health rule.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.ClientMessage)]
    [PlayerCommand("spec")]
    public void EnableSpectatorMode(
        Player currentPlayer,
        [CommandParameter(Name = "playerId")]Player targetPlayer,
        TeamTextDrawRenderer teamTextDrawRenderer)
    {
        const float minimumHealthToUseSpectatorCommand = 85f;

        if (currentPlayer == targetPlayer)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerIsEqualsToTargetPlayer);
            return;
        }

        if (targetPlayer.IsInClassSelection())
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerIsInClassSelection);
            return;
        }

        if (currentPlayer.GetRequiredInfo().Appearance.Team.RivalTeam.Flag.IsCarriedBy(currentPlayer))
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.HasCapturedFlag);
            return;
        }

        if (currentPlayer.Health < minimumHealthToUseSpectatorCommand)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.NotEnoughHealth);
            return;
        }

        Team removedTeam = currentPlayer.RemoveFromCurrentTeam();
        teamTextDrawRenderer.UpdateTeamMembers(removedTeam);
        currentPlayer.Interior = targetPlayer.Interior;
        currentPlayer.VirtualWorld = targetPlayer.VirtualWorld;
        currentPlayer.ToggleSpectating(true);
        currentPlayer.SpectatePlayer(targetPlayer);
        currentPlayer.SendClientMessage(Color.Yellow, Messages.ExitSpectatorMode);
    }
}
