namespace SAR_Overlay
{
    partial class FormTeleport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxLocations = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxLocations
            // 
            this.listBoxLocations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxLocations.FormattingEnabled = true;
            this.listBoxLocations.Items.AddRange(new object[] {
            "4080 2050 - Port Pixile",
            "3265 1215 - Goldwood",
            "2715 1400 - Ferm",
            "2144 1017 - Boloto",
            "687 697 - Start",
            "1480 1874 - Beavers",
            "1405 2660 - Pyramid Enter"});
            this.listBoxLocations.Location = new System.Drawing.Point(0, 0);
            this.listBoxLocations.Name = "listBoxLocations";
            this.listBoxLocations.Size = new System.Drawing.Size(149, 216);
            this.listBoxLocations.TabIndex = 0;
            this.listBoxLocations.DoubleClick += new System.EventHandler(this.ListBoxLocations_DoubleClick);
            // 
            // FormTeleport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(149, 216);
            this.Controls.Add(this.listBoxLocations);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTeleport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Teleport";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormTeleport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxLocations;
    }
}