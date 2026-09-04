namespace CTF.Application.Combos;

/// <summary>
/// Represents a combination of different advantages, such as health, armour, and weapons, 
/// that a player can use to gain an advantage in the game.
/// </summary>
/// <remarks>Change drivers: CD-05 (root; combo definitions)</remarks>
public interface ICombo
{
    /// <summary>
    /// Gets the name of a combo, e.g., 100 Health and 100 Armour.
    /// </summary>
    /// <remarks>Change drivers: CD-05 (root; combo definitions)</remarks>
    string Name { get; }

    /// <summary>
    /// Gets the required coins that a player must have to acquire the combo.
    /// </summary>
    /// <remarks>Change drivers: CD-05 (root; combo definitions: coin cost)</remarks>
    int RequiredCoins { get; }

    /// <summary>
    /// Assigns a combo to a player, e.g., 100 Health and 100 Armour.
    /// </summary>
    /// <remarks>Change drivers: CD-05 (root; combo definitions)</remarks>
    Result Give(Player player);
}
