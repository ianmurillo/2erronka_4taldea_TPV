using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NHibernate;

namespace _2taldea
{
    public partial class EskaerakForm : Form
    {
        private string nombreUsuario;
        private ISessionFactory sessionFactory;

        public EskaerakForm(string nombreUsuario, ISessionFactory sessionFactory)
        {
            InitializeComponent();
            this.nombreUsuario = nombreUsuario ?? throw new ArgumentNullException(nameof(nombreUsuario));
            this.sessionFactory = sessionFactory ?? throw new ArgumentNullException(nameof(sessionFactory));
        }

        private void EskaerakForm_Load(object sender, EventArgs e)
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

                int filas = 3;
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
                        BackColor = HayPedidoActivo(mesa.Id) ? Color.FromArgb(124, 132, 124) : Color.FromArgb(186, 69, 13),
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea mahaiak sortzean: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool HayPedidoActivo(int mahaila_id)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                return session.QueryOver<Eskaera>()
                              .Where(e => e.Mahaila.Id == mahaila_id && e.Egoera == true)
                              .RowCount() > 0;
            }
        }

        private void BtnMesa_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                int mahaila_id = (int)btn.Tag;
                EskaeraKudeatzaile.ProcesarMesa(mahaila_id, nombreUsuario, sessionFactory);
                btn.BackColor = HayPedidoActivo(mahaila_id) ? Color.FromArgb(124, 132, 124) : Color.FromArgb(186, 69, 13);
            }
        }

        private void BtnAtzera_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}