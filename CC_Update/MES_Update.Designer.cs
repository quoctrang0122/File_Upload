namespace CC_Update
{
    partial class MES_Update
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MES_Update));
            this.pnlLoading = new BevelPanel.AdvancedPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ptxwifi = new System.Windows.Forms.PictureBox();
            this.timUpdate = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.spinningCircles1 = new AltoControls.SpinningCircles();
            this.pnlLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptxwifi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLoading
            // 
            this.pnlLoading.BackColor = System.Drawing.Color.Transparent;
            this.pnlLoading.BackgroundGradientMode = BevelPanel.AdvancedPanel.PanelGradientMode.Vertical;
            this.pnlLoading.Controls.Add(this.label2);
            this.pnlLoading.Controls.Add(this.pictureBox1);
            this.pnlLoading.Controls.Add(this.label1);
            this.pnlLoading.Controls.Add(this.ptxwifi);
            this.pnlLoading.Controls.Add(this.spinningCircles1);
            this.pnlLoading.EdgeWidth = 0;
            this.pnlLoading.EndColor = System.Drawing.Color.WhiteSmoke;
            this.pnlLoading.FlatBorderColor = System.Drawing.Color.Empty;
            this.pnlLoading.ForeColor = System.Drawing.Color.Cyan;
            this.pnlLoading.Location = new System.Drawing.Point(5, 4);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.RectRadius = 10;
            this.pnlLoading.ShadowColor = System.Drawing.Color.Empty;
            this.pnlLoading.ShadowShift = 0;
            this.pnlLoading.ShadowStyle = BevelPanel.AdvancedPanel.ShadowMode.ForwardDiagonal;
            this.pnlLoading.Size = new System.Drawing.Size(285, 178);
            this.pnlLoading.StartColor = System.Drawing.Color.WhiteSmoke;
            this.pnlLoading.Style = BevelPanel.AdvancedPanel.BevelStyle.Flat;
            this.pnlLoading.TabIndex = 0;
            this.pnlLoading.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlLoading_Paint);
            this.pnlLoading.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlLoading_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(71, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Vui lòng chờ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(118)))), ((int)(((byte)(248)))));
            this.label1.Location = new System.Drawing.Point(8, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // ptxwifi
            // 
            this.ptxwifi.Location = new System.Drawing.Point(254, 4);
            this.ptxwifi.Name = "ptxwifi";
            this.ptxwifi.Size = new System.Drawing.Size(25, 25);
            this.ptxwifi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptxwifi.TabIndex = 1;
            this.ptxwifi.TabStop = false;
            // 
            // timUpdate
            // 
            this.timUpdate.Enabled = true;
            this.timUpdate.Interval = 1000;
            this.timUpdate.Tick += new System.EventHandler(this.timUpdate_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(106, 70);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(54, 51);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // spinningCircles1
            // 
            this.spinningCircles1.BackColor = System.Drawing.Color.Transparent;
            this.spinningCircles1.FullTransparent = true;
            this.spinningCircles1.Increment = 1F;
            this.spinningCircles1.Location = new System.Drawing.Point(76, 36);
            this.spinningCircles1.N = 8;
            this.spinningCircles1.Name = "spinningCircles1";
            this.spinningCircles1.Radius = 2.5F;
            this.spinningCircles1.Size = new System.Drawing.Size(129, 117);
            this.spinningCircles1.TabIndex = 5;
            this.spinningCircles1.Text = "spinningCircles1";
            // 
            // MES_Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(299, 189);
            this.Controls.Add(this.pnlLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MES_Update";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.DimGray;
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptxwifi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BevelPanel.AdvancedPanel pnlLoading;
        private System.Windows.Forms.Timer timUpdate;
        private System.Windows.Forms.PictureBox ptxwifi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private AltoControls.SpinningCircles spinningCircles1;
    }
}

