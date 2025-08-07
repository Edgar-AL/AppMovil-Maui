using MauiEnterprisingsApp.Entidades.Request;
using MauiEnterprisingsApp.Entidades.Response;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using TiendaVideoJuegos;

namespace MauiEnterprisingsApp;

public partial class OlvidoContraseniaView : ContentPage
{
    string URL = "https://localhost:44381/";
    public OlvidoContraseniaView()
	{
		InitializeComponent();
	}

    private async void btnSolicitarCodigo_Clicked(object sender, EventArgs e)
    {
        try
        {
            ReqActualizarCodigoVerificacion req = new ReqActualizarCodigoVerificacion
            {
                correo = txtCorreo.Text,
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(URL + "api/Usuario/CodigoActivacion", jsonContent);

            if (response.IsSuccessStatusCode)
            {
              
                 await DisplayAlert("C�digo enviado", "Se ha enviado un c�digo de verificaci�n a tu correo.", "Aceptar");
                contenedorCamposAdicionales.IsVisible = true;
            }
            else
            {
                await DisplayAlert("Error", "No se pudo solicitar un nuevo c�digo de activaci�n. Intente nuevamente m�s tarde.", "Aceptar");
            }

          
        }catch (Exception ex) {

            await DisplayAlert("Error interno", "Error en la aplicaci�n: " + ex.StackTrace, "Aceptar");
        }


       
    }

    private void OnCodeTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        if (entry.Text.Length == 1)
        {
            switch (entry.StyleId)
            {
                case "code1":
                    code2.Focus();
                    break;
                case "code2":
                    code3.Focus();
                    break;
                case "code3":
                    code4.Focus();
                    break;
                case "code4":
                    code5.Focus();
                    break;
                case "code5":
                    txtNuevaContrasena.Focus();
                    break;
            }
        }
    }

    private async void btnModificarContrasena_Clicked(object sender, EventArgs e)
    {
        try
        {

            string codigoVerificacion = $"{code1.Text}{code2.Text}{code3.Text}{code4.Text}{code5.Text}";


            ReqActualizarContrase�aOlvidada req = new ReqActualizarContrase�aOlvidada();

            req.correo = txtCorreo.Text;
            req.numeroVerificacion = codigoVerificacion;
            req.contrasenia = txtNuevaContrasena.Text;

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(URL + "api/Usuario/ActualizarContraseniaOlvido", jsonContent);

            if (response.IsSuccessStatusCode)
            {

                var responseContent = await response.Content.ReadAsStringAsync();
                ResActualizarContrase�aOlvidada res = JsonConvert.DeserializeObject<ResActualizarContrase�aOlvidada>(responseContent);

                if (res.resultado)
                {
                    await DisplayAlert("�xito", "Tu contrase�a ha sido modificada exitosamente.", "Aceptar");
                    await Navigation.PushAsync(new LoginView());

                }else if(res.listaDeErrores.Contains("El correo o el n�mero de verificaci�n especificados no coinciden con ning�n registro en la base de datos."))
                {
                    await DisplayAlert("Error", "El correo o el n�mero de verificaci�n especificados no coinciden con ning�n registro en la base de datos.", "Aceptar");
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo modificar la contrase�a. Verifica el c�digo e intenta nuevamente.", "Aceptar");
                }

            }

           
        }catch (Exception ex) {

            await DisplayAlert("Error interno", "Error en la aplicaci�n: " + ex.StackTrace, "Aceptar");
        }

    }
}