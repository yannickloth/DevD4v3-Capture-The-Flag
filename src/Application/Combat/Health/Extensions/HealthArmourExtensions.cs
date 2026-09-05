namespace CTF.Application.Combat.Health.Extensions;

/// <summary>
/// Provides extension methods for adding health and armour, bounded to their maximum values.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player)]
public static class HealthArmourExtensions
{
    /// <summary>
    /// Increases the amount of health of a player.
    /// </summary>
    /// <param name="player">The current player.</param>
    /// <param name="amount">The amount of health to be added.</param>
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player)]
    public static void AddHealth(this Player player, float amount)
    {
        if (amount < 0)
            amount = -amount;

        float total = player.Health + amount;

        if (total > 100)
            player.Health = 100;
        else
            player.Health += amount;
    }

    /// <summary>
    /// Increases the amount of armour of a player.
    /// </summary>
    /// <param name="player">The current player.</param>
    /// <param name="amount">The amount of armour to be added.</param>
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player)]
    public static void AddArmour(this Player player, float amount)
    {
        if (amount < 0)
            amount = -amount;

        float total = player.Armour + amount;

        if (total > 100)
            player.Armour = 100;
        else
            player.Armour += amount;
    }
}
