using RecetasApp.Models;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using RecetasApp.Services;

namespace RecetasApp.Views
{
    public partial class CreateRecetaPage : ContentPage
    {
        private readonly ApiService _apiService;
        private int categoriaSeleccionadaId = 2;
        private int userId;
        private Receta? _receta;

        bool isNewReceta = true;
        int? idReceta;


        public CreateRecetaPage(int user, Receta? receta = null)
        {
            InitializeComponent();
            if(receta != null)
            {
                isNewReceta = false;
                _receta = receta;
                LoadDataReceta();
                Btn.Text = "Guardar Receta";
            }

            _apiService = new ApiService();
            userId = user;
            LoadCategorias();
            Shell.SetTabBarIsVisible(this, false);
        }

        private void LoadDataReceta(){
            try{
                idReceta = _receta!.Id;
                categoriaSeleccionadaId = _receta.CategoriaId;
                Titulo.Text = _receta.Titulo;
                Descripcion.Text = _receta.Descripcion;
                TiempoPreparacion.Text = _receta.TiempoPreparacion;

                var dificultad = "";
                switch(_receta.Dificultad)
                {
                    case "facil":
                        dificultad = "Fácil";
                        break;
                    case "medio":
                        dificultad = "Media";
                        break;
                    case "dificil":
                        dificultad = "Difícil";
                        break;
                    default:
                        break;
                }
                
                Dificultad.SelectedItem = dificultad;
            } catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }

        private async void LoadCategorias()
        {
            try
            {
                var categorias = new List<Categoria>
                {
                    new Categoria { Id = 2, Nombre = "Cocina", ImagenUrl = "cocina.png" },
                    new Categoria { Id = 3, Nombre = "Repostería", ImagenUrl = "reposteria.png" },
                    new Categoria { Id = 4, Nombre = "Cócteles", ImagenUrl = "cocteles.png" },
                    new Categoria { Id = 5, Nombre = "Saludable", ImagenUrl = "saludable.png" }
                };

                CategoriaStackLayout.Children.Clear();

                foreach (var categoria in categorias)
                {
                    var stackLayout = new StackLayout
                    {
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        Spacing = 2
                    };

                    var image = new Image
                    {
                        Source = categoria.ImagenUrl,
                        HeightRequest = 40,
                        WidthRequest = 40,
                        HorizontalOptions = LayoutOptions.Center
                    };

                    var label = new Label
                    {
                        Text = categoria.Nombre,
                        TextColor = Colors.Black,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 12
                    };

                    stackLayout.Children.Add(image);
                    stackLayout.Children.Add(label);

                    var buttonFrame = new Frame
                    {
                        BorderColor = Color.FromArgb("#FFDAD9D9"),
                        BackgroundColor = categoria.Id == categoriaSeleccionadaId ? Color.FromArgb("#FFDAD9D9") : Colors.Transparent, 
                        CornerRadius = 20,
                        Padding = 0,
                        WidthRequest = 100,
                        HeightRequest = 100,
                        Content = stackLayout
                    };

                    buttonFrame.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() =>
                        {
                            SeleccionarCategoria(categoria.Id);
                        })
                    });

                    CategoriaStackLayout.Children.Add(buttonFrame);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
        private void SeleccionarCategoria(int categoriaId)
        {
            categoriaSeleccionadaId = categoriaId;
            LoadCategorias();
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            try{
                var titulo = Titulo.Text;
                var descripcion = Descripcion.Text;
                var tiempoPreparacion = TiempoPreparacion.Text;
                var selectedDificultad = Dificultad.SelectedItem as string;


                // Validar que los campos no estén vacíos
                if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descripcion) || string.IsNullOrWhiteSpace(tiempoPreparacion) || string.IsNullOrWhiteSpace(selectedDificultad))
                {
                    await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                    return;
                }

                var dificultad = "";
                var fechaPublicacion = DateTime.Now.ToString("yyyy-MM-dd");

                switch(selectedDificultad)
                {
                    case "Fácil":
                        dificultad = "facil";
                        break;
                    case "Media":
                        dificultad = "medio";
                        break;
                    case "Difícil":
                        dificultad = "dificil";
                        break;
                    default:
                        break;
                }

                var data = new
                {
                    titulo = titulo,
                    descripcion = descripcion,
                    usuario_id = userId,
                    categoria_id = categoriaSeleccionadaId,
                    dificultad = dificultad,
                    tiempo_preparacion = tiempoPreparacion,
                    fecha_publicacion = fechaPublicacion,
                    imagen = "todas.png"
                };

                if(isNewReceta){
                    var URL = ApiRoutes.ApiRoutes.BaseUrl +  ApiRoutes.ApiRoutes.Routes.RecetasIndex;;
                    var apiResponse = await _apiService.PostAsync<RecetaDetailResponse>(URL, data, true);

                    if (apiResponse.Status == 200 && apiResponse.Data != null)
                    {
                        var recetaDetails = apiResponse.Data.Recetas;
                        await DisplayAlert("Éxito", "Receta creada con éxito.", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error al crear receta.", "OK");
                    }
                } else{
                    var URL = ApiRoutes.ApiRoutes.BaseUrl +  ApiRoutes.ApiRoutes.Routes.RecetasIndex + "/" + idReceta;
                    var apiResponse = await _apiService.PutAsync<RecetaDetailResponse>(URL, data, true);

                    if (apiResponse.Status == 200 && apiResponse.Data != null)
                    {
                        var recetaDetails = apiResponse.Data.Recetas;
                        await DisplayAlert("Éxito", "Receta actualizada con éxito.", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error al crear receta.", "OK");
                    }
                }
            } catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                await DisplayAlert("Error", "Error al crear receta.", "OK");
            }
        }
        
       
    }
}
