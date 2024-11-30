using Microsoft.Maui.Storage; // Importar Preferences
using RecetasApp.Views;

namespace RecetasApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        var token = Preferences.Get("AuthToken", string.Empty);

        if (!string.IsNullOrEmpty(token))
        {
            MainPage = new AppShell();
        }
        else
        {
            MainPage = new LoginPage();
        }
    }

    public void SetMainPageToAppShell()
    {
        MainPage = new AppShell();
    }
}
