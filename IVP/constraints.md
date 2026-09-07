# Constraints on Applying IVP

Sibling of `changedrivers.md`. Documents the constraints under which IVP is applied, explaining why the current packages / modules / deployment units are shaped the way they are, and what IVP may and may not move.

## Scope of IVP

IVP (the Independent Variation Principle) is granularity-agnostic. It ranges from the finest grain (a single element) up to systems of systems. There is no "correct" granularity embedded in the principle: at every level the same rule holds — elements with identical change-driver sets belong together; elements with differing driver sets belong apart.

- A **class** and a **composite type** are themselves modules. Two members with different driver sets inside one class are themselves a contamination defect *at the class level*.
- A **namespace** is a module at the file-assembly level.
- A **project / assembly** is a module at the build level.
- A **deployable unit** (the gamemode `.dll`, the persistence providers) is a module at the deployment level.

Consequence: IVP never fixes a single boundary level. The same driver-set comparison can be run at the member level, the type level, the namespace level, the assembly level, and up to the systems-of-systems level. Each level yields its own partition; the levels compose, they do not override one another.

## IVP is one axis among many

IVP governs **one** decomposition axis: change coupling. It is the optimal target *for that axis only*. It does not, and cannot, override or ignore constraints from other axes. The paper states these other axes explicitly: security, deployment topology, performance, team allocation (and here: platform, persistence, and game rules).

When two axes recommend different groupings, the result is a **composition** problem, not a victory for one axis. IVP supplies the change-cost "north star": the grouping you would choose if only change cost mattered. Every deviation is then measurable — a quantified impurity (contamination, different drivers co-located) or incompleteness (scatter, same driver split) accepted in exchange for the other axis's constraint.

## The constraints (irreducible axes) in this codebase

These explain why certain modules / packages / deployment units exist regardless of their change-driver sets. They are **not** change-coupling scatter; they are decreed by another axis and are out of IVP's reach.

### 1. Platform axis — CD-01 (open.mp / SampSharp)

- The host bootstrap (`CTF.Host`, `Program.cs`) is bound to the SampSharp `StartupContext` / unmanaged ABI entry point. This is platform-decreed: the entry point *must* live in the deployable gamemode assembly; it cannot be regrouped by change-driver set.
- `CTF.Host` and `CTF.Application` are separated by the platform's ECS/system + DI bootstrap contract, not by change coupling. Their separation is a deployment/plugin-structure fact, not an IVP optimization.

### 2. Persistence axis — CD-19 / CD-30 / CD-20 (SQL dialect + repository contract)

- The three providers — `Persistence.InMemory`, `Persistence.MariaDB`, `Persistence.SQLite` — each implement the same outbound ports (`IPlayerRepository`, `ITopPlayersRepository`). Their separation is decreed by the **storage-technology** axis (one adaptor per DBMS), not by change coupling.
- The outbound **ports themselves** (`IPlayerRepository`, `ITopPlayersRepository`) live in `CTF.Application` as the hexagon's boundary. That placement is a dependency-inversion / architecture fact, not a change-driver decision.

### 3. Composition / DI axis — CD-21 (DI container)

- The composition root (`Startup`, all `ServiceCollectionExtensions`) necessarily references *every* driver's services to wire the object graph. It is an **irreducibly composite** element: it has a union driver set by construction, and no IVP split can reduce it without breaking wiring. This is the shared-driver case the paper's theorem flags (no strict uniqueness for irreducible composites).

### 4. Game axis — CD-02 / CD-07 (game rules, GunGame gate)

- `IGunGameMode` is a deliberate **cross-cutting gate**: GunGame suspends/replaces parts of weapons, combos, coins, and stats. That coupling is a real game rule (the optional mode overlays the base mode), not accidental scatter.
- Interactions where a flag score also awards coins/score/rank (CD-06 + CD-10 + CD-02 co-occurring on one handler) are **game-designed composites** — a single game event drives several reward systems by rule, not by grouping error.

### 5. Generated-code axis — CD-17 (*.Designer.cs)

- `Messages.Designer.cs` and the command-info resource classes are tool-generated from `.resx`. They cannot be regrouped or decomposed; their shape is fixed by the resource tooling. They are annotation-only (the class-level remark covers all generated members).

## What IVP may move

Everything *not* covered above is change-coupling grouping, and is IVP's legitimate object:

- Game-rule logic (CD-02) that is scattered across namespaces for historical reasons, not for the above axes.
- Coin economy (CD-06), player statistics/rank (CD-10), chat rules (CD-13), anti-cheat (CD-14), command set (CD-15), map config (CD-11) and rotation (CD-12) — to the extent they are grouped by topic rather than by a decreed axis.

## How the metric must be read

Given the above, the module-touch count $\kappa(\gamma)$ for a driver is **not** minimized to its absolute floor. It is minimized subject to the axes above. Therefore any before/after report must decompose $\kappa(\gamma)$ into:

- **Decreed touch** — modules the driver must touch because of platform / persistence / composition / game / generated-code axes (irreducible under IVP).
- **Change-coupling (removable) touch** — modules touched *only* because of topic-based grouping; this is the scatter IVP would remove.

Only the second quantity is the IVP optimization target. The first is a constant, explained by this document.

## Granularity of the measurement elements

- A "module" for the count is level-dependent: member → class (composite type), class → namespace, namespace → assembly/project, assembly → deployable unit. The same drivers measured at different levels give different $\kappa(\gamma)$; all are valid IVP readings.
- The default measurement boundary for the before report is the **namespace** (physical grouping as committed), with the class-level and assembly-level readings derivable by the same rule.

## Composition rule (when axes disagree)

IVP states the change-coupling grouping; the other axis states its grouping. The composition is recorded symmetrically: both recommendations stated, the chosen composition stated, and the accepted impurity/incompleteness quantified. No silent override, no silent demotion of either axis.

## Namespace-layer override (namespace single-set rule)

At the **namespace** layer the strict IVP rule applies without exception: every top-level class must live in a namespace whose top-level classes share exactly one change-driver set. The "irreducible essential composite" exemptions above apply at the **assembly / deployable-unit** layer only, not at the namespace layer. Where a provider or fixture set was previously cited as an essential composite, its *namespace* is now recursively split into exact-set sub-namespaces; the composite boundary moves down to the assembly/deployment level, which still keeps MariaDB / SQLite / InMemory / the repository ports / generated resources together as decreed by their axis.

Consequences applied in two passes (commit-per-change, verified by `dotnet build` 0 errors + 694 Application tests):

*Pass 1 — flatten each namespace's top-level classes into exact-set sub-namespaces (single top-level type per namespace).*
*Pass 2 — **causal nesting**: no namespace is nested under a parent whose root causality differs. Independent roots (config CD-17, composition CD-21, ports CD-20/CD-08, weapon catalogs CD-04, map rotation CD-12) are no longer hosted as children of an unrelated single-root topic namespace. Topic shells that grouped independent driver sets were dissolved; each class now sits in the namespace for its own change-driver root.*

Pass-2 restructures (all committed):

| Old nesting (violation) | New structure |
|---|---|
| `CTF.Application.Players` (CD-21 shell holding `PlayerServicesExtensions`) ⊃ `Accounts`(CD-08), `Settings`(CD-17), `TopPlayers`(CD-20), `Weapons`(CD-04) | `Players` dissolved. `PlayerServicesExtensions`,`ChatServicesExtensions` → `CTF.Application.Composition` (CD-21). `IPlayerRepository` → `Accounts` (CD-08). `CommandCooldowns`,`TopPlayersSettings` → `Configuration` (CD-17). `ITopPlayersRepository` → `Statistics.TopPlayers` (CD-10). Weapon runtime → `WeaponCatalogs` domain (CD-04). |
| `Players.TopPlayers`(CD-20) ⊃ `Settings`(CD-17) | `TopPlayersSettings` → `Configuration` (CD-17) |
| `Players.Weapons` ⊃ CD-04 sub-namespaces | moved under `WeaponCatalogs` domain root (`WeaponCatalogs`, `.ActiveCatalog`, `.System`, `.Catalogs`, `.Catalogs.Settings`) |
| `CTF.Application.Teams`(CD-02) ⊃ `Composition`(CD-21) | `TeamServicesExtensions` → `CTF.Application.Composition` (CD-21) |
| `CTF.Application.GunGames` root CD-17 (generated `GunGameMessages`) ⊃ CD-07 children | `GunGameMessages` re-rooted CD-07 (GunGame-mode messages); `GunGames` single root CD-07 |
| `Audio`(CD-40) ⊃ `Audio.Configuration`(CD-17) | `TeamSoundCatalog` → `CTF.Application.Configuration` (CD-17) |
| `GameRules`(CD-02) ⊃ `GameRules.Configuration`(CD-17) | `ClassSelectionSettings` → `CTF.Application.Configuration` (CD-17) |
| `Maps`(CD-11) ⊃ `Maps.Rotation`(CD-12) | `MapRotation` promoted to sibling CD-12 root (`CTF.Application.MapRotation`); test mirror → `Tests.MapRotation` |
| `Tests.Fakes`(CD-31) ⊃ `Fakes.Maps`(CD-11) | `FakeMap` → `CTF.Application.Tests.Maps` (CD-11) |
| `Tests.Players.Weapons`(CD-03) ⊃ `.Catalogs`(CD-04) | catalog tests → `CTF.Application.Tests.WeaponCatalogs` (CD-04) sibling root |

New single-root grouping namespaces introduced in pass 2:
- `CTF.Application.Composition` — pure CD-21 DI wiring (`PlayerServicesExtensions`, `ChatServicesExtensions`, `TeamServicesExtensions`).
- `CTF.Application.Configuration` — CD-17 config root (`TeamSoundCatalog`, `ClassSelectionSettings`, `CommandCooldowns`, `TopPlayersSettings`).
- `CTF.Application.MapRotation` / `CTF.Application.Tests.MapRotation` — CD-12 map-rotation domain.
- `CTF.Application.Tests.WeaponCatalogs` — CD-04 weapon-catalog tests.

Pass-1 splits (per-namespace single top-level set) that remain in force:

| Namespace split | Resulting single-set sub-namespaces |
|---|---|
| `CTF.Application.Tests.Players.Accounts` | split by sub-entity under test into `.Account`, `.Role`, `.Team`, `.FlagCounter`, `.StatsPerRound`, `.Core` |
| `Persistence.InMemory` | root holds the DI ext (CD-21); `.Models` (`FakePlayer`,`FakePlayerSeedData`), `.Repositories.Players`, `.Repositories.TopPlayers`, `.Ids` (`PlayerIdValueGenerator`) |
| `Persistence.MariaDB` | root holds `PersistenceMariaDBServicesExtensions` (keeps the `ns/sql` loader path); `.Settings`, `.Schema`, `.Repositories.Player`, `.Repositories.TopPlayers` |
| `Persistence.SQLite` | root holds `PersistenceSQLiteServicesExtensions`; `.Settings`, `.Schema`, `.Repositories.Player`, `.Repositories.TopPlayers` (plus pre-existing `.Extensions` CD-30) |
| `Persistence.Tests.Common` | `.Contracts` (`DatabaseProvider`,`IRepositoryManager`), `.TestCases`, `.Factory`, `.Paths`, `.PasswordHasher` |
| `Persistence.Tests.Common.DatabaseProviders` | `.InMemory`, `.MariaDb`, `.Sqlite` |

Each persistence provider keeps its assembly/deployable unit (its dialect × port union stays an essential composite **at the assembly level** per §2/§5); only the namespace layer is now single-set.

Intentionally left as cross-root nesting (assembly/axis-decreed, not topic grouping):

| Nesting | Reason |
|---|---|
| `CTF.Application` (CD-17, generated `Messages.Designer.cs`) hosting all domain namespaces | Assembly root namespace; the shared generated message resource is irreducibly CD-17 (generated-code axis §5). Mirrors the prior accepted state. |
| `CTF.Application.Tests` (CD-22) hosting test namespaces | Assembly root of the test project. |
| `Persistence.InMemory/MariaDB/SQLite` root (CD-21 DI ext) with CD-17/18/20/30 sub-namespaces | Each provider is a separate-assembly composition root whose DI extension must wire every service in the assembly (composition axis §3 + persistence axis §2). Its sub-namespaces are each single-set; the composite boundary is the assembly/deployable unit. |
| `CTF.Application.Teams` ⊃ `Teams.Ids` | `TeamId`'s root set (CD-02 + CD-31) is a subset of `Team`'s own root set (CD-02 + CD-31 + CD-44); the child shares the parent's causal root, so nesting is consistent, not a differing-root violation. |

---

## Appendix A — Essential composite elements (per-element citation)

The analysis in `before/analysis.md` §C.1 splits co-located driver-set mixes into *essential* (decreed by an axis) vs *spurious* (change-coupling scatter). This appendix lists **every essential composite element** and the exact axis that decrees its driver-set union, so the split is a citation, not a name heuristic.

| Composite element | Driver-set union (CD) | Decreed by (axis) | Rationale |
|---|---|---|---|
| `CTF.Host.Startup`, `CTF.Host.GameModeInit` | CD-01 + CD-17 + CD-21 + CD-23 + CD-24 | Composition/DI (CD-21) | The composition root must reference every driver's services to wire the object graph; no split reduces its union without breaking wiring. |
| `CTF.Host.Program` / `SampSharp.Entrypoint` | CD-01 + CD-22 | Platform (CD-01) | The unmanaged ABI entry point and source-generated bootstrap are fixed by the SampSharp host; cannot be regrouped. |
| `IPlayerRepository`, `ITopPlayersRepository` | CD-20 (+ CD-18/CD-10 per method) | Persistence (CD-20) | Outbound ports sit at the hexagon boundary by dependency-inversion; their per-method union is the repository contract, not a grouping choice. Each port is now nested under the root of the entity it persists: `IPlayerRepository` under `Accounts` (CD-08), `ITopPlayersRepository` under `Statistics.TopPlayers` (CD-10), with CD-20 kept as the subordinate repository-contract driver. The provider implementations remain the persistence axis's per-DBMS adaptors at the assembly level. |
| `Persistence.InMemory/MariaDB/SQLite` providers | CD-17 + CD-18 + CD-20 + CD-21 + (CD-19 \| CD-30) | Persistence/storage (CD-19/CD-30/CD-20) | One adaptor per DBMS; each provider must implement the full port + its dialect. Dialect is the only per-provider difference (CD-19 MariaDB vs CD-30 SQLite). |
| `IGunGameMode` + its consumers (`WeaponCatalogSystem`, `ComboSystem`, `PlayerKillingSpreeUpdater`, `PlayerRankUpdater`) | CD-07 + (CD-04/06/10) | Game (CD-07) | GunGame is a deliberate cross-cutting gate that suspends/replaces parts of weapons/combos/coins/stats; the co-location is a game rule. |
| `GunGameRewardTable` / `GunGameRewardGrant` (split by knowledge kind, see `6172ac3f` + follow-up) | Table: CD-07 (pure). Grant: CD-07 + CD-06 + CD-10 + CD-31 + CD-36 (uniform) | Game (CD-07) | The reward table (amounts + weapon options) is governed solely by CD-07 and is single-set pure. The grant flow is one orchestration that dispenses the reward through stable internal contracts (coin balance, stats renderer, player surface, client message); every member of the grant type carries the same uniform set. A mode-owned port layer over those engaged contracts was evaluated and rejected for now: they are internal, stable seams — if they churn independently of the mode, the port earns its place. Documented composite. |
| Flag event handlers (`OnFlagScore`, `OnFlagCaptured`, …) | CD-02 + CD-06 + CD-10 + CD-20 | Game (CD-02) | A single game event (flag score) drives several reward systems (coins, stats, persistence) by rule — game-designed composite, not grouping error. |
| `Messages.Designer.cs`, `GunGameMessages.Designer.cs`, `DetailedCommandInfo.Designer.cs` | CD-17 (+ CD-15) | Generated-code (CD-17) | Tool-generated from `.resx`; shape fixed by the resource tooling, annotation-only. Domain-scoped message resources are rooted at their owning domain (`DetailedCommandInfo` → CD-15 command set; `GunGameMessages` → CD-07 GunGame mode) so the namespace root stays single; only the assembly-wide `Messages.Designer.cs` stays CD-17 at the `CTF.Application` root. |
| Test fakes (`FakePlayer`, `FakeCarrier`, `FakeMap`, `FakePasswordHasher`, the `*RepositoryManager`s) | the mocked contract's domain driver (CD-01/11/20/25, CD-28 tooling subordinated) | Test/mocked-contract domain | A fake is driven by the domain driver of the seam it mimics (mock-inheritance rule). Namespace layer split by faked domain: player fakes under `Tests.Fakes` (CD-31); `FakeMap` (CD-11) now under `Tests.Maps`; persistence fakes split under `Persistence.*`; the union remains only at the class/assembly level. |
| `DatabaseProviderExtensions`, `RepositoryManagerFactory`, `DatabaseProvider` (test enum) | CD-17 + CD-19 + CD-30 + CD-21 | Persistence dispatch (CD-19/CD-30) | The provider dispatch is the storage axis's switch; it necessarily carries both SQL dialects. `RepositoryManagerFactory` / `DatabaseProvider` now split into single-set namespaces (`Persistence.Tests.Common.Factory` / `.Contracts`); the dispatch composite remains at the assembly level. |
| `CTF.Application.GameRules.Flag.FlagState.Flag` | CD-02 + CD-44 (flag model id) + CD-38 (flag map icon) + CD-39 (carrier attached-object) | Game (CD-02) | A single flag runtime object owns its identity (3D model id, radar icon id — platform resources) and its carrier-attachment rendering, alongside the flag game rules (capture/take/drop/return). These platform resources are the *flag's own identity*, not a separable subsystem: you cannot split the carrier-attachment rendering out of `Capture`/`Take` without breaking `SetCarrier`/`RemoveCarrier` state ownership. Intentional single-runtime-object composite. |
| `CTF.Application.Teams.Team`, `Team.NoTeam` | CD-02 + CD-44 (team skin id) + CD-40 (team sounds) + CD-35 (GameText color) + CD-34 (member-count text) + CD-10 (per-round team stats) | Game (CD-02) | The central team aggregate holds interlinked static instances (`Alpha` ↔ `Beta` rival, `None`) and one team's full identity+state+flag+members+sounds. Its members span the team's skin id, sound resources, GameText/textdraw display strings, and per-round stats — all platform/game domains on one object with shared static wiring. Splitting by platform domain fragments one team's interlinked instances. Intentional single-runtime-object composite. |
| `CTF.Application.Statistics.Score.Hud.PlayerStatsRenderer` | CD-10 + CD-31 (player surface) + CD-32 (ECS component) + CD-34 (textdraw API) | Render-bridge (CD-34 × CD-10) | A renderer necessarily bridges the content domain (player statistics, CD-10) and the rendering platform (textdraw API + ECS component, CD-34/CD-32): `CreateTextDraw`/`Show`/`Hide` are CD-34 lifecycle, `UpdateTextDraw` renders CD-10 content into the textdraw, `GetStatsAsText` formats CD-10 content. The bridge is the type's single responsibility; you cannot make it single-set without fragmenting the one `PlayerStatsTextDraw` component it manages across `Create`/`Update`/`Show`. Intentional content↔platform render-bridge composite. |

Every *other* co-located driver-set mix in `before/analysis.md` §C.1 (`Players`, `Accounts.Statistics`, `Teams`, `Teams.Flags`, `Maps`, `Weapons`, `Combos`, `GunGames`, `Chats`, …) is **spurious** — grouped by topic, with no decreed axis forcing the mixture — and is therefore the legitimate object of the IVP regroup.
