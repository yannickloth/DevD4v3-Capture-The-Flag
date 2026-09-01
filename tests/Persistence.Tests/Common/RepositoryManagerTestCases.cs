namespace Persistence.Tests.Common;

/// <summary>Provides the three provider cases for the repository test suite.</summary>
/// <remarks>Change drivers: CD-26 (NUnit test-case-source convention: <c>IEnumerable&lt;DatabaseProvider&gt;</c>); CD-19 (MariaDB SQL dialect), CD-30 (SQLite SQL dialect); CD-20 (outbound repository contract).</remarks>
public class RepositoryManagerTestCases : IEnumerable<DatabaseProvider>
{
    /// <remarks>Change drivers: CD-26 (NUnit test-case-source convention), CD-19 (MariaDB SQL dialect), CD-30 (SQLite SQL dialect).</remarks>
    public IEnumerator<DatabaseProvider> GetEnumerator()
    {
        yield return DatabaseProvider.InMemory;
        yield return DatabaseProvider.Sqlite;
        yield return DatabaseProvider.MariaDb;
    }

    /// <remarks>Change drivers: CD-26 (NUnit test-case-source convention).</remarks>
    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();
}
