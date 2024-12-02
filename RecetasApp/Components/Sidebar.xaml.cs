namespace RecetasApp.Components
{
    public partial class Sidebar : ContentView
    {
        public Sidebar()
        {
            InitializeComponent();
        }

        private void OnMisRecetasTapped(object sender, EventArgs e)
        {
            // Lógica para Mis Recetas
        }

        private void OnGuardadasTapped(object sender, EventArgs e)
        {
            // Lógica para Guardadas
        }

        private void OnCloseSidebarTapped(object sender, EventArgs e)
        {
            // Lógica para cerrar el sidebar
            this.IsVisible = false;
        }
    }
}
