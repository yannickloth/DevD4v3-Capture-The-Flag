namespace CTF.Application.Tests.Fakes;

/// <summary>Test double for the platform Player surface.</summary>
/// <remarks>Change drivers: CD-31 (root; player entity surface); CD-28 (NSubstitute mock contract) → CD-31</remarks>
public class FakePlayer2 : Player
{
    public FakePlayer2() : base(Substitute.For<IOmpEntityProvider>(), default)
    {
    }

    public override T GetComponent<T>()
    {
        return null;
    }
}
