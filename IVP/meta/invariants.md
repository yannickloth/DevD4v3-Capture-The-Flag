# Change-Driver Invariants

> The properties that must **never** be violated when identifying, annotating, ordering, or measuring change drivers. Each invariant names the property, states the rule, gives the failure it prevents, and (where applicable) the audit/check that enforces it. Companion to `methodology.md`.

## 1. Identification invariants (what a driver *is*)

| # | Invariant | Statement | Prevents | Check |
|---|---|---|---|---|
| **I1** | External-domain-only | A change driver is an external authority in the operating domain anchored to a verifiable artifact; never an internal structural property of the code graph. | "seam"/"code-under-test" invented drivers | counterfactual test |
| **I2** | Counterfactual-required | A driver is valid iff removing its anchoring artifact's condition removes the element's modification requirement. No artifact → no driver. | fabricated drivers | per-claim artifact check |
| **I3** | No-proxy / no-co-variation | Drivers are never derived from change-history correlation, ownership, naming, layer, or semantic similarity. Co-location implies nothing about identity. | proxy drivers | review |
| **I4** | Dependency ≠ driver | `Injected dependencies` remarks are supplementary; they **never** contribute to an element's Γ-set. An injection is driven by the injected type's contract + CD-21 wiring. | CD-21 30→102 leak | pinned scan window (`IvpMeasure`) |
| **I5** | Retired-driver (CD-29) | Being *depended upon* is a structural relation, not a driver. A test's real drivers are the **domain drivers of its code under test** (as root), tooling subordinated. | "code-under-test" pseudo-driver | `grep -rn CD-29` = ∅ |
| **I6** | Mock-inheritance | A mock/fake carries the domain driver of the seam it mimics (mocked interface or type+method-signature surface), not the seam's internal drivers. | mis-drive fakes by inner domain | prod/test mirror check |

## 2. Causal invariants

| # | Invariant | Statement | Prevents | Check |
|---|---|---|---|---|
| **I7** | Containment-not-ranking | Subordination ("exists because of") is causal containment (`Γ_exist(parent) ⊆ Γ_exist(child)`), never an importance ordering. No primary/main/secondary. | ranking vocabulary | review |
| **I8** | No self-root-contradiction | A driver cannot be `(root)` *and* sit on the left of its own arrow (`CD-X (…) → Y`). | contradictory marks | `Audit6b` |
| **I9** | No dangling subordination | Every `X → Y` target must be present as an `Y (label)` unit in the same remark. | arrow to non-existent driver | `Audit5` |
| **I10** | Siblings carry root marks | Each side of a `‖` unit is itself a `(root; …)`-marked driver; siblings are unordered and never chained into an arrow. | pseudo-siblings / chained siblings | `Audit7` |

## 3. Purity & measurement invariants (must survive the tool)

| # | Invariant | Statement | Prevents | Check |
|---|---|---|---|---|
| **I11** | Purity-over-sets-not-tokens | `purity(M) = 1 / \|distinct driver **sets**\|` among M's elements. A module whose elements all share `{CD-02, CD-10}` is pure; only multi-**set** co-location = contamination. Driver-count figures are token proxies, never purity. | misreading multi-driver as impure | `IvpMeasure` Γ-set logic |
| **I12** | Single-driver ≠ single-set | "One driver per class" is a token statement; purity is defined over set equality at module granularity. | conflating the two axes | — |
| **I13** | Γ-sets are the measurement invariant | Moving an element changes its namespace, **not** its drivers. Re-annotation is forbidden during/after refactor. The E/Γ partition is the before/after constant. | re-drive on repackaging | diff-against-baseline |
| **I14** | Granularity-relative | IVP holds at every level (member→class→namespace→assembly→deployable); a class/composite type is a module just like a namespace. Assembly purity ≈ 0 is expected, not a defect (assembly = deployment unit). | cross-granularity overreach | per-level recompute |
| **I15** | IVP-one-axis-among-many | IVP cannot override platform / persistence / composition-DI / game / generated-code axes. Deviations are quantified (contamination/incompleteness) and recorded symmetrically, never silently. Only change-coupling scatter is the IVP target. | silent axis demotion | `constraints.md` |

## 4. Process / meta invariants

| # | Invariant | Statement | Prevents | Check |
|---|---|---|---|---|
| **I16** | No-fabrication / honest caveats | Weighted cost `Σλ(γ)κ(γ)` is **not** computed because no activation-frequency artifact (λ) exists in the repo; unmeasured quantities are declared, never invented. Essential-vs-spurious is cited per element (`constraints.md` Appendix A); unlisted = spurious. | invented metrics / sound-right claims | review |
| **I17** | One-canonical-tool | All before/after numbers come from the single deterministic tool (`IvpMeasure.java`, pinned semantics); legacy ad-hoc scripts are inconsistent (leaked inject codes, disagreed on `record`) and are disallowed for measurement. | non-comparable baselines | pinned tool only |
| **I18** | Convergence | Change-driver assignment is done only after **2 consecutive review-fix rounds with 0 issues**. | premature "done" | review log |

## 5. Baseline facts (invariant numeric anchor)

| Quantity | Before value | Meaning |
|---|---|---|
| Elements `E` (annotated types) | 293 | code elements supplied to the partition |
| Distinct driver-sets `\|E/Γ\|` | 137 | Γ-equivalence classes = IVP normative partition (one module per set) |
| Namespaces (actual modules) | 57 | the partition the code actually has |
| Scattered sets (span > 1 namespace) | 28 | a driver-set living in several modules = the scatter/IVP defect |
| Composite namespaces (multi-set) | 43 / 57 | contamination |
| Mean namespace purity | 0.482 | inverse contamination |

**IVP-compliance invariant (target):** `#namespaces == #distinct-Γ-sets` (137). The gap `137 − 57` is the total E/Γ-non-conformance to eliminate while holding the Γ-sets (**I13**) and driver-activation table constant.
