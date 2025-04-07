namespace _2taldea
{
    partial class MesaDetallesForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label mesaLabel;
        private TabControl tabControl;
        private TabPage bebidasTab;
        private TabPage primerPlatoTab;
        private TabPage segundoPlatoTab;
        private TabPage postreaTab;

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
            mesaLabel = new Label();
            tabControl = new TabControl();
            bebidasTab = new TabPage();
            primerPlatoTab = new TabPage();
            segundoPlatoTab = new TabPage();
            postreaTab = new TabPage();
            tabControl.SuspendLayout();
            SuspendLayout();
            // 
            // mesaLabel
            // 
            mesaLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            mesaLabel.AutoSize = true;
            mesaLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            mesaLabel.ForeColor = Color.DarkSlateGray;
            mesaLabel.Location = new Point(1672, 10);
            mesaLabel.Name = "mesaLabel";
            mesaLabel.Size = new Size(79, 25);
            mesaLabel.TabIndex = 0;
            mesaLabel.Text = "Mesa: 0";
            // 
            // tabControl
            // 
            tabControl.Controls.Add(bebidasTab);
            tabControl.Controls.Add(primerPlatoTab);
            tabControl.Controls.Add(segundoPlatoTab);
            tabControl.Controls.Add(postreaTab);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1796, 930);
            tabControl.TabIndex = 0;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            // 
            // bebidasTab
            // 
            bebidasTab.BackColor = Color.FromArgb(9, 23, 37);
            bebidasTab.Location = new Point(4, 24);
            bebidasTab.Name = "bebidasTab";
            bebidasTab.Size = new Size(1788, 902);
            bebidasTab.TabIndex = 0;
            bebidasTab.Text = "Edaria";
            bebidasTab.Click += bebidasTab_Click;
            // 
            // primerPlatoTab
            // 
            primerPlatoTab.BackColor = Color.FromArgb(9, 23, 37);
            primerPlatoTab.Location = new Point(4, 24);
            primerPlatoTab.Name = "primerPlatoTab";
            primerPlatoTab.Size = new Size(1788, 902);
            primerPlatoTab.TabIndex = 1;
            primerPlatoTab.Text = "Lehen_Platera";
            primerPlatoTab.Click += primerPlatoTab_Click;
            // 
            // segundoPlatoTab
            // 
            segundoPlatoTab.BackColor = Color.FromArgb(9, 23, 37);
            segundoPlatoTab.Location = new Point(4, 24);
            segundoPlatoTab.Name = "segundoPlatoTab";
            segundoPlatoTab.Size = new Size(1788, 902);
            segundoPlatoTab.TabIndex = 2;
            segundoPlatoTab.Text = "Bigarren_Platera";
            // 
            // postreaTab
            // 
            postreaTab.BackColor = Color.FromArgb(9, 23, 37);
            postreaTab.Location = new Point(4, 24);
            postreaTab.Name = "postreaTab";
            postreaTab.Size = new Size(1788, 902);
            postreaTab.TabIndex = 3;
            postreaTab.Text = "Postrea";
            // 
            // MesaDetallesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1796, 930);
            Controls.Add(tabControl);
            Controls.Add(mesaLabel);
            Name = "MesaDetallesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Detalles de la Mesa";
            WindowState = FormWindowState.Maximized;
            Load += MesaDetallesForm_Load;
            tabControl.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
