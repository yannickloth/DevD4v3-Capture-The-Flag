namespace CTF.Application.GameRules.ClassSelection.System.ClassSelectionCommand;

/// <summary>
/// Redirects the player to class selection via the class command.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): teamTextDrawRenderer -> CD-34. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.CommandSet, ChangeDriver.Combat)]
public class ClassSelectionCommandSystem(
    TeamTextDrawRenderer teamTextDrawRenderer) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.GameRules, ChangeDriver.Combat)]
    private const float MinimumHealthToUseClassSelectionCommand = 85f;

    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.CommandSet, ChangeDriver.Combat)]
    [PlayerCommand("class")]
    public void RedirectToClassSelection(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        if (playerInfo.Appearance.Team.RivalTeam.Flag.IsCarriedBy(player))
        {
            player.SendClientMessage(Color.Red, Messages.HasCapturedFlag);
            return;
        }

        if (player.Health < MinimumHealthToUseClassSelectionCommand)
        {
            player.SendClientMessage(Color.Red, Messages.NotEnoughHealth);
            return;
        }

        Team removedTeam = player.RemoveFromCurrentTeam();
        teamTextDrawRenderer.UpdateTeamMembers(removedTeam);
        player.RedirectToClassSelection();
    }
}
