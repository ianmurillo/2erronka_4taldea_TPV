using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _2taldea
{
    public partial class EguraldiaForm : Form
    {
        private string nombreUsuario;
        private List<(string Fecha, Panel PanelDia)> panelesDias = new List<(string, Panel)>();

        public EguraldiaForm(string nombreUsuario)
        {
            InitializeComponent();
            this.nombreUsuario = nombreUsuario ?? throw new ArgumentNullException(nameof(nombreUsuario));
            labelNombreUsuario.Text = nombreUsuario;
        }

        private async void EguraldiaForm_Load(object sender, EventArgs e)
        {
            string ftpUrl = "ftp://192.168.115.153/eguraldia/eguraldia.xml";
            string ftpUser = "eguraldia";
            string ftpPass = "1WMG2023";

            try
            {
                string xmlContent = await Task.Run(() => LeerXmlDesdeFtp(ftpUrl, ftpUser, ftpPass));
                XDocument doc = XDocument.Parse(xmlContent);
                var dias = doc.Descendants("dia");

                flowLayoutPanelEguraldia.Controls.Clear();
                panelesDias.Clear();
                comboBoxFiltro.Items.Clear();
                comboBoxFiltro.Items.Add("Denak");

                foreach (var dia in dias)
                {
                    string fecha = dia.Attribute("fecha")?.Value ?? "N/A";

                    Panel panelDia = new Panel
                    {
                        Size = new Size(1100, 180),
                        BackColor = Color.FromArgb(50, 50, 50),
                        Margin = new Padding(10)
                    };

                    string tempMax = dia.Element("temperatura_maximoa")?.Value ?? "-";
                    string tempMin = dia.Element("temperatura_minimoa")?.Value ?? "-";
                    string estadoCielo = dia.Element("zeruaren_egoera")?.Value ?? "-";
                    string probPrecipitacion = dia.Element("euri_probabilitatea")?.Value ?? "-";
                    string viento = dia.Element("haize_abiadura")?.Value ?? "-";

                    Panel panelMedidas = new Panel
                    {
                        Size = new Size(300, 150),
                        Location = new Point(750, 10),
                        BackColor = Color.FromArgb(70, 70, 70)
                    };

                    string acciones = "";

                    if (int.TryParse(probPrecipitacion, out int prob) && prob > 30)
                        acciones += "Toldoa: IREKITA\n";
                    else
                        acciones += "Toldoa: ITXITA\n";

                    if (int.TryParse(tempMax, out int tMax) && tMax > 25)
                        acciones += "Ureztatze sistema: PIZTU\n";
                    else
                        acciones += "Ureztatze sistema: ITZALITA\n";

                    if (int.TryParse(tempMin, out int tMin) && tMin < 12)
                        acciones += "Berogailua: PIZTU\n";
                    else
                        acciones += "Berogailua: ITZALITA\n";

                    if (int.TryParse(viento, out int v) && v > 15)
                        acciones += "Haizearen geldotzeak: JARRI";
                    else
                        acciones += "Haizearen geldotzeak: KENDU";

                    panelMedidas.Controls.Add(new Label
                    {
                        Text = acciones,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        ForeColor = Color.LightGreen,
                        Location = new Point(10, 10),
                        Size = new Size(280, 130),
                        AutoSize = false
                    });

                    panelDia.Controls.Add(new Label
                    {
                        Text = $"Fecha: {fecha}",
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        ForeColor = Color.White,
                        Location = new Point(10, 10),
                        AutoSize = true
                    });

                    panelDia.Controls.Add(new Label
                    {
                        Text = $"Temp. Máx: {tempMax}°C",
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.White,
                        Location = new Point(10, 40),
                        AutoSize = true
                    });

                    panelDia.Controls.Add(new Label
                    {
                        Text = $"Temp. Mín: {tempMin}°C",
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.White,
                        Location = new Point(10, 65),
                        AutoSize = true
                    });

                    panelDia.Controls.Add(new Label
                    {
                        Text = $"Estado del cielo: {estadoCielo}",
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.White,
                        Location = new Point(10, 90),
                        AutoSize = true
                    });

                    panelDia.Controls.Add(new Label
                    {
                        Text = $"Precipitación: {probPrecipitacion}%",
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.White,
                        Location = new Point(10, 115),
                        AutoSize = true
                    });

                    panelDia.Controls.Add(new Label
                    {
                        Text = $"Viento: {viento} km/h",
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.White,
                        Location = new Point(10, 140),
                        AutoSize = true
                    });

                    panelDia.Controls.Add(panelMedidas);

                    panelesDias.Add((fecha, panelDia));

                    if (!comboBoxFiltro.Items.Contains(fecha))
                        comboBoxFiltro.Items.Add(fecha);
                }

                comboBoxFiltro.SelectedIndex = 0;
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el pronóstico desde FTP: " + ex.Message);
            }
        }

        private string LeerXmlDesdeFtp(string ftpUrl, string username, string password)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(username, password);
            request.UseBinary = true;
            request.UsePassive = true;

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }

        private void BtnAtzera_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnEguraldia_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void ComboBoxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            string filtroFecha = comboBoxFiltro.SelectedItem?.ToString();
            flowLayoutPanelEguraldia.Controls.Clear();

            if (string.IsNullOrEmpty(filtroFecha) || filtroFecha == "Denak")
            {
                foreach (var (fecha, panel) in panelesDias)
                    flowLayoutPanelEguraldia.Controls.Add(panel);
            }
            else
            {
                var panel = panelesDias.FirstOrDefault(p => p.Fecha == filtroFecha).PanelDia;
                if (panel != null)
                    flowLayoutPanelEguraldia.Controls.Add(panel);
            }
        }
    }
}
