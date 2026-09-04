# IVP "After" Metrics

Measured with the canonical tool (`java IVP/tools/IvpMeasure.java .`) at the
documentation commit. Raw output basis: `types=309, namespaces=64`.

> **Comparability caveat.** The annotation layer was *refined*, not just moved:
> CD-01 (platform umbrella) was decomposed into CD-31..CD-44. The tool's Γ-set
> semantics are unchanged, but the set space is finer after than before, so
> before→after deltas of *set counts* reflect the refinement as well as the
> reorganisation. Structural signals (scattered sets, composite namespaces,
> activation of the decomposed driver) are comparable; raw set counts are not.
> (Tool change: display labels for CD-31..CD-44 added — no measurement change.)

## Census

| Metric | Before | After | Reading |
|---|---|---|---|
| types | 293 | 309 | PlayerInfo split into sub-entities, FlagCarrier/FlagIdentity-grouping churn, new resource types |
| namespaces | 57 | 64 | per-driver modules added |
| distinct Γ-sets | 137 | 180 | ↑ expected — finer driver space splits former umbrella sets |
| scattered sets (span >1 namespace) | 28 | **15** | ↓ — the core IVP defect shrinking |
| composite namespaces | 43/57 | **35/64** | ↓ relative share (55% → 55%... see note) |
| CD-01 activation | 100+ | **0** | umbrella fully decomposed |

Note on composite namespaces: 35 of 64 namespaces hold >1 Γ-set. Under the
refined drivers every domain system legitimately carries platform *subordinates*
(CD-31/32/36 inside a CD-02-rooted system), so multi-set namespaces are expected;
the defect signal is *scattered sets*, which halved.

## Driver activation (top)

| Driver | Elements | Namespaces | Notes |
|---|---|---|---|
| CD-31 player entity & events | 59 | 19 | former CD-01 bulk |
| CD-02 CTF game rules | 53 | 10 | |
| CD-32 ECS runtime | 52 | 20 | |
| CD-17 .env schema | 49 | 24 | widest scatter — settings read across all domains |
| CD-10 stats/rank | 49 | 16 | |
| CD-36 client messages | 40 | 17 | |
| CD-20 repository contract | 39 | 15 | |
| CD-39 attached objects | **1** | **1** | isolated in `Flag.CarrierAttachment` — perfect containment |
| CD-16 RCON | 1 | 1 | own module |
| CD-23 Serilog | 3 | 3 | own module |

Single-driver, single-namespace drivers (purity 1.000 / concentration 1): Audio
(CD-40), MapIcons (CD-38), Pickups (CD-37), RconSecurity (CD-16), TextDraws
member `TeamTextDrawRenderer` set, Discord (CD-24), Ecs/Entrypoint (CD-32),
ServerService (CD-42), WeaponCatalogs (CD-04), Bcrypt (CD-25).

## Namespace-sets highlights

| Namespace | types | sets | composite? |
|---|---|---|---|
| CTF.Application.GameRules | 38 | 29 | the flag/team/state-machine domain; subordinates expected |
| CTF.Application.Statistics | 21 | 16 | |
| CTF.Application.Audio / Pickups / MapIcons / RconSecurity / Discord | 1–2 | 1–3 | near-pure per-driver modules |
| CTF.Host.Ecs / ServerService / CommandInfrastructure / Bcrypt / Logging | 1 | 1 | pure |

## Procedure

`java IVP/tools/IvpMeasure.java .` from the repo root. Baseline: pinned
`ivp-before` tag per `IVP/before/metrics.md` (293 types / 137 sets / 28
scattered / 43 of 57 composite).
