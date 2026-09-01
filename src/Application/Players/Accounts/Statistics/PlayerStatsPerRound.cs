namespace CTF.Application.Players.Accounts.Statistics;

/// <remarks>Change drivers: CD-10 (player-statistics/rank model), CD-06 (coin economy)</remarks>
public class PlayerStatsPerRound
{
    /// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
    public int Kills { get; private set; }

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
    public int Deaths { get; private set; }

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
    public int KillingSpree { get; private set; }

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model), CD-06 (coin economy)</remarks>
    public int Coins { get; private set; }

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
    public void AddKills() => Kills++;

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
    public void AddDeaths() => Deaths++;

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
    public void AddKillingSpree() => KillingSpree++;

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model), CD-06 (coin economy)</remarks>
    public bool HasSufficientCoins(int amount) => Coins >= amount;

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model), CD-06 (coin economy)</remarks>
    public bool HasInsufficientCoins(int amount) => !HasSufficientCoins(amount);

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model), CD-06 (coin economy)</remarks>
    public Result AddCoins(int value)
    {
        if (value < 1 || value > 100)
            return Result.Failure(Messages.InvalidAddCoins);

        Coins += value;
        if (Coins > 100) 
            Coins = 100;

        return Result.Success();
    }

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model), CD-06 (coin economy)</remarks>
    public Result SubtractCoins(int value)
    {
        if (value < -100 || value > -1)
            return Result.Failure(Messages.InvalidSubtractCoins);

        Coins -= -value;
        if (Coins < 0) 
            Coins = 0;

        return Result.Success();
    }

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model), CD-06 (coin economy)</remarks>
    public void ResetCoins() => Coins = 0;

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
    public void ResetKills()  => Kills = 0;

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
    public void ResetDeaths() => Deaths = 0;

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model)</remarks>
    public void ResetKillingSpree() => KillingSpree = 0;

    /// <remarks>Change drivers: CD-10 (player-statistics/rank model), CD-06 (coin economy)</remarks>
    public void ResetStats()
    {
        Kills = 0;
        Deaths = 0;
        KillingSpree = 0;
        Coins = 0;
    }
}
