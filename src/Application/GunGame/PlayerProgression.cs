namespace CTF.Application.GunGameRules;

/// <summary>
/// Represents a player's progression in the GunGame mode.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
public class PlayerProgression : Component
{
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public WeaponLevel WeaponLevel { get; private set; } = WeaponLevel.First;
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public int KillsTowardsNextLevel { get; private set; }

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public void AddKillsTowardsNextLevel()
        => KillsTowardsNextLevel++;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public bool CanLevelUp(KillsRequiredPerLevel requiredKills)
        => KillsTowardsNextLevel >= requiredKills.Value;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public void LevelUp(MaxWeaponLevel maxLevel)
    {
        WeaponLevel = WeaponLevel.Next(maxLevel);
        KillsTowardsNextLevel = 0;
    }

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public void LevelDown()
    {
        WeaponLevel = WeaponLevel.Previous();
        KillsTowardsNextLevel = 0;
    }

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public void Reset()
    {
        WeaponLevel = WeaponLevel.First;
        KillsTowardsNextLevel = 0;
    }
}
