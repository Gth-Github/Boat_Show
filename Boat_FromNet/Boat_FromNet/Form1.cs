using Boat_FromNet.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace Boat_FromNet
{


    /*主窗体程序：一个窗体其实就是一个类。Form1继承了系统的Form类
     partial：部分类。允许一个类放到多个文件中
    */
    public partial class Form1 : Form
    {
        //--------------窗体输入数据-------------
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        public Thread m_threadShipRun = null; //模拟船舶运行
        public int RUNSTATUS = 0; // 0停止 1运行
        runge_kutta m_runge_kutta = null;
        double dj = 0, d = 0; //舵角
        double lastdj = 0;
        int cz = 0, c = 0; //车钟
        int moxing = 1, m = 1; //船模型选择
        int st = 0, stt = 0;
        double Luo_jing = 0.0;
        double m_HDG = 0;//电罗经
        double m_MAG = 0;//磁罗经
        int T_RPM;
        int DeSt;//表示船的状态
        //--------网络通信输入数据（服务器接收端）---------
        public string ipAddress = "127.0.0.1";
        public int ConnectPort = 8090;
        public Socket socket;
        public IPEndPoint ipEnd;
        public Thread connectThread = null;
        private float X;
        private float Y;
        double m_SET = 0.0;

        [DllImport("ShipMotionDll.dll", EntryPoint = "ResetSolutionMem")]
        public static extern double ResetSolutionMem();
        [DllImport("ShipMotionDll.dll", EntryPoint = "GetVaribalZ")]
        public static extern double GetVaribalZ();
        [DllImport("ShipMotionDll.dll", EntryPoint = "SetDelta")]
        public static extern double SetDelta(double Del);
        [DllImport("ShipMotionDll.dll", EntryPoint = "SetChezhong")]
        public static extern double SetChezhong(int cz);
        //----------------------------------------------------------------------
        int D_Jao=0;
        double D_Ling=0;
        double m_hansu;
        double drift, cog, m_rot;
        int g_nlangji = 0;	//浪级
        int g_nSpeed = 1;	//速度
        int a, b;
        double deltae;
        double temp;
        double dtmpduojiao,dtmpduojiao2;
        double D_LingLast = 0;
        double m_COG,m_SOG, m_ROT,m_RAD;

        double m_DRIFT;
        static double m_nLastduojiao = 0;
        /// <summary>
        /// 初始化窗口
        /// </summary>
        public Form1()  
        {
            InitializeComponent();//对当前窗体的控件进行初始化工作
            this.Location = new Point(0,0);
        }
        /// <summary>
        /// 窗体在显示之前最后一个被触发的事件，所以一般在这个事件对窗体上的控件进行赋值初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            m_COG = 0;m_SOG = 0;m_HDG = 0;
            m_MAG = 0;m_ROT = 0;m_RAD = 0;
            m_DRIFT = 0;
            dtmpduojiao2 = 0;
            //------------------------------
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Data_Exchang);

            timer.AutoReset = true;
            //---------------驱动船舶运动Thread---------------
            m_threadShipRun = new Thread(ShipRunModelThread);
            //---------------网络通信Thread-------------------
            InitSocket();
            // 窗体 、控件 改变控制(控件随窗体改变而改变)
            this.Resize += new EventHandler(Form1_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
           //---------------------------
            Form1_Resize(new object(), new EventArgs());//x,y可在实例化时赋值,最后这句是新加的，在MDI时有用
            //开始时的初始化（之前开始按钮下的代码部分）
            if (m_runge_kutta == null)
            {
                m_runge_kutta = new runge_kutta(D_Ling, g_nlangji);
                m_runge_kutta.t = 0;
            }
            deltae = 0;
            //开启模拟运行线程
            //m_threadShipRun.Start();
            timer.Enabled = true;
            timer.Start();
            //开启数据接收线程
            connectThread.Start();
            //页面数据显示设置
            label1.Text = "当前时间:\n\n\n" + DateTime.Now.ToString();
            label3.Text = "HDG：" + m_HDG.ToString() + "度"; //1 HDG
            label2.Text = "MAG：" + m_MAG.ToString() + "度";// 2 MAG
            label4.Text = "HDG：" + m_COG.ToString() + "度"; // 3 M_COG
            label5.Text = "HDG：" + m_SOG.ToString() + "节";// 4 M_SOG
            label6.Text = "HDG：" + m_DRIFT.ToString() + "度"; // 5 m_DRIFT
            label7.Text = "HDG：" + m_ROT.ToString() + "度/分";  //6 m_ROT
            label8.Text = "HDG：" + m_RAD.ToString() + ""; // 7 m_RAD
            
        }
        //控制窗体、控件一起变化
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * Math.Min(newx, newy);
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }
        void Form1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
            this.Text = this.Width.ToString() + " " + this.Height.ToString();
        }
        /// <summary>
        /// 船模型运行线程
        /// </summary>
        public void ShipRunModelThread()
        {
            while (true)
            {
                float b;
                g_nSpeed = cz;
                g_nlangji = 0; //浪级固定了
                D_Ling = dj;
                m_runge_kutta.input((int)D_Ling, g_nlangji, g_nSpeed);//读入
                if (DeSt == 1) //应急舵
                {
                    //调整舵角的变化
                    if (D_Ling < -30)
                    {
                        D_Ling = -30;
                    }
                    else if (D_Ling > 30)
                    {
                        D_Ling = 30;
                    }
                    int a = (int)D_Ling;
                }
                if (m_runge_kutta != null)
                {
                    if (DeSt != 2)// 不是自动舵的情况
                    {
                        temp = D_Ling;
                        if (temp < -30)
                        {
                            temp = -30;
                        } if (temp > 30)
                        {
                            temp = 30;
                        }
                        deltae = (temp*1.0 / 180) * 3.1415926;
                        dtmpduojiao = 0;
                        m_runge_kutta.h = 0.2;//0.1  h 控制船运行的速度  h越大  船运行刷新越快 否则，越慢；
                        if (D_LingLast != D_Ling)
                        {
                            D_LingLast = D_Ling;
                            //m_runge_kutta.t = 0;/////////////////////
                            //m_runge_kutta.input((int)D_Ling, g_nlangji, g_nSpeed);//读入
                            dtmpduojiao2 = m_nLastduojiao;
                        }
                        m_runge_kutta.solution(ref deltae,ref dtmpduojiao,ref dtmpduojiao2, m_SET * 3.14 / 180);
                        m_nLastduojiao = dtmpduojiao;   

                    }
                    else//自动舵
                    {
                        dtmpduojiao2 = m_nLastduojiao;
                        m_runge_kutta.h = 1.2;   //初始设置为1.0
                        //m_runge_kutta.input((int)D_Ling, g_nlangji, g_nSpeed);//读入 
                        m_runge_kutta.solution(ref deltae, ref dtmpduojiao, ref dtmpduojiao2, (-1) * m_SET * 3.14 / 180.0);
                        m_nLastduojiao = dtmpduojiao;
                    }
                    m_nLastduojiao = (dtmpduojiao * 180.0) / 3.1415926;
                    if ((dtmpduojiao * 180.0) / 3.1415926 != D_Jao)
                    {
                        D_Jao = (int)((dtmpduojiao * 180.0) / 3.1415926);
                    }
                    if (m_runge_kutta.z[2] != Luo_jing)
                    {
                        m_HDG = m_runge_kutta.z[2];  //m_HDG设置罗经
                    }
                    Luo_jing = m_HDG = (m_HDG * 180) / 3.1415926;
                    while (m_HDG > 360)
                    {
                        m_HDG = m_HDG - 360;
                    }
                    while (m_HDG < -360)
                    {
                        m_HDG = m_HDG + 360;
                    }
                    if (m_HDG>0)
		            {
		            	m_HDG=360-m_HDG;
		            }
	            	else
	            	{
	            		m_HDG=-1*m_HDG;
	            	}
	            	m_HDG=((int)(m_HDG*10))/10.0;
                    //SOG
                    m_SOG = m_runge_kutta.z[8];
                    m_SOG = m_SOG * 3600 / 1852;
                    m_SOG=((int)(m_SOG*10))/10.0;
                    //DRIFT
                    m_DRIFT = m_runge_kutta.drift;	
		            m_DRIFT = (m_DRIFT*180)/3.1415926;
		            while (m_DRIFT>360)
		            {
		            	m_DRIFT=m_DRIFT-360;

		            }
		            if (m_DRIFT<0)
		            {
		            	m_DRIFT=m_DRIFT+360;
		            }
                    m_DRIFT = ((int)(m_DRIFT * 10)) / 10.0;
                    //COG
		            m_COG = m_runge_kutta.z[2] + m_runge_kutta.drift;
		            m_COG = (m_COG*180)/3.1415926;
		            while (m_COG>360)
		            {
		            	m_COG=m_COG-360;

		            }
		            while (m_COG<0)
		            {
		            	m_COG=m_COG+360;
	            	}
                    m_COG = ((int)(m_COG * 10)) / 10.0;
                    //ROT
		            m_ROT = m_runge_kutta.z[6];
		            m_ROT = (m_ROT*180*60)/3.1415926;
		            while (m_ROT>360)
		            {
		            	m_ROT=m_ROT-360;	
		            }
                    m_rot = m_ROT = (int)(m_ROT);
                    //RAD
                    m_RAD = D_Jao;
                    Luo_jing = m_HDG;
                    //RAD
		            m_RAD=D_Jao;
                    m_RAD = ((int)(m_RAD * 10)) / 10.0;
                    axBoatLocation1.AddRoutin((short)(- m_runge_kutta.z[1] / 10), (short)(m_runge_kutta.z[0] / 10));
                    //---------设置各个表盘-------------------------------------------------------------------------------
                    axRateOfTurn1.SetAngle(D_Jao);
                    axRuderAngle1.SetAngle(-m_ROT);

                    a = (int)Luo_jing;
                    b = (float)(Luo_jing - (int)Luo_jing) * 10;
                    axCompass1.SetEData(0, a, b);  

                    Random random = new Random();
                    if (random.Next(0, 1) == 1)
                    {
                        m_MAG = m_HDG + random.Next(0, 4);
                    }
                    else
                    {
                        m_MAG = m_HDG - random.Next(0, 4);
                    }
                    while (m_MAG < 0)
                    {
                        m_MAG = m_MAG + 360;
                    }

                    //设置RPM的值
                    int T_Speed = cz;
                    int T_RPM=0;
                    switch (T_Speed)
                    {
                        case 4:
                            T_RPM = 83;
                            break;
                        case 3:
                            T_RPM = 50;
                            break;
                        case 2:
                            T_RPM = 36;
                            break;
                        case 1:
                            T_RPM = 26;
                            break;
                        case 0:
                            T_RPM = 0;
                            break;
                        case -1:
                            T_RPM = -26;
                            break;
                        case -2:
                            T_RPM = -36;
                            break;
                        case -3:
                            T_RPM = -50;
                            break;
                        case -4:
                            T_RPM = -60;
                            break;
                    }
                    axRPM1.SetAngle(T_RPM);
                }
            }
        }
        /// <summary>
        /// cz数据变化时，给unity发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_Exchang(object sender, System.Timers.ElapsedEventArgs e)
        {
            axUnityWebPlayer1.SendMessage("Boat", "hs_boat", cz);
        }
        /// <summary>
        /// 接收数据传入
        /// </summary>
        public void InitSocket()
        {
            ipEnd = new IPEndPoint(IPAddress.Parse(ipAddress), ConnectPort);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(ipEnd);
            //开启一个接收线程
            connectThread = new Thread(new ThreadStart(SocketReceive));
        }
        //服务器接收
        public void SocketReceive()
        {
            double D_LingLast = 0;
            int recvLen = -1;
            String recvStr = String.Empty;
            byte[] recvData = new byte[1024];
            EndPoint clientEnd = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                recvData = new byte[1024];
                recvLen = socket.ReceiveFrom(recvData, ref clientEnd);
                recvStr = Encoding.UTF8.GetString(recvData, 0, recvLen);//////////////
                //Console.WriteLine("网络数据包:" + recvStr);
                MessageBox.Show("网络数据包:" + recvStr);
                string[] vals = recvStr.Split('#');
                int[] result = new int[6];
                for (int i = 0; i < vals.Length; i++)
                {
                    int n = vals[i].Length - 1;
                    if (vals[i][0] == '-') 
                    {
                        for (int j = n; j > 0; j--)
                        {
                            result[i] += Convert.ToInt32((vals[i][j] - '0') * Math.Pow(10, n - j));
                        }
                        result[i] = -result[i];
                    }
                    else
                    {
                        for (int j = 0; j <= n; j++)
                        {
                            result[i] += Convert.ToInt32((vals[i][j] - '0') * Math.Pow(10, n - j));
                        }
                    }
                    moxing = result[0];//模型
                    cz = result[1];    //车钟
                    dj = result[2];    //舵角
                    st = result[3];    //船模型线程运行状态  开始；继续/暂停；结束
                    m_SET = result[4]; //自动舵时候（船首向设置）
                    DeSt = result[5];  //运行状态（自动舵还是随动舵）
                }

                if (c != cz)
                {
                    axUnityWebPlayer1.SendMessage("Boat", "hs_boat", cz);
                    //shipMotion.SetChezhong(cz);
                    g_nSpeed = cz;
                    c = cz;
                }
                if (dj != d)
                {
                    axUnityWebPlayer1.SendMessage("Boat", "xuanzhuan", dj);
                    //shipMotion.SetDelta(dj);
                    D_Ling = dj;

                    d = dj;
                }
                if (moxing != m)
                {
                    axUnityWebPlayer1.SendMessage("Sphere", "data", moxing);
                    m = moxing;
                    //shipMotion.changemoxing(moxing);
                }
                bool ThredState1 = false;
                if (st == 1)//暂停
                {
                    if (m_threadShipRun.IsAlive)
                    {
                        m_threadShipRun.Suspend();
                        timer.Stop();
                        ThredState1 = false;
                    }
                }
                else if (st == 2 && !ThredState1)//继续
                {
                    m_threadShipRun.Resume();
                    timer.Start();
                    ThredState1 = true;
                }else if(st == 3){//开始
                    if (m_threadShipRun.IsAlive == false)
                    {
                        m_threadShipRun.Start();
                        timer.Start();
                        ThredState1 = true;
                    }
                }else if(st == 4){
                    System.Environment.Exit(0);
                    Application.Exit();
                    Application.ExitThread();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "当前时间:\n\n\n" + DateTime.Now.ToString(); //时间
            label3.Text = "HDG：" + m_HDG.ToString() + "度"; //1 HDG
            label2.Text = "MAG：" + m_MAG.ToString() + "度";// 2 MAG
            label4.Text = "COG：" + m_COG.ToString() + "度"; // 3 M_COG
            label5.Text = "SOG：" + m_SOG.ToString() + "节";// 4 M_SOG
            label6.Text = "DRIFT：" + m_DRIFT.ToString() + "度"; // 5 m_DRIFT
            label7.Text = "ROT：" + m_ROT.ToString() + "度/分";  //6 m_ROT
            label8.Text = "RAD：" + m_RAD.ToString() + ""; // 7 m_RAD
        }
    }
}
