namespace CTF.Application.Accounts.Authentication.ECS;

[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Ecs)]
public class AccountComponent : Component
{
    [ChangeDriversAttribute(ChangeDriver.Account)]
    public PlayerInfo PlayerInfo { get; }

    [ChangeDriversAttribute(ChangeDriver.Account)]
    public bool IsAuthenticated { get; private set; }

    [ChangeDriversAttribute(ChangeDriver.Account)]
    public bool IsUnauthenticated => !IsAuthenticated;

    [ChangeDriversAttribute(ChangeDriver.Account)]
    public void Authenticate() => IsAuthenticated = true;

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy)</remarks>
    public AccountComponent(PlayerInfo playerInfo, bool isAuthenticated)
    {
        ArgumentNullException.ThrowIfNull(playerInfo);
        PlayerInfo = playerInfo;
        IsAuthenticated = isAuthenticated;
    }

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy)</remarks>
    public AccountComponent(PlayerInfo playerInfo) 
        : this(playerInfo, isAuthenticated: false)
    {
    }
}
