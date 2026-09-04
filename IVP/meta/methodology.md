# Methodology — Identifying the Change Drivers of an Element or Module

> How to determine, document, and then **apply** the change-driver model to any element (type/member) or module (class/namespace/assembly). Companion to `invariants.md`. Terminology follows the IVP book series (`cohesion-2-nature`, `cohesion-4-causal`). Apply-phase rules below were distilled from the ongoing `-ivp` worktree refactor (`/home/nicky/code/Capture-The-Flag-ivp`, branch `apply-ivp`), which evolved from **type-level** regrouping → **member-level** application → **placement by root driver** (§6.2), **same-table entity decomposition** (§6.4), **sibling/nested module grouping** (§6.5), the **nested-module non-transmission rule** (§5), and the **CD-01 → CD-31..CD-44 decomposition** (§4).

## 1. Definitional core

| Rule | Statement |
|---|---|
| **Driver** | An **external forcing condition in the operating domain** that, when it changes, creates a requirement for an element to be modified, via a documentable step-by-step pathway anchored in a domain artifact (statute, contract, spec, standard, config schema). |
| **Counterfactual test** | *If the anchoring artifact changed to remove the relevant condition, would the element's modification requirement disappear or shift?* No artifact → no driver. |
| **No proxy reasoning** | Co-variation (files changed together), team ownership, layer uniformity, semantic similarity are **never** drivers. Only the external authority itself. |
| **No ranking** | "primary/main/secondary" is forbidden; all drivers are equal. Rarity/frequency informs isolation effort, never identity. |
| **Anchoring** | Every driver is anchored in a concrete verifiable artifact (README rule, `.env.example`, `schema.sql`, platform API, config section). |
| **Not eliminated by design** | Encapsulation/abstraction bounds blast radius but never removes a driver. |

## 2. Per-element identification (the task)

For **every** type/class/enum/record and **every** public/internal member (method, property, field, event, constructor, enum member):

1. Read the file and every file it reaches/relates to. Exhaustive — never skip a member.
2. For each element report: fully-qualified name (namespace + type + member).
3. State each driver as *"driver X creates a change requirement for this element because [specific pathway]; anchored in [artifact]"* — name the **specific rule**, not a vague category.
4. Select the driver by the code's *behaviour*, not its grouping (see map §4).
5. Include **only** drivers that actually force change; never add every driver to everything.
6. **Mock/dependency rule:** a mock/fake inherits the domain drivers of the *seam it mimics* (the mocked interface or type+method-signature surface), not the seam's internal drivers. A DI-injection point is driven by the injected type's own domain drivers (+ CD-21 wiring).

## 3. Annotation (how a driver is stamped)

- Add `<remarks>Change drivers: …</remarks>` inside/after the element's existing doc comment — one remark per element (type + each member). **No code changes.**
- File-scoped namespaces (`namespace X.Y;`) cannot carry `///` comments → namespace→driver map is kept in `changedrivers.md` §2 as the single source of truth (union of contained types' sets).
- Causal notation **augments**, never replaces, the flat codes:
  - `(root)` — causal head, no upstream predecessor.
  - `X → Y (reason)` — *subordinated* / "exists because of", with existence reason.
  - `X ‖ Y` — unordered siblings (neither causes the other); never chain siblings.
  - Single-driver elements need no arrow (their only driver is their root by default).
- Order clauses causally: root(s) first, then subordinated machinery in causal depth.
- `Injected dependencies` remarks are **supplementary and separate** — never contribute to the element's Γ-set.

### Example (AccountAuthenticator, as annotated)
```
<remarks>Change drivers:
  CD-08 (root);
  CD-20 → CD-08 (subordinated: persistence exists because the account exists);
  CD-25 → CD-08 (subordinated: hashing protects the password);
  CD-01 → CD-08 (platform mechanism).</remarks>
<remarks>Injected dependencies (change drivers of these elements):
  passwordHasher -> CD-25; playerRepository -> CD-20. Each injection is driven by the
  contract of its injected type + CD-21 (DI wiring).</remarks>
```

## 4. Driver-selection map (this codebase)

| Behaviour in code | Driver |
|---|---|
| game-rule logic (flag/match/round/team/score) | CD-02 |
| combat/weapon rules | CD-03 |
| weapon-catalog config | CD-04 |
| combo definitions | CD-05 |
| coin economy | CD-06 |
| GunGame gate | CD-07 |
| account/auth | CD-08 |
| authorization gating / role | CD-09 |
| player statistics / rank | CD-10 |
| map values | CD-11 |
| rotation | CD-12 |
| chat rules | CD-13 |
| anti-cheat policy | CD-14 |
| command set | CD-15 |
| RCON security | CD-16 |
| config-bound settings (+ domain driver if value feeds game logic) | CD-17 |
| database schema / player data model | CD-18 |
| MariaDB SQL dialect | CD-19 |
| outbound repository contract | CD-20 |
| DI container / composition | CD-21 |
| hosting / deployment | CD-22 |
| Serilog logging | CD-23 |
| Discord webhook | CD-24 |
| BCrypt hashing | CD-25 |
| NUnit test-framework contract | CD-26 |
| FluentAssertions contract | CD-27 |
| NSubstitute mock contract | CD-28 |
| SQLite SQL dialect | CD-30 |

### 4.1 Platform sub-drivers (CD-01 → CD-31..CD-44)

> The monolithic platform umbrella **CD-01 was DECOMPOSED** (kept for history; **no element may cite CD-01**): its subsystems vary independently (an attached-object change never forces a textdraw edit). Replace a bare `CD-01` clause with the specific sub-driver(s) the element actually uses — read the element's body; multiple allowed; keep arrows. (CD-29-retirement precedent.)

| Code | Platform sub-driver |
|---|---|
| CD-31 | Player entity & lifecycle events (OnPlayerConnect/…/Text/TakeDamage), player state & class APIs |
| CD-32 | ECS runtime (`ISystem`, `Component`, `[Event]`, `IEntityManager`, `IEcsStartup`, middleware) |
| CD-33 | Dialog API (`IDialogService`) |
| CD-34 | Textdraw API |
| CD-35 | GameText API |
| CD-36 | Client-message API (`SendClientMessage`) |
| CD-37 | Pickup API |
| CD-38 | Map-icon & radar API |
| CD-39 | Attached-object API (`SetAttachedObject`: bone/offset/rotation/scale/material color) |
| CD-40 | Audio API (`PlayAudioStream`; the `.env` audio URLs stay CD-17) |
| CD-41 | Timer API (`ITimerService`) |
| CD-42 | Server-service API (`IServerService`) |
| CD-43 | Command infrastructure (`[PlayerCommand]`, formatters, permission checker) |
| CD-44 | Model & skin id resources (object-model ids, skins, `FlagModel`/`FlagIcon`/`SkinTeamId`) |

Typical resolution: `[PlayerCommand]` handler → CD-43 + the APIs its body calls (`SendClientMessage`→CD-36, `GameText`→CD-35, dialogs→CD-33, `GetComponent`→CD-32, `player.Team/GiveWeapon/Kick`→CD-31). `[Event] OnPlayerX` → CD-31 + body APIs. Renderers → CD-34; Components → CD-32; skin/model/id holders → CD-44. Spurious platform clauses where no platform API exists are dropped.

> **CD-29 retired** (methodology error): being *depended upon* is structural, not a driver.

## 5. Causal-order (module/namespace level)

1. Determine each driver's role: **root** (independent authority) | **subordinated** (`→`, exists only because of the root) | **sibling** (`‖`, unordered).
2. Resolve role by the **existence counterfactual**: *would this element/feature exist without the superior mechanism/root?* No → subordinated; yes → sibling root.
3. Canonical resolutions observed here:
   - `CD-09 roles → CD-08`, `CD-10 stats → CD-08`, `CD-20/18/25 → CD-08` (account entity).
   - `CD-19 (MariaDB) ‖ CD-30 (SQLite)`; both `→` schema/entity.
   - `CD-18 schema ‖ CD-20 repo contract`, both `→ CD-08`.
   - `CD-08 ‖ CD-02` — account vs game rules are independent sibling roots.
   - Game-domain roots are **siblings**: `CD-07 ‖ CD-03 ‖ CD-02 ‖ CD-06 ‖ CD-10 ‖ CD-04 ‖ CD-05 ‖ CD-11 ‖ CD-12 ‖ CD-13 ‖ CD-14 ‖ CD-15` — none causes another to exist.
4. **Platform (CD-01) is decomposed** (CD-31..CD-44, §4.1) — resolve each element to the specific platform sub-driver(s) it uses; at the Host bootstrap (`Program.cs`/`Entrypoint`) the host/ECS driver is genuinely a root, elsewhere platform use is a subordinated mechanism (`CD-3x → <domain root>`).
5. **Test tooling** `CD-26/27/28 →` the asserted domain root of the code under test.
6. Never chain siblings (order exists only where one realizes the other).
7. **Nested-module non-transmission rule:** the containment law holds **between a module and its child modules** (a child exists only if the parent exists), but a parent module does **not** adopt its nested modules' subordinate driver sets the way it does a bare element's. A nested class (itself a module) may carry subordinates (e.g. `CarrierAttachment` with CD-39) that never appear on the parent (`Flag` stays pure `{CD-02}`) without violating containment. Only a *root* difference forces separation. *"Nested modules don't transmit their change driver set to their parent module like elements do."*

## 6. Applying the IVP (regrouping into modules)

> Distilled from the `-ivp` worktree refactor. This step turns the annotated driver model into a new physical grouping. Two module kinds exist: a **namespace** (contains types) and a **type** (contains members); both can nest. The refactor proceeds in two granularities: first **type-level** regrouping (whole types into flat modules), then **member-level** application (each attribute/function placed in its right module).

| Rule | Statement |
|---|---|
| **Grouping criterion** | Module membership is decided by the causal **root** driver, constrained by the containment law — **not** by an identical flat driver set. Group at the granularity that matches the driver difference (member → type → namespace → assembly → deployable). |
| **Containment law** | `Γ_exist(parent) ⊆ Γ_exist(child)`. Descending, driver sets only grow: `namespace-set ⊆ class-set ⊆ member-set`. A child exists only because its parent exists, so it may add drivers but never lacks one its parent has. *Refined by the nested-module rule (§5.7): nested **modules** don't transmit their driver set up to the parent the way elements do.* |
| **Root vs subordinated on an element** | A class with extra drivers that are *subordinates* (`X → module root`) belongs in that root's module. Only a type whose added driver is an **independent sibling root** moves out to that root's module. |
| **Type-as-module vs namespace** | A multi-type driver group = a **namespace-module**. A **single-type** driver group = a **class-module nested inside a top-level namespace-module** (not a namespace holding one type). A cohesive class holding attributes + methods is itself the module boundary. |
| **Flat top-level layout** | Each pure module is a **flat top-level sibling** under `CTF.Application`, named after its root driver's domain (see catalogue below), not nested under `Players`/`Teams`. |
| **Naming** | Module name = the driver's domain. A namespace must not equal a type name (C# collision) — e.g. the CD-07 module was named `GunGameRules`, not `GunGame`, to avoid shadowing the `GunGame` type. |
| **Nested types travel with their file** | Private/nested types (e.g. `NoTeam` in `Team.cs`) are not separate moveable modules; verify from source, never split them from the host file. |
| **Member-level application** | After the type-level regroup, IVP is applied **inside types**: every non-dependency attribute/function is reviewed, its change driver assigned, and it is placed in the **right module** (its own type, or moved to a type in the driver's module). See §6.2. |
| **Aggregates: move-out vs same-table split** | A *coherent-entity* member (columns/state mutators) stays on its entity; *foreign-domain* query/formatting/computation members move to their root's owning type. If a **persisted row** mixes columns driven by different authorities, model it as **multiple entities sharing one table** (§6.4) rather than one nested pseudo-object. An apparent member-root divergence may mean a missed driver. |
| **Never delete** | Move / refactor, **never delete** an element or code. Verify integrity via git (files tracked as renames `R`, complete). |
| **Irreducible composites** | Multi-root entity cores with no single causal root are **left as single modules** — forcing them flat misapplies IVP (see `constraints.md` Appendix A). |
| **Assembly policy** | New assemblies are **not** warranted by driver differences alone — choose the smallest sufficient boundary (sub-unit / class / file / module). Only add an assembly when a second axis (deployment, security, team, performance) *also* justifies the split; then record both grounds. |
| **Verification gate** | After every move: change only each file's `namespace X;` line, update `Usings.cs` in every referencing assembly, build all projects + run all tests, ensure **no element/code removed**. |

### Module catalogue (this refactor)

| Root driver | Top-level module |
|---|---|
| CD-02 (game rules) | `GameRules` |
| CD-03 (combat) | `Combat` |
| CD-04 (weapon catalog) | `WeaponCatalogs` |
| CD-05 (combo) | `Combos` |
| CD-06 (coin economy) | `CoinEconomy` |
| CD-07 (GunGame) | `GunGameRules` |
| CD-08 (account) | `Accounts` |
| CD-09 (authorization) | `Authorization` |
| CD-10 (statistics) | `Statistics` |
| CD-11 (map config) | `Maps` *(already top-level)* |
| CD-13 (chat) | `Chat` |
| CD-14 (anti-cheat) | `AntiCheat` |
| CD-15 (command set) | `Commands` |
| CD-31/32/34/37/38/40/43/44 (platform sub-drivers) | `Platform` dissolved into exact-root modules: `TextDraws` (CD-34), `Pickups` (CD-37), `MapIcons` (CD-38), `Audio` (CD-40), `PlayerResources` (CD-44), `CommandInfrastructure` (CD-43), ECS (CD-32) |

`CTF.Host` (deployment assembly) follows the same per-driver scheme under `CTF.Host.*`, dissolved along the refined roots: `Ecs` (CD-32), `ServerService` (CD-42), `CommandInfrastructure` (CD-43), plus `Composition` (CD-21), `Config` (CD-17), `Logging` (CD-23), `Deployment` (CD-22), `Discord` (CD-24), `Bcrypt` (CD-25). `Startup`/`Program` (composition roots) stay irreducible.

### 6.1 Hidden-driver seams to audit when regrouping

| Seam | Hidden driver | Check |
|---|---|---|
| Component seam | `: Component` subclasses must carry the ECS sub-driver **CD-32** (`CD-32 → root`) | audit Components |
| Command-system seam | `[PlayerCommand]` methods + `[RequiresMinimumRole]` → **CD-43** and **CD-09** | audit command classes |
| Platform-event seam | `[Event]`/component-adding methods on a **domain system** marked platform-rooted should be domain-rooted with the used sub-driver subordinate | audit event handlers |
| Member-level under-report | Type-level remarks often list only the root; members carry the full set. Verify purity at **type and member level**. | member scan |
| Injected-dependencies | A driver that appears only in `Injected dependencies` is a false positive — never counts toward the Γ-set. | I4/I13 |

### 6.2 Member-level application (attributes & functions)

Each non-dependency attribute/function is reviewed, given its **own** `Change drivers` remark, and placed in the type/module whose **causal root** matches the member's root. Members that run a type's *own* domain logic are domain-rooted (a platform call is only a subordinated mechanism); members that only touch platform data/rendering are rooted in the specific platform sub-driver (CD-31..44).

| Case | Rule |
|---|---|
| Domain-system `[Event]` handler that runs domain logic | **domain root** + `CD-3x → domain` (not a platform root) |
| Member that only reads/writes platform data or does platform rendering | platform sub-driver root (e.g. textdraw → CD-34; leave as-is) |
| Renderer members per content rendered | driver follows the content (e.g. `UpdateMapName`→CD-11, `UpdateTimeLeft`→CD-12, `Show`→CD-34) |
| Field changes when its *own* field's domain changes | that field's domain is the member root, even inside a differently-rooted aggregate |
| Genuinely foreign method (doesn't operate on its type's own state) | **move** it to the owning type in that driver's module (e.g. `IsCarryingEnemyFlag` CD-02 → `Flag`; `CanMoveUpToNextRank` CD-10 → `RankCollection`; `HasRole`/`IsAdmin` CD-09 → role type; `HasSkin` CD-44 → appearance) |
| Persistent row column / state mutator | stays on its same-table entity (§6.4); a single-table entity with mixed drivers is split into entities sharing the row |

Exhaustive discipline: review every file's non-dependency attributes/functions module-by-module, adjudicate each member-root divergence from its type root, and after each batch verify a green build + all tests + no element removed.

### 6.3 Dependency model (drivers transmit through injection)

| Rule | Statement |
|---|---|
| A dependency's own driver | A dependency has **one** change driver — its own contract/API (e.g. a logger → the logging-API doc). Nothing else. |
| Transmission | Where a dependency is injected/used/accessed, it **transmits its own driver** to the container/client as a subordinated clause (`dep → client-root`), e.g. `DiscordWebhookClient` gains `CD-23 → CD-24` from its injected `ILogger`. |
| Per-type contract driver | `ILogger`→CD-23, repo ports (`IPlayerRepository`)→CD-20, platform services by sub-driver (`IWorldService`→CD-36, `IDialogService`→CD-33, `IEntityManager`→CD-32, `ITimerService`→CD-41). Dependencies live in the module of their own contract driver. |
| DI is not a driver | Wiring (CD-21) is *how* injection happens, not *why* an element changes — **same pseudo-driver class CD-29 was retired for**. Drop spurious `CD-21 → X` subordinate clauses from consumers. |
| CD-21 root only on wiring | CD-21 remains **root only on genuine composition/wiring elements** (`ServiceCollectionExtensions`, composition root, host ECS builder). |

### 6.4 Same-table entity decomposition

When a **persisted row** stores columns driven by different authorities, do **not** model it as one nested pseudo-object — model it as **multiple domain entities sharing the same underlying table**, with the original type reduced to a thin aggregate root composing them. The table was the only thing forcing the drivers together, and the table need not change.

| Step | Action |
|---|---|
| Split | Per-driver entities, all mapping to the same row (e.g. `PlayerInfo` → `PlayerAccount` (CD-08, Accounts), `PlayerStatistics` (CD-10, Statistics), `PlayerRole` (CD-09, Authorization), `PlayerAppearance` (CD-44, PlayerResources)) |
| Aggregate root | `PlayerInfo` stays a thin CD-08 aggregate root exposing the sub-entities (`X.Account`, `X.Stats`, `X.Role`, `X.Appearance`) |
| Persistence | `players` schema untouched; persistence `SetValue` reflection targets the sub-entity instances |
| Call sites | migrate every receiver expression typed as the old entity to the composed accessors; compiler drives the split |
| Not applied to | in-memory/non-persisted aggregates whose sub-values are already separate objects (`Team`) — handled by move-out + separate types |

### 6.5 Sibling vs nested module grouping (primitive then nest)

For a module (type) whose members fall into multiple driver **sets** (e.g. `Flag`: `{CD-02}` and `{CD-02, CD-3x}`):

1. **Group by exact set first** — one *sibling* module per set of change drivers (the primitive grouping).
2. **Resident vs transmitted** — for a member carrying a subordinate platform driver, decide whether the subordinate is *resident* (contains actual platform calls) or merely *transmitted* (inherited from a platform-typed value it references).
3. **Nest by causality** — if one group is *causally downstream* of the parent (responds to a superset of the parent's events, no initiative of its own), nest it inside the parent (nested class/record, same file, same root); a group whose subordinate arrives via an external entity sits as a *sibling* beside the parent.
4. **Merge vs keep-separate** — merge only if one's amendment forces the other's change; do **not** merge two groupings that respond to *disjoint* amendment events and share only a coarse subordinate *label* (e.g. `CarrierAttachment` CD-39 vs visual identity — disjoint even though both stamp `{CD-02, CD-3x}`).

*"One module per set of change drivers … group in different siblings (one per set) — that's the primitive grouping to do first. Then see if one module can't be nested inside the other using causality."* Respect the non-transmission rule (§5.7): the parent stays pure on its own set; the nested module's subordinates don't leak up.

### 6.6 Module-necessity re-audit

Re-ask whether existing topic sub-modules earn their existence under IVP: a sub-module is necessary/right only when its elements are a genuine single-driver set and its membership is not an empty topic shell. A sub-module that only holds DI wiring (CD-21) or config (CD-17) for behaviour whose actual domain driver lives elsewhere should be **flattened into that driver module** (e.g. `Teams.Flags` → `GameRules`; `Teams.Statistics` CD-10 → `Statistics`; `ClassSelectionSettings` CD-17 → its domain module). Regroup toward **flat top-level modules per driver**.

## 7. Review-fix convergence

| Rule | Value |
|---|---|
| **Convergence** | Review-fix all assigned drivers until **2 consecutive rounds with 0 issues found**. |
| **Per element** | Ad-hoc, specific reasoning; **no transversal/greedy edits**. |
| **Delegation split** | `@explore` = analysis only (no writes); `@general` = annotation writes; one per subsystem (Teams, Accounts, Players-core, Weapons/Combos/GunGame/Maps, Host & Persistence). |
| **Verification** | `dotnet build` clean after annotation; only assigned tree edited; no logic changes. |
| **Audits** | Re-run `Audit5` (dangling →), `Audit6b` (root contradiction), `Audit7` (sibling w/o root mark) — see `../tools/`. |
| **Stopping rule** | "Done" = a phase complete at a verified green state, or reaching the boundary of what IVP-correctly applies to (irreducible composites / infra singletons). Do **not** claim whole-tree completion that isn't verified. |

## 8. Measurement (after annotation)

- Run the **single canonical tool** `IvpMeasure.java` (pinned semantics; legacy scripts are inconsistent → disallowed).
- Output: census (types / namespaces / distinct Γ-sets / scattered sets), driver-activation, global stats, per-namespace sets, prod/test split.
- Re-baseline procedure: capture → diff → update `../before/*` (or `../after/*`) → tag `ivp-<state>`.

## 9. Honest caveats

| Caveat | Status |
|---|---|
| Purity is over driver-**sets**, not driver count (see invariants) | enforced |
| Member-level detection is a heuristic (± few members) | documented |
| Essential-vs-spurious grounded in `constraints.md` Appendix A; unlisted = spurious | enforced |
| Activation-frequency `λ(γ)` — **no artifact exists in repo** → weighted cost `Σλκ` not computed | declared, not fabricated |
| Test-namespace completeness inflated (singleton sets); assembly purity ≈ 0 expected | documented |
| Persistent entities with mixed drivers are split into **same-table** sub-entities (`PlayerInfo` → `Account`/`Stats`/`Role`/`Appearance`); in-memory aggregates (`Team`) keep separate sub-objects | documented |
| Member-level IVP is applied to **non-dependency** attributes/functions; genuine single-row columns stay on their same-table entity | documented |
| Member-level pass over ~181 files was not fully verified in one go (~23 divergence categories were treated as correct without a fully resolved causal standard) — do not claim whole-member review that isn't done | declared |
| In the newest `Flag` working-tree state, `FlagIdentity` was removed (Model/Icon/ColorHex promoted to `Flag`); the assistant's causal rationale is not in the captured transcript — end state verified in source, reasoning inferred | declared |
| Companion docs lag the working refactor: `IVP/causal-order.md` still states the un-refined containment law and treats CD-01 as live; the canonical driver map was updated in `changedrivers.md` (CD-01 → CD-31..44) | documented |
