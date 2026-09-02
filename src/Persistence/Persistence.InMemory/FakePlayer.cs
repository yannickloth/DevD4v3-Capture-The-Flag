namespace Persistence.InMemory;

/// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract), CD-18 (database schema/player data model), CD-20 (outbound repository contract)</remarks>
public class FakePlayer
{
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    private const int NoSkin = -1;
    /// <remarks>Change drivers: CD-18 (database schema/player data model), CD-21 (DI container/composition)</remarks>
    public FakePlayer() => Id = PlayerIdValueGenerator.Instance.Next();
    /// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract), CD-18 (database schema/player data model), CD-21 (DI container/composition)</remarks>
    public FakePlayer(string name, string passwordHash)
    {
        Id = PlayerIdValueGenerator.Instance.Next();
        Name = name;
        PasswordHash = passwordHash;
    }

    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public int Id { get; }
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public string Name { get; set; }
    /// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract), CD-18 (database schema/player data model)</remarks>
    public string PasswordHash { get; set; }
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public int TotalKills { get; set; }
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public int TotalDeaths { get; set; }
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public int MaxKillingSpree { get; set; }
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public int BroughtFlags { get; set; }
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public int CapturedFlags { get; set; }
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public int DroppedFlags { get; set; }
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public int ReturnedFlags { get; set; }
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public int HeadShots { get; set; }
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public int GunGameWins { get; set; }
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public int SkinId { get; set; } = NoSkin;
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public RoleId RoleId { get; set; } = RoleId.Basic;
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public RankId RankId { get; set; } = RankId.Noob;
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public DateTime CreatedAt { get; set; } = DateTime.Parse("2023-10-12 12:19:24");
    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public DateTime LastConnection { get; set; } = DateTime.Parse("2023-10-13 12:19:24");
}
