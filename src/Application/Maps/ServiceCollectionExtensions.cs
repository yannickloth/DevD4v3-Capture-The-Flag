namespace CTF.Application.Maps;

[ChangeDriversAttribute(ChangeDriver.Map)]
public static class MapServicesExtensions
{
    [ChangeDriversAttribute(ChangeDriver.Map)]
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
