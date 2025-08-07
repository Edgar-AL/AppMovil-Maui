using MauiEnterprisingsApp.Entidades;
using MauiEnterprisingsApp.Utilitarios;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;

namespace MauiEnterprisingsApp;

public partial class ObtenerHistorialPedidosPorEmprendedor : ContentPage
{
    private List<HistorialPedido> _listaHistorialPedidosPorEmprendedor = new List<HistorialPedido>();

    public List<HistorialPedido> listaHistorialPedidosPorEmprendedor
    {
        get { return _listaHistorialPedidosPorEmprendedor; }
        set
        {
            _listaHistorialPedidosPorEmprendedor = value;
            OnPropertyChanged(nameof(listaHistorialPedidosPorEmprendedor));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ObtenerHistorialPedidosPorEmprendedor()
	{
		InitializeComponent();
        cargarHistorialPedidosPorEmprendedor();

    }

    private async void cargarHistorialPedidosPorEmprendedor()
    {
        listaHistorialPedidosPorEmprendedor = await obtenerHistorialPedidosPorEmprendedorApi();
        BindingContext = this;
    }

    public async Task<List<HistorialPedido>> obtenerHistorialPedidosPorEmprendedorApi()
    {
        List<HistorialPedido> retornarHistorial = new List<HistorialPedido>();
        string laUrl = "https://localhost:44381/api/HistorialPedidos/historialPedidosPorEmprendedor";

        try
        {
            ReqObtenerHistorialPedidosPorEmprendedor req = new ReqObtenerHistorialPedidosPorEmprendedor();
            //req.idEmprendedor = (int)Sesion.usuarioSesion.id;
            req.idEmprendedor = 5;

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(laUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    //si conecto a back
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ResObtenerHistorialPedidosPorEmprendedor res = JsonConvert.DeserializeObject<ResObtenerHistorialPedidosPorEmprendedor>(responseContent);
                    if (res.resultado)
                    {
                        retornarHistorial = res.listaHistorialPedidosPorEmprendedor;
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