namespace CTF.Application.Players.Headshots;

/// <summary>
/// Handles headshot detection, reward, and persistence.
/// </summary>
/// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification); CD-01 (open.mp/SampSharp platform API) → CD-03; CD-17 (game configuration/.env schema) → CD-03; CD-10 (player-statistics/rank model) → CD-03; CD-20 (outbound repository contract) → CD-03</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; worldService -> CD-01; headshotSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class HeadshotSystem(
    IPlayerRepository playerRepository,
    IWorldService worldService,
    HeadshotSettings headshotSettings) : ISystem
{
    /// <summary>
    /// This callback is called when a player takes damage.
    /// </summary>
    /// <param name="receiver">
    /// The player that took damage.
    /// </param>
    /// <param name="issuer">
    /// The player that caused the damage. <c>null</c> if self-inflicted.
    /// </param>
    /// <param name="amount">
    /// The amount of damage the player took (health and armour combined).
    /// </param>
    /// <param name="weapon">
    /// The ID of the weapon/reason for the damage.
    /// </param>
    /// <param name="bodyPart">
    /// The <see href="https://www.open.mp/docs/scripting/resources/bodyparts">body part</see> that was hit.
    /// </param>
    /// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification); CD-01 (open.mp/SampSharp platform API) → CD-03; CD-17 (game configuration/.env schema) → CD-03; CD-10 (player-statistics/rank model) → CD-03; CD-20 (outbound repository contract) → CD-03</remarks>
    [Event]
    public void OnPlayerTakeDamage(Player receiver, Player issuer, float amount, Weapon weapon, BodyPart bodyPart)
    {
        if (issuer.IsInvalidPlayer())
            return;

        if ((weapon >= Weapon.None && weapon <= Weapon.Cane) || (weapon >= Weapon.Colt45 && weapon <= Weapon.Sniper))
        {
            issuer.PlaySound(soundId: 17802);
        }

        if (issuer.Team != receiver.Team && weapon == Weapon.Sniper && bodyPart == BodyPart.Head)
        {
            PlayerInfo issuerInfo = issuer.GetRequiredInfo();
            PlayerInfo receiverInfo = receiver.GetRequiredInfo();
            issuerInfo.AddHeadShots();
            issuerInfo.StatsPerRound.AddCoins(5);
            playerRepository.UpdateHeadShots(issuerInfo);
            receiver.Health = 0;
            if (!receiverInfo.IsCarryingEnemyFlag())
            {
                issuer.PlayAudioStream(headshotSettings.AudioUrl);
                receiver.PlayAudioStream(headshotSettings.AudioUrl);
            }
            var message = Smart.Format(Messages.HeadshotToPlayer, new
            {
                PlayerName1 = issuer.Name,
                PlayerName2 = receiver.Name
            });
            worldService.SendClientMessage(Color.Yellow, message);
        }
    }
}
