using System;
using System.Windows.Forms;
using NHibernate;

namespace _2taldea
{
    public partial class ProduktuaEditForm : Form
    {
        private Almazena produktua;
        private ISessionFactory sessionFactory;

        public ProduktuaEditForm(Almazena produktua, ISessionFactory sessionFactory)
        {
            InitializeComponent();
            this.produktua = produktua;
            this.sessionFactory = sessionFactory;

            // Cargar los datos del producto en los controles
            txtIzena.Text = produktua.Izena;
            txtStock.Text = produktua.Stock.ToString();
            txtMin.Text = produktua.Min.ToString();
            txtMax.Text = produktua.Max.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                String izena = txtIzena.Text;
                int stock = Convert.ToInt16(txtStock.Text);
                int min = Convert.ToInt16(txtMin.Text);
                int max = Convert.ToInt16(txtMax.Text);

                String result = ProduktuaKudeatzailea.ProduktuaUpdate(sessionFactory, produktua, izena, stock, min, max);

                if (result == "true")
                {
                    MessageBox.Show("Produktua ongi editatuta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea produktua eguneratzean: {ex.Message}", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "Ziur zaude produktua ezabatu nahi duzula?",
                "Konfirmazioa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    string result = ProduktuaKudeatzailea.ProduktuaDelete(sessionFactory, produktua);

                    if (result == "true")
                    {
                        MessageBox.Show("Produktuak arrakastaz ezabatu da.", "Informazioa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show(result, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errorea produktua ezabatzean: {ex.Message}", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void labelProduktuak_Click(object sender, EventArgs e)
        {

        }

        private void ProduktuaEditForm_Load(object sender, EventArgs e)
        {

        }
    }
}
