using BackendEnterprisingsApp.AccesoDatos;
using BackendEnterprisingsApp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendEnterprisingsApp.Logica
{
    public class LogProducto
    {
        public ResInsertarProducto InsertarProducto(ReqInsertarProducto req)
        {
            ResInsertarProducto res = new ResInsertarProducto();
            short tipoRegistro = 0; // 1 Exitoso - 2 Error en Lógica - 3 en el catch

            try
            {
                // Validar si el nombre del producto está vacío o nulo
                if (string.IsNullOrEmpty(req.producto.nombreProducto))
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Nombre de producto faltante");
                    tipoRegistro = 2;
                }
                else
                {
                    using (conexionbdDataContext linq = new conexionbdDataContext()) 
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorDescripcion = "";

                        linq.SP_INGRESAR_PRODUCTO(req.producto.idCategoria, req.producto.idEmprendedor, req.producto.nombreProducto, req.producto.descripcion, req.producto.activo, ref idReturn, ref idError, ref errorDescripcion);

                        if (idError == null || idError == -1)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorDescripcion); 
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


        public ResObtenerProducto MostrarProductos()
        {
            ResObtenerProducto res = new ResObtenerProducto();
            short tipoRegistro = 0; // 1 Exitoso - 2 Error en Lógica - 3 en el catch
            List<Producto> productos = new List<Producto>();

            try
            {
                using (conexionbdDataContext linq = new conexionbdDataContext())
                {
                    var productosDB = linq.SP_OBTENER_PRODUCTO();

                    foreach (var productoDB in productosDB)
                    {
                        Producto producto = new Producto
                        {
                            idProducto = productoDB.ID_PRODUCTO,
                            idCategoria = (int)productoDB.ID_CATEGORIA,
                            nombreProducto = productoDB.NOMBRE_PRODUCTO,
                            descripcion = productoDB.DESCRIPCION,
                            activo = (int)productoDB.ACTIVO
                        };

                        productos.Add(producto);
                    }
                }

                res.resultado = true;
                res.listaDeErrores = new List<string>(); // Inicializa la lista de errores
                res.listaDeProductos = productos; // Asigna la lista de productos al objeto ResProducto
                tipoRegistro = 1;
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores = new List<string> { "Error interno: " + ex.Message };
                tipoRegistro = 3; // No Exitoso
            }

            return res;
        }

        public ResObtenerProductosPorEmprendedor MostrarProductosPorEmprendedor(ReqObtenerProductosPorEmprendedor req)
        {
            ResObtenerProductosPorEmprendedor res = new ResObtenerProductosPorEmprendedor();
            short tipoRegistro = 0; // 1 Exitoso - 2 Error en Lógica - 3 en el catch
            List<Producto> productosEmprendedor = new List<Producto>();

            try
            {
                using (conexionbdDataContext linq = new conexionbdDataContext())
                {
                    var productosDB = linq.SP_OBTENER_PRODUCTOS_EMPRENDEDOR1(req.idEmprendedor);

                    foreach (var productoDB in productosDB)
                    {
                        Producto producto = new Producto
                        {
                            idProducto = productoDB.ID_PRODUCTO,
                            idCategoria = (int)productoDB.ID_CATEGORIA,
                            idEmprendedor = (int)productoDB.ID_EMPRENDEDOR,
                            nombreProducto = productoDB.NOMBRE_PRODUCTO,
                            descripcion = productoDB.DESCRIPCION,
                            activo = (int)productoDB.ACTIVO
                        };

                        productosEmprendedor.Add(producto);
                    }
                }

                res.resultado = true;
                res.listaDeErrores = new List<string>(); // Inicializa la lista de errores
                res.listaProductosEmprendedor = productosEmprendedor; // Asigna la lista de productos al objeto ResProducto
                tipoRegistro = 1;
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.listaDeErrores = new List<string> { "Error interno: " + ex.Message };
                tipoRegistro = 3; // No Exitoso
            }

            return res;
        }



        public ResActivarProducto ActivarProducto(ReqActivarProducto req)
        {
            ResActivarProducto res = new ResActivarProducto();
            short tipoRegistro = 0; // 1 Exitoso - 2 Error en Lógica - 3 en el catch

            try
            {
                if (req.producto.idProducto  <= 0)
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("ID de producto inválido");
                    tipoRegistro = 2;
                }
                else
                {
                    using (conexionbdDataContext linq = new conexionbdDataContext())
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorDescripcion = "Error al intentar activar el producto";

                        linq.SP_ACTIVAR_PRODUCTO(req.producto.idProducto, ref idReturn, ref idError, ref errorDescripcion);

                        if (idReturn == null || idReturn == -1)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorDescripcion);
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

        public ResEliminarProducto EliminarProducto(ReqEliminarProducto req)
        {
            ResEliminarProducto res = new ResEliminarProducto();
            short tipoRegistro = 0; // 1 Exitoso - 2 Error en Lógica - 3 en el catch

            try
            {
                if (req.idProducto <= 0)
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("ID de producto inválido");
                    tipoRegistro = 2;
                }
                else
                {
                    using (conexionbdDataContext linq = new conexionbdDataContext())
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorDescripcion = "Error al eliminar Producto";

                        linq.SP_ELIMINAR_PRODUCTO(req.idProducto, ref idReturn, ref idError, ref errorDescripcion);

                        if (idReturn == null || idReturn == -1)
                        {
                            res.resultado = false;
                            res.listaDeErrores.Add(errorDescripcion);
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

        public ResActualizarProducto ActualizarProducto(ReqActualizarProducto req)
        {
            ResActualizarProducto res = new ResActualizarProducto();
            short tipoRegistro = 0; // 1 Exitoso - 2 Error en Lógica - 3 en el catch

            try
            {
                if (req == null || req.producto == null || req.producto.idProducto <= 0)
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("ID de producto faltante o inválido");
                    tipoRegistro = 2;
                }
                else if (req.producto.idCategoria <= 0)
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("ID de categoría faltante o inválido");
                    tipoRegistro = 2;
                }
                else if (String.IsNullOrEmpty(req.producto.nombreProducto))
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Nombre de producto faltante");
                    tipoRegistro = 2;
                }
                else
                {
                    using (conexionbdDataContext linq = new conexionbdDataContext())
                    {
                        int? idReturn = 0;
                        int? idError = 0;
                        string errorBd = "";

                        linq.SP_ACTUALIZAR_PRODUCTO(req.producto.idProducto, req.producto.idCategoria, req.producto.nombreProducto, req.producto.descripcion, req.producto.activo, ref idReturn, ref idError, ref errorBd);

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
