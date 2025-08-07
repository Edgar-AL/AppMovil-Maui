using BackendEnterprisingsApp.Entidades;
using BackendEnterprisingsApp.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ApiEnterprisingsApp.Controllers
{
    public class ProductoController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Producto/Mostrar")]
        public ResObtenerProducto mostrarProductos()
        {
            return new LogProducto().MostrarProductos();
        }


        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/Producto/Eliminar")]
        public ResEliminarProducto eliminarProducto(ReqEliminarProducto req)
        {
            return new LogProducto().EliminarProducto(req);
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Producto/Activar")]
        public ResActivarProducto activarProducto(ReqActivarProducto req)
        {
            return new LogProducto().ActivarProducto(req);
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Producto/Actualizar")]
        public ResActualizarProducto actualizarProducto(ReqActualizarProducto req)
        {
            return new LogProducto().ActualizarProducto(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Producto/Ingresar")]
        public ResInsertarProducto insertarProducto(ReqInsertarProducto req)
        {
            return new LogProducto().InsertarProducto(req);
        }
    }
}
