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
    public class ReporteController : ApiController
    {
        public ResObtenerReportes Get()
        {
            ReqObtenerReportes req = new ReqObtenerReportes();

            LogReporte logicaDelBackend = new LogReporte();
            return logicaDelBackend.obtenerReportes(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Reporte/IngresarReporte")]
        public ResIngresarReporte ingresarReporte([FromBody] ReqIngresarReporte req)
        {
            LogReporte logicaDelBackend = new LogReporte();
            return logicaDelBackend.ingresarReporte(req);

        }
    }
}
