using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp.Entidades
{
    public class Usuario
    {
        public Int64 id { get; set; }
        public string TipoRol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string contrasenia {  get; set; }
        public string numeroVerificacion {  get; set; }
        public int activo { get; set; }

    }
}
