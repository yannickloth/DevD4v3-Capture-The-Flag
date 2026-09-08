namespace CTF.Application.Configuration;

[ChangeDriversAttribute(ChangeDriver.Configuration)]
public class SQLiteSettings
{
    [ChangeDriversAttribute(ChangeDriver.Configuration)]
    public string DataSource { get; set; }
    [ChangeDriversAttribute(ChangeDriver.Configuration)]
    public string ConnectionString { get; set; }
}
