namespace CTF.Application.Teams.Flags.Events;

/// <summary>
/// This event occurs when a player attempts to pick up their own team's flag, which is currently at the base position.
/// </summary>
/// <remarks>Change drivers: CD-02 (CTF game-rules specification: own-flag-at-base rule), CD-01 (open.mp/SampSharp platform API: GameText).</remarks>
public class OnFlagAtBasePosition : IFlagEvent
{
    /// <summary>Gets the flag status handled by this event.</summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag state machine).</remarks>
    public FlagStatus FlagStatus => FlagStatus.BasePosition;

    /// <summary>Handles the own-flag-at-base event.</summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: own-flag-at-base rule), CD-01 (open.mp/SampSharp platform API: GameText).</remarks>
    public void Handle(Team team, Player player)
    {
        var text = Smart.Format(Messages.OnFlagAtBasePosition, new { team.GameTextColor });
        player.GameText(text, TimeSpan.FromSeconds(5), GameTextStyle.Style3);
    }
}
