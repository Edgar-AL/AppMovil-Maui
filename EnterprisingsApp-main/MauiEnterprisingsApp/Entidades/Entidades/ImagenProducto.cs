using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEnterprisingsApp.Entidades
{
    public class ImagenProducto
    {
        public int idImagen { get; set; }
        public int idProducto { get; set; }
        public string nombreImagen { get; set; }
        public string rutaImagen { get; set; }
    }
}
