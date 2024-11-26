namespace RecetasApp.Views
{
    using RecetasApp.Services;
    using RecetasApp.Models;
    using Microsoft.Maui.Controls;
    using System.Text.RegularExpressions;
    using System.Diagnostics;

    public partial class LoginPage : ContentPage
    {
        private readonly ApiService _apiService;
        private bool _isLoginVisible;
        private bool _isRegisterVisible;

        public bool IsLoginVisible
        {
            get => _isLoginVisible;
            set
            {
                _isLoginVisible = value;
                OnPropertyChanged(nameof(IsLoginVisible));
            }
        }

        public bool IsRegisterVisible
        {
            get => _isRegisterVisible;
            set
            {
                _isRegisterVisible = value;
                OnPropertyChanged(nameof(IsRegisterVisible));
            }
        }

        public LoginPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
             BindingContext = this;

            // Mostrar el formulario de inicio de sesión por defecto
            IsLoginVisible = true;
            IsRegisterVisible = false;
        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            IsLoginVisible = true;
            IsRegisterVisible = false;
        }

        private void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            IsLoginVisible = false;
            IsRegisterVisible = true;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try{
                var email = emailEntry.Text; 
                var password = passwordEntry.Text; 

                if (!IsValidEmail(email))
                {
                    await DisplayAlert("Error", "Por favor, ingresa un correo electrónico válido.", "OK");
                    emailEntry.Focus(); // Enfocar el campo del correo si no es válido
                    return;
                }

                if (!IsValidPassword(password))
                {
                    await DisplayAlert("Error", "La contraseña debe tener al menos 6 caracteres.", "OK");
                    passwordEntry.Focus(); // Enfocar el campo de contraseña si no es válida
                    return;
                }

                var URL = ApiRoutes.ApiRoutes.BaseUrl +  ApiRoutes.ApiRoutes.StudentLogin.Login;

                Debug.WriteLine("URL: " + URL);


                // Si el inicio de sesión es exitoso, navega a la página HomePage
                Debug.WriteLine("Intentando navegar a HomePage...");
                await Shell.Current.GoToAsync("//home");

    

                /*
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
                */

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            try{
                Debug.WriteLine("Intentando navegar a HomePage...");

                // Aquí realizas el login con la API o validaciones locales
                var loginSuccess = true; // Cambiar esta parte según tu lógica de login

                if (loginSuccess)
                {
                    // Cambio de la página principal a AppShell
                    (Application.Current as App).SetMainPageToAppShell();

                    // Navegar a la página de inicio dentro de AppShell
                    await Shell.Current.GoToAsync("//home");
                }
                else
                {
                    await DisplayAlert("Error", "Login fallido", "OK");
                }


            } 
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void OnEmailCompleted(object sender, EventArgs e)
        {
            passwordEntry.Focus(); 
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Expresión regular para validar el correo electrónico
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        private bool IsValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 6;
        }

    }
}
