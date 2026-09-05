namespace CTF.Application.Commands.Vip;

/// <summary>
/// Provides the VIP-role weapon command set.
/// </summary>
/// <remarks>Change drivers: CD-15 (root; command set); CD-09 (authorization policy) → CD-15; CD-31 (GiveWeapon) → CD-15</remarks>
public class VipCommands : ISystem
{
    /// <summary>Gives the player a chainsaw.</summary>
    /// <remarks>Change drivers: CD-15 (root; command set); CD-09 (authorization policy) → CD-15; CD-31 (GiveWeapon) → CD-15</remarks>
    [PlayerCommand("saw")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void Saw(Player player)
    {
        player.GiveWeapon(Weapon.Chainsaw, 1);
    }

    /// <summary>Gives the player a spray can.</summary>
    /// <remarks>Change drivers: CD-15 (root; command set); CD-09 (authorization policy) → CD-15; CD-31 (GiveWeapon) → CD-15</remarks>
    [PlayerCommand("spray")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void Spray(Player player) 
    {
        player.GiveWeapon(Weapon.Spraycan, IWeapon.UnlimitedAmmo);
    }

    /// <summary>Gives the player tear gas.</summary>
    /// <remarks>Change drivers: CD-15 (root; command set); CD-09 (authorization policy) → CD-15; CD-31 (GiveWeapon) → CD-15</remarks>
    [PlayerCommand("teargas")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void Teargas(Player player)
    {
        player.GiveWeapon(Weapon.Teargas, IWeapon.UnlimitedAmmo);
    }
}
