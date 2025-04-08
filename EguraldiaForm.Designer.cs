namespace _2taldea
{
    partial class EguraldiaForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label labelNombreUsuario;
        private Label labelTitulo;
        private FlowLayoutPanel flowLayoutPanelEguraldia;
        private Button btnAtzera;
        private PictureBox pictureBoxLogo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelNombreUsuario = new Label();
            labelTitulo = new Label();
            flowLayoutPanelEguraldia = new FlowLayoutPanel();
            btnAtzera = new Button();
            pictureBoxLogo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            SuspendLayout();
            // 
            // labelNombreUsuario
            // 
            labelNombreUsuario.AutoSize = true;
            labelNombreUsuario.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelNombreUsuario.ForeColor = Color.White;
            labelNombreUsuario.Location = new Point(1716, 50);
            labelNombreUsuario.Name = "labelNombreUsuario";
            labelNombreUsuario.Size = new Size(75, 32);
            labelNombreUsuario.TabIndex = 1;
            labelNombreUsuario.Text = "Izena";
            // 
            // labelLaburpena
            // 
            labelTitulo.AutoSize = true;
            labelTitulo.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitulo.ForeColor = Color.White;
            labelTitulo.Location = new Point(894, 50);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(178, 45);
            labelTitulo.TabIndex = 2;
            labelTitulo.Text = "Eguraldia";
            // 
            // flowLayoutPanelEguraldia
            // 
            flowLayoutPanelEguraldia.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanelEguraldia.AutoScroll = true;
            flowLayoutPanelEguraldia.BackColor = Color.FromArgb(186, 69, 13);
            flowLayoutPanelEguraldia.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelEguraldia.Location = new Point(321, 250);
            flowLayoutPanelEguraldia.Name = "flowLayoutPanelEguraldia";
            flowLayoutPanelEguraldia.Size = new Size(1197, 600);
            flowLayoutPanelEguraldia.TabIndex = 3;
            flowLayoutPanelEguraldia.WrapContents = false;
            //
            // btnAtzera
            // 
            btnAtzera.BackColor = Color.Red;
            btnAtzera.FlatStyle = FlatStyle.Flat;
            btnAtzera.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnAtzera.ForeColor = Color.White;
            btnAtzera.Location = new Point(1700, 900);
            btnAtzera.Name = "btnAtzera";
            btnAtzera.Size = new Size(131, 47);
            btnAtzera.TabIndex = 5;
            btnAtzera.Text = "Atzera";
            btnAtzera.UseVisualStyleBackColor = false;
            btnAtzera.Click += BtnAtzera_Click;
            //
            // pictureBoxLogo
            // 
            pictureBoxLogo.Image = Properties.Resources.logo;
            pictureBoxLogo.Location = new Point(50, 30);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(250, 200);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLogo.TabIndex = 2;
            pictureBoxLogo.TabStop = false;
            // 
            // EguraldiaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(9, 23, 37);
            ClientSize = new Size(1813, 1100);
            Controls.Add(labelNombreUsuario);
            Controls.Add(labelTitulo);
            Controls.Add(flowLayoutPanelEguraldia);
            Controls.Add(btnAtzera);
            Controls.Add(pictureBoxLogo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EguraldiaForm";
            WindowState = FormWindowState.Maximized;
            Load += EguraldiaForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
