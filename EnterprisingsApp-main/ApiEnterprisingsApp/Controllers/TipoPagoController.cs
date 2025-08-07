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
    public class TipoPagoController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/TipoPago/Ingresar")]

        public ResInsertarTipoPago ingresarPago(ReqInsertarTipoPago req)
        {
            return new LogTipoPago().insertarTipoPago(req);
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/TipoPago/Actualizar")]

        public ResActualizarTipoPago actualizarPago(ReqActulizarTipoPago req)
        {
            return new LogTipoPago().actualizarTipoPago(req);
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/TipoPago/Eliminar")]

        public ResEliminarTipoPago eliminarPago(ReqEliminarTipoPago req)
        {
            return new LogTipoPago().eliminarTipoPago(req);
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/TipoPago/Activar")]

        public ResActivarTipoPago activarPago(ReqActivarTipoPago req)
        {
            return new LogTipoPago().activarTipoPago(req);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/TipoPago/Mostrar")]

        public List<TipoPago> mostrarPago()
        {
            return new LogTipoPago().MostrarTiposPago();
        }
    }
}
