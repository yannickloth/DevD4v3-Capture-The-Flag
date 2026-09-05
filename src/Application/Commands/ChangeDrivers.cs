using CTF.Application.IVP;

namespace CTF.Application.Commands;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of the
/// <c>CTF.Application.Commands</c> root (root-first). Its own top-level type is the generated
/// <c>DetailedCommandInfo.Designer.cs</c> resource class, driven by CD-15 command set.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet)]
internal static class ChangeDrivers { }
