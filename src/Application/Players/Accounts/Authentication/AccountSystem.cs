namespace CTF.Application.Players.Accounts.Authentication;

/// <remarks>Change drivers: CD-08 (account & authentication policy), CD-01 (open.mp/SampSharp platform API), CD-20 (outbound repository contract)</remarks>
public class AccountSystem(
    IPlayerRepository playerRepository,
    AuthenticationDialog authenticationDialog) : ISystem
{
    /// <remarks>Change drivers: CD-08 (account & authentication policy), CD-01 (open.mp/SampSharp platform API), CD-20 (outbound repository contract)</remarks>
    [Event]
    public async Task OnPlayerConnect(Player player)
    {
        PlayerInfo playerInfo = playerRepository.GetOrDefault(player.Name);

        if (playerInfo is null)
        {
            playerInfo = CreatePlayerInfo(player.Name);
            player.AddComponent<AccountComponent>(playerInfo);
            await authenticationDialog.ShowSignup(player);
            return;
        }

        player.AddComponent<AccountComponent>(playerInfo);
        await authenticationDialog.ShowLogin(player);
    }

    /// <remarks>Change drivers: CD-08 (account & authentication policy)</remarks>
    private static PlayerInfo CreatePlayerInfo(string name)
    {
        var playerInfo = new PlayerInfo();
        playerInfo.SetName(name);
        return playerInfo;
    }
}
