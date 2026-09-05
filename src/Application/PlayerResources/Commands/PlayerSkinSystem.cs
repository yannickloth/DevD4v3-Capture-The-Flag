namespace CTF.Application.PlayerResources.Commands;

/// <remarks>Injected dependencies: playerRepository -> CD-20. Driven by the IPlayerRepository contract + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Model, ChangeDriver.Player, ChangeDriver.CommandSet, ChangeDriver.Repository)]
public class PlayerSkinSystem(IPlayerRepository playerRepository) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Model, ChangeDriver.Player, ChangeDriver.Repository, ChangeDriver.CommandSet)]
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
