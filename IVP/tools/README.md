# IVP Measurement Tools

Run from the repo root: `java IVP/tools/IvpMeasure.java [rootDir]` (default `.`). Java ≥ 25. Deterministic (verified double-run diff-clean).

## Canonical tool

| Tool | Produces |
|---|---|
| `IvpMeasure.java` | **the** measurement: census, driver-activation table, global stats, per-namespace sets, prod/test split |

### Pinned semantics (do not change without re-baselining)

| Aspect | Rule |
|---|---|
| Element | every `class` / `interface` / `enum` / `struct` / `record` declaration in `src/` + `tests/` (excl. `Usings.cs`, `obj/`, `bin/`) |
| Γ-set | CD codes on the element's **own** nearest `Change drivers:` remark within 12 lines above the declaration — that line only |
| `Injected dependencies` remarks | supplementary; **never** contribute to an element's set |
| Siblings/nesting | `record struct X` counts once; nested types counted if annotated |

### Why pinned

The original ad-hoc scripts (grep-driven, variable scan windows) were mutually inconsistent: greedy line-scans leaked CD codes from adjacent `Injected dependencies` remarks into type sets (CD-21 30→102), and type-regex variants disagreed on `record` counting (285 vs 286–289). Any before/after comparison must use one tool.

## Legacy scripts (historical reference only — do NOT use for measurements)

| Tool | Was used for | Caveat |
|---|---|---|
| `MetricsFull.java` | metrics.md §1–§5 draft | 12-line greedy scan; inject-leak; writes file (arg = path) |
| `Canonical.java` | E / \|E/Γ\| / ns counts | 8-line greedy; excludes `record` |
| `NsCohesion.java` / `AsmCohesion.java` | A.2 / A.3 cohesion tables | greedy scan |
| `ClassCohesion.java` | A.1 member-level cohesion | member detection heuristic (±) |
| `Scatter.java` | scattered-set count | greedy scan |
| `Pairs.java` | B.1 inter-namespace driver pairs | greedy scan |
| `IvpMetrics2.java` | per-driver namespace membership (B.2) | greedy scan |
| `CausalTables.java` | causal-partition.md root/sub data | reads `(root; …)` marks |
| `Audit5.java` / `Audit6b.java` / `Audit7.java` | annotation-integrity audits (dangling arrows / root contradictions / sibling marks) | safe to re-run anytime |
| `UnannotatedMembers.java` / `MemberCoverage.java` | member-annotation coverage | heuristic |
| `ProdTestSplit.java` | prod/test census | superseded by IvpMeasure |

## Re-baselining procedure (after any driver-model change)

1. `java IVP/tools/IvpMeasure.java .` → capture output.
2. Diff against the last committed baseline; explain every changed row.
3. Update `IVP/before/*` (or `IVP/after/*`) numeric tables from this output only.
4. Tag the commit (`git tag ivp-<state>`) so the measured tree is reproducible.