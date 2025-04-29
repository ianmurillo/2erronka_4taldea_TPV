using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2taldea
{
    internal class LoginKudeatzailea
    {
        public static bool LoginGerente(string userName, string password, ISessionFactory sessionFactory)
        {
            try
            {
                using (var session = sessionFactory.OpenSession())
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        string hql = @"SELECT COUNT(*) 
                                       FROM Langilea 
                                       WHERE izena = :userName 
                                         AND pasahitza = :password 
                                         AND nivel_permisos = 0";

                        var count = session.CreateQuery(hql)
                                           .SetParameter("userName", userName)
                                           .SetParameter("password", password)
                                           .UniqueResult<long>();

                        transaction.Commit();
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Errorea kontsultan: {ex.Message}",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea sesioan: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static bool LoginSukaldaria(string userName, string password, ISessionFactory sessionFactory)
        {
            try
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    string hql = @"SELECT COUNT(*) 
                                   FROM Langilea 
                                   WHERE izena = :userName 
                                     AND pasahitza = :password 
                                     AND nivel_permisos = 1";

                    var count = session.CreateQuery(hql)
                                       .SetParameter("userName", userName)
                                       .SetParameter("password", password)
                                       .UniqueResult<long>();

                    transaction.Commit();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Errorea kontsultan: {ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Errorea sesioan: {ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        }

        internal static bool LoginZerbitzaria(string userName, string password, ISessionFactory sessionFactory)
        {
            try
            {
                using (var session = sessionFactory.OpenSession())
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        string hql = @"SELECT COUNT(*) 
                                   FROM Langilea 
                                   WHERE izena = :userName 
                                     AND pasahitza = :password 
                                     AND nivel_permisos = 2";

                        var count = session.CreateQuery(hql)
                                           .SetParameter("userName", userName)
                                           .SetParameter("password", password)
                                           .UniqueResult<long>();

                        transaction.Commit();
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Errorea kontsultan: {ex.Message}",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea sesioan: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool ObtenerPermisoTxatDelUsuario(string userName, ISessionFactory sessionFactory)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                var langilea = session.QueryOver<Langilea>()
                                     .Where(l => l.Izena == userName) // Usa "Izena" para buscar por nombre
                                     .SingleOrDefault();

                if (langilea != null)
                {
                    return langilea.Txat;  // Devuelve el valor de la columna Txat
                }
                else
                {
                    throw new Exception($"No se encontró el trabajador con el nombre de usuario '{userName}'.");
                }
            }
        }

        public static int ObtenerIdDelUsuario(string userName, ISessionFactory sessionFactory)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                var langilea = session.QueryOver<Langilea>()
                                     .Where(l => l.Izena == userName) // Usa "Izena" en lugar de "UserName"
                                     .SingleOrDefault();

                if (langilea != null)
                {
                    return langilea.Id;  // Devolver el langilea_id
                }
                else
                {
                    throw new Exception($"No se encontró el trabajador con el nombre de usuario '{userName}'.");
                }
            }
        }

    }
}
