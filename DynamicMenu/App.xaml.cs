namespace DynamicMenu;

public partial class App : Application
{
    public App(IServiceProvider services)
    {
        InitializeComponent();
        //to be able to access StaticResource in XAML, App has to be created sooner than the rest of the app pages.
        //https://github.com/dotnet/maui/issues/11485
        //this is why AppShell is not passed as constructor parameter but is resolved like this instead.
        var appShell = services.GetRequiredService<AppShell>();
        MainPage = appShell;
    }
}
