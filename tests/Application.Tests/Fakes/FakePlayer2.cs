namespace CTF.Application.Tests.Fakes;

/// <summary>Test double for the platform Player surface.</summary>
/// <remarks>Change drivers: CD-29 (root; code-under-test: the platform Player surface); CD-28 (NSubstitute mock contract) → CD-29; CD-01 (open.mp/SampSharp platform API) → CD-29</remarks>
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
