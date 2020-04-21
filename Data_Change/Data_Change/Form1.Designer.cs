namespace Data_Change
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.axHaikuang1 = new AxHAIKUANGLib.AxHaikuang();
            this.axAttachbitmap1 = new AxATTACHBITMAPLib.AxAttachbitmap();
            this.axProgressB1 = new AxPROGRESSBLib.AxProgressB();
            this.axSpeed1 = new AxSPEEDLib.AxSpeed();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axHaikuang1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axAttachbitmap1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axProgressB1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSpeed1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Enabled = false;
            this.label4.Font = new System.Drawing.Font("华文中宋", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(589, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 85);
            this.label4.TabIndex = 9;
            this.label4.Text = "P";
            this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label4_MouseDown);
            this.label4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label4_MouseUp);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Enabled = false;
            this.label5.Font = new System.Drawing.Font("华文中宋", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(673, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 85);
            this.label5.TabIndex = 10;
            this.label5.Text = "   N";
            this.label5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label5_MouseDown);
            this.label5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label5_MouseUp);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Data_Change.Properties.Resources._2_2;
            this.pictureBox2.Location = new System.Drawing.Point(573, 212);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(160, 160);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Data_Change.Properties.Resources._1_1;
            this.pictureBox1.Location = new System.Drawing.Point(404, 212);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 160);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Yellow;
            this.button1.Location = new System.Drawing.Point(223, 317);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "NoFollow";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Yellow;
            this.button2.Location = new System.Drawing.Point(223, 277);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Follow";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Yellow;
            this.button3.Location = new System.Drawing.Point(221, 236);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(44, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "Auto";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("华文中宋", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(561, 378);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(79, 30);
            this.button4.TabIndex = 14;
            this.button4.Text = "开始";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("华文中宋", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(653, 378);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(79, 30);
            this.button5.TabIndex = 15;
            this.button5.Text = "退出";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文中宋", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(400, 384);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 19);
            this.label1.TabIndex = 16;
            this.label1.Text = "船首向";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("华文中宋", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(466, 381);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(51, 29);
            this.textBox1.TabIndex = 17;
            this.textBox1.Validated += new System.EventHandler(this.textBox1_Validated);
            // 
            // axHaikuang1
            // 
            this.axHaikuang1.Enabled = true;
            this.axHaikuang1.Location = new System.Drawing.Point(306, 212);
            this.axHaikuang1.Name = "axHaikuang1";
            this.axHaikuang1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axHaikuang1.OcxState")));
            this.axHaikuang1.Size = new System.Drawing.Size(88, 160);
            this.axHaikuang1.TabIndex = 3;
            // 
            // axAttachbitmap1
            // 
            this.axAttachbitmap1.Enabled = true;
            this.axAttachbitmap1.Location = new System.Drawing.Point(208, 212);
            this.axAttachbitmap1.Name = "axAttachbitmap1";
            this.axAttachbitmap1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAttachbitmap1.OcxState")));
            this.axAttachbitmap1.Size = new System.Drawing.Size(88, 160);
            this.axAttachbitmap1.TabIndex = 2;
            // 
            // axProgressB1
            // 
            this.axProgressB1.Enabled = true;
            this.axProgressB1.Location = new System.Drawing.Point(208, 8);
            this.axProgressB1.Name = "axProgressB1";
            this.axProgressB1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axProgressB1.OcxState")));
            this.axProgressB1.Size = new System.Drawing.Size(525, 194);
            this.axProgressB1.TabIndex = 1;
            this.axProgressB1.MouseCaptureChanged += new System.EventHandler(this.axProgressB1_MouseCaptureChanged);
            // 
            // axSpeed1
            // 
            this.axSpeed1.Enabled = true;
            this.axSpeed1.Location = new System.Drawing.Point(9, 8);
            this.axSpeed1.Name = "axSpeed1";
            this.axSpeed1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSpeed1.OcxState")));
            this.axSpeed1.Size = new System.Drawing.Size(188, 400);
            this.axSpeed1.TabIndex = 0;
            this.axSpeed1.MouseCaptureChanged += new System.EventHandler(this.axSpeed1_MouseCaptureChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(742, 419);
            this.ControlBox = false;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.axHaikuang1);
            this.Controls.Add(this.axAttachbitmap1);
            this.Controls.Add(this.axProgressB1);
            this.Controls.Add(this.axSpeed1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axHaikuang1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axAttachbitmap1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axProgressB1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSpeed1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private AxSPEEDLib.AxSpeed axSpeed1;
        private AxPROGRESSBLib.AxProgressB axProgressB1;
        private AxATTACHBITMAPLib.AxAttachbitmap axAttachbitmap1;
        private AxHAIKUANGLib.AxHaikuang axHaikuang1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

