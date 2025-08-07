using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEnterprisingsApp.Entidades
{
    public class Pedido
    {
        public int idPedido { get; set; }
        public int? idCliente { get; set; }
        public int? idEmprendedor { get; set; }
        public int? idProducto { get; set; }
        public int tipoPago { get; set; }
        public int tipoRetiro { get; set; }
        public DateTime fechaPedido { get; set; }
        public int? cantidad { get; set; }
        public string observaciones { get; set; }
        public int estado { get; set; }

        public string nombreUsuario { get; set; }
        public string apellidosUsuario { get; set; }
        public int telefono { get; set; }
        public string nombreProducto { get; set; }
        public string nombrePago { get; set; }
        public string nombreRetiro { get; set; }
    }
}
