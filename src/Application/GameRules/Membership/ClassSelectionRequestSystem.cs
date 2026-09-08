namespace CTF.Application.GameRules.PlayerNsNs2;

/// <summary>
/// Handles the class-selection request and spawn-request flow for players.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; classSelectionTextDrawRenderer -> CD-34; teamTextDrawRenderer -> CD-34. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
public class ClassSelectionRequestSystem(
    IWorldService worldService,
    ClassSelectionTextDrawRenderer classSelectionTextDrawRenderer,
    TeamTextDrawRenderer teamTextDrawRenderer) : ISystem
{
    /// <summary>
    /// This callback is called when a player changes class at class selection (and when class selection first appears).
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    [Event]
    public void OnPlayerRequestClass(Player player, Class @class)
    {
        if (player.HasForcedClassSelectionAfterDeath())
        {
            player.SetSpawnInfo(player.Team, player.Skin, player.Position, player.Angle);
            player.Spawn();
            return;
        }

        player.Color = Team.None.ColorHex;
        player.Position = new Vector3(-1389.137451f, 3314.043701f, 20.493314f);
        player.CameraPosition = new Vector3(-1399.776000f, 3310.254150f, 21.525623f);
        player.SetCameraLookAt(new Vector3(-1395.072143f, 3311.873291f, 22.027709f));
        player.Angle = 111.68f;
        player.Interior = 0;
        player.PlaySound(soundId: 1132);
        Team selectedTeam = @class.Id == (int)TeamId.Alpha ? Team.Alpha : Team.Beta;
        string gameText = selectedTeam.GetAvailabilityMessage();
        player.GameText(gameText, TimeSpan.FromMilliseconds(999999999), GameTextStyle.Style3);
        player.Team = (int)selectedTeam.Id;
    }

    /// <summary>
    /// This callback is called when a player attempts to spawn via class selection either 
    /// by pressing SHIFT or clicking the 'Spawn' button.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    [Event]
    public bool OnPlayerRequestSpawn(Player player)
    {
        Team selectedTeam = player.Team == (int)TeamId.Alpha ? Team.Alpha : Team.Beta;
        player.DisableClassSelection();
        player.HideGameText(style: 3);
        player.GetRequiredInfo().Appearance.SetTeam(selectedTeam.Id);
        player.StopAudioStream();
        selectedTeam.Members.Add(player);
        classSelectionTextDrawRenderer.Hide(player);
        teamTextDrawRenderer.UpdateTeamMembers(selectedTeam);
        var message = Smart.Format(Messages.PlayerAddedToTeam, new
        {
            PlayerName = player.Name,
            TeamName = selectedTeam.Name
        });
        worldService.SendClientMessage(selectedTeam.ColorHex, message);
        return true;
    }
}
