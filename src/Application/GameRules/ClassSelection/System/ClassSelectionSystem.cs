using CTF.Application.Configuration;

namespace CTF.Application.GameRules.ClassSelection.System;

/// <summary>
/// Handles the class-selection flow for players.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; classSelectionTextDrawRenderer -> CD-34; teamTextDrawRenderer -> CD-34; classSelectionSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.CommandSet, ChangeDriver.Configuration)]
public class ClassSelectionSystem(
    IWorldService worldService,
    ClassSelectionTextDrawRenderer classSelectionTextDrawRenderer,
    TeamTextDrawRenderer teamTextDrawRenderer,
    ClassSelectionSettings classSelectionSettings) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.GameRules, ChangeDriver.Combat)]
    private const float MinimumHealthToUseClassSelectionCommand = 85f;

    /// <summary>Adds player classes for team skins on game mode init.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Model)]
    [Event]
    public void OnGameModeInit(IServerService serverService)
    {
        serverService.AddPlayerClass((int)Team.Alpha.SkinId, new Vector3(0f, 0f, 0f), 0);
        serverService.AddPlayerClass((int)Team.Beta.SkinId, new Vector3(0f, 0f, 0f), 0);
    }

    /// <summary>Handles player connect for the class-selection flow.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.Audio, ChangeDriver.Player)]
    [Event]
    public void OnPlayerConnect(Player player)
    {
        player.Color = Team.None.ColorHex;
        player.AddComponent<ClassSelectionComponent>();
        player.RemoveAttachedObject(0);
        player.PlayAudioStream(classSelectionSettings.IntroAudioUrl);
        classSelectionTextDrawRenderer.Show(player);
    }

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

    /// <summary>Handles player disconnect by removing them from their team.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Account)]
    [Event]
    public void OnPlayerDisconnect(Player player, DisconnectReason reason)
    {
        if (player.IsUnauthenticated())
            return;

        PlayerInfo playerInfo = player.GetRequiredInfo();
        if (playerInfo.Appearance.Team == Team.None)
            return;

        playerInfo.Appearance.Team.Members.Remove(player);
        teamTextDrawRenderer.UpdateTeamMembers(playerInfo.Appearance.Team);
    }

    /// <summary>Redirects the player to class selection via the class command.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.CommandSet, ChangeDriver.Combat)]
    [PlayerCommand("class")]
    public void RedirectToClassSelection(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        if (playerInfo.Appearance.Team.RivalTeam.Flag.IsCarriedBy(player))
        {
            player.SendClientMessage(Color.Red, Messages.HasCapturedFlag);
            return;
        }

        if (player.Health < MinimumHealthToUseClassSelectionCommand)
        {
            player.SendClientMessage(Color.Red, Messages.NotEnoughHealth);
            return;
        }

        Team removedTeam = player.RemoveFromCurrentTeam();
        teamTextDrawRenderer.UpdateTeamMembers(removedTeam);
        player.RedirectToClassSelection();
    }
}
