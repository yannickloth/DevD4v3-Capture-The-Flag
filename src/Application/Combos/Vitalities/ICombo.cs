namespace CTF.Application.Combos;

/// <summary>
/// Represents a combination of different advantages, such as health, armour, and weapons, 
/// that a player can use to gain an advantage in the game.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combo)]
public interface ICombo
{
    /// <summary>
    /// Gets the name of a combo, e.g., 100 Health and 100 Armour.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    string Name { get; }

    /// <summary>
    /// Gets the required coins that a player must have to acquire the combo.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    int RequiredCoins { get; }

    /// <summary>
    /// Assigns a combo to a player, e.g., 100 Health and 100 Armour.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    Result Give(Player player);
}
