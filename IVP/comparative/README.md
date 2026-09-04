# IVP Comparative — Before → After

> Side-by-side reference for what applying IVP changed. Sources of record:
> [`IVP/before/metrics.md`](../before/metrics.md) (pinned `ivp-before` baseline),
> [`IVP/after/metrics.md`](../after/metrics.md) (canonical measurement of the
> current tree), [`IVP/after/changedrivers.md`](../after/changedrivers.md)
> (live catalogue), `IVP/before/changedrivers.md` (pre-refinement catalogue).

## 1. Comparability rules (read first)

The driver vocabulary itself changed: CD-01 (the monolithic platform driver) was
decomposed into CD-31..CD-44, and CD-29 had already been retired before the
baseline. Consequences:

- **Comparable**: scattered driver sets, composite/single-set namespace shares,
  per-driver activation and containment, CD-01 activation (= 0 after).
- **Not directly comparable**: raw distinct-set counts (137 → 180) and mean
  drivers per class (2.46 → 2.99) — the finer vocabulary splits umbrella sets
  and makes subordinate usage visible. Both rises are vocabulary effects plus
  reorganisation, and are reported, not hidden.

## 2. Cardinalities

| Cardinality | Before | After |
|---|---|---|
| Annotated types (`E`) | 293 | 309 |
| Namespaces (modules) | 57 | 64 |
| Distinct Γ-sets (`E/Γ`) | 137 | 180 |
| Scattered Γ-sets (>1 namespace) | **28** | **15** |
| Composite namespaces | 43/57 (75%) | 35/64 (55%) |
| Single-set namespaces | 14/57 | 29/64 |
| CD-01 (platform umbrella) activation | 114 elements / 35 ns | **0** (decomposed) |
| Mean / median drivers per class | 2.46 / 2 | 2.99 / 3 |

## 3. Driver activation — the drivers that matter

| Driver | Before (elem/ns) | After (elem/ns) | Change |
|---|---|---|---|
| CD-01 platform umbrella | 114 / 35 | **—** (decomposed) | umbrella eliminated |
| → CD-39 attached-object API | ⊂ CD-01 | **1 / 1** | perfectly contained in `Flag.CarrierAttachment` |
| → CD-33 dialogs | ⊂ CD-01 | 20 / 10 | enumerable |
| → CD-40 audio | ⊂ CD-01 | 11 / 4 | enumerable |
| → CD-34 textdraws | ⊂ CD-01 | 14 / 8 | enumerable |
| CD-02 CTF game rules | 50 / 14 | 53 / 10 | more concentrated |
| CD-10 stats/rank | 41 / 14 | 49 / 16 | PlayerInfo split adds elements |
| CD-17 .env schema | 54 / 27 | 49 / 24 | still the widest horizontal axis |

## 4. Structural deltas

| Concern | Before | After |
|---|---|---|
| Modules | 57 topic-named namespaces, most multi-root (`Players` fused 9 root sets, `Teams` 8) | per-root-driver modules (`Accounts`, `Audio`, `Pickups`, `MapIcons`, `TextDraws`, `PlayerResources`, `CommandInfrastructure`, `RconSecurity`, ...) |
| Platform placement | one `Platform`/umbrella home; platform usages invisible inside domain members | 14 subsystem modules; platform usage is a named subordinate (`CD-34 → CD-02`) on every element |
| Persisted state | `PlayerInfo`: one class mixing CD-08/09/10/CD-01, touched by every system | aggregate composing `PlayerAccount`/`PlayerStatistics`/`PlayerRole`/`PlayerAppearance` — same `players` row, independent variation |
| Flag carrier feature | carrier state, attachment rendering, name matching inline in `Flag`/`FlagSystem` | `Flag` (pure CD-02 rules) + nested `CarrierAttachment` (only CD-39 element) + sibling `FlagCarrier` |
| Nested classes | treated as transparent members — drivers flowed upward | **modules**: their driver sets stop at their own boundary; class gamma = union of direct elements |
| Annotations | ~445 CD-01-blurred remarks; per-element coverage partial in places | every element carries its own remark; IDs validated against the catalogue; gammas machine-audited |
| Sub-team layout | `Teams/{ClassSelection,Flags,Statistics}` sub-modules without distinct driver sets | dissolved into their true driver modules (GameRules, Statistics, DI wiring) |

## 5. What improved (with evidence)

1. **Scatter halved** (28 → 15 scattered sets). Remaining scatter is the decreed
   cross-cutting axes (CD-17, CD-20, CD-26/27/28), not domain scatter.
2. **Platform blast radius is enumerable per subsystem** — a `SetAttachedObject`
   change is a 1-element event; textdraw changes touch 42 named elements, not an
   umbrella.
3. **Single-set modules 14 → 29.** New modules (Pickups, MapIcons, Audio,
   RconSecurity, TextDraws, PlayerResources, CommandInfrastructure, all single-type
   Host modules) measure purity 1.000.
4. **Composite namespaces changed character** — domain root + enumerable platform
   subordinates instead of distinct roots fused by topic.
5. **Persisted state stopped being a coupling hub** — a stats-model change can no
   longer force an auth-policy edit via `PlayerInfo`.
6. **Change cost is greppable and machine-audited** — catalogue-validated IDs,
   class gammas = union of direct elements, compliance checks scripted.

## 6. What did not improve (honest)

1. **Distinct sets rose** (137 → 180) and **mean drivers per class rose**
   (2.46 → 2.99) — finer vocabulary + visible subordinates, not a regression in
   coupling, but not a reduction either.
2. **Large domain namespaces still measure low purity by set count**
   (`GameRules` 0.034) — the number counts subordinate tokens; the fused-root
   defect it measured before is gone, but the metric alone doesn't show that.
3. **Costs paid**: +7 namespaces, small extra types (`FlagCarrier`,
   `CarrierAttachment`), large churn across the refactor.
4. **Weighted change-cost is still unproven**: no λ(γ) activation-frequency
   artifact exists, so only unweighted module-touch counts are reported
   (`IVP/before/metrics.md` §7.4 caveat stands).

## 7. Reproduction

```
java IVP/tools/IvpMeasure.java .        # after tree
git worktree add ../ctf-before ivp-before && cd ../ctf-before
java IVP/tools/IvpMeasure.java .        # before baseline
```
