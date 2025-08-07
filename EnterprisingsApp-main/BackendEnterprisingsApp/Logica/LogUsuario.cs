using BackendEnterprisingsApp.AccesoDatos;
using BackendEnterprisingsApp.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BackendEnterprisingsApp.Logica;
using BackendEnterprisingsApp.Utilitarios;

namespace EnterprisingApp.Logica
{
    public class LogUsuario
    {

        public ResRegistrarUsuario registroUsuario(ReqRegistrarUsuario req)
        {
            short tipoRegistro = 0;
            ResRegistrarUsuario res = new ResRegistrarUsuario();
            LogEncriptacion encrip = new LogEncriptacion();

            try
            {
                if (String.IsNullOrEmpty(req.usuario.Nombre))
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Nombre faltante");
                    tipoRegistro = 2;
                }
                if (String.IsNullOrEmpty(req.usuario.Apellido))
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Apellido faltante");
                    tipoRegistro = 2;
                }
                if (String.IsNullOrEmpty(req.usuario.direccion))
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Direccion faltante");
                    tipoRegistro = 2;
                }
                if (String.IsNullOrEmpty(req.usuario.telefono))
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Telefono faltante");
                    tipoRegistro = 2;
                }
                if (String.IsNullOrEmpty(req.usuario.correo))
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Correo faltante");
                    tipoRegistro = 2;
                }
                if (String.IsNullOrEmpty(req.usuario.contrasenia))
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Contraseña faltante");
                    tipoRegistro = 2;
                }
                if (!res.listaDeErrores.Any())
                {
                    using (conexionbdDataContext Linq = new conexionbdDataContext())
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";
                        DateTime ahora = DateTime.UtcNow;
                        int tipo = 0;
                        string numeroVerificacion = GenerarCodigoVerificacion();

                        if (req.usuario.TipoRol == "Admin")
                        {
                            tipo = 1;
                        }
                        
                        else if (req.usuario.TipoRol == "Usuario")
                        {
                            tipo = 3;
                        }

                        Linq.SP_INGRESAR_USUARIO(tipo, req.usuario.Nombre, req.usuario.Apellido, req.usuario.direccion, req.usuario.telefono, req.usuario.correo, encrip.Encrypt(req.usuario.contrasenia), numeroVerificacion, ahora, 0, ref idReturn, ref idError, ref errorBd);

                        if (idError != null && idError != 0)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorBd);
                            tipoRegistro = 2;
                        }
                        else
                        {
                            res.resultado = true;
                            tipoRegistro = 1;

                            string codigoVerificacion = "";
                            string errorDescripcion = "";
                            DateTime? marcaTiempo = null;
                            Linq.SP_OBTENER_NUMERO_VERIFICACION_POR_CORREO(req.usuario.correo, ref codigoVerificacion, ref marcaTiempo, ref idError, ref errorDescripcion);

                            if (idError == 0)
                            {
                                EnviarCorreo(req.usuario.correo, codigoVerificacion);
                            }
                            else
                            {
                                res.resultado = false;
                                res.listaDeErrores.Add(errorDescripcion);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Error interno: " + ex.Message);
                tipoRegistro = 3; //No Exitoso
            }
            finally
            {
                Utilitarios.crearBitacora(res.listaDeErrores, tipoRegistro, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }
            return res;
        }

        public string GenerarCodigoVerificacion()
        {
            int longitud = 5;
            Guid miGuid = Guid.NewGuid();
            string token = Convert.ToBase64String(miGuid.ToByteArray());
            return token.Replace("=", "").Replace("+", "").Substring(0, longitud);
        }

        public void EnviarCorreo(string correoDestino, string codigoVerificacion)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("enterprisingapp@gmail.com", "qiyz cemy dpvv shxj ");

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("enterprisingapp@gmail.com");
                    mailMessage.To.Add(correoDestino);
                    mailMessage.Subject = "Código de Verificación";
                    mailMessage.Body = "El código para activar tu cuenta es: " + codigoVerificacion;

                    client.Send(mailMessage);
                }
            }
        }

        public ResRegistrarUsuario ActualizarUsuario(ReqRegistrarUsuario req)
        {
            ResRegistrarUsuario res = new ResRegistrarUsuario();
            short tipoRegistro = 0;

            try
            {
                int? idReturn = 0;
                int? errorId = 0;
                string errorDescripcion = "";

                using (var Linq = new conexionbdDataContext())
                {
                    if (string.IsNullOrWhiteSpace(req.usuario.correo) ||
                        string.IsNullOrWhiteSpace(req.usuario.Nombre) ||
                        string.IsNullOrWhiteSpace(req.usuario.Apellido) ||
                        string.IsNullOrWhiteSpace(req.usuario.direccion) ||
                        string.IsNullOrWhiteSpace(req.usuario.telefono))
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Al menos un campo debe ser especificado para la actualización.");
                        tipoRegistro = 3;
                    }
                    else
                    {
                        Linq.SP_ACTUALIZAR_USUARIO(req.usuario.correo, req.usuario.Nombre, req.usuario.Apellido, req.usuario.direccion, req.usuario.telefono, ref idReturn, ref errorId, ref errorDescripcion);

                        if (idReturn == -1)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add("No se especificaron campos para actualizar.");
                            tipoRegistro = 3; 
                        }
                        else if (errorId != 0)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add("Error al actualizar la información del usuario: " + errorDescripcion);
                            tipoRegistro = 3; 
                        }
                        else
                        {
                            res.resultado = true;
                            tipoRegistro = 1;
                        }
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
                Utilitarios.crearBitacora(res.listaDeErrores, tipoRegistro, GetType().Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }

            return res;
        }


        public ResActualizarContrasenia ActualizarContrasena(ReqActualizarContrasenia req)
        {
            ResActualizarContrasenia res = new ResActualizarContrasenia();
            short tipoRegistro = 0;
            LogEncriptacion encriptacion= new LogEncriptacion();
            try
            {
                int? idReturn = 0;
                int? errorId = 0;
                string errorDescripcion = "";

                using (var Linq = new conexionbdDataContext())
                {
               
                    int? validateReturn = 0;

                    Linq.SP_VALIDAR_CONTRASENA(req.correo, encriptacion.Encrypt(req.contraseniaActual), ref validateReturn, ref errorId, ref errorDescripcion);

                    if (validateReturn != 1)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("La contraseña actual no coincide.");
                        tipoRegistro = 3; 
                    }
                    else
                    {
                    
                        Linq.SP_ACTUALIZAR_CONTRASENA_USUARIO(req.correo, encriptacion.Encrypt(req.contraseniaNueva), ref idReturn, ref errorId, ref errorDescripcion);

                        if (idReturn == -1)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorDescripcion);
                            tipoRegistro = 3; 
                        }
                        else if (errorId != 0)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add("Error al actualizar la contraseña: " + errorDescripcion);
                            tipoRegistro = 3; 
                        }
                        else
                        {
                            res.resultado = true;
                            tipoRegistro = 1;
                        }
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
                Utilitarios.crearBitacora(res.listaDeErrores, tipoRegistro, GetType().Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }

            return res;
        }


        public ResActualizarContraseñaOlvidada ActualizarPorOlvido(ReqActualizarContraseñaOlvidada req)
        {
            ResActualizarContraseñaOlvidada res = new ResActualizarContraseñaOlvidada();
            short tipoRegistro = 0;
            LogEncriptacion encriptacion = new LogEncriptacion();
       
            try
            {
                int? idReturn = 0;
                int? errorId = 0;
                string errorDescripcion = "";


                using (var Linq = new conexionbdDataContext())
                {
                    
                    Linq.SP_ACTUALIZAR_CONTRASENA_USUARIO_OLVIDADA(req.correo, req.numeroVerificacion, encriptacion.Encrypt(req.contrasenia), ref idReturn, ref errorId, ref errorDescripcion);

                    if (idReturn == 1)
                    {
                        res.resultado = true;
                        tipoRegistro = 1;
                    }
                    else
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add(errorDescripcion);
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
                Utilitarios.crearBitacora(res.listaDeErrores, tipoRegistro, GetType().Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req), JsonConvert.SerializeObject(res));
            }

            return res;
        }



        public ResObtenerUsuarioPorCorreo ObtenerUsuarioPorCorreo(ReqObtnerUsuarioPorCorreo req)
        {
            ResObtenerUsuarioPorCorreo res = new ResObtenerUsuarioPorCorreo();
            short tipoRegistro = 0;

            try
            {
                string nombreUsuario = "";
                string apellidosUsuario = "";
                string direccion = "";
                string telefono = "";
                string nombreTipoRol = "";
                int? activo = 0;
                int? idReturn = 0;
                int? errorId = 0;
                string errorDescripcion = "";

                using (var Linq = new conexionbdDataContext())
                {
                    Linq.SP_OBTENER_USUARIO_POR_CORREO(req.correo, ref nombreUsuario, ref apellidosUsuario, ref direccion, ref telefono, ref nombreTipoRol, ref activo, ref idReturn, ref errorId, ref errorDescripcion);

                    if (errorId != 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Error al obtener el usuario: " + errorDescripcion);
                        tipoRegistro = 3;
                    }
                    else if (idReturn == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("El usuario especificado no existe.");
                        tipoRegistro = 3;
                    }
                    else
                    {
                        res.resultado = true;
                        res.Usuario = new Usuario
                        {
                            correo = req.correo,
                            Nombre = nombreUsuario,
                            Apellido = apellidosUsuario,
                            direccion = direccion,
                            telefono = telefono,
                            TipoRol = nombreTipoRol,
                            activo = (int)activo
                        };
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
                Utilitarios.crearBitacora(res.listaDeErrores, tipoRegistro, GetType().Name, MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(req.correo), JsonConvert.SerializeObject(res));
            }

            return res;
        }



    }
}
