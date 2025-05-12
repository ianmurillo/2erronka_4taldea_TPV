namespace _2taldea
{
    partial class ProduktuaAddForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtIzena;
        private TextBox txtMota;
        private TextBox txtEzaugarria;
        private TextBox txtStock;
        private TextBox txtUnitatea;
        private TextBox txtMax;
        private TextBox txtMin;
        private Button btnGorde;
        private Button btnUtzi;
        private Label labelTitle;
        private Label labelIzena;
        private Label labelMota;
        private Label labelEzaugarria;
        private Label labelStock;
        private Label labelUnitatea;
        private Label labelMax;
        private Label labelMin;
        private PictureBox pictureBox;

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
            txtIzena = new TextBox();
            txtMota = new TextBox();
            txtEzaugarria = new TextBox();
            txtStock = new TextBox();
            txtUnitatea = new TextBox();
            txtMax = new TextBox();
            txtMin = new TextBox();
            btnGorde = new Button();
            btnUtzi = new Button();
            labelTitle = new Label();
            labelIzena = new Label();
            labelMota = new Label();
            labelEzaugarria = new Label();
            labelStock = new Label();
            labelUnitatea = new Label();
            labelMax = new Label();
            labelMin = new Label();
            pictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // txtIzena
            // 
            txtIzena.BackColor = Color.FromArgb(186, 69, 13);
            txtIzena.ForeColor = Color.White;
            txtIzena.Location = new Point(850, 400);
            txtIzena.Name = "txtIzena";
            txtIzena.Size = new Size(350, 23);
            txtIzena.TabIndex = 0;
            // 
            // txtMota
            // 
            txtMota = new TextBox();
            txtMota.BackColor = Color.FromArgb(186, 69, 13);
            txtMota.ForeColor = Color.White;
            txtMota.Location = new Point(850, 600);
            txtMota.Name = "txtMota";
            txtMota.Size = new Size(350, 23);
            txtMota.TabIndex = 1;
            Controls.Add(txtMota);
            // 
            // txtEzaugarria
            // 
            txtEzaugarria = new TextBox();
            txtEzaugarria.BackColor = Color.FromArgb(186, 69, 13);
            txtEzaugarria.ForeColor = Color.White;
            txtEzaugarria.Location = new Point(850, 650);
            txtEzaugarria.Name = "txtEzaugarria";
            txtEzaugarria.Size = new Size(350, 23);
            txtEzaugarria.TabIndex = 2;
            Controls.Add(txtEzaugarria);
            // 
            // txtStock
            // 
            txtStock.BackColor = Color.FromArgb(186, 69, 13);
            txtStock.ForeColor = Color.White;
            txtStock.Location = new Point(850, 450);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(350, 23);
            txtStock.TabIndex = 3;
            // 
            // txtUnitatea
            // 
            txtUnitatea = new TextBox();
            txtUnitatea.BackColor = Color.FromArgb(186, 69, 13);
            txtUnitatea.ForeColor = Color.White;
            txtUnitatea.Location = new Point(850, 700);
            txtUnitatea.Name = "txtUnitatea";
            txtUnitatea.Size = new Size(350, 23);
            txtUnitatea.TabIndex = 4;
            Controls.Add(txtUnitatea);
            // 
            // txtMax
            // 
            txtMax.BackColor = Color.FromArgb(186, 69, 13);
            txtMax.ForeColor = Color.White;
            txtMax.Location = new Point(850, 503);
            txtMax.Name = "txtMax";
            txtMax.Size = new Size(350, 23);
            txtMax.TabIndex = 5;
            // 
            // txtMin
            // 
            txtMin.BackColor = Color.FromArgb(186, 69, 13);
            txtMin.ForeColor = Color.White;
            txtMin.Location = new Point(850, 554);
            txtMin.Name = "txtMin";
            txtMin.Size = new Size(350, 23);
            txtMin.TabIndex = 6;
            txtMin.TextChanged += txtMin_TextChanged;
            // 
            // btnGorde
            // 
            btnGorde.BackColor = Color.Green;
            btnGorde.FlatStyle = FlatStyle.Flat;
            btnGorde.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnGorde.ForeColor = Color.White;
            btnGorde.Location = new Point(500, 800);
            btnGorde.Name = "btnGorde";
            btnGorde.Size = new Size(150, 50);
            btnGorde.TabIndex = 7;
            btnGorde.Text = "Gorde";
            btnGorde.UseVisualStyleBackColor = false;
            btnGorde.Click += btnGorde_Click;
            // 
            // btnUtzi
            // 
            btnUtzi.BackColor = Color.Red;
            btnUtzi.FlatStyle = FlatStyle.Flat;
            btnUtzi.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnUtzi.ForeColor = Color.White;
            btnUtzi.Location = new Point(1300, 800);
            btnUtzi.Name = "btnUtzi";
            btnUtzi.Size = new Size(150, 50);
            btnUtzi.TabIndex = 8;
            btnUtzi.Text = "Utzi";
            btnUtzi.UseVisualStyleBackColor = false;
            btnUtzi.Click += btnUtzi_Click;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitle.ForeColor = Color.White;
            labelTitle.Location = new Point(850, 50);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(286, 45);
            labelTitle.TabIndex = 9;
            labelTitle.Text = "Produktua Gehitu";
            // 
            // labelIzena
            // 
            labelIzena.AutoSize = true;
            labelIzena.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelIzena.ForeColor = Color.White;
            labelIzena.Location = new Point(600, 400);
            labelIzena.Name = "labelIzena";
            labelIzena.Size = new Size(64, 25);
            labelIzena.TabIndex = 10;
            labelIzena.Text = "Izena:";
            // 
            // labelMota
            // 
            labelMota = new Label();
            labelMota.AutoSize = true;
            labelMota.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelMota.ForeColor = Color.White;
            labelMota.Location = new Point(600, 600);
            labelMota.Name = "labelMota";
            labelMota.Size = new Size(61, 25);
            labelMota.TabIndex = 11;
            labelMota.Text = "Mota:";
            Controls.Add(labelMota);
            // 
            // labelEzaugarria
            // 
            labelEzaugarria = new Label();
            labelEzaugarria.AutoSize = true;
            labelEzaugarria.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelEzaugarria.ForeColor = Color.White;
            labelEzaugarria.Location = new Point(600, 650);
            labelEzaugarria.Name = "labelEzaugarria";
            labelEzaugarria.Size = new Size(118, 25);
            labelEzaugarria.TabIndex = 12;
            labelEzaugarria.Text = "Ezaugarria:";
            Controls.Add(labelEzaugarria);
            // 
            // labelStock
            // 
            labelStock.AutoSize = true;
            labelStock.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelStock.ForeColor = Color.White;
            labelStock.Location = new Point(600, 450);
            labelStock.Name = "labelStock";
            labelStock.Size = new Size(68, 25);
            labelStock.TabIndex = 13;
            labelStock.Text = "Stock:";
            // 
            // labelUnitatea
            // 
            labelUnitatea = new Label();
            labelUnitatea.AutoSize = true;
            labelUnitatea.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelUnitatea.ForeColor = Color.White;
            labelUnitatea.Location = new Point(600, 700);
            labelUnitatea.Name = "labelUnitatea";
            labelUnitatea.Size = new Size(98, 25);
            labelUnitatea.TabIndex = 14;
            labelUnitatea.Text = "Unitatea:";
            Controls.Add(labelUnitatea);
            // 
            // labelMax
            // 
            labelMax.AutoSize = true;
            labelMax.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelMax.ForeColor = Color.White;
            labelMax.Location = new Point(600, 503);
            labelMax.Name = "labelMax";
            labelMax.Size = new Size(100, 25);
            labelMax.TabIndex = 15;
            labelMax.Text = "Maximoa:";
            // 
            // labelMin
            // 
            labelMin.AutoSize = true;
            labelMin.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelMin.ForeColor = Color.White;
            labelMin.Location = new Point(600, 554);
            labelMin.Name = "labelMin";
            labelMin.Size = new Size(96, 25);
            labelMin.TabIndex = 16;
            labelMin.Text = "Minimoa:";
            // 
            // pictureBox
            // 
            pictureBox.Image = Properties.Resources.logo;
            pictureBox.Location = new Point(50, 30);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(250, 200);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 17;
            pictureBox.TabStop = false;
            // 
            // ProduktuaAddForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(9, 23, 37);
            ClientSize = new Size(1920, 1061);
            Controls.Add(labelTitle);
            Controls.Add(labelIzena);
            Controls.Add(txtIzena);
            Controls.Add(labelMota);
            Controls.Add(txtMota);
            Controls.Add(labelEzaugarria);
            Controls.Add(txtEzaugarria);
            Controls.Add(labelStock);
            Controls.Add(txtStock);
            Controls.Add(labelUnitatea);
            Controls.Add(txtUnitatea);
            Controls.Add(labelMin);
            Controls.Add(txtMin);
            Controls.Add(labelMax);
            Controls.Add(txtMax);
            Controls.Add(btnGorde);
            Controls.Add(btnUtzi);
            Controls.Add(pictureBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "ProduktuaAddForm";
            Text = "Produktua Gehitu";
            WindowState = FormWindowState.Maximized;
            Load += ProduktuaAddForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}




