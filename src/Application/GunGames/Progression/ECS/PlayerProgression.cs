namespace CTF.Application.GunGames.EcsNsNs2;

/// <summary>
/// Represents a player's progression in the GunGame mode.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Ecs)]
public class PlayerProgression : Component
{
    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Ecs)]
    public WeaponLevel WeaponLevel { get; private set; } = WeaponLevel.First;
    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Ecs)]
    public int KillsTowardsNextLevel { get; private set; }

    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Ecs)]
    public void AddKillsTowardsNextLevel()
        => KillsTowardsNextLevel++;

    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Ecs)]
    public bool CanLevelUp(KillsRequiredPerLevel requiredKills)
        => KillsTowardsNextLevel >= requiredKills.Value;

    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Ecs)]
    public void LevelUp(MaxWeaponLevel maxLevel)
    {
        WeaponLevel = WeaponLevel.Next(maxLevel);
        KillsTowardsNextLevel = 0;
    }

    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Ecs)]
    public void LevelDown()
    {
        WeaponLevel = WeaponLevel.Previous();
        KillsTowardsNextLevel = 0;
    }

    [ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Ecs)]
    public void Reset()
    {
        WeaponLevel = WeaponLevel.First;
        KillsTowardsNextLevel = 0;
    }
}
