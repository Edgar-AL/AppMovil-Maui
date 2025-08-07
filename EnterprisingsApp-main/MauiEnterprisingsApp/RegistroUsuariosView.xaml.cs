
using MauiEnterprisingsApp;
using MauiEnterprisingsApp.Entidades.Entidades;
using MauiEnterprisingsApp.Entidades.Request;
using MauiEnterprisingsApp.Entidades.Response;
using MauiEnterprisingsApp.Utilitarios;
using Newtonsoft.Json;
using System.Text;

namespace FrontEnterprisingApp;

public partial class RegistroUsuariosView : ContentPage
{
    string URL = "https://localhost:44381/";

    public RegistroUsuariosView()
	{
		InitializeComponent();
	}


    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox)
        {
            if (checkBox.IsChecked)
            {
                if (checkBox == chkAdmin)
                {
                    chkUsuario.IsChecked = false;
                }
                else if (checkBox == chkUsuario)
                {
                    chkAdmin.IsChecked = false;
                }
            }
        }
    }

    private async void btnRegistrar_Usuarios_clicked(object sender, EventArgs e)
    {
        try
        {
            ReqRegistrarUsuario req = new ReqRegistrarUsuario
            {
                usuario = new Usuario()
            };

            if (chkAdmin.IsChecked)
            {
                req.usuario.TipoRol = "Admin";
            }
            else if (chkUsuario.IsChecked)
            {
                req.usuario.TipoRol = "Usuario";
            }

            req.usuario.Nombre = txtNombre.Text;
            req.usuario.Apellido = txtApellido.Text;
            req.usuario.direccion = txtDireccion.Text;
            req.usuario.telefono = txtTelefono.Text;
            req.usuario.correo = txtCorreo.Text;
            req.usuario.contrasenia = txtContraseña.Text;

            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            using HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(URL + "api/Usuario/Registro", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ResRegistrarUsuario>(responseContent);

                if (res.resultado)
                {
                    await DisplayAlert("¡Éxito!", "Registrado correctamente, se ha enviado un código de activación a su correo", "Aceptar");
                    Navigation.PushAsync(new ActivacionCuentaView());
                }
                else if (res.listaDeErrores.Contains("ERROR DESDE BD: CORREO YA REGISTRADO"))
                {
                    await DisplayAlert("Error", "El correo ingresado ya se encuentra registrado", "Aceptar");
                }
                else
                {
                    await DisplayAlert("Error", "ha ocurrido un error, vuelva a intentar, si el problema persiste intente mas tarde", "Aceptar");
                }
            }
        }

        catch (Exception ex)
        {
            await DisplayAlert("Error interno", "Error en la aplicación: " + ex.StackTrace, "Aceptar");
        }
    }
}