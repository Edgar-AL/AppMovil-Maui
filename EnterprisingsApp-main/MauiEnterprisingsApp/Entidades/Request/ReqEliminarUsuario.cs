using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEnterprisingsApp.Entidades.Request
{
    public class ReqEliminarUsuario
    {
        public string correo { get; set; }
        public string contrasenia { get; set; }
    }
}
