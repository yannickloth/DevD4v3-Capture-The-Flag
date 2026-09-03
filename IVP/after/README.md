# IVP "After" Measurement

Measure the refactored tree with the **same** canonical tool used for the before state, so before/after numbers are directly comparable.

## Procedure

1. In the `apply-ivp` worktree (refactored tree), from its root:
   ```
   java /path/to/main/IVP/tools/IvpMeasure.java .
   ```
   (or after syncing main's `IVP/tools` into the branch: `java IVP/tools/IvpMeasure.java .`)
2. Save the output as the raw after-tables.
3. Write `IVP/after/metrics.md` + `IVP/after/analysis.md` mirroring the `IVP/before/*` structure, from this output only.
4. Compare against the before baseline (pinned tag) — every delta must trace to a structural change (namespace/assembly repackaging), not a tooling change.
5. Tag the measured commit: `git tag ivp-after`.

## Expected effects of IVP repackaging (what to look for)

| Metric | Before | Expected after |
|---|---|---|
| distinct driver-sets (E/Γ) | 137 | ↓ toward the #namespaces (one module per driver-set is the normative partition) |
| scattered sets (span >1 namespace) | 28 | ↓ toward 0 (a driver-set living in several modules is the IVP defect) |
| composite namespaces (multi-set mixes) | 43/57 | ↓ (namespaces align to single driver-sets or documented siblings) |
| driver activation (elements per driver) | as recorded | **unchanged** (annotation layer identical — only layout moves) |
| purity per namespace | 0.100–1.000 | ↑ (fewer co-located sets per namespace) |

## Non-goals

- Do NOT re-annotate drivers during/after the refactor: the Γ-sets are the measurement invariant. Moving an element changes its namespace, not its drivers.
- Do NOT use the legacy greedy scripts (see `../tools/README.md`).
- Do NOT "fix" an after-number to match before: deltas are the signal.

## Reference baseline

- **Baseline = `IvpMeasure` output on the pinned `ivp-before` tag.** The tag is created after the in-flight change-driver fixes land; the before docs are then recomputed from the same tool (single source of truth for both states).
- Heads-up: the original recorded values (285 types / 148 sets) were measured on a partial snapshot with greedy-scan artifacts and are superseded. The canonical recomputation on the corrected annotation layer (CD-29 retired, tests re-rooted, exhaustive review) gives **293 types / 137 sets / 28 scattered / 43 of 57 composite** — see the Recomputation note in `IVP/before/metrics.md`.