# Change-Driver Causal Order & Subordination

> Reference for turning the *flat* change-driver sets into a **causal order** (which driver's *existence* depends on which). This is the `Γ_exist` nesting relation from the IVP book series (`cohesion-4-causal`, §Nesting Semantics):
>
> The **existence-governing** drivers of a module `M`, written `Γ_exist(M)`, are the change drivers whose activation can require `M` itself to be created, removed, or restructured. Because a child module exists only if its parent exists, `Γ_exist(parent) ⊆ Γ_exist(child)`.
>
> Three relations are distinguished — never conflated:
> - **`→`** — *subordinated to* / "exists because of": `X → Y` reads "driver X's presence on this element exists only because driver Y's domain exists". Causal precedence.
> - **`,` / `‖`** — *siblings*: two drivers co-present at the same causal depth with **no** ordering between them (neither causes the other).
> - **root** — a domain driver with no upstream `→` predecessor: it is the causal source; everything subordinated to it exists because of it.
>
> **This is NOT driver ranking.** IVP forbids "primary/secondary/main". Subordination is *causal containment* ("the repo's persistence exists because the customer exists"), not importance. Siblings are explicitly allowed and flagged as unordered.

---

## 1. The causal roots (domain drivers)

These are the drivers that exist independently — nothing upstream forces them. Each is the causal head of a subordination chain.

| Root driver | Domain | Exists because of (external authority) |
|---|---|---|
| CD-08 | account / authentication | account policy (nickname identity, login) |
| CD-09 | authorization / roles | role ladder (Basic/VIP/Moderator/Admin) |
| CD-10 | player statistics / rank | stats + rank model |
| CD-02 | CTF game rules | the game rulebook (flags, match, round) |
| CD-07 | GunGame mode | the GunGame mode spec |
| CD-04 | weapon catalog | weapon catalog spec |
| CD-05 | combo definitions | combo spec |
| CD-06 | coin economy | coin economy spec |
| CD-11 | map configuration | the map `.ini` schema |
| CD-12 | map rotation | rotation rules |
| CD-13 | chat rules | chat rulebook |
| CD-14 | anti-cheat policy | anti-cheat (C-Bug) rule |
| CD-15 | command set | the command catalogue |
| CD-16 | RCON security | RCON policy |
| CD-01 | platform (Host bootstrap) | open.mp/SampSharp ABI + ECS host |
| CD-22 | hosting/deployment | deployment layout |

> Note: CD-01 appears in *two* roles. At the **Host boundary** (`Program.cs`/`Entrypoint`) and platform-interface injection points it is a *root* (the platform contract exists independently). Elsewhere (textdraws, timers, pickups realizing a domain behaviour) it is *subordinated* — a *means* the domain uses, not a root. This duality must be resolved element-by-element, not by a blanket rule.

## 2. Subordinated drivers (exist only because of a root)

| Driver | Subordinated to | Why (existence reason) |
|---|---|---|
| CD-20 (outbound repository contract) | the entity it persists (CD-08 for `PlayerInfo`) | a repository exists to store a domain entity; no entity → no repo |
| CD-18 (database schema / player data model) | the entity it persists (CD-08) | the schema mirrors the entity's fields |
| CD-19 (MariaDB SQL dialect) | the schema/entity (CD-18/CD-08) | a dialect exists to realize a schema on a DBMS |
| CD-30 (SQLite SQL dialect) | the schema/entity (CD-18/CD-08) | sibling to CD-19 (no order between MariaDB and SQLite) |
| CD-25 (BCrypt hashing) | the credential it hashes (CD-08) | hashing exists to protect the password field |
| CD-21 (DI container/composition) | the objects it wires (their domains) | DI exists to compose; no services → no wiring |
| CD-24 (Discord webhook) | the notification intent (CD-08 / CD-02) | webhook exists to announce account/game events |
| CD-26/27/28 (NUnit/FA/NSubstitute) | the code under test (CD-29) | a test framework exists to exercise production code |
| CD-29 (depended-on contract) | the seam's domain | a contract exists to abstract a domain service |

## 3. Sibling relations (unordered — do not chain)

- **CD-19 (MariaDB) ‖ CD-30 (SQLite)** — two DBMS dialects, independent, neither causes the other.
- **CD-18 (schema) ‖ CD-20 (repo contract), both → CD-08 (account)** — the schema and the repo interface are co-subordinated to the same entity.
- **CD-08 (account) ‖ CD-02 (game rules)** — the account system and the game rulebook are independent roots (accounts exist without the game; the game exists without accounts).
- **CD-08 ‖ CD-09 ‖ CD-10** — account identity, role authority, and statistics are *distinct* authorities; whether roles/stats are subordinated to account or siblings is the flagged ambiguity (§5).
- **The game-domain roots are siblings: CD-07 (GunGame) ‖ CD-03 (combat/weapon) ‖ CD-02 (game rules) ‖ CD-06 (coin economy) ‖ CD-10 (stats) ‖ CD-04 (weapon catalog) ‖ CD-05 (combo) ‖ CD-11 (map) ‖ CD-12 (rotation) ‖ CD-13 (chat) ‖ CD-14 (anti-cheat) ‖ CD-15 (command set).** None *causes* another to exist: GunGame reuses the weapon rules but does not create them; a flag score grants coins but does not create the coin economy. Each is an independent domain authority. When two or more co-occur on one element, they are siblings `‖`, never a chain — unless one is the *mechanism* through which the other is realized (see the platform case below).

## 3b. The two cross-cutting "mechanism" drivers (context-dependent)

- **CD-01 (platform)** — root at the Host bootstrap only; on a domain element it is the *mechanism* through which the domain is realized, so it is subordinated: `CD-01 → <domain root>` (or `CD-01 → {A ‖ B}` when several sibling roots co-occur). Example: on a `FlagSystem`/`PlayerLeveledDown` the platform calls (textdraws, GiveWeapon, messages) realize the game rules → `CD-01 → CD-02` / `CD-01 → {CD-07 ‖ CD-03}`.
- **CD-17 (config)** — root (the `.env` schema exists independently); it is not subordinated to any domain, it *parameterizes* domains. On a settings class it stands alone as the root.
- **CD-21 (DI)** — subordinated to composition; renders as `CD-21 → <composed domains>`. On a pure wiring element (a `ServiceCollectionExtensions`) it is the root (the wiring *is* the element's purpose).

## 4. Canonical chains (root → subordinated, siblings pooled)

The chains the packaging refactor will follow (each `→` = nesting one level deeper; `‖` = siblings that live side-by-side at the same depth):

```
CD-02 → CD-20 → CD-18 → { CD-19 ‖ CD-30 }      # flag/match persistence
CD-08 → CD-20 → CD-18 → { CD-19 ‖ CD-30 }      # account persistence  (Customer → CustomerRepo)
CD-08 → CD-25                                   # password hashing
CD-08 → CD-10 → CD-20                           # stats, then persists to repo
CD-08 → CD-09                                   # roles governing accounts
CD-07 → CD-10 → CD-20                           # GunGame wins tracked in stats, persisted
CD-11 → CD-01                                   # map config realized via platform rendering
CD-04 → CD-01                                   # catalog realized via platform
CD-01 (root, Host bootstrap only) → CD-21       # host bootstrap composes via DI
{ CD-08 ‖ CD-02 } → CD-24                       # game/account events announced via webhook (sibling triggers)
```

The nested-package translation (later step): each `A → B` becomes `A.B` (e.g. `Accounts` → `Accounts.Persistence`), and `A ‖ B` become *sibling* subpackages with no nesting between them.

## 5. Resolved via the counterfactual test

The deciding question is: **would this element/feature exist without the superior mechanism or causal root?** If no → the driver is *subordinated*; if yes (it has an independent reason to exist) → it is a *sibling root*.

1. **CD-09 (roles) → CD-08 (account).** `PlayerInfo.RoleId` exists to authorize *the account* (gate commands, gate lists). No account → no per-account role assignment. The role *ladder* is an external authority, but the element carrying the role is subordinated to the account it describes. → subordinated.
2. **CD-10 (stats) → CD-08 (account).** `PlayerInfo`'s stats/kills/rank exist to record *the account's* history. No account → no per-account stats. → subordinated.
3. **CD-01 (platform): root at the Host boundary, subordinated on domain services.** The counterfactual splits by where the element sits:
   - `Program.cs`/`Entrypoint`/`Startup`: nothing upstream forces the platform bootstrap → the platform IS the requirement → **root**.
   - A domain service using `IWorldService`/`ITimerService`/textdraws/pickups: would the feature exist without the platform? No — the feature is *realized through* the platform → **subordinated (mechanism)**.
   - Platform-interface *injection points* likewise: subordinated to the domain they serve.

These resolutions close §5; the chains in §4 are updated accordingly (CD-01 listed as root only at the Host boundary).

## 6. Notation for source remarks (to be applied)

The existing flat remarks (`CD-X (label), CD-Y (label)`) will be augmented, *not replaced*, with the causal reading. Target form:

```
<remarks>Change drivers:
  CD-08 (root);
  CD-20 → CD-08 (subordinated: persistence exists because the account exists);
  CD-18 → CD-08 (subordinated: schema mirrors the account);
  CD-19 ‖ CD-30 (siblings: DBMS dialects);
  CD-25 → CD-08 (subordinated: hashing protects the password).
</remarks>
```

- **`(root)`** marks a causal head.
- **`X → Y (reason)`** marks subordination with the existence reason.
- **`X ‖ Y`** marks unordered siblings.
- Elements with a single driver need no `→` (their only driver is their root by default).
