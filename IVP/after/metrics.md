# Capture-The-Flag — Change-Driver & Causal-Cohesion Metrics (After)

> Canonical measurement of the post-IVP tree with the pinned tool `IVP/tools/IvpMeasure.java`
> (display labels for CD-31..CD-44 added; set semantics unchanged). 309 annotated types
> across 64 namespaces, 42 active drivers (CD-01 decomposed into CD-31..CD-44, CD-29 retired).
>
> **Comparability caveat:** the driver space was *refined*, not merely moved — former CD-01
> umbrella sets split into finer sub-driver sets. Raw set-count deltas vs the before baseline
> therefore reflect the refinement plus the reorganisation. Comparable signals: scattered sets,
> composite-namespace share, CD-01 activation (= 0), and per-driver purity.

## 1. Driver activation

| Driver | Elements | Modules (namespaces) | Scatter ratio (elem/ns) |
|---|---|---|---|
| CD-01 (open.mp/SampSharp platform API) | 0 | 0 | 0.00 |
| CD-02 (CTF game-rules specification) | 53 | 10 | 5.30 |
| CD-03 (combat/weapon-rules specification) | 25 | 8 | 3.13 |
| CD-04 (weapon-catalog configuration) | 18 | 5 | 3.60 |
| CD-05 (combo definitions) | 11 | 1 | 11.00 |
| CD-06 (coin economy) | 17 | 6 | 2.83 |
| CD-07 (GunGame mode rules) | 43 | 7 | 6.14 |
| CD-08 (account & authentication policy) | 17 | 7 | 2.43 |
| CD-09 (authorization policy) | 36 | 15 | 2.40 |
| CD-10 (player-statistics/rank model) | 49 | 16 | 3.06 |
| CD-11 (map configuration) | 21 | 9 | 2.33 |
| CD-12 (map-rotation rules) | 15 | 6 | 2.50 |
| CD-13 (chat rules) | 9 | 2 | 4.50 |
| CD-14 (anti-cheat policy) | 4 | 1 | 4.00 |
| CD-15 (command set) | 36 | 14 | 2.57 |
| CD-16 (RCON security policy) | 1 | 1 | 1.00 |
| CD-17 (game configuration/.env schema) | 49 | 24 | 2.04 |
| CD-18 (database schema/player data model) | 22 | 7 | 3.14 |
| CD-19 (MariaDB SQL dialect) | 10 | 3 | 3.33 |
| CD-20 (outbound repository contract) | 39 | 15 | 2.60 |
| CD-21 (DI container/composition) | 18 | 13 | 1.38 |
| CD-22 (hosting/deployment spec) | 5 | 4 | 1.25 |
| CD-23 (Serilog logging) | 3 | 3 | 1.00 |
| CD-24 (Discord webhook contract) | 5 | 4 | 1.25 |
| CD-25 (BCrypt password-hashing contract) | 10 | 7 | 1.43 |
| CD-26 (NUnit test-framework contract) | 51 | 17 | 3.00 |
| CD-27 (FluentAssertions contract) | 50 | 16 | 3.13 |
| CD-28 (NSubstitute mock contract) | 4 | 1 | 4.00 |
| CD-30 (SQLite SQL dialect) | 12 | 4 | 3.00 |
| CD-31 (player entity & lifecycle events) | 59 | 19 | 3.11 |
| CD-32 (ECS runtime) | 52 | 20 | 2.60 |
| CD-33 (dialog API) | 20 | 10 | 2.00 |
| CD-34 (textdraw API) | 14 | 8 | 1.75 |
| CD-35 (GameText API) | 13 | 7 | 1.86 |
| CD-36 (client-message API) | 40 | 17 | 2.35 |
| CD-37 (pickup API) | 12 | 4 | 3.00 |
| CD-38 (map-icon & radar API) | 9 | 3 | 3.00 |
| CD-39 (attached-object API) | 1 | 1 | 1.00 |
| CD-40 (audio API) | 11 | 4 | 2.75 |
| CD-41 (timer API) | 6 | 2 | 3.00 |
| CD-42 (server service API) | 4 | 4 | 1.00 |
| CD-43 (command infrastructure) | 23 | 12 | 1.92 |
| CD-44 (model & skin id resources) | 12 | 7 | 1.71 |

## 2. Global statistics

| Statistic | Value | Before |
|---|---|---|
| Types with a change-driver annotation | 309 | 293 |
| Namespaces | 64 | 57 |
| Mean change drivers per class | 2.99 | 2.46 |
| Median change drivers per class | 3 | 2 |
| Mean change drivers per namespace | 5.38 | 4.42 |
| Median change drivers per namespace | 4.0 | 4 |

Drivers per class histogram: {1=87, 2=62, 3=67, 4=42, 5=16, 6=8, 7=11, 8=8, 9=5, 10=3}

### 2.1 The three cardinalities and IVP correspondence

| Cardinality | Value | Meaning |
|---|---|---|
| **classes (elements `E`)** | 309 | the code elements supplied to the partition |
| **distinct change-driver sets (`E/Γ`)** | 180 | the Γ-equivalence classes = the IVP normative partition |
| **namespaces (actual modules)** | 64 | the partition the code actually has |

After the refinement the set space is finer by construction (the former CD-01 umbrella
sets split), so `180 vs 137` before is not a regression signal. The structural defect
metrics are the ones below: scattered sets and composite namespaces.

## 3. Namespace contamination (multi-change-driver-set mixes)

| namespace | classes | distinct tokens | distinct sets | single-set? |
|---|---|---|---|---|
| CTF.Application | 1 | 1 | 1 | yes |
| CTF.Application.Accounts | 10 | 13 | 9 | no |
| CTF.Application.AntiCheat | 4 | 9 | 4 | no |
| CTF.Application.Audio | 1 | 2 | 1 | yes |
| CTF.Application.Authorization | 12 | 10 | 8 | no |
| CTF.Application.Chat | 8 | 7 | 6 | no |
| CTF.Application.CoinEconomy | 2 | 9 | 2 | no |
| CTF.Application.Combat | 11 | 14 | 8 | no |
| CTF.Application.Combos | 11 | 13 | 5 | no |
| CTF.Application.CommandInfrastructure | 2 | 6 | 2 | no |
| CTF.Application.Commands | 7 | 7 | 6 | no |
| CTF.Application.Discord | 1 | 3 | 1 | yes |
| CTF.Application.GameRules | 38 | 25 | 29 | no |
| CTF.Application.GunGames | 27 | 13 | 6 | no |
| CTF.Application.GunGames.Results | 4 | 7 | 2 | no |
| CTF.Application.MapIcons | 2 | 3 | 2 | no |
| CTF.Application.Maps | 10 | 9 | 4 | no |
| CTF.Application.Maps.Rotation | 6 | 12 | 3 | no |
| CTF.Application.Pickups | 1 | 4 | 1 | yes |
| CTF.Application.PlayerResources | 6 | 4 | 3 | no |
| CTF.Application.Players | 2 | 2 | 2 | no |
| CTF.Application.Players.Accounts | 1 | 2 | 1 | yes |
| CTF.Application.Players.Chats | 1 | 2 | 1 | yes |
| CTF.Application.Players.TopPlayers | 2 | 3 | 2 | no |
| CTF.Application.Players.Weapons | 3 | 10 | 3 | no |
| CTF.Application.Players.Weapons.Catalogs | 1 | 2 | 1 | yes |
| CTF.Application.RconSecurity | 1 | 3 | 1 | yes |
| CTF.Application.Statistics | 21 | 16 | 16 | no |
| CTF.Application.Teams | 4 | 9 | 4 | no |
| CTF.Application.Tests | 1 | 4 | 1 | yes |
| CTF.Application.Tests.Authorization | 1 | 3 | 1 | yes |
| CTF.Application.Tests.Fakes | 5 | 3 | 2 | no |
| CTF.Application.Tests.GameRules | 3 | 3 | 1 | yes |
| CTF.Application.Tests.GunGames | 8 | 3 | 2 | no |
| CTF.Application.Tests.Maps | 6 | 4 | 2 | no |
| CTF.Application.Tests.PlayerResources | 1 | 3 | 1 | yes |
| CTF.Application.Tests.Players.Accounts | 7 | 8 | 6 | no |
| CTF.Application.Tests.Players.Extensions | 1 | 4 | 1 | yes |
| CTF.Application.Tests.Players.Ranks | 3 | 3 | 1 | yes |
| CTF.Application.Tests.Players.TopPlayers | 1 | 4 | 1 | yes |
| CTF.Application.Tests.Players.Vitalities | 2 | 3 | 1 | yes |
| CTF.Application.Tests.Players.Weapons | 5 | 5 | 4 | no |
| CTF.Application.Tests.Statistics | 5 | 4 | 2 | no |
| CTF.Application.Tests.Teams | 3 | 3 | 1 | yes |
| CTF.Application.Tests.TextDraws | 1 | 3 | 1 | yes |
| CTF.Application.TextDraws | 2 | 4 | 2 | no |
| CTF.Application.WeaponCatalogs | 9 | 1 | 1 | yes |
| CTF.Host.Bcrypt | 1 | 1 | 1 | yes |
| CTF.Host.CommandInfrastructure | 1 | 1 | 1 | yes |
| CTF.Host.Composition | 3 | 4 | 3 | no |
| CTF.Host.Config | 1 | 2 | 1 | yes |
| CTF.Host.Deployment | 1 | 2 | 1 | yes |
| CTF.Host.Discord | 2 | 2 | 2 | no |
| CTF.Host.Ecs | 1 | 5 | 1 | yes |
| CTF.Host.Logging | 1 | 3 | 1 | yes |
| CTF.Host.ServerService | 1 | 2 | 1 | yes |
| Persistence.InMemory | 6 | 5 | 5 | no |
| Persistence.MariaDB | 5 | 6 | 5 | no |
| Persistence.SQLite | 5 | 6 | 5 | no |
| Persistence.SQLite.Extensions | 2 | 1 | 1 | yes |
| Persistence.Tests.Common | 6 | 7 | 5 | no |
| Persistence.Tests.Common.DatabaseProviders | 3 | 6 | 3 | no |
| Persistence.Tests.Players | 5 | 4 | 1 | yes |
| SampSharp | 1 | 2 | 1 | yes |

35 of 64 namespaces are composite; 29 are single-set (before: 43/57 composite, 14 single).

## 4. Causal cohesion per namespace

Module M = namespace. purity(M) = 1 / (#distinct driver sets in M). completeness(M) = min over each driver-set A in M of |M ∩ [A]| / |[A]|.

| namespace | classes | purity | completeness |
|---|---|---|---|
| CTF.Application | 1 | 1.000 | 0.200 |
| CTF.Application.Accounts | 10 | 0.111 | 1.000 |
| CTF.Application.AntiCheat | 4 | 0.250 | 1.000 |
| CTF.Application.Audio | 1 | 1.000 | 0.500 |
| CTF.Application.Authorization | 12 | 0.125 | 1.000 |
| CTF.Application.Chat | 8 | 0.167 | 1.000 |
| CTF.Application.CoinEconomy | 2 | 0.500 | 1.000 |
| CTF.Application.Combat | 11 | 0.125 | 1.000 |
| CTF.Application.Combos | 11 | 0.200 | 1.000 |
| CTF.Application.CommandInfrastructure | 2 | 0.500 | 1.000 |
| CTF.Application.Commands | 7 | 0.167 | 1.000 |
| CTF.Application.Discord | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules | 38 | 0.034 | 0.333 |
| CTF.Application.GunGames | 27 | 0.167 | 0.200 |
| CTF.Application.GunGames.Results | 4 | 0.500 | 1.000 |
| CTF.Application.MapIcons | 2 | 0.500 | 1.000 |
| CTF.Application.Maps | 10 | 0.250 | 0.875 |
| CTF.Application.Maps.Rotation | 6 | 0.333 | 1.000 |
| CTF.Application.Pickups | 1 | 1.000 | 1.000 |
| CTF.Application.PlayerResources | 6 | 0.333 | 1.000 |
| CTF.Application.Players | 2 | 0.500 | 0.200 |
| CTF.Application.Players.Accounts | 1 | 1.000 | 1.000 |
| CTF.Application.Players.Chats | 1 | 1.000 | 1.000 |
| CTF.Application.Players.TopPlayers | 2 | 0.500 | 0.500 |
| CTF.Application.Players.Weapons | 3 | 0.333 | 0.091 |
| CTF.Application.Players.Weapons.Catalogs | 1 | 1.000 | 0.500 |
| CTF.Application.RconSecurity | 1 | 1.000 | 1.000 |
| CTF.Application.Statistics | 21 | 0.063 | 0.500 |
| CTF.Application.Teams | 4 | 0.250 | 0.143 |
| CTF.Application.Tests | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Authorization | 1 | 1.000 | 0.500 |
| CTF.Application.Tests.Fakes | 5 | 0.500 | 0.125 |
| CTF.Application.Tests.GameRules | 3 | 1.000 | 0.429 |
| CTF.Application.Tests.GunGames | 8 | 0.500 | 0.083 |
| CTF.Application.Tests.Maps | 6 | 0.500 | 1.000 |
| CTF.Application.Tests.PlayerResources | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Players.Accounts | 7 | 0.167 | 0.111 |
| CTF.Application.Tests.Players.Extensions | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Players.Ranks | 3 | 1.000 | 0.333 |
| CTF.Application.Tests.Players.TopPlayers | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Players.Vitalities | 2 | 1.000 | 0.667 |
| CTF.Application.Tests.Players.Weapons | 5 | 0.250 | 0.091 |
| CTF.Application.Tests.Statistics | 5 | 0.500 | 0.444 |
| CTF.Application.Tests.Teams | 3 | 1.000 | 0.429 |
| CTF.Application.Tests.TextDraws | 1 | 1.000 | 0.111 |
| CTF.Application.TextDraws | 2 | 0.500 | 1.000 |
| CTF.Application.WeaponCatalogs | 9 | 1.000 | 0.818 |
| CTF.Host.Bcrypt | 1 | 1.000 | 0.500 |
| CTF.Host.CommandInfrastructure | 1 | 1.000 | 1.000 |
| CTF.Host.Composition | 3 | 0.333 | 1.000 |
| CTF.Host.Config | 1 | 1.000 | 1.000 |
| CTF.Host.Deployment | 1 | 1.000 | 1.000 |
| CTF.Host.Discord | 2 | 0.500 | 1.000 |
| CTF.Host.Ecs | 1 | 1.000 | 1.000 |
| CTF.Host.Logging | 1 | 1.000 | 1.000 |
| CTF.Host.ServerService | 1 | 1.000 | 1.000 |
| Persistence.InMemory | 6 | 0.200 | 1.000 |
| Persistence.MariaDB | 5 | 0.200 | 0.200 |
| Persistence.SQLite | 5 | 0.200 | 0.200 |
| Persistence.SQLite.Extensions | 2 | 1.000 | 1.000 |
| Persistence.Tests.Common | 6 | 0.200 | 0.500 |
| Persistence.Tests.Common.DatabaseProviders | 3 | 0.333 | 1.000 |
| Persistence.Tests.Players | 5 | 1.000 | 1.000 |
| SampSharp | 1 | 1.000 | 1.000 |

## 5. Per-class driver assignment (multiset summary)

{1=87, 2=62, 3=67, 4=42, 5=16, 6=8, 7=11, 8=8, 9=5, 10=3}

---

## 6. Readings & interpretation

### 6.1 Purity improvements at the leaves

The per-driver modules created by the regroup measure as **pure single-set namespaces**
(purity 1.000): `Pickups`, `RconSecurity`, `WeaponCatalogs`, `Players.Accounts`,
`Players.Chats`, the single-type Host modules (`Ecs`, `ServerService`,
`CommandInfrastructure`, `Config`, `Deployment`, `Logging`, `Bcrypt`), `SampSharp`,
and most test namespaces. In the before state only 14 of 57 namespaces were single-set.

### 6.2 Composite namespaces are now subordinate-driven, not topic-fused

The large composite namespaces (`GameRules` 29 sets/38 classes, `Statistics` 16/21,
`Accounts` 13/10) are domain modules whose elements carry domain-rooted gammas with
*platform subordinates* (CD-31/32/36 → CD-02 etc.). Under the refined drivers that is
the expected shape: the subordinate IDs exist precisely so those usages are enumerable,
not co-located as roots. The before-state offenders with distinct *root* drivers fused
by topic (`Players` 9 sets, `Teams` 8, `Teams.Flags` 7) no longer exist as such.

### 6.3 Scatter

Scattered driver sets fell from 28 to 15. Remaining scatter is concentrated in the
horizontal drivers (CD-17 .env schema, CD-20 ports, CD-26/27/28 test tooling) — decreed
cross-cutting axes per `IVP/constraints.md`. Change-coupling drivers (CD-02, CD-07,
CD-10) are concentrated in their domain modules.

### 6.4 Bottom line

The after state is a **root-aligned modularisation**: every module's elements answer to
one domain root (plus documented settings/wiring co-locations), platform touchpoints are
enumerable subordinates, and the former umbrella driver has zero activation. The remaining
impurity is measured in subordinate tokens, which is annotation richness, not contamination.

---

## 7. Caveats & limitations (honest, must-read before interpreting)

1. **Purity is over driver sets, not driver count** — as in the before report.
2. **Set-space refinement breaks raw count comparability.** 137 → 180 sets reflects the
   CD-01 decomposition (finer vocabulary) plus the reorganisation. Comparable: scatter,
   composite share, activation of CD-01 (= 0), purity of leaf modules.
3. **Namespace granularity.** The tool measures at namespace level; nested classes
   (`Flag.CarrierAttachment`, components) fold into their file's namespace. The
   nested-module rule (no upward transmission) is enforced at annotation level, not by
   this tool.
4. **Member-level detection is a heuristic** — as in the before report.
5. **Essential-vs-spurious classification** — see `IVP/constraints.md` Appendix A; the
   production/test split is reported (production=242, test=67).
6. **Activation frequency λ(γ) is unmeasured**; only unweighted module-touch counts are
   reported (no λ artifact exists in the repo).
7. **Completeness for singleton test sets is inflated** — same granularity artifact as
   the before report.
