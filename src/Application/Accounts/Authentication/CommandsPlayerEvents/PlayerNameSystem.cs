namespace CTF.Application.Accounts.Authentication.CommandsPlayerEvents;

/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; worldService -> CD-36. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.CommandSet, ChangeDriver.Player, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure, ChangeDriver.Repository)]
public class PlayerNameSystem(
    IPlayerRepository playerRepository,
    IWorldService worldService) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Player, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure, ChangeDriver.Repository, ChangeDriver.CommandSet)]
    [PlayerCommand("changename")]
    public void ChangeName(Player player, string newName)
    {
        if (playerRepository.Exists(newName))
        {
            player.SendClientMessage(Color.Red, Messages.PlayerNameAlreadyExists);
            return;
        }

        PlayerInfo playerInfo = player.GetRequiredInfo();
        string oldName = playerInfo.Account.Name;
        Result result = playerInfo.Account.SetName(newName);
        if (result.IsFailed)
        {
            player.SendClientMessage(Color.Red, result.Message);
            return;
        }

        var message = Smart.Format(Messages.NameSuccessfullyChanged, new { OldName = oldName, NewName = newName });
        worldService.SendClientMessage(Color.Yellow, message);
        player.SetName(newName);
        playerRepository.UpdateName(playerInfo);
    }
}
