using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace _2taldea
{
    internal class EskaeraKudeatzaile2
    {
        public static void ProcesarMesa(int mahaila_id, string nombreUsuario, ISessionFactory sessionFactory)
        {
            try
            {
                using (ISession session = sessionFactory.OpenSession())
                {
                    var pedidosActivos = session.QueryOver<Eskaera>()
                                                .Where(e => e.Mahaila.Id == mahaila_id && e.Egoera == true)
                                                .List();

                    if (pedidosActivos == null || pedidosActivos.Count == 0)
                    {
                        MessageBox.Show("No hay pedidos activos para esta mesa.", "Resumen de Mesa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Mostrar el resumen en un nuevo formulario, pasando todos los pedidos activos
                    EskaeraResumenForm resumenForm = new EskaeraResumenForm(mahaila_id, pedidosActivos.ToList(), nombreUsuario, sessionFactory);
                    resumenForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar la mesa: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
