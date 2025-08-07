using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp.Entidades
{
    public class HistorialPedido
    {
        public int idHistorial {  get; set; }
        public int idPedido { get; set; }
        public int idEmprendedor { get; set; }
        public string nombreProducto { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public int nuevoEstado { get; set; }
    }
}
