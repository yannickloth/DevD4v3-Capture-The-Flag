namespace CTF.Host.Services;

/// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API); CD-21 (DI container/composition)</remarks>
public class CommandUsageFormatter : ICommandTextFormatter
{
    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API); CD-21 (DI container/composition)</remarks>
    public string FormatCommandUsage(
        string commandName,
        string group,
        CommandParameterInfo[] parameters,
        bool includeSlash = true)
    {
        var prefix = includeSlash ? "/" : "";
        var groupPrefix = string.IsNullOrEmpty(group)
            ? ""
            : $"{group} ";

        var parameterText = string.Join(
            " ",
            parameters.Select(parameterInfo => $"[{parameterInfo.Name}]")
        );

        return $"{prefix}{groupPrefix}{commandName} {parameterText}".Trim();
    }
}
