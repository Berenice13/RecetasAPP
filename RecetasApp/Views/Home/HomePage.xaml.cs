using RecetasApp.Models;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using Microsoft.Maui.Layouts;
using RecetasApp.Services;

namespace RecetasApp.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly ApiService _apiService;
        private int categoriaSeleccionadaId = 1;

        public HomePage()
        {
            _apiService = new ApiService();
            InitializeComponent();
            LoadCategorias();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadRecetas();
        }


        private async void LoadCategorias()
        {
            try
            {
                var categorias = new List<Categoria>
                {
                    new Categoria { Id = 1, Nombre = "Todas", ImagenUrl = "todas.png" },
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
                        Spacing = 5
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
            LoadRecetas(categoriaId);
        }

        private async void LoadRecetas(int? categoriaId = null)
        {
            try
            {
                var URL = ApiRoutes.ApiRoutes.BaseUrl + ApiRoutes.ApiRoutes.Routes.RecetasIndex;
                var response = await _apiService.GetAsync<RecetaResponse>(URL, true);

                if (response != null && response.Status == 200 && response.Data != null)
                {
                    var recetas = response.Data.Recetas;
                    if (categoriaId.HasValue && categoriaId.Value != 1) 
                    {
                        recetas = recetas?.Where(r => r.CategoriaId == categoriaId.Value).ToList();
                    }

                    RecetasGrid.Children.Clear();
                    int row = 0, column = 0;

                    foreach (var receta in recetas!)
                    {
                        Debug.WriteLine("Receta: " + receta.Titulo);
                        Debug.WriteLine("Imagen: " + receta.Imagen);

                        var stackLayout = new StackLayout { Spacing = 6 };
                        var image = new Image
                        {
                            Source = string.IsNullOrEmpty(receta.Imagen) ? "todas.png" : receta.Imagen,
                            HeightRequest = 150,
                            Aspect = Aspect.AspectFill
                        };

                        var labelTitle = new Label
                        {
                            Text = receta.Titulo,
                            FontSize = 16,
                            TextColor = Colors.Black,
                            FontAttributes = FontAttributes.Bold
                        };

                        var button = new Button
                        {
                            Text = "Ver Receta",
                            BackgroundColor = Color.FromArgb("#84cbea"),
                            TextColor = Colors.White,
                            CornerRadius = 10
                        };

                        button.Clicked += async (sender, e) =>
                        {
                            // Navegar a la página de la receta pasando los datos
                            await Navigation.PushAsync(new VerRecetaPage(receta));
                        };

                        stackLayout.Children.Add(image);
                        stackLayout.Children.Add(labelTitle);
                        stackLayout.Children.Add(button);

                        var cardFrame = new Frame
                        {
                            CornerRadius = 10,
                            BorderColor = Color.FromArgb("#FFDAD9D9"),
                            Padding = 10,
                            Margin = new Thickness(4),
                            BackgroundColor = Colors.White,
                            Content = stackLayout
                        };

                        if (column > 1)
                        {
                            column = 0;
                            row++;
                            RecetasGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        }

                        RecetasGrid.Children.Add(cardFrame);
                        Grid.SetRow(cardFrame, row);
                        Grid.SetColumn(cardFrame, column);
                        column++;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    
    }
}
