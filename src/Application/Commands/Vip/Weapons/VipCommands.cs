namespace CTF.Application.Commands.PlayerNsNs3;

/// <summary>
/// Provides the VIP-role weapon command set.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player)]
public class VipCommands : ISystem
{
    /// <summary>Gives the player a chainsaw.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player)]
    [PlayerCommand("saw")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void Saw(Player player)
    {
        player.GiveWeapon(Weapon.Chainsaw, 1);
    }

    /// <summary>Gives the player a spray can.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player)]
    [PlayerCommand("spray")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void Spray(Player player) 
    {
        player.GiveWeapon(Weapon.Spraycan, IWeapon.UnlimitedAmmo);
    }

    /// <summary>Gives the player tear gas.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player)]
    [PlayerCommand("teargas")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void Teargas(Player player)
    {
        player.GiveWeapon(Weapon.Teargas, IWeapon.UnlimitedAmmo);
    }
}
