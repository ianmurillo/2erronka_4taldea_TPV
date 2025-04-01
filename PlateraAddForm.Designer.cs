namespace _2taldea
{
    partial class PlateraAddForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtIzena;
        private ComboBox cmbKategoria; // Se ha corregido a ComboBox
        private TextBox txtMenu;
        private TextBox txtPrezioa;
        private Button btnGorde;
        private Button btnUtzi;
        private Label labelTitle;
        private Label labelIzena;
        private Label labelKategoria;
        private Label labelKantitatea;
        private Label labelPrezioa;
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
            cmbKategoria = new ComboBox();
            txtMenu = new TextBox();
            txtPrezioa = new TextBox();
            btnGorde = new Button();
            btnUtzi = new Button();
            labelTitle = new Label();
            labelIzena = new Label();
            labelKategoria = new Label();
            labelKantitatea = new Label();
            labelPrezioa = new Label();
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
            // cmbKategoria
            // 
            cmbKategoria.BackColor = Color.FromArgb(186, 69, 13);
            cmbKategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKategoria.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            cmbKategoria.ForeColor = SystemColors.Window;
            cmbKategoria.FormattingEnabled = true;
            cmbKategoria.Items.AddRange(new object[] { "Edaria", "Lehen_Platera", "Bigarren_Platera", "Postrea" });
            cmbKategoria.Location = new Point(850, 450);
            cmbKategoria.Name = "cmbKategoria";
            cmbKategoria.Size = new Size(350, 33);
            cmbKategoria.TabIndex = 1;
            // 
            // txtMenu
            // 
            txtMenu.BackColor = Color.FromArgb(186, 69, 13);
            txtMenu.ForeColor = Color.White;
            txtMenu.Location = new Point(850, 563);
            txtMenu.Name = "txtMenu";
            txtMenu.Size = new Size(350, 23);
            txtMenu.TabIndex = 3;
            // 
            // txtPrezioa
            // 
            txtPrezioa.BackColor = Color.FromArgb(186, 69, 13);
            txtPrezioa.ForeColor = Color.White;
            txtPrezioa.Location = new Point(850, 512);
            txtPrezioa.Name = "txtPrezioa";
            txtPrezioa.Size = new Size(350, 23);
            txtPrezioa.TabIndex = 2;
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
            btnGorde.TabIndex = 5;
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
            btnUtzi.TabIndex = 6;
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
            labelTitle.Size = new Size(233, 45);
            labelTitle.TabIndex = 7;
            labelTitle.Text = "Platera Gehitu";
            // 
            // labelIzena
            // 
            labelIzena.AutoSize = true;
            labelIzena.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelIzena.ForeColor = Color.White;
            labelIzena.Location = new Point(600, 400);
            labelIzena.Name = "labelIzena";
            labelIzena.Size = new Size(64, 25);
            labelIzena.TabIndex = 8;
            labelIzena.Text = "Izena:";
            // 
            // labelKategoria
            // 
            labelKategoria.AutoSize = true;
            labelKategoria.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelKategoria.ForeColor = Color.White;
            labelKategoria.Location = new Point(600, 450);
            labelKategoria.Name = "labelKategoria";
            labelKategoria.Size = new Size(103, 25);
            labelKategoria.TabIndex = 9;
            labelKategoria.Text = "Kategoria:";
            // 
            // labelKantitatea
            // 
            labelKantitatea.AutoSize = true;
            labelKantitatea.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelKantitatea.ForeColor = Color.White;
            labelKantitatea.Location = new Point(600, 563);
            labelKantitatea.Name = "labelKantitatea";
            labelKantitatea.Size = new Size(69, 25);
            labelKantitatea.TabIndex = 12;
            labelKantitatea.Text = "Menu:";
            // 
            // labelPrezioa
            // 
            labelPrezioa.AutoSize = true;
            labelPrezioa.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelPrezioa.ForeColor = Color.White;
            labelPrezioa.Location = new Point(600, 512);
            labelPrezioa.Name = "labelPrezioa";
            labelPrezioa.Size = new Size(83, 25);
            labelPrezioa.TabIndex = 10;
            labelPrezioa.Text = "Prezioa:";
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(0, 0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(100, 50);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // PlateraAddForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(9, 23, 37);
            ClientSize = new Size(1920, 1061);
            Controls.Add(labelTitle);
            Controls.Add(txtIzena);
            Controls.Add(cmbKategoria);
            Controls.Add(txtMenu);
            Controls.Add(txtPrezioa);
            Controls.Add(labelIzena);
            Controls.Add(labelKategoria);
            Controls.Add(labelKantitatea);
            Controls.Add(labelPrezioa);
            Controls.Add(btnGorde);
            Controls.Add(btnUtzi);
            Name = "PlateraAddForm";
            Text = "Platera Gehitu";
            Load += PlateraAddForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
