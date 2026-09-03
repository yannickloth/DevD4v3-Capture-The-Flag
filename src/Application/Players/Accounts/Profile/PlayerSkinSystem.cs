namespace CTF.Application.Players.Accounts.Profile;

/// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API); CD-15 (command set) → CD-01; CD-20 (outbound repository contract) → CD-01</remarks>
/// <remarks>Injected dependencies: playerRepository -> CD-20. Driven by the IPlayerRepository contract + CD-21 (DI wiring).</remarks>
public class PlayerSkinSystem(IPlayerRepository playerRepository) : ISystem
{
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API); CD-20 (outbound repository contract) → CD-01</remarks>
    [PlayerCommand("skin")]
    public void SetSkin(Player player, [CommandParameter(Name = "skinId")]int newSkinId)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        int oldSkinId = playerInfo.SkinId;
        Result result = playerInfo.SetSkin(newSkinId);
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
        var message = Smart.Format(Messages.SavedSkin, new { playerInfo.SkinId });
        player.SendClientMessage(Color.Yellow, message);
    }
}
