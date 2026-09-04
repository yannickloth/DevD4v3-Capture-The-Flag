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
| `Accounts` | CD-08 | AccountAuthenticator, AccountComponent, AccountSystem, AuthenticationDialog, PlayerInfo (aggregate), PlayerAccount, PlayerName/PasswordSystem, PlayerExtensions |
| `AntiCheat` | CD-14 | AntiCBugSystem/Commands/Settings, LastFiredTimeComponent |
| `Audio` | CD-40 | TeamSounds |
| `Authorization` | CD-09 | PlayerRole, RoleId/Collection, PlayerRoleSystem/Checker/Extensions, Admin/VIPListSystem, ServerOwner*, RequiresMinimumRoleAttribute |
| `Bcrypt` (Host) | CD-25 | PasswordHasherBcrypt |
| `Chat` | CD-13 | ChatSystem, IChatMessage, Private*Chat, PrivateMessageSystem |
| `CoinEconomy` | CD-06 | PlayerCoinsSystem |
| `Combat` | CD-03 | Health/Armour/Headshot/WeaponSelection systems, WeaponPack, Vitality |
| `Combos` | CD-05 | ComboSystem, ICombo, *Vitality, RocketLauncherSystem, ComboSettings |
| `CommandInfrastructure` | CD-43 | PlayerCommandTextSystem, PlayerCommandLockMiddleware, CommandUsageFormatter (Host) |
| `Commands` | CD-15 | Admin/Moderator/Vip/BasicCommands |
| `Composition` (Host) | CD-21 | Application/Database/HostEcs service extensions |
| `Config` (Host) | CD-17 | AppSettingsExtensions |
| `Deployment` (Host) | CD-22 | GameModePaths |
| `Discord` | CD-24 | DiscordWebhookClient (Host), PlayerActivityNotificationSystem |
| `Ecs` (Host) | CD-32 | Startup, Entrypoint |
| `GameRules` | CD-02 (+settings CD-17, wiring CD-21) | Flag (aggregate), FlagCarrier, FlagSystem + On*Flag handlers, TeamBalancer, TeamMembers, PlayerSpawn/Death/Pause/Welcome systems, ClassSelection* |
| `GunGames(/Results)` | CD-07 | GunGameSystem, progression types, GunGameReward, result handlers |
| `MapIcons` | CD-38 | TeamIconService, FlagIcon |
| `Maps(/Rotation)` | CD-11, CD-12 | MapCollection, MapInfoService, rotation service/system, LoadTime/TimeLeft, MapTextDrawRenderer |
| `Pickups` | CD-37 | TeamPickupService |
| `PlayerResources` | CD-44 | FlagModel, SkinTeamId, ExteriorMarker, PlayerSkinSystem/Extensions, PlayerAppearance |
| `Players(/Weapons,/TopPlayers,/Chats,/Accounts)` | CD-17 settings, CD-20 ports, CD-04, CD-21 wiring, CD-08 port | CommandCooldowns, IPlayerRepository, ITopPlayersRepository, WeaponCatalogSystem, catalogs, service extensions |
| `RconSecurity` | CD-16 | RconSecuritySystem |
| `ServerService` (Host) | CD-42 | GameModeInit |
| `Statistics` | CD-10 | PlayerStatistics (entity), PlayerStatsPerRound, rank types, stat systems/updaters, TopPlayers* types, TeamStats* |
| `Teams` | CD-02 (+wiring CD-21, TeamId CD-31‖CD-02) | Team aggregate, TeamId |
| `TextDraws` | CD-34 | TeamTextDrawRenderer, ClassSelectionTextDrawRenderer |
| `WeaponCatalogs` | CD-04 | the seven catalogs |
| Host `Logging` | CD-23 | SerilogExtensions |

## 3. Structural conventions

1. **Aggregate roots with same-table sub-entities.** `PlayerInfo` composes
   `PlayerAccount` (CD-08), `PlayerStatistics` (CD-10), `PlayerRole` (CD-09),
   `PlayerAppearance` (CD-44) — four domain entities over one `players` row.
2. **Grouping per amendment unit.** `Flag` composes nested `CarrierAttachment`
   (the only CD-39 element) beside direct elements; sibling `FlagCarrier` owns the
   carrier state. `FlagIdentity` was dissolved: Model/Icon/ColorHex no longer share
   one set under the refined drivers (CD-44 / CD-38 / CD-02).
3. **Nested classes are modules.** Their driver sets do not transmit to the enclosing
   type; class gamma = union of direct elements only.
4. **Placement follows root.** Domain systems using platform APIs as subordinates
   stay in their domain module (`PlayerSpawnLockMiddleware` CD-02 in GameRules,
   `PlayerStatsRenderer` CD-10 in Statistics); platform-*rooted* types live in the
   per-subsystem modules above.
5. **Settings and wiring co-locate with their domain** (`FlagAutoReturnSettings` CD-17
   in GameRules, `ServiceCollectionExtensions` CD-21 per module) — a documented
   deviation traded for locality.

## 4. Compliance state

| Check | Result |
|-------|--------|
| Every element carries its own `Change drivers` remark | ✅ 0 missing (generated `*.Designer.cs` excluded) |
| Driver IDs ∈ catalogue | ✅ 0 invalid |
| CD-01 / CD-29 citations | ✅ 0 |
| Class gamma = union of direct elements | ✅ 33 gaps completed; remaining class-only drivers audited as base-class / signature-type / injected-contract transmission |
| Module root sets | ✅ single-root per module except the documented settings/wiring co-locations |

## 5. Impact sets under the refined drivers

A variation now enumerates its blast radius by grep (see catalogue §Impact sets):
attached-object API → 5 elements, dialogs → 10, timers → 16, audio → 19,
pickups → 24, textdraws → 42 — each an independent driver instead of all 445
elements firing on a monolithic platform driver.
