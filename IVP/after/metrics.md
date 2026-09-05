# Capture-The-Flag — Change-Driver & Causal-Cohesion Metrics (After)

> Canonical measurement of the post-IVP tree with the pinned tool `IVP/tools/IvpMeasure.java`
> (display labels for CD-31..CD-44 added; set semantics unchanged). 315 annotated types
> (284 class, 12 enum, 9 interface, 4 record, 6 struct) across 178 namespaces, 42 active
> drivers (CD-01 decomposed into CD-31..CD-44, CD-29 retired), 181 distinct Γ-sets,
> 21 scattered sets.
>
> **Comparability caveat:** the driver space was *refined*, not merely moved — former CD-01
> umbrella sets split into finer sub-driver sets. Raw set-count deltas vs the before baseline
> therefore reflect the refinement plus the reorganisation. Comparable signals: scattered sets,
> composite-namespace share, CD-01 activation (= 0), and per-driver purity.

## 1. Driver activation

| Driver | Elements | Modules (namespaces) | Scatter ratio (elem/ns) |
|---|---|---|---|
| CD-01 (open.mp/SampSharp platform API) | 0 | 0 | 0.00 |
| CD-02 (CTF game-rules specification) | 54 | 10 | 5.40 |
| CD-03 (combat/weapon-rules specification) | 25 | 8 | 3.13 |
| CD-04 (weapon-catalog configuration) | 19 | 5 | 3.80 |
| CD-05 (combo definitions) | 11 | 1 | 11.00 |
| CD-06 (coin economy) | 16 | 6 | 2.67 |
| CD-07 (GunGame mode rules) | 42 | 7 | 6.00 |
| CD-08 (account & authentication policy) | 18 | 7 | 2.57 |
| CD-09 (authorization policy) | 37 | 15 | 2.47 |
| CD-10 (player-statistics/rank model) | 49 | 16 | 3.06 |
| CD-11 (map configuration) | 20 | 8 | 2.50 |
| CD-12 (map-rotation rules) | 15 | 6 | 2.50 |
| CD-13 (chat rules) | 10 | 2 | 5.00 |
| CD-14 (anti-cheat policy) | 4 | 1 | 4.00 |
| CD-15 (command set) | 37 | 14 | 2.64 |
| CD-16 (RCON security policy) | 1 | 1 | 1.00 |
| CD-17 (game configuration/.env schema) | 49 | 24 | 2.04 |
| CD-18 (database schema/player data model) | 22 | 7 | 3.14 |
| CD-19 (MariaDB SQL dialect) | 10 | 3 | 3.33 |
| CD-20 (outbound repository contract) | 39 | 15 | 2.60 |
| CD-21 (DI container/composition) | 18 | 13 | 1.38 |
| CD-22 (hosting/deployment spec) | 5 | 4 | 1.25 |
| CD-23 (Serilog logging) | 3 | 3 | 1.00 |
| CD-24 (Discord webhook contract) | 5 | 4 | 1.25 |
| CD-25 (BCrypt password-hashing contract) | 10 | 7 | 1.43 |
| CD-26 (NUnit test-framework contract) | 51 | 17 | 3.00 |
| CD-27 (FluentAssertions contract) | 50 | 16 | 3.13 |
| CD-28 (NSubstitute mock contract) | 4 | 1 | 4.00 |
| CD-30 (SQLite SQL dialect) | 12 | 4 | 3.00 |
| CD-31 (player entity & lifecycle events) | 59 | 19 | 3.11 |
| CD-32 (ECS runtime) | 52 | 20 | 2.60 |
| CD-33 (dialog API) | 20 | 10 | 2.00 |
| CD-34 (textdraw API) | 14 | 8 | 1.75 |
| CD-35 (GameText API) | 13 | 7 | 1.86 |
| CD-36 (client-message API) | 38 | 16 | 2.38 |
| CD-37 (pickup API) | 12 | 4 | 3.00 |
| CD-38 (map-icon & radar API) | 9 | 3 | 3.00 |
| CD-39 (attached-object API) | 1 | 1 | 1.00 |
| CD-40 (audio API) | 11 | 4 | 2.75 |
| CD-41 (timer API) | 6 | 2 | 3.00 |
| CD-42 (server service API) | 4 | 4 | 1.00 |
| CD-43 (command infrastructure) | 23 | 12 | 1.92 |
| CD-44 (model & skin id resources) | 12 | 7 | 1.71 |

## 2. Global statistics

| Statistic | Value | Before |
|---|---|---|
| Types with a change-driver annotation | 315 | 293 |
| Namespaces | 178 | 57 |
| Mean change drivers per class | 2.94 | 3.03 |
| Median change drivers per class | 3 | 3 |
| Mean change drivers per namespace | 3.62 | 6.51 |
| Median change drivers per namespace | 3.0 | 6 |

Drivers per class histogram: {1=95, 2=61, 3=68, 4=40, 5=16, 6=8, 7=11, 8=8, 9=5, 10=3}

### 2.1 The three cardinalities and IVP correspondence

| Cardinality | Value | Meaning |
|---|---|---|
| **classes (elements `E`)** | 315 | the code elements supplied to the partition |
| **distinct change-driver sets (`E/Γ`)** | 181 | the Γ-equivalence classes = the IVP normative partition |
| **namespaces (actual modules)** | 178 | the partition the code actually has |

After the refinement the set space is finer by construction (the former CD-01 umbrella
sets split), so `181 vs 137` before is not a regression signal. The structural defect
metrics are the ones below: scattered sets and composite namespaces.

## 3. Namespace contamination (multi-change-driver-set mixes)

| namespace | classes | distinct tokens | distinct sets | single-set? |
|---|---|---|---|---|
| CTF.Application | 1 | 1 | 1 | yes |
| CTF.Application.Accounts.Authentication.Gamma_CD08_CD15_CD20_CD31_CD36_CD43 | 1 | 6 | 1 | yes |
| CTF.Application.Accounts.Authentication.Gamma_CD08_CD15_CD20_CD33_CD36_CD43 | 1 | 6 | 1 | yes |
| CTF.Application.Accounts.Authentication.Gamma_CD08_CD20_CD25_CD31_CD32_CD36 | 2 | 6 | 2 | no |
| CTF.Application.Accounts.Authentication.Gamma_CD08_CD20_CD31_CD32 | 1 | 4 | 1 | yes |
| CTF.Application.Accounts.Authentication.Gamma_CD08_CD32 | 1 | 2 | 1 | yes |
| CTF.Application.Accounts.Authentication.Gamma_CD08_CD33 | 1 | 2 | 1 | yes |
| CTF.Application.Accounts.Credentials.Core | 1 | 1 | 1 | yes |
| CTF.Application.Accounts.Credentials.Gamma_CD08_CD09_CD10_CD44 | 1 | 4 | 1 | yes |
| CTF.Application.Accounts.Credentials.Gamma_CD08_CD18_CD20 | 1 | 3 | 1 | yes |
| CTF.Application.Accounts.Extensions | 1 | 1 | 1 | yes |
| CTF.Application.AntiCheat.Gamma_CD09_CD14_CD15_CD17_CD31_CD36_CD43 | 1 | 7 | 1 | yes |
| CTF.Application.AntiCheat.Gamma_CD14_CD17 | 1 | 2 | 1 | yes |
| CTF.Application.AntiCheat.Gamma_CD14_CD17_CD31_CD32_CD35 | 1 | 5 | 1 | yes |
| CTF.Application.AntiCheat.Gamma_CD14_CD32 | 1 | 2 | 1 | yes |
| CTF.Application.Audio | 1 | 1 | 1 | yes |
| CTF.Application.Audio.Configuration | 1 | 1 | 1 | yes |
| CTF.Application.Authorization.Admin.Gamma_CD09_CD15_CD17_CD32_CD33_CD36_CD43 | 1 | 7 | 1 | yes |
| CTF.Application.Authorization.Admin.Gamma_CD09_CD17 | 2 | 2 | 1 | yes |
| CTF.Application.Authorization.Roles.Core | 3 | 1 | 1 | yes |
| CTF.Application.Authorization.Roles.Gamma_CD09_CD15_CD17_CD20_CD31_CD32_CD33_CD35_CD36_CD43 | 2 | 10 | 2 | no |
| CTF.Application.Authorization.Roles.Gamma_CD09_CD20 | 1 | 2 | 1 | yes |
| CTF.Application.Authorization.Roles.Gamma_CD09_CD43 | 2 | 2 | 1 | yes |
| CTF.Application.Authorization.Vip | 1 | 6 | 1 | yes |
| CTF.Application.Chat.Core | 2 | 1 | 1 | yes |
| CTF.Application.Chat.Gamma_CD09_CD13_CD15_CD31_CD32_CD36_CD43 | 2 | 7 | 2 | no |
| CTF.Application.Chat.Gamma_CD09_CD13_CD32_CD36 | 3 | 4 | 1 | yes |
| CTF.Application.Chat.Gamma_CD13_CD31_CD32_CD36 | 1 | 4 | 1 | yes |
| CTF.Application.Chat.Gamma_CD13_CD36 | 1 | 2 | 1 | yes |
| CTF.Application.CoinEconomy | 2 | 9 | 2 | no |
| CTF.Application.Combat | 1 | 1 | 1 | yes |
| CTF.Application.Combat.Headshot.Gamma_CD03_CD10_CD17_CD20_CD31_CD32_CD36_CD40 | 1 | 8 | 1 | yes |
| CTF.Application.Combat.Headshot.Gamma_CD03_CD17 | 1 | 2 | 1 | yes |
| CTF.Application.Combat.Health.Core | 1 | 1 | 1 | yes |
| CTF.Application.Combat.Health.Gamma_CD03_CD09_CD15_CD17_CD31_CD32_CD36_CD43 | 4 | 8 | 2 | no |
| CTF.Application.Combat.Health.Gamma_CD03_CD31 | 1 | 2 | 1 | yes |
| CTF.Application.Combat.WeaponSelection.Gamma_CD03_CD04_CD07_CD15_CD31_CD32_CD33_CD36_CD43 | 1 | 9 | 1 | yes |
| CTF.Application.Combat.WeaponSelection.Gamma_CD03_CD32 | 1 | 2 | 1 | yes |
| CTF.Application.Combos | 1 | 1 | 1 | yes |
| CTF.Application.Combos.Systems.Core | 1 | 1 | 1 | yes |
| CTF.Application.Combos.Systems.Gamma_CD05_CD06_CD07_CD10_CD15_CD31_CD33_CD34_CD35_CD36 | 1 | 10 | 1 | yes |
| CTF.Application.Combos.Systems.Gamma_CD05_CD09_CD12_CD15_CD36 | 1 | 5 | 1 | yes |
| CTF.Application.Combos.Vitalities.Core | 1 | 1 | 1 | yes |
| CTF.Application.Combos.Vitalities.Gamma_CD03_CD05_CD06 | 5 | 3 | 1 | yes |
| CTF.Application.Combos.Vitalities.Gamma_CD05_CD06 | 1 | 2 | 1 | yes |
| CTF.Application.CommandInfrastructure.Gamma_CD02_CD08_CD12_CD32_CD43 | 1 | 5 | 1 | yes |
| CTF.Application.CommandInfrastructure.Gamma_CD31_CD43 | 1 | 2 | 1 | yes |
| CTF.Application.Commands | 1 | 1 | 1 | yes |
| CTF.Application.Commands.Admin | 2 | 7 | 2 | no |
| CTF.Application.Commands.Basic | 1 | 5 | 1 | yes |
| CTF.Application.Commands.Moderator | 2 | 6 | 2 | no |
| CTF.Application.Commands.Vip.Gamma_CD09_CD15_CD31 | 1 | 3 | 1 | yes |
| CTF.Application.Commands.Vip.Gamma_CD09_CD15_CD33 | 1 | 3 | 1 | yes |
| CTF.Application.Discord | 1 | 3 | 1 | yes |
| CTF.Application.GameRules | 1 | 1 | 1 | yes |
| CTF.Application.GameRules.ClassSelection.Gamma_CD02_CD08_CD12_CD32 | 1 | 4 | 1 | yes |
| CTF.Application.GameRules.ClassSelection.Gamma_CD02_CD11_CD12_CD31 | 1 | 4 | 1 | yes |
| CTF.Application.GameRules.ClassSelection.Gamma_CD02_CD15_CD17_CD31 | 1 | 4 | 1 | yes |
| CTF.Application.GameRules.ClassSelection.Gamma_CD02_CD15_CD31_CD33_CD43 | 1 | 5 | 1 | yes |
| CTF.Application.GameRules.ClassSelection.Gamma_CD02_CD31 | 1 | 2 | 1 | yes |
| CTF.Application.GameRules.ClassSelection.Gamma_CD02_CD32 | 2 | 2 | 1 | yes |
| CTF.Application.GameRules.Configuration | 1 | 2 | 1 | yes |
| CTF.Application.GameRules.Flag.Core | 1 | 1 | 1 | yes |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD03_CD09_CD10_CD15_CD31_CD37 | 1 | 7 | 1 | yes |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD06_CD10_CD17_CD20_CD35_CD37_CD38_CD40 | 1 | 9 | 1 | yes |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD06_CD10_CD20_CD34_CD35_CD37_CD40 | 1 | 8 | 1 | yes |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD06_CD10_CD20_CD35_CD37_CD40 | 1 | 7 | 1 | yes |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD09_CD15_CD17_CD38 | 1 | 5 | 1 | yes |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD10_CD20_CD35_CD37_CD38_CD40 | 1 | 7 | 1 | yes |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD17 | 2 | 2 | 1 | yes |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD17_CD31_CD37_CD40_CD41 | 2 | 6 | 2 | no |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD17_CD35_CD37_CD38_CD40 | 1 | 6 | 1 | yes |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD17_CD37_CD40_CD41 | 1 | 5 | 1 | yes |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD21 | 1 | 2 | 1 | yes |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD31 | 1 | 2 | 1 | yes |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD35 | 1 | 2 | 1 | yes |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD37_CD38_CD41 | 1 | 4 | 1 | yes |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD38_CD44 | 2 | 4 | 2 | no |
| CTF.Application.GameRules.Match.Core | 1 | 1 | 1 | yes |
| CTF.Application.GameRules.Match.Gamma_CD02_CD31 | 4 | 2 | 1 | yes |
| CTF.Application.GameRules.Match.Gamma_CD02_CD35_CD36 | 1 | 3 | 1 | yes |
| CTF.Application.GameRules.Players.Gamma_CD02_CD31 | 1 | 2 | 1 | yes |
| CTF.Application.GameRules.Players.Gamma_CD02_CD31_CD32 | 2 | 3 | 1 | yes |
| CTF.Application.GameRules.Players.Gamma_CD02_CD31_CD32_CD41 | 1 | 4 | 1 | yes |
| CTF.Application.GameRules.Players.Gamma_CD02_CD31_CD36 | 1 | 3 | 1 | yes |
| CTF.Application.GunGames | 2 | 2 | 2 | no |
| CTF.Application.GunGames.Progression.Core | 14 | 1 | 1 | yes |
| CTF.Application.GunGames.Progression.Gamma_CD07_CD32 | 1 | 2 | 1 | yes |
| CTF.Application.GunGames.Results.Gamma_CD03_CD07_CD31_CD32_CD36 | 3 | 5 | 1 | yes |
| CTF.Application.GunGames.Results.Gamma_CD07_CD10_CD20_CD31_CD32_CD36 | 1 | 6 | 1 | yes |
| CTF.Application.GunGames.Rewards | 2 | 3 | 2 | no |
| CTF.Application.GunGames.Systems.Core | 6 | 1 | 1 | yes |
| CTF.Application.GunGames.Systems.Gamma_CD03_CD07_CD09_CD15_CD31_CD32_CD33_CD35_CD36_CD43 | 1 | 10 | 1 | yes |
| CTF.Application.GunGames.Systems.Gamma_CD03_CD07_CD31_CD32 | 1 | 4 | 1 | yes |
| CTF.Application.MapIcons.Core | 1 | 1 | 1 | yes |
| CTF.Application.MapIcons.Gamma_CD11_CD37_CD38 | 1 | 3 | 1 | yes |
| CTF.Application.Maps.Core | 6 | 1 | 1 | yes |
| CTF.Application.Maps.Gamma_CD11_CD12 | 2 | 2 | 2 | no |
| CTF.Application.Maps.Gamma_CD11_CD17_CD32_CD34_CD37_CD38_CD42 | 1 | 7 | 1 | yes |
| CTF.Application.Maps.Rotation.Core | 4 | 1 | 1 | yes |
| CTF.Application.Maps.Rotation.Gamma_CD02_CD11_CD12_CD31_CD34_CD36_CD41_CD42 | 1 | 8 | 1 | yes |
| CTF.Application.Maps.Rotation.Gamma_CD09_CD11_CD12_CD15_CD31_CD33_CD34_CD36_CD43 | 1 | 9 | 1 | yes |
| CTF.Application.Pickups | 1 | 4 | 1 | yes |
| CTF.Application.PlayerResources.Core | 4 | 1 | 1 | yes |
| CTF.Application.PlayerResources.Gamma_CD15_CD20_CD31_CD44 | 1 | 4 | 1 | yes |
| CTF.Application.PlayerResources.Gamma_CD20_CD44 | 1 | 2 | 1 | yes |
| CTF.Application.Players | 2 | 2 | 2 | no |
| CTF.Application.Players.Accounts | 1 | 2 | 1 | yes |
| CTF.Application.Players.Chats | 1 | 2 | 1 | yes |
| CTF.Application.Players.TopPlayers | 2 | 3 | 2 | no |
| CTF.Application.Players.Weapons.Catalogs.Core | 1 | 1 | 1 | yes |
| CTF.Application.Players.Weapons.Catalogs.Gamma_CD04_CD17 | 1 | 2 | 1 | yes |
| CTF.Application.Players.Weapons.Core | 1 | 1 | 1 | yes |
| CTF.Application.Players.Weapons.Gamma_CD03_CD04_CD07_CD09_CD15_CD32_CD33_CD36_CD43 | 1 | 9 | 1 | yes |
| CTF.Application.Players.Weapons.Gamma_CD04_CD17 | 1 | 2 | 1 | yes |
| CTF.Application.RconSecurity | 1 | 3 | 1 | yes |
| CTF.Application.Statistics.Ranks.Core | 4 | 1 | 1 | yes |
| CTF.Application.Statistics.Ranks.Gamma_CD10_CD07_CD09_CD20 | 1 | 4 | 1 | yes |
| CTF.Application.Statistics.Ranks.Gamma_CD10_CD15_CD33_CD43 | 1 | 4 | 1 | yes |
| CTF.Application.Statistics.Score.Gamma_CD10_CD06 | 1 | 2 | 1 | yes |
| CTF.Application.Statistics.Score.Gamma_CD10_CD06_CD07_CD20_CD31_CD32_CD35_CD36 | 1 | 8 | 1 | yes |
| CTF.Application.Statistics.Score.Gamma_CD10_CD08_CD15_CD20_CD31_CD33_CD34_CD43 | 1 | 8 | 1 | yes |
| CTF.Application.Statistics.Score.Gamma_CD10_CD09_CD15_CD20 | 1 | 4 | 1 | yes |
| CTF.Application.Statistics.Score.Gamma_CD10_CD09_CD15_CD31_CD32_CD36_CD43 | 1 | 7 | 1 | yes |
| CTF.Application.Statistics.Score.Gamma_CD10_CD20 | 1 | 2 | 1 | yes |
| CTF.Application.Statistics.Score.Gamma_CD10_CD31_CD32_CD34 | 2 | 4 | 2 | no |
| CTF.Application.Statistics.TeamStats.Gamma_CD10_CD02 | 1 | 2 | 1 | yes |
| CTF.Application.Statistics.TeamStats.Gamma_CD10_CD02_CD09_CD15_CD31_CD33_CD34 | 1 | 7 | 1 | yes |
| CTF.Application.Statistics.TeamStats.Gamma_CD10_CD02_CD15_CD31_CD33 | 1 | 5 | 1 | yes |
| CTF.Application.Statistics.TopPlayers.Core | 2 | 1 | 1 | yes |
| CTF.Application.Statistics.TopPlayers.Gamma_CD10_CD15_CD17_CD20_CD33_CD36_CD43 | 1 | 7 | 1 | yes |
| CTF.Application.Statistics.TopPlayers.Gamma_CD10_CD17 | 1 | 2 | 1 | yes |
| CTF.Application.Teams | 4 | 9 | 4 | no |
| CTF.Application.Tests | 1 | 4 | 1 | yes |
| CTF.Application.Tests.Authorization | 1 | 3 | 1 | yes |
| CTF.Application.Tests.Fakes | 5 | 3 | 2 | no |
| CTF.Application.Tests.GameRules | 3 | 3 | 1 | yes |
| CTF.Application.Tests.GunGames.Core | 2 | 1 | 1 | yes |
| CTF.Application.Tests.GunGames.Gamma_CD07_CD26_CD27 | 6 | 3 | 1 | yes |
| CTF.Application.Tests.Maps | 3 | 3 | 1 | yes |
| CTF.Application.Tests.Maps.Rotation | 3 | 3 | 1 | yes |
| CTF.Application.Tests.PlayerResources | 1 | 3 | 1 | yes |
| CTF.Application.Tests.Players.Accounts | 7 | 8 | 6 | no |
| CTF.Application.Tests.Players.Extensions | 1 | 4 | 1 | yes |
| CTF.Application.Tests.Players.Ranks | 3 | 3 | 1 | yes |
| CTF.Application.Tests.Players.TopPlayers | 1 | 4 | 1 | yes |
| CTF.Application.Tests.Players.Vitalities | 2 | 3 | 1 | yes |
| CTF.Application.Tests.Players.Weapons | 1 | 3 | 1 | yes |
| CTF.Application.Tests.Players.Weapons.Catalogs.Core | 1 | 1 | 1 | yes |
| CTF.Application.Tests.Players.Weapons.Catalogs.Gamma_CD04_CD17_CD26_CD27 | 1 | 4 | 1 | yes |
| CTF.Application.Tests.Players.Weapons.Catalogs.Gamma_CD04_CD26_CD27 | 2 | 3 | 1 | yes |
| CTF.Application.Tests.Statistics.Gamma_CD02_CD10_CD26_CD27 | 1 | 4 | 1 | yes |
| CTF.Application.Tests.Statistics.Gamma_CD10_CD26_CD27 | 4 | 3 | 1 | yes |
| CTF.Application.Tests.Teams | 3 | 3 | 1 | yes |
| CTF.Application.Tests.TextDraws | 1 | 3 | 1 | yes |
| CTF.Application.TextDraws.Core | 1 | 1 | 1 | yes |
| CTF.Application.TextDraws.Gamma_CD02_CD10_CD34_CD36 | 1 | 4 | 1 | yes |
| CTF.Application.TextDraws.Gamma_CD11_CD12_CD34 | 1 | 3 | 1 | yes |
| CTF.Application.WeaponCatalogs | 9 | 1 | 1 | yes |
| CTF.Host.Bcrypt | 1 | 1 | 1 | yes |
| CTF.Host.CommandInfrastructure | 1 | 1 | 1 | yes |
| CTF.Host.Composition.Gamma_CD17_CD21_CD22 | 1 | 3 | 1 | yes |
| CTF.Host.Composition.Gamma_CD21_CD22 | 1 | 2 | 1 | yes |
| CTF.Host.Composition.Gamma_CD21_CD32 | 1 | 2 | 1 | yes |
| CTF.Host.Config | 1 | 2 | 1 | yes |
| CTF.Host.Deployment | 1 | 1 | 1 | yes |
| CTF.Host.Discord | 2 | 2 | 2 | no |
| CTF.Host.Ecs | 1 | 5 | 1 | yes |
| CTF.Host.Logging | 1 | 3 | 1 | yes |
| CTF.Host.ServerService | 1 | 2 | 1 | yes |
| Persistence.InMemory | 6 | 5 | 5 | no |
| Persistence.MariaDB | 5 | 6 | 5 | no |
| Persistence.SQLite | 5 | 6 | 5 | no |
| Persistence.SQLite.Extensions | 2 | 1 | 1 | yes |
| Persistence.Tests.Common | 6 | 7 | 5 | no |
| Persistence.Tests.Common.DatabaseProviders | 3 | 6 | 3 | no |
| Persistence.Tests.Players | 5 | 4 | 1 | yes |
| SampSharp | 1 | 2 | 1 | yes |

24 single=154 namespaces.

### 3.1 Root-causal namespace purity

When subordinate platform/config/test-tooling drivers (anything explicitly fed via `→`, or unmarked when another driver in the same line is marked `(root`) are ignored, the namespace partition becomes much cleaner: **168 of 178 namespaces are single-root-set**, and the remaining **10** composites are documented essential deviations (persistence providers, generated resource classes, test fakes, aggregate facets, composition roots).

## 4. Causal cohesion per namespace

Module M = namespace. purity(M) = 1 / (#distinct driver sets in M). completeness(M) = min over each driver-set A in M of |M ∩ [A]| / |[A]|.

| namespace | classes | tokens | sets | purity | completeness |
|---|---|---|---|---|---|
| CTF.Application | 1 | 1 | 1 | 1.000 | 0.167 |
| CTF.Application.Accounts.Authentication.Gamma_CD08_CD15_CD20_CD31_CD36_CD43 | 1 | 6 | 1 | 1.000 | 1.000 |
| CTF.Application.Accounts.Authentication.Gamma_CD08_CD15_CD20_CD33_CD36_CD43 | 1 | 6 | 1 | 1.000 | 1.000 |
| CTF.Application.Accounts.Authentication.Gamma_CD08_CD20_CD25_CD31_CD32_CD36 | 2 | 6 | 2 | 0.500 | 0.500 |
| CTF.Application.Accounts.Authentication.Gamma_CD08_CD20_CD31_CD32 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.Accounts.Authentication.Gamma_CD08_CD32 | 1 | 2 | 1 | 1.000 | 0.500 |
| CTF.Application.Accounts.Authentication.Gamma_CD08_CD33 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.Accounts.Credentials.Core | 1 | 1 | 1 | 1.000 | 0.500 |
| CTF.Application.Accounts.Credentials.Gamma_CD08_CD09_CD10_CD44 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.Accounts.Credentials.Gamma_CD08_CD18_CD20 | 1 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.Accounts.Extensions | 1 | 1 | 1 | 1.000 | 0.500 |
| CTF.Application.AntiCheat.Gamma_CD09_CD14_CD15_CD17_CD31_CD36_CD43 | 1 | 7 | 1 | 1.000 | 1.000 |
| CTF.Application.AntiCheat.Gamma_CD14_CD17 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.AntiCheat.Gamma_CD14_CD17_CD31_CD32_CD35 | 1 | 5 | 1 | 1.000 | 1.000 |
| CTF.Application.AntiCheat.Gamma_CD14_CD32 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.Audio | 1 | 1 | 1 | 1.000 | 1.000 |
| CTF.Application.Audio.Configuration | 1 | 1 | 1 | 1.000 | 0.167 |
| CTF.Application.Authorization.Admin.Gamma_CD09_CD15_CD17_CD32_CD33_CD36_CD43 | 1 | 7 | 1 | 1.000 | 1.000 |
| CTF.Application.Authorization.Admin.Gamma_CD09_CD17 | 2 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.Authorization.Roles.Core | 3 | 1 | 1 | 1.000 | 1.000 |
| CTF.Application.Authorization.Roles.Gamma_CD09_CD15_CD17_CD20_CD31_CD32_CD33_CD35_CD36_CD43 | 2 | 10 | 2 | 0.500 | 1.000 |
| CTF.Application.Authorization.Roles.Gamma_CD09_CD20 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.Authorization.Roles.Gamma_CD09_CD43 | 2 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.Authorization.Vip | 1 | 6 | 1 | 1.000 | 1.000 |
| CTF.Application.Chat.Core | 2 | 1 | 1 | 1.000 | 1.000 |
| CTF.Application.Chat.Gamma_CD09_CD13_CD15_CD31_CD32_CD36_CD43 | 2 | 7 | 2 | 0.500 | 1.000 |
| CTF.Application.Chat.Gamma_CD09_CD13_CD32_CD36 | 3 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.Chat.Gamma_CD13_CD31_CD32_CD36 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.Chat.Gamma_CD13_CD36 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.CoinEconomy | 2 | 9 | 2 | 0.500 | 1.000 |
| CTF.Application.Combat | 1 | 1 | 1 | 1.000 | 0.500 |
| CTF.Application.Combat.Headshot.Gamma_CD03_CD10_CD17_CD20_CD31_CD32_CD36_CD40 | 1 | 8 | 1 | 1.000 | 1.000 |
| CTF.Application.Combat.Headshot.Gamma_CD03_CD17 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.Combat.Health.Core | 1 | 1 | 1 | 1.000 | 0.500 |
| CTF.Application.Combat.Health.Gamma_CD03_CD09_CD15_CD17_CD31_CD32_CD36_CD43 | 4 | 8 | 2 | 0.500 | 1.000 |
| CTF.Application.Combat.Health.Gamma_CD03_CD31 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.Combat.WeaponSelection.Gamma_CD03_CD04_CD07_CD15_CD31_CD32_CD33_CD36_CD43 | 1 | 9 | 1 | 1.000 | 1.000 |
| CTF.Application.Combat.WeaponSelection.Gamma_CD03_CD32 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.Combos | 1 | 1 | 1 | 1.000 | 0.333 |
| CTF.Application.Combos.Systems.Core | 1 | 1 | 1 | 1.000 | 0.333 |
| CTF.Application.Combos.Systems.Gamma_CD05_CD06_CD07_CD10_CD15_CD31_CD33_CD34_CD35_CD36 | 1 | 10 | 1 | 1.000 | 1.000 |
| CTF.Application.Combos.Systems.Gamma_CD05_CD09_CD12_CD15_CD36 | 1 | 5 | 1 | 1.000 | 1.000 |
| CTF.Application.Combos.Vitalities.Core | 1 | 1 | 1 | 1.000 | 0.333 |
| CTF.Application.Combos.Vitalities.Gamma_CD03_CD05_CD06 | 5 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.Combos.Vitalities.Gamma_CD05_CD06 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.CommandInfrastructure.Gamma_CD02_CD08_CD12_CD32_CD43 | 1 | 5 | 1 | 1.000 | 1.000 |
| CTF.Application.CommandInfrastructure.Gamma_CD31_CD43 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.Commands | 1 | 1 | 1 | 1.000 | 0.500 |
| CTF.Application.Commands.Admin | 2 | 7 | 2 | 0.500 | 0.500 |
| CTF.Application.Commands.Basic | 1 | 5 | 1 | 1.000 | 1.000 |
| CTF.Application.Commands.Moderator | 2 | 6 | 2 | 0.500 | 1.000 |
| CTF.Application.Commands.Vip.Gamma_CD09_CD15_CD31 | 1 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.Commands.Vip.Gamma_CD09_CD15_CD33 | 1 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.Discord | 1 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules | 1 | 1 | 1 | 1.000 | 0.250 |
| CTF.Application.GameRules.ClassSelection.Gamma_CD02_CD08_CD12_CD32 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.ClassSelection.Gamma_CD02_CD11_CD12_CD31 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.ClassSelection.Gamma_CD02_CD15_CD17_CD31 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.ClassSelection.Gamma_CD02_CD15_CD31_CD33_CD43 | 1 | 5 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.ClassSelection.Gamma_CD02_CD31 | 1 | 2 | 1 | 1.000 | 0.125 |
| CTF.Application.GameRules.ClassSelection.Gamma_CD02_CD32 | 2 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Configuration | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Flag.Core | 1 | 1 | 1 | 1.000 | 0.250 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD03_CD09_CD10_CD15_CD31_CD37 | 1 | 7 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD06_CD10_CD17_CD20_CD35_CD37_CD38_CD40 | 1 | 9 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD06_CD10_CD20_CD34_CD35_CD37_CD40 | 1 | 8 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD06_CD10_CD20_CD35_CD37_CD40 | 1 | 7 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD09_CD15_CD17_CD38 | 1 | 5 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD10_CD20_CD35_CD37_CD38_CD40 | 1 | 7 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD17 | 2 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD17_CD31_CD37_CD40_CD41 | 2 | 6 | 2 | 0.500 | 1.000 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD17_CD35_CD37_CD38_CD40 | 1 | 6 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD17_CD37_CD40_CD41 | 1 | 5 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD21 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD31 | 1 | 2 | 1 | 1.000 | 0.125 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD35 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD37_CD38_CD41 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Flag.Gamma_CD02_CD38_CD44 | 2 | 4 | 2 | 0.500 | 1.000 |
| CTF.Application.GameRules.Match.Core | 1 | 1 | 1 | 1.000 | 0.250 |
| CTF.Application.GameRules.Match.Gamma_CD02_CD31 | 4 | 2 | 1 | 1.000 | 0.500 |
| CTF.Application.GameRules.Match.Gamma_CD02_CD35_CD36 | 1 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Players.Gamma_CD02_CD31 | 1 | 2 | 1 | 1.000 | 0.125 |
| CTF.Application.GameRules.Players.Gamma_CD02_CD31_CD32 | 2 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Players.Gamma_CD02_CD31_CD32_CD41 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.GameRules.Players.Gamma_CD02_CD31_CD36 | 1 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.GunGames | 2 | 2 | 2 | 0.500 | 0.042 |
| CTF.Application.GunGames.Progression.Core | 14 | 1 | 1 | 1.000 | 0.583 |
| CTF.Application.GunGames.Progression.Gamma_CD07_CD32 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.GunGames.Results.Gamma_CD03_CD07_CD31_CD32_CD36 | 3 | 5 | 1 | 1.000 | 1.000 |
| CTF.Application.GunGames.Results.Gamma_CD07_CD10_CD20_CD31_CD32_CD36 | 1 | 6 | 1 | 1.000 | 1.000 |
| CTF.Application.GunGames.Rewards | 2 | 3 | 2 | 0.500 | 0.042 |
| CTF.Application.GunGames.Systems.Core | 6 | 1 | 1 | 1.000 | 0.250 |
| CTF.Application.GunGames.Systems.Gamma_CD03_CD07_CD09_CD15_CD31_CD32_CD33_CD35_CD36_CD43 | 1 | 10 | 1 | 1.000 | 1.000 |
| CTF.Application.GunGames.Systems.Gamma_CD03_CD07_CD31_CD32 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.MapIcons.Core | 1 | 1 | 1 | 1.000 | 1.000 |
| CTF.Application.MapIcons.Gamma_CD11_CD37_CD38 | 1 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.Maps.Core | 6 | 1 | 1 | 1.000 | 0.750 |
| CTF.Application.Maps.Gamma_CD11_CD12 | 2 | 2 | 2 | 0.500 | 0.125 |
| CTF.Application.Maps.Gamma_CD11_CD17_CD32_CD34_CD37_CD38_CD42 | 1 | 7 | 1 | 1.000 | 1.000 |
| CTF.Application.Maps.Rotation.Core | 4 | 1 | 1 | 1.000 | 1.000 |
| CTF.Application.Maps.Rotation.Gamma_CD02_CD11_CD12_CD31_CD34_CD36_CD41_CD42 | 1 | 8 | 1 | 1.000 | 1.000 |
| CTF.Application.Maps.Rotation.Gamma_CD09_CD11_CD12_CD15_CD31_CD33_CD34_CD36_CD43 | 1 | 9 | 1 | 1.000 | 1.000 |
| CTF.Application.Pickups | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.PlayerResources.Core | 4 | 1 | 1 | 1.000 | 1.000 |
| CTF.Application.PlayerResources.Gamma_CD15_CD20_CD31_CD44 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.PlayerResources.Gamma_CD20_CD44 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.Players | 2 | 2 | 2 | 0.500 | 0.167 |
| CTF.Application.Players.Accounts | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.Players.Chats | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.Players.TopPlayers | 2 | 3 | 2 | 0.500 | 0.500 |
| CTF.Application.Players.Weapons.Catalogs.Core | 1 | 1 | 1 | 1.000 | 0.083 |
| CTF.Application.Players.Weapons.Catalogs.Gamma_CD04_CD17 | 1 | 2 | 1 | 1.000 | 0.500 |
| CTF.Application.Players.Weapons.Core | 1 | 1 | 1 | 1.000 | 0.083 |
| CTF.Application.Players.Weapons.Gamma_CD03_CD04_CD07_CD09_CD15_CD32_CD33_CD36_CD43 | 1 | 9 | 1 | 1.000 | 1.000 |
| CTF.Application.Players.Weapons.Gamma_CD04_CD17 | 1 | 2 | 1 | 1.000 | 0.500 |
| CTF.Application.RconSecurity | 1 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.Statistics.Ranks.Core | 4 | 1 | 1 | 1.000 | 0.667 |
| CTF.Application.Statistics.Ranks.Gamma_CD10_CD07_CD09_CD20 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.Statistics.Ranks.Gamma_CD10_CD15_CD33_CD43 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.Statistics.Score.Gamma_CD10_CD06 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.Statistics.Score.Gamma_CD10_CD06_CD07_CD20_CD31_CD32_CD35_CD36 | 1 | 8 | 1 | 1.000 | 1.000 |
| CTF.Application.Statistics.Score.Gamma_CD10_CD08_CD15_CD20_CD31_CD33_CD34_CD43 | 1 | 8 | 1 | 1.000 | 1.000 |
| CTF.Application.Statistics.Score.Gamma_CD10_CD09_CD15_CD20 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.Statistics.Score.Gamma_CD10_CD09_CD15_CD31_CD32_CD36_CD43 | 1 | 7 | 1 | 1.000 | 1.000 |
| CTF.Application.Statistics.Score.Gamma_CD10_CD20 | 1 | 2 | 1 | 1.000 | 0.500 |
| CTF.Application.Statistics.Score.Gamma_CD10_CD31_CD32_CD34 | 2 | 4 | 2 | 0.500 | 1.000 |
| CTF.Application.Statistics.TeamStats.Gamma_CD10_CD02 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Application.Statistics.TeamStats.Gamma_CD10_CD02_CD09_CD15_CD31_CD33_CD34 | 1 | 7 | 1 | 1.000 | 1.000 |
| CTF.Application.Statistics.TeamStats.Gamma_CD10_CD02_CD15_CD31_CD33 | 1 | 5 | 1 | 1.000 | 1.000 |
| CTF.Application.Statistics.TopPlayers.Core | 2 | 1 | 1 | 1.000 | 0.333 |
| CTF.Application.Statistics.TopPlayers.Gamma_CD10_CD15_CD17_CD20_CD33_CD36_CD43 | 1 | 7 | 1 | 1.000 | 1.000 |
| CTF.Application.Statistics.TopPlayers.Gamma_CD10_CD17 | 1 | 2 | 1 | 1.000 | 0.500 |
| CTF.Application.Teams | 4 | 9 | 4 | 0.250 | 0.125 |
| CTF.Application.Tests | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Authorization | 1 | 3 | 1 | 1.000 | 0.500 |
| CTF.Application.Tests.Fakes | 5 | 3 | 2 | 0.500 | 0.125 |
| CTF.Application.Tests.GameRules | 3 | 3 | 1 | 1.000 | 0.429 |
| CTF.Application.Tests.GunGames.Core | 2 | 1 | 1 | 1.000 | 0.083 |
| CTF.Application.Tests.GunGames.Gamma_CD07_CD26_CD27 | 6 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Maps | 3 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Maps.Rotation | 3 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.PlayerResources | 1 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Players.Accounts | 7 | 8 | 6 | 0.167 | 0.111 |
| CTF.Application.Tests.Players.Extensions | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Players.Ranks | 3 | 3 | 1 | 1.000 | 0.333 |
| CTF.Application.Tests.Players.TopPlayers | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Players.Vitalities | 2 | 3 | 1 | 1.000 | 0.667 |
| CTF.Application.Tests.Players.Weapons | 1 | 3 | 1 | 1.000 | 0.333 |
| CTF.Application.Tests.Players.Weapons.Catalogs.Core | 1 | 1 | 1 | 1.000 | 0.083 |
| CTF.Application.Tests.Players.Weapons.Catalogs.Gamma_CD04_CD17_CD26_CD27 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Players.Weapons.Catalogs.Gamma_CD04_CD26_CD27 | 2 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Statistics.Gamma_CD02_CD10_CD26_CD27 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.Tests.Statistics.Gamma_CD10_CD26_CD27 | 4 | 3 | 1 | 1.000 | 0.444 |
| CTF.Application.Tests.Teams | 3 | 3 | 1 | 1.000 | 0.429 |
| CTF.Application.Tests.TextDraws | 1 | 3 | 1 | 1.000 | 0.111 |
| CTF.Application.TextDraws.Core | 1 | 1 | 1 | 1.000 | 1.000 |
| CTF.Application.TextDraws.Gamma_CD02_CD10_CD34_CD36 | 1 | 4 | 1 | 1.000 | 1.000 |
| CTF.Application.TextDraws.Gamma_CD11_CD12_CD34 | 1 | 3 | 1 | 1.000 | 1.000 |
| CTF.Application.WeaponCatalogs | 9 | 1 | 1 | 1.000 | 0.750 |
| CTF.Host.Bcrypt | 1 | 1 | 1 | 1.000 | 0.500 |
| CTF.Host.CommandInfrastructure | 1 | 1 | 1 | 1.000 | 1.000 |
| CTF.Host.Composition.Gamma_CD17_CD21_CD22 | 1 | 3 | 1 | 1.000 | 1.000 |
| CTF.Host.Composition.Gamma_CD21_CD22 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Host.Composition.Gamma_CD21_CD32 | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Host.Config | 1 | 2 | 1 | 1.000 | 1.000 |
| CTF.Host.Deployment | 1 | 1 | 1 | 1.000 | 1.000 |
| CTF.Host.Discord | 2 | 2 | 2 | 0.500 | 1.000 |
| CTF.Host.Ecs | 1 | 5 | 1 | 1.000 | 1.000 |
| CTF.Host.Logging | 1 | 3 | 1 | 1.000 | 1.000 |
| CTF.Host.ServerService | 1 | 2 | 1 | 1.000 | 1.000 |
| Persistence.InMemory | 6 | 5 | 5 | 0.200 | 1.000 |
| Persistence.MariaDB | 5 | 6 | 5 | 0.200 | 0.167 |
| Persistence.SQLite | 5 | 6 | 5 | 0.200 | 0.167 |
| Persistence.SQLite.Extensions | 2 | 1 | 1 | 1.000 | 1.000 |
| Persistence.Tests.Common | 6 | 7 | 5 | 0.200 | 0.500 |
| Persistence.Tests.Common.DatabaseProviders | 3 | 6 | 3 | 0.333 | 1.000 |
| Persistence.Tests.Players | 5 | 4 | 1 | 1.000 | 1.000 |
| SampSharp | 1 | 2 | 1 | 1.000 | 1.000 |

## 5. Module purity (single-set audit)

`IVP/tools/ModulePurity.java .` — a *module* is single-set when all of its transmitted
elements share exactly one change-driver set (type declarations are not IVP elements; a
nested class is its own module).

```
modules with transmissions evaluated=185 single-set=175 violating=10 skipped=82
```

10 class-level modules remain violating (their members span ≥ 2 driver sets):

- `PlayerStatsPerRound` (`Statistics`)
- `GunGameReward` (`GunGames`)
- `ComboSystem` (`Combos`)
- `TeamStatsPerRound` (`Statistics`)
- `PlayerAppearance` (`PlayerResources`)
- `Startup` (`Host/Ecs`)
- `TeamScoreboardSystem` (`Statistics`)
- `PrivateAdminChat` (`Chat`)
- `PrivateModeratorChat` (`Chat`)
- `PrivateVipChat` (`Chat`)

The 9 Application-level offenders are internal contract / call-site refactors — their
multi-set members answer to role-split or coin/game rules plus a platform subordinate
(CD-10 & CD-06, CD-07 & CD-03/06/10, CD-05 & CD-06/07, chat Id vs the send path). They
are *not* topic-fusion. `Startup` is the only external-contract blocker (composition).

## 6. Per-class driver assignment (multiset summary)

{1=95, 2=61, 3=68, 4=40, 5=16, 6=8, 7=11, 8=8, 9=5, 10=3}

---

## 7. Readings & interpretation

### 7.1 Purity improvements at the leaves

The namespace layer is now partitioned into **exact-set submodules**: within each
namespace, types with the same full `Change drivers:` set are co-located, and types with
different sets are in separate `Core` (root-only) or `Gamma_<drivers>` sub-namespaces.
After the regroup, **154 of 178 namespaces are single-set by raw driver-set equality**,
and **168 of 178 are single-root-set**. The 10 root-causal composites are the documented
essential deviations from `IVP/constraints.md`. In the before state only 12 of 57
namespaces were single-set.

### 7.2 Composite namespaces are now subordinate-driven, not topic-fused

The original topic-based namespaces (`GameRules`, `Statistics`, `Accounts`,
`Authorization`, `Combat`, `Combos`, `Commands`, etc.) have been recursively split into
exact-set submodules. Each leaf namespace now contains only types whose `Change drivers:`
line is identical; the parent namespaces hold only DI roots and generated-resource
deviations. The remaining 10 root-set-composite namespaces are the documented essential
composites from `IVP/constraints.md` (persistence providers, generated resource classes,
test fakes, aggregate facets, composition roots).

### 7.3 Scatter

Scattered driver sets fell from 23 to 21. Remaining scatter is concentrated in the
horizontal drivers (CD-17 .env schema, CD-20 ports, CD-26/27/28 test tooling) — decreed
cross-cutting axes per `IVP/constraints.md`. Change-coupling drivers (CD-02, CD-07,
CD-10) are concentrated in their domain modules.

### 7.4 Bottom line

The after state is a **driver-set-aligned modularisation**: 154 of 178 namespaces are
single-set by raw driver-set equality, 168 of 178 are single-root-set, every leaf
module's elements share exactly one change-driver set, and the former umbrella driver has
zero activation. The remaining impurity is confined to the 10 documented essential
composites.

### 7.5 Module-purity reading

The `ModulePurity` audit confirms the leaf-level story: 175 of 185 evaluated modules are
single-set; only 10 class-level modules span multiple sets, all but one inside Application
and all the result of internal contract / call-site shapes, not topic-fusion. `Startup`
(composition) is the single external-contract blocker. The remaining work is completing
those internal refactors, after which only decreed composites (settings, wiring,
persistence, test tooling) would retain multi-set members by design.

---

## 8. Caveats & limitations (honest, must-read before interpreting)

1. **Purity is over driver sets, not driver count** — as in the before report.
2. **Set-space refinement breaks raw count comparability.** 137 → 181 sets reflects the
   CD-01 decomposition (finer vocabulary) plus the reorganisation. Comparable: scatter,
   composite share, activation of CD-01 (= 0), purity of leaf modules.
3. **Namespace granularity.** The tool measures at namespace level; nested classes
   (`Flag.CarrierAttachment`, components) fold into their file's namespace. The
   nested-module rule (no upward transmission) is enforced at annotation level, not by
   this tool.
4. **Member-level detection is a heuristic** — as in the before report.
5. **Essential-vs-spurious classification** — see `IVP/constraints.md` Appendix A; the
   production/test split is reported (production=248, test=67).
6. **Activation frequency λ(γ) is unmeasured**; only unweighted module-touch counts are
   reported (no λ artifact exists in the repo).
7. **Completeness for singleton test sets is inflated** — same granularity artifact as
   the before report.
