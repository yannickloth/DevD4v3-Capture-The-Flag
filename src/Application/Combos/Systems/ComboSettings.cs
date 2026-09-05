namespace CTF.Application.Combos.Systems;

[ChangeDriversAttribute(ChangeDriver.Combo)]
public class ComboSettings
{
    [ChangeDriversAttribute(ChangeDriver.Combo)]
    public bool IsRocketLauncherDisabled { get; set; } = true;
}
