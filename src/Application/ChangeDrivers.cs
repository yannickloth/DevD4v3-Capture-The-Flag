using CTF.Application.IVP;

namespace CTF.Application;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of the
/// <c>CTF.Application</c> assembly root (root-first). Its only own top-level type is the
/// generated <c>Messages.Designer.cs</c> resource class, driven by CD-17 game configuration;
/// every sub-namespace re-declares its own root-first chain.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Configuration)]
internal static class ChangeDrivers { }
