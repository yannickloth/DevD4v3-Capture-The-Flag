# Causal Partition — Namespaces & Assemblies

> Companion to `causal-order.md` (per-element rules) and `before/changedrivers.md` §2 (namespace → driver sets). This table gives each **namespace** and **assembly** its *causal* reading: which drivers the module exists **for** (roots), and which drivers it merely **uses** (subordinated/mechanism).
>
> Derived from the per-element `(root; …)` annotations; a driver is listed as a namespace root when it is the root of the namespace's characteristic elements, and as subordinated when it appears only as `X → root` machinery.
>
> Notation: `→` = exists because of (subordinated); `‖` = sibling roots (unordered).

---

## 1. Namespace causal table (production)

| Namespace | Causal roots (exists for) | Subordinated / mechanism |
|---|---|---|
| `CTF.Application` (messages) | CD-17 (config/messages) | — |
| `CTF.Application.GunGames` | CD-07 (GunGame rules) | CD-03, CD-10 → CD-07; CD-01 → mechanics; CD-21 wiring |
| `CTF.Application.GunGames.Results` | CD-07 (GunGame rules) | CD-03 ‖ CD-01 → CD-07; CD-20 → persistence |
| `CTF.Application.GunGames.WeaponProgressions` | CD-07 | — |
| `CTF.Application.GunGames.WeaponProgressions.Definitions` | CD-07 | — |
| `CTF.Application.Maps` | CD-11 (map configuration) | CD-12 → rotation; CD-01 → mechanics |
| `CTF.Application.Maps.Rotation` | CD-12 (rotation rules) | CD-11 → CD-12; CD-15, CD-01 subordinated |
| `CTF.Application.Players` | CD-02 ‖ CD-08 ‖ CD-16 (mixed shell: game events, account shell, RCON) | CD-11/CD-12 → CD-02; CD-24 → notifications; CD-17 config |
| `CTF.Application.Players.Accounts` | CD-08 (account) ‖ CD-10 (stats model) | CD-20 → CD-08; CD-09 → CD-08; CD-01 → mechanics |
| `CTF.Application.Players.Accounts.Authentication` | CD-08 | CD-20 ‖ CD-25 → CD-08; CD-01 → CD-08 |
| `CTF.Application.Players.Accounts.Profile` | CD-08 (name/password) ‖ CD-01 (skin) | CD-20 → CD-08/CD-01 |
| `CTF.Application.Players.Accounts.Roles` | CD-09 (authorization) | CD-17 ‖ CD-20 ‖ CD-01 → CD-09 |
| `CTF.Application.Players.Accounts.Statistics` | CD-10 (stats) ‖ CD-06 (coins) | CD-07/CD-08/CD-09/CD-20/CD-17 subordinated |
| `CTF.Application.Players.AntiCBug` | CD-14 (anti-cheat) | CD-17 → CD-14; CD-01 → mechanics |
| `CTF.Application.Players.Chats` | CD-13 (chat rules) | CD-09 → CD-13; CD-01 → mechanics |
| `CTF.Application.Players.Chats.Definitions` | CD-13 | CD-09 → CD-13; CD-01 → mechanics |
| `CTF.Application.Players.Combos` | CD-05 (combo definitions) | CD-06 → CD-05; CD-07/CD-12/CD-15/CD-17 subordinated |
| `CTF.Application.Players.Combos.Definitions` | CD-05 | CD-06 ‖ CD-17 → CD-05 |
| `CTF.Application.Players.GeneralCommands` | CD-15 (command set) | CD-02/CD-09 subordinated; CD-01 mechanics |
| `CTF.Application.Players.Headshots` | CD-03 (combat rules: headshot) | CD-10/CD-20/CD-17 → CD-03; CD-01 mechanics |
| `CTF.Application.Players.Pause` | CD-02 (pause rule) | CD-01 → mechanics |
| `CTF.Application.Players.Ranks` | CD-10 (rank model) | CD-15/CD-01 subordinated |
| `CTF.Application.Players.TopPlayers` | CD-10 (leaderboard) | CD-17/CD-20/CD-15 subordinated |
| `CTF.Application.Players.Vitalities` | CD-03 (health/armour rules) | CD-15/CD-17/CD-09 subordinated; CD-01 mechanics |
| `CTF.Application.Players.Weapons` | CD-04 (catalog) ‖ CD-03 (selection rules) | CD-07 → CD-04; CD-15/CD-17 subordinated |
| `CTF.Application.Players.Weapons.Catalogs` | CD-04 | CD-17 → CD-04 |
| `CTF.Application.Teams` | CD-02 (team rules) ‖ CD-01 (team entity) | CD-10 (team stats) sibling; CD-11/CD-15/CD-17 subordinated |
| `CTF.Application.Teams.ClassSelection` | CD-02 (class-selection flow) | CD-08/CD-03/CD-12/CD-15/CD-17 subordinated |
| `CTF.Application.Teams.Flags` | CD-02 (flag rules) | CD-03/CD-06/CD-09/CD-10/CD-11/CD-21 subordinated |
| `CTF.Application.Teams.Flags.AutoReturn` | CD-02 (auto-return rule) | CD-17 → CD-02; CD-01 mechanics |
| `CTF.Application.Teams.Flags.Carriers` | CD-02 (carrier rules) | CD-09/CD-15/CD-17 subordinated |
| `CTF.Application.Teams.Flags.Events` | CD-02 (flag events) | CD-06/CD-10/CD-20/CD-17 → CD-02 |
| `CTF.Application.Teams.Matches` | CD-02 (match rules) | CD-01 → mechanics |
| `CTF.Application.Teams.Statistics` | CD-10 (team stats) ‖ CD-02 | CD-09/CD-15 subordinated |
| `CTF.Host` | CD-01 (bootstrap) ‖ CD-21 (composition) | CD-22/CD-23/CD-24/CD-17 subordinated |
| `CTF.Host.Extensions` | CD-21 (wiring) ‖ CD-17 (config binding) | CD-19/CD-30 dialects; CD-22/CD-23/CD-24 subordinated |
| `CTF.Host.Services` | CD-01 ‖ CD-24 ‖ CD-25 (service realizations) | CD-21 → wiring |
| `Persistence.InMemory` | CD-20 (repo contract) | CD-18 ‖ CD-25 → CD-20; CD-21 wiring; CD-17 seed |
| `Persistence.MariaDB` | CD-20 ‖ CD-19 (MariaDB dialect) | CD-18 → CD-20; CD-25 → CD-20; CD-17/CD-21 subordinated |
| `Persistence.SQLite` | CD-20 ‖ CD-30 (SQLite dialect) | CD-18 → CD-20; CD-25 → CD-20; CD-17/CD-21 subordinated |
| `Persistence.SQLite.Extensions` | CD-30 (SQLite dialect helpers) | — |
| `SampSharp` (entry point) | CD-01 (host ABI) | CD-22 → deployment |

## 2. Namespace causal table (tests)

| Namespace | Causal root | Subordinated |
|---|---|---|
| `CTF.Application.Tests` | CD-29 (code-under-test) | CD-26/CD-27 → CD-29; CD-11/CD-22 asserted |
| `CTF.Application.Tests.Fakes` | CD-29 (mocked seams) | CD-28 → CD-29; CD-01/CD-11 asserted |
| `CTF.Application.Tests.GunGames` | CD-29 (GunGame seams) | CD-07 asserted; CD-26/CD-27 → CD-29 |
| `CTF.Application.Tests.Maps` | CD-29 (map seams) | CD-11/CD-12 asserted; tooling → CD-29 |
| `CTF.Application.Tests.Players.*` | CD-29 (per-area seams) | production drivers asserted (CD-02/03/04/06/08/09/10/17); tooling → CD-29 |
| `CTF.Application.Tests.Teams*` | CD-29 (team seams) | CD-02/CD-10 asserted; tooling → CD-29 |
| `Persistence.Tests.Common` | CD-20 (repo seams) ‖ CD-19 ‖ CD-30 | CD-18/CD-21/CD-25 subordinated; CD-26 tooling |
| `Persistence.Tests.Players` | CD-29 (repo behaviour) | CD-18/CD-20 asserted; tooling → CD-29 |

## 3. Assembly causal table

| Assembly | Causal roots (exists for) | Subordinated / mechanism |
|---|---|---|
| `CTF.Application` | CD-02 (game rules) ‖ CD-08 (account) ‖ CD-10 (stats) ‖ CD-07 (GunGame) ‖ CD-04 (weapons) ‖ CD-05 (combos) ‖ CD-11 (maps) — the domain hexagon | everything else (CD-01 platform, CD-20 ports, CD-13/14/15/16 policies, CD-06) realized inside |
| `CTF.Host` | CD-01 (platform bootstrap) ‖ CD-21 (composition) | CD-17 config binding, CD-22 deployment, CD-23 logging, CD-24 webhook, CD-25 hashing, CD-19/CD-30 dialect dispatch |
| `Persistence.InMemory` | CD-20 (repo contract) | CD-18 ‖ CD-25 → CD-20; CD-21 wiring |
| `Persistence.MariaDB` | CD-20 ‖ CD-19 (dialect) | CD-18/CD-25 → CD-20; CD-17/CD-21 subordinated |
| `Persistence.SQLite` | CD-20 ‖ CD-30 (dialect) | CD-18/CD-25 → CD-20; CD-17/CD-21 subordinated |
| `CTF.Application.Tests` | CD-29 (code-under-test seams) | CD-26/CD-27/CD-28 tooling; asserted production drivers |
| `Persistence.Tests` | CD-29 ‖ CD-20 (repo behaviour) | tooling + dialects subordinated |

## 4. Reading

- The **application hexagon** (`CTF.Application`) roots the *domain drivers*; platform (CD-01), persistence ports (CD-20), and policies (CD-13/14/15/16) are realized within it.
- The **host** roots the *platform/composition* axis — the one assembly where CD-01 is genuinely a root.
- The **persistence providers** root the *storage* axis: the repo contract plus their dialect (CD-19 vs CD-30 as sibling variants).
- **Test assemblies** root CD-29: they exist to assert the production seams; the tooling (CD-26/27/28) and the asserted production drivers are subordinated to that purpose.
- Nesting translation for the later packaging step: each namespace whose roots are a *subset* of another's root set nests inside it (`Γ_exist(parent) ⊆ Γ_exist(child)`); namespaces with disjoint roots stay siblings. E.g. `Accounts.Persistence` would nest under `Accounts` (its CD-20 root exists because the account root exists); `Maps` and `Players` stay siblings.
