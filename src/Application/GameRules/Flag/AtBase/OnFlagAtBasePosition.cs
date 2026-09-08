namespace CTF.Application.GameRules.GameTextDomain;

/// <summary>
/// This event occurs when a player attempts to pick up their own team's flag, which is currently at the base position.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.GameText)]
public class OnFlagAtBasePosition : IFlagEvent
{
    /// <summary>Gets the flag status handled by this event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public FlagStatus FlagStatus => FlagStatus.BasePosition;

    /// <summary>Handles the own-flag-at-base event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public void Handle(Team team, Player player)
    {
        var text = Smart.Format(Messages.OnFlagAtBasePosition, new { team.GameTextColor });
        player.GameText(text, TimeSpan.FromSeconds(5), GameTextStyle.Style3);
    }
}
