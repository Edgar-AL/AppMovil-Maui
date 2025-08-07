using BackendEnterprisingsApp.AccesoDatos;
using BackendEnterprisingsApp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp.Logica
{
    public class LogCategoria
    {
        public ResInsertarCategoria insertarCategoria(ReqInsertarCategoria req)  
        {
            ResInsertarCategoria res = new ResInsertarCategoria();
            short tipoRegistro = 0; // 1 Exitoso - 2 Error en Lógica - 3 en el catch

            try
            {
                if (String.IsNullOrEmpty(req.categoria.nombreCategoria))
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Nombre de categoría faltante");
                    tipoRegistro = 2;
                }

                if (!res.listaDeErrores.Any())
                {
                    using (conexionbdDataContext linq = new conexionbdDataContext())
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_INGRESAR_CATEGORIA(req.categoria.nombreCategoria, ref idReturn, ref idError, ref errorBd);

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
                tipoRegistro = 3; // No Exitoso
            }

            return res;
        }

        public ResObtenerCategoria MostrarCategorias()
        {
            ResObtenerCategoria res = new ResObtenerCategoria();
            short tipoRegistro = 0; // 1 Exitoso - 2 Error en Lógica - 3 en el catch
            List<Categoria> categorias = new List<Categoria>();

            try
            {
                using (conexionbdDataContext linq = new conexionbdDataContext())
                {
                    var categoriasDB = linq.SP_OBTENER_CATEGORIA();

                    foreach (var categoriaDB in categoriasDB)
                    {
                        Categoria categoria = new Categoria
                        {
                            idCategoria = categoriaDB.ID_CATEGORIA,
                            nombreCategoria = categoriaDB.NOMBRE_CATEGORIA,
                            activo = (int)categoriaDB.ACTIVO
                        };

                        categorias.Add(categoria);
                    }
                }

                res.resultado = true;
                res.listaDeCategorias = categorias; // Asigna la lista de categorías al objeto ResCategoria
                tipoRegistro = 1;
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores.Add("Error interno");
                tipoRegistro = 3; // No Exitoso
            }

            return res;
        }


        public ResActualizarCategoria actualizarCategoria(ReqActualizarCategoria req)
        {
            ResActualizarCategoria res = new ResActualizarCategoria();
            short tipoRegistro = 0; // 1 Exitoso - 2 Error en Lógica - 3 en el catch

            try
            {
                if (req.categoria.idCategoria <= 0)
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("ID de categoría faltante o inválido");
                    tipoRegistro = 2;
                }
                else if (String.IsNullOrEmpty(req.categoria.nombreCategoria))
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Nombre de categoría faltante");
                    tipoRegistro = 2;
                }
                else
                {
                    using (conexionbdDataContext linq = new conexionbdDataContext())
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_ACTUALIZAR_CATEGORIA(req.categoria.idCategoria, req.categoria.nombreCategoria, ref idReturn, ref idError, ref errorBd);

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
                tipoRegistro = 3; // No Exitoso
            }

            return res;
        }

        public ResEliminarCategoria eliminarCategoria(ReqEliminarCategoria req)
        {
            ResEliminarCategoria res = new ResEliminarCategoria();
            short tipoRegistro = 0; // 1 Exitoso - 2 Error en Lógica - 3 en el catch

            try
            {
                if (req.categoria.idCategoria <= 0)
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("ID de categoría inválido");
                    tipoRegistro = 2;
                }
                else
                {
                    using (conexionbdDataContext linq = new conexionbdDataContext())
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_ELIMINAR_CATEGORIA(req.categoria.idCategoria, ref idReturn, ref idError, ref errorBd);
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
                tipoRegistro = 3; // No Exitoso
            }

            return res;
        }

        public ResActivarCategoria activarCategoria(ReqActivarCategoria req)
        {
            ResActivarCategoria res = new ResActivarCategoria();
            short tipoRegistro = 0; // 1 Exitoso - 2 Error en Lógica - 3 en el catch

            try
            {
                if (req.categoria.idCategoria <= 0)
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("ID de categoría faltante o inválido");
                    tipoRegistro = 2;
                }
                else
                {
                    using (conexionbdDataContext linq = new conexionbdDataContext())
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_ACTIVAR_CATEGORIA(req.categoria.idCategoria, ref idReturn, ref idError, ref errorBd);
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
                tipoRegistro = 3; // No Exitoso
            }

            return res;
        }





    }
}
