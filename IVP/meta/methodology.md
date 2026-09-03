# Methodology — Identifying the Change Drivers of an Element or Module

> How to determine and document the change drivers of any element (type/member) or module (class/namespace/assembly). Companion to `invariants.md`. Terminology follows the IVP book series (`cohesion-2-nature`, `cohesion-4-causal`).

## 1. Definitional core

| Rule | Statement |
|---|---|
| **Driver** | An **external forcing condition in the operating domain** that, when it changes, creates a requirement for an element to be modified, via a documentable step-by-step pathway anchored in a domain artifact (statute, contract, spec, standard, config schema). |
| **Counterfactual test** | *If the anchoring artifact changed to remove the relevant condition, would the element's modification requirement disappear or shift?* No artifact → no driver. |
| **No proxy reasoning** | Co-variation (files changed together), team ownership, layer uniformity, semantic similarity are **never** drivers. Only the external authority itself. |
| **No ranking** | "primary/main/secondary" is forbidden; all drivers are equal. Rarity/frequency informs isolation effort, never identity. |
| **Anchoring** | Every driver is anchored in a concrete verifiable artifact (README rule, `.env.example`, `schema.sql`, platform API, config section). |
| **Not eliminated by design** | Encapsulation/abstraction bounds blast radius but never removes a driver. |

## 2. Per-element identification (the task)

For **every** type/class/enum/record and **every** public/internal member (method, property, field, event, constructor, enum member):

1. Read the file and every file it reaches/relates to. Exhaustive — never skip a member.
2. For each element report: fully-qualified name (namespace + type + member).
3. State each driver as *"driver X creates a change requirement for this element because [specific pathway]; anchored in [artifact]"* — name the **specific rule**, not a vague category.
4. Select the driver by the code's *behaviour*, not its grouping (see map §4).
5. Include **only** drivers that actually force change; never add every driver to everything.
6. **Mock/dependency rule:** a mock/fake inherits the domain drivers of the *seam it mimics* (the mocked interface or type+method-signature surface), not the seam's internal drivers. A DI-injection point is driven by the injected type's own domain drivers (+ CD-21 wiring).

## 3. Annotation (how a driver is stamped)

- Add `<remarks>Change drivers: …</remarks>` inside/after the element's existing doc comment — one remark per element (type + each member). **No code changes.**
- File-scoped namespaces (`namespace X.Y;`) cannot carry `///` comments → namespace→driver map is kept in `changedrivers.md` §2 as the single source of truth (union of contained types' sets).
- Causal notation **augments**, never replaces, the flat codes:
  - `(root)` — causal head, no upstream predecessor.
  - `X → Y (reason)` — *subordinated* / "exists because of", with existence reason.
  - `X ‖ Y` — unordered siblings (neither causes the other); never chain siblings.
  - Single-driver elements need no arrow (their only driver is their root by default).
- Order clauses causally: root(s) first, then subordinated machinery in causal depth.
- `Injected dependencies` remarks are **supplementary and separate** — never contribute to the element's Γ-set.

### Example (AccountAuthenticator, as annotated)
```
<remarks>Change drivers:
  CD-08 (root);
  CD-20 → CD-08 (subordinated: persistence exists because the account exists);
  CD-25 → CD-08 (subordinated: hashing protects the password);
  CD-01 → CD-08 (platform mechanism).</remarks>
<remarks>Injected dependencies (change drivers of these elements):
  passwordHasher -> CD-25; playerRepository -> CD-20. Each injection is driven by the
  contract of its injected type + CD-21 (DI wiring).</remarks>
```

## 4. Driver-selection map (this codebase)

| Behaviour in code | Driver |
|---|---|
| game-rule logic (flag/match/round/team/score) | CD-02 |
| combat/weapon rules | CD-03 |
| weapon-catalog config | CD-04 |
| combo definitions | CD-05 |
| coin economy | CD-06 |
| GunGame gate | CD-07 |
| account/auth | CD-08 |
| authorization gating / role | CD-09 |
| player statistics / rank | CD-10 |
| map values | CD-11 |
| rotation | CD-12 |
| chat rules | CD-13 |
| anti-cheat policy | CD-14 |
| command set | CD-15 |
| RCON security | CD-16 |
| config-bound settings (+ domain driver if value feeds game logic) | CD-17 |
| database schema / player data model | CD-18 |
| MariaDB SQL dialect | CD-19 |
| outbound repository contract | CD-20 |
| DI container / composition | CD-21 |
| hosting / deployment | CD-22 |
| Serilog logging | CD-23 |
| Discord webhook | CD-24 |
| BCrypt hashing | CD-25 |
| NUnit test-framework contract | CD-26 |
| FluentAssertions contract | CD-27 |
| NSubstitute mock contract | CD-28 |
| SQLite SQL dialect | CD-30 |

> **CD-29 retired** (methodology error): being *depended upon* is structural, not a driver.

## 5. Causal-order (module/namespace level)

1. Determine each driver's role: **root** (independent authority) | **subordinated** (`→`, exists only because of the root) | **sibling** (`‖`, unordered).
2. Resolve role by the **existence counterfactual**: *would this element/feature exist without the superior mechanism/root?* No → subordinated; yes → sibling root.
3. Canonical resolutions observed here:
   - `CD-09 roles → CD-08`, `CD-10 stats → CD-08`, `CD-20/18/25 → CD-08` (account entity).
   - `CD-19 (MariaDB) ‖ CD-30 (SQLite)`; both `→` schema/entity.
   - `CD-18 schema ‖ CD-20 repo contract`, both `→ CD-08`.
   - `CD-08 ‖ CD-02` — account vs game rules are independent sibling roots.
   - Game-domain roots are **siblings**: `CD-07 ‖ CD-03 ‖ CD-02 ‖ CD-06 ‖ CD-10 ‖ CD-04 ‖ CD-05 ‖ CD-11 ‖ CD-12 ‖ CD-13 ‖ CD-14 ‖ CD-15` — none causes another to exist.
4. **CD-01 (platform) is dual — resolve element-by-element, not by blanket rule:**
   - **root** at the Host bootstrap (`Program.cs`/`Entrypoint`/`Startup`): nothing upstream forces the platform.
   - **subordinated mechanism** (`→ <domain root>`) on any domain service realized *through* the platform (textdraws, timers, pickups, GiveWeapon, messages).
5. **Test tooling** `CD-26/27/28 →` the asserted domain root of the code under test.
6. Never chain siblings (order exists only where one realizes the other).

## 6. Review-fix convergence

| Rule | Value |
|---|---|
| **Convergence** | Review-fix all assigned drivers until **2 consecutive rounds with 0 issues found**. |
| **Per element** | Ad-hoc, specific reasoning; **no transversal/greedy edits**. |
| **Delegation split** | `@explore` = analysis only (no writes); `@general` = annotation writes; one per subsystem (Teams, Accounts, Players-core, Weapons/Combos/GunGame/Maps, Host & Persistence). |
| **Verification** | `dotnet build` clean after annotation; only assigned tree edited; no logic changes. |
| **Audits** | Re-run `Audit5` (dangling →), `Audit6b` (root contradiction), `Audit7` (sibling w/o root mark) — see `../tools/`. |

## 7. Measurement (after annotation)

- Run the **single canonical tool** `IvpMeasure.java` (pinned semantics; legacy scripts are inconsistent → disallowed).
- Output: census (types / namespaces / distinct Γ-sets / scattered sets), driver-activation, global stats, per-namespace sets, prod/test split.
- Re-baseline procedure: capture → diff → update `../before/*` (or `../after/*`) → tag `ivp-<state>`.

## 8. Honest caveats

| Caveat | Status |
|---|---|
| Purity is over driver-**sets**, not driver count (see invariants) | enforced |
| Member-level detection is a heuristic (± few members) | documented |
| Essential-vs-spurious grounded in `constraints.md` Appendix A; unlisted = spurious | enforced |
| Activation-frequency `λ(γ)` — **no artifact exists in repo** → weighted cost `Σλκ` not computed | declared, not fabricated |
| Test-namespace completeness inflated (singleton sets); assembly purity ≈ 0 expected | documented |
