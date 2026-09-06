namespace CTF.Application.CoinEconomy;

/// <summary>
/// Represents a player's per-round coin balance under the coin economy.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Coin)]
public class PlayerCoins
{
    [ChangeDriversAttribute(ChangeDriver.Coin)]
    public int Balance { get; private set; }

    [ChangeDriversAttribute(ChangeDriver.Coin)]
    public bool HasSufficientCoins(int amount) => Balance >= amount;

    [ChangeDriversAttribute(ChangeDriver.Coin)]
    public bool HasInsufficientCoins(int amount) => !HasSufficientCoins(amount);

    [ChangeDriversAttribute(ChangeDriver.Coin)]
    public Result AddCoins(int value)
    {
        if (value < 1 || value > 100)
            return Result.Failure(Messages.InvalidAddCoins);

        Balance += value;
        if (Balance > 100)
            Balance = 100;

        return Result.Success();
    }

    [ChangeDriversAttribute(ChangeDriver.Coin)]
    public Result SubtractCoins(int value)
    {
        if (value < -100 || value > -1)
            return Result.Failure(Messages.InvalidSubtractCoins);

        Balance -= -value;
        if (Balance < 0)
            Balance = 0;

        return Result.Success();
    }

    [ChangeDriversAttribute(ChangeDriver.Coin)]
    public void Reset() => Balance = 0;
}
