using MauiEnterprisingsApp.Entidades;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;

namespace MauiEnterprisingsApp;

public partial class ObtenerReportes : ContentPage
{
    private List<Reporte> _listaReportes = new List<Reporte>();

    public List<Reporte> listaReportes
    {
        get { return _listaReportes; }
        set
        {
            _listaReportes = value;
            OnPropertyChanged(nameof(listaReportes));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public ObtenerReportes()
	{
		InitializeComponent();
        cargarReportes();
	}

    private async void cargarReportes()
    {
        listaReportes = await obtenerReportesApi();
        BindingContext = this;
    }

    public async Task<List<Reporte>> obtenerReportesApi()
    {
        List<Reporte> retornarReportes = new List<Reporte>();
        string laUrl = "https://localhost:44381/api/Reporte/obtenerReportes";

        try
        {
            ReqObtenerReportes req = new ReqObtenerReportes();

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(laUrl);

                if (response.IsSuccessStatusCode)
                {
                    //si conecto a back
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResObtenerReportes res = JsonConvert.DeserializeObject<ResObtenerReportes>(responseContent);
                    if (res.resultado)
                    {
                        retornarReportes = res.listaReportes;
                    }
                    else
                    {
                        Console.WriteLine("El backend retorno error");
                    }
                }
                else
                {
                    Console.WriteLine("Error conectando al back");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error grave: " + ex.ToString());
        }

        return retornarReportes;
    }
}