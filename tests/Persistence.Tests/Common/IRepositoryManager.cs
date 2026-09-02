namespace Persistence.Tests.Common;

/// <summary>Abstracts a configured persistence stack so repository tests run provider-agnostically.</summary>
/// <remarks>Change drivers: CD-29 (root; code-under-test: exposes the repository seams under test); CD-20 (outbound repository contract) → CD-29; CD-19 (MariaDB SQL dialect) → CD-29; CD-30 (SQLite SQL dialect) → CD-29</remarks>
public interface IRepositoryManager : IDisposable
{
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract)</remarks>
    IPlayerRepository PlayerRepository { get; }
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract)</remarks>
    ITopPlayersRepository TopPlayersRepository { get; }
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model: seed data lifecycle) → CD-20</remarks>
    void InitializeSeedData();
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    void RemoveSeedData();
}
