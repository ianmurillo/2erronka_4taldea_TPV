namespace _2taldea
{
    partial class ProduktuakFiltratu
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnPrezioa;
        private Button btnStocka;

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
            btnPrezioa = new Button();
            btnStocka = new Button();
            SuspendLayout();
            // 
            // btnPrezioa
            // 
            btnPrezioa.BackColor = Color.FromArgb(186, 69, 13);
            btnPrezioa.FlatStyle = FlatStyle.Flat;
            btnPrezioa.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnPrezioa.ForeColor = Color.White;
            btnPrezioa.Location = new Point(50, 50);
            btnPrezioa.Name = "btnPrezioa";
            btnPrezioa.Size = new Size(150, 50);
            btnPrezioa.TabIndex = 0;
            btnPrezioa.Text = "Filtratu Prezioa";
            btnPrezioa.UseVisualStyleBackColor = false;
            btnPrezioa.Click += btnPrezioa_Click;
            // 
            // btnStocka
            // 
            btnStocka.BackColor = Color.FromArgb(186, 69, 13);
            btnStocka.FlatStyle = FlatStyle.Flat;
            btnStocka.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnStocka.ForeColor = Color.White;
            btnStocka.Location = new Point(50, 120);
            btnStocka.Name = "btnStocka";
            btnStocka.Size = new Size(150, 50);
            btnStocka.TabIndex = 1;
            btnStocka.Text = "Filtratu Stocka";
            btnStocka.UseVisualStyleBackColor = false;
            btnStocka.Click += btnStocka_Click;
            // 
            // ProduktuakFiltratu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(9, 23, 37);
            ClientSize = new Size(250, 200);
            Controls.Add(btnPrezioa);
            Controls.Add(btnStocka);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "ProduktuakFiltratu";
            Text = "Filtratu Produktuak";
            Load += ProduktuakFiltratu_Load;
            ResumeLayout(false);
        }
    }
}
