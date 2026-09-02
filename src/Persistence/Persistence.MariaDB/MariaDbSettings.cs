namespace Persistence.MariaDB;

/// <remarks>Change drivers: CD-17 (root; root; game configuration/.env schema)</remarks>
public class MariaDbSettings
{
    /// <remarks>Change drivers: CD-17 (root; root; game configuration/.env schema)</remarks>
    public string Server { get; set; }
    /// <remarks>Change drivers: CD-17 (root; root; game configuration/.env schema)</remarks>
    public uint Port { get; set; }
    /// <remarks>Change drivers: CD-17 (root; root; game configuration/.env schema)</remarks>
    public string Database { get; set; }
    /// <remarks>Change drivers: CD-17 (root; root; game configuration/.env schema)</remarks>
    public string UserName { get; set; }
    /// <remarks>Change drivers: CD-17 (root; root; game configuration/.env schema)</remarks>
    public string Password { get; set; }
    /// <remarks>Change drivers: CD-17 (root; root; game configuration/.env schema)</remarks>
    public string ConnectionString { get; set; }
}
