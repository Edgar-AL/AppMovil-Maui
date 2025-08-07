
using MauiEnterprisingsApp.Entidades.Request;
using MauiEnterprisingsApp.Entidades.Response;
using Newtonsoft.Json;
using System.Text;
using TiendaVideoJuegos;


namespace MauiEnterprisingsApp;

public partial class ActivacionCuentaView : ContentPage
{
    string URL = "https://localhost:44381/";
    public ActivacionCuentaView()
	{
		InitializeComponent();
	}


    private async void btnActivarCuenta_Clicked(object sender, EventArgs e)
    {
        await btnActivarCuenta_ClickedAsync(sender, e);
    }

    private async Task btnActivarCuenta_ClickedAsync(object sender, EventArgs e)
    {
        try
        {
            StringBuilder codigoActivacion = new StringBuilder();

            string digito1 = entry1.Text;
            string digito2 = entry2.Text;
            string digito3 = entry3.Text;
            string digito4 = entry4.Text;
            string digito5 = entry5.Text;

            codigoActivacion.Append(digito1);
            codigoActivacion.Append(digito2);
            codigoActivacion.Append(digito3);
            codigoActivacion.Append(digito4);
            codigoActivacion.Append(digito5);

            string codigoActivacionCompleto = codigoActivacion.ToString();

            ReqActivacionCuenta req = new ReqActivacionCuenta
            {
                correo = txtCorreoElectronico.Text,
                numeroVerificacion = codigoActivacionCompleto
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(URL + "api/Usuario/ActivacionCuenta", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResActivacionCuenta res = JsonConvert.DeserializeObject<ResActivacionCuenta>(responseContent);

                if (res.resultado)
                {
                    await DisplayAlert("¡Éxito!", "Su cuenta ha sido activada con éxito", "Aceptar");
                    await Navigation.PushAsync(new LoginView());
                }
                else if (res.listaDeErrores.Contains("El código de verificación ha expirado."))
                {
                    bool retry = await DisplayAlert("Código Expirado!", "Su código de activación ha expirado. ¿Desea solicitar un nuevo código?", "Sí", "No");
                    if (retry)
                    {
                        await SolicitarNuevoCodigoAsync(req.correo);
                    }
                }
                else if (res.listaDeErrores.Contains("El usuario ya está verificado o no existe."))
                {
                    await DisplayAlert("¡Código inválido!", "El usuario ya está verificado o el código es incorrecto.", "Aceptar");
                }
                else
                {
                    await DisplayAlert("¡ERROR!", "Su cuenta no ha sido activada", "Aceptar");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error interno", "Error en la aplicación: " + ex.StackTrace, "Aceptar");
        }
    }

    private async Task SolicitarNuevoCodigoAsync(string correo)
    {
        try
        {
            
            var ReqActualizarCodigoVerificacion = new { correo = correo };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(ReqActualizarCodigoVerificacion), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(URL + "api/Usuario/CodigoActivacion", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Nuevo Código Solicitado", "Se ha enviado un nuevo código de activación a su correo electrónico.", "Aceptar");
                LimpiarEntries();
                await Navigation.PushAsync(new LoginView());
            }
            else
            {
                await DisplayAlert("Error", "No se pudo solicitar un nuevo código de activación. Intente nuevamente más tarde.", "Aceptar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error interno", "Error en la aplicación: " + ex.StackTrace, "Aceptar");
        }
    }

    private void LimpiarEntries()
    {
        entry1.Text = string.Empty;
        entry2.Text = string.Empty;
        entry3.Text = string.Empty;
        entry4.Text = string.Empty;
        entry5.Text = string.Empty;
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        if (entry == null || entry.Text.Length < 1)
            return;

        if (entry == entry1)
            entry2.Focus();
        else if (entry == entry2)
            entry3.Focus();
        else if (entry == entry3)
            entry4.Focus();
        else if (entry == entry4)
            entry5.Focus();
      
    }



}


