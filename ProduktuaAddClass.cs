using System;
using _2taldea;
using NHibernate;

public class ProduktuaAddClass
{
    private ISessionFactory sessionFactory;

    public ProduktuaAddClass(ISessionFactory sessionFactory)
    {
        this.sessionFactory = sessionFactory ?? throw new ArgumentNullException(nameof(sessionFactory));
    }

    public bool AgregarProducto(string nombre, string mota, string ezaugarria, int stock, string unitatea, int min, int max, int? createdBy, out string mensaje)
    {
        try
        {
            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var nuevoProducto = new Almazena
                {
                    Izena = nombre,
                    Mota = mota,
                    Ezaugarria = ezaugarria,
                    Stock = stock,
                    Unitatea = unitatea,
                    Min = min,
                    Max = max,
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    CreatedBy = createdBy
                };

                session.Save(nuevoProducto);
                transaction.Commit();

                mensaje = "Producto añadido correctamente.";
                return true;
            }
        }
        catch (Exception ex)
        {
            mensaje = $"Error al añadir el producto: {ex.Message}";
            return false;
        }
    }

}
