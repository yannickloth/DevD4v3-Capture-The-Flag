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

---

## Appendix A — Essential composite elements (per-element citation)

The analysis in `before/analysis.md` §C.1 splits co-located driver-set mixes into *essential* (decreed by an axis) vs *spurious* (change-coupling scatter). This appendix lists **every essential composite element** and the exact axis that decrees its driver-set union, so the split is a citation, not a name heuristic.

| Composite element | Driver-set union (CD) | Decreed by (axis) | Rationale |
|---|---|---|---|
| `CTF.Host.Startup`, `CTF.Host.GameModeInit` | CD-01 + CD-17 + CD-21 + CD-23 + CD-24 | Composition/DI (CD-21) | The composition root must reference every driver's services to wire the object graph; no split reduces its union without breaking wiring. |
| `CTF.Host.Program` / `SampSharp.Entrypoint` | CD-01 + CD-22 | Platform (CD-01) | The unmanaged ABI entry point and source-generated bootstrap are fixed by the SampSharp host; cannot be regrouped. |
| `IPlayerRepository`, `ITopPlayersRepository` | CD-20 (+ CD-18/CD-10 per method) | Persistence (CD-20) | Outbound ports sit at the hexagon boundary by dependency-inversion; their per-method union is the repository contract, not a grouping choice. |
| `Persistence.InMemory/MariaDB/SQLite` providers | CD-17 + CD-18 + CD-20 + CD-21 + (CD-19 \| CD-30) | Persistence/storage (CD-19/CD-30/CD-20) | One adaptor per DBMS; each provider must implement the full port + its dialect. Dialect is the only per-provider difference (CD-19 MariaDB vs CD-30 SQLite). |
| `IGunGameMode` + its consumers (`WeaponCatalogSystem`, `ComboSystem`, `PlayerKillingSpreeUpdater`, `PlayerRankUpdater`) | CD-07 + (CD-04/06/10) | Game (CD-07) | GunGame is a deliberate cross-cutting gate that suspends/replaces parts of weapons/combos/coins/stats; the co-location is a game rule. |
| Flag event handlers (`OnFlagScore`, `OnFlagCaptured`, …) | CD-02 + CD-06 + CD-10 + CD-20 | Game (CD-02) | A single game event (flag score) drives several reward systems (coins, stats, persistence) by rule — game-designed composite, not grouping error. |
| `Messages.Designer.cs`, `GunGameMessages.Designer.cs`, `DetailedCommandInfo.Designer.cs` | CD-17 (+ CD-15) | Generated-code (CD-17) | Tool-generated from `.resx`; shape fixed by the resource tooling, annotation-only. |
| Test fakes (`FakePlayer`, `FakeCarrier`, `FakeMap`, `FakePasswordHasher`, the `*RepositoryManager`s) | CD-01/11/20/25 (+ CD-28/29) | Test/code-under-test (CD-29) | A fake is driven by the seam it mimics (mock-inheritance rule); its union is the mocked contract, not a domain grouping. |
| `DatabaseProviderExtensions`, `RepositoryManagerFactory`, `DatabaseProvider` (test enum) | CD-17 + CD-19 + CD-30 + CD-21 | Persistence dispatch (CD-19/CD-30) | The provider dispatch is the storage axis's switch; it necessarily carries both SQL dialects. |

Every *other* co-located driver-set mix in `before/analysis.md` §C.1 (`Players`, `Accounts.Statistics`, `Teams`, `Teams.Flags`, `Maps`, `Weapons`, `Combos`, `GunGames`, `Chats`, …) is **spurious** — grouped by topic, with no decreed axis forcing the mixture — and is therefore the legitimate object of the IVP regroup.
