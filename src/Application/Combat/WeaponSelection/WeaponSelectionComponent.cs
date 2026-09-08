namespace CTF.Application.Combat.EcsDomain;

[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Ecs)]
public class WeaponSelectionComponent : Component
{
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Ecs)]
    public WeaponPack SelectedWeapons { get; } = [];
}
