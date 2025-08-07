using BackendEnterprisingsApp.AccesoDatos;
using BackendEnterprisingsApp.Entidades;
using EnterprisingApp.Logica;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp.Logica
{
    public class LogActivacionCuenta
    {
        public ResActivacionCuenta ActivarCuenta(ReqActivacionCuenta req)
        {
            ResActivacionCuenta res = new ResActivacionCuenta();
            short tipoRegistro = 0;

            try
            {
                int? idReturn = 0;
                int? errorId = 0;
                string errorDescripcion = "";

                using (var Linq = new conexionbdDataContext())
                {
                    DateTime ahora = DateTime.UtcNow;

                    Linq.SP_ACTIVAR_USUARIO(req.correo, req.numeroVerificacion,ahora, ref idReturn, ref errorId, ref errorDescripcion);

                    if (errorId == 2) 
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("El código de verificación ha expirado.");
                        tipoRegistro = 3; 
                    }
                    else if (idReturn == -1)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("El usuario ya está verificado o no existe.");
                        tipoRegistro = 3; 
                    }
                    else if (errorId != 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Error al activar la cuenta: " + errorDescripcion);
                        tipoRegistro = 3; 
                    }
                    else
                    {
                        res.resultado = true;
                        tipoRegistro = 1;
                    }
                }
            }
            catch (Exception)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Error interno");
                tipoRegistro = 3; 
            }
            finally
            {
    
                Utilitarios.Utilitarios.crearBitacora(res.listaDeErrores, tipoRegistro, GetType().Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }

            return res;
        }



        public ResActualizarCodigoVerificacion ActualizarCodigoVerificacion(ReqActualizarCodigoVerificacion req)
        {
            ResActualizarCodigoVerificacion res = new ResActualizarCodigoVerificacion();
            short tipoRegistro = 0;
            LogUsuario user = new LogUsuario();
            try
            {
                int? idReturn = 0;
                int? errorId = 0;
                string errorDescripcion = "";

             
                string nuevoCodigoVerificacion = user.GenerarCodigoVerificacion();
                DateTime marcaTiempoVerificacion = DateTime.UtcNow;

                using (var Linq = new conexionbdDataContext())
                {
                    Linq.SP_ACTUALIZAR_CODIGO_VERIFICACION(req.correo, nuevoCodigoVerificacion, marcaTiempoVerificacion, ref idReturn, ref errorId, ref errorDescripcion);

                    if (idReturn == -1)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Error al actualizar el código de verificación: " + errorDescripcion);
                        tipoRegistro = 3; 
                    }
                    else if (errorId == 0)
                    {
                        res.resultado = true;
                        res.listaDeErrores.Add("código de verificación reenviado: " + errorDescripcion);
                        tipoRegistro = 1;
                        user.EnviarCorreo(req.correo, nuevoCodigoVerificacion);
                    }
                    else if (errorId != 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Error al actualizar el código de verificación: " + errorDescripcion);
                        tipoRegistro = 3; 
                    }

                }
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Error interno: " + ex.Message);
                tipoRegistro = 3; 
            }
            finally
            {
                Utilitarios.Utilitarios.crearBitacora(res.listaDeErrores, tipoRegistro, GetType().Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }

            return res;
        }


        public ResEliminarUsuario EliminarUsuario(ReqEliminarUsuario req)
        {
            ResEliminarUsuario res = new ResEliminarUsuario();
            short tipoRegistro = 0;
            LogEncriptacion encriptacion = new LogEncriptacion();
            try
            {
                string contrasenaEncriptada = encriptacion.Encrypt(req.contrasenia);

                using (var Linq = new conexionbdDataContext())
                {
                    int? idReturn = 0;
                    int? errorId = 0;
                    string errorDescripcion = "";

                    Linq.SP_ELIMINAR_USUARIO(req.correo, contrasenaEncriptada, ref idReturn, ref errorId, ref errorDescripcion);

                    if (idReturn == -1)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add(errorDescripcion);
                        tipoRegistro = 3; 
                    }
                    else if (errorId != 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Error al eliminar usuario: " + errorDescripcion);
                        tipoRegistro = 3; 
                    }
                    else
                    {
                        res.resultado = true;
                        tipoRegistro = 1; 
                    }
                }
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Error interno: " + ex.Message);
                tipoRegistro = 3; 
            }
            finally
            {
                Utilitarios.Utilitarios.crearBitacora(res.listaDeErrores, tipoRegistro, GetType().Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }

            return res;
        }




    }
}
