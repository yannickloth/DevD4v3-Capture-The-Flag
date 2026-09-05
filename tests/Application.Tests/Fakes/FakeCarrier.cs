namespace CTF.Application.Tests.Fakes;

/// <summary>Test double for the platform Player surface.</summary>
[ChangeDriversAttribute(ChangeDriver.Player, ChangeDriver.NSubstitute)]
public class FakeCarrier : Player
{
    public FakeCarrier() : base(Substitute.For<IOmpEntityProvider>(), default)
    {
        
    }

    public override bool SetAttachedObject(
        int index,
        int modelId,
        Bone bone,
        Vector3 offset,
        Vector3 rotation,
        Vector3 scale,
        Color materialColor1,
        Color materialColor2) => true;

    [ChangeDriversAttribute(ChangeDriver.Player, ChangeDriver.NSubstitute)]
    public override bool RemoveAttachedObject(int index) => true;
}
