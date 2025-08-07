using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp.Entidades
{
    public class ResIniciarSesion : ResBase
    {
        public Usuario Usuario { get; internal set; }
    }
}
