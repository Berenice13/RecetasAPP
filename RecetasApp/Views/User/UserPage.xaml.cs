using System.Windows.Input;
using RecetasApp.Models;

namespace RecetasApp.Views
{
    public partial class UserPage : ContentPage
    {
        public List<Receta> Recetas { get; set; }
        public List<Receta> RecetasFiltradas { get; set; }

        
        public UserPage()
        {
            Recetas = new List<Receta>();
            RecetasFiltradas = new List<Receta>();

            // Vinculamos los comandos al BindingContext
            InitializeComponent();
            LoadRecetas();
            BindingContext = this;
        }

        private void LoadRecetas()
        {
            try
            {
                Recetas = new List<Receta>
                {
                    new Receta
                    {
                        Titulo = "Tarta de Manzana",
                        Descripcion = "Una deliciosa receta de postre.",
                        Imagen = "reposteria.png", 
                        TiempoPreparacion = 30,
                        Dificultad = "Fácil"
                    },
                    new Receta
                    {
                        Titulo = "Sopa de Pollo",
                        Descripcion = "Receta clásica para el frío.",
                        Imagen = "reposteria.png",
                        TiempoPreparacion = 45,
                        Dificultad = "Media"
                    },
                    new Receta
                    {
                        Titulo = "Ensalada César",
                        Descripcion = "Una ensalada fresca y ligera.",
                        Imagen = "reposteria.png",
                        TiempoPreparacion = 15,
                        Dificultad = "Fácil"
                    }
                };

                // Establecer RecetasFiltradas a las recetas cargadas
                RecetasFiltradas = new List<Receta>(Recetas);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}
