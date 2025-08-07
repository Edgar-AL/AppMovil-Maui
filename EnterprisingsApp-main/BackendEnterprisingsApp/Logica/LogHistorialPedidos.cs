using BackendEnterprisingsApp.AccesoDatos;
using BackendEnterprisingsApp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp
{
    public class LogHistorialPedidos
    {
        string con = "";

        public ResObtenerHistorialPedidos obtenerHistorialPedidos(ReqObtenerHistorialPedidos req)
        {
            ResObtenerHistorialPedidos res = new ResObtenerHistorialPedidos();
            short tipoRegistro = 0;

            try
            {
                conexionbdDataContext miLinq = new conexionbdDataContext();
                List<SP_OBTENER_HISTORIAL_PEDIDOSResult> listaDeLinq = new List<SP_OBTENER_HISTORIAL_PEDIDOSResult>();
                listaDeLinq = miLinq.SP_OBTENER_HISTORIAL_PEDIDOS().ToList();
                res.listaHistorialPedidos = this.crearListaHistorialPedidos(listaDeLinq);
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

        private List<HistorialPedido> crearListaHistorialPedidos(List<SP_OBTENER_HISTORIAL_PEDIDOSResult> listaDeLinq)
        {
            List<HistorialPedido> listaArmada = new List<HistorialPedido>();
            foreach (SP_OBTENER_HISTORIAL_PEDIDOSResult tipoComplejo in listaDeLinq)
            {
                listaArmada.Add(this.crearHistorialPedido(tipoComplejo));
            }
            return listaArmada;
        }

        private HistorialPedido crearHistorialPedido(SP_OBTENER_HISTORIAL_PEDIDOSResult unTipoComplejo)
        {
            HistorialPedido unHistorialPedido = new HistorialPedido();
            unHistorialPedido.idHistorial = (int)unTipoComplejo.ID_HISTORIAL;
            unHistorialPedido.idPedido = (int)unTipoComplejo.ID_PEDIDO;
            unHistorialPedido.nombreProducto = unTipoComplejo.NOMBRE_PRODUCTO;
            unHistorialPedido.nuevoEstado = (int)unTipoComplejo.ESTADO_PEDIDO;
            unHistorialPedido.fechaActualizacion = (DateTime)unTipoComplejo.FECHA_ACTUALIZACION;

            return unHistorialPedido;
        }

        // ESTA PARTE ES PARA ESPECIFICAMENTE LO QUE ES MOSTRAR EL HISTORIAL DE LOS PEDIDOS POR EMPRENDEDOR //


        public ResObtenerHistorialPedidosPorEmprendedor obtenerHistorialPedidosPorEmprendedor(ReqObtenerHistorialPedidosPorEmprendedor req)
        {
            ResObtenerHistorialPedidosPorEmprendedor res = new ResObtenerHistorialPedidosPorEmprendedor();
            short tipoRegistro = 0;

            try
            {
                conexionbdDataContext miLinq = new conexionbdDataContext();
                List<SP_OBTENER_HISTORIAL_PEDIDOS_EMPRENDEDORResult> listaDeLinq = new List<SP_OBTENER_HISTORIAL_PEDIDOS_EMPRENDEDORResult>();
                listaDeLinq = miLinq.SP_OBTENER_HISTORIAL_PEDIDOS_EMPRENDEDOR(req.idEmprendedor).ToList();
                res.listaHistorialPedidosPorEmprendedor = this.crearListaHistorialPedidosPorEmprendedor(listaDeLinq);
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

        private List<HistorialPedido> crearListaHistorialPedidosPorEmprendedor(List<SP_OBTENER_HISTORIAL_PEDIDOS_EMPRENDEDORResult> listaDeLinq)
        {
            List<HistorialPedido> listaArmada = new List<HistorialPedido>();
            foreach (SP_OBTENER_HISTORIAL_PEDIDOS_EMPRENDEDORResult tipoComplejo in listaDeLinq)
            {
                listaArmada.Add(this.crearHistorialPedidoPorEmprendedor(tipoComplejo));
            }
            return listaArmada;
        }

        private HistorialPedido crearHistorialPedidoPorEmprendedor(SP_OBTENER_HISTORIAL_PEDIDOS_EMPRENDEDORResult unTipoComplejo)
        {
            HistorialPedido unHistorialPedido = new HistorialPedido();
            unHistorialPedido.idHistorial = (int)unTipoComplejo.ID_HISTORIAL;
            unHistorialPedido.idPedido = (int)unTipoComplejo.ID_PEDIDO;
            unHistorialPedido.idEmprendedor = (int)unTipoComplejo.ID_USUARIO;
            unHistorialPedido.nombreProducto = unTipoComplejo.NOMBRE_PRODUCTO;
            unHistorialPedido.nuevoEstado = (int)unTipoComplejo.ESTADO_PEDIDO;
            unHistorialPedido.fechaActualizacion = (DateTime)unTipoComplejo.FECHA_ACTUALIZACION;

            return unHistorialPedido;
        }
    }
}
