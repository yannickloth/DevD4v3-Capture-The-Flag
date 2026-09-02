// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20; CD-30 (SQLite SQL dialect) → CD-18; CD-21 (DI container/composition) → CD-20</remarks>
global using SampSharp.Entities;
global using System.Text.RegularExpressions;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Data.Sqlite;
global using YeSql.Net;
global using Persistence.SQLite.Extensions;
global using GameMode.Common;
global using CTF.Application.Players.Accounts;
global using CTF.Application.Players.Accounts.Roles;
global using CTF.Application.Players.Ranks;
global using CTF.Application.Players.TopPlayers;