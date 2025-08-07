using FrontEnterprisingApp;
using MauiEnterprisingsApp;
using MauiEnterprisingsApp.Entidades.Request;
using MauiEnterprisingsApp.Entidades.Response;
using MauiEnterprisingsApp.Utilitarios;
using Newtonsoft.Json;
using System.Text;


namespace TiendaVideoJuegos;
public partial class LoginView : ContentPage
{

    String laURL = "https://localhost:44381/";

    public LoginView()
    {
        InitializeComponent();
    }

    private async void btnIngresar_Clicked(object sender, EventArgs e)
    {
        try
        {
            ReqIniciarSesion req = new ReqIniciarSesion();

            req.correo = txtCorreo.Text;
            req.contrasena = txtPassword.Text;

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(laURL + "api/Usuario/IniciarSesion", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResIniciarSesion res = new ResIniciarSesion();

                res = JsonConvert.DeserializeObject<ResIniciarSesion>(responseContent);

                if (res.resultado)
                {
                    Sesion.usuarioSesion.id = res.Usuario.id;
                    Sesion.usuarioSesion.TipoRol = res.Usuario.TipoRol;
                    Sesion.usuarioSesion.correo = res.Usuario.correo;
                    Sesion.usuarioSesion.Nombre = res.Usuario.Nombre;
                    Sesion.usuarioSesion.Apellido = res.Usuario.Apellido;

                    await DisplayAlert("Benvenido", "Disfruta de nuestra app", "Aceptar");
                    Navigation.PushAsync(new ObtenerHistorialPedidosPorEmprendedor());

                }
                else if (res.listaDeErrores.Contains("Tu cuenta no está activa. Por favor, contacta al administrador."))
                {
                    await DisplayAlert("Active su cuenta", "! Tu cuenta aun no se encuentra activa ¡", "Aceptar");
                    Navigation.PushAsync(new ActivacionCuentaView());
                }else if(res.listaDeErrores.Contains("No se encontró ningún usuario con el correo especificado."))
                {
                    await DisplayAlert("Cuenta no registrada", "No se encontró ningún usuario con el correo especificado.", "Aceptar");
                }
                else
                {
                    await DisplayAlert("Incorrecto", "¡Usuario o contraseña incorrecto!", "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("No se encontró el backend", "Error en la conexión con el EndPoint", "Aceptar");
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error interno", "Error en la aplicación: " + ex.StackTrace.ToString(), "Aceptar");
        }
    }

    private void btnActivarCuenta_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ActivacionCuentaView());


    }

    private void btnRegistrarse_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegistroUsuariosView());


    }
    private async void btnOlvideContrasena_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OlvidoContraseniaView());
    
    }
}