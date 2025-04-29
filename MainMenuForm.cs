using System;
using System.Windows.Forms;
using NHibernate;
using NHibernate.Cfg;

namespace _2taldea
{
    public partial class MainMenuForm : Form
    {
        private string nombreUsuario;
        private ISessionFactory sessionFactory;

        public MainMenuForm(string nombreUsuario, ISessionFactory sessionFactory)
        {
            ConfigureNHibernate();
            InitializeComponent();
            labelIzena.Text = nombreUsuario;
            this.nombreUsuario = nombreUsuario;
            this.sessionFactory = sessionFactory; // Guardar la instancia de ISessionFactory
        }

        private void ConfigureNHibernate()
        {
            try
            {
                var configuration = new Configuration();
                configuration.Configure(); // Carga la configuración desde App.config o hibernate.cfg.xml
                sessionFactory = configuration.BuildSessionFactory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea NHibernate konfiguratzean: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            labelIzena.Text = nombreUsuario;
        }

        private void btnProduktuak_Click(object sender, EventArgs e)
        {

            // Pasar `sessionFactory` al constructor de `ProduktuakForm`
            ProduktuakForm produktuakForm = new ProduktuakForm(nombreUsuario, sessionFactory);
            produktuakForm.Show();
        }

        private void btnKomandak_Click(object sender, EventArgs e)
        {
            KomandakForm komandakForm = new KomandakForm(nombreUsuario, sessionFactory);
            komandakForm.Show(); // Mostrar el formulario KomandakForm
        }




        private void btnEskaerak_Click(object sender, EventArgs e)
        {
            EskaerakForm eskaerakForm = new EskaerakForm(nombreUsuario, sessionFactory);
            eskaerakForm.Show();
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelIzena_Click(object sender, EventArgs e)
        {
        }

        private void buttonTxat_Click(object sender, EventArgs e)
        {
            if (LoginKudeatzailea.ObtenerPermisoTxatDelUsuario(nombreUsuario, sessionFactory))
            {
                txat txatForm = new txat(nombreUsuario);
                txatForm.Show();
            }else
            {
                MessageBox.Show("Error, ez duzu txatean sartzeko baimenik");
            }
            
        }

        private void btnEguraldia_Click(object sender, EventArgs e)
        {
            EguraldiaForm eguraldiaForm = new EguraldiaForm(nombreUsuario);
            eguraldiaForm.Show();
        }
    }
}
