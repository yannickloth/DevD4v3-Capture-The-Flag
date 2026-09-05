namespace CTF.Application.Combat.WeaponSelection.Gamma_CD03_CD32;

/// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification: weapon selection); CD-32 (ECS component) → CD-03</remarks>
public class WeaponSelectionComponent : Component
{
    /// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification: weapon selection); CD-32 (ECS component) → CD-03</remarks>
    public WeaponPack SelectedWeapons { get; } = [];
}
