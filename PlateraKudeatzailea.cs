using NHibernate;

namespace _2taldea
{
        public static class PlateraKudeatzailea
        {
            public static string PlateraAdd(ISessionFactory sessionFactory, string izena, string platera_mota, float prezioa, int menu)
            {
                try
                {
                    // Usamos un bloque using para asegurar que la sesión y transacción se cierren correctamente
                    using (ISession session = sessionFactory.OpenSession())
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        // Crear un nuevo objeto Plato
                        Platera plato = new Platera
                        {
                            Izena = izena,
                            PlateraMota = platera_mota,
                            Prezioa = prezioa,
                            Menu = menu
                        };

                        // Guardar el plato en la base de datos
                        session.Save(plato);

                        // Confirmamos la transacción
                        transaction.Commit();

                        // Retornamos un mensaje de éxito
                        return "true";
                    }
                }
                catch (Exception ex)
                {
                    // Si ocurre algún error, lo retornamos
                    return "Error al guardar el plato: " + ex.Message;
                }
            }

            // Aquí puedes añadir más métodos para otras operaciones con los platos, como actualizar, eliminar, etc.
        }

    }