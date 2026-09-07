namespace Persistence.Tests.Common.Contracts;

/// <summary>Abstracts a configured persistence stack so repository tests run provider-agnostically.</summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.MariaDbDialect, ChangeDriver.SqliteDialect)]
public interface IRepositoryManager : IDisposable
{
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.MariaDbDialect, ChangeDriver.SqliteDialect)]
    IPlayerRepository PlayerRepository { get; }
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.MariaDbDialect, ChangeDriver.SqliteDialect)]
    ITopPlayersRepository TopPlayersRepository { get; }
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.MariaDbDialect, ChangeDriver.SqliteDialect)]
    void InitializeSeedData();
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.MariaDbDialect, ChangeDriver.SqliteDialect)]
    void RemoveSeedData();
}
