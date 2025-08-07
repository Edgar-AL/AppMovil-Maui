using BackendEnterprisingsApp.AccesoDatos;
using BackendEnterprisingsApp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp
{
    public class LogPedido
    {
        string con = "";

        public ResIngresarPedido ingresarPedido(ReqIngresarPedido req)
        {
            ResIngresarPedido res = new ResIngresarPedido();
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
                    if (req.pedido.idCliente == 0)
                    {
                        res.listaDeErrores.Add("ID de usuario faltante");
                        res.resultado = false;
                    }
                    if (req.pedido.idEmprendedor == 0)
                    {
                        res.listaDeErrores.Add("ID de usuario faltante");
                        res.resultado = false;
                    }
                    if (req.pedido.idProducto == 0)
                    {
                        res.listaDeErrores.Add("ID de producto faltante");
                        res.resultado = false;
                    }
                    if (req.pedido.tipoPago == 0)
                    {
                        res.listaDeErrores.Add("Tipo de pago faltante");
                        res.resultado = false;
                    }
                    if (req.pedido.tipoRetiro == 0)
                    {
                        res.listaDeErrores.Add("Tipo de retiro faltante");
                        res.resultado = false;
                    }
                    if (req.pedido.fechaPedido == null)
                    {
                        res.listaDeErrores.Add("Fecha de pedido faltante");
                        res.resultado = false;
                    }
                    if (req.pedido.cantidad == 0)
                    {
                        res.listaDeErrores.Add("Cantidad faltante");
                        res.resultado = false;
                    }
                    if (String.IsNullOrEmpty(req.pedido.observaciones))
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
                        miLinq.SP_INGRESAR_PEDIDO((int?)req.pedido.idCliente, (int?)req.pedido.idEmprendedor, (int?)req.pedido.idProducto, req.pedido.tipoPago, req.pedido.tipoRetiro, (int?)req.pedido.cantidad, req.pedido.observaciones, ref idReturn, ref idError, ref errorBD);

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

        public ResActualizarPedidoUsuario actualizarPedidoUsuario(ReqActualizarPedidoUsuario req)
        {
            ResActualizarPedidoUsuario res = new ResActualizarPedidoUsuario();
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
                    if (req.pedido.idPedido == 0)
                    {
                        res.listaDeErrores.Add("ID de pedido faltante");
                        res.resultado = false;
                    }
                    if (req.pedido.tipoPago == 0)
                    {
                        res.listaDeErrores.Add("Tipo de pago faltante");
                        res.resultado = false;
                    }
                    if (req.pedido.tipoRetiro == 0)
                    {
                        res.listaDeErrores.Add("Tipo de retiro faltante");
                        res.resultado = false;
                    }
                    if (req.pedido.cantidad == 0)
                    {
                        res.listaDeErrores.Add("Cantidad faltante");
                        res.resultado = false;
                    }
                    if (String.IsNullOrEmpty(req.pedido.observaciones))
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
                        miLinq.SP_ACTUALIZAR_PEDIDO_USUARIO((int?)req.pedido.idPedido, (int?)req.pedido.tipoPago, (int?)req.pedido.tipoRetiro, (int?)req.pedido.cantidad, req.pedido.observaciones, ref idReturn, ref idError, ref errorBD);

                        if (idError == null || idError == 0)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorBD);
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
                tipoRegistro = 2; //No Exitoso
            }
            finally
            {
                //Se bitacorea todo resultado. Exitoso o no exitoso.
                //Utilitarios.crearBitacora(res.listaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(null));
            }
            return res;
        }

        public ResActualizarEstadoPedido actualizarEstadoPedido(ReqActualizarEstadoPedido req)
        {

            ResActualizarEstadoPedido res = new ResActualizarEstadoPedido();
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
                    if (req.pedido.idPedido == 0)
                    {
                        res.listaDeErrores.Add("ID de pedido faltante");
                        res.resultado = false;
                    }
                    if (req.pedido.estado == 0)
                    {
                        res.listaDeErrores.Add("Tipo de pago faltante");
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
                        miLinq.SP_ACTUALIZAR_ESTADO_PEDIDO((int?)req.pedido.idPedido, (int?)req.pedido.estado, ref idReturn, ref idError, ref errorBD);

                        if (idError == null || idError == 0)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorBD);
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
                tipoRegistro = 2; //No Exitoso
            }
            finally
            {
                //Se bitacorea todo resultado. Exitoso o no exitoso.
                //Utilitarios.crearBitacora(res.listaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(null));
            }
            return res;
        }
        
        public ResEliminarPedido eliminarPedido(ReqEliminarPedido req)
        {
            ResEliminarPedido res = new ResEliminarPedido();
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
                    if (req.idPedido == 0)
                    {
                        res.listaDeErrores.Add("ID de pedido faltante");
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
                        miLinq.SP_ELIMINAR_PEDIDO((int?)req.idPedido, ref idReturn, ref idError, ref errorBD);

                        if (idError == null || idError == 0)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorBD);
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
                tipoRegistro = 2; //No Exitoso
            }
            finally
            {
                //Se bitacorea todo resultado. Exitoso o no exitoso.
                //Utilitarios.crearBitacora(res.listaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(null));
            }
            return res;
        }

        public ResObtenerPedidos obtenerPedidos(ReqObtenerPedidos req)
        {
            ResObtenerPedidos res = new ResObtenerPedidos();
            short tipoRegistro = 0;

            try
            {
                conexionbdDataContext miLinq = new conexionbdDataContext();
                List<SP_OBTENER_PEDIDOSResult> listaDeLinq = new List<SP_OBTENER_PEDIDOSResult>();
                listaDeLinq = miLinq.SP_OBTENER_PEDIDOS().ToList();
                res.listaPedidos = this.crearListaPedidos(listaDeLinq);
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

        private List<Pedido> crearListaPedidos(List<SP_OBTENER_PEDIDOSResult> listaDeLinq)
        {
            List<Pedido> listaArmada = new List<Pedido>();
            foreach (SP_OBTENER_PEDIDOSResult tipoComplejo in listaDeLinq)
            {
                listaArmada.Add(this.crearPedido(tipoComplejo));
            }
            return listaArmada;
        }

        private Pedido crearPedido(SP_OBTENER_PEDIDOSResult unTipoComplejo)
        {
            Pedido unPedido = new Pedido();
            unPedido.idPedido = (int)unTipoComplejo.ID_PEDIDO;
            unPedido.nombreCliente = unTipoComplejo.NOMBRE_CLIENTE;
            unPedido.apellidosCliente = unTipoComplejo.APELLIDOS_CLIENTE;
            unPedido.telefono = (int)unTipoComplejo.TELEFONO;
            unPedido.nombreProducto = unTipoComplejo.NOMBRE_PRODUCTO;
            unPedido.nombrePago = unTipoComplejo.NOMBRE_PAGO;
            unPedido.nombreRetiro = unTipoComplejo.NOMBRE_RETIRO;
            unPedido.fechaPedido = (DateTime)unTipoComplejo.FECHA_PEDIDO;
            unPedido.cantidad = (int)unTipoComplejo.CANTIDAD;
            unPedido.observaciones = unTipoComplejo.OBSERVACIONES;
            unPedido.estado = (int)unTipoComplejo.ESTADO;

            return unPedido;
        }

        // ESTA PARTE ES PARA ESPECIFICAMENTE LO QUE ES MOSTRAR LOS PEDIDOS POR EMPRENDEDOR //

        public ResObtenerPedidosPorEmprendedor obtenerPedidosPorEmprendedor(ReqObtenerPedidosPorEmprendedor req)
        {
            ResObtenerPedidosPorEmprendedor res = new ResObtenerPedidosPorEmprendedor();
            short tipoRegistro = 0;

            try
            {
                conexionbdDataContext miLinq = new conexionbdDataContext();
                List<SP_OBTENER_PEDIDOS_EMPRENDEDORResult> listaDeLinq = new List<SP_OBTENER_PEDIDOS_EMPRENDEDORResult>();
                listaDeLinq = miLinq.SP_OBTENER_PEDIDOS_EMPRENDEDOR(req.idEmprendedor).ToList();
                res.listaPedidosEmprendedor = this.crearListaPedidosPorEmprendedor(listaDeLinq);
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

        private List<Pedido> crearListaPedidosPorEmprendedor(List<SP_OBTENER_PEDIDOS_EMPRENDEDORResult> listaDeLinq)
        {
            List<Pedido> listaArmada = new List<Pedido>();
            foreach (SP_OBTENER_PEDIDOS_EMPRENDEDORResult tipoComplejo in listaDeLinq)
            {
                listaArmada.Add(this.crearPedidoPorEmprendedor(tipoComplejo));
            }
            return listaArmada;
        }

        private Pedido crearPedidoPorEmprendedor(SP_OBTENER_PEDIDOS_EMPRENDEDORResult unTipoComplejo)
        {
            Pedido unPedido = new Pedido();
            unPedido.idPedido = (int)unTipoComplejo.ID_PEDIDO;
            unPedido.nombreCliente = unTipoComplejo.NOMBRE_CLIENTE;
            unPedido.apellidosCliente = unTipoComplejo.APELLIDOS_CLIENTE;
            unPedido.telefono = (int)unTipoComplejo.TELEFONO;
            unPedido.nombreProducto = unTipoComplejo.NOMBRE_PRODUCTO;
            unPedido.nombrePago = unTipoComplejo.NOMBRE_PAGO;
            unPedido.nombreRetiro = unTipoComplejo.NOMBRE_RETIRO;
            unPedido.fechaPedido = (DateTime)unTipoComplejo.FECHA_PEDIDO;
            unPedido.cantidad = (int)unTipoComplejo.CANTIDAD;
            unPedido.observaciones = unTipoComplejo.OBSERVACIONES;
            unPedido.estado = (int)unTipoComplejo.ESTADO;

            return unPedido;
        }
    }
}
