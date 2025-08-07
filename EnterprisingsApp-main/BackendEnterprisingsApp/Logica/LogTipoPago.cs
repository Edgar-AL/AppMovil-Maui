using BackendEnterprisingsApp.AccesoDatos;
using BackendEnterprisingsApp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp.Logica
{
    public class LogTipoPago
    {
        public ResInsertarTipoPago insertarTipoPago(ReqInsertarTipoPago req)
        {
            ResInsertarTipoPago res = new ResInsertarTipoPago();
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

                    if (req.tipoPago.nombrePago == "")
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Tipo de pago faltante");
                        tipoRegistro = 2;
                    }

                    if (!res.listaDeErrores.Any())
                    {
                        conexionbdDataContext linq = new conexionbdDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_INGRESAR_TIPO_PAGO(req.tipoPago.nombrePago, ref idReturn, ref idError, ref errorBd);
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

        public ResActualizarTipoPago actualizarTipoPago(ReqActulizarTipoPago req)
        {
            ResActualizarTipoPago res = new ResActualizarTipoPago();
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
                    if (req.tipoPago.idPago == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("ID de tipo de pago faltante");
                        tipoRegistro = 2;
                    }
                    else if (req.tipoPago.nombrePago == "")
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Nombre de tipo de pago faltante");
                        tipoRegistro = 2;
                    }
                    else
                    {
                        conexionbdDataContext linq = new conexionbdDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_ACTUALIZAR_TIPO_PAGO(req.tipoPago.idPago, req.tipoPago.nombrePago, ref idReturn, ref idError, ref errorBd);
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

        public ResEliminarTipoPago eliminarTipoPago(ReqEliminarTipoPago req)
        {
            ResEliminarTipoPago res = new ResEliminarTipoPago();
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
                    if (req.idTipoPago == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("ID de tipo de pago inválido");
                        tipoRegistro = 2;
                    }
                    else
                    {
                        conexionbdDataContext linq = new conexionbdDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_ELIMINAR_TIPO_PAGO(req.idTipoPago, ref idReturn, ref idError, ref errorBd);
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

        public ResActivarTipoPago activarTipoPago(ReqActivarTipoPago req)
        {
            ResActivarTipoPago res = new ResActivarTipoPago();
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
                    if (req.idTipoPago == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("ID de tipo de pago inválido");
                        tipoRegistro = 2;
                    }
                    else
                    {
                        conexionbdDataContext linq = new conexionbdDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_ACTIVAR_TIPO_PAGO(req.idTipoPago, ref idReturn, ref idError, ref errorBd);
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

        public List<TipoPago> MostrarTiposPago()
        {
            ResObtenerTipoPago res = new ResObtenerTipoPago();
            short tipoRegistro = 0; 
            List<TipoPago> tiposPago = new List<TipoPago>();

            try
            {
                conexionbdDataContext linq = new conexionbdDataContext();
                var tiposPagoDB = linq.SP_OBTENER_TIPO_PAGO();

                foreach (var tipoPagoDB in tiposPagoDB)
                {
                    TipoPago tipoPago = new TipoPago
                    {

                        idPago = tipoPagoDB.ID_TIPOPAGO,
                        nombrePago = tipoPagoDB.NOMBRE_PAGO
                    };

                    tiposPago.Add(tipoPago);
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

            return tiposPago;
        }

    }
}
