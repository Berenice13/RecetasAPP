namespace RecetasApp.Views
{
    using RecetasApp.Services;
    using RecetasApp.Models;
    using Microsoft.Maui.Controls;

    public partial class LoginPage : ContentPage
    {
        private readonly ApiService _apiService;

        public LoginPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try{
                var email = emailEntry.Text; // El Entry para el correo
                var password = passwordEntry.Text; // El Entry para la contraseña

                // Crear un objeto con los datos de login
                var loginData = new
                {
                    Email = email,
                    Password = password
                };

                // Llamamos al servicio para hacer login, esperando una respuesta con 'data' de tipo string (por ejemplo, un token)
                var apiResponse = await _apiService.PostAsync<string>(ApiRoutes.ApiRoutes.StudentLogin.Login, loginData);

                if (apiResponse.Status == "Success")
                {
                    // Si el login fue exitoso, navega a otra página o guarda el token
                    await DisplayAlert("Éxito", "Inicio de sesión exitoso", "OK");

                    // Si tienes un token, lo puedes almacenar
                    var token = apiResponse.Data; // Este sería el token recibido
                }
                else
                {
                    // Si el login falla, mostramos el mensaje de error devuelto por la API
                    await DisplayAlert("Error", apiResponse.Error ?? "Error desconocido", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
