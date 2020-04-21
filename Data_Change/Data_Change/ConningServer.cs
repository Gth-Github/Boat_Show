using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data_Change
{
    public class ConningServer
    {
        string recvStr1;
        Socket socket1;
        EndPoint clientEnd1;
        IPEndPoint ipEnd1;
        string sendStr1;
        byte[] recvData1 = new byte[1024];
        byte[] sendData1 = new byte[1024];
        int recvLen1;
        float m = 0;
        //初始化
        public void InitSocket(String ipAddress, int ConnectPort)
        {
            ipEnd1 = new IPEndPoint(IPAddress.Parse(ipAddress), ConnectPort);
            socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket1.Bind(ipEnd1);
            //定义客户端    （将客户端信息进行存储）
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            clientEnd1 = (EndPoint)sender;
            Console.WriteLine("客户端"+clientEnd1.ToString()+"已连接");
            //开启一个线程连接  （根据不同的客户端创建不同的接收线程）
            if (ConnectPort == 8007)
            {
                Thread connectThread1 = new Thread(CzSocketReceive);
                connectThread1.Start();
            }
            else if (ConnectPort == 7003)
            {
                //connectThread2 = new Thread(new ThreadStart(DjSocketReceive));
                Thread connectThread2 = new Thread(DjSocketReceive);
                connectThread2.Start();
            }
        }
        void SocketSend(string sendStr)
        {
            sendData1 = new byte[1024];
            sendData1 = Encoding.UTF8.GetBytes(sendStr);
            socket1.SendTo(sendData1, sendData1.Length, SocketFlags.None, clientEnd1);
        }
        //车钟服务器接收
        void CzSocketReceive()
        {
            while (true)
            {
                recvData1 = new byte[1024];
                recvLen1 = socket1.ReceiveFrom(recvData1, ref clientEnd1);
                recvStr1 = Encoding.UTF8.GetString(recvData1, 0, recvLen1);
                //Debug.Log("收到得信息 " + recvStr);
            }
        }
        //舵角服务器接收
        void DjSocketReceive()
        {
            while (true)
            {
                recvData1 = new byte[1024];
                recvLen1 = socket1.ReceiveFrom(recvData1, ref clientEnd1);
                recvStr1 = Encoding.UTF8.GetString(recvData1, 0, recvLen1);
                //Debug.Log("收到得信息 " + recvStr);
            }
        }
        public string retrundata()
        {
            return recvStr1;
        }
    }
}
