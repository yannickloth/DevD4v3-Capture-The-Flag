# IVP Meta — Methodology & Invariants

> Documents *how change drivers are identified, annotated and measured* in this project, distilled from the opencode session history (`~/.local/share/opencode/opencode-stable.db`, directory `Capture-The-Flag`) and the analysis artifacts it produced. This folder is the **process reference**; the *catalogue/measurements* live in `../before/`, `../after/`, `causal-order.md`, `causal-partition.md`, `constraints.md`.

| Doc | Purpose |
|---|---|
| `methodology.md` | The tasks/rules to identify the change drivers of an element or module (what a driver is, how to find/annotate/order/review it) |
| `invariants.md` | The change-driver **invariants** — properties that must never be violated (identification, causal, purity/measurement, process) |

## Provenance (what "all this" is)

Synthesised from 21 Capture-The-Flag sessions across three lineages:

| Lineage | Root session | Subagents | Work |
|---|---|---|---|
| IVP study | `ses_fa2562dccffecvsOwHO5CR7RBx` | `@explore` ×5 (analyze subsystems), `@general` ×5 (annotate subsystems) | Change-driver definition, exhaustive per-element inventory, causal ordering, metrics, cohesion |
| Content migration | `ses_fa2283dc3ffeNmBelgpi59UvdW` | `@general` ×6 (CD-29 test-stamp rewrites) | CTF.Host/CTF.Application content migration; retire spurious CD-29 |
| Synthesis | `ses_f9b28f92affekl1gWDfiWQghuM` | — | Distil this methodology + invariants |

## Where each rule lands

| Concern | Reference |
|---|---|
| The 29-driver catalogue (CD-01..CD-30, CD-29 retired) | `../before/changedrivers.md` §1 |
| Namespace → driver-set map (source of truth for namespaces) | `../before/changedrivers.md` §2 |
| Per-element driver assignment (stamped in source) | `../before/changedrivers.md` §3 + `<remarks>` in code |
| Causal order & subordination | `../causal-order.md` |
| Causal partition (namespaces/assemblies) | `../causal-partition.md` |
| IVP-application constraints (other axes) | `../constraints.md` |
| Canonical measurement tool + pinned semantics | `../tools/README.md`, `../tools/IvpMeasure.java` |
