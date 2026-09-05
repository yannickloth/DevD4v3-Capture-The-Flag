namespace CTF.Application.Accounts.Authentication.RepositoryBCrypt;

/// <remarks>Injected dependencies (change drivers of these elements): passwordHasher -> CD-25; playerRepository -> CD-20. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Repository, ChangeDriver.BCrypt, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
public class AccountAuthenticator(
    IPasswordHasher passwordHasher,
    IPlayerRepository playerRepository)
{
    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
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

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.BCrypt, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
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

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Ecs)]
    private class FailedAttemptCountComponent : Component
    {
        [ChangeDriversAttribute(ChangeDriver.Account)]
        public int Value { get; set; } = 0;
    }
}
