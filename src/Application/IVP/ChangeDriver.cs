using System.Reflection;

namespace CTF.Application.IVP;

/// <summary>
/// The full change-driver catalogue of the IVP model.
///
/// Each member's numeric value is the change-driver id (CD-02..CD-28, CD-30, CD-31..CD-44).
/// CD-01 was decomposed into CD-31..CD-44 and CD-29 was retired, so neither appears here.
/// Every member carries a <see cref="ChangeDriverAttribute"/> with its canonical CD code,
/// short name and full description (mirrors <c>IVP/after/changedrivers.md</c>).
/// </summary>
public enum ChangeDriver
{
    [ChangeDriver("CD-02", "CTF game-rules specification",
        "Flag rules (steal/capture/score/drop/return/auto-return/carrier pause), match end, round transition, team balancing")]
    GameRules = 2,

    [ChangeDriver("CD-03", "Combat / weapon-rules specification",
        "Health/armour, headshot, weapon selection")]
    Combat = 3,

    [ChangeDriver("CD-04", "Weapon-catalog configuration",
        "The closed catalog set and active selection")]
    WeaponCatalog = 4,

    [ChangeDriver("CD-05", "Combo definitions",
        "Redeemable combos, RocketLauncher toggle")]
    Combo = 5,

    [ChangeDriver("CD-06", "Coin economy",
        "Per-round earn/spend model")]
    Coin = 6,

    [ChangeDriver("CD-07", "GunGame mode rules",
        "Progression, knife-steal, final-kill win")]
    GunGame = 7,

    [ChangeDriver("CD-08", "Account & authentication policy",
        "Login/signup, password/name rules")]
    Account = 8,

    [ChangeDriver("CD-09", "Authorization policy",
        "Role ladder, command gating, server-owner key")]
    Authorization = 9,

    [ChangeDriver("CD-10", "Player-statistics / rank model",
        "Lifetime & per-round stats, ranks, top players")]
    Statistics = 10,

    [ChangeDriver("CD-11", "Map configuration",
        "Per-map .ini data: spawns, flag locations, interior, weather")]
    Map = 11,

    [ChangeDriver("CD-12", "Map-rotation rules",
        "Round timer, load countdown, next-map selection")]
    MapRotation = 12,

    [ChangeDriver("CD-13", "Chat rules",
        "Prefix-routed private chat tiers, PM block")]
    Chat = 13,

    [ChangeDriver("CD-14", "Anti-cheat policy",
        "C-Bug detection and toggle")]
    AntiCheat = 14,

    [ChangeDriver("CD-15", "Command set",
        "Command definitions and help text")]
    CommandSet = 15,

    [ChangeDriver("CD-16", "RCON security policy",
        "Kick on RCON login attempt")]
    RconSecurity = 16,

    [ChangeDriver("CD-17", "Game configuration / .env schema",
        "ServerInfo__*, CommandCooldowns__*, TopPlayers__*, ServerOwner__*, FlagCarrier__*, FlagAutoReturn__*, AntiCBug__*, Headshot__*, ClassSelection__*, WeaponCatalog__*, audio URLs, DatabaseProvider")]
    Configuration = 17,

    [ChangeDriver("CD-18", "Database schema / player data model",
        "players columns, seed data")]
    DatabaseSchema = 18,

    [ChangeDriver("CD-19", "MariaDB SQL dialect",
        "MySqlConnector, @params, LAST_INSERT_ID()")]
    MariaDbDialect = 19,

    [ChangeDriver("CD-20", "Outbound repository contract",
        "IPlayerRepository, ITopPlayersRepository")]
    Repository = 20,

    [ChangeDriver("CD-21", "DI container / composition",
        "Registrations, ECS system/middleware wiring")]
    Composition = 21,

    [ChangeDriver("CD-22", "Hosting / deployment spec",
        "gamemode/ layout, entry, environment selection")]
    Hosting = 22,

    [ChangeDriver("CD-23", "Serilog logging",
        "Sinks, templates, level overrides")]
    Logging = 23,

    [ChangeDriver("CD-24", "Discord webhook contract",
        "Payload shape, DISCORD_WEBHOOK_URL")]
    Discord = 24,

    [ChangeDriver("CD-25", "BCrypt password-hashing contract",
        "Algorithm and hash format")]
    BCrypt = 25,

    [ChangeDriver("CD-26", "NUnit test-framework contract",
        "Test projects")]
    NUnit = 26,

    [ChangeDriver("CD-27", "FluentAssertions contract",
        "Test projects")]
    FluentAssertions = 27,

    [ChangeDriver("CD-28", "NSubstitute mock contract",
        "tests/Application.Tests")]
    NSubstitute = 28,

    [ChangeDriver("CD-30", "SQLite SQL dialect",
        "Microsoft.Data.Sqlite, positional params")]
    SqliteDialect = 30,

    [ChangeDriver("CD-31", "Player entity & lifecycle events",
        "Player, OnPlayer* events, name/team/spawn/score state, spectating, classes")]
    Player = 31,

    [ChangeDriver("CD-32", "ECS runtime",
        "ISystem, Component, [Event], IEcsStartup, IEcsBuilder, middlewares, IEntityManager")]
    Ecs = 32,

    [ChangeDriver("CD-33", "Dialog API",
        "IDialogService, dialog types")]
    Dialog = 33,

    [ChangeDriver("CD-34", "Textdraw API",
        "TextDraw/PlayerTextDraw, fonts, preview models")]
    TextDraw = 34,

    [ChangeDriver("CD-35", "GameText API",
        "Display, text-color codes, styles")]
    GameText = 35,

    [ChangeDriver("CD-36", "Client-message API",
        "SendClientMessage(ToAll), message colors")]
    ClientMessage = 36,

    [ChangeDriver("CD-37", "Pickup API",
        "Pickup creation (streamer), pickup model ids, OnPlayerPickUpPickup")]
    Pickup = 37,

    [ChangeDriver("CD-38", "Map-icon & radar API",
        "CreateDynamicMapIcon, map-icon ids, Show/HideOnRadarMap")]
    MapIcon = 38,

    [ChangeDriver("CD-39", "Attached-object API",
        "Set/RemoveAttachedObject: index, bone, offset, rotation, scale, material colors")]
    AttachedObject = 39,

    [ChangeDriver("CD-40", "Audio API",
        "PlayAudioStream (the .env URLs are CD-17)")]
    Audio = 40,

    [ChangeDriver("CD-41", "Timer API",
        "ITimerService, TimerReference, intervals")]
    Timer = 41,

    [ChangeDriver("CD-42", "Server service API",
        "IServerService: name, language, gamemode text, ped anims")]
    ServerService = 42,

    [ChangeDriver("CD-43", "Command infrastructure",
        "[PlayerCommand], tags, ICommandTextFormatter, IPermissionChecker")]
    CommandInfrastructure = 43,

    [ChangeDriver("CD-44", "Model & skin id resources",
        "Object model ids, skin ids, no-skin sentinel")]
    Model = 44,
}

/// <summary>Metadata attached to each <see cref="ChangeDriver"/> member.</summary>
[AttributeUsage(AttributeTargets.Field)]
public sealed class ChangeDriverAttribute(string code, string name, string description) : Attribute
{
    public string Code { get; } = code;
    public string Name { get; } = name;
    public string Description { get; } = description;
}

/// <summary>Readable change-driver catalogue entry.</summary>
public readonly record struct ChangeDriverInfo(string Code, string Name, string Description, ChangeDriver Driver)
{
    /// <summary>The canonical id, e.g. "CD-02".</summary>
    public string Id => Code;
}

/// <summary>Extension access to the change-driver catalogue.</summary>
public static class ChangeDriverCatalog
{
    /// <summary>Returns the metadata attached to a change-driver value.</summary>
    public static ChangeDriverInfo GetInfo(this ChangeDriver driver)
    {
        FieldInfo field = typeof(ChangeDriver).GetField(driver.ToString())
            ?? throw new InvalidOperationException($"No field for {driver}");
        var attr = field.GetCustomAttribute<ChangeDriverAttribute>()
            ?? throw new InvalidOperationException($"ChangeDriver {driver} lacks ChangeDriver metadata");
        return new ChangeDriverInfo(attr.Code, attr.Name, attr.Description, driver);
    }

    /// <summary>All catalogue entries, ordered by change-driver id.</summary>
    public static IEnumerable<ChangeDriverInfo> All =>
        Enum.GetValues<ChangeDriver>().Select(d => d.GetInfo());
}

/// <summary>
/// An element's change drivers as an <b>existence-ordered chain</b>.
/// There is no "root" concept: <c>Order</c> lists drivers by existence-causality, so that each
/// driver <c>Order[i]</c> exists <i>only because</i> the earlier drivers <c>Order[0..i-1]</c> exist.
/// E.g. <c>[Account, Repository, DatabaseSchema]</c> means the repository exists because the account
/// domain exists, and the schema exists because the repository exists. This ordering is exactly what
/// justifies namespace nesting: a type whose chain is a strict suffix of another's nests under it.
/// </summary>
public readonly record struct CausalChain(ChangeDriver[] Order)
{
    /// <summary>The first (existence-basis) driver of the chain.</summary>
    public ChangeDriver First => Order[0];

    /// <summary>Depth of the chain (number of causal steps).</summary>
    public int Depth => Order.Length;

    /// <summary>True when this chain is a strict extension (suffix) of <paramref name="prefix"/>.</summary>
    public bool IsExtensionOf(CausalChain prefix)
    {
        if (Order.Length <= prefix.Order.Length) return false;
        for (int i = 0; i < prefix.Order.Length; i++)
            if (Order[i] != prefix.Order[i]) return false;
        return true;
    }

    public override string ToString() => string.Join(" → ", Order.Select(d => d.GetInfo().Code));
}

/// <summary>
/// Declares the existence-ordered change-driver chain of an annotated element
/// (class, method, property, or a namespace <c>ChangeDrivers</c> marker type).
/// Referenced as <c>[ChangeDrivers(...)]</c>; use the full <c>ChangeDriversAttribute</c> spelling
/// inside a namespace that also declares a <c>ChangeDrivers</c> marker type to avoid ambiguity.
/// </summary>
[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface |
    AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field,
    AllowMultiple = false)]
public sealed class ChangeDriversAttribute : Attribute
{
    /// <summary>The existence-ordered causal chain declared by this annotation.</summary>
    public CausalChain Chain { get; }

    /// <summary>The first (existence-basis) driver of the chain.</summary>
    public ChangeDriver First => Chain.First;

    public ChangeDriversAttribute(params ChangeDriver[] order)
        => Chain = new CausalChain(order ?? Array.Empty<ChangeDriver>());
}

/// <summary>Reflection helpers to read change-driver annotations.</summary>
public static class ChangeDrivers
{
    /// <summary>Returns the <see cref="CausalChain"/> declared on a type or member, or <see langword="null"/>.</summary>
    public static CausalChain? ChainOf(MemberInfo member)
        => member.GetCustomAttribute<ChangeDriversAttribute>()?.Chain;
}
