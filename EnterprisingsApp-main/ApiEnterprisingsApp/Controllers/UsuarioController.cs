using BackendEnterprisingsApp.Entidades;
using BackendEnterprisingsApp.Logica;
using EnterprisingApp.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ApiEnterprisingsApp.Controllers
{
    public class UsuarioController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Usuario/Registro")]

        public ResRegistrarUsuario registrarUsuario(ReqRegistrarUsuario reqRegistrarUsuario)
        {
            return new LogUsuario().registroUsuario(reqRegistrarUsuario);
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Usuario/IniciarSesion")]

        public ResIniciarSesion IniciarSesion(ReqIniciarSesion reqIniciarSesion)
        {
            return new LogIniciarSesion().IniciarSesion(reqIniciarSesion);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Usuario/ActivacionCuenta")]

        public ResActivacionCuenta ActivarCuenta(ReqActivacionCuenta reqActivacionCuenta)
        {
            return new LogActivacionCuenta().ActivarCuenta(reqActivacionCuenta);
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Usuario/CodigoActivacion")]

        public ResActualizarCodigoVerificacion ActualizarCodigo(ReqActualizarCodigoVerificacion reqActualizarCodigoVerificacion)
        {
            return new LogActivacionCuenta().ActualizarCodigoVerificacion(reqActualizarCodigoVerificacion);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Usuario/EliminarUsuario")]

        public ResEliminarUsuario EliminarUsuario(ReqEliminarUsuario reqEliminarUsuario)
        {
            return new LogActivacionCuenta().EliminarUsuario(reqEliminarUsuario);
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Usuario/ActualizarUsuario")]

        public ResRegistrarUsuario ActualizarUsuario(ReqRegistrarUsuario reqRegistrarUsuario)
        {
            return new LogUsuario().ActualizarUsuario(reqRegistrarUsuario);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Usuario/ActualizarContrasenia")]

        public ResActualizarContrasenia actualizarContresenia(ReqActualizarContrasenia reqActualizarContrasenia)
        {
            return new LogUsuario().ActualizarContrasena(reqActualizarContrasenia);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Usuario/ObtenerUsuario")]

        public ResObtenerUsuarioPorCorreo obtenerUsuario(ReqObtnerUsuarioPorCorreo reqObtenerUsuarioPorCorreo)
        {
            return new LogUsuario().ObtenerUsuarioPorCorreo(reqObtenerUsuarioPorCorreo);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Usuario/ActualizarContraseniaOlvido")]

        public ResActualizarContraseñaOlvidada actualizarContraseñaOlvidada(ReqActualizarContraseñaOlvidada reqActualizarContraseñaOlvidada)
        {
            return new LogUsuario().ActualizarPorOlvido(reqActualizarContraseñaOlvidada);
        }
    }
}
