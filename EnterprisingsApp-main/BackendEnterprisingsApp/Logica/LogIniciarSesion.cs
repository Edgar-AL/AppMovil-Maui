using BackendEnterprisingsApp.AccesoDatos;
using BackendEnterprisingsApp.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp.Logica
{
    public class LogIniciarSesion
    {

        public ResIniciarSesion IniciarSesion(ReqIniciarSesion req)
        {
            ResIniciarSesion res = new ResIniciarSesion();
            LogEncriptacion encrip = new LogEncriptacion();

            try
            {
                int? activo = ObtenerEstadoCuenta(req, res);
                if (activo == null || activo == 0)
                    return res;

                string contrasenaEncriptada = ObtenerContrasenaEncriptada(req, res);
                if (contrasenaEncriptada == null)
                    return res;

                if (!ValidarContrasena(req, contrasenaEncriptada, res))
                    return res;

                ObtenerDetallesUsuario(req, res);
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Error interno: " + ex.Message);
            }
            finally
            {
                CrearBitacora(req, res);
            }

            return res;
        }

        private int? ObtenerEstadoCuenta(ReqIniciarSesion req, ResIniciarSesion res)
        {
            int? activo = 0;
            int? errorId = 0;
            string errorDescripcion = "";
            using (var Linq = new conexionbdDataContext())
            {
                Linq.SP_OBTENER_ACTIVO_POR_CORREO(req.correo, ref activo, ref errorId, ref errorDescripcion);
            }

            if (errorId != 0)
            {
                res.resultado = false;
                res.listaDeErrores.Add(errorDescripcion);
                return null;
            }

            if (activo == 0)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Tu cuenta no está activa. Por favor, contacta al administrador.");
                return 0;
            }
            

            return activo;
        }

        private string ObtenerContrasenaEncriptada(ReqIniciarSesion req, ResIniciarSesion res)
        {
            string contrasenaEncriptada = "";
            int? errorId = 0;
            string errorDescripcion = "";
            using (var Linq = new conexionbdDataContext())
            {
                Linq.SP_VERIFICAR_CORREO_Y_OBTENER_CONTRASENA(req.correo, ref contrasenaEncriptada, ref errorId, ref errorDescripcion);
            }

            if (errorId != 0)
            {
                res.resultado = false;
                res.listaDeErrores.Add(errorDescripcion);
                return null;
            }

            return contrasenaEncriptada;
        }

        private bool ValidarContrasena(ReqIniciarSesion req, string contrasenaEncriptada, ResIniciarSesion res)
        {
            LogEncriptacion encrip = new LogEncriptacion();
            string contrasenaIngresadaEncriptada = encrip.Encrypt(req.contrasena);
            int? activo = 0;
            int? errorId = 0;
            string errorDescripcion = "";

            using (var Linq = new conexionbdDataContext())
            {
                Linq.SP_OBTENER_ACTIVO_POR_CORREO(req.correo, ref activo, ref errorId, ref errorDescripcion);
            }

            if (activo == 2)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Registrese para poder disfrutar de la app");
                return false;
            }

            if (contrasenaIngresadaEncriptada != contrasenaEncriptada)
            {
                res.resultado = false;
                res.listaDeErrores.Add("La contraseña ingresada es incorrecta.");
                return false;
            }

            return true;
        }


        private void ObtenerDetallesUsuario(ReqIniciarSesion req, ResIniciarSesion res)
        {
            int? idReturn = 0;
            int? idUsuario = 0;
            string nombreUsuario = "";
            string apellidosUsuario = "";
            string direccion = "";
            string telefono = "";
            string correoUsuario = "";
            string nombreTipoRol = "";
            int? activo = 0;
            int? errorId = 0;
            string errorDescripcion = "";
            using (var Linq = new conexionbdDataContext())
            {
                Linq.SP_OBTENER_USUARIO_POR_CORREO(req.correo, ref nombreUsuario, ref apellidosUsuario, ref direccion, ref telefono, ref nombreTipoRol, ref activo, ref idReturn, ref errorId, ref errorDescripcion);

            }

            if (idReturn != 1 || errorId != 0)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Error al obtener detalles del usuario.");
                return;
            }

            res.resultado = true;
            res.Usuario = new Usuario
            {
                Nombre = nombreUsuario,
                TipoRol = nombreTipoRol,
                Apellido = apellidosUsuario,
                direccion = direccion,
                telefono = telefono,
                correo = req.correo,
            };
        }

        private void CrearBitacora(ReqIniciarSesion req, ResIniciarSesion res)
        {
            Utilitarios.Utilitarios.crearBitacora(
                res.listaDeErrores,
                res.resultado ? (short)1 : (short)2,
                "LogIniciarSesion",
                "IniciarSesion",
                JsonConvert.SerializeObject(req),
                JsonConvert.SerializeObject(res)
            );
        }


    }
}
