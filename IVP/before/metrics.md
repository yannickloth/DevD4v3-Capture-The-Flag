# Capture-The-Flag — Change-Driver & Causal-Cohesion Metrics

> Regenerated from the fixed driver model (CD-19 = MariaDB SQL dialect, CD-30 = SQLite SQL dialect, tests included). 285 types across 57 namespaces, 30 change drivers.

## 1. Driver activation

| Driver | Elements | Modules (namespaces) | Scatter ratio (elem/ns) |
|---|---|---|---|
| CD-01 (open.mp/SampSharp platform API) | 109 | 33 | 3.30 |
| CD-02 (CTF game-rules specification) | 47 | 13 | 3.62 |
| CD-03 (combat/weapon-rules specification) | 19 | 8 | 2.38 |
| CD-04 (weapon-catalog configuration) | 18 | 3 | 6.00 |
| CD-05 (combo definitions) | 11 | 2 | 5.50 |
| CD-06 (coin economy) | 17 | 6 | 2.83 |
| CD-07 (GunGame mode rules) | 41 | 8 | 5.13 |
| CD-08 (account & authentication policy) | 16 | 8 | 2.00 |
| CD-09 (authorization policy) | 25 | 10 | 2.50 |
| CD-10 (player-statistics/rank model) | 38 | 13 | 2.92 |
| CD-11 (map configuration) | 24 | 8 | 3.00 |
| CD-12 (map-rotation rules) | 13 | 6 | 2.17 |
| CD-13 (chat rules) | 7 | 2 | 3.50 |
| CD-14 (anti-cheat policy) | 4 | 1 | 4.00 |
| CD-15 (command set) | 16 | 10 | 1.60 |
| CD-16 (RCON security policy) | 1 | 1 | 1.00 |
| CD-17 (game configuration/.env schema) | 50 | 27 | 1.85 |
| CD-18 (database schema/player data model) | 21 | 6 | 3.50 |
| CD-19 (MariaDB SQL dialect) | 11 | 4 | 2.75 |
| CD-20 (outbound repository contract) | 37 | 15 | 2.47 |
| CD-21 (DI container/composition) | 30 | 16 | 1.88 |
| CD-22 (hosting/deployment spec) | 4 | 4 | 1.00 |
| CD-23 (Serilog logging) | 2 | 2 | 1.00 |
| CD-24 (Discord webhook contract) | 5 | 4 | 1.25 |
| CD-25 (BCrypt password-hashing contract) | 11 | 7 | 1.57 |
| CD-26 (NUnit test-framework contract) | 45 | 13 | 3.46 |
| CD-27 (FluentAssertions contract) | 44 | 12 | 3.67 |
| CD-28 (NSubstitute mock contract) | 4 | 1 | 4.00 |
| CD-29 (code-under-test contract) | 57 | 15 | 3.80 |
| CD-30 (SQLite SQL dialect) | 13 | 5 | 2.60 |

## 2. Global statistics

| Statistic | Value |
|---|---|
| Types with a change-driver annotation | 285 |
| Namespaces | 57 |
| Mean change drivers per class | 2.60 |
| Median change drivers per class | 2 |
| Mean change drivers per namespace | 4.61 |
| Median change drivers per namespace | 5 |

Drivers per class histogram: {1=78, 2=77, 3=47, 4=61, 5=23, 6=3}

### 2.1 The three cardinalities and IVP correspondence

The three quantities the theory relates:

| Cardinality | Value | Meaning |
|---|---|---|
| **classes (elements `E`)** | 285 | the code elements supplied to the partition |
| **distinct change-driver sets (`E/Γ`)** | 148 | the Γ-equivalence classes = the IVP **normative partition** (one module per driver-set) |
| **namespaces (actual modules)** | 57 | the partition the code actually has |

**In an IVP-compliant system `#namespaces` would equal `#distinct-Γ-sets`** (each `[A]` is its own module). Here `57 ≠ 148`: the code fuses **148** driver-equivalence classes into only **57** namespaces — a ~2.6× over-coarsening. The gap `148 − 57` is the total `E/Γ`-non-conformance (contamination net of scatter):

- **elements → sets (`285 → 148`)** is the *cohesion* reduction: multiple elements sharing one Γ-set belong together (the Unification axis). The 148 = the set of all distinct driver-sets actually assigned across the 285 elements.
- **sets → namespaces (`148 → 57`)** is where the code departs from IVP: 148 driver-classes are bundled into 57 topic-named namespaces. This is the contamination the IVP regroup would undo (subject to the platform/persistence/composition/game constraints in `constraints.md` Appendix A).

> **Precision note:** the "distinct-Γ-sets = 148" is computed by the deterministic canonical pass (run 3× identically). The authoritative **type count is 285** = annotated type declarations (225 production + 60 test), excluding the generated `Program.cs` `Entrypoint` and the 9 unannotated declaration lines. Earlier mentions of "137"/"27"/"289" came from less-strict parsers and are superseded by 148 sets / 26 scattered / 285 types — see §7 caveat 2.

## 3. Namespace contamination (multi-change-driver-set mixes)

| namespace | classes | distinct tokens | distinct sets | single-set? |
|---|---|---|---|---|
| CTF.Application | 1 | 1 | 1 | yes |
| CTF.Application.GunGames | 12 | 7 | 5 | no |
| CTF.Application.GunGames.Results | 6 | 5 | 3 | no |
| CTF.Application.GunGames.WeaponProgressions | 5 | 1 | 1 | yes |
| CTF.Application.GunGames.WeaponProgressions.Definitions | 8 | 1 | 1 | yes |
| CTF.Application.Maps | 14 | 5 | 6 | no |
| CTF.Application.Maps.Rotation | 2 | 4 | 2 | no |
| CTF.Application.Players | 10 | 9 | 9 | no |
| CTF.Application.Players.Accounts | 2 | 5 | 2 | no |
| CTF.Application.Players.Accounts.Authentication | 5 | 4 | 3 | no |
| CTF.Application.Players.Accounts.Profile | 3 | 3 | 2 | no |
| CTF.Application.Players.Accounts.Roles | 9 | 4 | 4 | no |
| CTF.Application.Players.Accounts.Statistics | 10 | 8 | 10 | no |
| CTF.Application.Players.AntiCBug | 4 | 3 | 3 | no |
| CTF.Application.Players.Chats | 5 | 4 | 5 | no |
| CTF.Application.Players.Chats.Definitions | 4 | 3 | 2 | no |
| CTF.Application.Players.Combos | 5 | 7 | 5 | no |
| CTF.Application.Players.Combos.Definitions | 6 | 3 | 2 | no |
| CTF.Application.Players.GeneralCommands | 6 | 3 | 4 | no |
| CTF.Application.Players.Headshots | 2 | 5 | 2 | no |
| CTF.Application.Players.Pause | 3 | 2 | 1 | yes |
| CTF.Application.Players.Ranks | 5 | 1 | 1 | yes |
| CTF.Application.Players.TopPlayers | 6 | 4 | 4 | no |
| CTF.Application.Players.Vitalities | 6 | 4 | 4 | no |
| CTF.Application.Players.Weapons | 6 | 7 | 6 | no |
| CTF.Application.Players.Weapons.Catalogs | 10 | 2 | 2 | no |
| CTF.Application.Teams | 12 | 6 | 7 | no |
| CTF.Application.Teams.ClassSelection | 6 | 6 | 5 | no |
| CTF.Application.Teams.Flags | 8 | 7 | 8 | no |
| CTF.Application.Teams.Flags.AutoReturn | 2 | 3 | 2 | no |
| CTF.Application.Teams.Flags.Carriers | 3 | 5 | 3 | no |
| CTF.Application.Teams.Flags.Events | 6 | 6 | 5 | no |
| CTF.Application.Teams.Matches | 3 | 2 | 2 | no |
| CTF.Application.Teams.Statistics | 3 | 5 | 3 | no |
| CTF.Application.Tests | 1 | 5 | 1 | yes |
| CTF.Application.Tests.Fakes | 5 | 4 | 2 | no |
| CTF.Application.Tests.GunGames | 7 | 4 | 2 | no |
| CTF.Application.Tests.Maps | 6 | 5 | 2 | no |
| CTF.Application.Tests.Players.Accounts | 9 | 8 | 5 | no |
| CTF.Application.Tests.Players.Extensions | 1 | 5 | 1 | yes |
| CTF.Application.Tests.Players.Ranks | 3 | 4 | 1 | yes |
| CTF.Application.Tests.Players.TopPlayers | 1 | 5 | 1 | yes |
| CTF.Application.Tests.Players.Vitalities | 2 | 4 | 1 | yes |
| CTF.Application.Tests.Players.Weapons | 5 | 6 | 4 | no |
| CTF.Application.Tests.Teams | 5 | 5 | 2 | no |
| CTF.Application.Tests.Teams.Flags | 1 | 4 | 1 | yes |
| CTF.Host | 3 | 6 | 3 | no |
| CTF.Host.Extensions | 5 | 8 | 5 | no |
| CTF.Host.Services | 4 | 5 | 4 | no |
| Persistence.InMemory | 6 | 5 | 5 | no |
| Persistence.MariaDB | 5 | 6 | 5 | no |
| Persistence.SQLite | 5 | 6 | 5 | no |
| Persistence.SQLite.Extensions | 2 | 1 | 1 | yes |
| Persistence.Tests.Common | 6 | 8 | 6 | no |
| Persistence.Tests.Common.DatabaseProviders | 3 | 7 | 3 | no |
| Persistence.Tests.Players | 5 | 5 | 1 | yes |
| SampSharp | 1 | 2 | 1 | yes |

43 of 57 namespaces are composite; 14 are single-set.

## 4. Causal cohesion per namespace

Module M = namespace. purity(M) = 1 / (#distinct driver sets in M). completeness(M) = min over each driver-set A in M of |M ∩ [A]| / |[A]|, where [A] = system-wide elements with driver-set A.

| namespace | classes | purity | completeness |
|---|---|---|---|
| CTF.Application | 1 | 1.000 | 0.167 |
| CTF.Application.GunGames | 12 | 0.200 | 0.167 |
| CTF.Application.GunGames.Results | 6 | 0.333 | 0.095 |
| CTF.Application.GunGames.WeaponProgressions | 5 | 1.000 | 0.238 |
| CTF.Application.GunGames.WeaponProgressions.Definitions | 8 | 1.000 | 0.381 |
| CTF.Application.Maps | 14 | 0.167 | 0.500 |
| CTF.Application.Maps.Rotation | 2 | 0.500 | 0.500 |
| CTF.Application.Players | 10 | 0.111 | 0.143 |
| CTF.Application.Players.Accounts | 2 | 0.500 | 1.000 |
| CTF.Application.Players.Accounts.Authentication | 5 | 0.333 | 0.333 |
| CTF.Application.Players.Accounts.Profile | 3 | 0.500 | 0.667 |
| CTF.Application.Players.Accounts.Roles | 9 | 0.250 | 0.800 |
| CTF.Application.Players.Accounts.Statistics | 10 | 0.100 | 0.111 |
| CTF.Application.Players.AntiCBug | 4 | 0.333 | 1.000 |
| CTF.Application.Players.Chats | 5 | 0.200 | 0.111 |
| CTF.Application.Players.Chats.Definitions | 4 | 0.500 | 0.500 |
| CTF.Application.Players.Combos | 5 | 0.200 | 0.167 |
| CTF.Application.Players.Combos.Definitions | 6 | 0.500 | 0.833 |
| CTF.Application.Players.GeneralCommands | 6 | 0.250 | 0.111 |
| CTF.Application.Players.Headshots | 2 | 0.500 | 1.000 |
| CTF.Application.Players.Pause | 3 | 1.000 | 0.214 |
| CTF.Application.Players.Ranks | 5 | 1.000 | 0.625 |
| CTF.Application.Players.TopPlayers | 6 | 0.250 | 0.250 |
| CTF.Application.Players.Vitalities | 6 | 0.250 | 0.222 |
| CTF.Application.Players.Weapons | 6 | 0.167 | 0.500 |
| CTF.Application.Players.Weapons.Catalogs | 10 | 0.500 | 0.500 |
| CTF.Application.Teams | 12 | 0.143 | 0.214 |
| CTF.Application.Teams.ClassSelection | 6 | 0.200 | 0.111 |
| CTF.Application.Teams.Flags | 8 | 0.125 | 0.071 |
| CTF.Application.Teams.Flags.AutoReturn | 2 | 0.500 | 0.333 |
| CTF.Application.Teams.Flags.Carriers | 3 | 0.333 | 0.333 |
| CTF.Application.Teams.Flags.Events | 6 | 0.200 | 0.071 |
| CTF.Application.Teams.Matches | 3 | 0.500 | 0.143 |
| CTF.Application.Teams.Statistics | 3 | 0.333 | 0.125 |
| CTF.Application.Tests | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Fakes | 5 | 0.500 | 1.000 |
| CTF.Application.Tests.GunGames | 7 | 0.500 | 1.000 |
| CTF.Application.Tests.Maps | 6 | 0.500 | 1.000 |
| CTF.Application.Tests.Players.Accounts | 9 | 0.200 | 0.286 |
| CTF.Application.Tests.Players.Extensions | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Players.Ranks | 3 | 1.000 | 0.500 |
| CTF.Application.Tests.Players.TopPlayers | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Players.Vitalities | 2 | 1.000 | 0.667 |
| CTF.Application.Tests.Players.Weapons | 5 | 0.250 | 0.333 |
| CTF.Application.Tests.Teams | 5 | 0.500 | 0.571 |
| CTF.Application.Tests.Teams.Flags | 1 | 1.000 | 0.143 |
| CTF.Host | 3 | 0.333 | 0.333 |
| CTF.Host.Extensions | 5 | 0.200 | 0.333 |
| CTF.Host.Services | 4 | 0.250 | 0.333 |
| Persistence.InMemory | 6 | 0.200 | 1.000 |
| Persistence.MariaDB | 5 | 0.200 | 0.167 |
| Persistence.SQLite | 5 | 0.200 | 0.167 |
| Persistence.SQLite.Extensions | 2 | 1.000 | 1.000 |
| Persistence.Tests.Common | 6 | 0.167 | 1.000 |
| Persistence.Tests.Common.DatabaseProviders | 3 | 0.333 | 1.000 |
| Persistence.Tests.Players | 5 | 1.000 | 1.000 |
| SampSharp | 1 | 1.000 | 1.000 |

## 5. Per-class driver assignment (multiset summary)

Full per-element assignment lives in the source `<remarks>`; here the driver-set frequency per class count.

{1=78, 2=77, 3=47, 4=61, 5=23, 6=3}

---

## 6. Readings & interpretation

### 6.1 Global statistics

- **Types with a driver**: 285 (225 production + 60 test).
- **Mean drivers per class = 2.60**, **median = 2**. The mean is pulled above the median by a thin tail of 5- and 6-driver classes (23 + 3). The typical class answers to 2 drivers.
- **Mean drivers per namespace = 4.61**, **median = 5**. A namespace is, on average, a 4–5-driver mix — not a single-driver module.

### 6.2 Drivers per class — histogram

| drivers/class | 1 | 2 | 3 | 4 | 5 | 6 |
|---|---|---|---|---|---|---|
| # classes | 78 | 77 | 47 | 61 | 23 | 3 |

- **78 types carry a single change driver (token count 1); 211 carry ≥2 drivers.** This is a statement about the *element's own driver count*, **not about module purity**. A type with `{CD-06, CD-10}` is an *irreducible composite element* — it is not "impure"; purity concerns a *module* whose elements have *differing* driver **sets**, not a single element answering to several drivers.
- **Purity ≠ "single driver".** Purity of a module is `1 / |distinct driver sets|` among its elements. A module is pure iff *all* its elements share the *same driver set*, however many drivers that set contains. Two elements both carrying `{CD-02, CD-10}` are a *pure* group (purity 1); an element carrying `{CD-02}` next to one carrying `{CD-10}` is impure (purity ½). "Single driver" and "pure" are distinct axes.

### 6.3 Namespaces are multi-change-driver-*set* mixes (contamination, not just multi-driver)

The IVP Separation condition is about driver **sets**, not driver tokens. Measured by distinct driver **sets** per namespace:

- **43 of 57 namespaces are composite** (co-locate elements with differing driver sets); **14 are single-set**.

**Worst offenders** (distinct sets, i.e. maximal contamination):

| sets | classes | namespace |
|---|---|---|
| 10 | 10 | CTF.Application.Players.Accounts.Statistics |
| 9 | 10 | CTF.Application.Players |
| 8 | 8 | CTF.Application.Teams.Flags |
| 7 | 12 | CTF.Application.Teams |
| 6 | 14 | CTF.Application.Maps |
| 6 | 6 | CTF.Application.Players.Weapons |
| 6 | 6 | Persistence.Tests.Common |
| 5 | 12 | CTF.Application.GunGames |

`Accounts.Statistics` splits 10 classes into 10 distinct driver sets — every class is its own set, so **no separation has happened at all**. The distinct-*set* count exceeds the distinct-*token* count whenever the same tokens recombine differently across classes (e.g. 8 tokens can produce 10 sets).

Grouping by *topic* (statistics, flags, weapons, chat, maps) produced namespaces whose elements have **differing** driver sets — exactly the contamination IVP eliminates by grouping by identical driver set.

### 6.4 Driver breadth & scatter

Two regimes again, read off the activation table (§1):

- **Decreed cross-cutting drivers** (irreducible per `constraints.md`): CD-01 platform (33 ns), CD-17 config (27), CD-21 DI (16), CD-20 repo contract (15), CD-29 code-under-test (15, the test-assembly mirror of the production surface), CD-26 NUnit (13). These are horizontal axes; their breadth is platform/composition/persistence/test-tooling structure, not removable scatter.
- **Change-coupling drivers with genuine scatter** (the IVP regrouping candidates): CD-15 command set (1.60 ratio — nearly one command per namespace), CD-17 (1.85), CD-21 (1.88), CD-01 (3.30), CD-02 game-rules (3.62), CD-10 stats (2.92). Lowest scatter ratio = most thinly spread.

### 6.5 Causal cohesion (§4)

Purity and completeness land in `]0, 1]`. Two facts stand out:

- **Production namespaces are low-purity**: the top quartile sits near purity 0.10–0.20 (`Accounts.Statistics` 0.100, `Players` 0.111, `Teams.Flags` 0.125, `Maps` 0.167), i.e. a module's elements realise ~8–10 distinct driver sets. This is the quantitative face of the contamination above.
- **Completeness is high for whole-domain modules**: namespaces that *fully* own a driver set (all its elements co-located) reach completeness 1.0 (e.g. `Persistence.InMemory`, `Accounts.Statistics` is low, but `Persistence.*` providers group all elements of each repo-contract set). Where completeness is low, the same driver set is scattered across several namespaces — the "scatter" defect (§5.2 — the modules touched per driver activation being the count of those namespaces).

**Granularity caveat (honest limitation):** the causal-cohesion completeness for *test* namespaces reads near 1.0 because each test class carries a largely-unique driver set (framework + code-under-test + its specific production driver), so its equivalence class `[A]` is a singleton that appears in exactly one namespace. That is numerically correct but reflects the driver-set *singleton* nature of test classes, not a meaningful "completeness" claim about test organisation. The metric is computed correctly; its interpretation at test-class granularity is limited by the singleton driver sets.

### 6.6 Bottom line

The "before" state is a **low-purity, topic-grouped modularisation**: 83% of namespaces composite (43/57, i.e. co-locating differing driver sets), mean 4.6 driver *tokens* per namespace and 2.6 driver *tokens* per class (token counts, not purity). The change-propagation cost (modules touched per driver activation) is driven by the *scatter* of the change-coupling drivers (command set, game rules, stats, coins, GunGame) across namespaces — which is precisely what the IVP regroup (group by identical driver set, subject to the platform/persistence/composition/game constraints) would tighten.

---

## 7. Caveats & limitations (honest, must-read before interpreting)

1. **Purity is over *driver sets*, not driver count.** Purity(M) = `1 / |distinct driver sets|` among M's elements. "Single driver" is a token count, unrelated to purity. A module whose elements all share `{CD-02, CD-10}` is pure (purity 1); "multi-driver" ≠ "impure". Only "multi-**set**" → impure. Any number reported as "drivers per class/namespace" is a *token* count (how many distinct driver codes appear), a proxy for — not equal to — the set-based contamination.
2. **Member-level detection is a heuristic.** Per-class cohesion treats a class's *members* as its elements. Members were identified by line indentation + a visibility keyword + a nearby `Change drivers` remark. This mis-binds a small number of members: e.g. `PlayerInfo` has 57 member-like lines but only 54 carry a remark — the 3 top-of-class `const`/`partial Regex` declarations (`PlayerNamePattern`, `NoSkin`, `NoAccount`, `PlayerNameRegex`) were not individually annotated and are either skipped or defaulted by the detector. The *ranking* of classes by purity is robust; the *exact* per-class member count (± a few) is approximate.
3. **Essential-vs-spurious classification.** The essential composite elements (composition root `Startup`, the three providers, the repository ports, the `IGunGameMode` gate, flag-event handlers, `*.Designer.cs`, test fakes, the provider dispatch) are cited to their decreed axis in `IVP/constraints.md` **Appendix A**. Everything not in that appendix is treated as **spurious** (change-coupling scatter). The earlier namespace-name heuristic has been superseded by this per-element citation.
4. **Activation frequency `λ(γ)` is unmeasured.** Weighted change-propagation cost `Σ λ(γ)·κ(γ)` requires a per-driver activation-frequency artifact (release cadence, beta schedule). No such artifact exists in the repo (enforced by the "never fabricate" rule). Only the *unweighted* `κ(γ)` module-touch counts are reported; the weighted optimum is deferred until a `λ` source is declared.
5. **Completeness for test namespaces is inflated.** Each test class carries a largely-unique driver set (framework + code-under-test + its specific production driver), so its equivalence class `[A]` is a singleton appearing in exactly one namespace → completeness reads 1.0 without meaning the test code is well-organised by driver. It reflects the singleton nature of test driver-sets, not a real completeness claim.
6. **Completeness for test namespaces reads near 1.0** and **assembly-level purity is near 0** (not a defect — an assembly legitimately bundles many driver classes). Both are granularity artifacts, not findings.
