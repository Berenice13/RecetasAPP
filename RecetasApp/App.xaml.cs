using RecetasApp.Views;

namespace RecetasApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new LoginPage();
	}

	// Método para cambiar la página principal a AppShell
    public void SetMainPageToAppShell()
    {
        MainPage = new AppShell();
    }
}
