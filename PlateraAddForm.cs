using NHibernate;
using System;
using System.Windows.Forms;

namespace _2taldea
{
    public partial class PlateraAddForm : Form
    {
        private ISessionFactory sessionFactory;

        public PlateraAddForm(ISessionFactory sessionFactory)
        {
            InitializeComponent();
            this.sessionFactory = sessionFactory ?? throw new ArgumentNullException(nameof(sessionFactory));
        }

        private void btnGorde_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los datos del formulario
                string izena = txtIzena.Text;
                string kategoria = cmbKategoria.Text;
                float prezioa = Convert.ToSingle(txtPrezioa.Text);
                int menu = Convert.ToInt32(txtMenu.Text);

                // Llamar a la función PlateraAdd de PlateraKudeatzailea para guardar el plato en la base de datos
                string result = PlateraKudeatzailea.PlateraAdd(sessionFactory, izena, kategoria, prezioa, menu);

                // Mostrar mensaje según el resultado
                if (result == "true")
                {
                    MessageBox.Show("Platera ongi gordeta", "Informazioa", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Errorea: " + ex.Message);
            }
        }

        private void btnUtzi_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario
            this.Close();
        }

        private void PlateraAddForm_Load(object sender, EventArgs e)
        {

        }

        private void txtKategoria_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
