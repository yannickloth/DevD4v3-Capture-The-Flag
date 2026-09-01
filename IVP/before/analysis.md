# Capture-The-Flag — "Before" Cohesion, Coupling & Scatter Analysis

> Complement to `metrics.md`. Measures the committed "before" modularisation at every granularity against the 30-driver change-driver model (`CD-01..CD-30`). Terminology and formulas follow the IVP paper series (`cohesion-2-nature`, `cohesion-4-causal`):
> - **causal cohesion** `H_causal(M) = (purity(M), completeness(M))`
> - `purity(M) = 1 / |𝒜(M)|` where `𝒜(M)` = the set of *distinct change-driver sets* among M's elements (type-level or member-level, per the granularity).
> - `completeness(M) = min over A ∈ 𝒜(M) of |M ∩ [A]| / |[A]|`, where `[A]` = all elements system-wide with driver-set A.
> - **HHI contamination extent** = `1 − Σᵢ sᵢ²` where `sᵢ` is the share of each driver-set within the module (supplements purity; = 0 when pure, → 1 as contamination spreads evenly).

Scope: 285 annotated types, 57 namespaces, 30 change drivers (type-level). Class-level cohesion is computed over ~1010 members (member-level; see caveat 2 — the member count is approximate).

Note: Composite types (like class, record, struct, interface) are modules exactly like namespaces.
---

## A. Causal cohesion
no
### A.1 — Class-level (members are the module's elements)

Lowest-purity classes (most contaminated; purity = 1 / #distinct member driver-sets):

| class | members | purity | completeness | contamination extent (1−HHI) |
|---|---|---|---|---|
| PlayerInfo | 57 | 0.083 | 0.100 | 0.835 |
| Team | 23 | 0.143 | 0.017 | 0.624 |
| GunGameSystem | 10 | 0.200 | 0.016 | 0.720 |
| WeaponSelectionSystem | 7 | 0.200 | 0.017 | 0.776 |
| ClassSelectionSystem | 6 | 0.200 | 0.077 | 0.778 |
| Flag | 15 | 0.200 | 0.017 | 0.596 |
| FlagSystem | 6 | 0.200 | 0.020 | 0.778 |
| InMemory/MariaDb/Sqlite RepositoryManager (tests) | 7–9 | 0.200 | ~0.1 | ~0.77 |
| MapTextDrawRenderer | 12 | 0.250 | 0.053 | 0.681 |
| MapRotationService | 13 | 0.250 | 0.077 | 0.568 |

**Reading.** `PlayerInfo` is the most impure class: 57 members realise ~12 distinct driver sets (account identity, stats, coins, rank, roles, skin, team, persistence columns …). Its completeness 0.10 means its member driver-sets are themselves scattered — each of its driver sets is shared with other classes, so `PlayerInfo` holds only a fraction of each set system-wide. This is the signature of a *god aggregate*: the account entity absorbs many domain concerns instead of delegating them.

### A.2 — Namespace-level (types are the module's elements)

- **mean purity 0.461, min purity 0.100** across 57 namespaces.
- purity distribution is bimodal: a single-set (pure, purity 1.0) tail and a contaminated bulk. 43 of 57 namespaces are composite (purity < 1).

### A.3 — Assembly-level (project = module, types as elements)

| assembly | distinct type-driver-sets | purity |
|---|---|---|
| src/Application | 86 | 0.012 |
| src/Host | 11 | 0.091 |
| src/Persistence | 15 | 0.067 |
| tests/Application.Tests | 20 | 0.050 |
| tests/Persistence.Tests | 10 | 0.100 |

**Reading.** At assembly granularity purity collapses toward 0 (0.012 for the Application core) because a whole assembly legitimately hosts many domains. This is *not* a defect at assembly granularity — it is the expected result of an assembly bundling many driver-equivalence classes. Cohesion is granularity-relative; the assembly number is a floor, not a target. The meaningful granularities are class and namespace.

---

## B. Coupling (change-coupling, not dependency)

### B.1 — Inter-namespace shared-driver overlap

Top pairs of namespaces sharing the most change drivers (Jaccard on driver sets):

| namespace A | namespace B | shared drivers | jaccard |
|---|---|---|---|
| Persistence.Tests.Common | Persistence.Tests.Common.DatabaseProviders | 7 | 0.875 |
| CTF.Host | CTF.Host.Extensions | 6 | 0.750 |
| GunGames | Players.Accounts.Statistics | 5 | 0.500 |
| GunGames | Players.Combos | 5 | 0.556 |
| GunGames | Players.Weapons | 5 | 0.556 |
| Maps | Players | 5 | 0.556 |
| Players | Teams | 5 | 0.500 |
| Players.Accounts | Players.Accounts.Statistics | 5 | 0.625 |
| Teams | Teams.Flags | 5 | 0.625 |
| Persistence.InMemory / MariaDB / SQLite (mutual) | — | 5 | 0.71–0.83 |

**Reading.** The strong overlaps fall into two kinds: (a) **parent–child** namespace pairs (Teams↔Teams.Flags, Accounts↔Accounts.Statistics, Host↔Host.Extensions) — expected containment; and (b) **cross-cutting-driver clusters** — GunGames shares drivers with Statistics/Combos/Weapons because the GunGame *gate* (CD-07) legitimately suspends/replaces those subsystems' behaviours (a game-designed composite, not scatter). The persistence providers share their 5 drivers (CD-17/18/20/21 + their dialect) by the repository-contract axis — decreed, not spurious.

### B.2 — Per-driver causal reach (which namespaces each driver touches)

| driver | namespaces touched |
|---|---|
| CD-01 platform | 33 |
| CD-17 config | 27 |
| CD-21 DI | 16 |
| CD-20 repo contract | 15 |
| CD-29 code-under-test | 15 |
| CD-02 game-rules | 13 |
| CD-10 stats | 13 |
| CD-26 NUnit | 13 |
| CD-27 FluentAssertions | 12 |
| CD-09 authorization | 10 |

The long tail is the decreed horizontal axes (platform/config/DI/repo/test-tooling). The *change-coupling* candidates for the IVP regroup are the mid-band game-domain drivers (CD-02, CD-06, CD-07, CD-10, CD-15) whose reach is dispersion, not platform necessity.

---

## C. Scatter & contamination (the two IVP defect types)

- **Scatter** — `27` distinct type-driver-sets are split across **more than one** namespace (their equivalence class `[A]` is not contained in a single module).
- **Contamination** — `43 of 57` namespaces co-locate types with **differing** driver sets.

### C.1 — Essential vs spurious coupling (rough classification)

Using the `constraints.md` axes, a co-location is **essential** when a platform/persistence/composition/test axis decrees it; otherwise **spurious** (change-coupling, the IVP objective's target).

- **Spurious (removable by IVP regroup):** all `CTF.Application.*` production namespaces — `Players` (9 sets), `Accounts.Statistics` (10), `Teams` (8), `Teams.Flags` (8), `Maps` (6), `Weapons` (6), `Combos` (5), `GunGames` (5), `Chats` (5), etc. These mix differing driver sets by *topic*, not by a decreed axis.
- **Essential (irreducible):** `Persistence.InMemory/MariaDB/SQLite` (repo-contract axis), `CTF.Host`/`Host.Extensions`/`Host.Services` (composition/platform axis), and the *test* namespaces (code-under-test axis).

> The essential co-locations are enumerated per element in `IVP/constraints.md` **Appendix A** (composition root, three providers, repository ports, `IGunGameMode` gate, flag-event handlers, `*.Designer.cs`, fakes, provider dispatch). Anything not listed there is **spurious**.

---

## D. Change-propagation cost

`κ(γ)` = modules (namespaces) touched per driver activation. Full table in `metrics.md` §1.

- Highest `κ`: CD-01 platform (33), CD-17 config (27), CD-21 DI (16), CD-20 repo (15), CD-29 code-under-test (15).
- These are decreed horizontal axes. The IVP objective (minimise change-propagation) can only compress the **spurious** portion of the mid-band (CD-02 game-rules 13, CD-10 stats 13, CD-09 authorization 10, CD-15 command-set 10).

> **Unmeasurable without an artifact:** activation-frequency weighting `λ(γ)`. No release-cadence / beta-frequency artifact exists in the repo (`.env`, config, docs, git history are the only sources, and none record driver activation rates). Weighted cost `Σ λ(γ)·κ(γ)` is therefore **not computed**; it is flagged for a future pass once a `λ` source is declared. This is not fabricated.

---

## F. Test ↔ production driver alignment

Sample of the mock-inheritance validation (does a test's asserted production driver match the production type's actual driver set?):

| test class | asserts driver | production mirror |
|---|---|---|
| FakePlayer/FakeCarrier/FakePlayer2/FakePlayer3 | CD-01 | `Player` (platform surface) ✓ |
| ProcessKillTests / WeaponLevelTests / … | CD-07 | GunGame progression ✓ |
| IsCarryingEnemyFlagTests | CD-02 | Team/Flag ✓ |
| PasswordTests | CD-08 | PlayerInfo.SetPassword ✓ |
| WeaponCatalogTests / Settings / Type | CD-04 | WeaponCatalog ✓ |

The mock-inheritance rule holds: each test/fake carries the driver of the seam (interface or type surface) it mimics, not the seam's internal domain drivers. A full machine check requires an explicit test-namespace → production-namespace mirror map (the sample above is manual; the mirror is derivable by stripping `.Tests` and `Persistence.Tests` → `Persistence`).

---

## Bottom line

The "before" state is a **low-purity, topic-grouped modularisation**: 285 classes (elements) realize **148 distinct change-driver sets** but are placed in only **57 namespaces** (the theory's modules); 43/57 namespaces are composite and 26 of the 148 driver-sets are scattered across >1 namespace. Mean namespace purity 0.461, assembly purity near 0 (expected), class-level purity as low as 0.083 (`PlayerInfo`). The 57-vs-148 gap is the IVP non-conformance (the code fuses 148 `E/Γ` classes into 57 topic modules); the change-coupling drivers (game-rules, stats, coins, GunGame, command-set) are dispersed across namespaces beyond what the platform/persistence/composition axes decree — this dispersion is the exact quantity an IVP regroup (group by identical driver set, subject to `constraints.md`) would tighten, and it is what the "after" measurement (in a separate branch/worktree) must show reduced.

---

## Caveats & limitations (honest)

1. **Purity is over *driver sets*, not driver count.** `purity(M) = 1 / |distinct driver sets|`. A class whose members all share `{CD-02, CD-10}` is pure; "multi-driver" ≠ impure. Only differing *sets* co-located make a module impure. "Drivers per class/namespace" figures are token counts, a proxy for — not equal to — the set-based contamination.
2. **Member-level detection is a heuristic.** Members were found by indentation + visibility keyword + a nearby `Change drivers` remark. A small number mis-bind: `PlayerInfo` has 57 member-like lines but 54 remarks (the 3 top-of-class `const`/`partial Regex` declarations are unannotated). Class *rankings* are robust; exact member counts are ± a few.
3. **Essential-vs-spurious is grounded in `constraints.md` Appendix A**, which cites each essential composite element (`Startup`, the three providers, the `IPlayerRepository` ports, `IGunGameMode`, `*.Designer.cs`, the fakes, the provider dispatch) to its decreed axis. The §C.1 "spurious" bucket (topic-grouped `CTF.Application.*` namespaces) is everything *not* in that appendix. For any co-location not listed here nor in Appendix A, the default classification is **spurious** (change-coupling scatter).
4. **λ(γ) unmeasured.** Weighted cost `Σ λ(γ)·κ(γ)` needs a per-driver activation-frequency artifact, which the repo does not contain. Only `κ(γ)` (module-touch count) is reported.
5. **Test-namespace completeness is inflated** (singleton driver sets) and **assembly purity is near 0** (expected, not a defect) — both granularity artifacts documented in `metrics.md` §7.
