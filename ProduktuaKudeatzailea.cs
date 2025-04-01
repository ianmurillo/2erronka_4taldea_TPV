using NHibernate.Mapping;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2taldea
{
    internal class ProduktuaKudeatzailea
    {
        public static String ProduktuaAdd(ISessionFactory sessionFactory, String izena, int stock, int min, int max)
        {
            try
            {
                using (var session = sessionFactory.OpenSession())
                using (var transaction = session.BeginTransaction())
                {
                    var produktua = new Almazena
                    {
                        Izena = izena,
                        Stock = stock,
                        Min = min,
                        Max = max
                    };

                    session.Save(produktua);
                    transaction.Commit();
                    return "true";
                    
                }
            }
            catch (Exception ex)
            {
                return $"Error al guardar el producto: {ex.Message}";
            }
        }
        public static string ProduktuaUpdate(
             ISessionFactory sessionFactory,
             Almazena produktua,
             string izena,
             int stock,
             int min,
             int max)
        {
            try
            {
                using (var session = sessionFactory.OpenSession())
                using (var transaction = session.BeginTransaction())
                {
                    produktua.Izena = izena;
                    produktua.Stock = stock;
                    produktua.Min = min;
                    produktua.Max = max;

                    session.Update(produktua);
                    transaction.Commit();
                    return "true";
                }
            }
            catch (Exception ex)
            {
                return $"Errorea produktua eguneratzean: {ex.Message}";
            }
        }

        public static List<Almazena> ObtenerProduktuak(ISessionFactory sessionFactory)
        {
            try
            {
                using (var session = sessionFactory.OpenSession())
                {
                    // Ejecutar la consulta para obtener todos los productos
                    var produktuak = session.CreateQuery("FROM Almazena").List<Almazena>().ToList();

                    return produktuak;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los productos: {ex.Message}");
                return new List<Almazena>(); // Retornar lista vacía en caso de error
            }
        }


        public static string ProduktuaDelete(ISessionFactory sessionFactory, Almazena produktua)
        {
            try
            {
                using (var session = sessionFactory.OpenSession())
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(produktua);
                    transaction.Commit();
                    return "true";
                }
            }
            catch (Exception ex)
            {
                return $"Errorea produktua ezabatzean: {ex.Message}";
            }
        }
        public static List<Almazena> FiltrarProduktuak(ISessionFactory sessionFactory, string criterio)
        {
            try
            {
                using (var session = sessionFactory.OpenSession())
                {
                    string query = criterio == "Stock"
                        ? "FROM Almazena ORDER BY Stock DESC"
                        : "FROM Almazena ORDER BY Stock DESC";

                    var produktuak = session.CreateQuery(query).List<Almazena>().ToList();
                    return produktuak;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al filtrar los productos: {ex.Message}");
                return new List<Almazena>(); 
            }
        }
        public static (List<Almazena> produktuak, List<Almazena> produktuakStockBaxua) ObtenerProduktuakConAlertas(ISessionFactory sessionFactory)
        {
            try
            {
                using (var session = sessionFactory.OpenSession())
                {
                    // Obtener todos los productos
                    var produktuak = session.CreateQuery("FROM Almazena").List<Almazena>().ToList();

                    // Filtrar productos con stock insuficiente
                    var produktuakStockBaxua = produktuak.Where(p => p.Stock < p.Min).ToList();

                    return (produktuak, produktuakStockBaxua);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los productos: {ex.Message}");
                return (new List<Almazena>(), new List<Almazena>()); // Retornar listas vacías en caso de error
            }
        }

    }

}
