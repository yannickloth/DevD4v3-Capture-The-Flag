namespace Persistence.MariaDB.Settings;

[ChangeDriversAttribute(ChangeDriver.Configuration)]
public class MariaDbSettings
{
    [ChangeDriversAttribute(ChangeDriver.Configuration)]
    public string Server { get; set; }
    [ChangeDriversAttribute(ChangeDriver.Configuration)]
    public uint Port { get; set; }
    [ChangeDriversAttribute(ChangeDriver.Configuration)]
    public string Database { get; set; }
    [ChangeDriversAttribute(ChangeDriver.Configuration)]
    public string UserName { get; set; }
    [ChangeDriversAttribute(ChangeDriver.Configuration)]
    public string Password { get; set; }
    [ChangeDriversAttribute(ChangeDriver.Configuration)]
    public string ConnectionString { get; set; }
}
