using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace _2taldea
{
    internal class EskaeraKudeatzaile
    {
        public static void ProcesarMesa(int mahaila_id, string nombreUsuario, ISessionFactory sessionFactory)
        {
            try
            {
                using (ISession session = sessionFactory.OpenSession())
                {
                    var pedidosActivos = session.CreateQuery("FROM Eskaera WHERE MahailaId = :mahaila_id AND Egoera = true")
                                                .SetParameter("mahaila_id", mahaila_id)
                                                .List<Eskaera>();

                    if (pedidosActivos.Count == 0)
                    {
                        MessageBox.Show("No hay pedidos activos para esta mesa.", "Resumen de Mesa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Mostrar el resumen en un nuevo formulario, pasando el nombre del usuario
                    EskaeraResumenForm resumenForm = new EskaeraResumenForm(mahaila_id, pedidosActivos.ToList(), nombreUsuario, sessionFactory);
                    resumenForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea mahaia aukeratzean: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal static object HayPedidoActivo(ISessionFactory sessionFactory)
        {
            throw new NotImplementedException();
        }
    }
}
