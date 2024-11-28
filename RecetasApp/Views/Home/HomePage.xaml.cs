using RecetasApp.Models;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using Microsoft.Maui.Layouts;

namespace RecetasApp.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            LoadCategorias();
            LoadRecetas();
        }

        private void LoadCategorias()
        {
            try
            {
                // Crear la lista de categorías directamente en el código
                var categorias = new List<Categoria>
                {
                    new Categoria { Id = 1, Nombre = "Todas", ImagenUrl = "todas.png" },
                    new Categoria { Id = 2, Nombre = "Cocina", ImagenUrl = "cocina.png" },
                    new Categoria { Id = 3, Nombre = "Repostería", ImagenUrl = "reposteria.png" },
                    new Categoria { Id = 4, Nombre = "Cócteles", ImagenUrl = "cocteles.png" },
                    new Categoria { Id = 5, Nombre = "Salsas", ImagenUrl = "saludable.png" }
                };

                // Crear dinámicamente los botones para cada categoría
                foreach (var categoria in categorias)
                {
                    // Crear el StackLayout para el contenido del botón
                    var stackLayout = new StackLayout
                    {
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        Spacing = 5
                    };

                    // Crear la imagen
                    var image = new Image
                    {
                        Source = categoria.ImagenUrl,  // Ruta de la imagen
                        HeightRequest = 40,            // Ajusta el tamaño de la imagen
                        WidthRequest = 40,
                        HorizontalOptions = LayoutOptions.Center
                    };

                    // Crear el texto
                    var label = new Label
                    {
                        Text = categoria.Nombre,
                        TextColor = Colors.Black,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 12
                    };

                    // Agregar la imagen y el texto al StackLayout
                    stackLayout.Children.Add(image);  // Agregar la imagen
                    stackLayout.Children.Add(label);  // Agregar el texto

                    // Crear el Frame (como contenedor para el botón)
                    var buttonFrame = new Frame
                    {
                        BorderColor = Color.FromArgb("#FFDAD9D9"),
                        BackgroundColor = Colors.Transparent,
                        CornerRadius = 20,
                        Padding = 0,  // Quitar padding extra para no modificar el tamaño
                        WidthRequest = 100,  // Asegura un ancho fijo
                        HeightRequest = 100, // Asegura una altura fija
                        Content = stackLayout  // Asignamos el StackLayout como contenido
                    };

                    // Agregar la acción del botón
                    buttonFrame.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() =>
                        {
                            DisplayAlert("Categoría seleccionada", categoria.Nombre, "OK");
                        })
                    });

                    // Agregar el Frame al layout
                    CategoriaStackLayout.Children.Add(buttonFrame);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void LoadRecetas()
        {
            try
            {
                // Simular datos de recetas como si vinieran de una API o base de datos
                var recetas = new List<Receta>
                {
                    new Receta { Titulo = "Tarta de manzana", Imagen = "todas.png" },
                    new Receta { Titulo = "Sopa de pollo", Imagen = "todas.png" },
                    new Receta { Titulo = "Ensalada César", Imagen = "todas.png" },
                    new Receta { Titulo = "Mousse de chocolate", Imagen = "todas.png" }
                };

                // Variables para manejar las filas y columnas
                int row = 0;
                int column = 0;

                // Crear dinámicamente las cards para cada receta
                foreach (var receta in recetas)
                {
                    // Crear el StackLayout para la card
                    var stackLayout = new StackLayout
                    {
                        Spacing = 6
                    };

                    // Crear la imagen
                    var image = new Image
                    {
                        Source = receta.Imagen,  // Ruta de la imagen
                        HeightRequest = 150,     // Ajusta el tamaño de la imagen
                        Aspect = Aspect.AspectFill
                    };

                    // Crear el título
                    var labelTitle = new Label
                    {
                        Text = receta.Titulo,
                        FontSize = 16,
                        TextColor = Colors.Black,
                        FontAttributes = FontAttributes.Bold
                    };

                    // Crear el botón
                    var button = new Button
                    {
                        Text = "Ver Receta",
                        BackgroundColor = Color.FromArgb("#84cbea"),
                        TextColor = Colors.White,
                        CornerRadius = 10
                    };

                    // Agregar los elementos al StackLayout
                    stackLayout.Children.Add(image);
                    stackLayout.Children.Add(labelTitle);
                    stackLayout.Children.Add(button);

                    // Crear el Frame que contiene la card
                    var cardFrame = new Frame
                    {
                        CornerRadius = 10,
                        BorderColor = Color.FromArgb("#FFDAD9D9"),
                        Padding = 10,
                        BackgroundColor = Colors.White,
                        Content = stackLayout
                    };

                    // Si es necesario agregar una nueva fila al Grid
                    if (column > 1)
                    {
                        column = 0;
                        row++;
                        RecetasGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    }

                    // Agregar la card al Grid en la posición correcta
                    RecetasGrid.Children.Add(cardFrame);
                    Grid.SetRow(cardFrame, row);
                    Grid.SetColumn(cardFrame, column);

                    // Avanzar al siguiente columna
                    column++;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}
