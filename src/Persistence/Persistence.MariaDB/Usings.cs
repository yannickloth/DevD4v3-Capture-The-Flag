// <remarks>Change drivers: CD-20 (outbound repository contract); CD-18 (database schema/player data model); CD-19 (MariaDB SQL dialect) → CD-18; CD-21 (DI container/composition)</remarks>
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using MySqlConnector;
global using YeSql.Net;
global using GameMode.Common;
global using CTF.Application.Players.Accounts;
global using CTF.Application.Players.Accounts.Roles;
global using CTF.Application.Players.Ranks;
global using CTF.Application.Players.TopPlayers;