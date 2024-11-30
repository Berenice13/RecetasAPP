using RecetasApp.Models;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;

namespace RecetasApp.Views
{
    public partial class SearchPage : ContentPage
    {
        // Lista original de recetas
        public List<Receta> Recetas { get; set; }

        // Lista de recetas filtradas que se mostrarán
        public List<Receta> RecetasFiltradas { get; set; }

        public SearchPage()
        {
            InitializeComponent();

            // Cargar las recetas dinámicamente
        
            
            // Establecer el BindingContext para que el CollectionView pueda acceder a la lista de recetas filtradas
            BindingContext = this;
        }

        

        // Método para cargar las recetas
        private void LoadRecetas()
        {
            Recetas = new List<Receta>
            {
                new Receta
                {
                    Titulo = "Tarta de Manzana",
                    Descripcion = "Una deliciosa receta de postre.",
                    Imagen = "reposteria.png", 
                    TiempoPreparacion = "30",
                    Dificultad = "Fácil"
                },
                new Receta
                {
                    Titulo = "Sopa de Pollo",
                    Descripcion = "Receta clásica para el frío.",
                    Imagen = "reposteria.png",
                    TiempoPreparacion = "45",
                    Dificultad = "Media"
                },
                new Receta
                {
                    Titulo = "Ensalada César",
                    Descripcion = "Una ensalada fresca y ligera.",
                    Imagen = "reposteria.png",
                    TiempoPreparacion = "15",
                    Dificultad = "Fácil"
                }
            };

            // Inicializar la lista de recetas filtradas con todas las recetas al principio
            RecetasFiltradas = new List<Receta>(Recetas);
        }

     
    }
}
