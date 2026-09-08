namespace CTF.Application.Chat.EcsNsNs2;

/// <summary>
/// Records whether a player has blocked incoming private messages.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Ecs)]
public class PrivateMessageComponent : Component
{
    [ChangeDriversAttribute(ChangeDriver.Chat)]
    public bool IsBlocked { get; set; }
}
