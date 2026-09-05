namespace CTF.Application.Chat.Core;

/// <summary>
/// Helper for chat-message text manipulation that carries only chat-rule drivers.
/// </summary>
/// <remarks>Change drivers: CD-13 (root; chat rules: prefix replacement)</remarks>
internal static class ChatText
{
    /// <summary>Replaces the first character of a chat message in place.</summary>
    /// <remarks>Change drivers: CD-13 (root; chat rules: prefix replacement)</remarks>
    internal static unsafe void ReplaceFirstCharacter(string originalText, char newCharacter)
    {
        fixed (char* text = originalText)
        {
            text[0] = newCharacter;
        }
    }
}
