using BackendEnterprisingsApp.AccesoDatos;
using BackendEnterprisingsApp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp.Logica
{
    public class LogResena
    {
        public ResInsertarResena insertarResena(ReqInsertarResena req)
        {
            ResInsertarResena res = new ResInsertarResena();
            short tipoRegistro = 0; 
            try
            {
                if(req == null)
                {
                    res.listaDeErrores.Add("Req nulo");
                    res.resultado = false;
                }
                else
                {
                    if (req.resena.idProducto == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Producto faltante");
                        tipoRegistro = 2;
                    }
                    if (req.resena.idUsuario == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Usuario faltante");
                        tipoRegistro = 2;
                    }

                    if (req.resena.valoracion == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Valoracion faltante");
                        tipoRegistro = 2;
                    }

                    if (req.resena.resena == "")
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Reseña faltante");
                        tipoRegistro = 2;
                    }
                    else
                    {
                        conexionbdDataContext linq = new conexionbdDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_INGRESAR_RESENA(req.resena.idResena, req.resena.idProducto, req.resena.idUsuario, req.resena.valoracion, req.resena.resena, ref idReturn, ref idError, ref errorBd);

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
                res.listaDeErrores.Add("Error interno: " + ex.Message);
                tipoRegistro = 3; 
            }
            return res;
        }


        public ResActualizarResena actualizarResena(ReqActualizarResena req)
        {
            ResActualizarResena res = new ResActualizarResena();
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
                    if (req.resena.idResena == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("ID de reseña faltante");
                        tipoRegistro = 2;
                    }
                    else if (req.resena.idProducto == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Producto faltante");
                        tipoRegistro = 2;
                    }
                    if (req.resena.idUsuario == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Usuario faltante");
                        tipoRegistro = 2;
                    }

                    if (req.resena.valoracion == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Valoracion faltante");
                        tipoRegistro = 2;
                    }

                    if (req.resena.resena == "")
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Reseña faltante");
                        tipoRegistro = 2;
                    }
                    else
                    {
                        conexionbdDataContext linq = new conexionbdDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_ACTUALIZAR_RESENA(req.resena.idProducto, req.resena.idResena, req.resena.valoracion, req.resena.resena, ref idReturn, ref idError, ref errorBd);
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

        public ResEliminarResena eliminarResena(ReqEliminarResena req)
        {
            ResEliminarResena res = new ResEliminarResena();
            short tipoRegistro = 0; // 1 Exitoso - 2 Error en Lógica - 3 en el catch
            try
            {
                if (req == null)
                {
                    res.listaDeErrores.Add("Req nulo");
                    res.resultado = false;
                }
                else
                {
                    if (req.idResena == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("ID de reseña faltante");
                        tipoRegistro = 2;
                    }
                    else
                    {
                        conexionbdDataContext linq = new conexionbdDataContext();
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_ELIMINAR_RESENA(req.idResena, ref idReturn, ref idError, ref errorBd);
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

        public List<Resena> MostrarResena()
        {
            ResObtenerResena res = new ResObtenerResena();
            short tipoRegistro = 0; 
            List<Resena> resenas = new List<Resena>();

            try
            {
                using (conexionbdDataContext linq = new conexionbdDataContext())
                {
                    var resenaBD = linq.SP_OBTENER_RESENA();

                   
                    var resenasProyectadas = resenaBD.Select(resenaDB => new Resena
                    {
                        nombreProducto = resenaDB.NOMBRE_PRODUCTO,
                        nombreUsuario = resenaDB.NOMBRE_USUARIO,
                        apellidosUsuario = resenaDB.APELLIDOS_USUARIO,
                        valoracion = (int)resenaDB.VALORACION,
                        resena = resenaDB.RESENA
                    });

                    resenas.AddRange(resenasProyectadas);

                    res.resultado = true;
                    tipoRegistro = 1;
                }
            }
            catch (Exception ex)
            {
               
                res.resultado = false;
                res.listaDeErrores.Add("Error interno: " + ex.Message);
                tipoRegistro = 3;
            }

            return resenas;
        }

    }
}
