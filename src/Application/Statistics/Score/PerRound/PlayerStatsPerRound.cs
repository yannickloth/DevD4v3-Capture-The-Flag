namespace CTF.Application.Statistics.Score.PerRound;

[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Coin)]
public class PlayerStatsPerRound
{
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public int Kills { get; private set; }

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public int Deaths { get; private set; }

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public int KillingSpree { get; private set; }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Coin)]
    public int Coins { get; private set; }

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void AddKills() => Kills++;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void AddDeaths() => Deaths++;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void AddKillingSpree() => KillingSpree++;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Coin)]
    public bool HasSufficientCoins(int amount) => Coins >= amount;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Coin)]
    public bool HasInsufficientCoins(int amount) => !HasSufficientCoins(amount);

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Coin)]
    public Result AddCoins(int value)
    {
        if (value < 1 || value > 100)
            return Result.Failure(Messages.InvalidAddCoins);

        Coins += value;
        if (Coins > 100) 
            Coins = 100;

        return Result.Success();
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Coin)]
    public Result SubtractCoins(int value)
    {
        if (value < -100 || value > -1)
            return Result.Failure(Messages.InvalidSubtractCoins);

        Coins -= -value;
        if (Coins < 0) 
            Coins = 0;

        return Result.Success();
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Coin)]
    public void ResetCoins() => Coins = 0;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void ResetKills()  => Kills = 0;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void ResetDeaths() => Deaths = 0;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void ResetKillingSpree() => KillingSpree = 0;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Coin)]
    public void ResetStats()
    {
        Kills = 0;
        Deaths = 0;
        KillingSpree = 0;
        Coins = 0;
    }
}
