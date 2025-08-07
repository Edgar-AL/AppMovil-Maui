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
    public class TipoRetiroController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/TipoRetiro/Ingresar")]

        public ResInsertarTipoRetiro ingresarRetiro(ReqInsertarTipoRetiro req)
        {
            return new LogTipoRetiro().insertarTipoRetiro(req);
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/TipoRetiro/Actualizar")]

        public ResActualizarTipoRetiro actulizarRetiro(ReqActualizarTipoRetiro req)
        {
            return new LogTipoRetiro().actualizarTipoRetiro(req);
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/TipoRetiro/Eliminar")]

        public ResEliminarTipoRetiro eliminarRetiro(ReqEliminarTipoRetiro req)
        {
            return new LogTipoRetiro().eliminarTipoRetiro(req);
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/TipoRetiro/Activar")]

        public ResActivarTipoRetiro activarRetiro(ReqActivarTipoRetiro req)
        {
            return new LogTipoRetiro().activarTipoReiro(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/TipoRetiro/Mostrar")]

        public List<TipoRetiro> mostrarRetiro()
        {
            return new LogTipoRetiro().MostrarTiposRetiro();
        }
    }
}
