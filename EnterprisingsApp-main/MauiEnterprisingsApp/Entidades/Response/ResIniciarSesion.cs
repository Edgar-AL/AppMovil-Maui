using MauiEnterprisingsApp.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEnterprisingsApp.Entidades.Response
{
    public class ResIniciarSesion : ResBase
    {
        public Usuario Usuario { get; set; }
    }
}
