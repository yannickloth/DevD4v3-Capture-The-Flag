namespace CTF.Application.Tests.Players;

/// <summary>Test double for the platform Player surface.</summary>
[ChangeDriversAttribute(ChangeDriver.Player, ChangeDriver.NSubstitute)]
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
