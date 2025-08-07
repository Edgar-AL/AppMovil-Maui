using BackendEnterprisingsApp.AccesoDatos;
using BackendEnterprisingsApp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp
{
    public class LogImagenProducto
    {
        public ResIngresarImagen ingresarImagen(ReqIngresarImagen req)
        {
            ResIngresarImagen res = new ResIngresarImagen();
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
                    if (req.imagenProducto.idProducto == 0)
                    {
                        res.listaDeErrores.Add("ID de producto faltante");
                        res.resultado = false;
                    }
                    if (String.IsNullOrEmpty(req.imagenProducto.nombreImagen))
                    {
                        res.listaDeErrores.Add("Detalles de nombre de imagen faltante");
                        res.resultado = false;
                    }
                    if (String.IsNullOrEmpty(req.imagenProducto.rutaImagen))
                    {
                        res.listaDeErrores.Add("Detalles de ruta de la imagen faltante");
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
                        miLinq.SP_INSERTAR_IMAGEN((int?)req.imagenProducto.idProducto, req.imagenProducto.nombreImagen, req.imagenProducto.rutaImagen, ref idReturn, ref idError, ref errorBD);

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

        public ResActualizarImagen actualizarImagen(ReqActualizarImagen req)
        {
            ResActualizarImagen res = new ResActualizarImagen();
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
                    if (req.imagenProducto.idProducto == 0)
                    {
                        res.listaDeErrores.Add("ID de producto faltante");
                        res.resultado = false;
                    }
                    if (String.IsNullOrEmpty(req.imagenProducto.nombreImagen))
                    {
                        res.listaDeErrores.Add("Detalles de nombre de imagen faltante");
                        res.resultado = false;
                    }
                    if (String.IsNullOrEmpty(req.imagenProducto.rutaImagen))
                    {
                        res.listaDeErrores.Add("Detalles de ruta de la imagen faltante");
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
                        miLinq.SP_ACTUALIZAR_IMAGEN((int?)req.imagenProducto.idProducto, req.imagenProducto.nombreImagen, req.imagenProducto.rutaImagen, ref idReturn, ref idError, ref errorBD);

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

        public ResObtenerImagen obtenerImagenes(ReqObtenerImagen req)
        {
            ResObtenerImagen res = new ResObtenerImagen();
            short tipoRegistro = 0;

            try
            {
                conexionbdDataContext miLinq = new conexionbdDataContext();
                List<SP_OBTENER_IMAGENES_PRODUCTOResult> listaDeLinq = new List<SP_OBTENER_IMAGENES_PRODUCTOResult>();
                listaDeLinq = miLinq.SP_OBTENER_IMAGENES_PRODUCTO().ToList();
                res.listaImagenProducto = this.crearListaImagenes(listaDeLinq);
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

        private List<ImagenProducto> crearListaImagenes(List<SP_OBTENER_IMAGENES_PRODUCTOResult> listaDeLinq)
        {
            List<ImagenProducto> listaArmada = new List<ImagenProducto>();
            foreach (SP_OBTENER_IMAGENES_PRODUCTOResult tipoComplejo in listaDeLinq)
            {
                listaArmada.Add(this.crearImagen(tipoComplejo));
            }
            return listaArmada;
        }

        private ImagenProducto crearImagen(SP_OBTENER_IMAGENES_PRODUCTOResult unTipoComplejo)
        {
            ImagenProducto unaImagen = new ImagenProducto();
            unaImagen.idImagen = (int)unTipoComplejo.ID_IMAGEN;
            unaImagen.idProducto = (int)unTipoComplejo.ID_PRODUCTO;
            unaImagen.nombreImagen = unTipoComplejo.NOMBRE_IMAGEN;
            unaImagen.rutaImagen = unTipoComplejo.RUTA_IMAGEN;

            return unaImagen;
        }
    }
}
