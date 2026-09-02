namespace CTF.Application.Tests.Fakes;

/// <summary>Test double for the platform Player surface.</summary>
/// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: the platform Player surface); CD-28 (NSubstitute mock contract) → CD-01</remarks>
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

    public override bool RemoveAttachedObject(int index) => true;
}
