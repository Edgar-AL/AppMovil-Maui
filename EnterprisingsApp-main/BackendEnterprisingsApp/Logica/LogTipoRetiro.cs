using BackendEnterprisingsApp.AccesoDatos;
using BackendEnterprisingsApp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp.Logica
{
    public class LogTipoRetiro
    {
        public ResInsertarTipoRetiro insertarTipoRetiro(ReqInsertarTipoRetiro req)
        {
            ResInsertarTipoRetiro res = new ResInsertarTipoRetiro();
            short tipoRegistro = 0;
            try
            {
                if (req == null)
                {
                    res.listaDeErrores.Add("Req nulo");
                    res.resultado = false;
                }
                else
                {

                    if (req.tipoRetiro.nombreRetiro == "")
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Tipo del retiro faltante");
                        tipoRegistro = 2;
                    }

                    if (!res.listaDeErrores.Any())
                    {
                        conexionbdDataContext linq = new conexionbdDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_INGRESAR_TIPO_RETIRO(req.tipoRetiro.nombreRetiro, ref idReturn, ref idError, ref errorBd);
                        if (idError == null || idError == 0)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorBd);
                            tipoRegistro = 2;
                        }
                        else
                        {
                            res.resultado = true;
                            tipoRegistro = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Error interno");
                tipoRegistro = 3;
            }
            return res;
        }

        public ResActualizarTipoRetiro actualizarTipoRetiro(ReqActualizarTipoRetiro req)
        {
            ResActualizarTipoRetiro res = new ResActualizarTipoRetiro();
            short tipoRegistro = 0; 
            try
            {
                if (req == null)
                {
                    res.listaDeErrores.Add("Req nulo");
                    res.resultado = false;
                }
                else
                {
                    if (req.tipoRetiro.idRetiro == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("ID de tipo de retiro faltante");
                        tipoRegistro = 2;
                    }
                    else if (req.tipoRetiro.nombreRetiro == "")
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Nombre de tipo de retiro faltante");
                        tipoRegistro = 2;
                    }
                    else
                    {
                        conexionbdDataContext linq = new conexionbdDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_ACTUALIZAR_TIPO_RETIRO(req.tipoRetiro.idRetiro, req.tipoRetiro.nombreRetiro, ref idReturn, ref idError, ref errorBd);
                        if (idError == null || idError == 0)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorBd);
                            tipoRegistro = 2;
                        }
                        else
                        {
                            res.resultado = true;
                            tipoRegistro = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Error interno");
                tipoRegistro = 3; //No Exitoso
            }
            return res;
        }

        public ResEliminarTipoRetiro eliminarTipoRetiro(ReqEliminarTipoRetiro req)
        {
            ResEliminarTipoRetiro res = new ResEliminarTipoRetiro();
            short tipoRegistro = 0;
            try
            {
                if (req == null)
                {
                    res.listaDeErrores.Add("Req nulo");
                    res.resultado = false;
                }
                else
                {
                    if (req.idTipoRetiro == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("ID de tipo de retiro inválido");
                        tipoRegistro = 2;
                    }
                    else
                    {
                        conexionbdDataContext linq = new conexionbdDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_ELIMINAR_TIPO_RETIRO(req.idTipoRetiro, ref idReturn, ref idError, ref errorBd);
                        if (idError == null && idError == 0)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorBd);
                            tipoRegistro = 2;
                        }
                        else
                        {
                            res.resultado = true;
                            tipoRegistro = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Error interno");
                tipoRegistro = 3; //No Exitoso
            }
            return res;
        }

        public ResActivarTipoRetiro activarTipoReiro(ReqActivarTipoRetiro req)
        {
            ResActivarTipoRetiro res = new ResActivarTipoRetiro();
            short tipoRegistro = 0;
            try
            {
                if (req == null)
                {
                    res.listaDeErrores.Add("Req nulo");
                    res.resultado = false;
                }
                else
                {
                    if (req.idTipoRetiro == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("ID de tipo de retiro inválido");
                        tipoRegistro = 2;
                    }
                    else
                    {
                        conexionbdDataContext linq = new conexionbdDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_ACTIVAR_TIPO_RETIRO(req.idTipoRetiro, ref idReturn, ref idError, ref errorBd);
                        if (idError == null && idError == 0)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorBd);
                            tipoRegistro = 2;
                        }
                        else
                        {
                            res.resultado = true;
                            tipoRegistro = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Error interno");
                tipoRegistro = 3; 
            }
            return res;
        }

        public List<TipoRetiro> MostrarTiposRetiro()
        {
            ResObtenerTipoRetiro res = new ResObtenerTipoRetiro();
            short tipoRegistro = 0; 
            List<TipoRetiro> tiposRetiro = new List<TipoRetiro>();
           

            try
            {
                conexionbdDataContext linq = new conexionbdDataContext();
                var tiposRetiroDB = linq.SP_OBTENER_TIPO_RETIRO();

                foreach (var tipoRetiroDB in tiposRetiroDB)
                {
                    TipoRetiro tipoRetiro = new TipoRetiro
                    {

                      idRetiro = tipoRetiroDB.ID_TIPORETIRO,
                      nombreRetiro = tipoRetiroDB.NOMBRE_RETIRO
                    };

                    tiposRetiro.Add(tipoRetiro);
                }
                res.resultado = true;
                tipoRegistro = 1;
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Error interno");
                tipoRegistro = 3; 
            }

            return tiposRetiro;
        }


    }
}
