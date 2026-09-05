namespace CTF.Application.PlayerResources;

/// <summary>
/// See <see href="https://dev.prineside.com/en/gtasa_samp_model_id/search/?q=flag">flag models</see>.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Model)]
public enum FlagModel
{
    /// <summary>No flag model.</summary>
    [ChangeDriversAttribute(ChangeDriver.Model)]
    None = 0,
    /// <summary>The red flag model.</summary>
    [ChangeDriversAttribute(ChangeDriver.Model)]
    Red = 19306,
    /// <summary>The blue flag model.</summary>
    [ChangeDriversAttribute(ChangeDriver.Model)]
    Blue = 19307
}
