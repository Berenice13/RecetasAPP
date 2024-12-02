using Microsoft.Maui.Controls;

namespace RecetasApp.Views
{
    public partial class EditUserPage : ContentPage
    {
        public EditUserPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }

        // Función que se llama cuando se hace clic en "Cambiar foto"
        private async void OnChangePhotoClicked(object sender, EventArgs e)
        {
            // Lógica para cambiar la foto (puedes usar un picker o tomar una foto)
            // Ejemplo: Pick a photo using the media picker or camera
        }

        // Función que se llama cuando se hace clic en "Guardar cambios"
        private void OnSaveChangesClicked(object sender, EventArgs e)
        {

            // Guardar cambios (enviar a un backend, guardar localmente, etc.)
        }
    }
}
