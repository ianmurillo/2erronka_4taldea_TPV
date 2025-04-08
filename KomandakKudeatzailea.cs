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
                    var mesa = session.Get<Mahaia>(mahaila_id);
                    if (mesa == null) throw new Exception($"No se encontró la mesa con ID {mahaila_id}.");

                    // Verificar si ya existe una Eskaera activa para esta mesa
                    var eskaeraExistente = session.QueryOver<Eskaera>()
                        .Where(e => e.Mahaila.Id == mahaila_id && e.Egoera == true)
                        .SingleOrDefault();

                    Eskaera eskaera;
                    if (eskaeraExistente != null)
                    {
                        eskaera = eskaeraExistente; // Usar la misma Eskaera activa
                    }
                    else
                    {
                        // Si no hay un pedido activo, crear uno nuevo
                        int nuevoEskaeraZenb = ObtenerNuevoEskaeraZenb(session);
                        eskaera = new Eskaera
                        {
                            Id = nuevoEskaeraZenb,
                            Mahaila = mesa,
                            Langilea = session.Get<Langilea>(langilea_id),
                            Egoera = true
                        };
                        session.Save(eskaera);
                    }

                    foreach (var item in resumen)
                    {
                        var nombrePlato = item.Key;
                        var cantidad = item.Value.cantidad;

                        var plato = session.QueryOver<Platera>()
                                           .Where(p => p.Izena == nombrePlato)
                                           .SingleOrDefault();
                        if (plato == null) throw new Exception($"Plato '{nombrePlato}' no encontrado.");

                        var ingredientes = session.QueryOver<AlmazenaPlatera>()
                                                  .Where(ap => ap.Platera.Id == plato.Id)
                                                  .List();

                        foreach (var ingrediente in ingredientes)
                        {
                            var producto = session.Get<Almazena>(ingrediente.Almazena.Id);
                            if (producto == null) throw new Exception($"Producto asociado al plato '{nombrePlato}' no encontrado.");

                            int totalNecesario = ingrediente.Kantitatea * cantidad;
                            if (producto.Stock < totalNecesario)
                                throw new Exception($"Stock insuficiente de '{producto.Izena}' para '{nombrePlato}'. Necesario: {totalNecesario}, disponible: {producto.Stock}");

                            producto.Stock -= totalNecesario;
                            session.Update(producto);
                        }

                        for (int i = 0; i < cantidad; i++)
                        {
                            EskaeraPlatera nuevaEskaeraPlatera = new EskaeraPlatera
                            {
                                Eskaera = eskaera,  // Usar la Eskaera existente o recién creada
                                Platera = plato,
                                Egoera = true,
                                EskaeraOrdua = DateTime.Now
                            };
                            session.Save(nuevaEskaeraPlatera);
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
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
