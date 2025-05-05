using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualBasic;

namespace _2taldea
{
    public partial class txat : Form
    {
        private String encryptCode = "tralalerotralala";
        private TextBox textBoxMessage;
        private Button buttonSend;
        private ListBox listBoxChat;

        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private Thread listenerThread;

        private String izena;

        public txat(String izena)
        {
            this.izena = izena;
            Console.WriteLine(this.izena);
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            InitializeChatComponents();
        }

        private void txat_Load(object sender, EventArgs e)
        {
            ConnectToServer();
        }

        private Button buttonSendFile; // Añadir esta nueva variable arriba, junto a buttonSend y textBoxMessage

        private void InitializeChatComponents()
        {
            // Panel para mensajes de chat
            FlowLayoutPanel panelChat = new FlowLayoutPanel
            {
                Location = new Point(0, 0),
                Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 70),
                AutoScroll = true,
                BackColor = Color.White,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };

            textBoxMessage = new TextBox
            {
                Location = new Point(10, this.ClientSize.Height - 60),
                Size = new Size(this.ClientSize.Width - 220, 50), // espacio para dos botones
                Multiline = true,
                Name = "textBoxMessage",
                Font = new Font("Segoe UI", 12),
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.None
            };

            buttonSend = new Button
            {
                Location = new Point(this.ClientSize.Width - 200, this.ClientSize.Height - 60),
                Size = new Size(90, 50),
                Name = "buttonSend",
                Text = "Enviar",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            buttonSend.Click += new EventHandler(buttonSend_Click);

            buttonSendFile = new Button
            {
                Location = new Point(this.ClientSize.Width - 100, this.ClientSize.Height - 60),
                Size = new Size(90, 50),
                Name = "buttonSendFile",
                Text = "Archivo",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            buttonSendFile.Click += new EventHandler(buttonSendFile_Click);

            this.Controls.Add(panelChat);
            this.Controls.Add(textBoxMessage);
            this.Controls.Add(buttonSend);
            this.Controls.Add(buttonSendFile);

            this.Resize += (s, e) =>
            {
                panelChat.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 70);
                textBoxMessage.Location = new Point(10, this.ClientSize.Height - 60);
                textBoxMessage.Size = new Size(this.ClientSize.Width - 220, 50);
                buttonSend.Location = new Point(this.ClientSize.Width - 200, this.ClientSize.Height - 60);
                buttonSendFile.Location = new Point(this.ClientSize.Width - 100, this.ClientSize.Height - 60);
            };

            panelChat.SizeChanged += (s, e) => AdjustMessagePanelMargins(panelChat);
        }

        private void AdjustMessagePanelMargins(FlowLayoutPanel panelChat) { 
            foreach (FlowLayoutPanel messagePanel in panelChat.Controls.OfType<FlowLayoutPanel>()) { 
                bool isUser = messagePanel.Controls.OfType<Label>().FirstOrDefault()?.BackColor == Color.LightBlue; 
                int marginLeft = isUser ? panelChat.Width - messagePanel.PreferredSize.Width - 30 : 0; 
                int marginRight = isUser ? 0 : 0; 
                messagePanel.Margin = new Padding(marginLeft, 0, marginRight, 10); 
            } 
        }

        private void AddMessageToPanel(string message, bool isUserMessage)
        {
            FlowLayoutPanel panelChat = this.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
            if (panelChat == null) return;

            // Separar el nombre del remitente del mensaje
            message = message.Trim();
            var parts = message.Split(new char[] { '>' }, 2);
            if (parts.Length < 2) return;
            string senderName = parts[0];
            string msg = Decrypt(parts[1], encryptCode);

            message = senderName + ">" + msg;

            bool isUser = (senderName.Trim() == this.izena.Trim());

            Label labelMessage = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(panelChat.Width - 30, 0),
                Text = message,  // Mostrar solo el contenido del mensaje
                Font = new Font("Segoe UI", 12),
                BackColor = isUser ? Color.LightBlue : Color.LightGray,
                ForeColor = Color.Black,
                Padding = new Padding(10),
                Margin = new Padding(0),
                BorderStyle = BorderStyle.FixedSingle,
            };

            FlowLayoutPanel messagePanel = new FlowLayoutPanel
            {
                MaximumSize = new Size(panelChat.Width - 30, 0),
                AutoSize = true,
                BackColor = Color.Transparent,
                Padding = new Padding(5),
                Margin = isUser ? new Padding(panelChat.Width - labelMessage.Width - 30, 0, 0, 0) : new Padding(0),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Anchor = isUser ? AnchorStyles.Right : AnchorStyles.Left,
                Dock = isUser ? DockStyle.Right : DockStyle.Left
            };

            messagePanel.Controls.Add(labelMessage);
            panelChat.Controls.Add(messagePanel);
            panelChat.ScrollControlIntoView(messagePanel);
            AdjustMessagePanelMargins(panelChat);

        }



        private void buttonSend_Click(object sender, EventArgs e)
        {
            string message = textBoxMessage.Text;

            if (!string.IsNullOrEmpty(message))
            {
                string formattedMessage = this.izena + ">" + Encrypt(message,encryptCode);
                AddMessageToPanel(formattedMessage, true);
                writer.WriteLine(formattedMessage);
                textBoxMessage.Clear();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un mensaje.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
        public static string Encrypt(string plainText, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32)); // 256-bit key
                aes.IV = new byte[16]; // Usamos IV en blanco por simplicidad (no recomendado para producción)

                ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(plainText), 0, plainText.Length);

                return Convert.ToBase64String(encrypted);
            }
        }

        public static string Decrypt(string cipherText, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32)); // 256-bit key
                aes.IV = new byte[16];

                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] decrypted = decryptor.TransformFinalBlock(Convert.FromBase64String(cipherText), 0, Convert.FromBase64String(cipherText).Length);

                return Encoding.UTF8.GetString(decrypted);
            }
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("192.168.115.153", 5555);
                reader = new StreamReader(client.GetStream());
                writer = new StreamWriter(client.GetStream());
                writer.AutoFlush = true;

                listenerThread = new Thread(ListenForMessages);
                listenerThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con el servidor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListenForMessages()
        {
            try
            {
                string message;
                while ((message = reader.ReadLine()) != null)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        string[] parts = message.Split(new char[] { '>' }, 3);

                        if (parts.Length == 3)
                        {
                            string remitente = parts[0].Trim();
                            string fileName = parts[1].Trim();
                            string encodedFile = parts[2].Trim();

                            if (IsProbablyBase64(encodedFile))
                            {
                                SaveReceivedFile(fileName, encodedFile);
                                AddMessageToPanel(remitente + " > " + fileName, false);
                                return;
                            }
                        }

                        // Si no era un archivo, tratar como mensaje normal
                        AddMessageToPanel(message, false);
                    }));
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show("Error al escuchar mensajes del servidor: " + ioEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ThreadInterruptedException)
            {
                // El hilo fue interrumpido
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado al escuchar mensajes del servidor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DisconnectFromServer();
            }
        }


        private void buttonSendFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;
                        string fileName = Path.GetFileName(filePath);
                        byte[] fileBytes = File.ReadAllBytes(filePath);
                        string encodedFile = Convert.ToBase64String(fileBytes);

                        string formattedMessage = this.izena + ">" + fileName + ">" + encodedFile;
                        writer.WriteLine(formattedMessage);

                        // También lo mostramos en nuestro chat
                        AddMessageToPanel(this.izena + " > " + fileName, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al enviar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void SaveReceivedFile(string fileName, string encodedFile)
        {
            try
            {
                byte[] fileBytes = Convert.FromBase64String(encodedFile);

                // Guardar en la carpeta Descargas del usuario
                string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
                string savePath = Path.Combine(downloadsPath, fileName);

                File.WriteAllBytes(savePath, fileBytes);

                //MessageBox.Show($"Archivo recibido y guardado en: {savePath}", "Archivo recibido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al guardar el archivo: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Comprobar de forma sencilla si el tercer campo parece ser base64
        private bool IsProbablyBase64(string base64)
        {
            // Evitar errores: que tenga longitud múltiplo de 4 y solo caracteres válidos
            if (string.IsNullOrEmpty(base64) || base64.Length % 4 != 0)
                return false;

            return System.Text.RegularExpressions.Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", System.Text.RegularExpressions.RegexOptions.None);
        }


        private void DisconnectFromServer()
        {
            try
            {
                if (listenerThread != null && listenerThread.IsAlive)
                {
                    listenerThread.Interrupt();
                    listenerThread.Join();
                }

                if (writer != null)
                {
                    writer.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }

                if (client != null)
                {
                    client.Close();
                }

                MessageBox.Show("Desconectado del servidor.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al desconectar del servidor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Hide();
        }
    }
}
