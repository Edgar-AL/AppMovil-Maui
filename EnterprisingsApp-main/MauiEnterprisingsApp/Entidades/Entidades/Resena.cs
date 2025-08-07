using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEnterprisingsApp.Entidades.Entidades
{
    public class Resena
    {
        public int idResena { get; set; }
        public int idProducto { get; set; }
        public int idUsuario { get; set; }
        public int valoracion { get; set; }
        public string resena { get; set; }
        public string nombreProducto { get; set; }
        public string nombreUsuario { get; set; }
        public string apellidosUsuario { get; set; }


    }
}
