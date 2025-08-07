using BackendEnterprisingsApp;
using BackendEnterprisingsApp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ApiEnterprisingsApp.Controllers
{
    public class PedidoController : ApiController
    {
        public ResObtenerPedidos Get()
        {
            ReqObtenerPedidos req = new ReqObtenerPedidos();

            LogPedido logicaDelBackend = new LogPedido();
            return logicaDelBackend.obtenerPedidos(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Pedido/pedidosPorEmprendedor")]
        public ResObtenerPedidosPorEmprendedor obtenerPedidosPorEmprendedor([FromBody] ReqObtenerPedidosPorEmprendedor req)
        {
            LogPedido logicaDelBackend = new LogPedido();
            return logicaDelBackend.obtenerPedidosPorEmprendedor(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Pedido/IngresarPedido")]
        public ResIngresarPedido ingresarPedido([FromBody] ReqIngresarPedido req)
        {
            LogPedido logicaDelBackend = new LogPedido();
            return logicaDelBackend.ingresarPedido(req);

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Pedido/EliminarPedido")]
        public ResEliminarPedido eliminarPedido([FromBody] ReqEliminarPedido req)
        {
            LogPedido logicaDelBackend = new LogPedido();
            return logicaDelBackend.eliminarPedido(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Pedido/ActualizarPedido")]
        public ResActualizarPedidoUsuario actualizarPedidoUsuario([FromBody] ReqActualizarPedidoUsuario req)
        {
            LogPedido logicaDelBackend = new LogPedido();
            return logicaDelBackend.actualizarPedidoUsuario(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Pedido/ActualizarEstadoPedido")]
        public ResActualizarEstadoPedido actualizarEstadoPedido([FromBody] ReqActualizarEstadoPedido req)
        {
            LogPedido logicaDelBackend = new LogPedido();
            return logicaDelBackend.actualizarEstadoPedido(req);
        }
    }
}
