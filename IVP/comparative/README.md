# IVP Comparative — Before → After

> Side-by-side reference for what applying IVP changed. Sources of record:
> [`IVP/before/metrics.md`](../before/metrics.md) (pinned `ivp-before` baseline),
> [`IVP/after/metrics.md`](../after/metrics.md) (canonical measurement of the
> current tree), [`IVP/after/changedrivers.md`](../after/changedrivers.md)
> (live catalogue), `IVP/before/changedrivers.md` (pre-refinement catalogue).

## 1. Comparability rules (read first)

Both states are measured on the **same refined driver layer** (CD-01 decomposed
into CD-31..CD-44 with zero citations; CD-29 retired): the before tree was
re-baselined remark-only from the apply-ivp annotations (`main` a3cb4a9), so the
states differ by *code structure only*. Consequences:

- **Comparable**: scattered driver sets, composite/single-set namespace shares,
  per-driver activation and containment, CD-01 activation (= 0 after).
- Raw distinct-set counts are comparable in direction but still encode the
  annotation-layer additions (sub-drivers named on elements that previously
  carried no remark): 171 → 180.

## 2. Cardinalities

| Cardinality | Before | After |
|---|---|---|
| Annotated types (`E`) | 293 | 309 |
| Namespaces (modules) | 57 | 64 |
| Distinct Γ-sets (`E/Γ`) | 171 | 180 |
| Scattered Γ-sets (>1 namespace) | **23** | **15** |
| Composite namespaces | 45/57 (79%) | 35/64 (55%) |
| Single-set namespaces | 12/57 | 29/64 |
| CD-01 activation | **0** (decomposed; catalogue keeps the row for history) | **0** |
| CD-39 attached-object activation | **0** (attachment inline, unattributed) | 1 / 1 (`Flag.CarrierAttachment`) |
| Mean / median drivers per class | ~2.9 / 3 | 2.99 / 3 |

## 3. Driver activation — the drivers that matter

| Driver | Before (elem/ns) | After (elem/ns) | Change |
|---|---|---|---|
| CD-01 platform umbrella | 0 / 0 (decomposed) | 0 / 0 | umbrella stays eliminated in both |
| CD-39 attached-object API | **0 / 0** (inline in `Flag`, unattributed) | **1 / 1** | isolated and named in `Flag.CarrierAttachment` |
| CD-33 dialogs | 20 / 13 | 20 / 10 | slightly more concentrated |
| CD-40 audio | 11 / 6 | 11 / 4 | more concentrated |
| CD-34 textdraws | 14 / 8 | 14 / 8 | same, now in its own module |
| CD-02 CTF game rules | 50 / 14 | 53 / 10 | more concentrated |
| CD-10 stats/rank | 44 / 16 | 49 / 16 | PlayerInfo split adds elements |
| CD-17 .env schema | 50 / 26 | 49 / 24 | still the widest horizontal axis |

## 4. Structural deltas

| Concern | Before | After |
|---|---|---|
| Modules | 57 topic-named namespaces, mostly multi-set (`Players` 10 sets over 10 classes, `Teams` 10/13) | per-root-driver modules (`Accounts`, `Audio`, `Pickups`, `MapIcons`, `TextDraws`, `PlayerResources`, `CommandInfrastructure`, `RconSecurity`, ...) |
| Platform placement | one `Platform`/umbrella home; platform usages invisible inside domain members | 14 subsystem modules; platform usage is a named subordinate (`CD-34 → CD-02`) on every element |
| Persisted state | `PlayerInfo`: one class mixing CD-08/09/10/CD-01, touched by every system | aggregate composing `PlayerAccount`/`PlayerStatistics`/`PlayerRole`/`PlayerAppearance` — same `players` row, independent variation |
| Flag carrier feature | carrier state, attachment rendering, name matching inline in `Flag`/`FlagSystem` | `Flag` (pure CD-02 rules) + nested `CarrierAttachment` (only CD-39 element) + sibling `FlagCarrier` |
| Nested classes | treated as transparent members — drivers flowed upward | **modules**: their driver sets stop at their own boundary; class gamma = union of direct elements |
| Annotations | same refined layer (re-baselined remark-for-remark); platform usages unattributed where elements predated the sub-drivers | every element carries its own remark; IDs validated against the catalogue; gammas machine-audited |
| Sub-team layout | `Teams/{ClassSelection,Flags,Statistics}` sub-modules without distinct driver sets | dissolved into their true driver modules (GameRules, Statistics, DI wiring) |

## 5. What improved (with evidence)

1. **Scatter cut by a third** (23 → 15 scattered sets), with the remaining
   scatter concentrated in the decreed cross-cutting axes (CD-17, CD-20,
   CD-26/27/28), not domain scatter.
2. **Platform blast radius is named and contained per subsystem** — the
   attached-object API is a 1-element module member (`Flag.CarrierAttachment`)
   instead of inline unattributed calls; every subsystem (dialogs, audio,
   textdraws, pickups, timers) has its own ID, module, and enumerable touch set.
3. **Single-set modules 12 → 29.** New modules (Pickups, MapIcons, Audio,
   RconSecurity, TextDraws, PlayerResources, CommandInfrastructure, all single-type
   Host modules) measure purity 1.000.
4. **Composite namespaces changed character** — domain root + enumerable platform
   subordinates instead of distinct roots fused by topic.
5. **Persisted state stopped being a coupling hub** — a stats-model change can no
   longer force an auth-policy edit via `PlayerInfo`.
6. **Change cost is greppable and machine-audited** — catalogue-validated IDs,
   class gammas = union of direct elements, compliance checks scripted.

## 6. What did not improve (honest)

1. **Distinct sets rose slightly** (171 → 180) — annotation additions
   (sub-driver remarks on elements that previously carried none), not new
   coupling; mean/median drivers per class are unchanged (~2.9 / 3).
2. **Large domain namespaces still measure low purity by set count**
   (`GameRules` 0.034) — the number counts subordinate tokens; the fused-root
   defect it measured before is gone, but the metric alone doesn't show that.
3. **Costs paid**: +7 namespaces, small extra types (`FlagCarrier`,
   `CarrierAttachment`), large churn across the refactor.
4. **Weighted change-cost is still unproven**: no λ(γ) activation-frequency
   artifact exists, so only unweighted module-touch counts are reported
   (`IVP/before/metrics.md` §7.4 caveat stands).

## 7. Why not maximal cohesion everywhere?

IVP groups elements by *identical driver sets* — it does not make multi-driver elements disappear. Four structural reasons the system cannot reach purity 1.0 / completeness 1.0 everywhere:

1. **Subordinates are real variation.** A CD-02-rooted system that plays audio and shows GameText genuinely answers to `{CD-02, CD-40, CD-35}`. That is one coherent element, not contamination — the refinement exists to *name* these, and naming them lowers set-purity while raising honesty. The alternative (max purity) would be exiling every platform call into adapters, adding indirection without removing coupling.
2. **Decreed composites and cross-cutting axes.** `IVP/constraints.md` App. A: `Startup` (composition), the repository managers (ports × dialects), DI extensions, `.env` schema, test tooling — these fuse drivers by decree of other axes (composition, persistence, deployment). IVP is one axis among many.
3. **Documented locality deviations.** Settings (CD-17) and DI wiring (CD-21) co-locate with their domain module instead of their driver module — an accepted trade, and it caps both purity and the single-set namespace count.
4. **Shared-set design.** `Team`/`Flag` and their `OnFlag*`/`FlagSystem` handlers intentionally share Γ-sets across type boundaries (domain model + reaction systems). That deflates completeness of the individual types while being causally correct — the sets are split by role, not scattered by accident.

So the residual impurity is: subordinate visibility (metric artifact), decreed composites (other axes), locality trades (accepted), and role-split sets (by design). What IVP removed is the *fifth* kind: distinct root drivers fused by topic — which the before state was full of.

## 8. The four-quadrant verdict

### Big wins

- **`PlayerInfo` god aggregate dissolved** — the before report's headline offender (52 members, purity 0.077, extent 0.827) is now a 4-property root; members live in `PlayerAccount`/`PlayerStatistics`/`PlayerRole`/`PlayerAppearance`.
- **`Flag` at purity 1.000**, and the attached-object API is a **1-element driver** (CD-39, `CarrierAttachment`) — the smallest possible blast radius.
- **Scattered sets 23 → 15**; remaining scatter is the decreed horizontal axes (CD-17, CD-20, CD-26/27/28).
- **Single-set namespaces 12 → 29**, pure-and-complete leaf modules across Application and Host.
- **Production completeness = 1.0 in 30/45 namespaces**; severe incompleteness (< 0.5) halved.
- **Impact sets are greppable**: any driver's touch list is a query, machine-audited against the catalogue.

### Big losses

- **`GameRules` is now the system's most contaminated module by the metric**: purity 0.034, 29 Γ-sets over 38 classes. The sets are subordinate-driven (domain root + named platform IDs), but the number is real and it is the price of subordinate visibility plus one big domain module.
- **Class-weighted purity flat (0.337 → 0.337)** — the aggregate metric we hoped to move did not move.
- **10 types declined in member-level purity** (`PlayerRepository` 0.5 → 0.25, `PlayerPauseSystem` 1.0 → 0.25, `FakePlayer` 0.5 → 0.333, ...) — subordinate-ID visibility split formerly identical sets.

### Good surprises

- **`GunGames.Results` completeness 0.042 → 1.000** (purity 0.25 → 0.500): the GunGame reward handlers now fully own their driver sets in one module — before, they were the report's worst scatter offender.
- **New modules are pure *and* complete on arrival**: `Pickups`, `RconSecurity`, `MapIcons`, `TextDraws`, `Discord`, `Players.Accounts/Chats`, `WeaponCatalogs` (completeness 0.818), every single-type Host module.
- **`Maps.Rotation` completeness 1.000** (0.500 before) after absorbing `LoadTime`/`TimeLeft` — the one declining purity came with full set ownership.
- **`PlayerExtensions` purity 0.333 → 1.000** as a side effect of the account split.
- **The paired-namespace trend**: of namespaces existing in both trees, 9 improved in purity, only 1 declined.

### Disappointments

- **Class-weighted purity did not improve at all** (0.337 → 0.337) — large domain modules dominate the weighting and legitimately hold many distinct Γ-sets. If the project's success metric were weighted purity, IVP did not deliver it.
- **`Team` completeness is still 0.022** and `GunGameSystem` 0.021 — the domain-model/reaction-system role split keeps their sets shared across types; the metric reads it as incompleteness.
- **Composite namespaces remain 55%** — the subordinate-driven composites are honest, but a reader of only the composite count would miss the difference from before.
- **The weighted change-cost claim remains unproven** — no λ(γ) artifact exists, so "IVP reduced change cost" rests on unweighted touch counts and argument, not measurement.

## 9. Causal cohesion evolution (purity & completeness)

From §4 of both metrics reports (same driver layer; structure-only diff):

| Measure | Before | After |
|---|---|---|
| Mean purity per namespace (all) | 0.442 | **0.622** |
| Mean purity per namespace (production) | 0.367 | **0.574** |
| Class-weighted purity | 0.337 | 0.337 (flat) |
| Pure namespaces (purity = 1.0) | 12 | **29** |
| Pure production namespaces | 5/42 | **18/45** |
| Class-weighted completeness (all / production) | 0.531 / 0.541 | **0.627 / 0.672** |
| Production namespaces with completeness < 0.5 | 15/42 | **8/45** |
| Production namespaces with completeness = 1.0 | — | **30/45** |
| Paired namespaces: purity improved / declined | — | 9 improved, 1 declined (`Maps.Rotation` 0.500 → 0.333, LoadTime/TimeLeft joined) |

Reading:

- **Unweighted purity and completeness clearly improved** — the regroup produced
  many pure *and* complete leaf modules (Pickups, RconSecurity, MapIcons,
  TextDraws, Discord, all single-type Host modules). Before, the worst namespaces
  were multi-root topic fusions (`Players`, `Teams`, `Players.Accounts.Statistics`
  at purity 0.100); after, the lowest are single-root domains with
  subordinate-rich elements (`GameRules` 0.034, `Statistics` 0.063).
- **Class-weighted purity is flat (0.337 → 0.337)** — the candid limit of the win.
  Weighted by class count, the big domain modules dominate and legitimately hold
  many distinct Γ-sets because every platform touchpoint now carries a named
  subordinate ID. The removed contamination (distinct roots fused by topic) is
  offset in this number by subordinate visibility — annotation richness, not
  residual topic-fusion.
- **Completeness is the systemic win**: production namespaces fully owning their
  driver sets went from a minority to 30/45, and severe incompleteness (< 0.5)
  halved — driver sets are no longer split across modules.

## 10. Pure modules — namespaces and types

**Namespaces** (module purity, §4 of both metrics reports):

| | Before | After |
|---|---|---|
| Pure namespaces (purity 1.0) | 12/57 | **29/64** |
| Pure production namespaces | 5/42 | **18/45** |
| Pure *and* complete (purity 1.0, completeness 1.0) | sparse | Pickups, RconSecurity, MapIcons, TextDraws, Discord, WeaponCatalogs, Players.Accounts/Chats, Ecs, ServerService, CommandInfrastructure, Config, Deployment, Logging, Bcrypt, SampSharp, most test namespaces |

**Types** (member-level purity, `ClassCohesion` tool; a type is pure when all
its members share one Γ-set):

| | Before | After |
|---|---|---|
| Types measured | 233 | 249 |
| Mean member-level purity | 0.812 | 0.807 |
| Pure types | 159 (68%) | **169 (68%)** |

The type-level aggregate is flat, but the extremes moved decisively:

- **`PlayerInfo`, the system's worst god aggregate** (52 members, purity 0.077,
  contamination extent 0.827 — the before report's headline offender) is now a
  4-property composition root (purity 0.250); its former members live in the
  single-purpose sub-entities `PlayerAccount`, `PlayerStatistics`, `PlayerRole`,
  `PlayerAppearance`.
- **`Flag` reached purity 1.000** (0.500 before): pure CD-02 rule members, with
  CD-39 isolated in the nested `CarrierAttachment` module and carrier state in
  the sibling `FlagCarrier`.
- `PlayerExtensions` 0.333 → 1.000; `Team` 0.125 → 0.143.
- **10 types declined** — a refinement effect, not new coupling: their members
  gained distinct platform-subordinate IDs (CD-31/32/36/41/43), splitting
  formerly identical Γ-sets (e.g. `PlayerRepository` 0.5 → 0.25 with the
  dialect/port naming, `PlayerPauseSystem` 1.0 → 0.25). The drivers were always
  there; the annotations now say so.

Reading: *namespace* purity is where IVP moved the system (structural regroup);
*type* purity improved where it mattered (the god aggregate and the flag rules
engine) and stayed flat in aggregate because subordinate visibility splits
sets without adding coupling.

## 11. Reproduction

```
java IVP/tools/IvpMeasure.java .        # after tree
git worktree add ../ctf-before main && cd ../ctf-before
java IVP/tools/IvpMeasure.java .        # before baseline
```
