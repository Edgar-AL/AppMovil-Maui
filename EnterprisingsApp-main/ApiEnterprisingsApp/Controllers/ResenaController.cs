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
    public class ResenaController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Resena/Ingresar")]

        public ResInsertarResena ingresarResena(ReqInsertarResena req)
        {
            return new LogResena().insertarResena(req);
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Resena/Actualizar")]

        public ResActualizarResena actualizarResena(ReqActualizarResena req)
        {
            return new LogResena().actualizarResena(req);
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/Resena/Eliminar")]

        public ResEliminarResena eliminarResena(ReqEliminarResena req)
        {
            return new LogResena().eliminarResena(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Resena/Mostrar")]

        public List<Resena> mostrarResena()
        {
            return new LogResena().MostrarResena();
        }
    }
}
