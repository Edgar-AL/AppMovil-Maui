using TiendaVideoJuegos;

namespace MauiEnterprisingsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new IngresarReporte());
        }
    }
}
