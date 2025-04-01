using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2taldea
{
    internal class KomandakKudeatzailea
    {
        private readonly ISessionFactory sessionFactory;

        public KomandakKudeatzailea(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        // Método para obtener todas las mesas
        public static List<Mahaia> ObtenerMesas(ISessionFactory sessionFactory)
        {
            try
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.CreateQuery("FROM Mahaia").List<Mahaia>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las mesas: {ex.Message}");
                return new List<Mahaia>();
            }
        }

        // Método para cargar platos por categoría
        public List<Platera> CargarPlatos(string categoria)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                return session.QueryOver<Platera>()
                              .Where(p => p.PlateraMota == categoria)
                              .List()
                              .ToList();
            }
        }

        // Método para guardar un pedido y activarlo
        public static void GuardarEskaera(ISessionFactory sessionFactory, int mahaila_id, Dictionary<string, (int cantidad, float precio)> resumen, int langilea_id)
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    // Verificar si la mesa existe
                    var mesa = session.Get<Mahaia>(mahaila_id);
                    if (mesa == null)
                    {
                        throw new Exception($"No se encontró la mesa con ID {mahaila_id}.");
                    }

                    // Obtener el siguiente número de pedido
                    int nuevoEskaeraZenb = ObtenerNuevoEskaeraZenb(session);

                    // Crear la nueva 'Eskaera' (pedido)
                    Eskaera nuevaEskaera = new Eskaera
                    {
                        Id = nuevoEskaeraZenb,
                        Mahaila = mesa, // Relacionar el pedido con la mesa
                        Langilea = session.Get<Langilea>(langilea_id),
                        Egoera = true // Estado del pedido (activo)
                    };

                    session.Save(nuevaEskaera); // Guardar la 'Eskaera' (el pedido en sí)

                    // Iterar a través de los platos en el resumen del pedido
                    foreach (var item in resumen)
                    {
                        var nombrePlato = item.Key;
                        var cantidad = item.Value.cantidad;
                        var precio = item.Value.precio;

                        // Buscar el plato en la tabla 'Platera'
                        var plato = session.QueryOver<Platera>()
                                           .Where(p => p.Izena == nombrePlato)
                                           .SingleOrDefault();

                        if (plato == null)
                        {
                            throw new Exception($"Plato '{nombrePlato}' no encontrado en la base de datos.");
                        }

                        // Buscar todos los productos en 'Almazena' relacionados con el plato
                        var productos = session.QueryOver<Almazena>()
                                               .Where(p => p.Izena == plato.Izena)
                                               .List();

                        if (productos == null || productos.Count == 0)
                        {
                            throw new Exception($"No se encontró un producto asociado al plato '{nombrePlato}' en la tabla Almazena.");
                        }

                        // Verificar si hay stock suficiente
                        int stockTotalDisponible = productos.Sum(p => p.Stock);
                        if (stockTotalDisponible < cantidad)
                        {
                            throw new Exception($"Stock insuficiente para '{nombrePlato}'. Disponible: {stockTotalDisponible}, solicitado: {cantidad}");
                        }

                        // Reducir el stock en los productos
                        int cantidadRestante = cantidad;
                        foreach (var producto in productos)
                        {
                            if (cantidadRestante == 0)
                                break;

                            int reducirEnEsteProducto = Math.Min(producto.Stock, cantidadRestante);
                            producto.Stock -= reducirEnEsteProducto;
                            cantidadRestante -= reducirEnEsteProducto;
                            session.Update(producto); // Actualizar la cantidad de stock
                        }

                        // Crear la relación entre la 'Eskaera' (pedido) y los platos en la tabla 'EskaeraPlatera'
                        for (int i = 0; i < cantidad; i++) // Guardar los platos según la cantidad pedida
                        {
                            EskaeraPlatera nuevaEskaeraPlatera = new EskaeraPlatera
                            {
                                Eskaera = nuevaEskaera,  // Relacionar el pedido con el plato
                                Platera = plato,         // Relacionar el plato con el pedido
                                NotaGehigarriak = null,  // Puedes agregar una nota adicional aquí
                                AteratzeOrdua = null,    // Hora de entrega, si es necesario
                                Egoera = true // Estado del plato (en proceso)
                            };

                            session.Save(nuevaEskaeraPlatera); // Guardar la relación en la tabla 'EskaeraPlatera'
                        }
                    }

                    transaction.Commit(); // Confirmar la transacción
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Revertir cambios en caso de error
                    Console.WriteLine($"Error al guardar el pedido: {ex.Message}");
                    throw;
                }
            }
        }








        // Método para desactivar los pedidos de una mesa
        public static void BorrarPedidos(ISessionFactory sessionFactory, int mahaila_id)
        {
            try
            {
                using (ISession session = sessionFactory.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var pedidosParaActualizar = session.QueryOver<Eskaera>()
                                                        .Where(e => e.Mahaila.Id == mahaila_id && e.Egoera == true)
                                                        .List();

                    foreach (var pedido in pedidosParaActualizar)
                    {
                        pedido.Egoera = false;
                        session.Update(pedido);
                    }

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al desactivar los pedidos: {ex.Message}");
            }
        }

        // Método para obtener el siguiente número de pedido
        private static int ObtenerNuevoEskaeraZenb(ISession session)
        {
            var lastEskaera = session.QueryOver<Eskaera>()
                                     .OrderBy(e => e.Id).Desc
                                     .Take(1)
                                     .SingleOrDefault();

            return (lastEskaera?.Id ?? 0) + 1;
        }

        // Método para cargar el resumen de un pedido activo
        public static string CargarResumen(ISessionFactory sessionFactory, int mahaila_id)
        {
            try
            {
                using (ISession session = sessionFactory.OpenSession())
                {
                    // Obtener los pedidos activos de la mesa
                    var pedidosActivos = session.QueryOver<Eskaera>()
                                                .Where(e => e.Mahaila.Id == mahaila_id && e.Egoera == true)
                                                .List();

                    if (pedidosActivos == null || pedidosActivos.Count == 0)
                    {
                        return "Ez dago komandarik mahai honetarako."; // No hay pedido activo
                    }

                    Dictionary<string, (int cantidad, float precio)> resumen = new();

                    // Recorrer los pedidos activos
                    foreach (var pedido in pedidosActivos)
                    {
                        // Obtener los platos asociados al pedido desde la tabla eskaera_platera
                        var platosPedido = session.QueryOver<EskaeraPlatera>()
                                                  .Where(ep => ep.Eskaera.Id == pedido.Id) // Relacionamos con el pedido
                                                  .List();

                        foreach (var platoPedido in platosPedido)
                        {
                            var platera = platoPedido.Platera; // Accedemos al plato directamente desde la relación

                            if (platera == null) continue;

                            string nombrePlato = platera.Izena;
                            float precioPlato = (float)platera.Prezioa;

                            if (resumen.ContainsKey(nombrePlato))
                            {
                                resumen[nombrePlato] = (resumen[nombrePlato].cantidad + 1, precioPlato);
                            }
                            else
                            {
                                resumen[nombrePlato] = (1, precioPlato);
                            }
                        }
                    }

                    // Crear el resumen de los pedidos
                    string resumenTexto = "Komandaren laburpena:\n\n";
                    float total = 0;

                    foreach (var item in resumen)
                    {
                        float subtotal = item.Value.cantidad * item.Value.precio;
                        resumenTexto += $"- {item.Key}: {item.Value.cantidad} x {item.Value.precio:C2} = {subtotal:C2}\n";
                        total += subtotal;
                    }

                    resumenTexto += $"\nTotala: {total:C2}";

                    return resumenTexto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Errorea laburpena kargatzean: {ex.Message}", ex);
            }
        }
    }
}
