using System;
using System.Windows.Forms;
using NHibernate;

namespace _2taldea
{
    public partial class ProduktuaAddForm : Form
    {
        private ISessionFactory sessionFactory;

        public ProduktuaAddForm(ISessionFactory sessionFactory)
        {
            InitializeComponent();
            this.sessionFactory = sessionFactory ?? throw new ArgumentNullException(nameof(sessionFactory));

        }

        private void btnGorde_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtIzena.Text) ||
                    string.IsNullOrWhiteSpace(txtMota.Text) ||
                    string.IsNullOrWhiteSpace(txtEzaugarria.Text) ||
                    string.IsNullOrWhiteSpace(txtStock.Text) ||
                    string.IsNullOrWhiteSpace(txtUnitatea.Text) ||
                    string.IsNullOrWhiteSpace(txtMin.Text) ||
                    string.IsNullOrWhiteSpace(txtMax.Text))
                {
                    MessageBox.Show("Bete eremu guztiak mesedez.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string izena = txtIzena.Text;
                string mota = txtMota.Text;
                string ezaugarria = txtEzaugarria.Text;
                int stock = int.Parse(txtStock.Text);
                string unitatea = txtUnitatea.Text;
                int min = int.Parse(txtMin.Text);
                int max = int.Parse(txtMax.Text);

                // Simulamos CreatedBy = 1 (en la práctica debería venir del usuario logueado)
                int createdBy = 1;

                var add = new ProduktuaAddClass(sessionFactory);
                if (add.AgregarProducto(izena, mota, ezaugarria, stock, unitatea, min, max, createdBy, out string mensaje))
                {
                    MessageBox.Show(mensaje, "Informazioa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(mensaje, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea: " + ex.Message);
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnUtzi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProduktuaAddForm_Load(object sender, EventArgs e)
        {

        }

        private void txtMin_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
