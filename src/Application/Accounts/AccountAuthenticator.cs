namespace CTF.Application.Accounts;

/// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-20 (outbound repository contract) → CD-08; CD-25 (BCrypt password-hashing contract) → CD-08; CD-01 (open.mp/SampSharp platform API) → CD-08</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): passwordHasher -> CD-25; playerRepository -> CD-20. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class AccountAuthenticator(
    IPasswordHasher passwordHasher,
    IPlayerRepository playerRepository)
{
    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-20 (outbound repository contract) → CD-08; CD-01 (open.mp/SampSharp platform API) → CD-08</remarks>
    public Result Signup(Player player, string enteredPassword)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        Result passwordResult = playerInfo.Account.SetPassword(enteredPassword);
        if (passwordResult.IsFailed)
        {
            player.SendClientMessage(Color.Red, passwordResult.Message);
            return Result.Failure();
        }

        player.GetComponent<AccountComponent>().Authenticate();
        var message = Smart.Format(Messages.CreatePlayerAccount, new { Password = enteredPassword });
        player.SendClientMessage(Color.Red, message);
        playerInfo.Account.SetName(player.Name);
        playerRepository.Create(playerInfo);
        return Result.Success();
    }

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-25 (BCrypt password-hashing contract) → CD-08; CD-01 (open.mp/SampSharp platform API) → CD-08</remarks>
    public Result Login(Player player, string enteredPassword)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        bool isWrongPassword = !passwordHasher.Verify(enteredPassword, passwordHash: playerInfo.Account.Password);
        if (isWrongPassword)
        {
            const int MaxFailedAttempts = 4;
            var failedAttemptCount = player.GetComponent<FailedAttemptCountComponent>()
                ?? player.AddComponent<FailedAttemptCountComponent>();

            failedAttemptCount.Value++;
            if (failedAttemptCount.Value == MaxFailedAttempts)
            {
                player.Kick();
                return Result.Failure();
            }

            player.SendClientMessage(Color.Red, Messages.WrongPassword);
            return Result.Failure();
        }

        player.GetComponent<FailedAttemptCountComponent>()?.Destroy();
        player.GetComponent<AccountComponent>().Authenticate();
        player.SendClientMessage(Color.Red, Messages.SuccessfulLogin);
        return Result.Success();
    }

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-01 (open.mp/SampSharp platform API) → CD-08</remarks>
    private class FailedAttemptCountComponent : Component
    {
        /// <remarks>Change drivers: CD-08 (root; account & authentication policy)</remarks>
        public int Value { get; set; } = 0;
    }
}
