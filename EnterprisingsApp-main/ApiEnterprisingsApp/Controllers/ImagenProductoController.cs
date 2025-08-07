using BackendEnterprisingsApp.Entidades;
using BackendEnterprisingsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ApiEnterprisingsApp.Controllers
{
    public class ImagenProductoController : ApiController
    {
        public ResObtenerImagen Get()
        {
            ReqObtenerImagen req = new ReqObtenerImagen();

            LogImagenProducto logicaDelBackend = new LogImagenProducto();
            return logicaDelBackend.obtenerImagenes(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ImagenProducto/IngresarImagen")]
        public ResIngresarImagen ingresarImagen([FromBody] ReqIngresarImagen req)
        {
            LogImagenProducto logicaDelBackend = new LogImagenProducto();
            return logicaDelBackend.ingresarImagen(req);

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ImagenProducto/ActualizarImagen")]
        public ResActualizarImagen actualizarImagen([FromBody] ReqActualizarImagen req)
        {
            LogImagenProducto logicaDelBackend = new LogImagenProducto();
            return logicaDelBackend.actualizarImagen(req);
        }
    }
}
