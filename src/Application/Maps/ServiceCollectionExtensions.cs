namespace CTF.Application.Maps;

/// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
public static class MapServicesExtensions
{
    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
    public static IServiceCollection AddMapServices(
        this IServiceCollection services,
        string mapsPath)
    {
        services
            .AddSingleton<MapInfoService>()
            .AddSingleton<MapRotationService>()
            .AddSingleton<MapTextDrawRenderer>()
            .AddSingleton(_ => new MapCollection(mapsPath))
            .AddSingleton(sp =>
            {
                var maps = sp.GetRequiredService<MapCollection>();
                return new MapInfoService(
                    initialMap: maps.GetById(0).Value,
                    mapsPath: mapsPath);
            }); ;

        return services;
    }
}
