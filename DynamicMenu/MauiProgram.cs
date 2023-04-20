using CommunityToolkit.Maui;

using DynamicMenu.Services;
using DynamicMenu.ViewModels;
using DynamicMenu.Views;
namespace DynamicMenu;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<AppShell>();
        //pages which don't have parameters in constructors, are automagically instantiated by MAUI using parameterless constructor
        //pages which have parameters in constructors, aren't created by MAUI and thus we have to create them via DI
        builder.Services.AddSingleton<LoginPage>(); 

        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<AppShellViewModel>();
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<AppState>();

        return builder.Build();
    }
}
