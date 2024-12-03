using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;
using RecetasApp.Models;
using RecetasApp.Services;

namespace RecetasApp.Views
{
    public partial class EditUserPage : ContentPage
    {
        private readonly ApiService _apiService;

        public EditUserPage()
        {
            _apiService = new ApiService();
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadInfoUser();
        }

        private async void LoadInfoUser()
        {
            try{
                var URL = ApiRoutes.ApiRoutes.BaseUrl +  ApiRoutes.ApiRoutes.Routes.UserInfo;
                var data = new {};

                var apiResponse = await _apiService.PostAsync<DataUser>(URL, data, true);
                
                if (apiResponse.Status == 200 && apiResponse.Data != null)
                {
                    var datosUser = apiResponse.Data.User;
                    UserName.Text = datosUser!.Nombre;
                    UserEmail.Text = datosUser!.Email;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            try{
                var nombre = UserName.Text; 
                var email = UserEmail.Text; 

                // Validar que los campos no estén vacíos
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(email))
                {
                    await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                    return;
                }

                if (!IsValidEmail(email))
                {
                    await DisplayAlert("Error", "Por favor, ingresa un correo electrónico válido.", "OK");
                    UserEmail.Focus();
                    return;
                }


                var URL = ApiRoutes.ApiRoutes.BaseUrl +  ApiRoutes.ApiRoutes.Routes.UserUpdate;
                var data = new
                {
                    nombre = nombre,
                    email = email
                };

                var apiResponse = await _apiService.PutAsync<TokenResponse>(URL, data, false);
                
                if (apiResponse.Status == 200 && apiResponse.Data != null)
                {
                    await DisplayAlert("Éxito", "Usuario actualizado exitosamente.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", apiResponse.Msg ?? "Error al actualizar usuario.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                await DisplayAlert("Error", ex.Message, "OK");
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

    }
}
