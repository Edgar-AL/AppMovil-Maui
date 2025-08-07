using BackendEnterprisingsApp.AccesoDatos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp.Utilitarios
{
    public class Utilitarios
    {
        public static void crearBitacora(List<String> listaDeErrores, short tipo, string laClase, string elMetodo, string elRequest, string elResponse)
        {
            try
            {
                conexionbdDataContext linq = new conexionbdDataContext();
                linq.SP_INSERTAR_BITACORA(laClase, elMetodo, tipo, listaDeErrores.ToString(), elRequest, elResponse);
            }
            catch (Exception e)
            {
               

                TextWriter loguearEnTexto = new StreamWriter("C:\\Users\\josep\\OneDrive\\Documentos\\Bitacora .NET\\logErroresEnBdXH.txt.");
                loguearEnTexto.WriteLine("NO SE PUDO BITACOREAR EN BD EL MENSAJE DE ERROR FUE: " + e.StackTrace.ToString() + " --> CLASE: " + laClase + " METODO: " + elMetodo + " TIPO " + tipo + " DESCRIPCION " + listaDeErrores.ToString() + " REQ: " + elRequest + " RES " + elResponse + " FECHA: " + DateTime.Now.ToString());

            }
        }
    }
}
