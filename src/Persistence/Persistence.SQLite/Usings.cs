// <remarks>Change drivers: CD-32 (root; platform/ECS runtime usings); CD-23 (Serilog logging) → CD-32; CD-17 (game configuration/.env schema) → CD-32; CD-19 (MariaDB SQL dialect) → CD-32; CD-30 (SQLite SQL dialect) → CD-32; CD-20 (outbound repository contract) → CD-32</remarks>
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
