namespace CTF.Application.Statistics.Score.PerRound;

[ChangeDriversAttribute(ChangeDriver.Statistics)]
public class PlayerStatsPerRound
{
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public int Kills { get; private set; }

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public int Deaths { get; private set; }

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public int KillingSpree { get; private set; }

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void AddKills() => Kills++;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void AddDeaths() => Deaths++;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void AddKillingSpree() => KillingSpree++;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void ResetKills() => Kills = 0;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void ResetDeaths() => Deaths = 0;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void ResetKillingSpree() => KillingSpree = 0;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void ResetStats()
    {
        Kills = 0;
        Deaths = 0;
        KillingSpree = 0;
    }
}
