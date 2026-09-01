namespace CTF.Application.Tests.Fakes;

/// <summary>Test double for the platform Player surface (auth-aware).</summary>
/// <remarks>Change drivers: CD-28 (NSubstitute mock contract), CD-29 (code-under-test: the platform Player surface (auth-aware)), CD-01 (open.mp/SampSharp platform API)</remarks>
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
