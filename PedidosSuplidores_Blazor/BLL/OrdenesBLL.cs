using Microsoft.EntityFrameworkCore;
using PedidosSuplidores_Blazor.DAL;
using PedidosSuplidores_Blazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PedidosSuplidores_Blazor.BLL
{
    public class OrdenesBLL
    {
        public static bool Guardar(Ordenes orden)
        {
            if (!Existe(orden.OrdenId))
                return Insertar(orden);
            else
                return Modificar(orden);
        }

        private static bool Insertar(Ordenes orden)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Ordenes.Add(orden);
                paso = contexto.SaveChanges() > 0;

                Afectar(orden, 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        private static bool Modificar(Ordenes orden)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM MorasDetalle Where MoraId={orden.OrdenId}");

                foreach (var item in orden.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                List<OrdenesDetalle> detalle = orden.Detalle;
                foreach (OrdenesDetalle det in detalle)
                {
                    Productos producto = ProductosBLL.Buscar(det.ProductoId);
                    if (producto != null)
                    {
                        producto.Inventario -= det.Cantidad;
                        ProductosBLL.Guardar(producto);
                    }
                }
                Afectar(orden, 1);



                contexto.Entry(orden).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var orden = OrdenesBLL.Buscar(id);

                if (orden != null)
                {
                    contexto.Ordenes.Remove(orden);
                    paso = contexto.SaveChanges() > 0;
                    List<OrdenesDetalle> detalle = orden.Detalle;
                    foreach (OrdenesDetalle det in detalle)
                    {
                        Productos producto = ProductosBLL.Buscar(det.ProductoId);
                        if (producto != null)
                        {
                            producto.Inventario -= det.Cantidad;
                            ProductosBLL.Guardar(producto);
                        }
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static Ordenes Buscar(int id)
        {
            Ordenes orden = new Ordenes();
            Contexto contexto = new Contexto();

            try
            {
                orden = contexto.Ordenes.Include(O => O.Detalle)
                    .Where(O => O.OrdenId == id)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return orden;
        }

        /*public static List<Ordenes> GetList(Expression<Func<Ordenes, bool>> criterio)
        {
            List<Ordenes> Lista = new List<Ordenes>();
            Contexto contexto = new Contexto();

            try
            {
                Lista = contexto.Moras.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }*/
        public static List<Ordenes> GetList()
        {
            List<Ordenes> lista = new List<Ordenes>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Ordenes.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Ordenes.Any(O => O.OrdenId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

        public static void Afectar(Ordenes orden, int efecto)
        {

            try
            {
                List<OrdenesDetalle> detalle = orden.Detalle;
                foreach (OrdenesDetalle det in detalle)
                {
                    Productos producto = ProductosBLL.Buscar(det.ProductoId);
                    if (producto != null && efecto == 1)                 
                        producto.Inventario += det.Cantidad;
                    else
                        producto.Inventario -= det.Cantidad;


                    ProductosBLL.Guardar(producto);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
