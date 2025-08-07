using BackendEnterprisingsApp.AccesoDatos;
using BackendEnterprisingsApp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp
{
    public class LogReporte
    {
        public ResIngresarReporte ingresarReporte(ReqIngresarReporte req)
        {
            ResIngresarReporte res = new ResIngresarReporte();
            short tipoRegistro = 0;

            try
            {
                if (req == null)
                {
                    res.listaDeErrores.Add("Res nulo");
                    res.resultado = false;
                }
                else
                {
                    if (req.reporte.idUsuario == 0)
                    {
                        res.listaDeErrores.Add("ID de usuario reportado faltante");
                        res.resultado = false;
                    }
                    if (String.IsNullOrEmpty(req.reporte.descripcionReporte))
                    {
                        res.listaDeErrores.Add("Detalles de pedido faltante");
                        res.resultado = false;
                    }

                    if (!res.listaDeErrores.Any())
                    {
                        //No hay errores
                        //Mandar a AD
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBD = "";

                        conexionbdDataContext miLinq = new conexionbdDataContext();
                        miLinq.SP_INSERTAR_REPORTE((int?)req.reporte.idUsuario, req.reporte.descripcionReporte, ref idReturn, ref idError, ref errorBD);

                        if (idError == null || idError == 0)
                        {
                            res.resultado = true;
                            tipoRegistro = 1;
                        }
                        else
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorBD);
                            tipoRegistro = 2;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Error interno");
                tipoRegistro = 2; //No Exitoso
            }
            finally
            {
                //Se bitacorea todo resultado. Exitoso o no exitoso.
                //Utilitarios.crearBitacora(res.listaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(null));
            }
            return res;
        }
        public ResObtenerReportes obtenerReportes(ReqObtenerReportes req)
        {
            ResObtenerReportes res = new ResObtenerReportes();
            short tipoRegistro = 0;

            try
            {
                conexionbdDataContext miLinq = new conexionbdDataContext();
                List<SP_OBTENER_REPORTESResult> listaDeLinq = new List<SP_OBTENER_REPORTESResult>();
                listaDeLinq = miLinq.SP_OBTENER_REPORTES().ToList();
                res.listaReportes = this.crearListaReportes(listaDeLinq);
                res.resultado = true;

            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add(ex.StackTrace); //!!!!!!
            }
            finally
            {
                //	Utilitarios.bitacorear(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, tipoDeTransaccion, (int)errorId, descripcionError, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res;
        }

        private List<Reporte> crearListaReportes(List<SP_OBTENER_REPORTESResult> listaDeLinq)
        {
            List<Reporte> listaArmada = new List<Reporte>();
            foreach (SP_OBTENER_REPORTESResult tipoComplejo in listaDeLinq)
            {
                listaArmada.Add(this.crearReporte(tipoComplejo));
            }
            return listaArmada;
        }

        private Reporte crearReporte(SP_OBTENER_REPORTESResult unTipoComplejo)
        {
            Reporte unReporte = new Reporte();
            unReporte.idReporte = (int)unTipoComplejo.ID_REPORTE;
            unReporte.nombreUsuario = unTipoComplejo.NOMBRE_USUARIO;
            unReporte.apellidosUsuario = unTipoComplejo.APELLIDOS_USUARIO;
            unReporte.correoUsuario = unTipoComplejo.CORREO;
            unReporte.fechaReporte = (DateTime)unTipoComplejo.FECHA_REPORTE;
            unReporte.descripcionReporte = unTipoComplejo.DESCRIPCION_REPORTE;

            return unReporte;
        }
    }
}
