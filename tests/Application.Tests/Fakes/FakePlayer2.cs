namespace CTF.Application.Tests.Fakes;

/// <summary>Test double for the platform Player surface.</summary>
/// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: the platform Player surface); CD-28 (NSubstitute mock contract) → CD-01</remarks>
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
