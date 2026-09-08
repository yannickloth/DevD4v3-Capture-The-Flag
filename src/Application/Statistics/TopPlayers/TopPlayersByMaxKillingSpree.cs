namespace CTF.Application.Statistics;

/// <summary>
/// Represents a top player entry ranked by maximum killing spree.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Statistics)]
public class TopPlayersByMaxKillingSpree
{
    /// <summary>Gets the player name.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public string PlayerName { get; init; }

    /// <summary>Gets the maximum killing spree.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public int MaxKillingSpree { get; init; }
}
