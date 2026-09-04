# Change-Driver Catalogue (After)

> Live catalogue for the post-IVP tree (`src/Application`, `src/Host`, `src/Persistence`).
> Supersedes `IVP/before/changedrivers.md`. Terminology follows `IVP/causal-order.md`
> (`→` subordination, `‖` siblings, root = causal head) and `IVP/constraints.md`
> (module per change-driver set, granularity independence).

## Containment & transmission rules

| Rule | Statement |
|------|-----------|
| Containment | module root driver set ⊇ class gamma ⊇ member gamma |
| Class gamma | the **union of the gammas of its direct elements only** |
| Nested classes are modules | a nested class's driver set **does not transmit** to the enclosing type; crossing its boundary is a call-site dependency, driven by the called operation's contract |
| Placement follows root | an element/type lives in the module of its **root** driver; subordinates never determine placement |
| Dependency transmission | an injected dependency (or a referenced type) transmits **its own contract drivers** to the using element, plus CD-21 wiring |
| No umbrella drivers | CD-01 was decomposed (see below); a driver ID must name one amendment unit |

## Retired / decomposed

- **CD-29** — retired (methodology error: depended-on seam is not a driver). Never renumbered.
- **CD-01** — decomposed into CD-31..CD-44: the platform umbrella bundled independently
  varying subsystems (a model-id renumbering never forces a textdraw edit). No element may cite CD-01.

## Catalogue

| ID | Driver | Anchors |
|----|--------|---------|
| CD-02 | CTF game-rules specification — flag rules (steal/capture/score/drop/return/auto-return/carrier pause), match end, round transition, team balancing | `README.md` gameplay rules; `Flag`, `Team`, `FlagSystem` |
| CD-03 | Combat / weapon-rules specification — health/armour, headshot, weapon selection | `README.md` weapon system; `Combat/*`, `GameMode.Common` weapon model |
| CD-04 | Weapon-catalog configuration — the closed catalog set and active selection | `WeaponCatalogType`, `WeaponCatalogSettings` |
| CD-05 | Combo definitions — redeemable combos, RocketLauncher toggle | `ICombo`, `ComboSettings` |
| CD-06 | Coin economy — per-round earn/spend model | `PlayerStatsPerRound` coins, `PlayerCoinsSystem` |
| CD-07 | GunGame mode rules — progression, knife-steal, final-kill win | `GunGames/*`, `GunGameReward` |
| CD-08 | Account & authentication policy — login/signup, password/name rules | `AccountAuthenticator`, `AuthenticationDialog`, `PlayerInfo` |
| CD-09 | Authorization policy — role ladder, command gating, server-owner key | `RoleId`, `PlayerRoleChecker`, `ServerOwnerSettings` |
| CD-10 | Player-statistics / rank model — lifetime & per-round stats, ranks, top players | `PlayerStatistics`, `RankCollection`, `ITopPlayersRepository` |
| CD-11 | Map configuration — per-map `.ini` data: spawns, flag locations, interior, weather | `Maps/Files/*.ini`, `MapInfoService`, `IMap` |
| CD-12 | Map-rotation rules — round timer, load countdown, next-map selection | `MapRotationService`/`System`, `LoadTime`, `TimeLeft` |
| CD-13 | Chat rules — prefix-routed private chat tiers, PM block | `ChatSystem`, `IChatMessage`, `PrivateMessageSystem` |
| CD-14 | Anti-cheat policy — C-Bug detection and toggle | `AntiCBugSystem`, `AntiCBugSettings` |
| CD-15 | Command set — command definitions and help text | `*Commands`, `DetailedCommandInfo` |
| CD-16 | RCON security policy — kick on RCON login attempt | `RconSecuritySystem` |
| CD-17 | Game configuration / `.env` schema — `ServerInfo__*`, `CommandCooldowns__*`, `TopPlayers__*`, `ServerOwner__*`, `FlagCarrier__*`, `FlagAutoReturn__*`, `AntiCBug__*`, `Headshot__*`, `ClassSelection__*`, `WeaponCatalog__*`, audio URLs, `DatabaseProvider` | `.env.example`, `AppSettingsExtensions` |
| CD-18 | Database schema / player data model — `players` columns, seed data | `schema.sql`, `FakePlayer` |
| CD-19 | MariaDB SQL dialect — `MySqlConnector`, `@params`, `LAST_INSERT_ID()` | `Persistence.MariaDB/*` |
| CD-20 | Outbound repository contract — `IPlayerRepository`, `ITopPlayersRepository` | the ports |
| CD-21 | DI container / composition — registrations, ECS system/middleware wiring | `Startup`, `ServiceCollectionExtensions` |
| CD-22 | Hosting / deployment spec — `gamemode/` layout, entry, environment selection | `GameModePaths`, `Program.cs`, `Dockerfile` |
| CD-23 | Serilog logging — sinks, templates, level overrides | `SerilogExtensions` |
| CD-24 | Discord webhook contract — payload shape, `DISCORD_WEBHOOK_URL` | `DiscordWebhookClient`, `IDiscordWebhookClient` |
| CD-25 | BCrypt password-hashing contract — algorithm and hash format | `PasswordHasherBcrypt`, `IPasswordHasher` |
| CD-26 | NUnit test-framework contract | test projects |
| CD-27 | FluentAssertions contract | test projects |
| CD-28 | NSubstitute mock contract | `tests/Application.Tests` |
| CD-30 | SQLite SQL dialect — `Microsoft.Data.Sqlite`, positional params | `Persistence.SQLite/*` |
| CD-31 | Player entity & lifecycle events — `Player`, `OnPlayer*` events, name/team/spawn/score state, spectating, classes | `SampSharp.OpenMp.Entities` `Player` |
| CD-32 | ECS runtime — `ISystem`, `Component`, `[Event]`, `IEcsStartup`, `IEcsBuilder`, middlewares, `IEntityManager` | `SampSharp.Entities` |
| CD-33 | Dialog API — `IDialogService`, dialog types | `SampSharp.Entities.SAMP` dialogs |
| CD-34 | Textdraw API — `TextDraw`/`PlayerTextDraw`, fonts, preview models | `SampSharp.Entities.SAMP` textdraws |
| CD-35 | GameText API — display, text-color codes, styles | `Player.GameText`, open.mp GameText resources |
| CD-36 | Client-message API — `SendClientMessage(ToAll)`, message colors | `SampSharp.Entities.SAMP` messaging |
| CD-37 | Pickup API — pickup creation (streamer), pickup model ids, `OnPlayerPickUpPickup` | streamer service |
| CD-38 | Map-icon & radar API — `CreateDynamicMapIcon`, map-icon ids, `Show/HideOnRadarMap` | streamer service, player radar |
| CD-39 | Attached-object API — `Set/RemoveAttachedObject`: index, bone, offset, rotation, scale, material colors | `Player.SetAttachedObject` |
| CD-40 | Audio API — `PlayAudioStream` (the `.env` URLs are CD-17) | `Player.PlayAudioStream` |
| CD-41 | Timer API — `ITimerService`, `TimerReference`, intervals | `SampSharp.Entities` timers |
| CD-42 | Server service API — `IServerService`: name, language, gamemode text, ped anims | `IServerService` |
| CD-43 | Command infrastructure — `[PlayerCommand]`, tags, `ICommandTextFormatter`, `IPermissionChecker` | `SampSharp.Entities.SAMP.Commands` |
| CD-44 | Model & skin id resources — object model ids, skin ids, no-skin sentinel | `FlagModel`, `SkinTeamId`, `PlayerAppearance` |

## Impact-set examples (what a variation touches)

| Driver changes | Elements citing it |
|---|---|
| CD-39 attached-object API | 5 |
| CD-33 dialogs | 10 |
| CD-40 audio | 19 |
| CD-41 timers | 16 |
| CD-37 pickups | 24 |
| CD-34 textdraws | 42 |
