namespace CTF.Application.PlayerResources;

/// <remarks>Change drivers: CD-44 (root; skin id resources); CD-31 (player state); CD-15 (command set) → CD-44; CD-20 (outbound repository contract) → CD-44</remarks>
/// <remarks>Injected dependencies: playerRepository -> CD-20. Driven by the IPlayerRepository contract + CD-21 (DI wiring).</remarks>
public class PlayerSkinSystem(IPlayerRepository playerRepository) : ISystem
{
    /// <remarks>Change drivers: CD-44 (root; skin id resources); CD-31 (player state); CD-20 (outbound repository contract) → CD-44; CD-15 (command set) → CD-44</remarks>
    [PlayerCommand("skin")]
    public void SetSkin(Player player, [CommandParameter(Name = "skinId")]int newSkinId)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        int oldSkinId = playerInfo.Appearance.SkinId;
        Result result = playerInfo.Appearance.SetSkin(newSkinId);
        if (result.IsFailed)
        {
            player.SendClientMessage(Color.Red, result.Message);
            return;
        }

        if (oldSkinId == newSkinId)
        {
            player.SendClientMessage(Color.Red, Messages.OldSkinIsEqualsToNewSkin);
            return;
        }

        player.Skin = newSkinId;
        player.GameText($"Skin ID {newSkinId}", TimeSpan.FromSeconds(3), GameTextStyle.Style4);
        playerRepository.UpdateSkin(playerInfo);
        var message = Smart.Format(Messages.SavedSkin, new { playerInfo.Appearance.SkinId });
        player.SendClientMessage(Color.Yellow, message);
    }
}
