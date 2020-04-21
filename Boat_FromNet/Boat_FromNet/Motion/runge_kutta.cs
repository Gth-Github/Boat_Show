using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Boat_FromNet.Motion
{
    //备注：sudu (sd)表示cz
    // D_Ling、nOldDuoling、nNewDuoling （duoling）表示舵角 dj 
    public class runge_kutta
    {
        const int LOOP_TIMES = 1;//solution内循环次数
        const int PARAM_NUM = 9;//参数个数
        private int n, k, duoling;
        private bool m_bFirstTime;
        public double deltae;//delta, delta0;
        public int langji, sd;
        public double t, drift, h;
        double[] y, d, b, c;
        public double[] z = new double[PARAM_NUM];
        public bool Load_state;
        
        private double dj;
        private int p;

        static int times = 0;
        static double x0, xe, y0, ye, bosai0, bosaie, bosai2, FAI0, FAIe, u0, ue, v0, ve, r0, p0, pe;
        //代替上面
        //static double[] x0= new double[2]; 
        //static double[] y0= new double[2]; 
        //static double[] bosai0= new double[3]; 
        //static double[] FAI0= new double[2]; 
        //static double[] u0= new double[2]; 
        //static double[] v0= new double[2];
        //static double[] p0= new double[2];
        //static double r0;
        

        public runge_kutta(double deltae, int langji)
        {
            //参考int[] result = new int[6];
            k = LOOP_TIMES;
            n = PARAM_NUM;
            //分配空间
            y = new double[n];
            d = new double[n];
            b = new double[n];
            c = new double[n];
            //memset(y, 0, sizeof(y));
            //memset(d, 0, sizeof(d));
            //memset(b, 0, sizeof(b));
            //memset(c, 0, sizeof(c));
            //memset(z, 0, sizeof(z));
            m_bFirstTime = true;

            this.deltae = deltae;
            this.langji = langji;

            c[4] = 9.2;
            t = 0.0;
            drift = 0;
        }
        //由文件读入数据成员t、h，以及n个未知函数在起始点t处的函数值
        public void input(int duoling, int langji, int sudu)
        {
            this.langji = langji;
            this.duoling = duoling;
            //this.t = 0;
            this.sd = sudu;
            switch (this.sd)
            {
                case -5:
                   // y[4] = 14.3;//12.2
                    //break;
                case -4:
                    //y[4] = -14.3;//12.2
                    //break;
                case -3:
                    //y[4] = 14.3;//12.2
                    //break;
                case -2:
                    y[4] = -14.3;//12.2
                   break;
                case -1:
                    y[4] = -9.2;//8.5
                    break;
                case 0:
                    y[4] = 0.00000001;
                    break;
                case 1:
                    y[4] = 9.2;//8.5
                    break;
                case 2:
                    //y[4] = 14.3;//12.2
                    //break;
                case 3:
                    //y[4] = 14.3;//12.2
                    //break;
                case 4:
                    //y[4] = 14.3;//12.2
                    //break;
                case 5:
                    y[4] = 14.3;//12.2
                    break;
            }
        }

        public void solution(ref double deltae,ref double delta,ref double delta0, double SetHangXiang)
        {
            //参考int[] result = new int[6];
            int i, j, kk;
            double te;
            double[] a = new double[4];

            double KP, KI, KD, sethangxiang;

            te = 2.5;
            a[0] = h/2.0; a[1] = a[0];
            a[2] = h; a[3] = h;

            KP = 2.1;
            KD = 0.9;
            KI = 0.01;
            sethangxiang = SetHangXiang;
            for (kk = 0; kk < k; kk++)
            {
                //func(tt, y, d, ref deltae, ref delta, ref delta0);
                func(t, y, d, deltae,ref delta,ref delta0);
                for (i = 0; i < n - 1; i++)
                {
                    b[i] = y[i]; c[i] = y[i];
                }
                for (j = 0; j <= 2; j++)
                {
                    for (i = 0; i <= n - 1; i++)
                    {
                        y[i] = c[i] + a[j] * d[i];
                        b[i] = b[i] + a[j + 1] * d[i] / 3.0;
                    }
                    func(t + a[j], y, d, deltae,ref delta,ref delta0);
                }
                for (i = 0; i < n - 1; i++)
                {
                    y[i] = b[i] + h * d[i] / 6.0;
                }
                t += h;

                times++;
                if (times > 2)
                {
                    times = 2;
                    xe = (y[4] * Math.Cos(y[2]) - y[5] * Math.Cos(y[3]) * Math.Sin(y[2])) * h + x0;//////////决定船行驶路线
                    //x0[1] = (y[4] * cos(y[2]) - y[5] * cos(y[3]) * sin(y[2])) * h + x0[0];
                    ye = (y[4] * Math.Sin(y[2]) + y[5] * Math.Cos(y[3]) * Math.Cos(y[2])) * h + y0;//////////决定船行驶路线
                    bosai0 = bosaie;
                    bosaie = bosai2;
                    bosai2 = (y[6] * Math.Cos(y[3])) * h + bosaie;
                    FAIe = y[7] * h + FAI0;
                    ue = (xe - x0) / h;
                    ve = (ye - y0) / h;

                    r0 = (bosai2 - bosaie) / h;

                    pe = (FAIe - FAI0) / h;

                    deltae = deltae + (KP + KI / 2 + KD) * (sethangxiang - bosai2) + (KI / 2 - KP - 2 * KD) * (sethangxiang - bosaie) + KD * (sethangxiang - bosai0);
                }
                //保存最后运算结果
                z[0] = xe;
                z[1] = ye;
                z[2] = bosai2;//HDG
                z[3] = FAIe;
                z[4] = ue;
                z[5] = ve;
                z[6] = r0;
                z[7] = pe;
                z[8] = delta;//sqrt(u0[1]*u0[1]+v0[1]*v0[1]);//SOG
                //z[9]=atan(-y[5]/y[4]);//DRIFT
                drift = Math.Atan(-y[5] / y[4]);//DRIFT
                //Debug 
                //char szBuffer[512] = {0};
                //sprintf(szBuffer, "%f	%f	%f	%f	%f	%f	%f	%f	%f\n", z[0], z[1], z[2], z[3], z[4], z[5], z[6], z[7], z[8]);
                //OutputDebugString(szBuffer);
                //Debug end

                x0 = xe;
                y0 = ye;


                FAI0 = FAIe;
                u0 = ue;
                v0 = ve;
                p0 = pe;
            }
            //	delta0=delta;
        }
        //public void func(double t, double[] y, double[] d, ref double deltae, ref double delta, ref double delta0)
        void func(double t, double[] y, double[] d,double deltae,ref double delta,ref double delta0)
        {
            // fun中变量
            int i, tw;

            double m, L, D, B, midu, CB, ZG, H, T, Dp, np, tp, b1, b2, P, hR, epxlong, hs;

            //double  nw;
            double V, RP, Beita, IZ, m11, m22, m66, Xu, XVR, XH;
            double K, TP, Yv, Yr, Yvv, Yvr, Yrr, YH, Lv, Nv, Nr, Nvvr, Nvrr, Nrr, NvFAI, NrFAI, NFAI, NH;
            double Np, GZFAI, ZH, KH, wp0, Beitap, wp, Jp, Kt, XP, S, lamuda, fa, sita, gs, uR, vR, UR;//S0
            double alfaR, tR, aH, xR, xH, zR, FN, XR, YR, NR, KR, c1, te;	  //delta01,
            double kafang, boxiangjiao, Ab, Bb, Cbb, Cbc, Db,/* *omiga, *E,*/ deltaomiga, h13, Zb;
            double ran_numf;//, *zsx, *Skexi;
            //	double	  *Xw, *Yw, *Kw, *Nw, *Xd, *Yd, *Nd, *bochangbi, *Cxwd, *Cywd, *Cnwd; 
            //throw new NotImplementedException();
            tw = 12;

            //参考int[] result = new int[6];
            double[] omiga = new double[12];
            double[] E = new double[12];
            double[] zsx = new double[12];
            double[] Skexi = new double[12];
            double[] Xw = new double[12];
            double[] Yw = new double[12];
            double[] Kw = new double[12];
            double[] Nw = new double[12];
            double[] Xd = new double[12];
            double[] Yd = new double[12];
            double[] Nd = new double[12];
            double[] bochangbi = new double[12];
            double[] Cxwd = new double[12];
            double[] Cywd = new double[12];
            double[] Cnwd = new double[12];
            for (int aa = 0; aa < 12; aa++)
            {
                omiga[aa] = 0.0;
                E[aa] = 0.0; zsx[aa] = 0.0;
                Skexi[aa] = 0.0; Xw[aa] = 0.0;
                Yw[aa] = 0.0; Kw[aa] = 0.0;
                Nw[aa] = 0.0; Xd[aa] = 0.0; Yd[aa] = 0.0;
                Nd[aa] = 0.0; bochangbi[aa] = 0.0; Cxwd[aa] = 0.0;
                Cywd[aa] = 0.0; Cnwd[aa] = 0.0;
            }

            omiga[0] = 0.01;
            E[0] = 0;
            zsx[0] = 0;
            Skexi[0] = 0;

            Xw[0] = 0;
            Yw[0] = 0;
            Nw[0] = 0;
            Kw[0] = 0;
            ///////////////////////////////////////////////////////////
            //zch 加了.0，表示double
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
            np = 3;         //主机转速
            tp = 0.167;   //0.08  推力减额系数;
            b1 = 6.5;   //舵叶弦长
            b2 = 4.6;   //舵叶弦长
            P = 3.7;   //7.6 螺距比
            hR = 7.2;   //舵叶高
            epxlong = 0.9;//实验系数
            hs = 6.9; //螺旋桨浸深
            delta = 1; //舵角 

            deltaomiga = 0.1; //浪频率间隔
            h13 = 1;  //浪高
            Zb = 6;  // 浮心
            boxiangjiao = 3.1415926 / 20;  //波向角 

            ///////////////////////////////////////////////////////////
            //zch 加了.0，表示double
            V = Math.Sqrt(Math.Pow(y[4], 2.0) + Math.Pow(y[5], 2.0));
            RP = y[6] * L / V;
            te = 2.5;
            Beita = Math.Atan(-y[5] / y[4]);

            //各纬度惯性矩及附加质量的计算
            IZ = m * (Math.Pow(0.245 * L, 2.0));//	zch 加了.0，表示double
            m11 = m * (1 / 100) * (0.398 + 11.98 * CB * (1 + 3.73 * (D / B)) - (2.89 * CB * (L / B)) * (1 + 1.13 * (D / B)) + 0.175 * CB * Math.Pow((L / B), 2) * (1 + 0.541 * (D / B)) - 1.107 * (L / B) * (D / B));
            m22 = m * (0.882 - 0.54 * CB * (1 - 1.6 * (D / B)) - 0.156 * (1 - 0.673 * CB) * (L / B) + 0.826 * (D / B) * (L / B) * (1 - 0.678 * (D / B)) - 0.638 * (L / B) * (D / B) * (1 - 0.669 * (D / B)));
            m66 = m * Math.Pow((L * 0.01 * (33 - 76.85 * CB * (1 - 0.784 * CB) + 3.43 * (L / B) * (1 - 0.63 * CB))), 2);


            //纵向水动力的计算
            Xu = 59.12 * Math.Pow(y[4], 4) - 462.8 * Math.Pow(y[4], 3) + 8775 * Math.Pow(y[4], 2) + 28940 * y[4] + 67640;
            XVR = (1.6757 * CB - 0.5054 - 1) * m22;
            XH = XVR * y[5] * y[6] - Xu;

            //横向水动力的计算
            K = 2 * D / L;
            TP = T / D;
            Yv = -0.5 * midu * L * D * V * (3.14159 * K / 2 + 1.4 * CB * (B / L)) * (1 + 0.67 * TP);
            Yr = 0.5 * midu * Math.Pow(L, 2) * D * V * (3.14159 * K / 4) * (1 + 0.8 * TP);
            Yvv = 0.5 * midu * L * D * (0.048265 - 6.293 * (1 - CB) * (D / B));
            Yvr = 0.5 * midu * Math.Pow(L, 2) * D * (-0.3791 + 1.28 * (1 - CB) * (D / B));
            Yrr = 0.5 * midu * Math.Pow(L, 3) * D * (0.0045 - 0.445 * (1 - CB) * (D / B));
            YH = Yv * y[5] + Yr * y[6] + Yvv * y[5] * Math.Abs(y[5]) + Yvr * y[6] * Math.Abs(y[5]) + Yrr * y[6] * Math.Abs(y[6]);

            //首摇力矩的计算
            Lv = K / (0.5 * 3.1415926 * K + 1.4 * CB * (B / L));
            Nv = -0.5 * midu * Math.Pow(L, 2) * D * V * K * (1 - 0.27 * TP / Lv);
            Nr = -0.5 * midu * Math.Pow(L, 3) * D * V * (0.54 * K - Math.Pow(K, 2)) * (1 + 0.3 * TP);
            Nvvr = 0.5 * midu * Math.Pow(L, 3) * D * (-6.0856 + 137.4735 * CB * (B / L) - 1029.514 * Math.Pow((CB * (B / L)), 2) + 2480.6082 * Math.Pow((CB * (B / L)), 3));
            Nvrr = 0.5 * midu * Math.Pow(L, 4) * D * (-0.0635 + 0.044145 * CB * (D / B));
            Nrr = 0.5 * midu * Math.Pow(L, 4) * D * (-0.0805 + 8.6092 * Math.Pow((CB * (B / L)), 2) - 36.9816 * Math.Pow((CB * (B / L)), 3));
            NvFAI = 0.5 * midu * Math.Pow(L, 2) * D * V * (-1.72 * K);
            NrFAI = 0.5 * midu * Math.Pow(L, 3) * D * V * (2.6 * (0.54 * K - Math.Pow(K, 2)));
            NFAI = 0.5 * midu * Math.Pow(L, 2) * D * Math.Pow(V, 2) * (-0.008);
            NH = Nv * y[5] + Nr * y[6] + Nvvr * Math.Pow(y[5], 2) * y[6] + Nrr * y[6] * Math.Abs(y[6]) + Nvrr * y[5] * Math.Pow(y[6], 2) + NFAI * y[3] + NvFAI * y[5] * Math.Abs(y[3]) + NrFAI * y[6] * Math.Abs(y[3]);

            //横倾力矩的计算

            Np = 0.12 * Math.Sqrt((m * (Math.Pow(B, 2) + 4 * Math.Pow(ZG, 2)) / (12)) * H * m * 9.8);
            GZFAI = H * Math.Sin(y[3]);
            ZH = ZG - (4 - B / D + 0.02 * Math.Pow((B / D - 5.35), 3)) * D;
            KH = -Np * y[7] - m * 9.8 * GZFAI - YH * ZH;

            //螺旋桨纵向推进力计算
            wp0 = 0.5 * CB - 0.05;
            Beitap = Beita - 0.5 * RP;
            wp = wp0 * Math.Exp(-4 * Math.Pow(Beitap, 2));
            Jp = y[4] * (1 - wp) / (Dp * np);
            Kt = 0.25035 * Math.Pow(Jp, 3) - 0.58638 * Math.Pow(Jp, 2) - 0.067363 * Jp + 0.42379;

            double npresult = Math.Pow(np, 2);
            double dpresult = Math.Pow(Dp, 4);
            XP = 4.0 * (1.0 - tp) * midu * npresult * dpresult * Kt;


            //舵力的计算 
            S = 1 - y[4] * (1 - wp) / (np * P);
            delta = deltae - (deltae - delta0) * Math.Exp(-t / te);
            //	delta0=delta;
            lamuda = 2 * hR / (b1 + b2);
            fa = 6.13 * lamuda / (2.25 + lamuda);
            sita = Dp / hR;
            gs = sita * (0.6 / epxlong) * S * (2 - (2 - (0.6 / epxlong)) * S) / Math.Pow((1 - S), 2);

            if (delta > 0)
                c1 = 1.065;
            else
                c1 = 0.935;


            uR = y[4] * (1 - wp) * Math.Sqrt(1 + c1 * gs);
            vR = (1.163314 - 1.982836 * CB + 1.390152 * Math.Pow(CB, 2)) * (y[5] - L * y[6]);
            UR = Math.Sqrt(Math.Pow(uR, 2) + Math.Pow(vR, 2));
            //S0=1-y[4]*(1-wp0)/(np*P);
            //delta01=-(2*S0+0.6);
            alfaR = delta - Math.Atan(vR / uR);//-delta01

            tR = 1 - (0.7382 - 0.0539 * CB + 0.1755 * Math.Pow(CB, 2));
            aH = 0.6784 - 1.3374 * CB + 1.8891 * Math.Pow(CB, 2);
            xR = -0.5 * L;
            xH = -(0.4 + 0.1 * CB) * L;
            zR = ZG - D + hs;

            FN = 0.5 * midu * 0.5 * (b1 + b2) * hR * fa * Math.Pow(UR, 2) * Math.Sin(alfaR);


            XR = -(1 - tR) * FN * Math.Sin(delta);
            YR = (1 + aH) * FN * Math.Cos(delta);
            NR = (xR + aH * xH) * FN * Math.Cos(delta);
            KR = (1 + aH) * zR * FN * Math.Cos(delta);

            //波浪主扰动力

            kafang = y[2] - boxiangjiao;
            Ab = L * Math.Cos(kafang) / (2 * 9.8);
            Bb = B * Math.Sin(kafang) / (2 * 9.8);
            Cbb = 4 * midu * Math.Pow(9.8, 3) / Math.Cos(kafang);
            Cbc = 4 * midu * Math.Pow(9.8, 3) / Math.Sin(kafang);
            Db = V * Math.Cos(kafang) / 9.8;

            //srand((unsigned)time(NULL));    
            double sumX, sumY, sumK, sumN, sumXd, sumYd, sumNd;
            sumX = 0;
            sumY = 0;
            sumK = 0;
            sumN = 0;
            sumXd = 0;
            sumYd = 0;
            sumNd = 0;

            for (i = 1; i < tw; i++)
            {
                ran_numf = 0;//rand()/(double)(RAND_MAX); 

                //cout<<ran_numf<<endl;
                omiga[i] = deltaomiga * i;
                //cout<<omiga[i]<<endl;
                Skexi[i] = (8.1 * Math.Pow(10.0, (-3.0)) * Math.Pow(9.8, 2.0) / Math.Pow(omiga[i], 5.0)) * Math.Exp(-3.11 / (h13 * h13 * Math.Pow(omiga[i], 4.0)));
                //cout<<Skexi[i]<<endl;
                E[i] = Math.Sqrt(2 * Skexi[i] * deltaomiga);
                //cout<<E[i]<<endl;
                zsx[i] = 1 - Math.Exp(-Math.Pow(omiga[i], 2.0) * D / 9.8);
                //cout<<zsx[i]<<endl;
                Xw[i] = -(Cbc / Math.Pow(omiga[i], 4)) * E[i] * zsx[i] * Math.Sin(Math.Pow(omiga[i], 2) * Ab) * Math.Sin(Math.Pow(omiga[i], 2) * Bb) * Math.Sin((omiga[i] - Math.Pow(omiga[i], 2) * Db) * t - 2 * 3.14159 * ran_numf);
                sumX += Xw[i];
                //cout<<Xw[i]<<endl;
                //cout<<sumX<<endl;
                Yw[i] = +(Cbb / Math.Pow(omiga[i], 4)) * E[i] * zsx[i] * Math.Sin(Math.Pow(omiga[i], 2) * Ab) * Math.Sin(Math.Pow(omiga[i], 2) * Bb) * Math.Sin((omiga[i] - Math.Pow(omiga[i], 2) * Db) * t - 2 * 3.14159 * ran_numf);
                sumY += Yw[i];
                Kw[i] = -(Cbb / Math.Pow(omiga[i], 4)) * E[i] * zsx[i] * Math.Sin(Math.Pow(omiga[i], 2) * Ab) * Math.Sin(Math.Pow(omiga[i], 2) * Bb) * Math.Sin((omiga[i] - Math.Pow(omiga[i], 2) * Db) * t - 2 * 3.14159 * ran_numf) * Zb +
                    (Cbb / (2 * 9.8 * Math.Pow(omiga[i], 2))) * E[i] * zsx[i] * Math.Sin(Math.Pow(omiga[i], 2) * Ab) * Math.Sin((omiga[i] - Math.Pow(omiga[i], 2) * Db) * t - 2 * 3.14159 * ran_numf) *
                    ((2 * Math.Pow(9.8, 2) * Math.Sin(Math.Pow(omiga[i], 2) * Bb)) / (Math.Pow(omiga[i], 4) * Math.Pow(Math.Sin(kafang), 2)) - (B * 9.8 * Math.Cos(Math.Pow(omiga[i], 2) * Bb)) / (Math.Pow(omiga[i], 2) * Math.Sin(kafang)));
                sumK += Kw[i];
                Nw[i] = -((Math.Sin(kafang) * Cbc) / (2 * 9.8 * Math.Pow(omiga[i], 2))) * E[i] * zsx[i] * Math.Sin(Math.Pow(omiga[i], 2) * Bb) * Math.Cos((omiga[i] - Math.Pow(omiga[i], 2) * Db) * t - 2 * 3.14159 * ran_numf) *
                    ((2 * Math.Pow(9.8, 2) * Math.Sin(Math.Pow(omiga[i], 2) * Ab)) / (Math.Pow(omiga[i], 4) * Math.Pow(Math.Cos(kafang), 2)) - (L * 9.8 * Math.Cos(Math.Pow(omiga[i], 2) * Bb)) / (Math.Pow(omiga[i], 2) * Math.Cos(kafang)));
                sumN += Nw[i];


                bochangbi[i] = (2 * 3.1415926 * 9.8) / (Math.Pow(omiga[i], 2) * L);
                Cxwd[i] = 0.05 - 0.2 * bochangbi[i] + 0.75 * Math.Pow(bochangbi[i], 2) - 0.51 * Math.Pow(bochangbi[i], 3);
                Cywd[i] = 0.46 + 6.83 * bochangbi[i] - 15.65 * Math.Pow(bochangbi[i], 2) + 8.44 * Math.Pow(bochangbi[i], 3);
                Cnwd[i] = -0.11 + 0.68 * bochangbi[i] - 0.79 * Math.Pow(bochangbi[i], 2) + 0.21 * Math.Pow(bochangbi[i], 3);

                Xd[i] = midu * 9.8 * L * Math.Cos(kafang) * Cxwd[i] * Skexi[i] * deltaomiga;
                sumXd += Xd[i];
                Yd[i] = midu * 9.8 * L * Math.Sin(kafang) * Cywd[i] * Skexi[i] * deltaomiga;
                sumYd += Yd[i];
                Nd[i] = midu * 9.8 * Math.Pow(L, 2) * Math.Sin(kafang) * Cnwd[i] * Skexi[i] * deltaomiga;
                sumNd += Nd[i];

            }

            d[0] = y[4];
            d[1] = y[5];
            d[2] = y[6];
            d[3] = y[7];
            d[4] = (XH + XP + XR + sumX + sumXd + (m + m22) * y[5] * y[6]) / (m + m11);
            d[5] = (YH + YR + sumY + sumYd - (m + m11) * y[4] * y[6]) / (m + m22);
            d[6] = (NH + NR + sumN + sumNd) / (IZ + m66);
            d[7] = (KH + KR + sumK) / (m * (Math.Pow(B, 2) + 4 * Math.Pow(ZG, 2)) / 12);

            //	delete [] omiga, E, zsx, Skexi, Xw, Yw, Kw, Nw, Xd, Yd, Nd, bochangbi, Cxwd, Cywd, Cnwd; 

        }
    }
}
