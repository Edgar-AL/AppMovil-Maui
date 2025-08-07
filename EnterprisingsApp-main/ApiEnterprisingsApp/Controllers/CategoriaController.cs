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
    public class CategoriaController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Categoria/Ingresar")]
        public ResInsertarCategoria ingresarCategoria(ReqInsertarCategoria req)
        {
            return new LogCategoria().insertarCategoria(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Categoria/Mostrar")]
        public ResObtenerCategoria mostrarCategoria()
        {
            return new LogCategoria().MostrarCategorias();
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Categoria/Actualizar")]
        public ResActualizarCategoria actualizarCategoria(ReqActualizarCategoria req)
        {
            return new LogCategoria().actualizarCategoria(req);
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/Categoria/Eliminar")]
        public ResEliminarCategoria eliminarCategoria(ReqEliminarCategoria req)
        {
            return new LogCategoria().eliminarCategoria(req);
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Categoria/Activar")]
        public ResActivarCategoria activarCategoria(ReqActivarCategoria req)
        {
            return new LogCategoria().activarCategoria(req);
        }
    }
}
