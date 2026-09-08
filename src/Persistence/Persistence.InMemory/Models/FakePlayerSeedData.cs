namespace CTF.Application.SchemaNs3;

[ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
internal static class FakePlayerSeedData
{
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public static Dictionary<int, FakePlayer> Create()
    {
        const string password = "$2a$10$60QnEiafBCLfVBMfQkExVeolyBxVHWcSQKTvkxVJj9FUozRpRP/GW";
        FakePlayer[] players =
        [
            new()
            {
                Name = "Admin_Player",
                PasswordHash = password,
                RoleId = RoleId.Admin,
                RankId = RankId.Noob,
                SkinId = 146
            },
            new()
            {
                Name = "Moderator_Player",
                PasswordHash = password,
                RoleId = RoleId.Moderator,
                RankId = RankId.Noob,
                SkinId = 146
            },
            new()
            {
                Name = "VIP_Player",
                PasswordHash = password,
                RoleId = RoleId.VIP,
                RankId = RankId.Noob,
                SkinId = 146
            },
            new()
            {
                Name = "Basic_Player",
                PasswordHash = password,
                RoleId = RoleId.Basic,
                RankId = RankId.Noob,
                SkinId = 146
            },
            new()
            {
                Name = "Basic_Player(2)",
                PasswordHash = password,
                RoleId = RoleId.Basic,
                RankId = RankId.SemiAdvance,
                TotalKills = 150,
                MaxKillingSpree = 10,
                SkinId = 131
            },
            new()
            {
                Name = "Basic_Player(3)",
                PasswordHash = password,
                RoleId = RoleId.Basic,
                RankId = RankId.SemiAdvance,
                TotalKills = 160,
                MaxKillingSpree = 15,
                SkinId = 140
            },
            new()
            {
                Name = "Basic_Player(4)",
                PasswordHash = password,
                RoleId = RoleId.Basic,
                RankId = RankId.SemiAdvance,
                TotalKills = 170,
                MaxKillingSpree = 20,
                SkinId = 137
            },
            new()
            {
                Name = "Basic_Player(5)",
                PasswordHash = password,
                RoleId = RoleId.Basic,
                RankId = RankId.Advanced,
                TotalKills = 200,
                MaxKillingSpree = 25,
                SkinId = 100
            },
            new()
            {
                Name = "Basic_Player(6)",
                PasswordHash = password,
                RoleId = RoleId.Basic,
                RankId = RankId.Hitman,
                TotalKills = 251,
                MaxKillingSpree = 50,
                SkinId = 98
            },
            new()
            {
                Name = "Basic_Player(7)",
                PasswordHash = password,
                RoleId = RoleId.Basic,
                RankId = RankId.Advanced,
                TotalKills = 200,
                MaxKillingSpree = 30,
                SkinId = 150
            }
        ];
        return players.ToDictionary(player => player.Id, player => player);
    }
}
