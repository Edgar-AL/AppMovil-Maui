using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp.Entidades
{
    public class ReqActualizarContrasenia
    {
        public string correo {  get; set; }
        public string contraseniaActual { get; set; }

        public string contraseniaNueva { get; set; }

    }
}
