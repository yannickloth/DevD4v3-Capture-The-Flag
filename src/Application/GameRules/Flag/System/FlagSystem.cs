namespace CTF.Application.GameRules.Flag.System;

/// <summary>
/// Handles flag-related events such as disconnect, death, team change, pickup, and the return command.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; flagEvents (FrozenDictionary&lt;FlagStatus, IFlagEvent&gt;) -> CD-02; teamPickupService -> CD-37; flagAutoReturnTimer -> CD-02; playerStatsRenderer -> CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Pickup, ChangeDriver.Player, ChangeDriver.Combat, ChangeDriver.Statistics, ChangeDriver.CommandSet, ChangeDriver.Authorization)]
public class FlagSystem(
    IWorldService worldService,
    FrozenDictionary<FlagStatus, IFlagEvent> flagEvents,
    TeamPickupService teamPickupService,
    FlagAutoReturnTimer flagAutoReturnTimer,
    PlayerStatsRenderer playerStatsRenderer) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Coin)]
    private const int CarrierKillEarnedCoins  = 4;

    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Combat)]
    private const int CarrierKillEarnedHealth = 10;

    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Statistics)]
    private const int CarrierKillEarnedScore  = 2;

    /// <summary>Handles flag drop when a carrying player disconnects.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    [Event]
    public void OnPlayerDisconnect(Player player, DisconnectReason reason)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        if (playerInfo.Appearance.Team.RivalTeam.Flag.IsCarriedBy(player))
        {
            Team currentTeam = playerInfo.Appearance.Team;
            IFlagEvent flagDropped = flagEvents[FlagStatus.Dropped];
            flagDropped.Handle(currentTeam.RivalTeam, player);
        }
    }

    /// <summary>Handles flag drop and rewards when a carrying player dies.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Combat, ChangeDriver.Coin, ChangeDriver.Statistics)]
    [Event]
    public void OnPlayerDeath(Player victim, Player killer, Weapon reason)
    {
        PlayerInfo victimInfo = victim.GetRequiredInfo();
        if (victimInfo.Appearance.Team.RivalTeam.Flag.IsCarriedBy(victim))
        {
            Team currentTeam = victimInfo.Appearance.Team;
            IFlagEvent flagDropped = flagEvents[FlagStatus.Dropped];
            flagDropped.Handle(currentTeam.RivalTeam, victim);
            if (killer is not null)
            {
                PlayerInfo killerInfo = killer.GetRequiredInfo();
                killerInfo.Stats.PerRound.AddCoins(CarrierKillEarnedCoins);
                killer.AddHealth(CarrierKillEarnedHealth);
                killer.AddScore(CarrierKillEarnedScore);
                playerStatsRenderer.UpdateTextDraw(killer);
            }
        }
    }

    /// <summary>Drops the flag when a carrying player changes teams.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    [Event]
    public void OnTeamChange(Player player, Team selectedTeam)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();

        if (!playerInfo.Appearance.Team.RivalTeam.Flag.IsCarriedBy(player))
            return;

        IFlagEvent flagDropped = flagEvents[FlagStatus.Dropped];
        flagDropped.Handle(selectedTeam, player);
    }

    /// <summary>Handles flag pickup interactions.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Pickup, ChangeDriver.Player)]
    [Event]
    public void OnPlayerPickUpPickup(Player player, Pickup pickup)
    {
        if (pickup.Model == (int)FlagModel.Red)
        {
            FlagStatus flagStatus = Team.Alpha.HandleFlagInteraction(flagPicker: player);
            IFlagEvent flagEvent = flagEvents[flagStatus];
            flagEvent.Handle(Team.Alpha, player);
        }
        else if (pickup.Model == (int)FlagModel.Blue)
        {
            FlagStatus flagStatus = Team.Beta.HandleFlagInteraction(flagPicker: player);
            IFlagEvent flagEvent = flagEvents[flagStatus];
            flagEvent.Handle(Team.Beta, player);
        }
        else if (pickup.Model == (int)ExteriorMarker.Red)
        {
            if (player.Team == (int)TeamId.Alpha)
                player.GameText(Messages.RedFlagIsNotAtBasePosition, TimeSpan.FromSeconds(5), GameTextStyle.Style3);
        }
        else if (pickup.Model == (int)ExteriorMarker.Blue)
        {
            if (player.Team == (int)TeamId.Beta)
                player.GameText(Messages.BlueFlagIsNotAtBasePosition, TimeSpan.FromSeconds(5), GameTextStyle.Style3);
        }
    }

    /// <summary>Returns a flag to its base position via the returnflag command.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Timer)]
    [PlayerCommand("returnflag")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void ReturnToBasePosition(
        Player player,
        [CommandParameter(Name = "red/blue")]string color)
    {
        Team team = color.ToLower() switch
        {
            "red" => Team.Alpha,
            "blue" => Team.Beta,
            _ => null
        };

        if (team is null)
        {
            player.SendClientMessage(Color.Red, Messages.InvalidFlagColor);
            return;
        }

        var message = Smart.Format(Messages.ReturnFlagToBasePosition, new
        {
            PlayerName = player.Name,
            team.ColorName
        });

        team.Flag.Carrier?.Player.HideOnRadarMap();
        team.Flag.ReturnToBase();
        teamPickupService.CreateFlagFromBasePosition(team);
        teamPickupService.DestroyExteriorMarker(team);
        team.Sounds.PlayFlagReturnedSound();
        flagAutoReturnTimer.Stop(team);
        worldService.GameText($"~n~~n~~n~{team.GameTextColor}{team.ColorName} flag returned!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);
        worldService.SendClientMessage(Color.Yellow, message);
    }
}
