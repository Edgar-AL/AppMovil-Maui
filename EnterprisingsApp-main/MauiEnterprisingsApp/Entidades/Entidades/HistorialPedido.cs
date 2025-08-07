using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEnterprisingsApp.Entidades
{
    public class HistorialPedido
    {
        public int idHistorial { get; set; }
        public int idPedido { get; set; }
        public int idEmprendedor { get; set; }
        public string nombreProducto { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public int nuevoEstado { get; set; }

        public string estadoDescripcion
        {
            get
            {
                return nuevoEstado switch
                {
                    0 => "Pedido eliminado",
                    1 => "Pedido entregado",
                    2 => "Pedido pendiente",
                    _ => "Estado desconocido"
                };
            }
        }
    }
}
