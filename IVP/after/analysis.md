# IVP After — Architecture & Compliance Report

> State of the tree after the full IVP pass. The catalogue lives in
> [`changedrivers.md`](changedrivers.md); the method in `IVP/meta` and `IVP/causal-order.md`.
> Compliance state at commit `35146f5+` (post module-placement fixes).

## 1. Driver sets

- 42 active drivers: CD-02..CD-28, CD-30, CD-31..CD-44.
- CD-01 **decomposed** into the 14 platform sub-drivers CD-31..CD-44 — zero citations remain.
- CD-29 retired (methodology error, never renumbered).

## 2. Module map (module → root driver sets → types)

Each module groups one coherent driver set. Sub-modules refine a parent domain
(`Maps/Rotation`, `GunGames/Results`, `Players/*`).

| Module | Root driver(s) | Contents |
|--------|----------------|----------|
| `Accounts` | CD-08 | (empty parent; sub-namespaces below) |
| `Accounts.Authentication` | CD-08 | `AccountAuthenticator`, `AccountComponent`, `AccountSystem`, `AuthenticationDialog`, `PlayerNameSystem`, `PlayerPasswordSystem` |
| `Accounts.Credentials` | CD-08 | `PlayerInfo` (aggregate), `PlayerAccount`, `PlayerNamePolicy` |
| `Accounts.Extensions` | CD-08 | `PlayerExtensions` |
| `AntiCheat` | CD-14 | AntiCBugSystem/Commands/Settings, LastFiredTimeComponent |
| `Audio` | CD-40 | TeamSounds |
| `Audio.Configuration` | CD-17 | TeamSoundCatalog |
| `Authorization` | CD-09 | (empty parent; sub-namespaces below) |
| `Authorization.Roles` | CD-09 | `PlayerRole`, `RoleId`/`Collection`, `PlayerRoleSystem`/`Checker`/`Extensions`, `RequiresMinimumRoleAttribute` |
| `Authorization.Admin` | CD-09 | `AdminListSystem`, `ServerOwnerPlayerExtensions`, `ServerOwnerSettings` |
| `Authorization.Vip` | CD-09 | `VIPListSystem` |
| `Bcrypt` (Host) | CD-25 | PasswordHasherBcrypt |
| `Chat` | CD-13 | ChatSystem, ChatText, IChatMessage, Private*Chat, PrivateMessageSystem |
| `CoinEconomy` | CD-06 | PlayerCoinsSystem |
| `Combat` | CD-03 | `WeaponPack` (kept in parent to avoid type/namespace shadowing) |
| `Combat.Health` | CD-03 | `HealthSystem`, `ArmourSystem`, `HealthArmourExtensions`, `Vitality` |
| `Combat.Headshot` | CD-03 | `HeadshotSystem`, `HeadshotSettings` |
| `Combat.WeaponSelection` | CD-03 | `WeaponSelectionComponent`, `WeaponSelectionSystem` |
| `Combos` | CD-05 | `ServiceCollectionExtensions` (DI registration root) |
| `Combos.Systems` | CD-05 | `ComboSystem`, `ComboSettings`, `RocketLauncherSystem` |
| `Combos.Vitalities` | CD-05 | `ICombo`, vitality implementations (Flamethrower/Grenades/Molotov/RocketLauncher/SatchelCharges/TearGas) |
| `CommandInfrastructure` | CD-43 | PlayerCommandTextSystem, PlayerCommandLockMiddleware, CommandUsageFormatter (Host) |
| `Commands` | CD-15 | `DetailedCommandInfo.Designer.cs` (generated resource); `ServiceCollectionExtensions` if present |
| `Commands.Admin` | CD-15 | `AdminCommands` |
| `Commands.Basic` | CD-15 | `BasicCommands` |
| `Commands.Moderator` | CD-15 | `ModeratorCommands` |
| `Commands.Vip` | CD-15 | `VipCommands`, `VipHelpCommands` |
| `Composition` (Host) | CD-21 | Application/Database/HostEcs service extensions |
| `Config` (Host) | CD-17 | AppSettingsExtensions |
| `Deployment` (Host) | CD-22 | GameModePaths |
| `Discord` | CD-24 | DiscordWebhookClient (Host), PlayerActivityNotificationSystem |
| `Ecs` (Host) | CD-32 | Startup, Entrypoint |
| `GameRules` | CD-02 (+wiring CD-21) | Module-level `ServiceCollectionExtensions` (DI registration root) |
| `GameRules.Flag` | CD-02 | `Flag` aggregate, `FlagCarrier`, `FlagSystem`, `On*Flag` handlers, `FlagAutoReturnTimer`/`Settings`, `FlagCarrier*` systems, `FlagStateResetter`, `IFlagEvent`, `FlagStatus` |
| `GameRules.ClassSelection` | CD-02 | `ClassSelectionComponent`/`System`/`Extensions`/`RedirectExtensions`, `TeamSelectionSystem`, `PlayerSpawnSystem`, `PlayerSpawnLockMiddleware` |
| `GameRules.Players` | CD-02 | `PlayerDataComponent`, `PlayerDeathSystem`, `PlayerPauseSystem`/`Extensions`, `PlayerWelcomeSystem` |
| `GameRules.Match` | CD-02 | `MatchPlayers`, `MatchResult`, `MatchResultAnnouncer`, `TeamBalancer`, `TeamMembers`, `TeamPlayerExtensions` |
| `GameRules.Configuration` | CD-17 | `ClassSelectionSettings` |
| `GunGames` | CD-07 (+generated CD-17) | `GunGameSystem`, `ServiceCollectionExtensions`; `GunGameMessages.Designer.cs` is the documented generated-code deviation |
| `GunGames.Progression` | CD-07 | weapon progressions (`Classic`, `Hardcore`, `ReverseClassic`, `Pistols`, `Rifles`, `Shotguns`, `Smgs`, `Powerful`, `Active`), `PlayerProgression`, `WeaponLevel`/`Progression`/`ProgressionType` |
| `GunGames.Results` | CD-07 | result handlers (`PlayerLeveledDown`/`Up`, `PlayerReachedFinalLevel`, `PlayerScoredFinalKill`) |
| `GunGames.Rewards` | CD-07 | `GunGameReward` |
| `GunGames.Systems` | CD-07 | `GunGame`, `GunGameSession`, `GunGameResult`, `GunGameWeaponEnforcer`, `IGunGameMode`, `IGunGameResultHandler` |
| `MapIcons` | CD-38 | TeamIconService, FlagIcon |
| `Maps` | CD-11 | `MapCollection`, `MapInfoService`, `LoadTime`/`TimeLeft` |
| `Maps.Rotation` | CD-12 | rotation service/system |
| `Pickups` | CD-37 | TeamPickupService |
| `PlayerResources` | CD-44 | FlagModel, SkinTeamId, ExteriorMarker, PlayerSkinSystem/Extensions, PlayerAppearance |
| `Players(/Weapons,/TopPlayers,/Chats,/Accounts)` | CD-17 settings, CD-20 ports, CD-04, CD-21 wiring, CD-08 port | CommandCooldowns, IPlayerRepository, ITopPlayersRepository, WeaponCatalogSystem; `Players.Weapons.Catalogs` refines `WeaponCatalogSettings` + `WeaponCatalogTypeValidator`; service extensions |
| `RconSecurity` | CD-16 | RconSecuritySystem |
| `ServerService` (Host) | CD-42 | GameModeInit |
| `Statistics` | CD-10 | (empty parent; sub-namespaces below) |
| `Statistics.Ranks` | CD-10 | `Rank`, `RankId`/`Collection`, `RankSystem`, `PlayerRankUpdater`, `PlayerRankExtensions` |
| `Statistics.Score` | CD-10 | `PlayerStatistics`, `PlayerScoreSystem`, `PlayerKillsSystem`, `PlayerKillingSpreeUpdater`, `PlayerStatsPerRound`, `PlayerStatsSystem`, `PlayerStatsRenderer` |
| `Statistics.TeamStats` | CD-10 | `TeamStatsSystem`, `TeamStatsPerRound`, `TeamScoreboardSystem` |
| `Statistics.TopPlayers` | CD-10 | `TopPlayersSystem`, `MaxTopPlayers`, `TopPlayersByMaxKillingSpree`, `TopPlayersByTotalKills` |
| `Teams` | CD-02 (+wiring CD-21, TeamId CD-31‖CD-02) | Team aggregate (composite), TeamId |
| `TextDraws` | CD-34 | TeamTextDrawRenderer, ClassSelectionTextDrawRenderer, MapTextDrawRenderer |
| `WeaponCatalogs` | CD-04 | the seven catalogs + `WeaponCatalog` base + `WeaponCatalogType` |
| Host `Logging` | CD-23 | SerilogExtensions |

## 3. Structural conventions

1. **Aggregate roots with same-table sub-entities.** `PlayerInfo` composes
   `PlayerAccount` (CD-08), `PlayerStatistics` (CD-10), `PlayerRole` (CD-09),
   `PlayerAppearance` (CD-44) — four domain entities over one `players` row.
2. **Grouping per amendment unit.** `Flag` composes nested `CarrierAttachment`
   (the only CD-39 element) beside direct elements; sibling `FlagCarrier` owns the
   carrier state. `FlagIdentity` was dissolved: Model/Icon/ColorHex no longer share
   one set under the refined drivers (CD-44 / CD-38 / CD-02).
3. **Type declarations are not IVP elements; nested classes are separate modules.**
   Class-level driver sets do not transmit to the enclosing type; a nested class is its
   own module and class gamma = union of its direct elements only. Marker interfaces and
   bare type declarations carry no change driver of their own.
4. **Dependency identity: a dependency is a parameter.** Using elements inherit only the
   driver set of the *declared contract* they reference — an injected `IWeaponRepository`
   (CD-20) carries CD-20, not the driver set of any particular implementation, and
   platform calls surface only through the named sub-driver they touch (CD-31..CD-44).
5. **Empty marker interface `ISystem` carries no change driver.** It declares no contract,
   so it never adds a driver to an implementing element's gamma; ECS membership is already
   carried by the sub-driver on the concrete member.
6. **Placement follows root.** Domain systems using platform APIs as subordinates
   stay in their domain module (`PlayerSpawnLockMiddleware` CD-02 in GameRules,
   `PlayerStatsRenderer` CD-10 in Statistics); platform-*rooted* types live in the
   per-subsystem modules above.
7. **Settings and wiring co-locate with their domain** (`FlagAutoReturnSettings` CD-17
   in GameRules, `ServiceCollectionExtensions` CD-21 per module) — a documented
   deviation traded for locality.

## 4. Compliance state

| Check | Result |
|-------|--------|
| Every element carries its own `Change drivers` remark | ✅ 0 missing (generated `*.Designer.cs` excluded) |
| Driver IDs ∈ catalogue | ✅ 0 invalid |
| CD-01 / CD-29 citations | ✅ 0 |
| Class gamma = union of direct elements | ✅ 33 gaps completed; remaining class-only drivers audited as base-class / signature-type / injected-contract transmission |
| Module root sets | ✅ 81/91 namespaces single-root-set; the 10 root-set composites are the documented essential deviations in `IVP/constraints.md` (persistence providers, generated resource classes, test fakes, aggregate facets, composition roots) |
| Single-set module audit (`ModulePurity`) | ⚠️ modules with transmissions evaluated=185, single-set=175, violating=10, skipped=82 — the 10 are class-level and internal: `PlayerStatsPerRound`, `GunGameReward`, `ComboSystem`, `TeamStatsPerRound`, `PlayerAppearance`, `Startup`, `TeamScoreboardSystem`, `PrivateAdminChat`, `PrivateModeratorChat`, `PrivateVipChat` |

### 4.1 Remaining module-purity violations

The 10 class-level modules that still span ≥ 2 driver sets are **internal** — none is a
cross-domain topic fusion. Their members mix a domain root with platform subordinates or
a role/coin split that the call site forces (e.g. `PlayerStatsPerRound` on CD-10 & CD-06,
`GunGameReward` on CD-07 & CD-03/06/10, `Private*Chat.Id` vs its send path). Closing them
requires internal contract / call-site redesigns, not regrouping. `Startup` is the only
**external-contract** blocker: its `ConfigureServices` composes CD-17/21/23/24/32 and is
called by the platform's entry point, so it cannot be split without touching the hosting
contract. After those internal refactors, only the decreed composites (settings, wiring,
persistence × dialect, test tooling) would remain multi-set by design.

## 5. Impact sets under the refined drivers

A variation now enumerates its blast radius by grep (see catalogue §Impact sets):
attached-object API → 5 elements, dialogs → 10, timers → 16, audio → 19,
pickups → 24, textdraws → 42 — each an independent driver instead of all 445
elements firing on a monolithic platform driver.
