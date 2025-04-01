using System;
using System.Collections.Generic;
using System.Drawing; // Para colores en el formulario
using System.IO;
using System.Windows.Forms;
using NHibernate;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Borders;

// Alias para evitar conflictos
using PdfColor = iText.Kernel.Colors.Color;
using PdfImage = iText.Layout.Element.Image;
namespace _2taldea
{
    public partial class EskaeraResumenForm2 : Form
    {
        private List<Eskaera> eskaerak;
        private int mahaila_id;
        private ISessionFactory sessionFactory;

        public EskaeraResumenForm2(int mahaila_id, List<Eskaera> eskaerak, string nombreUsuario, ISessionFactory sessionFactory)
        {
            InitializeComponent();
            this.mahaila_id = mahaila_id;
            this.eskaerak = eskaerak;
            this.sessionFactory = sessionFactory;

            // Asignar el nombre del usuario directamente (sin necesidad de cambiarlo después)
            labelNombreUsuario.Text = nombreUsuario;

            CargarDatos();
        }

        private void CargarDatos()
        {
            labelMesa.Text = $"{mahaila_id},";
            flowLayoutPanelPedidos.Controls.Clear();
            float totalPrecioa = 0;

            // Asegúrate de tener una sesión abierta
            using (ISession session = sessionFactory.OpenSession())
            {
                foreach (var eskaera in eskaerak)
                {
                    // Obtener el Eskaera_platera correspondiente
                    var eskaeraPlatera = session.QueryOver<EskaeraPlatera>()
                                                .Where(ep => ep.Eskaera.Id == eskaera.Id)
                                                .SingleOrDefault();

                    // Si no encontramos el Eskaera_platera, saltamos a la siguiente iteración
                    if (eskaeraPlatera == null) continue;

                    // Obtener el Platera correspondiente a partir del platera_id en Eskaera_platera
                    var platera = session.Get<Platera>(eskaeraPlatera.Platera.Id);
                    if (platera == null) continue;

                    FlowLayoutPanel panelProducto = new FlowLayoutPanel
                    {
                        FlowDirection = FlowDirection.LeftToRight,
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        Padding = new Padding(0, 10, 0, 10),
                        Margin = new Padding(0, 0, 0, 10),
                        Width = flowLayoutPanelPedidos.ClientSize.Width - 10
                    };

                    FlowLayoutPanel innerPanel = new FlowLayoutPanel
                    {
                        FlowDirection = FlowDirection.LeftToRight,
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        Width = panelProducto.Width - 20,
                        Margin = new Padding(0)
                    };

                    Label lblProducto = new Label
                    {
                        Text = platera.Izena,  // Accedemos a Izena de la tabla Platera
                        Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point),
                        ForeColor = System.Drawing.Color.White,
                        AutoSize = true
                    };
                    innerPanel.Controls.Add(lblProducto);

                    Label lblPrecio = new Label
                    {
                        Text = $"{platera.Prezioa}€",  // Accedemos a Prezioa de la tabla Platera
                        Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point),
                        ForeColor = System.Drawing.Color.White,
                        AutoSize = true
                    };
                    lblPrecio.Anchor = AnchorStyles.Right;
                    innerPanel.Controls.Add(lblPrecio);

                    panelProducto.Controls.Add(innerPanel);
                    flowLayoutPanelPedidos.Controls.Add(panelProducto);

                    Label lblLinea = new Label
                    {
                        Text = string.Empty,
                        Height = 2,
                        BackColor = System.Drawing.Color.White,
                        Dock = DockStyle.Top,
                        Width = flowLayoutPanelPedidos.ClientSize.Width
                    };
                    flowLayoutPanelPedidos.Controls.Add(lblLinea);

                    totalPrecioa += (float)platera.Prezioa;  // Conversión explícita de double a float
                }
            }

            labelPrezioa.Text = $"Prezioa: {totalPrecioa}€";
        }


        private void BtnAtzera_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEskaeraSortu2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Baimenik ez.");
        }

        private void EskaeraResumenForm_Load(object sender, EventArgs e)
        {

        }
    }
}

