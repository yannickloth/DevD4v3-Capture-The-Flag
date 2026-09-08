namespace CTF.Application.Maps;

[ChangeDriversAttribute(ChangeDriver.Map)]
public interface IMap
{
    [ChangeDriversAttribute(ChangeDriver.Map)]
    int Id { get; }
    [ChangeDriversAttribute(ChangeDriver.Map)]
    string Name { get; }
}
