namespace CTF.Application.Tests.Fakes;

/// <summary>Test double for the platform Player surface (auth-aware).</summary>
/// <remarks>Change drivers: CD-31 (root; player entity surface, auth-aware); CD-28 (NSubstitute mock contract) → CD-31</remarks>
public class FakePlayer3 : Player
{
    public FakePlayer3() : base(Substitute.For<IOmpEntityProvider>(), default)
    {
    }

    public bool IsAuthenticated { get; set; } = true;

    public override T GetComponent<T>()
    {
        var accountComponent = new AccountComponent(new PlayerInfo(), IsAuthenticated);
        return accountComponent as T;
    }
}
