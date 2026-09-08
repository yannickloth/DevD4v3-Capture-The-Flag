namespace CTF.Application.SchemaNs3;

[ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
public class FakePlayer
{
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    private const int NoSkin = -1;
    /// <remarks>Change drivers: CD-18 (root; database schema/player data model); CD-25 (BCrypt password-hashing contract) → CD-18</remarks>
    public FakePlayer() => Id = PlayerIdValueGenerator.Instance.Next();
    /// <remarks>Change drivers: CD-18 (root; database schema/player data model); CD-25 (BCrypt password-hashing contract) → CD-18</remarks>
    public FakePlayer(string name, string passwordHash)
    {
        Id = PlayerIdValueGenerator.Instance.Next();
        Name = name;
        PasswordHash = passwordHash;
    }

    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public int Id { get; }
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public string Name { get; set; }
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public string PasswordHash { get; set; }
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public int TotalKills { get; set; }
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public int TotalDeaths { get; set; }
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public int MaxKillingSpree { get; set; }
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public int BroughtFlags { get; set; }
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public int CapturedFlags { get; set; }
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public int DroppedFlags { get; set; }
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public int ReturnedFlags { get; set; }
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public int HeadShots { get; set; }
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public int GunGameWins { get; set; }
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public int SkinId { get; set; } = NoSkin;
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public RoleId RoleId { get; set; } = RoleId.Basic;
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public RankId RankId { get; set; } = RankId.Noob;
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public DateTime CreatedAt { get; set; } = DateTime.Parse("2023-10-12 12:19:24");
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
    public DateTime LastConnection { get; set; } = DateTime.Parse("2023-10-13 12:19:24");
}
