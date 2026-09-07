namespace CTF.Application.GunGames.Rewards;

/// <summary>
/// Grants the GunGame victory rewards to the winner and their teammates.
/// This is a uniform multi-domain composite (CD-07 GunGame mode + the engaged CD-06 coin,
/// CD-10 statistics, CD-31 player, CD-36 client-message contracts): the GunGame-mode rules
/// own the orchestration; the engaged contracts are internal, stable seams. Documented in
/// <c>IVP/constraints.md</c> Appendix A as an intentional multi-domain composite.
/// </summary>
/// <remarks>Injected dependencies: playerStatsRenderer -> CD-10. Driven by the PlayerStatsRenderer contract + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.ClientMessage)]
public class GunGameRewardGrant(PlayerStatsRenderer playerStatsRenderer)
{
    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.ClientMessage)]
    public void Give(Player winner)
    {
        var weaponReward = GunGameRewardTable.WeaponRewards[Random.Shared.Next(GunGameRewardTable.WeaponRewards.Length)];
        var winnerRewardSummary = Smart.Format(GunGameMessages.WinnerRewardSummary, new
        {
            Health = GunGameRewardTable.WinnerEarnedHealth,
            Armour = GunGameRewardTable.WinnerEarnedArmour,
            Coins  = GunGameRewardTable.WinnerEarnedCoins,
            Weapon = weaponReward.Weapon.Name
        });
        PlayerInfo winnerInfo = winner.GetRequiredInfo();
        winner.AddHealth(GunGameRewardTable.WinnerEarnedHealth);
        winner.AddArmour(GunGameRewardTable.WinnerEarnedArmour);
        winner.GiveWeapon(weaponReward.Weapon.Id, weaponReward.Ammo);

        // Although the detonator is automatically available when Satchel Charges
        // are given, its HUD icon is not displayed unless it is explicitly granted.
        if (weaponReward.Weapon.Id == Weapon.SatchelCharge)
            winner.GiveWeapon(Weapon.Detonator, 1);

        winnerInfo.Coins.AddCoins(GunGameRewardTable.WinnerEarnedCoins);
        playerStatsRenderer.UpdateTextDraw(winner);

        winner.SendClientMessage(Color.Yellow, GunGameMessages.WinnerRewardGranted);
        winner.SendClientMessage(Color.Yellow, winnerRewardSummary);

        var teamRewardGranted = Smart.Format(GunGameMessages.TeamRewardGranted, new
        {
            Team = winnerInfo.Appearance.Team.Name,
            Killer = winner.Name
        });

        var teamRewardSummary = Smart.Format(GunGameMessages.TeamRewardSummary, new
        {
            Team   = winnerInfo.Appearance.Team.Name,
            Health = GunGameRewardTable.TeamEarnedHealth,
            Armour = GunGameRewardTable.TeamEarnedArmour,
            Coins  = GunGameRewardTable.TeamEarnedCoins,
            Score  = GunGameRewardTable.TeamEarnedScore
        });

        foreach (Player teammate in winnerInfo.Appearance.Team.Members)
        {
            if (teammate == winner)
                continue;

            PlayerInfo teammateInfo = teammate.GetRequiredInfo();
            teammate.AddHealth(GunGameRewardTable.TeamEarnedHealth);
            teammate.AddArmour(GunGameRewardTable.TeamEarnedArmour);
            teammate.AddScore(GunGameRewardTable.TeamEarnedScore);
            teammateInfo.Coins.AddCoins(GunGameRewardTable.TeamEarnedCoins);
            playerStatsRenderer.UpdateTextDraw(teammate);

            teammate.SendClientMessage(Color.LightGreen, teamRewardGranted);
            teammate.SendClientMessage(Color.LightGreen, teamRewardSummary);
        }
    }
}
