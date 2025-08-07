using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp.Entidades
{
    public class Producto
    {
        public int idProducto {  get; set; }
        public int idEmprendedor { get; set; }
        public string nombreProducto { get; set; }
        public int idCategoria { get; set;}
        public string descripcion { get;set;}
        public int activo { get; set;}   
    }
}
