# IVP After — Architecture & Compliance Report

> State of the tree after the full IVP pass. The catalogue lives in
> [`changedrivers.md`](changedrivers.md); the method in `IVP/meta` and `IVP/causal-order.md`.
> Compliance state at commit `35146f5+` (post module-placement fixes).

## 1. Driver sets

- 42 active drivers: CD-02..CD-28, CD-30, CD-31..CD-44.
- CD-01 **decomposed** into the 14 platform sub-drivers CD-31..CD-44 — zero citations remain.
- CD-29 retired (methodology error, never renumbered).

## 2. Module map

The namespace layer is now partitioned by **exact change-driver set**. Each original
domain namespace is split into `Core` (types whose set is exactly the domain root) and
`Gamma_<sorted CD ids>` (types that add platform/config/test subordinates) sub-namespaces.
Parent namespaces hold only DI roots and documented generated-resource deviations.

Examples:

| Parent namespace | Exact-set sub-namespaces |
|---|---|
| `CTF.Application.Statistics.Score` | `Core` (`CD-10`), `Gamma_CD10_CD20`, `Gamma_CD10_CD06`, `Gamma_CD10_CD31_CD32_CD34`, … |
| `CTF.Application.GameRules.Flag` | `Core` (`CD-02`), `Gamma_CD02_CD17`, `Gamma_CD02_CD31`, `Gamma_CD02_CD35`, … |
| `CTF.Application.Combat.Health` | `Core` (`CD-03`), `Gamma_CD03_CD31`, `Gamma_CD03_CD09_CD15_CD17_CD31_CD32_CD36_CD43`, … |
| `CTF.Application.Accounts.Authentication` | `Core` (`CD-08`), `Gamma_CD08_CD20_CD25_CD31_CD32_CD36`, `Gamma_CD08_CD32`, … |

The full live map is produced by `java IVP/tools/NamespaceRootPurity.java .` (root-causal
view) and `java IVP/tools/ExactSetGroups.java .` (exact full-set view).

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
7. **Exact-set submodules.** Inside each domain namespace, types with identical full
   `Change drivers:` sets are co-located; types with different sets are split into `Core`
   (root-only) or `Gamma_<drivers>` sub-namespaces. This is the strict IVP submodule rule.
8. **Settings and wiring co-locate with their domain** (`FlagAutoReturnSettings` CD-17
   in GameRules, `ServiceCollectionExtensions` CD-21 per module) — a documented
   deviation traded for locality.

## 4. Compliance state

| Check | Result |
|-------|--------|
| Every element carries its own `Change drivers` remark | ✅ 0 missing (generated `*.Designer.cs` excluded) |
| Driver IDs ∈ catalogue | ✅ 0 invalid |
| CD-01 / CD-29 citations | ✅ 0 |
| Class gamma = union of direct elements | ✅ 33 gaps completed; remaining class-only drivers audited as base-class / signature-type / injected-contract transmission |
| Module root sets | ✅ 168/178 namespaces single-root-set; the 10 root-set composites are the documented essential deviations in `IVP/constraints.md` (persistence providers, generated resource classes, test fakes, aggregate facets, composition roots) |
| Exact-set namespace audit | ✅ 154/178 namespaces single-set by full `Change drivers:` equality; every non-essential namespace is recursively split into `Core`/`Gamma_*` exact-set submodules |
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
