using RecetasApp.Views;

namespace RecetasApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new LoginPage();
	}
}
