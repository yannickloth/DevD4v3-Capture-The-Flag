namespace CTF.Application.Players.Accounts.Authentication;

/// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-20 (outbound repository contract) → CD-08; CD-01 (open.mp/SampSharp platform API) → CD-08</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; authenticationDialog -> CD-29+CD-08. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class AccountSystem(
    IPlayerRepository playerRepository,
    AuthenticationDialog authenticationDialog) : ISystem
{
    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-20 (outbound repository contract) → CD-08; CD-01 (open.mp/SampSharp platform API) → CD-08</remarks>
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

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy)</remarks>
    private static PlayerInfo CreatePlayerInfo(string name)
    {
        var playerInfo = new PlayerInfo();
        playerInfo.SetName(name);
        return playerInfo;
    }
}
