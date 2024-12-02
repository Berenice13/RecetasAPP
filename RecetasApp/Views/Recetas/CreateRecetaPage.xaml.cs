using RecetasApp.Models;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;

namespace RecetasApp.Views
{
    public partial class CreateRecetaPage : ContentPage
    {

        public CreateRecetaPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);

        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            try{

            } catch (Exception ex)
            {
                await DisplayAlert("Error", "Error al guardar la receta.", "OK");
            }
        }

        

     
    }
}
