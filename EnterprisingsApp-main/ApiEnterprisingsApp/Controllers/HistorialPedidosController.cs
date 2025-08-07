using BackendEnterprisingsApp.Entidades;
using BackendEnterprisingsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

namespace ApiEnterprisingsApp.Controllers
{
    public class HistorialPedidosController : ApiController
    {
        public ResObtenerHistorialPedidos Get()
        {
            ReqObtenerHistorialPedidos req = new ReqObtenerHistorialPedidos();

            LogHistorialPedidos logicaDelBackend = new LogHistorialPedidos();
            return logicaDelBackend.obtenerHistorialPedidos(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/HistorialPedidos/historialPedidosPorEmprendedor")]
        public ResObtenerHistorialPedidosPorEmprendedor obtenerHistorialPedidosPorEmprendedor([FromBody] ReqObtenerHistorialPedidosPorEmprendedor req)
        {
            LogHistorialPedidos logicaDelBackend = new LogHistorialPedidos();
            return logicaDelBackend.obtenerHistorialPedidosPorEmprendedor(req);
        }
    }
}
