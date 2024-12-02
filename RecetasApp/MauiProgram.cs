using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace RecetasApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
    {
		var builder = MauiApp.CreateBuilder();
		builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit() // Encadenar el método aquí
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("FontAwesome.otf", "FontAwesome");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
