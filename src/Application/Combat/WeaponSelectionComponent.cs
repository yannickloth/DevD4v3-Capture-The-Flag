namespace CTF.Application.Combat;

/// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification: weapon selection); CD-01 (open.mp/SampSharp platform API: ECS component) → CD-03</remarks>
public class WeaponSelectionComponent : Component
{
    /// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification: weapon selection); CD-01 (open.mp/SampSharp platform API: ECS component) → CD-03</remarks>
    public WeaponPack SelectedWeapons { get; } = [];
}
