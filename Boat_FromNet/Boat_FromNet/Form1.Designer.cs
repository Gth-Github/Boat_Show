using System.Windows.Forms;
namespace Boat_FromNet
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
        //protected override void Dispose(bool disposing)
        //{
         //   if (disposing && (components != null))
         //   {
          //      components.Dispose();
         //   }
        //    base.Dispose(disposing);
       // }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.axRuderAngle1 = new AxRUDERANGLELib.AxRuderAngle();
            this.axRateOfTurn1 = new AxRATEOFTURNLib.AxRateOfTurn();
            this.axRPM1 = new AxRPMLib.AxRPM();
            this.axStartAir1 = new AxSTARTAIRLib.AxStartAir();
            this.axCompass1 = new AxCOMPASSLib.AxCompass();
            this.axBoatLocation1 = new AxBOATLOCATIONLib.AxBoatLocation();
            ((System.ComponentModel.ISupportInitialize)(this.axRuderAngle1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axRateOfTurn1)).BeginInit() ;
            ((System.ComponentModel.ISupportInitialize)(this.axRPM1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axStartAir1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCompass1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axBoatLocation1)).BeginInit();
            var unity = new AxUnityWebPlayerAXLib.AxUnityWebPlayer();
            ((System.ComponentModel.ISupportInitialize)(unity)).BeginInit();
            Controls.Add(unity);
            ((System.ComponentModel.ISupportInitialize)(unity)).EndInit();
            unity.src = Application.StartupPath + "\\Nonetship\\Nonetship.unity3d";
            MessageBox.Show("src:" + unity.src);
            AxHost.State objstate = unity.OcxState;
            this.Controls.Remove(unity);
            unity.Dispose();
            this.axUnityWebPlayer1 = new AxUnityWebPlayerAXLib.AxUnityWebPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.axUnityWebPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文中宋", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(725, 497);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = " ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("华文中宋", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(16, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("华文中宋", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(144, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "label2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("华文中宋", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(272, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("华文中宋", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(400, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "label5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("华文中宋", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(528, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("华文中宋", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(656, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "label7";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("华文中宋", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(784, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 16);
            this.label8.TabIndex = 14;
            this.label8.Text = "label8";
            // 
            // axRuderAngle1
            // 
            this.axRuderAngle1.Enabled = true;
            this.axRuderAngle1.Location = new System.Drawing.Point(737, 299);
            this.axRuderAngle1.Name = "axRuderAngle1";
            this.axRuderAngle1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axRuderAngle1.OcxState")));
            this.axRuderAngle1.Size = new System.Drawing.Size(150, 150);
            this.axRuderAngle1.TabIndex = 5;
            // 
            // axRateOfTurn1
            // 
            this.axRateOfTurn1.Enabled = true;
            this.axRateOfTurn1.Location = new System.Drawing.Point(569, 299);
            this.axRateOfTurn1.Name = "axRateOfTurn1";
            this.axRateOfTurn1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axRateOfTurn1.OcxState")));
            this.axRateOfTurn1.Size = new System.Drawing.Size(150, 150);
            this.axRateOfTurn1.TabIndex = 4;
            // 
            // axRPM1
            // 
            this.axRPM1.Enabled = true;
            this.axRPM1.Location = new System.Drawing.Point(569, 455);
            this.axRPM1.Name = "axRPM1";
            this.axRPM1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axRPM1.OcxState")));
            this.axRPM1.Size = new System.Drawing.Size(150, 150);
            this.axRPM1.TabIndex = 2;
            // 
            // axStartAir1
            // 
            this.axStartAir1.Enabled = true;
            this.axStartAir1.Location = new System.Drawing.Point(12, 299);
            this.axStartAir1.Name = "axStartAir1";
            this.axStartAir1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axStartAir1.OcxState")));
            this.axStartAir1.Size = new System.Drawing.Size(225, 306);
            this.axStartAir1.TabIndex = 6;
            // 
            // axCompass1
            // 
            this.axCompass1.Enabled = true;
            this.axCompass1.Location = new System.Drawing.Point(254, 299);
            this.axCompass1.Name = "axCompass1";
            this.axCompass1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCompass1.OcxState")));
            this.axCompass1.Size = new System.Drawing.Size(300, 306);
            this.axCompass1.TabIndex = 3;
            // 
            // axUnityWebPlayer1
            // 
            this.axUnityWebPlayer1.Enabled = true;
            this.axUnityWebPlayer1.Location = new System.Drawing.Point(12, 37);
            this.axUnityWebPlayer1.Name = "axUnityWebPlayer1";
            //this.axUnityWebPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axUnityWebPlayer1.OcxState")));

            this.axUnityWebPlayer1.OcxState = objstate;
            this.axUnityWebPlayer1.Size = new System.Drawing.Size(450, 252);
            this.axUnityWebPlayer1.TabIndex = 0;
            // 
            // axBoatLocation1
            // 
            this.axBoatLocation1.Enabled = true;
            this.axBoatLocation1.Location = new System.Drawing.Point(481, 37);
            this.axBoatLocation1.Name = "axBoatLocation1";
            this.axBoatLocation1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axBoatLocation1.OcxState")));
            this.axBoatLocation1.Size = new System.Drawing.Size(406, 252);
            this.axBoatLocation1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(899, 614);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.axStartAir1);
            this.Controls.Add(this.axRuderAngle1);
            this.Controls.Add(this.axRateOfTurn1);
            this.Controls.Add(this.axCompass1);
            this.Controls.Add(this.axRPM1);
            this.Controls.Add(this.axBoatLocation1);
            this.Controls.Add(this.axUnityWebPlayer1);
            this.Name = "Form1";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axRuderAngle1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axRateOfTurn1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axRPM1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axStartAir1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCompass1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axBoatLocation1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axUnityWebPlayer1)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void @new(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private AxUnityWebPlayerAXLib.AxUnityWebPlayer axUnityWebPlayer1;
        private AxBOATLOCATIONLib.AxBoatLocation axBoatLocation1;
        private AxRPMLib.AxRPM axRPM1;
        private AxCOMPASSLib.AxCompass axCompass1;
        private AxRATEOFTURNLib.AxRateOfTurn axRateOfTurn1;
        private AxRUDERANGLELib.AxRuderAngle axRuderAngle1;
        private AxSTARTAIRLib.AxStartAir axStartAir1;
        private System.Windows.Forms.Label label1;//显示当前时间
        public System.Windows.Forms.Timer timer1;//定时器（用于当前时间控制）
        private System.Windows.Forms.Label label3; // MAG（m_MAG）
        private System.Windows.Forms.Label label2;//HDG（m_HDG）
        private System.Windows.Forms.Label label4;//HDG (M_COG)
        private System.Windows.Forms.Label label5;//HDG （m_SOG）
        private System.Windows.Forms.Label label6;//HDG （m_DRIFT）
        private System.Windows.Forms.Label label7;// HDG （m_ROT）
        private System.Windows.Forms.Label label8;  // HDG (m_RAD)
    }
}