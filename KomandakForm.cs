using NHibernate;

namespace _2taldea
{
    public partial class KomandakForm : Form
    {
        private string nombreUsuario;
        private ISessionFactory sessionFactory;
        private Dictionary<int, Button> mesaButtons = new Dictionary<int, Button>();

        public KomandakForm(string nombreUsuario, ISessionFactory sessionFactory)
        {
            InitializeComponent();
            this.nombreUsuario = nombreUsuario ?? throw new ArgumentNullException(nameof(nombreUsuario));
            this.sessionFactory = sessionFactory ?? throw new ArgumentNullException(nameof(sessionFactory));
        }

        private void KomandakForm_Load(object sender, EventArgs e)
        {
            labelIzena.Text = nombreUsuario;
            CrearMesas();
        }

        private void CrearMesas()
        {
            try
            {
                var mesas = KomandakKudeatzailea.ObtenerMesas(sessionFactory)
                                .Where(m => m.Habilitado) // Filtrar solo las mesas habilitadas
                                .ToList();

                int filas = 2;
                int buttonWidth = 175;
                int buttonHeight = 175;
                int buttonSpacingHorizontal = 40;
                int buttonSpacingVertical = 50;
                int mesasPorFila = (int)Math.Ceiling((double)mesas.Count / filas);
                int totalWidth = mesasPorFila * buttonWidth + (mesasPorFila - 1) * buttonSpacingHorizontal;
                int startX = (this.ClientSize.Width - totalWidth) / 2;
                int startY = 300;

                for (int i = 0; i < mesas.Count; i++)
                {
                    Mahaia mesa = mesas[i];
                    Button btnMesa = new Button
                    {
                        Text = $"{mesa.MahailaZenbakia} .Mahaia\n{mesa.Eserlekuak} pertsonentzat",
                        Width = buttonWidth,
                        Height = buttonHeight,
                        Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                        BackColor = ObtenerColorMesa(mesa.Id),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Tag = mesa.Id,
                        Enabled = mesa.Habilitado // Deshabilitar botón si la mesa no está habilitada
                    };

                    int column = i % mesasPorFila;
                    int row = i / mesasPorFila;
                    btnMesa.Location = new Point(
                        startX + column * (buttonWidth + buttonSpacingHorizontal),
                        startY + row * (buttonHeight + buttonSpacingVertical) + (row * 20)
                    );

                    btnMesa.Click += BtnMesa_Click;
                    this.Controls.Add(btnMesa);
                    mesaButtons[mesa.Id] = btnMesa;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea mahaiak sortzean: {ex.Message}", "Arazoak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Color ObtenerColorMesa(int mahaila_id)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                // Buscar si hay algún pedido activo (Egoera = true) para esta mesa
                bool hayPedidoActivo = session.QueryOver<Eskaera>()
                                              .Where(e => e.Mahaila.Id == mahaila_id && e.Egoera == true)
                                              .RowCount() > 0;

                // Si hay un pedido activo, la mesa se muestra en color naranja; si no, en gris
                return hayPedidoActivo ? Color.FromArgb(124, 132, 124) : Color.FromArgb(186, 69, 13);
            }
        }

        private void BtnMesa_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn != null)
                {
                    int mahaila_id = (int)btn.Tag;
                    string userName = this.nombreUsuario;

                    MesaDetallesForm detallesForm = new MesaDetallesForm(mahaila_id, sessionFactory, userName);
                    detallesForm.ShowDialog();

                    // Actualizar color de la mesa después de cerrar detalles
                    btn.BackColor = ObtenerColorMesa(mahaila_id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea mahaia aukeratzean: {ex.Message}", "Arazoak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAtzera_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
