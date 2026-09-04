# Capture-The-Flag — Change-Driver Catalogue

> This document is the result of applying the **Independent Variation Principle (IVP)** to the Capture-The-Flag gamemode (C# project code: `src/Application`, `src/Host`, `src/Persistence`).
>
> A **change driver** is an external forcing condition in the operating domain that, when it changes, creates a requirement for an element to be modified, via a documentable step-by-step pathway anchored in a domain artifact (statute, contract, specification, standard, config schema). Drivers are **not** ranked; every driver below is equal. Rarity/frequency informs isolation effort, not driver identity.
>
> Each driver is anchored in a concrete, verifiable artifact of this project or its environment. The counterfactual test applies to every claim: *if the anchoring artifact changed to remove the relevant condition, would the element's modification requirement disappear or shift?*

---

## 1. Driver Catalogue

The following change drivers were identified from the codebase. Each is an external authority; grouped below only where two apparent authorities are governed by a single shared amendment unit (per the paper's co-variation rule).

| ID | Change driver | Anchoring artifact(s) |
|----|---------------|----------------------|
| **CD-01** | **DECOMPOSED** (was: open.mp / SampSharp platform API). The platform driver was a coarse bundle whose subsystems vary independently (e.g. an attached-object API change never forces a textdraw edit). It is decomposed into CD-31..CD-44 below; no element may cite CD-01. kept for history; do not reference. | `external/SampSharp`, SampSharp packages, open.mp server API |
| **CD-02** | **CTF game-rules specification** — the Capture-the-Flag gameplay rules: game objective, flag rules (steal/capture/score/drop/return/auto-return/carrier-death/disconnect/pause), match end conditions, round transition rules, team balancing, death/respawn. | `README.md` § *Gameplay / Gameplay Rules*; game-design spec |
| **CD-03** | **Combat / weapon-rules specification** — health & armour rules, headshot rule, weapon-selection rules (spawn selection, weapon pack, one-weapon-per-GTA-slot, unlimited ammo, parachute), kill/score/coins-on-kill rules. | `README.md` § *Weapon System*; `GameMode.Common` weapon model (`IWeapon`, `WeaponDefinitions`) |
| **CD-04** | **Weapon-catalog configuration** — the closed set of weapon catalogs (`Walking`, `Run`, `Mixed`, `RifleOnly`, `War`, `Heavy`, `Melee`), the weapons each catalog contains, and the active catalog selection. | `WeaponCatalogType`, `WeaponCatalogSettings`, catalog DI registrations; `.env` `WeaponCatalog__Type` |
| **CD-05** | **Combo definitions** — the redeemable combat combos (health/armour/weapon package + coin cost) and the RocketLauncher enable/disable toggle. | `ICombo`, `ComboSettings`, combo DI registrations; `.env` cooldown config |
| **CD-06** | **Coin economy** — the per-round coin earn/spend model (100-coin cap, coin rewards for kills/spree/flag events/rank-up/score, combo redemption resets coins). | `PlayerStatsPerRound`, `PlayerCoinsSystem`, combo specs; `README.md` § *Combo System* |
| **CD-07** | **GunGame mode rules** — the optional game mode: kills-per-level, weapon-progression ordering, knife-steal (demotion) rule, final-kill win condition, winner & team rewards, interaction with coin/spree/rank rewards and weapon selection. | `GunGame` engine, `WeaponProgression`/definitions, `GunGameReward`, `README.md` § *GunGame* |
| **CD-08** | **Account & authentication policy** — the login/signup flow (nickname-based), password rules (length, BCrypt hashing), name rules (length/charset/uniqueness), kick-after-failed-attempts. | `AccountAuthenticator`, `AuthenticationDialog`, `PlayerInfo`, `IPasswordHasher`/`PasswordHasherBcrypt`, `README.md` § *Account System* |
| **CD-09** | **Authorization policy** — the role ladder (`Basic`/`VIP`/`Moderator`/`Admin`), minimum-role command gating, server-owner secret-key escalation. | `RoleId`, `RoleCollection`, `RequiresMinimumRoleAttribute`, `PlayerRoleChecker`, `ServerOwnerSettings`, `.env` `ServerOwner__*` |
| **CD-10** | **Player-statistics / rank model** — lifetime & per-round stats (kills, deaths, score, killing spree, coins, flags captured/brought/dropped/returned, headshots, GunGame wins), rank tiers & kill thresholds, top-players leaderboard. | `PlayerInfo`, `PlayerStatsPerRound`, `RankCollection`/`IRank`/`RankId`, `ITopPlayersRepository`, `.env` `TopPlayers__*` |
| **CD-11** | **Map configuration** — per-map gameplay data: team spawn locations, flag locations, interior, weather, world time; the map-name/id identity. | map `.ini` files (`src/Application/Maps/Files/*.ini`), `MapInfoService`, `IMap`/`MapCollection` |
| **CD-12** | **Map-rotation rules** — round timer (15 min), load countdown (10 s), next-map selection, forced-map-change, rotation-timer start/stop. | `MapRotationService`/`MapRotationSystem`, `LoadTime`, `TimeLeft`, `README.md` § *Round Transition Rules* |
| **CD-13** | **Chat rules** — prefix-routed private chat tiers (team `!`, admin `#`, moderator `&`, VIP `$`), PM send/block/unblock, chat during class selection. | `ChatSystem`, `IChatMessage`, chat definitions, `PrivateMessageSystem`; `README.md` § command docs |
| **CD-14** | **Anti-cheat policy** — the C-Bug (weapon fire→crouch) detection rule and its enable/disable toggle. | `AntiCBugSystem`, `AntiCBugSettings`; `.env` `AntiCBug__Disabled` |
| **CD-15** | **Command set** — the admin/moderator/VIP/basic command definitions and their help text. | `AdminCommands`, `ModeratorCommands`, `VipCommands`, `BasicCommands`, `DetailedCommandInfo` (`.resx`) |
| **CD-16** | **RCON security policy** — the rule that a connected player attempting an RCON login is kicked. | `RconSecuritySystem`; `config.json` rcon settings |
| **CD-17** | **Game configuration / `.env` schema** — all server configuration bound from `.env`/environment: `ServerInfo__*`, `CommandCooldowns__*`, `TopPlayers__*`, `ServerOwner__*`, `FlagCarrier__*`, `FlagAutoReturn__*`, `AntiCBug__*`, `Headshot__*`, `ClassSelection__*`, `WeaponCatalog__*`, audio URLs, `DatabaseProvider`. | `.env.example`, `AppSettingsExtensions` |
| **CD-18** | **Database schema / player data model** — the `players` table columns and seed data that the outbound ports persist. | `schema.sql` (SQLite & MariaDB), `seed_data.sql`, `FakePlayer` |
| **CD-19** | **MariaDB SQL dialect** — the MariaDB/MySQL SQL dialect and driver: `MySqlConnector`, `@parameter` placeholders, `enum` role column, `LAST_INSERT_ID()`. A change in the MariaDB dialect/driver forces only the MariaDB provider to change. | `MySqlConnector` (MariaDB), `Persistence.MariaDB/*` |
| **CD-30** | **SQLite SQL dialect** — the SQLite SQL dialect and driver: `Microsoft.Data.Sqlite`, positional parameters, int-based role column, `CreateRegexpFunction`. A change in the SQLite dialect/driver forces only the SQLite provider to change. | `Microsoft.Data.Sqlite` (SQLite), `Persistence.SQLite/*` |
| **CD-31** | **Player entity & lifecycle events** — the `Player` entity and its lifecycle events (`OnPlayerConnect`, `OnPlayerDisconnect`, `OnPlayerDeath`, `OnPlayerSpawn`, `OnPlayerRequestSpawn`, `OnPlayerText`, `OnPlayerKeyStateChange`, `OnPlayerTakeDamage`, `OnPlayerUpdate`, `OnPlayerPauseStateChange`), player state APIs (name, team, spawn, score, spectating, radar show/hide), player classes. | `SampSharp.OpenMp.Entities` `Player`, player event wiring |
| **CD-32** | **ECS runtime** — the entity-component-system runtime: `ISystem`, `Component`, `[Event]`, `IEcsStartup`, `IEcsBuilder`, middlewares, `IEntityManager`, startup context, component storage. | `SampSharp.Entities` |
| **CD-33** | **Dialog API** — `IDialogService` and the dialog types and their handlers. | `SampSharp.Entities.SAMP` dialogs |
| **CD-34** | **Textdraw API** — `TextDraw`/`PlayerTextDraw` creation, fonts, preview models, positions. | `SampSharp.Entities.SAMP` textdraws |
| **CD-35** | **GameText API** — `GameText` display and its text-color codes (`~r~` etc.) and styles. | open.mp GameText resources, `Player.GameText` |
| **CD-36** | **Client-message API** — `SendClientMessage`/`SendClientMessageToAll` and message colors. | `SampSharp.Entities.SAMP` client messaging |
| **CD-37** | **Pickup API** — pickup creation (incl. streamer pickups), pickup model ids, `OnPlayerPickUpPickup`. | streamer service, `OnPlayerPickUpPickup` |
| **CD-38** | **Map-icon & radar API** — map icon creation (`CreateDynamicMapIcon`, map-icon ids) and player radar visibility (`ShowOnRadarMap`/`HideOnRadarMap`). | streamer service map icons, player radar API |
| **CD-39** | **Attached-object API** — `SetAttachedObject`/`RemoveAttachedObject`: index, bone, offset, rotation, scale, material colors. | `Player.SetAttachedObject`, open.mp attached objects |
| **CD-40** | **Audio API** — `PlayAudioStream` and audio stream playback. (The `.env` audio URLs are CD-17.) | `Player.PlayAudioStream` |
| **CD-41** | **Timer API** — `ITimerService`, `TimerReference`, timer callbacks and intervals. | `SampSharp.Entities` timers |
| **CD-42** | **Server service API** — `IServerService`: server name, language, website, gamemode text, `UsePlayerPedAnims`, `DisableInteriorEnterExits`. | `IServerService` |
| **CD-43** | **Command infrastructure** — the player-command framework: `[PlayerCommand]`, command definitions/tags, `ICommandTextFormatter`, `IPermissionChecker`, command plumbing. | `SampSharp.Entities.SAMP.Commands` |
| **CD-44** | **Model & skin id resources** — the open.mp id spaces for object models (flag models, pickup models), skins, and player-class skin ids. | open.mp resources (model ids, skin ids), `FlagModel`, `FlagIcon`, `SkinTeamId` |
| **CD-20** | **Outbound repository contract** — the persistence ports `IPlayerRepository` and `ITopPlayersRepository` that all providers implement. | `IPlayerRepository`, `ITopPlayersRepository` |
| **CD-21** | **DI container / composition** — the dependency-injection registrations and the ECS system/middleware wiring. | `Startup`, `ServiceCollectionExtensions` (all subsystems), `Microsoft.Extensions.DependencyInjection` |
| **CD-22** | **Hosting / deployment spec** — the open.mp deployment layout (`gamemode/` working directory, maps folder, yesql SQL folders), server entry, environment selection. | `GameModePaths`, `Program.cs`, `Dockerfile`, `.env` (Docker section) |
| **CD-23** | **Serilog logging** — the logging framework configuration (level overrides, sinks, output templates). | `SerilogExtensions`, `Startup` |
| **CD-24** | **Discord webhook contract** — the external Discord integration: message payload shape and webhook URL. | `DiscordWebhookClient`, `IDiscordWebhookClient`, `.env` `DISCORD_WEBHOOK_URL` |
| **CD-25** | **BCrypt password-hashing contract** — the password hashing algorithm and hash format stored in the DB. | `PasswordHasherBcrypt`, `BCrypt.Net.BCrypt`, `IPasswordHasher`, `password` column |
| **CD-26** | **NUnit test-framework contract** — the test framework: `[Test]`, `[TestCase]`, `[TestCaseSource]`, `[SetUp]`, `[OneTimeSetUp]`, test-runner lifecycle, and the `IEnumerable<*>` test-case-source convention. When the NUnit API/attributes or the test data shape changes, test methods must change. | `NUnit`, `NUnit3TestAdapter`, `NUnit.Analyzers` (test projects `tests/**`) |
| **CD-27** | **FluentAssertions contract** — the assertion fluent-API (`Should().Be()`, `Should().Throw<>()`, `Should().BeEquivalentTo()`). When assertion conventions/semantics change, the assert blocks must change. | `FluentAssertions` (test projects `tests/**`) |
| **CD-28** | **NSubstitute mock contract** — the mocking/substitute API (`Substitute.For<*>`). When the substitution API changes, fakes built on it must change. | `NSubstitute` (test project `tests/Application.Tests` only) |

> **CD-29 removed.** A former driver "Depended-on contract (seam)" was retired as a **methodology error**: it treated being *depended upon* (a structural property of the dependency graph) as if it were a change driver. Per the IVP definition, a change driver is a domain condition anchored in a domain artifact with a causal pathway (Definition of Change Driver, Criterion 1 & 2) — it is never a generic "relation to another element". A test's real change drivers are the **domain drivers of the code under test**; a dependency-injection point is driven by the **injected type's own domain drivers** (plus CD-21 wiring), not by a seam abstraction. CD-29 was retired and not renumbered; CD-30 (SQLite dialect) follows CD-28. See `causal-order.md` §2.

---

## 2. Namespace Driver Assignments

> **CD-01 decomposition note.** CD-01 was an umbrella over independently-varying platform subsystems. It is decomposed into CD-31..CD-44. In the namespace table below, every row that previously cited `CD-01` now spans the sub-drivers CD-31..CD-44 actually present in its elements; the per-element stamps in the source remain the single source of truth for which subsystem applies where.



1 yesC# file-scoped namespaces (`namespace X.Y;`) cannot carry `///` XML doc comments, so the namespace → driver mapping is recorded here as the single source of truth. Each namespace's driver set is the union of the drivers of the types and members it contains.

| Namespace | Change drivers |
|-----------|----------------|
| `CTF.Application` | CD-17 |
| `CTF.Application.GunGames` | CD-03, CD-06, CD-07, CD-09, CD-10, CD-15, CD-17, CD-31, CD-32, CD-33, CD-35, CD-36, CD-43 |
| `CTF.Application.GunGames.Results` | CD-03, CD-07, CD-10, CD-17, CD-20, CD-31, CD-32, CD-36 |
| `CTF.Application.GunGames.WeaponProgressions` | CD-07 |
| `CTF.Application.GunGames.WeaponProgressions.Definitions` | CD-07 |
| `CTF.Application.Maps` | CD-11, CD-12, CD-17, CD-32, CD-34, CD-36, CD-37, CD-38, CD-42 |
| `CTF.Application.Maps.Rotation` | CD-02, CD-09, CD-11, CD-12, CD-15, CD-31, CD-33, CD-34, CD-36, CD-41, CD-42, CD-43 |
| `CTF.Application.Players` | CD-02, CD-08, CD-11, CD-12, CD-16, CD-17, CD-21, CD-24, CD-31, CD-32, CD-36, CD-43 |
| `CTF.Application.Players.Accounts` | CD-08, CD-09, CD-10, CD-20, CD-44 |
| `CTF.Application.Players.Accounts.Authentication` | CD-08, CD-20, CD-25, CD-31, CD-32, CD-33, CD-36 |
| `CTF.Application.Players.Accounts.Profile` | CD-08, CD-15, CD-20, CD-31, CD-33, CD-36, CD-43, CD-44 |
| `CTF.Application.Players.Accounts.Roles` | CD-09, CD-15, CD-17, CD-20, CD-31, CD-32, CD-33, CD-35, CD-36, CD-43 |
| `CTF.Application.Players.Accounts.Statistics` | CD-06, CD-07, CD-08, CD-09, CD-10, CD-15, CD-17, CD-20, CD-31, CD-32, CD-33, CD-34, CD-35, CD-36, CD-43 |
| `CTF.Application.Players.AntiCBug` | CD-09, CD-14, CD-15, CD-17, CD-31, CD-32, CD-35, CD-36, CD-43 |
| `CTF.Application.Players.Chats` | CD-09, CD-13, CD-15, CD-21, CD-31, CD-32, CD-36, CD-43 |
| `CTF.Application.Players.Chats.Definitions` | CD-09, CD-13, CD-32, CD-36 |
| `CTF.Application.Players.Combos` | CD-05, CD-06, CD-07, CD-09, CD-10, CD-12, CD-15, CD-31, CD-33, CD-34, CD-35, CD-36 |
| `CTF.Application.Players.Combos.Definitions` | CD-03, CD-05, CD-06 |
| `CTF.Application.Players.GeneralCommands` | CD-09, CD-15, CD-17, CD-31, CD-32, CD-33, CD-36, CD-42 |
| `CTF.Application.Players.Headshots` | CD-03, CD-10, CD-17, CD-20, CD-31, CD-32, CD-36, CD-40 |
| `CTF.Application.Players.Pause` | CD-02, CD-31, CD-32, CD-41 |
| `CTF.Application.Players.Ranks` | CD-10, CD-15, CD-33, CD-43 |
| `CTF.Application.Players.TopPlayers` | CD-10, CD-15, CD-17, CD-20, CD-33, CD-36, CD-43 |
| `CTF.Application.Players.Vitalities` | CD-03, CD-09, CD-15, CD-17, CD-31, CD-32, CD-36, CD-43 |
| `CTF.Application.Players.Weapons` | CD-03, CD-04, CD-07, CD-09, CD-15, CD-17, CD-31, CD-32, CD-33, CD-36, CD-43 |
| `CTF.Application.Players.Weapons.Catalogs` | CD-04, CD-17 |
| `CTF.Application.Teams` | CD-02, CD-10, CD-11, CD-15, CD-17, CD-21, CD-31, CD-33, CD-34, CD-35, CD-36, CD-37, CD-38, CD-40, CD-43, CD-44 |
| `CTF.Application.Teams.ClassSelection` | CD-02, CD-08, CD-12, CD-15, CD-17, CD-31, CD-32, CD-34, CD-36, CD-40 |
| `CTF.Application.Teams.Flags` | CD-02, CD-03, CD-07, CD-09, CD-10, CD-15, CD-21, CD-31, CD-37, CD-38, CD-41, CD-44 |
| `CTF.Application.Teams.Flags.AutoReturn` | CD-02, CD-17, CD-37, CD-40, CD-41 |
| `CTF.Application.Teams.Flags.Carriers` | CD-02, CD-09, CD-15, CD-17, CD-31, CD-37, CD-38, CD-40, CD-41 |
| `CTF.Application.Teams.Flags.Events` | CD-02, CD-06, CD-10, CD-17, CD-20, CD-34, CD-35, CD-37, CD-38, CD-40 |
| `CTF.Application.Teams.Matches` | CD-02, CD-31, CD-35, CD-36 |
| `CTF.Application.Teams.Statistics` | CD-02, CD-09, CD-10, CD-15, CD-31, CD-33, CD-34 |
| `CTF.Host` | CD-11, CD-17, CD-21, CD-22, CD-23, CD-24, CD-32, CD-42 |
| `CTF.Host.Extensions` | CD-17, CD-21, CD-22, CD-23, CD-24, CD-32 |
| `CTF.Host.Services` | CD-23, CD-24, CD-25, CD-43 |
| `Persistence.InMemory` | CD-17, CD-18, CD-20, CD-21, CD-25 |
| `Persistence.MariaDB` | CD-17, CD-18, CD-19, CD-20, CD-21, CD-25 |
| `Persistence.SQLite` | CD-17, CD-18, CD-19, CD-20, CD-21, CD-25, CD-30 |
| `Persistence.SQLite.Extensions` | CD-30 |
| `SampSharp` | CD-22, CD-32 |

### Test-project namespaces

| Namespace | Change drivers |
|-----------|----------------|
| `CTF.Application.Tests` | CD-11, CD-22, CD-26, CD-27 |
| `CTF.Application.Tests.Fakes` | CD-11, CD-18, CD-25, CD-28, CD-31 |
| `CTF.Application.Tests.GunGames` | CD-07, CD-26, CD-27 |
| `CTF.Application.Tests.Maps` | CD-11, CD-12, CD-26, CD-27 |
| `CTF.Application.Tests.Players.Accounts` | CD-02, CD-06, CD-08, CD-09, CD-10, CD-26, CD-27, CD-44 |
| `CTF.Application.Tests.Players.Extensions` | CD-08, CD-26, CD-27, CD-32 |
| `CTF.Application.Tests.Players.Ranks` | CD-10, CD-26, CD-27 |
| `CTF.Application.Tests.Players.TopPlayers` | CD-10, CD-17, CD-26, CD-27 |
| `CTF.Application.Tests.Players.Vitalities` | CD-03, CD-26, CD-27 |
| `CTF.Application.Tests.Players.Weapons` | CD-03, CD-04, CD-17, CD-26, CD-27 |
| `CTF.Application.Tests.Teams` | CD-02, CD-10, CD-26, CD-27 |
| `CTF.Application.Tests.Teams.Flags` | CD-02, CD-26, CD-27 |
| `Persistence.Tests.Common` | CD-11, CD-19, CD-20, CD-21, CD-22, CD-25, CD-26, CD-27, CD-30 |
| `Persistence.Tests.Common.DatabaseProviders` | CD-18, CD-19, CD-20, CD-21, CD-25, CD-30 |
| `Persistence.Tests.Players` | CD-18, CD-20, CD-26, CD-27 |

> Note: `SampSharp` here is the `Program.cs` source-generated entry point namespace (not the vendored framework), driven by the host ABI contract (CD-32) and deployment layout (CD-22).

## 3. Per-Element Driver Assignments

> The same drivers are stamped into the C# XML doc comments on each element (type and member). The exhaustive per-element assignment is maintained **directly in the source** (each type and member carries a `<remarks>Change drivers: ...</remarks>` doc comment). The catalogue tables above are the shared vocabulary; see the code annotations for the element-by-element mapping.

---

## 4. Notes on Identification Methodology

- **Counterfactual grounding.** Every driver above survives the counterfactual test: removing the referenced artifact condition eliminates the element's modification requirement. No driver was invented; each traces to a read artifact (README rules, `.env.example`, `schema.sql`, platform API usage, config sections).
- **No proxy reasoning.** Co-variation (files changed together), team ownership, layer uniformity, and semantic similarity were NOT used as drivers. Drivers are the external authorities themselves.
- **No ranking.** No "primary"/"secondary"/"main" vocabulary is used; all drivers are equal forces on the elements that respond to them.
- **Not eliminated by design.** Encapsulation/abstraction bounds blast radius but never eliminates a driver; e.g. the platform sub-drivers (CD-31..CD-44) and the game rules (CD-02) drive elements regardless of how they are factored.
- **Scope.** Project C# code (`src/Application`, `src/Host`, `src/Persistence`) plus the test projects (`tests/Application.Tests`, `tests/Persistence.Tests`) are analysed. The vendored `external/SampSharp` framework is excluded.
