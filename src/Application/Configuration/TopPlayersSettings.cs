namespace CTF.Application.Configuration.StatisticsDomain;

/// <summary>
/// Represents the configuration thresholds for qualifying top players.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Configuration, ChangeDriver.Statistics)]
public class TopPlayersSettings
{
    /// <summary>
    /// Gets the required total kills for a player to be considered in the top players.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Configuration, ChangeDriver.Statistics)]
    public int RequiredTotalKills { get; init; } = 150;

    /// <summary>
    /// Gets the required maximum killing spree for a player to be considered in the top players.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Configuration, ChangeDriver.Statistics)]
    public int RequiredMaxKillingSpree { get; init; } = 10;
}
