namespace CTF.Application.Players.GeneralCommands;

/// <summary>
/// Provides the VIP-role command set.
/// </summary>
/// <remarks>Change drivers: CD-15 (command set), CD-09 (authorization policy), CD-01 (open.mp/SampSharp platform API)</remarks>
public class VipCommands : ISystem
{
    /// <summary>Shows the VIP commands dialog.</summary>
    /// <remarks>Change drivers: CD-15 (command set), CD-09 (authorization policy), CD-01 (open.mp/SampSharp platform API)</remarks>
    [PlayerCommand("cmdsvip")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void ShowVipCommands(Player player, IDialogService dialogService)
    {
        var content = Smart.Format(DetailedCommandInfo.VIP, new 
        { 
            Color1 = Color.Yellow,
            Color2 = Color.White
        });

        var dialog = new MessageDialog(
            caption: "VIP Commands", 
            content, 
            button1: "Close"
        );

        dialogService.ShowAsync(player, dialog);
    }

    /// <summary>Gives the player a chainsaw.</summary>
    /// <remarks>Change drivers: CD-15 (command set), CD-09 (authorization policy), CD-01 (open.mp/SampSharp platform API)</remarks>
    [PlayerCommand("saw")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void Saw(Player player)
    {
        player.GiveWeapon(Weapon.Chainsaw, 1);
    }

    /// <summary>Gives the player a spray can.</summary>
    /// <remarks>Change drivers: CD-15 (command set), CD-09 (authorization policy), CD-01 (open.mp/SampSharp platform API)</remarks>
    [PlayerCommand("spray")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void Spray(Player player) 
    {
        player.GiveWeapon(Weapon.Spraycan, IWeapon.UnlimitedAmmo);
    }

    /// <summary>Gives the player tear gas.</summary>
    /// <remarks>Change drivers: CD-15 (command set), CD-09 (authorization policy), CD-01 (open.mp/SampSharp platform API)</remarks>
    [PlayerCommand("teargas")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void Teargas(Player player)
    {
        player.GiveWeapon(Weapon.Teargas, IWeapon.UnlimitedAmmo);
    }
}
