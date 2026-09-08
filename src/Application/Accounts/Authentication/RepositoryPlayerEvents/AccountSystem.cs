namespace CTF.Application.Accounts.EcsNsNs2;

/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; authenticationDialog -> CD-08. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs)]
public class AccountSystem(
    IPlayerRepository playerRepository,
    AuthenticationDialog authenticationDialog) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs)]
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

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs)]
    private static PlayerInfo CreatePlayerInfo(string name)
    {
        var playerInfo = new PlayerInfo();
        playerInfo.Account.SetName(name);
        return playerInfo;
    }
}
