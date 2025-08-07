using MauiEnterprisingsApp.Entidades;
using Newtonsoft.Json;
using System.Text;

namespace MauiEnterprisingsApp;

public partial class IngresarReporte : ContentPage
{
    string URL = "https://localhost:44381/";

    public IngresarReporte()
    {
        InitializeComponent();
    }

    private async void btnEnviarReporte_Clicked(object sender, EventArgs e)
    {
        try
        {
            ReqIngresarReporte req = new ReqIngresarReporte
            {
                reporte = new Reporte()
            };

            req.reporte.idUsuario = 5;
            req.reporte.descripcionReporte = txtReporteEditor.Text;

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            using HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(URL + "api/Reporte/IngresarReporte", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResIngresarReporte>(responseContent);

                if (res.resultado)
                {
                    await DisplayAlert("¡Éxito!", "El reporte hecho se ha realizado satisfactoriamente.", "Aceptar");
                    // Navegar a la vista del emprendedor
                    // Navigation.PushAsync(new VistaDelEmprendedor());
                }
                else
                {
                    await DisplayAlert("Error", "Ha ocurrido un error, vuelva a intentar. Si el problema persiste, intente más tarde.", "Aceptar");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error interno", "Error en la aplicación: " + ex.StackTrace, "Aceptar");
        }
    }
}