namespace SAR_Overlay
{
    partial class OverlayForm
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
            this.buttonNight = new System.Windows.Forms.Button();
            this.buttonMatchID = new System.Windows.Forms.Button();
            this.buttonSoccer = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.buttonTeleport = new System.Windows.Forms.Button();
            this.buttonSwithGas = new System.Windows.Forms.Button();
            this.buttonDuel = new System.Windows.Forms.Button();
            this.trackBarGasSpeed = new System.Windows.Forms.TrackBar();
            this.trackBarGasDamage = new System.Windows.Forms.TrackBar();
            this.trackBarBulletSpeed = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGasSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGasDamage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBulletSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNight
            // 
            this.buttonNight.Location = new System.Drawing.Point(82, 0);
            this.buttonNight.Name = "buttonNight";
            this.buttonNight.Size = new System.Drawing.Size(64, 32);
            this.buttonNight.TabIndex = 0;
            this.buttonNight.Text = "Ночь";
            this.buttonNight.UseVisualStyleBackColor = true;
            this.buttonNight.Click += new System.EventHandler(this.ButtonNight_Click);
            // 
            // buttonMatchID
            // 
            this.buttonMatchID.Location = new System.Drawing.Point(12, 0);
            this.buttonMatchID.Name = "buttonMatchID";
            this.buttonMatchID.Size = new System.Drawing.Size(64, 32);
            this.buttonMatchID.TabIndex = 1;
            this.buttonMatchID.Text = "ID Матча";
            this.buttonMatchID.UseVisualStyleBackColor = true;
            this.buttonMatchID.Click += new System.EventHandler(this.ButtonMatchID_Click);
            // 
            // buttonSoccer
            // 
            this.buttonSoccer.Location = new System.Drawing.Point(152, 0);
            this.buttonSoccer.Name = "buttonSoccer";
            this.buttonSoccer.Size = new System.Drawing.Size(64, 32);
            this.buttonSoccer.TabIndex = 2;
            this.buttonSoccer.Text = "Мяч";
            this.buttonSoccer.UseVisualStyleBackColor = true;
            this.buttonSoccer.Click += new System.EventHandler(this.ButtonSoccer_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(1159, 0);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(64, 32);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Старт";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.LightGray;
            this.checkBox1.Location = new System.Drawing.Point(1045, 9);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(108, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Добавить ботов";
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // buttonTeleport
            // 
            this.buttonTeleport.Location = new System.Drawing.Point(222, 0);
            this.buttonTeleport.Name = "buttonTeleport";
            this.buttonTeleport.Size = new System.Drawing.Size(64, 32);
            this.buttonTeleport.TabIndex = 5;
            this.buttonTeleport.Text = "Телепорт";
            this.buttonTeleport.UseVisualStyleBackColor = true;
            this.buttonTeleport.Click += new System.EventHandler(this.ButtonTeleport_Click);
            // 
            // buttonSwithGas
            // 
            this.buttonSwithGas.Location = new System.Drawing.Point(292, 0);
            this.buttonSwithGas.Name = "buttonSwithGas";
            this.buttonSwithGas.Size = new System.Drawing.Size(64, 32);
            this.buttonSwithGas.TabIndex = 6;
            this.buttonSwithGas.Text = "Газ";
            this.buttonSwithGas.UseVisualStyleBackColor = true;
            this.buttonSwithGas.Click += new System.EventHandler(this.ButtonSwithGas_Click);
            // 
            // buttonDuel
            // 
            this.buttonDuel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDuel.Location = new System.Drawing.Point(975, 0);
            this.buttonDuel.Name = "buttonDuel";
            this.buttonDuel.Size = new System.Drawing.Size(64, 32);
            this.buttonDuel.TabIndex = 7;
            this.buttonDuel.Text = "Сценарий";
            this.buttonDuel.UseVisualStyleBackColor = true;
            this.buttonDuel.Click += new System.EventHandler(this.ButtonDuel_Click);
            // 
            // trackBarGasSpeed
            // 
            this.trackBarGasSpeed.BackColor = System.Drawing.SystemColors.ControlLight;
            this.trackBarGasSpeed.Location = new System.Drawing.Point(854, 0);
            this.trackBarGasSpeed.Maximum = 30;
            this.trackBarGasSpeed.Minimum = 4;
            this.trackBarGasSpeed.Name = "trackBarGasSpeed";
            this.trackBarGasSpeed.Size = new System.Drawing.Size(115, 45);
            this.trackBarGasSpeed.TabIndex = 8;
            this.trackBarGasSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarGasSpeed.Value = 10;
            this.trackBarGasSpeed.Scroll += new System.EventHandler(this.TrackBarGasSpeed_Scroll);
            // 
            // trackBarGasDamage
            // 
            this.trackBarGasDamage.BackColor = System.Drawing.SystemColors.ControlLight;
            this.trackBarGasDamage.Location = new System.Drawing.Point(733, 0);
            this.trackBarGasDamage.Minimum = 1;
            this.trackBarGasDamage.Name = "trackBarGasDamage";
            this.trackBarGasDamage.Size = new System.Drawing.Size(115, 45);
            this.trackBarGasDamage.TabIndex = 9;
            this.trackBarGasDamage.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarGasDamage.Value = 1;
            this.trackBarGasDamage.Scroll += new System.EventHandler(this.TrackBarGasDamage_Scroll);
            // 
            // trackBarBulletSpeed
            // 
            this.trackBarBulletSpeed.BackColor = System.Drawing.SystemColors.ControlLight;
            this.trackBarBulletSpeed.Location = new System.Drawing.Point(612, 0);
            this.trackBarBulletSpeed.Maximum = 20;
            this.trackBarBulletSpeed.Minimum = 5;
            this.trackBarBulletSpeed.Name = "trackBarBulletSpeed";
            this.trackBarBulletSpeed.Size = new System.Drawing.Size(115, 45);
            this.trackBarBulletSpeed.TabIndex = 10;
            this.trackBarBulletSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarBulletSpeed.Value = 10;
            this.trackBarBulletSpeed.Visible = false;
            // 
            // OverlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 32);
            this.Controls.Add(this.trackBarBulletSpeed);
            this.Controls.Add(this.trackBarGasDamage);
            this.Controls.Add(this.trackBarGasSpeed);
            this.Controls.Add(this.buttonDuel);
            this.Controls.Add(this.buttonSwithGas);
            this.Controls.Add(this.buttonTeleport);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonSoccer);
            this.Controls.Add(this.buttonMatchID);
            this.Controls.Add(this.buttonNight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OverlayForm";
            this.Text = "OverlayForm";
            this.Load += new System.EventHandler(this.OverlayForm_Load);
            this.Click += new System.EventHandler(this.OverlayForm_Click);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGasSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGasDamage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBulletSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNight;
        private System.Windows.Forms.Button buttonMatchID;
        private System.Windows.Forms.Button buttonSoccer;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button buttonTeleport;
        private System.Windows.Forms.Button buttonSwithGas;
        private System.Windows.Forms.Button buttonDuel;
        private System.Windows.Forms.TrackBar trackBarGasSpeed;
        private System.Windows.Forms.TrackBar trackBarGasDamage;
        private System.Windows.Forms.TrackBar trackBarBulletSpeed;
    }
}