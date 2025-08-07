using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEnterprisingsApp.Entidades
{
    public class Reporte
    {
        public int idReporte { get; set; }
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string apellidosUsuario { get; set; }
        public string nombreCompleto => $"{nombreUsuario} {apellidosUsuario}";
        public string correoUsuario { get; set; }
        public DateTime fechaReporte { get; set; }
        public string descripcionReporte { get; set; }
    }
}
