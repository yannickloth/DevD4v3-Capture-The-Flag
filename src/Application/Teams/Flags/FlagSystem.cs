namespace CTF.Application.Teams.Flags;

/// <summary>
/// Handles flag-related events such as disconnect, death, team change, pickup, and the return command.
/// </summary>
/// <remarks>Change drivers: CD-09 (authorization policy: moderator gating); CD-02 (CTF game-rules specification: flag steal/capture/drop/return rules); CD-15 (command set: returnflag command); CD-03 (combat/weapon-rules specification: carrier-kill rewards); CD-01 (open.mp/SampSharp platform API: player events, pickups)</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-01; flagEvents (FrozenDictionary&lt;FlagStatus, IFlagEvent&gt;) -> CD-02; teamPickupService -> CD-29+CD-01; flagAutoReturnTimer -> CD-29+CD-02; playerStatsRenderer -> CD-29+CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class FlagSystem(
    IWorldService worldService,
    FrozenDictionary<FlagStatus, IFlagEvent> flagEvents,
    TeamPickupService teamPickupService,
    FlagAutoReturnTimer flagAutoReturnTimer,
    PlayerStatsRenderer playerStatsRenderer) : ISystem
{
    private const int CarrierKillEarnedCoins  = 4;
    private const int CarrierKillEarnedHealth = 10;
    private const int CarrierKillEarnedScore  = 2;

    /// <summary>Handles flag drop when a carrying player disconnects.</summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: carrier-disconnect drop rule); CD-01 (open.mp/SampSharp platform API: OnPlayerDisconnect)</remarks>
    [Event]
    public void OnPlayerDisconnect(Player player, DisconnectReason reason)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        if (playerInfo.IsCarryingEnemyFlag())
        {
            Team currentTeam = playerInfo.Team;
            IFlagEvent flagDropped = flagEvents[FlagStatus.Dropped];
            flagDropped.Handle(currentTeam.RivalTeam, player);
        }
    }

    /// <summary>Handles flag drop and rewards when a carrying player dies.</summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: carrier-death drop rule); CD-06 (coin economy: coins-on-kill); CD-03 (combat/weapon-rules specification: carrier-kill rewards); CD-01 (open.mp/SampSharp platform API: OnPlayerDeath)</remarks>
    [Event]
    public void OnPlayerDeath(Player victim, Player killer, Weapon reason)
    {
        PlayerInfo victimInfo = victim.GetRequiredInfo();
        if (victimInfo.IsCarryingEnemyFlag())
        {
            Team currentTeam = victimInfo.Team;
            IFlagEvent flagDropped = flagEvents[FlagStatus.Dropped];
            flagDropped.Handle(currentTeam.RivalTeam, victim);
            if (killer is not null)
            {
                PlayerInfo killerInfo = killer.GetRequiredInfo();
                killerInfo.StatsPerRound.AddCoins(CarrierKillEarnedCoins);
                killer.AddHealth(CarrierKillEarnedHealth);
                killer.AddScore(CarrierKillEarnedScore);
                playerStatsRenderer.UpdateTextDraw(killer);
            }
        }
    }

    /// <summary>Drops the flag when a carrying player changes teams.</summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: carrier team-change drop rule)</remarks>
    [Event]
    public void OnTeamChange(Player player, Team selectedTeam)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();

        if (!playerInfo.IsCarryingEnemyFlag())
            return;

        IFlagEvent flagDropped = flagEvents[FlagStatus.Dropped];
        flagDropped.Handle(selectedTeam, player);
    }

    /// <summary>Handles flag pickup interactions.</summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag steal/capture/return rules); CD-01 (open.mp/SampSharp platform API: OnPlayerPickUpPickup)</remarks>
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
    /// <remarks>Change drivers: CD-09 (authorization policy: moderator gating); CD-02 (CTF game-rules specification: flag return rule); CD-15 (command set: returnflag command); CD-01 (open.mp/SampSharp platform API: pickups, audio, timers)</remarks>
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

        team.Flag.Carrier?.HideOnRadarMap();
        team.Flag.ReturnToBase();
        teamPickupService.CreateFlagFromBasePosition(team);
        teamPickupService.DestroyExteriorMarker(team);
        team.Sounds.PlayFlagReturnedSound();
        flagAutoReturnTimer.Stop(team);
        worldService.GameText($"~n~~n~~n~{team.GameTextColor}{team.ColorName} flag returned!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);
        worldService.SendClientMessage(Color.Yellow, message);
    }
}
