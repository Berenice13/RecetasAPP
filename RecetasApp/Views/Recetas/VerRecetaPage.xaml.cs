using System.Diagnostics;
using Microsoft.Maui.Controls;
using RecetasApp.Models;

namespace RecetasApp.Views
{
    public partial class VerRecetaPage : ContentPage
    {
        private Receta _receta;
        private int _currentRating = 0;


        public VerRecetaPage(Receta receta)
        {
            _receta = receta;
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
            LoadReceta();
        }

        public void LoadReceta(){
            RecetaImage.Source = string.IsNullOrEmpty(_receta.Imagen) ? "todas.png" : _receta.Imagen;
            RecetaTitle.Text = _receta.Titulo;
            RecetaUsuario.Text = _receta.UsuarioId.ToString();
            RecetaTiempo.Text = _receta.TiempoPreparacion + " minutos";
            RecetaDescripcion.Text = _receta.Descripcion;
            RecetaDificultad.Text = _receta.Dificultad != null ? CapitalizeFirstLetter(_receta.Dificultad) : string.Empty;
        }

        private string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        private void OnStarClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && int.TryParse(button.CommandParameter.ToString(), out int rating))
            {
                _currentRating = rating;
                UpdateStarImages();
            }
        }

        private void UpdateStarImages()
        {
            Star1.Source = _currentRating >= 1 ? "llena.png" : "sola.png";
            Star2.Source = _currentRating >= 2 ? "llena.png" : "sola.png";
            Star3.Source = _currentRating >= 3 ? "llena.png" : "sola.png";
            Star4.Source = _currentRating >= 4 ? "llena.png" : "sola.png";
            Star5.Source = _currentRating >= 5 ? "llena.png" : "sola.png";
        }
    
    }
}