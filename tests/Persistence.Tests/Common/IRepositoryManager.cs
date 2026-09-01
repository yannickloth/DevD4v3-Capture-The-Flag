namespace Persistence.Tests.Common;

/// <summary>Abstracts a configured persistence stack so repository tests run provider-agnostically.</summary>
/// <remarks>Change drivers: CD-29 (code-under-test: exposes the repository seams under test); CD-20 (outbound repository contract); CD-19 (MariaDB SQL dialect), CD-30 (SQLite SQL dialect).</remarks>
public interface IRepositoryManager : IDisposable
{
    /// <remarks>Change drivers: CD-20 (outbound repository contract).</remarks>
    IPlayerRepository PlayerRepository { get; }
    /// <remarks>Change drivers: CD-20 (outbound repository contract).</remarks>
    ITopPlayersRepository TopPlayersRepository { get; }
    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model: seed data lifecycle).</remarks>
    void InitializeSeedData();
    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model).</remarks>
    void RemoveSeedData();
}
