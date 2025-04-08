using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace _2taldea
{
    public partial class EguraldiaForm : Form
    {
        private string nombreUsuario;

        public EguraldiaForm(string nombreUsuario)
        {
            InitializeComponent();
            this.nombreUsuario = nombreUsuario ?? throw new ArgumentNullException(nameof(nombreUsuario));
            this.Load += EguraldiaForm_Load;

            labelNombreUsuario.Text = nombreUsuario;
        }

        private async void EguraldiaForm_Load(object sender, EventArgs e)
        {
            // URL del archivo XML en GitHub
            string apiUrl = "https://api.github.com/repos/benatge2/EguraldiaXml/contents/eguraldia.xml?ref=master";

            try
            {
                // Usamos HttpClient para obtener los datos del archivo XML
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");

                // Hacemos una solicitud GET al API de GitHub
                string json = await client.GetStringAsync(apiUrl);
                dynamic data = JsonConvert.DeserializeObject(json);
                string base64Content = data.content;

                // Decodificamos el contenido base64 del XML
                base64Content = base64Content.Replace("\n", "").Replace("\r", "");
                string xmlContent = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64Content));

                // Parseamos el XML
                XDocument doc = XDocument.Parse(xmlContent);
                var dias = doc.Descendants("dia");

                // Limpiamos el FlowLayoutPanel antes de agregar nuevos controles
                flowLayoutPanelEguraldia.Controls.Clear();

                // Iteramos sobre cada día y mostramos la información en el FlowLayoutPanel
                foreach (var dia in dias)
                {
                    Panel panelDia = new Panel
                    {
                        Size = new Size(1100, 150),
                        BackColor = Color.FromArgb(50, 50, 50),
                        Margin = new Padding(10)
                    };

                    Label labelFecha = new Label
                    {
                        Text = $"Fecha: {dia.Attribute("fecha").Value}",
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        ForeColor = Color.White,
                        Location = new Point(10, 10)
                    };

                    Label labelTempMax = new Label
                    {
                        Text = $"Temp. Max: {dia.Element("temperatura").Element("maxima").Value}°C",
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.White,
                        Location = new Point(10, 50)
                    };

                    Label labelTempMin = new Label
                    {
                        Text = $"Temp. Min: {dia.Element("temperatura").Element("minima").Value}°C",
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.White,
                        Location = new Point(10, 80)
                    };

                    Label labelProbPrecipitacion = new Label
                    {
                        Text = $"Probabilidad de precipitación: {dia.Element("prob_precipitacion")?.Value ?? "N/A"}",
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.White,
                        Location = new Point(10, 110)
                    };

                    panelDia.Controls.Add(labelFecha);
                    panelDia.Controls.Add(labelTempMax);
                    panelDia.Controls.Add(labelTempMin);
                    panelDia.Controls.Add(labelProbPrecipitacion);

                    flowLayoutPanelEguraldia.Controls.Add(panelDia);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el archivo de pronóstico: " + ex.Message);
            }
        }

        private void BtnAtzera_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnEguraldia_Click(object sender, EventArgs e)
        {
            // Abrir el formulario en pantalla completa
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
