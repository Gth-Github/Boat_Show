using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YimaEncNavigator
{
    [Serializable]
    public class CShipMotion
    {
        int shipID = -1;
        int n;
        double t, h;
        public double[] y, z, d, b, c;

        //车钟
        int chezhong;

        //原solution理的变量
        double[] a;
        double tt, te, KP, KI, KD, sethangxiang, x0, y0, bosai0, FAI0, u0, v0, r0, p0;
        double xe, ye, bosaie, FAIe, ue, ve, re, pe;


        //func里变量
        double m, L, D, B, midu, CB, ZG, H, T, Dp, np, tp, b1, b2, P, hR, epxlong, hs;
        double boxiangjiao, deltaomiga, h13;
        double Zb;

        int isdouble;
        int hengqing;
        double[] switchchezhong;

        public double deltae, delta, delta0;

        //初始位置
        long m_x, m_y;
        double m_heading;
        Random rand = null;

        public CShipMotion(int shipid,int model, int nn, long startX, long startY, double heading, double delta, double speed, int cz, double step)
        {
            shipID = shipid;

            n = nn;
            y = new double[n];     //动态分配内存
            d = new double[n];
            b = new double[n];
            c = new double[n];
            z = new double[n];

            a = new double[4];
            switchchezhong = new double[27];

            //初始化
            t = 0.0;          //时间点
            h = step;           //时间间隔,单位（s）
            y[0] = 0.0;               //初始纵位移
            y[1] = 0.0;               //初始横位移
            y[2] = heading / 180.0 * Common.PI;  //初始船首向
            y[3] = 0.0;               //初始横摇角度
            y[4] = speed;       //初始纵向速度（相对于船首向来说） 29knot
            y[5] = 0.0;               //初始横向速度（相对于船首向来说）
            y[6] = 0.0;
            y[7] = 0.0;

            chezhong = cz;

            //原solution理的变量
            //初始值
            xe = 0.0;
            x0 = 0.0;
            ye = 0.0;
            y0 = 0.0;
            bosaie = 0.0;
            bosai0 = 0.0;
   
            FAIe = 0.0;
            FAI0 = 0.0;
            ue = 0.0;
            u0 = 0.0;
            ve = 0.0;
            v0 = 0.0;
            re = 0.0;
            r0 = 0.0;
            pe = 0.0;
            p0 = 0.0;

            KP = 2.1;
            KD = 0.9;
            KI = 0.08;
            //sethangxiang=-1.8;
            sethangxiang = 0.0;

            //srand((unsigned)time(NULL));  
            //srand(GetTickCount());//毫秒级
            rand = new Random(unchecked((int)DateTime.Now.Ticks));

            te = 2.5;
            a[0] = h / 2.0;
            a[1] = a[0];
            a[2] = h;
            a[3] = h;
            tt = t;

            ////舵角
            //this->delta0 = delta0;		//初始舵角
            this.delta = delta;
            //this->deltae = deltae;

            //func
            //zch 加了.0，表示double
            m = 55000 * Math.Pow(10.0, 3.0);  //船质量
            L = 280.0;              //船长
            D = 9.45;            //型深
            B = 35.5;            //型宽
            Zb = 6.0;             // 浮心
            midu = 1000;        //水的密度
            CB = 0.597;         //方形系数
            ZG = 14.0;             //重心高度
            H = 2.9;              //初稳心高
            T = 0.058 * D;          //首尾吃水差
            Dp = 4.3;       //7.88; 螺旋桨直径   
            np = chezhong;         //主机转速，车钟()  5
            tp = 0.167;   //0.08  推力减额系数;         0.2~0.25
            b1 = 13.0;   //舵叶弦长
            b2 = 11.0;   //舵叶弦长
            P = 3.7;   //7.6 螺距比
            hR = 7.5;   //舵叶高
            epxlong = 0.9;//实验系数
            hs = 6.9; //螺旋桨浸深

            deltaomiga = 0.1; //浪频率间隔
            h13 = 0.001;  //浪高(0~1)

            //boxiangjiao=3.1415926/20;  //波向角 
            boxiangjiao = 0.001;

            isdouble = 1;       //是否是双螺旋桨 1为但螺旋桨 2为双螺旋桨
            hengqing = 1;       //倒退时是否横倾 1为横倾 0为不横倾
            switchchezhong[0] = 7.3;
            switchchezhong[1] = 0.13;
            switchchezhong[2] = 0.135;
            switchchezhong[3] = 1.0;
            switchchezhong[4] = 7.0;
            switchchezhong[5] = 0.12;
            switchchezhong[6] = 0.135;
            switchchezhong[7] = 1.0;
            switchchezhong[8] = 6.0;
            switchchezhong[9] = 0.1;
            switchchezhong[10] = 0.12;
            switchchezhong[11] = 1.0;
            switchchezhong[12] = 0.0000000000000000001;
            switchchezhong[13] = 0.0;
            switchchezhong[14] = 1.0;
            switchchezhong[15] = 1.2;
            switchchezhong[16] = 0.09;
            switchchezhong[17] = 0.9;
            switchchezhong[18] = 1.8;
            switchchezhong[19] = 0.13;
            switchchezhong[20] = 0.9;
            switchchezhong[21] = 2.4;
            switchchezhong[22] = 0.15;
            switchchezhong[23] = 0.9;
            switchchezhong[24] = 2.9;
            switchchezhong[25] = 0.16;
            switchchezhong[26] = 0.9;

            m_x = startX;
            m_y = startY;
            m_heading = heading;

            //////车钟调整
            //if(chezhong == 3)
            //{
            //	tp = 0.167;
            //	y[4] = 9.2;
            //}
            //else if (chezhong == 5)
            //{//0.2~0.25
            //	tp = 0.22;
            //	y[4] = 14.92;
            //}
            solution();
        }

        /// <summary>
        /// 执行定步长Runge-Kutta法
        /// </summary>
        public void solution()
        {
            func(tt, y, d, ref deltae, ref delta, ref delta0);
            for (int i = 0; i <= n - 1; i++)
            {
                b[i] = y[i];
                c[i] = y[i];
            }
            for (int j = 0; j <= 2; j++)
            {
                for (int i = 0; i <= n - 1; i++)
                {
                    y[i] = c[i] + a[j] * d[i];
                    b[i] = b[i] + a[j + 1] * d[i] / 3.0;
                }
                func(tt + a[j], y, d, ref deltae,ref delta,ref delta0);
            }


            for (int i = 0; i <= n - 1; i++)
            {
                y[i] = b[i] + h * d[i] / 6.0;
                /*z[i][kk]=y[i];*/
            }

            tt = tt + h;

            xe = (y[4] * Math.Cos(y[2]) - y[5] * Math.Cos(y[3]) * Math.Sin(y[2])) * h + x0;
            ye = (y[4] * Math.Sin(y[2]) + y[5] * Math.Cos(y[3]) * Math.Cos(y[2])) * h + y0;
            bosaie = (y[6] * Math.Cos(y[3])) * h + bosai0;
            FAIe = y[7] * h + FAI0;
            ue = (xe - x0) / h;
            ve = (ye - y0) / h;
            re = (bosaie - bosai0) / h;
            pe = (FAIe - FAI0) / h;

            //根据航线调整指令舵角
            /*
            //if(kk % 10==0)
            {
                deltae = deltae +
                    (KP+KI/2+KD)*(sethangxiang-bosai0[kk])+
                    (KI/2-KP-2*KD)*(sethangxiang-bosai0[kk-1])+
                    KD*(sethangxiang-bosai0[kk-2]);
            }*/


            //x0 y0  对应分别表示 纵向 横向的位移 bosai0 FAI0分别为艏摇 横摇的角度
            //u0 v0  对应分别表示 纵向 横向的速度 r0 p0 分别为艏摇 横摇的角速度

            z[0] = xe;          //纵向位移
            z[1] = ye;          //横向位移
            z[2] = bosaie;      //艏摇角度
            z[3] = FAIe;            //横摇角度
            z[4] = ue;          //纵向速度
            z[5] = ve;          //横向速度
            z[6] = re;          //艏摇角速度
            z[7] = pe;          //横摇角速度
            z[8] = delta;               //实时舵角


            x0 = xe;
            y0 = ye;
            bosai0 = bosaie;
            FAI0 = FAIe;
            u0 = ue;
            v0 = ve;
            r0 = re;
            p0 = pe;
        }

        public void changemoxing(int moxing)    //更换模型 1:5万吨 2:巡逻30米 3：巡逻40米 4：巡逻60米 5：海监1500
        {
            if (moxing == 1) //5万吨
            {
                m = 55000 * Math.Pow(10.0, 3.0);  //船质量
                L = 280;              //船长
                D = 9.45;            //型深
                B = 35.5;            //型宽
                midu = 1000;        //水的密度
                CB = 0.597;         //方形系数
                ZG = 14;             //重心高度
                H = 2.9;              //初稳心高
                T = 0.058 * D;          //首尾吃水差
                Dp = 4.3;       //7.88; 螺旋桨直径   
                b1 = 13;   //舵叶弦长
                b2 = 11;   //舵叶弦长
                P = 3.7;   //7.6 螺距比
                hR = 7.5;   //舵叶高
                hs = 6.9; //螺旋桨浸深
                Zb = 6;             // 浮心
                isdouble = 1;       //是否是双螺旋桨 1为但螺旋桨 2为双螺旋桨
                hengqing = 1;       //倒退时是否横倾 1为横倾 0为不横倾
                switchchezhong[0] = 7.3;
                switchchezhong[1] = 0.13;
                switchchezhong[2] = 0.135;
                switchchezhong[3] = 1;
                switchchezhong[4] = 7;
                switchchezhong[5] = 0.12;
                switchchezhong[6] = 0.135;
                switchchezhong[7] = 1;
                switchchezhong[8] = 6;
                switchchezhong[9] = 0.1;
                switchchezhong[10] = 0.12;
                switchchezhong[11] = 1;
                switchchezhong[12] = 0.0000000000000000000000000000000000001;
                switchchezhong[13] = 0;
                switchchezhong[14] = 1;
                switchchezhong[15] = 1.2;
                switchchezhong[16] = 0.09;
                switchchezhong[17] = 0.9;
                switchchezhong[18] = 1.8;
                switchchezhong[19] = 0.13;
                switchchezhong[20] = 0.9;
                switchchezhong[21] = 2.4;
                switchchezhong[22] = 0.15;
                switchchezhong[23] = 0.9;
                switchchezhong[24] = 2.9;
                switchchezhong[25] = 0.16;
                switchchezhong[26] = 0.9;
            }
            else if (moxing == 2) //巡逻30
            {
                m = 118.8 * Math.Pow(10.0, 3.0);  //船质量
                L = 33.2;              //船长
                D = 3.2;            //型深
                B = 6;            //型宽
                midu = 1000;        //水的密度
                CB = 0.444;         //方形系数
                ZG = 2.309;             //重心高度
                H = 0.6;              //初稳心高
                T = 0.008 * D;          //首尾吃水差
                Dp = 1.1;       //7.88; 螺旋桨直径 
                b1 = 0.9;   //舵叶弦长
                b2 = 0.75;   //舵叶弦长
                P = 1.1;   //7.6 螺距比
                hR = 1;   //舵叶高
                hs = 2.2; //螺旋桨浸深
                Zb = 0.9;  // 浮心
                isdouble = 2;       //是否是双螺旋桨 1为但螺旋桨 2为双螺旋桨
                hengqing = 0;       //倒退时是否横倾 1为横倾 0为不横倾
                switchchezhong[0] = 4;
                switchchezhong[1] = 0.03;
                switchchezhong[2] = 0.13;
                switchchezhong[3] = 1;
                switchchezhong[4] = 3.5;
                switchchezhong[5] = 0.03;
                switchchezhong[6] = 0.135;
                switchchezhong[7] = 1;
                switchchezhong[8] = 3;
                switchchezhong[9] = 0.03;
                switchchezhong[10] = 0.13;
                switchchezhong[11] = 1;
                switchchezhong[12] = 0.0000000000000000000000000000000000001;
                switchchezhong[13] = 0;
                switchchezhong[14] = 1;
                switchchezhong[15] = 4;
                switchchezhong[16] = 0.03;
                switchchezhong[17] = 0.9;
                switchchezhong[18] = 6;
                switchchezhong[19] = 0.04;
                switchchezhong[20] = 0.9;
                switchchezhong[21] = 8;
                switchchezhong[22] = 0.05;
                switchchezhong[23] = 0.9;
                switchchezhong[24] = 10;
                switchchezhong[25] = 0.055;
                switchchezhong[26] = 0.9;
            }
            else if (moxing == 3) //巡逻40
            {
                m = 347.27 * Math.Pow(10.0, 3.0);  //船质量
                L = 47.4;              //船长
                D = 4.7;            //型深
                B = 8;            //型宽
                midu = 1000;        //水的密度
                CB = 0.449;         //方形系数
                ZG = 3.16;             //重心高度
                H = 0.7;              //初稳心高
                T = 0.008 * D;          //首尾吃水差
                Dp = 1.56;       //7.88; 螺旋桨直径  
                b1 = 1.08;   //舵叶弦长
                b2 = 0.9;   //舵叶弦长
                P = 1.1;   //7.6 螺距比
                hR = 1.35;   //舵叶高
                hs = 3.3; //螺旋桨浸深
                Zb = 1.7;  // 浮心
                isdouble = 2;       //是否是双螺旋桨 1为但螺旋桨 2为双螺旋桨
                hengqing = 0;       //倒退时是否横倾 1为横倾 0为不横倾
                switchchezhong[0] = 4.3;
                switchchezhong[1] = 0.03;
                switchchezhong[2] = 0.135;
                switchchezhong[3] = 1;
                switchchezhong[4] = 4;
                switchchezhong[5] = 0.03;
                switchchezhong[6] = 0.135;
                switchchezhong[7] = 1;
                switchchezhong[8] = 3;
                switchchezhong[9] = 0.03;
                switchchezhong[10] = 0.13;
                switchchezhong[11] = 1;
                switchchezhong[12] = 0.0000000000000000000000000000000000001;
                switchchezhong[13] = 0;
                switchchezhong[14] = 1;
                switchchezhong[15] = 4;
                switchchezhong[16] = 0.03;
                switchchezhong[17] = 0.9;
                switchchezhong[18] = 6;
                switchchezhong[19] = 0.04;
                switchchezhong[20] = 0.9;
                switchchezhong[21] = 8;
                switchchezhong[22] = 0.05;
                switchchezhong[23] = 0.9;
                switchchezhong[24] = 10;
                switchchezhong[25] = 0.055;
                switchchezhong[26] = 0.9;
            }
            else if (moxing == 4) //巡逻60
            {
                m = 757.34 * Math.Pow(10.0, 3.0);  //船质量
                L = 64;              //船长
                D = 5;            //型深
                B = 10.2;            //型宽
                midu = 1000;        //水的密度
                CB = 0.462;         //方形系数
                ZG = 3.725;             //重心高度
                H = 0.8;              //初稳心高
                T = 0.008 * D;          //首尾吃水差
                Dp = 1.762;       //7.88; 螺旋桨直径  
                b1 = 1.8;   //舵叶弦长
                b2 = 1.4;   //舵叶弦长
                P = 1.1;   //7.6 螺距比
                hR = 2;   //舵叶高
                hs = 4; //螺旋桨浸深
                Zb = 2;  // 浮心
                isdouble = 2;       //是否是双螺旋桨 1为但螺旋桨 2为双螺旋桨
                hengqing = 0;       //倒退时是否横倾 1为横倾 0为不横倾
                switchchezhong[0] = 3.5;
                switchchezhong[1] = 0.03;
                switchchezhong[2] = 0.135;
                switchchezhong[3] = 1;
                switchchezhong[4] = 3.2;
                switchchezhong[5] = 0.03;
                switchchezhong[6] = 0.135;
                switchchezhong[7] = 1;
                switchchezhong[8] = 3;
                switchchezhong[9] = 0.03;
                switchchezhong[10] = 0.13;
                switchchezhong[11] = 1;
                switchchezhong[12] = 0.0000000000000000000000000000000000001;
                switchchezhong[13] = 0;
                switchchezhong[14] = 1;
                switchchezhong[15] = 4;
                switchchezhong[16] = 0.03;
                switchchezhong[17] = 0.9;
                switchchezhong[18] = 6;
                switchchezhong[19] = 0.04;
                switchchezhong[20] = 0.9;
                switchchezhong[21] = 8;
                switchchezhong[22] = 0.05;
                switchchezhong[23] = 0.9;
                switchchezhong[24] = 10;
                switchchezhong[25] = 0.055;
                switchchezhong[26] = 0.9;
            }
            else if (moxing == 5)//海监1500
            {
                m = 1976 * Math.Pow(10.0, 3.0);  //船质量
                L = 89;              //船长
                D = 6.3;            //型深
                B = 12.2;            //型宽
                midu = 1000;        //水的密度
                CB = 0.459;         //方形系数
                ZG = 4.814;             //重心高度
                H = 1;              //初稳心高
                T = 0.008 * D;          //首尾吃水差
                Dp = 2.7;       //7.88; 螺旋桨直径   
                b1 = 1.6;   //舵叶弦长
                b2 = 1.2;   //舵叶弦长
                P = 1.5;   //7.6 螺距比
                hR = 2.8;   //舵叶高
                hs = 5.2; //螺旋桨浸深
                Zb = 2.5;  // 浮心
                isdouble = 2;       //是否是双螺旋桨 1为但螺旋桨 2为双螺旋桨
                hengqing = 0;       //倒退时是否横倾 1为横倾 0为不横倾
                switchchezhong[0] = 3.5;
                switchchezhong[1] = 0.03;
                switchchezhong[2] = 0.135;
                switchchezhong[3] = 1;
                switchchezhong[4] = 3.2;
                switchchezhong[5] = 0.03;
                switchchezhong[6] = 0.135;
                switchchezhong[7] = 1;
                switchchezhong[8] = 3;
                switchchezhong[9] = 0.03;
                switchchezhong[10] = 0.13;
                switchchezhong[11] = 1;
                switchchezhong[12] = 0.0000000000000000000000000000000000001;
                switchchezhong[13] = 0;
                switchchezhong[14] = 1;
                switchchezhong[15] = 2;
                switchchezhong[16] = 0.03;
                switchchezhong[17] = 0.9;
                switchchezhong[18] = 3;
                switchchezhong[19] = 0.04;
                switchchezhong[20] = 0.9;
                switchchezhong[21] = 4;
                switchchezhong[22] = 0.05;
                switchchezhong[23] = 0.9;
                switchchezhong[24] = 5;
                switchchezhong[25] = 0.055;
                switchchezhong[26] = 0.9;
            }
            else if (moxing == 6)  //油船模型，双螺旋，无侧倾
            {
                m = 5300 * Math.Pow(10.0, 3.0);  //船质量
                L = 87.5;              //船长
                D = 5.6;            //型深
                B = 16.2;            //型宽
                midu = 1000;        //水的密度
                CB = 0.597;         //方形系数
                ZG = 3;             //重心高度
                H = 2.8;              //初稳心高
                T = 0.058 * D;          //首尾吃水差
                Dp = 2.1;       //7.88; 螺旋桨直径   


                b1 = 1.6;   //舵叶弦长
                b2 = 1.5;   //舵叶弦长
                P = 3.7;   //7.6 螺距比
                hR = 2.8;   //舵叶高

                hs = 1.5; //螺旋桨浸深
                Zb = 3.8;  // 浮心
                isdouble = 2;       //是否是双螺旋桨 1为但螺旋桨 2为双螺旋桨
                hengqing = 0;       //倒退时是否横倾 1为横倾 0为不横倾

                switchchezhong[0] = 7.3;
                switchchezhong[1] = 0.13;
                switchchezhong[2] = 0.135;
                switchchezhong[3] = 1;
                switchchezhong[4] = 7;
                switchchezhong[5] = 0.12;
                switchchezhong[6] = 0.135;
                switchchezhong[7] = 1;
                switchchezhong[8] = 6;
                switchchezhong[9] = 0.1;
                switchchezhong[10] = 0.12;
                switchchezhong[11] = 1;
                switchchezhong[12] = 0.0000000000000000000000000000000000001;
                switchchezhong[13] = 0;
                switchchezhong[14] = 1;
                switchchezhong[15] = 1.5;
                switchchezhong[16] = 0.07;
                switchchezhong[17] = 0.9;
                switchchezhong[18] = 2;
                switchchezhong[19] = 0.12;
                switchchezhong[20] = 0.9;
                switchchezhong[21] = 3;
                switchchezhong[22] = 0.14;
                switchchezhong[23] = 0.9;
                switchchezhong[24] = 3.5;
                switchchezhong[25] = 0.15;
                switchchezhong[26] = 0.9;
            }
            else if (moxing == 7)   //散货船模型，双桨无侧倾
            {

                m = 7300 * Math.Pow(10.0, 3.0);  //船质量
                L = 107.5;              //船长
                D = 5.6;            //型深
                B = 19.2;            //型宽
                midu = 1000;        //水的密度
                CB = 0.597;         //方形系数
                ZG = 3.5;             //重心高度
                H = 3.4;              //初稳心高
                T = 0.058 * D;          //首尾吃水差
                Dp = 2.2;       //7.88; 螺旋桨直径   

                b1 = 1.1;   //舵叶弦长
                b2 = 1.0;   //舵叶弦长
                P = 3.7;   //7.6 螺距比
                hR = 2.2;   //舵叶高

                hs = 3; //螺旋桨浸深
                Zb = 3.8;  // 浮心
                isdouble = 2;       //是否是双螺旋桨 1为但螺旋桨 2为双螺旋桨
                hengqing = 0;       //倒退时是否横倾 1为横倾 0为不横倾


                switchchezhong[0] = 7.3;
                switchchezhong[1] = 0.13;
                switchchezhong[2] = 0.135;
                switchchezhong[3] = 1;
                switchchezhong[4] = 7;
                switchchezhong[5] = 0.12;
                switchchezhong[6] = 0.135;
                switchchezhong[7] = 1;
                switchchezhong[8] = 6;
                switchchezhong[9] = 0.1;
                switchchezhong[10] = 0.12;
                switchchezhong[11] = 1;
                switchchezhong[12] = 0.0000000000000000000000000000000000001;
                switchchezhong[13] = 0;
                switchchezhong[14] = 1;
                switchchezhong[15] = 1.5;
                switchchezhong[16] = 0.07;
                switchchezhong[17] = 0.9;
                switchchezhong[18] = 2;
                switchchezhong[19] = 0.12;
                switchchezhong[20] = 0.9;
                switchchezhong[21] = 3;
                switchchezhong[22] = 0.14;
                switchchezhong[23] = 0.9;
                switchchezhong[24] = 3.5;
                switchchezhong[25] = 0.15;
                switchchezhong[26] = 0.9;
            }
        }

        public void ResetSolutionMem()
        {
            t = 0.0;//time
            //原solution理的变量
            //初始值
            xe = x0 = 0.0;
            ye = y0 = 0.0;
            bosaie = bosai0 = 0.0;
            FAIe = FAI0 = 0.0;
            ue = u0 = 0.0;
            ve = v0 = 0.0;
            re = r0 = 0.0;
            pe = p0 = 0.0;
        }

        //获得T时刻后的船舶状态
        public bool GetOffsetStatus(ref double xOffset, ref double yOffset, ref double headOffset, ref double delta)
        {
            solution();
            xOffset = z[1] / 1852.0;
            yOffset = z[0] / 1852.0;
            headOffset = z[2] / Common.PI * 180.0;
            return true;
        }

        //获得初始位置
        public void GetInitPosition(ref long startX, ref long startY)
        {
            startX = m_x;
            startY = m_y;
        }

        //设定初始位置
        public void SetInitPosition(long startX, long startY)
        {
            m_x = startX;
            m_y = startY;
        }

        //获得初始船首向
        public void GetInitHeading(ref double heading)
        {
            heading = m_heading;
        }

        //设置初始船首向
        public void SetInitHeading(double heading)
        {
            m_heading = heading;
            y[2] = heading / 180.0 * Common.PI;
        }

        //设置车钟
        public void SetChezhong(int cz)
        {
            if (cz > 0)
            {
                chezhong = cz - 1;
            }
            else if (cz < 0)
            {
                chezhong = cz + 1;
                if (cz == -5)
                {
                    chezhong = -3;
                }
            }
            else
                chezhong = cz;
        }

        public void SetDelta(double Del)
        {
             delta = Del;
             delta0 = Del;
             deltae = Del;
        }

        //获取数组Z
        public double[] GetVaribalZ()
        {
            return z;
        }

        /// <summary>
        /// //计算船舶四自由运动的主函数
        /// </summary>
        /// <param name="t"></param>
        /// <param name="y"></param>
        /// <param name="d"></param>
        /// <param name="deltae"></param>
        /// <param name="delta"></param>
        /// <param name="delta0"></param>
        public void func(double t, double[] y, double[] d,ref double deltae, ref double delta, ref double delta0)
        {
            int i, tw;

            //double  nw;
            double V, RP, Beita, IZ, m11, m22, m66, Xu, XVR, XH, jxlv;
            double K, TP, Yv, Yr, Yvv, Yvr, Yrr, YH, Lv, Nv, Nr, Nvvr, Nvrr, Nrr, NvFAI, NrFAI, NFAI, NH;
            double Np, GZFAI, ZH, KH, wp0, Beitap, wp = 0.0, Jp, Kt, XP, S, lamuda, fa, sita, gs, uR, vR, UR;//S0
            double alfaR, tR, aH, xR, xH, zR, FN, XR, YR, NR, KR, c1, te;     //delta01,
            double kafang, Ab, Bb, Cbb, Cbc, Db;
            double[] omiga, E;
            double ran_numf; 
            double[] zsx, Skexi;
            double[] Xw, Yw, Kw, Nw, Xd, Yd, Nd, bochangbi, Cxwd, Cywd, Cnwd;

            tw = 12;

            omiga = new double[tw];
            E = new double[tw];
            zsx = new double[tw];
            Skexi = new double[tw];
            Xw = new double[tw];
            Yw = new double[tw];
            Kw = new double[tw];
            Nw = new double[tw];
            Xd = new double[tw];
            Yd = new double[tw];
            Nd = new double[tw];
            bochangbi = new double[tw];
            Cxwd = new double[tw];
            Cywd = new double[tw];
            Cnwd = new double[tw];


            omiga[0] = 0.01;
            E[0] = 0.0;
            zsx[0] = 0.0;
            Skexi[0] = 0.0;

            Xw[0] = 0.0;
            Yw[0] = 0.0;
            Nw[0] = 0.0;
            Kw[0] = 0.0;
            ///////////////////////////////////////////////////////////


            switch (chezhong)
            {
                case -3:   //-5.5769 n
                    np = switchchezhong[0];  //主机转速 
                    if (y[4] > 0.0)
                        tp = switchchezhong[1];
                    else
                        tp = switchchezhong[2];   //0.08  推力减额系数;
                    epxlong = switchchezhong[3];//实验系数 
                    break;

                case -2:  //-5.128 n
                    np = switchchezhong[4];  //主机转速
                    if (y[4] > 0.0)
                        tp = switchchezhong[5];
                    else
                        tp = switchchezhong[6];   //0.08  推力减额系数;
                    epxlong = switchchezhong[7];//实验系数 
                    break;
                case -1: // -3.8 n
                    np = switchchezhong[8];  //主机转速
                    if (y[4] > 0.0)
                        tp = switchchezhong[9];
                    else
                        tp = switchchezhong[10];  //0.08  推力减额系数;
                    epxlong = switchchezhong[11];//实验系数
                    break;
                case 0:
                    np = switchchezhong[12];  //主机转速
                    tp = switchchezhong[13];   //0.08  推力减额系数;
                    epxlong = switchchezhong[14];//实验系数
                    break;
                case 1:  // 8.63 n
                    np = switchchezhong[15];  //主机转速
                    tp = switchchezhong[16];   //0.08  推力减额系数;
                    epxlong = switchchezhong[17];//实验系数
                    break;
                case 2: // 13.77 n
                    np = switchchezhong[18];  //主机转速
                    tp = switchchezhong[19];   //0.08  推力减额系数;
                    epxlong = switchchezhong[20];//实验系数
                    break;
                case 3:  // 18.404n
                    np = switchchezhong[21];  //主机转速
                    tp = switchchezhong[22];   //0.08  推力减额系数;
                    epxlong = switchchezhong[23];//实验系数
                    break;
                case 4: // 21.9178n
                    np = switchchezhong[24];  //主机转速
                    tp = switchchezhong[25];   //0.08  推力减额系数;
                    epxlong = switchchezhong[26];//实验系数
                    break;
            }


            if (chezhong > 0 && y[4] < 0.0000001 && y[4] > -0.0000001)
            {
                y[4] = 0.0000001;
            }

            ///////////////////////////////////////////////////////////
            //zch 加了.0，表示double
            V = Math.Sqrt(Math.Pow(y[4], 2.0) + Math.Pow(y[5], 2.0));
            RP = y[6] * L / V;
            te = 2.5;
            Beita = Math.Atan(-y[5] / y[4]);

            //各纬度惯性矩及附加质量的计算
            IZ = m * (Math.Pow(0.245 * L, 2.0));//	zch 加了.0，表示double
            m11 = m * (1.0 / 100.0) * (0.398 + 11.98 * CB * (1.0 + 3.73 * (D / B)) - (2.89 * CB * (L / B)) * (1.0 + 1.13 * (D / B)) + 0.175 * CB * Math.Pow((L / B), 2.0) * (1.0 + 0.541 * (D / B)) - 1.107 * (L / B) * (D / B));
            m22 = m * (0.882 - 0.54 * CB * (1.0 - 1.6 * (D / B)) - 0.156 * (1.0 - 0.673 * CB) * (L / B) + 0.826 * (D / B) * (L / B) * (1.0 - 0.678 * (D / B)) - 0.638 * (L / B) * (D / B) * (1.0 - 0.669 * (D / B)));
            m66 = m * Math.Pow((L * 0.01 * (33.0 - 76.85 * CB * (1.0 - 0.784 * CB) + 3.43 * (L / B) * (1.0 - 0.63 * CB))), 2.0);



            //纵向水动力的计算
            if (chezhong == -1 || chezhong == -2 || chezhong == -3)
                y[4] = Math.Abs(y[4]);
            Xu = 59.12 * Math.Pow(y[4], 4.0) - 462.8 * Math.Pow(y[4], 3.0) + 8775 * Math.Pow(y[4], 2.0) + 28940 * y[4] + 67640.0; //Xu=-1268*pow(y[4],4)+67311*pow(y[4],3)-pow(10,6)*pow(y[4],2)+pow(10,7)*y[4]-3*pow(10,7);
            XVR = (1.6757 * CB - 0.5054 - 1.0) * m22;
            XH = XVR * y[5] * y[6] - Xu;

            if (chezhong == 0)
            { XH = -(4735 * Math.Pow(y[4], 2.0) + 4589 * y[4]); }

            //横向水动力的计算
            K = 2.0 * D / L;
            TP = T / D;
            Yv = -0.5 * midu * L * D * V * (3.14159 * K / 2.0 + 1.4 * CB * (B / L)) * (1.0 + 0.67 * TP);
            Yr = 0.5 * midu * Math.Pow(L, 2.0) * D * V * (3.14159 * K / 4.0) * (1.0 + 0.8 * TP);
            Yvv = 0.5 * midu * L * D * (0.048265 - 6.293 * (1.0 - CB) * (D / B));
            Yvr = 0.5 * midu * Math.Pow(L, 2.0) * D * (-0.3791 + 1.28 * (1.0 - CB) * (D / B));
            Yrr = 0.5 * midu * Math.Pow(L, 3.0) * D * (0.0045 - 0.445 * (1.0 - CB) * (D / B));
            YH = Yv * y[5] + Yr * y[6] + Yvv * y[5] * Math.Abs(y[5]) + Yvr * y[6] * Math.Abs(y[5]) + Yrr * y[6] * Math.Abs(y[6]);



            //首摇力矩的计算
            Lv = K / (0.5 * 3.1415926 * K + 1.4 * CB * (B / L));
            Nv = -0.5 * midu * Math.Pow(L, 2.0) * D * V * K * (1 - 0.27 * TP / Lv);
            Nr = -0.5 * midu * Math.Pow(L, 3.0) * D * V * (0.54 * K - Math.Pow(K, 2.0)) * (1 + 0.3 * TP);
            Nvvr = 0.5 * midu * Math.Pow(L, 3.0) * D * (-6.0856 + 137.4735 * CB * (B / L) - 1029.514 * Math.Pow((CB * (B / L)), 2.0) + 2480.6082 * Math.Pow((CB * (B / L)), 3.0));
            Nvrr = 0.5 * midu * Math.Pow(L, 4.0) * D * (-0.0635 + 0.044145 * CB * (D / B));
            Nrr = 0.5 * midu * Math.Pow(L, 4.0) * D * (-0.0805 + 8.6092 * Math.Pow((CB * (B / L)), 2.0) - 36.9816 * Math.Pow((CB * (B / L)), 3.0));
            NvFAI = 0.5 * midu * Math.Pow(L, 2.0) * D * V * (-1.72 * K);
            NrFAI = 0.5 * midu * Math.Pow(L, 3.0) * D * V * (2.6 * (0.54 * K - Math.Pow(K, 2.0)));
            NFAI = 0.5 * midu * Math.Pow(L, 2.0) * D * Math.Pow(V, 2.0) * (-0.008);
            NH = Nv * y[5] + Nr * y[6] + Nvvr * Math.Pow(y[5], 2.0) * y[6] + Nrr * y[6] * Math.Abs(y[6]) + Nvrr * y[5] * Math.Pow(y[6], 2.0) + NFAI * y[3] + NvFAI * y[5] * Math.Abs(y[3]) + NrFAI * y[6] * Math.Abs(y[3]);


            //横倾力矩的计算

            Np = 0.12 * Math.Sqrt((m * (Math.Pow(B, 2) + 4.0 * Math.Pow(ZG, 2.0)) / (12.0)) * H * m * 9.8);
            GZFAI = H * Math.Sin(y[3]);
            ZH = ZG - (4.0 - B / D + 0.02 * Math.Pow((B / D - 5.35), 3.0)) * D;
            KH = -Np * y[7] - m * 9.8 * GZFAI - YH * ZH;


            //螺旋桨纵向推进力计算
            if (chezhong == 0)
                XP = 0.0;

            else if (chezhong == -1 || chezhong == -2 || chezhong == -3)
            {
                c1 = 0.935;
                wp0 = 0.5 * CB - 0.05;
                Beitap = Beita - 0.5 * RP;
                wp = wp0 * Math.Exp(-4.0 * Math.Pow(Beitap, 2.0));
                Jp = y[4] * (1.0 - wp) / (Dp * np);
                Kt = 0.25035 * Math.Pow(Jp, 3.0) - 0.58638 * Math.Pow(Jp, 2.0) - 0.067363 * Jp + 0.42379;       //Kt=0.5292*Math.Pow(Jp,3)-0.9795*Math.Pow(Jp,2)+0.0762*Jp+0.4162;
                jxlv = -0.8611 * Math.Pow(Jp, 2.0) + 1.4966 * Jp - 0.0161;
                XP = -4.0 * isdouble * (1.0 - tp) * midu * Math.Pow(np, 2.0) * Math.Pow(Dp, 4.0) * Kt;//*jxlv
            }
            else
            {
                c1 = 0.935;
                wp0 = 0.5 * CB - 0.05;
                Beitap = Beita - 0.5 * RP;
                wp = wp0 * Math.Exp(-4.0 * Math.Pow(Beitap, 2.0));
                Jp = y[4] * (1 - wp) / (Dp * np);
                Kt = 0.25035 * Math.Pow(Jp, 3.0) - 0.58638 * Math.Pow(Jp, 2.0) - 0.067363 * Jp + 0.42379;       //Kt=0.5292*Math.Pow(Jp,3)-0.9795*Math.Pow(Jp,2)+0.0762*Jp+0.4162;
                jxlv = -0.8611 * Math.Pow(Jp, 2.0) + 1.4966 * Jp - 0.0161;
                XP = 4.0 * isdouble * (1.0 - tp) * midu * Math.Pow(np, 2.0) * Math.Pow(Dp, 4) * Kt;//*jxlv
            }

            //倒车时螺旋桨产生的横向力
            if (chezhong == -1 || chezhong == -2 || chezhong == -3)
                YH = YH - 0.2 * XP * (double)hengqing;

            //倒车时螺旋桨产生的横向力矩
            if (chezhong == -1 || chezhong == -2 || chezhong == -3)
                NH = NH - 0.1 * XP * L * (double)hengqing;

            //舵力的计算 
            if (chezhong == 0 || chezhong == -1 || chezhong == -2 || chezhong == -3)
            {
                tR = 0.0;
                aH = 0.0;
                xR = 0.0;
                xH = 0.0;
                zR = 0.0;
                FN = 0.0;
            }
            else
            {
                S = 1.0 - y[4] * (1.0 - wp) / (np * P);
                delta = deltae - (deltae - delta0) * Math.Exp(-t / te);
                //delta0=delta;
                lamuda = 2.0 * hR / (b1 + b2);
                fa = 6.13 * lamuda / (2.25 + lamuda);
                sita = Dp / hR;
                gs = sita * (0.6 / epxlong) * S * (2.0 - (2.0 - (0.6 / epxlong)) * S) / Math.Pow((1.0 - S), 2.0);

                if (delta > 0.0)
                    c1 = 1.065;
                else
                    c1 = 0.935;


                uR = y[4] * (1.0 - wp) * Math.Sqrt(1.0 + c1 * gs);
                vR = (1.163314 - 1.982836 * CB + 1.390152 * Math.Pow(CB, 2.0)) * (y[5] - L * y[6]);
                UR = Math.Sqrt(Math.Pow(uR, 2.0) + Math.Pow(vR, 2.0));
                //S0=1-y[4]*(1-wp0)/(np*P);
                //delta01=-(2*S0+0.6);
                alfaR = delta - Math.Atan(vR / uR);//-delta01

                tR = 1.0 - (0.7382 - 0.0539 * CB + 0.1755 * Math.Pow(CB, 2.0));
                aH = 0.6784 - 1.3374 * CB + 1.8891 * Math.Pow(CB, 2.0);
                xR = -0.5 * L;
                xH = -(0.4 + 0.1 * CB) * L;
                zR = ZG - D + hs;

                FN = isdouble * 0.5 * midu * 0.5 * (b1 + b2) * hR * fa * Math.Pow(UR, 2.0) * Math.Sin(alfaR);
            }

            //	if (chezhong==-2)
            //	FN=0;

            XR = -(1.0 - tR) * FN * Math.Sin(delta);
            YR = (1.0 + aH) * FN * Math.Cos(delta);
            NR = (xR + aH * xH) * FN * Math.Cos(delta);
            KR = (1.0 + aH) * zR * FN * Math.Cos(delta);


            //波浪主扰动力

            kafang = y[2] - boxiangjiao;
            Ab = L * Math.Cos(kafang) / (2.0 * 9.8);
            Bb = B * Math.Sin(kafang) / (2.0 * 9.8);
            Cbb = 4.0 * midu * Math.Pow(9.8, 3.0) / Math.Cos(kafang);
            Cbc = 4.0 * midu * Math.Pow(9.8, 3.0) / Math.Sin(kafang);
            Db = V * Math.Cos(kafang) / 9.8;

            //srand((unsigned)time(NULL));    
            double sumX, sumY, sumK, sumN, sumXd, sumYd, sumNd;
            sumX = 0.0;
            sumY = 0.0;
            sumK = 0.0;
            sumN = 0.0;
            sumXd = 0.0;
            sumYd = 0.0;
            sumNd = 0.0;

            for (i = 1; i < tw; i++)
            {
                ran_numf = rand.Next() / (double)(Common.RAND_MAX);

                //cout<<ran_numf<<endl;
                omiga[i] = deltaomiga * (double)i;
                //cout<<omiga[i]<<endl;
                Skexi[i] = (8.1 * Math.Pow(10.0, (-3.0)) * Math.Pow(9.8, 2.0) / Math.Pow(omiga[i], 5.0)) * Math.Exp(-3.11 / (h13 * h13 * Math.Pow(omiga[i], 4.0)));
                //cout<<Skexi[i]<<endl;
                E[i] = Math.Sqrt(2.0 * Skexi[i] * deltaomiga);
                //cout<<E[i]<<endl;
                zsx[i] = 1.0 - Math.Exp(-Math.Pow(omiga[i], 2.0) * D / 9.8);
                //cout<<zsx[i]<<endl;
                Xw[i] = -(Cbc / Math.Pow(omiga[i], 4.0)) * E[i] * zsx[i] * Math.Sin(Math.Pow(omiga[i], 2.0) * Ab) * Math.Sin(Math.Pow(omiga[i], 2.0) * Bb) * Math.Sin((omiga[i] - Math.Pow(omiga[i], 2.0) * Db) * t - 2.0 * 3.14159 * ran_numf);
                sumX += Xw[i];
                //cout<<Xw[i]<<endl;
                //cout<<sumX<<endl;
                Yw[i] = +(Cbb / Math.Pow(omiga[i], 4.0)) * E[i] * zsx[i] * Math.Sin(Math.Pow(omiga[i], 2.0) * Ab) * Math.Sin(Math.Pow(omiga[i], 2.0) * Bb) * Math.Sin((omiga[i] - Math.Pow(omiga[i], 2.0) * Db) * t - 2.0 * 3.14159 * ran_numf);
                sumY += Yw[i];
                Kw[i] = -(Cbb / Math.Pow(omiga[i], 4.0)) * E[i] * zsx[i] * Math.Sin(Math.Pow(omiga[i], 2.0) * Ab) * Math.Sin(Math.Pow(omiga[i], 2.0) * Bb) * Math.Sin((omiga[i] - Math.Pow(omiga[i], 2.0) * Db) * t - 2.0 * 3.14159 * ran_numf) * Zb +
                    (Cbb / (2.0 * 9.8 * Math.Pow(omiga[i], 2.0))) * E[i] * zsx[i] * Math.Sin(Math.Pow(omiga[i], 2.0) * Ab) * Math.Sin((omiga[i] - Math.Pow(omiga[i], 2.0) * Db) * t - 2.0 * 3.14159 * ran_numf) *
                    ((2.0 * Math.Pow(9.8, 2.0) * Math.Sin(Math.Pow(omiga[i], 2.0) * Bb)) / (Math.Pow(omiga[i], 4.0) * Math.Pow(Math.Sin(kafang), 2.0)) - (B * 9.8 * Math.Cos(Math.Pow(omiga[i], 2) * Bb)) / (Math.Pow(omiga[i], 2.0) * Math.Sin(kafang)));
                sumK += Kw[i];
                Nw[i] = -((Math.Sin(kafang) * Cbc) / (2.0 * 9.8 * Math.Pow(omiga[i], 2.0))) * E[i] * zsx[i] * Math.Sin(Math.Pow(omiga[i], 2.0) * Bb) * Math.Cos((omiga[i] - Math.Pow(omiga[i], 2.0) * Db) * t - 2.0 * 3.14159 * ran_numf) *
                    ((2.0 * Math.Pow(9.8, 2.0) * Math.Sin(Math.Pow(omiga[i], 2.0) * Ab)) / (Math.Pow(omiga[i], 4.0) * Math.Pow(Math.Cos(kafang), 2.0)) - (L * 9.8 * Math.Cos(Math.Pow(omiga[i], 2.0) * Bb)) / (Math.Pow(omiga[i], 2.0) * Math.Cos(kafang)));
                sumN += Nw[i];


                bochangbi[i] = (2.0 * 3.1415926 * 9.8) / (Math.Pow(omiga[i], 2.0) * L);
                Cxwd[i] = 0.05 - 0.2 * bochangbi[i] + 0.75 * Math.Pow(bochangbi[i], 2.0) - 0.51 * Math.Pow(bochangbi[i], 3.0);
                Cywd[i] = 0.46 + 6.83 * bochangbi[i] - 15.65 * Math.Pow(bochangbi[i], 2.0) + 8.44 * Math.Pow(bochangbi[i], 3.0);
                Cnwd[i] = -0.11 + 0.68 * bochangbi[i] - 0.79 * Math.Pow(bochangbi[i], 2.0) + 0.21 * Math.Pow(bochangbi[i], 3.0);

                Xd[i] = midu * 9.8 * L * Math.Cos(kafang) * Cxwd[i] * Skexi[i] * deltaomiga;
                sumXd += Xd[i];
                Yd[i] = midu * 9.8 * L * Math.Sin(kafang) * Cywd[i] * Skexi[i] * deltaomiga;
                sumYd += Yd[i];
                Nd[i] = midu * 9.8 * Math.Pow(L, 2.0) * Math.Sin(kafang) * Cnwd[i] * Skexi[i] * deltaomiga;
                sumNd += Nd[i];

            }

            d[0] = y[4];
            d[1] = y[5];
            d[2] = y[6];
            d[3] = y[7];
            d[4] = (XH + XP + XR + sumX + sumXd + (m + m22) * y[5] * y[6]) / (m + m11);
            d[5] = (YH + YR + sumY + sumYd - (m + m11) * y[4] * y[6]) / (m + m22);
            d[6] = (NH + NR + sumN + sumNd) / (IZ + m66);
            d[7] = (KH + KR + sumK) / (m * (Math.Pow(B, 2.0) + 4.0 * Math.Pow(ZG, 2.0)) / 12.0);
        }
    }
}
