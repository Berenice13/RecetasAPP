using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace RecetasApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Ajustar el modo del teclado para que se desplace el contenido
        Window.SetSoftInputMode(SoftInput.AdjustResize);
    }
}

