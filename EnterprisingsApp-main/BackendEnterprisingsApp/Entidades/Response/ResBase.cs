using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp.Entidades
{
    public class ResBase
    {
        public Boolean resultado { get; set; }
        public List<String> listaDeErrores = new List<String>();

    }
}
