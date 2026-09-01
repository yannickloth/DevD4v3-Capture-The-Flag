namespace CTF.Application.Players.Accounts.Authentication;

/// <remarks>Change drivers: CD-08 (account & authentication policy), CD-01 (open.mp/SampSharp platform API)</remarks>
public class AccountComponent : Component
{
    /// <remarks>Change drivers: CD-08 (account & authentication policy)</remarks>
    public PlayerInfo PlayerInfo { get; }

    /// <remarks>Change drivers: CD-08 (account & authentication policy)</remarks>
    public bool IsAuthenticated { get; private set; }

    /// <remarks>Change drivers: CD-08 (account & authentication policy)</remarks>
    public bool IsUnauthenticated => !IsAuthenticated;

    /// <remarks>Change drivers: CD-08 (account & authentication policy)</remarks>
    public void Authenticate() => IsAuthenticated = true;

    /// <remarks>Change drivers: CD-08 (account & authentication policy)</remarks>
    public AccountComponent(PlayerInfo playerInfo, bool isAuthenticated)
    {
        ArgumentNullException.ThrowIfNull(playerInfo);
        PlayerInfo = playerInfo;
        IsAuthenticated = isAuthenticated;
    }

    /// <remarks>Change drivers: CD-08 (account & authentication policy)</remarks>
    public AccountComponent(PlayerInfo playerInfo) 
        : this(playerInfo, isAuthenticated: false)
    {
    }
}
