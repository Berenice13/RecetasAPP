using RecetasApp.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using RecetasApp.Services;

namespace RecetasApp.Views
{
    public partial class TopPage : ContentPage
    {
        public ObservableCollection<Receta> _recetas { get; set; }

        public List<Receta> RecetasFiltradas { get; set; }
        private readonly ApiService _apiService;

        private bool isSidebarVisible = false;
        
        public ObservableCollection<Receta> Recetas
        {
            get => _recetas;
            set
            {
                _recetas = value;
                OnPropertyChanged();
            }
        }

        public TopPage()
        {
            _recetas = new ObservableCollection<Receta>();
            RecetasFiltradas = new List<Receta>();
            _apiService = new ApiService();

            InitializeComponent();
            LoadRecetasTop();
            BindingContext = this;
        }
        
        private async void LoadRecetasTop()
        {
            try
            {
                var URL = ApiRoutes.ApiRoutes.BaseUrl + ApiRoutes.ApiRoutes.Routes.RecetasTop;
                var response = await _apiService.GetAsync<RecetaResponse>(URL, true);

                if (response != null && response.Status == 200 && response.Data != null)
                {
                    var recetas = response.Data.Recetas;
                    RecetasGrid.Children.Clear();
                    RecetasGrid.RowDefinitions.Clear();

                    int row = 0;
                    int count = 1;

                    foreach (var receta in recetas!)
                    {
                        RecetasGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        var horizontalLayout = new HorizontalStackLayout
                        {
                            Spacing = 10,
                            VerticalOptions = LayoutOptions.Center
                        };

                        var frameImg = new Frame
                        {
                            CornerRadius = 50, 
                            BackgroundColor = Colors.White,
                            HeightRequest = 100,
                            WidthRequest = 100,
                            Padding = 5, 
                            IsClippedToBounds = true,
                            HorizontalOptions = LayoutOptions.Start,
                            VerticalOptions = LayoutOptions.Center,
                            Content = new Image
                            {
                                Source = string.IsNullOrEmpty(receta.Imagen) ? "todas.png" : receta.Imagen,
                                HeightRequest = 100,
                                WidthRequest = 100,
                                Aspect = Aspect.AspectFill,
                                HorizontalOptions = LayoutOptions.Fill,
                                VerticalOptions = LayoutOptions.Fill
                            }
                        };


                        // Círculo con el número
                        var numberCircle = new Frame
                        {
                            WidthRequest = 40,
                            HeightRequest = 40,
                            CornerRadius = 20,
                            BackgroundColor = Colors.Black,
                            BorderColor = Colors.Black,
                            Padding = 0,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start,
                            Content = new Label
                            {
                                Text = count.ToString(),
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center,
                                FontSize = 18,
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Colors.White
                            }
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

                        var horizontalRatingLayout = new HorizontalStackLayout
                        {
                            Spacing = 5,
                            VerticalOptions = LayoutOptions.Center,
                            Children =
                            {
                                new Image
                                {
                                    Source = "llena.png",
                                    HeightRequest = 20,
                                    WidthRequest = 20,
                                    Aspect = Aspect.AspectFit,
                                    VerticalOptions = LayoutOptions.Center
                                },
                                new Label
                                {
                                    Text = receta.PuntuacionMedia.ToString(),
                                    FontSize = 15,
                                    TextColor = Colors.Gray
                                },
                            }
                        };


                        var button = new Button
                        {
                            Text = "Ver Receta",
                            BackgroundColor = Color.FromArgb("#84cbea"),
                            TextColor = Colors.White,
                            CornerRadius = 10,
                            WidthRequest = 150,
                        };

                        button.Clicked += async (sender, e) =>
                        {
                            await Navigation.PushAsync(new VerRecetaPage(receta));
                        };

                        verticalLayout.Children.Add(labelTitle);
                        verticalLayout.Children.Add(horizontalRatingLayout);
                        verticalLayout.Children.Add(button);

                        horizontalLayout.Children.Add(numberCircle);
                        horizontalLayout.Children.Add(frameImg);
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

                        RecetasGrid.Children.Add(cardFrame);
                        Grid.SetRow(cardFrame, row); 
                        Grid.SetColumn(cardFrame, 0); 
                        
                        row++;
                        count++;
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
