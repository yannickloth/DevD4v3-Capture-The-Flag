namespace CTF.Application.Players.Accounts.Profile;

/// <remarks>Change drivers: CD-08 (account & authentication policy), CD-20 (outbound repository contract), CD-01 (open.mp/SampSharp platform API)</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; worldService -> CD-01. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class PlayerNameSystem(
    IPlayerRepository playerRepository,
    IWorldService worldService) : ISystem
{
    /// <remarks>Change drivers: CD-08 (account & authentication policy), CD-20 (outbound repository contract), CD-01 (open.mp/SampSharp platform API)</remarks>
    [PlayerCommand("changename")]
    public void ChangeName(Player player, string newName)
    {
        if (playerRepository.Exists(newName))
        {
            player.SendClientMessage(Color.Red, Messages.PlayerNameAlreadyExists);
            return;
        }

        PlayerInfo playerInfo = player.GetRequiredInfo();
        string oldName = playerInfo.Name;
        Result result = playerInfo.SetName(newName);
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
