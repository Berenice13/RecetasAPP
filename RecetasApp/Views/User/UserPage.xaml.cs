using System.Collections.ObjectModel;
using System.Diagnostics;
using RecetasApp.Models;
using RecetasApp.Services;

namespace RecetasApp.Views
{
    public partial class UserPage : ContentPage
    {
        public ObservableCollection<Receta> _recetas { get; set; }

        public List<Receta> RecetasFiltradas { get; set; }

        public ObservableCollection<Receta> _recetasFav { get; set; }

        public List<Receta> RecetasFiltradasFav { get; set; }
        private readonly ApiService _apiService;

        private bool isSidebarVisible = false;

        private int userId;
        
        public ObservableCollection<Receta> Recetas
        {
            get => _recetas;
            set
            {
                _recetas = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Receta> RecetasFav
        {
            get => _recetasFav;
            set
            {
                _recetasFav = value;
                OnPropertyChanged();
            }
        }

        private bool _isMisRecetasVisible;
        private bool _isFavoritasVisible;

        public bool IsMisRecetasVisible
        {
            get => _isMisRecetasVisible;
            set
            {
                _isMisRecetasVisible = value;
                OnPropertyChanged(nameof(IsMisRecetasVisible));
            }
        }

        public bool IsFavoritasVisible
        {
            get => _isFavoritasVisible;
            set
            {
                _isFavoritasVisible = value;
                OnPropertyChanged(nameof(IsFavoritasVisible));
            }
        }

        public UserPage()
        {
            userId = 0;
            _recetas = new ObservableCollection<Receta>();
            RecetasFiltradas = new List<Receta>();
            _recetasFav = new ObservableCollection<Receta>();
            RecetasFiltradasFav = new List<Receta>();
            _apiService = new ApiService();

            InitializeComponent();
       
            BindingContext = this;
            IsMisRecetasVisible = true;
            IsFavoritasVisible = false;
            CantRecetas.Text = "0";
            CantFav.Text = "0";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadRecetas();
            LoadRecetasFavoritas();
            LoadInfoUser();
        }

        private void OnMisRecetasBtnClicked(object sender, EventArgs e)
        {
            IsMisRecetasVisible = true;
            IsFavoritasVisible = false;
        }

        private void OnFavoritasBtnClicked(object sender, EventArgs e)
        {
            IsMisRecetasVisible = false;
            IsFavoritasVisible = true;
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
                    UserNombre.Text = datosUser!.Nombre;
                    UserCorreo.Text = datosUser!.Email;
                    NombreSide.Text = datosUser!.Nombre;
                    userId = datosUser!.Id;
                    Debug.WriteLine("ID: " + userId);

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }

        private async void LoadRecetas()
        {
            try
            {
                var URL = ApiRoutes.ApiRoutes.BaseUrl + ApiRoutes.ApiRoutes.Routes.RecetasUser;
                var response = await _apiService.GetAsync<RecetaResponse>(URL, true);

                if (response != null && response.Status == 200 && response.Data != null)
                {
                    var recetas = response.Data.Recetas;

                    RecetasGrid.Children.Clear();
                    RecetasGrid.RowDefinitions.Clear(); // Limpiar las filas existentes

                    int row = 0;

                    CantRecetas.Text = recetas!.Count.ToString();

                    foreach (var receta in recetas)
                    {
                        RecetasGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                        // Crear el layout horizontal
                        var horizontalLayout = new HorizontalStackLayout
                        {
                            Spacing = 10,
                            VerticalOptions = LayoutOptions.Center
                        };

                        // Imagen a la izquierda
                        var image = new Image
                        {
                            Source = string.IsNullOrEmpty(receta.Imagen) ? "todas.png" : receta.Imagen,
                            HeightRequest = 100,
                            WidthRequest = 100,
                            Aspect = Aspect.AspectFill,
                            HorizontalOptions = LayoutOptions.Start
                        };

                        // Contenido de texto a la derecha
                        var verticalLayout = new VerticalStackLayout
                        {
                            Spacing = 5,
                            VerticalOptions = LayoutOptions.Center
                        };

                        var labelTitle = new Label
                        {
                            Text = receta.Titulo,
                            FontSize = 16,
                            TextColor = Colors.Black,
                            FontAttributes = FontAttributes.Bold
                        };

                        var labelDetails = new Label
                        {
                            Text = $"{receta.TiempoPreparacion} minutos | Dificultad: {receta.Dificultad}",
                            FontSize = 14,
                            TextColor = Colors.Gray
                        };

                        // Botón Ver Receta
                        var verRecetaButton = new Button
                        {
                            Text = "Ver Receta",
                            BackgroundColor = Color.FromArgb("#84cbea"),
                            TextColor = Colors.White,
                            CornerRadius = 10,
                            WidthRequest = 100,
                        };

                        verRecetaButton.Clicked += async (sender, e) =>
                        {
                            await Navigation.PushAsync(new VerRecetaPage(receta));
                        };

                        // Botón Editar Receta
                        var editarRecetaButton = new Button
                        {
                            Text = "Editar Receta",
                            BackgroundColor = Color.FromArgb("#f0ad4e"),
                            TextColor = Colors.White,
                            CornerRadius = 10,
                            WidthRequest = 100,
                        };

                        editarRecetaButton.Clicked += async (sender, e) =>
                        {
                            // Navegar a la página de edición pasando la receta seleccionada
                            await Navigation.PushAsync(new CreateRecetaPage(userId, receta));
                        };

                        // Layout para contener ambos botones
                        var buttonsLayout = new HorizontalStackLayout
                        {
                            Spacing = 10,
                            VerticalOptions = LayoutOptions.Center
                        };

                        buttonsLayout.Children.Add(verRecetaButton);
                        buttonsLayout.Children.Add(editarRecetaButton);

                        verticalLayout.Children.Add(labelTitle);
                        verticalLayout.Children.Add(labelDetails);
                        verticalLayout.Children.Add(buttonsLayout);

                        // Agregar la imagen y el contenido al layout horizontal
                        horizontalLayout.Children.Add(image);
                        horizontalLayout.Children.Add(verticalLayout);

                        // Crear el Frame que contendrá el layout horizontal
                        var cardFrame = new Frame
                        {
                            CornerRadius = 10,
                            BorderColor = Color.FromArgb("#FFDAD9D9"),
                            Padding = 10,
                            Margin = new Thickness(4),
                            BackgroundColor = Colors.White,
                            Content = horizontalLayout
                        };

                        RecetasGrid.Children.Add(cardFrame);
                        Grid.SetRow(cardFrame, row);
                        Grid.SetColumn(cardFrame, 0);
                        row++;
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }

        private async void LoadRecetasFavoritas()
        {
            try
            {
                var URL = ApiRoutes.ApiRoutes.BaseUrl + ApiRoutes.ApiRoutes.Routes.RecetasFavoritas;
                var response = await _apiService.GetAsync<RecetaResponse>(URL, true);

                if (response != null && response.Status == 200 && response.Data != null)
                {
                    var recetas = response.Data.Recetas;
                    RecetasGuardadasGrid.Children.Clear();
                    RecetasGuardadasGrid.RowDefinitions.Clear();
                    int row = 0;
                    CantFav.Text = recetas!.Count.ToString();

                    foreach (var receta in recetas)
                    {
                        RecetasGuardadasGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        var horizontalLayout = new HorizontalStackLayout
                        {
                            Spacing = 10,
                            VerticalOptions = LayoutOptions.Center
                        };

                        var image = new Image
                        {
                            Source = string.IsNullOrEmpty(receta.Imagen) ? "todas.png" : receta.Imagen,
                            HeightRequest = 100,
                            WidthRequest = 100,
                            Aspect = Aspect.AspectFill,
                            HorizontalOptions = LayoutOptions.Start
                        };

                        var verticalLayout = new VerticalStackLayout
                        {
                            Spacing = 5,
                            VerticalOptions = LayoutOptions.Center
                        };

                        var labelTitle = new Label
                        {
                            Text = receta.Titulo,
                            FontSize = 16,
                            TextColor = Colors.Black,
                            FontAttributes = FontAttributes.Bold
                        };

                        var labelDetails = new Label
                        {
                            Text = $"{receta.TiempoPreparacion} minutos | Dificultad: {receta.Dificultad}",
                            FontSize = 14,
                            TextColor = Colors.Gray
                        };

                        var button = new Button
                        {
                            Text = "Ver Receta",
                            BackgroundColor = Color.FromArgb("#84cbea"),
                            TextColor = Colors.White,
                            CornerRadius = 10,
                            WidthRequest = 200,
                        };

                        button.Clicked += async (sender, e) =>
                        {
                            await Navigation.PushAsync(new VerRecetaPage(receta));
                        };

                        verticalLayout.Children.Add(labelTitle);
                        verticalLayout.Children.Add(labelDetails);
                        verticalLayout.Children.Add(button);

                        // Agregar la imagen y el contenido al layout horizontal
                        horizontalLayout.Children.Add(image);
                        horizontalLayout.Children.Add(verticalLayout);

                        var cardFrame = new Frame
                        {
                            CornerRadius = 10,
                            BorderColor = Color.FromArgb("#FFDAD9D9"),
                            Padding = 10,
                            Margin = new Thickness(4),
                            BackgroundColor = Colors.White,
                            Content = horizontalLayout
                        };

                        RecetasGuardadasGrid.Children.Add(cardFrame);
                        Grid.SetRow(cardFrame, row);
                        Grid.SetColumn(cardFrame, 0); 
                        row++; 
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }

        private async void OnRecetaTapped(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine(sender.GetType().ToString());
                var receta = ((TapGestureRecognizer)sender).CommandParameter as Receta;

                // Agregar depuración
                Debug.WriteLine("Tipo de CommandParameter: " + receta?.GetType().ToString());

                if (receta != null)
                {
                    await Navigation.PushAsync(new VerRecetaPage(receta));
                }
                else
                {
                    Debug.WriteLine("Error: El CommandParameter no es una instancia de Receta.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en OnRecetaTapped: " + ex.Message);
            }
        }

        private async void OnMenuTapped(object sender, EventArgs e)
        {
            if (isSidebarVisible)
                await HideSidebar();
            else
                await ShowSidebar();
        }

        private async void OnCloseSidebarTapped(object sender, EventArgs e)
        {
            await HideSidebar();
        }

        private async Task ShowSidebar()
        {
            isSidebarVisible = true;
            Overlay.IsVisible = true; // Mostrar el overlay
            await Sidebar.TranslateTo(0, 0, 250, Easing.CubicInOut);
        }

        private async Task HideSidebar()
        {
            isSidebarVisible = false;
            Overlay.IsVisible = false; // Ocultar el overlay
            await Sidebar.TranslateTo(300, 0, 250, Easing.CubicInOut);
        }

        private async void OnEditarPerfilTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditUserPage());
        }

        private void OnFavoritasTapped(object sender, EventArgs e)
        {
            Console.WriteLine("Navegar a Favoritas");
        }

        private async void OnCerrarSesionClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Cerrar sesión", "¿Seguro que deseas cerrar sesión?", "Sí", "No");

            if (answer)
            {
                Preferences.Remove("AuthToken");
                if (Application.Current != null)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
            }
        }

        private async void OnCreateClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateRecetaPage(userId, null));
        }




    }
}
