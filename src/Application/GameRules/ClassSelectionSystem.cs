namespace CTF.Application.GameRules;

/// <summary>
/// Handles the class-selection flow for players.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection and death/respawn); CD-31 (player events, class selection) → CD-02; CD-15 (command set: class command) → CD-02; CD-17 (game configuration/.env schema: ClassSelection__*) → CD-02</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; classSelectionTextDrawRenderer -> CD-34; teamTextDrawRenderer -> CD-34; classSelectionSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class ClassSelectionSystem(
    IWorldService worldService,
    ClassSelectionTextDrawRenderer classSelectionTextDrawRenderer,
    TeamTextDrawRenderer teamTextDrawRenderer,
    ClassSelectionSettings classSelectionSettings) : ISystem
{
    /// <remarks>Change drivers: CD-15 (root; command set: class command health threshold); CD-02 (CTF game-rules specification) → CD-15; CD-03 (combat/weapon-rules specification) → CD-15</remarks>
    private const float MinimumHealthToUseClassSelectionCommand = 85f;

    /// <summary>Adds player classes for team skins on game mode init.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class selection); CD-44 (player classes/skin ids) → CD-02</remarks>
    [Event]
    public void OnGameModeInit(IServerService serverService)
    {
        serverService.AddPlayerClass((int)Team.Alpha.SkinId, new Vector3(0f, 0f, 0f), 0);
        serverService.AddPlayerClass((int)Team.Beta.SkinId, new Vector3(0f, 0f, 0f), 0);
    }

    /// <summary>Handles player connect for the class-selection flow.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection flow); CD-17 (game configuration/.env schema: ClassSelection__IntroAudioUrl) → CD-02; CD-34; CD-40; CD-31 (OnPlayerConnect, audio, textdraw) → CD-02</remarks>
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
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection flow); CD-31 (OnPlayerRequestClass, camera/team) → CD-02</remarks>
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
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection and team membership); CD-31 (OnPlayerRequestSpawn) → CD-02</remarks>
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
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team membership); CD-31 (OnPlayerDisconnect) → CD-02; CD-08 (account & authentication policy) → CD-02</remarks>
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
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: class-selection and flag-drop rule); CD-15 (command set: class command) → CD-02; CD-03 (combat/weapon-rules specification: health gate) → CD-02</remarks>
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
