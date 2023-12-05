using Microsoft.Extensions.Logging;
using MovieBase.ClientLib;
using MovieBase.Common.Interfaces;
using MovieBase.XPlatform.Services;

namespace MovieBase.XPlatform;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        RegisterServices(builder.Services);

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<MainViewModel>();
        services.AddTransient<MainPage>();
        services.AddTransient<AboutViewModel>();
        services.AddTransient<AboutPage>();
        services.AddTransient<ListViewModel>();
        services.AddTransient<ListPage>();

        services.AddSingleton<IMovieService, MovieService>();
        services.AddSingleton<INavigationService, NavigationService>();
    }
}
