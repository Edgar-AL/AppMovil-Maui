using MauiEnterprisingsApp.Entidades;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;

namespace MauiEnterprisingsApp;

public partial class ObtenerHistorialPedidos : ContentPage
{
    private List<HistorialPedido> _listaHistorialPedidos = new List<HistorialPedido>();

    public List<HistorialPedido> listaHistorialPedidos
    {
        get { return _listaHistorialPedidos; }
        set
        {
            _listaHistorialPedidos = value;
            OnPropertyChanged(nameof(listaHistorialPedidos));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public ObtenerHistorialPedidos()
	{
		InitializeComponent();
        cargarHistorialPedidos();

    }

    private async void cargarHistorialPedidos()
    {
        listaHistorialPedidos = await obtenerHistorialPedidosApi();
        BindingContext = this;
    }

    public async Task<List<HistorialPedido>> obtenerHistorialPedidosApi()
    {
        List<HistorialPedido> retornarHistorial = new List<HistorialPedido>();
        string laUrl = "https://localhost:44381/api/HistorialPedidos/obtenerHistorialPedidos";

        try
        {
            ReqObtenerHistorialPedidos req = new ReqObtenerHistorialPedidos();

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(laUrl);

                if (response.IsSuccessStatusCode)
                {
                    //si conecto a back
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResObtenerHistorialPedidos res = JsonConvert.DeserializeObject<ResObtenerHistorialPedidos>(responseContent);
                    if (res.resultado)
                    {
                        retornarHistorial = res.listaHistorialPedidos;
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

        return retornarHistorial;
    }
}