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

                // Validar que los campos no estén vacíos
                /*if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                    return;
                }

                if (!IsValidEmail(email))
                {
                    await DisplayAlert("Error", "Por favor, ingresa un correo electrónico válido.", "OK");
                    emailEntry.Focus();
                    return;
                }

                if (!IsValidPassword(password))
                {
                    await DisplayAlert("Error", "La contraseña debe tener al menos 6 caracteres.", "OK");
                    passwordEntry.Focus();
                    return;
                }

                var URL = ApiRoutes.ApiRoutes.BaseUrl +  ApiRoutes.ApiRoutes.Routes.Login;
                var loginData = new
                {
                    email = email,
                    password = password
                };

                var apiResponse = await _apiService.PostAsync<TokenResponse>(URL, loginData, false);
                
                if (apiResponse.Status == 200 && apiResponse.Data != null)
                {
                    var tokenDetails = apiResponse.Data.Token; 
                    var token = tokenDetails?.Token;
                    Preferences.Set("AuthToken", token);

                    if (Application.Current is App app)
                    {
                        app.SetMainPageToAppShell();
                    }

                    await Shell.Current.GoToAsync("//home");
                }
                else
                {
                    await DisplayAlert("Error", apiResponse.Msg ?? "Error al iniciar sesión.", "OK");
                }*/

                // Cambio de la página principal a AppShell
                    if (Application.Current is App app)
                    {
                        app.SetMainPageToAppShell();
                    }

                    // Navegar a la página de inicio dentro de AppShell
                    await Shell.Current.GoToAsync("//home");
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
                var nombre = registerNameEntry.Text;
                var email = registerEmailEntry.Text; 
                var password = registerPasswordEntry.Text; 
                var confirmPassword = confirmRegisterPassword.Text;

                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                    return;
                }

                if(password != confirmPassword)
                {
                    await DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");
                    confirmRegisterPassword.Focus();
                    return;
                }

                if (!IsValidEmail(email))
                {
                    await DisplayAlert("Error", "Por favor, ingresa un correo electrónico válido.", "OK");
                    registerEmailEntry.Focus(); 
                    return;
                }

                if (!IsValidPassword(password))
                {
                    await DisplayAlert("Error", "La contraseña debe tener al menos 6 caracteres.", "OK");
                    registerPasswordEntry.Focus();
                    return;
                }

                var URL = ApiRoutes.ApiRoutes.BaseUrl +  ApiRoutes.ApiRoutes.Routes.Register;
                var registerData = new
                {
                    nombre = nombre,
                    email = email,
                    password = password,
                    password_confirmation = confirmPassword
                };

                var apiResponse = await _apiService.PostAsync<RegisterData>(URL, registerData, false);

                Debug.WriteLine(apiResponse);
                if ((apiResponse.Status == 201 || apiResponse.Status == 200) && apiResponse.Data != null)
                {
                    var usuarioRegistrado = apiResponse.Data.User;
                    await LoginAsync(email, password);
                }
                else
                {
                    // Si el registro falla, mostramos el mensaje de error devuelto por la API
                    var errorMessage = apiResponse?.Msg ?? "Error al registrar usuario.";
                    await DisplayAlert("Error", errorMessage, "OK");
                }
            } 
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void OnNameCompleted(object sender, EventArgs e)
        {
            registerEmailEntry.Focus(); 
        }

        private void OnEmailCompleted(object sender, EventArgs e)
        {
            if(IsLoginVisible){
                passwordEntry.Focus(); 
            } else{
                registerPasswordEntry.Focus();
            }
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

        private void OnTogglePasswordVisibilityClicked(object sender, EventArgs e)
        {
            registerPasswordEntry.IsPassword = !registerPasswordEntry.IsPassword;
        }

        private void OnTogglePasswordConfirm(object sender, EventArgs e)
        {
            confirmRegisterPassword.IsPassword = !confirmRegisterPassword.IsPassword;
        }


        private async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var URL = ApiRoutes.ApiRoutes.BaseUrl +  ApiRoutes.ApiRoutes.Routes.Login;
                var loginData = new
                {
                    email = email,
                    password = password
                };

                var apiResponse = await _apiService.PostAsync<TokenResponse>(URL, loginData, false);
                Debug.WriteLine(apiResponse);

                if (apiResponse.Status == 200 && apiResponse.Data != null)
                {
                    // Guardar el token o realizar otras acciones necesarias
                    var token = apiResponse.Data; // Este sería el token recibido
                    await DisplayAlert("Éxito", "Usuario creado con éxito", "OK");
                    if (Application.Current is App app)
                    {
                        app.SetMainPageToAppShell();
                    }

                    await Shell.Current.GoToAsync("//home");
                    return true;
                }
                else
                {
                    await DisplayAlert("Error", apiResponse.Msg ?? "Error desconocido", "OK");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected error: {ex.Message}");
                await DisplayAlert("Error", "Error al inciar sesión", "OK");
                return false;
            }
        }
    }
}
