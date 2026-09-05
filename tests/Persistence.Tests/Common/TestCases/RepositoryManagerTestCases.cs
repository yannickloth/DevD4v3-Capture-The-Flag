namespace Persistence.Tests.Common.TestCases;

/// <summary>Provides the three provider cases for the repository test suite.</summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.NUnit, ChangeDriver.MariaDbDialect, ChangeDriver.SqliteDialect)]
public class RepositoryManagerTestCases : IEnumerable<DatabaseProvider>
{
    [ChangeDriversAttribute(ChangeDriver.NUnit, ChangeDriver.MariaDbDialect, ChangeDriver.SqliteDialect)]
    public IEnumerator<DatabaseProvider> GetEnumerator()
    {
        yield return DatabaseProvider.InMemory;
        yield return DatabaseProvider.Sqlite;
        yield return DatabaseProvider.MariaDb;
    }

    [ChangeDriversAttribute(ChangeDriver.NUnit)]
    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();
}
