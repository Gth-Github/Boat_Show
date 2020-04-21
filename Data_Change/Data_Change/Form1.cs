using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

//using System.Drawing.Point;


namespace Data_Change
{
    public partial class Form1 : Form
    {
        //客户端
        public string recvStr;
        Socket socket;
        EndPoint serverEnd;
        IPEndPoint ipEnd;
        byte[] sendData = new byte[1024];
        string value;
        float cz = 3, c = 0; // 车钟
        int dj = 0, d = 0; // 船舵角
        static int moxing = 1, m = 1; // 表示模型选择
        int st = 0, stt = 0; // 表示开始、暂停、继续
        int DeSt = 0, DeStt = 0;//表示应急舵1、自动舵2、随动舵3
        public Thread m_threadDataChange = null;//向显示端发送数据线程
        public Thread cz_threadDataChange = null;//接收车钟的数据
        public Thread dj_threadDataChange = null;//接收舵角的数据
        Boolean State = false;
        int heading=0,heading1=0,heading2;
        int sleeptime = 5000;
        private float X;
        private float Y;
        string czip = "192.168.0.189";
        int czport = 8002;
        string djip = "192.168.0.189";
        int djport = 7003;
        public Form1()
        {     
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(900, 0);//用于设置窗体的显示位置（窗体的样式设置为Manual）
            //label外观设置
            label4.BackColor = Color.Transparent;
            label4.Parent = pictureBox2;
            label4.Location = new Point(10, 70);
            label5.BackColor = Color.Transparent;
            label5.Parent = pictureBox2;
            label5.Location = new Point(110, 70);
            this.textBox1.Text = "0";
        } 
        public void DataChangeThread()
        {
            while (true)
            {
                if (c != cz || dj != d || m != moxing || stt!=st || heading!=heading1 || DeSt!=DeStt)
                {
                    value = moxing + "#" + cz + "#" + dj+"#"+st+"#"+heading+"#"+DeSt;
                    SocketSend(value);
                    d = dj;
                    c = cz;
                    m = moxing;
                    stt = st;
                    heading1 = heading;
                    DeStt = DeSt;
                    st = 0;
                }
            }
        }
        short T_RPM = 0;
        private void axSpeed1_MouseCaptureChanged(object sender, EventArgs e)
        {
            cz = axSpeed1.GetSpeed();
            int T_Speed = (int)cz;
            short start = (short)cz;
            axSpeed1.SetStartAir(start);

            //表盘的车钟值(OK)
            //int T_RPM=0;
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
            axSpeed1.SetRPM(T_RPM);
        }
        private void axProgressB1_MouseCaptureChanged(object sender, EventArgs e)
        {

            //当为自动舵时，不起作用
            if (DeSt == 2)
            {
                return;
            }
            dj = Convert.ToInt32(axProgressB1.GetUpdata());
            axProgressB1.SetUpdata(dj);
        }
        /// <summary>
        /// 向显示端发送数据通信
        /// </summary>
        public void InitSocket()
        {
            ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8090); // IP 地址    以及端口号
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            serverEnd = (EndPoint)sender;
            Console.WriteLine("等待连接");
        }
        public void SocketSend(string sendStr)
        {
            // 清空
            sendData = new byte[1024];
            //数据转换
            sendData = Encoding.UTF8.GetBytes(sendStr);//BitConverter.GetBytes(sendStr);  
            //发送给指定的服务端
            socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipEnd);
            //Console.WriteLine("已发送数据：" + value);
        }
        /*模型按钮控制*/
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (moxing == 1)
            {
                //this.pictureBox1.Image = Image.FromFile("C:\\Users\\Administrator\\Desktop\\0-2.jpg");
                pictureBox1.Image = Properties.Resources._1_2;
                moxing = 2;
                return;
            }
            if (moxing == 2)
            {
                //this.pictureBox1.Image = Image.FromFile("PanelText\\0-3.jpg");
                pictureBox1.Image = Properties.Resources._1_3;
                moxing = 3;
                return;
            }
            if (moxing == 3)
            {
                //this.pictureBox1.Image = Image.FromFile("C:\\Users\\Administrator\\Desktop\\0-1.jpg");
                pictureBox1.Image = Properties.Resources._1_1;
                moxing = 1;
                return;
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources._1_1;
            moxing = 1;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources._1_3;
            moxing = 3;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources._1_2;
            moxing = 2;
        }
        /*应急按钮*/
        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = Properties.Resources._2_1;
            if (dj >= -33)
            {
                dj -= 2;
                axProgressB1.SetUpdata(dj);
                axProgressB1.SetBlock(dj);
            }
        }

        private void label4_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = Properties.Resources._2_2;
        }

        private void label5_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = Properties.Resources._2_3; 
            if (dj < 33)
            {
                dj += 2;
                axProgressB1.SetUpdata(dj);
                axProgressB1.SetBlock(dj);
            }
        }

        private void label5_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = Properties.Resources._2_2;
        }

        private void button1_Click(object sender, EventArgs e)// 应急舵1
        {
            if (button1.BackColor == Color.Red)
            {
                button1.BackColor = Color.Yellow;
                label4.Enabled = false;
                label5.Enabled = false;
                return;
            }
            button1.BackColor = Color.Red;
            button2.BackColor = Color.Yellow;
            button3.BackColor = Color.Yellow;
            label4.Enabled = true;
            label5.Enabled = true;
            DeSt = 1;//表示应急舵
        }

        private void button3_Click(object sender, EventArgs e)//自动舵2
        {
            if (button3.BackColor == Color.Red)
            {
                button3.BackColor = Color.Yellow;
                //m_threadDataChange.Resume();
                State = false;   /*设置自动舵*/
                heading = heading1;//设置船首向
                return;
            }
            button3.BackColor = Color.Red;
            button1.BackColor = Color.Yellow;
            button2.BackColor = Color.Yellow;
            if(dj>0){
                axProgressB1.SetUpdata(35);
            }
            else
            {
                axProgressB1.SetUpdata(-35);
            }
            DeSt = 2;
            State = true;
            label4.Enabled = false;
            label5.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)//随动舵3
        {
            if (button2.BackColor == Color.Red)
            {
                button2.BackColor = Color.Yellow;

                return;
            }
            button2.BackColor = Color.Red;
            button1.BackColor = Color.Yellow;
            button3.BackColor = Color.Yellow;
            label4.Enabled = false;
            label5.Enabled = false;
            DeSt = 3;

        }
        /// <summary>
        /// 开始按钮设置（控制通信线程进行控制开关）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            // status（st）开始0 暂停 1 继续 2  
            if (this.button4.Text == "开始")
            {
                    //硬件接收车钟数据
                    
                    cz_threadDataChange = new Thread(cz_threadDataThread);
                    cz_threadDataChange.Start();
                    //硬件接收舵角数据
                    Thread t1 = new Thread(dj_AskMsg);
                    t1.Start();

                    dj_threadDataChange = new Thread(dj_threadDatathread);
                    dj_threadDataChange.Start();
                //向显示端发送数据
                InitSocket();
                m_threadDataChange = new Thread(DataChangeThread);
                m_threadDataChange.Start();
                MessageBox.Show("线程开启");
                this.button4.Text = "暂停";
                st = 3;
                return;
            }
            if (this.button4.Text == "暂停")
            {
                st = 1;
                m_threadDataChange.Suspend();
                this.button4.Text = "继续";
                MessageBox.Show("线程挂起");
                return;
            }
            if (this.button4.Text == "继续")
            {
                m_threadDataChange.Resume();
                this.button4.Text = "暂停";
                MessageBox.Show("线程继续");
                st = 2;
                return;
            }
        }
        void dj_AskMsg(object obj)
        {
            InitSocket2(djip, djport);
            clientEnd2 = new IPEndPoint(IPAddress.Parse("192.168.0.233"), 10007);
            while (true)
            {
                Byte[] askMsg = new byte[8];
                askMsg[0] = 0x01;   //地址码
                askMsg[1] = 0x03;   //功能码

                askMsg[2] = 0x00;   //起始地址
                askMsg[3] = 0x40;

                askMsg[4] = 0x00;   //数据长度
                askMsg[5] = 0x02;

                askMsg[6] = 0xC5;   //校验码低位
                askMsg[7] = 0xDF;   //校验码高位
                socket2.SendTo(askMsg, askMsg.Length,SocketFlags.None,clientEnd2);
                Thread.Sleep(sleeptime);
            }
        }
        void dj_threadDatathread(object obj)
        {
            //InitSocket2(djip, djport);
            MessageBox.Show("开启接收dj线程");
            while (true)
            {
                recvData2 = new byte[20];
                recvLen2 = socket2.ReceiveFrom(recvData2, ref clientEnd2);
                if (recvLen2 == 9)
                {
                    int num = (int)recvData2[3] * 256 + (int)recvData2[4];
                    //int mid = 520;     //506版
                    int mid = 484;      //理工版119
                    double result = 0;
                    if (num >= mid)
                        result = (num - mid) / 8.8 + 0.5;
                    else
                        result = (num - mid) / 8.8 - 0.5;
                    if (result > 0)
                    {
                        dj = (int)result;
                                        }
                    else
                    {
                        dj = (int)result;
                    }
                    sleeptime = 500;
                }
                axProgressB1.SetUpdata(dj);
            }

            Thread.Sleep(300);
        }
        string recvStr1;
        Socket socket1;
        EndPoint clientEnd1;
        IPEndPoint ipEnd1;
        string sendStr1;
        byte[] recvData1 = new byte[1024];
        byte[] sendData1 = new byte[1024];
        int recvLen1;
        void cz_threadDataThread(object obj)
        {
            InitSocket1(czip, czport);
            MessageBox.Show("开启接收cz线程");
            while (true)
            {
                recvData1 = new byte[1024];
                recvLen1 = socket1.ReceiveFrom(recvData1, ref clientEnd1);
                recvStr1 = Encoding.UTF8.GetString(recvData1, 0, recvLen1);
                if (recvStr1.CompareTo("$LDXDR,A,09,,TRUE,A,09,,SET*12\r\n") == 0)
                {
                    cz = 5;
                }
                else if (recvStr1.CompareTo("$LDXDR,A,07,,TRUE,A,07,,SET*12\r\n") == 0)
                {
                    cz = 4;
                }
                else if (recvStr1.CompareTo("$LDXDR,A,05,,TRUE,A,05,,SET*12\r\n") == 0)
                {
                    cz = 3;
                }
                else if (recvStr1.CompareTo("$LDXDR,A,03,,TRUE,A,03,,SET*12\r\n") == 0)
                {
                    cz = 2;
                }
                else if (recvStr1.CompareTo("$LDXDR,A,01,,TRUE,A,01,,SET*12\r\n") == 0)
                {
                    cz = 1;
                }
                else if (recvStr1.CompareTo("$LDXDR,A,00,,TRUE,A,00,,SET*12\r\n") == 0)
                {
                    cz = 0;
                }
                else if (recvStr1.CompareTo("$LDXDR,A,02,,TRUE,A,02,,SET*12\r\n") == 0)
                {
                    cz = -1;
                }
                else if (recvStr1.CompareTo("$LDXDR,A,04,,TRUE,A,04,,SET*12\r\n") == 0)
                {
                    cz = -2;
                }
                else if (recvStr1.CompareTo("$LDXDR,A,06,,TRUE,A,06,,SET*12\r\n") == 0)
                {
                    cz = -3;
                }
                else if (recvStr1.CompareTo("$LDXDR,A,08,,TRUE,A,08,,SET*12\r\n") == 0)
                {
                    cz = -4;
                }
                else if (recvStr1.CompareTo("$LDXDR,A,10,,TRUE,A,10,,SET*12\r\n") == 0)
                {
                    cz = -5;
                }
                axSpeed1.SetSpeed(cz);
                
            }
        }

        public void InitSocket1(String ipAddress, int ConnectPort)
        {
            ipEnd1 = new IPEndPoint(IPAddress.Parse(ipAddress), ConnectPort);
            socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket1.Bind(ipEnd1);
            //定义客户端    （将客户端信息进行存储）
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            clientEnd1 = (EndPoint)sender;
            Console.WriteLine("客户端" + clientEnd1.ToString() + "已连接");
        }

        string recvStr2;
        Socket socket2;
        EndPoint clientEnd2;
        IPEndPoint ipEnd2;
        string sendStr2;
        byte[] recvData2 = new byte[1024];
        byte[] sendData2 = new byte[1024];
        int recvLen2;
        public void InitSocket2(String ipAddress, int ConnectPort)
        {
            ipEnd2 = new IPEndPoint(IPAddress.Parse(ipAddress), ConnectPort);
            socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket2.Bind(ipEnd2);
            //定义客户端    （将客户端信息进行存储）
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            clientEnd2 = (EndPoint)sender;
            Console.WriteLine("客户端" + clientEnd2.ToString() + "已连接");
        }
        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            st = 4;
            Thread.Sleep(1000);
            System.Environment.Exit(0);
            Application.Exit();
            this.Close();
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            String textbox1 = this.textBox1.Text;
            heading = int.Parse(textbox1);
        }



    }
}
