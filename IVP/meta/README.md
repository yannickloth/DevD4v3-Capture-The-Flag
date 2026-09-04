# IVP Meta — Methodology & Invariants

> Documents *how change drivers are identified, annotated and measured* in this project, distilled from the opencode session history (`~/.local/share/opencode/opencode-stable.db`, directory `Capture-The-Flag`) and the analysis artifacts it produced. This folder is the **process reference**; the *catalogue/measurements* live in `../before/`, `../after/`, `causal-order.md`, `causal-partition.md`, `constraints.md`.

| Doc | Purpose |
|---|---|
| `methodology.md` | The tasks/rules to identify, document, then **apply** the change drivers of an element or module (what a driver is; how to find/annotate/order/review it; how to regroup into modules by root driver) |
| `invariants.md` | The change-driver **invariants** — properties that must never be violated (identification, causal, purity/measurement, process) |

## Provenance (what "all this" is)

Synthesised from opencode sessions across the main and `-ivp` worktrees:

| Lineage | Root session | Worktree | Subagents | Work |
|---|---|---|---|---|
| IVP study | `ses_fa2562dccffecvsOwHO5CR7RBx` | main | `@explore` ×5, `@general` ×5 | Change-driver definition, exhaustive per-element inventory, causal ordering, metrics, cohesion |
| Content migration | `ses_fa2283dc3ffeNmBelgpi59UvdW` | main | `@general` ×6 (CD-29 test rewrites) | Content migration; retire spurious CD-29 |
| Synthesis | `ses_f9b28f92affekl1gWDfiWQghuM` | main | — | Distil methodology + invariants |
| IVP application | `ses_f998c5385ffebVMHxkBa3fZCqI` | `-ivp` (`apply-ivp`) | `@general` ×many (PlayerInfo call-site migration, driver audits, CD-01 re-stamp) | Apply IVP: type-level regroup → **member-level** application (§6.2) → **placement by root driver** → **same-table entity decomposition** (`PlayerInfo` → `Account`/`Stats`/`Role`/`Appearance`, §6.4) → **sibling/nested grouping** (`Flag`, §6.5) → **module re-audit** (§6.6); corrected dependency model (§6.3); **decomposed CD-01 → CD-31..44** (§4.1); removed `IRank`. Distilled into `methodology.md` §6 |

## Where each rule lands

| Concern | Reference |
|---|---|
| The driver catalogue (CD-02..CD-30 domain drivers; CD-29 retired; platform CD-01 decomposed into CD-31..CD-44) | `../before/changedrivers.md` §1 |
| Namespace → driver-set map (source of truth for namespaces) | `../before/changedrivers.md` §2 |
| Per-element driver assignment (stamped in source) | `../before/changedrivers.md` §3 + `<remarks>` in code |
| Causal order & subordination | `../causal-order.md` |
| Causal partition (namespaces/assemblies) | `../causal-partition.md` |
| IVP-application constraints (other axes) | `../constraints.md` |
| Applying IVP / regrouping by root driver into modules | `methodology.md` §6 (see `-ivp` worktree, branch `apply-ivp`) |
| Canonical measurement tool + pinned semantics | `../tools/README.md`, `../tools/IvpMeasure.java` |
