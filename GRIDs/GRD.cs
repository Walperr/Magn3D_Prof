﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GRIDs.Exporters;
using GRIDs.Exporters.Interfaces;
using GRIDs.Parsers;
using GRIDs.Parsers.Interfaces;
using Vectors;

namespace GRIDs
{
    public class GRD : IGrid
    {
        private int _N, _sNx, _sNy;
        private double _x_min, _x_max, _y_min, _y_max, _z_min, _z_max, _z_mean, _dX, _dY;
        private List<double> _z,_x,_y;

        public int SNx => _sNx;
        public int SNy => _sNy;
        public List<double> X => _x;
        public List<double> Y => _y;
        public List<double> Z => _z;

        public double Xmin => _x_min;
        public double Xmax => _x_max;
        
        public double Ymin => _y_min;
        public double Ymax => _y_max;
        
        public double Zmin => _z_min;
        public double Zmax => _z_max;
        public double Zmean => _z_mean;

        public double dX => _dX;
        public double dY => _dY;
        
        /// <summary>
        /// Создает сетку с заданным размером и шагом, устанавливая значение в каждом узле, равным z_values
        /// </summary>
        public GRD(double x_min, double x_max, double y_min, double y_max, double dx, double dy, double z_values)
        {
            _x_min = x_min;
            _x_max = x_max;
            _dX = dx;
            
            _y_min = y_min;
            _y_max = y_max;
            _dY = dy;

            _x = new List<double>();

            while(x_min < x_max)
            {
                _x.Add(x_min);
                x_min += dx;
            }
            _sNx = _x.Count;
            _y = new List<double>();

            while (y_min < y_max)
            {
                _y.Add(y_min);
                y_min += dy;
            }
            _sNy = _y.Count;
            _z = new List<double>();
            _N = _sNx * _sNy;

            for(int i = 0; i < _N; i++)
            {
                _z.Add(z_values);
            }

            _z_min = z_values;
            _z_max = z_values;
            _z_mean = z_values;
        }

        public GRD(double z_min, double z_max, double[] x, double[] y, double[] z)
        {
            _N = z.Length;
            _sNx = x.Length;
            _sNy = y.Length;
        
            _x_min = x[0];
            _x_max = x[_sNx - 1];
            _dX = (_x_max - _x_min) / (_sNx - 1);
            
            _y_min = y[0];
            _y_max = y[_sNy - 1];
            _dY = (_y_max - _y_min) / (_sNy - 1);
            
            _z_min = z_min;
            _z_max = z.Max();
            _z = z.ToList();
            _x = x.ToList();
            _y = y.ToList();

            var count = 0;
            foreach(var value in z)
            {
                if (!double.IsNaN(value))
                {
                    count++;
                    _z_mean += value;
                }
            }
            _z_mean /= count;
        }

        /// <summary>
        /// Загружает сетку из .GRD файла
        /// </summary>
        /// <param name="filename">Путь к файлу</param>
        /// <returns></returns>
        public static IGrid ReadGRD(string filename)
        {
            using (IGridParser parser = new GridParser(new BinaryReader(File.OpenRead(filename),Encoding.Default)))
            {
                return parser.ReadGRD();
            }
        }

        public void SaveGRD(string filename)
        {
            using (IGridExporter exporter = new GridExporter(new BinaryWriter(File.OpenWrite(filename),Encoding.Default),this))
            {
                exporter.SaveGrid();
            }
        }
        
        /// <summary>
        /// Возвращает координаты точки с индексами i,j
        /// </summary>
        public Vector3 GetCoordinates(int i, int j)
        {
            return new Vector3(_x[i],_y[j], _z[_x.Count*j+i]);
        }

        public Vector2 GetXY(double z_value)
        {
            for (int i = 0; i < _x.Count; i++)
            for (int j = 0; j < _y.Count; j++)
                if (Math.Abs(z_value - GetCoordinates(i, j).Z) < 0.001)
                    return new Vector2(_x[i], _y[j]);

            throw new ArgumentException("z_value not found");
        }
    
        public void SaveDAT(string filename)
        {
            string s = "";

            for(int i = 0; i < _sNx; i++)
            {
                for (int j = 0; j < _sNy; j++)
                {
                    var point = GetCoordinates(i, j);
                    s += point.X.ToString().Replace(",", ".") + "," + point.Y.ToString().Replace(",", ".") + "," + point.Z.ToString().Replace(",", ".") + "\n";
                }
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(filename)) // Создаем новый файл с именем filename
                {
                    sw.Write(s); // Записываем в него данные

                    sw.Close(); // Закрываем файл
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Возвращает интерполируемые значения z сетки, в указанных точках Points.
        /// </summary>
        public List<double> Interp(int LP, List<Vector2> points)
        {
            int LX = _x.Count, LY = _y.Count, N = points.Count,
                KX = 1, KY = 1, LOC, LOC1, N0,
                LX0, LXM1, LXM2, LP0,
                LY0, IX, IY, N1, I, IZ,
                LP1, LP2, KXM1, LYM1,
                LYM2, LYM3, J, JZ, IX1,
                IY1, IXM1, IYM1, LXM3, KYM1;

            double DX, DY, VX, VDX2, QX1, VDY2,
                QY1, XIX, YIY, Z02, S1, S2,
                S3, S4, S5, S6, S12, S14, C0,
                S23, C1, C2, C3, S25, S34, S45,
                S16, R10, S36, Z00, Z01,
                Z002, Z10, Z11, Z21, Z12, Z20,
                Z22, Z001, EXY, UX, UY,
                D2, D3, R0, R1, R2, V3, V2,
                CC1, VY, QX2, QY2, X2, X3, TLOC,
                XM1, YM1, X10, Y10;

            double[] A = new double[5];
            double[] B = new double[5];
            double[] D = new double[2];
            double[] E = new double[2];
            double[,] Q = new double[5, 2];
            double[] OX = new double[3];
            double[] OY = new double[3];
            double[,] O = new double[3, 3];
            double[] W = new double[N];

            double[,] Z_ = new double[LX, LY];

            for (int i = 0; i < LX; i++)
            for (int j = 0; j < LY; j++)
                Z_[i, j] = _z[LX * j + i];

            N0 = N;
            LX0 = LX;
            LY0 = LY;
            LP0 = LP;
            LP1 = LP + 1;
            LP2 = LP1 + 1;
            LXM1 = LX0 - 1;
            LYM1 = LY0 - 1;
            LXM2 = LXM1 - 1;
            LYM2 = LYM1 - 1;

            if (LP0 < 0) LP0 = 0;

            if (LP0 > 3) LP0 = 3;

            if (LX0 > LX0 + 2) goto _510;
            if (LY0 > LY0 + 2) goto _510;

            for (IX = 2; IX <= LX0; IX++)
            {
                if (_x[IX - 2] - _x[IX -1] < 0) continue;
                if (_x[IX - 2] - _x[IX -1] >= 0) goto _510;
            }

            for (IY = 2; IY <= LY0; IY++)
            {
                if (_y[IY - 2] - _y[IY-1] < 0) continue;
                if (_y[IY - 2] - _y[IY-1] >= 0) goto _510;
            }

            for (N1 = 1; N1 <= N0; N1++)
            {
                UX = points[N1-1].x;
                UY = points[N1-1].y;

                for (IX = 1; IX <= LXM2; IX++)
                {
                    if (UX - _x[IX] < 0) goto _32;
                    if (UX - _x[IX] == 0) goto _33;
                    if (UX - _x[IX] > 0) goto _31;
                    _31:;
                }
        
                KX = LXM1;
                goto _34;
                _32:
                KX = IX;
                goto _34;
                _33:
                KX = IX + 1;
                _34:
                for (IY = 1; IY <= LYM2; IY++)
                {
                    if (UY - _y[IY] < 0) goto _36;
                    if (UY - _y[IY] == 0) goto _37;
                    if (UY - _y[IY] > 0) goto _35;
                    _35:;
                }
        
                KY = LYM1;
                goto _38;
                _36:
                KY = IY;
                goto _38;
                _37:
                KY = IY + 1;
                _38:
                if (LP0 == 1) goto _800;

                D[0] = _x[KX] - _x[KX-1];
                D[1] = _y[KY] - _y[KY-1];

                E[0] = (UX - _x[KX-1]) / D[0];
                E[1] = (UY - _y[KY-1]) / D[1];

                if (LP0 != 0) goto _39;

                EXY = E[0] * E[1];

                W[N1-1] = Z_[KX-1, KY-1] * (EXY + 1.0 - E[0] - E[1]) + Z_[KX-1, KY] * (E[1] - EXY) +
                          Z_[KX, KY] * EXY + Z_[KX, KY - 1] * (E[0] - EXY);
                goto _30;

                _39:
                if (LP0 > 2) goto _51;
                if (KX > 1) goto _52;
                _55:
                if (KY != LYM1) goto _60;
                _56:
                KY = LYM2;

                goto _60;

                _52:
                if (KX < LXM1) goto _53;
                KX = LXM2;
                goto _55;
                _53:
                if (KY > 1) goto _54;

                goto _60;

                _54:
                if (KY < LYM1) goto _49;

                goto _56;

                _51:
                LYM3 = LYM2 - 1;
                LXM3 = LXM2 - 1;

                if (KX > 1) goto _72;

                _75:
                if (KY < LYM2) goto _60;

                _76:
                KY = LYM3;

                goto _60;
                _72:
                if (KX < LXM2) goto _73;

                KX = LXM3;

                goto _75;

                _73:
                if (KY > 1) goto _74;

                goto _60;

                _74:
                if (KY < LYM2) goto _49;

                goto _76;

                _49:
                I = 1;

                XM1 = (_x[KX - 2] - _x[KX-1]) / D[0];
                X2 = (_x[KX] - _x[KX-1]) / D[0];
                X3 = (_x[KX + 1] - _x[KX-1]) / D[0];

                _46:
                D2 = D[I - 1] * D[I - 1];
                Q[0, I - 1] = 1.0;
                Q[1, I - 1] = (E[I-1] - XM1) * D[I-1];
                V2 = E[I-1] * E[I-1];
                R1 = E[I-1] - 1.0;

                D3 = D2 * D[I-1];
                Q[2, I-1] = E[I-1] * Q[1, I-1] * 0.5 * D[I-1];
                R2 = R1 * R1;
                V3 = V2 * E[I-1];

                if (LP0 > 2) goto _42;

                Q[3, I-1] = (X2 - XM1) * V3 * (R1 - 2 * R2) / 6.0 * D3;

                goto _77;

                _42:
                Q[3, I-1] = (E[I-1] - 1.0) * Q[2, I-1] / 3.0 * D[I-1];
                CC1 = 1.0 + X2;
                C0 = X2 - CC1 + 1;
                C1 = 2.0 * CC1 - 3.0 * X2 - 1.0;
                C2 = 6.0 * CC1 - 10.0 * X2 - 2.0;
                C3 = 4.0 * CC1 - 10.0 * X2 - 2.0;

                Q[4, I-1] = (X3 - XM1) * V3 * E[I-1] * (C0 + C1 * R1 + (C2 + C3 * R1) * R2 / 24.0 * D3 * D[I-1]);

                _77:
                if (I == 1) goto _44;
                if (I == 2) goto _45;

                _44:
                XM1 = (_y[KY - 2] - _y[KY-1]) / D[1];
                X2 = (_y[KY] - _y[KY-1]) / D[1];
                X3 = (_y[KY + 1] - _y[KY-1]) / D[1];
                I = 2;

                goto _46;

                _45:

                R10 = 0.0;
                KX = KX - 1;
                KY = KY - 1;
                KXM1 = KX - 1;
                KYM1 = KY - 1;

                for (IX = 1; IX <= LP2; IX++)
                {
                    OPER(ref IX, ref KX, ref _x, ref A);

                    for (IY = 1; IY <= LP2; IY++)
                    {
                        OPER(ref IY, ref KY, ref _y, ref B);
                        R0 = 0.0;

                        for (I = 1; I <= IX; I++)
                        {
                            IZ = KXM1 + I;

                            for (J = 1; J <= IY; J++)
                            {
                                JZ = KXM1 + J;

                                R0 = R0 + (Z_[IZ-1, JZ-1] * A[I-1] * B[J-1]);
                            }
                        }

                        R10 = R10 + R0 * Q[IX-1, 0] * Q[IY-1, 1];
                    }
                }

                W[N1-1] = R10;
                goto _30;

                _60:
                Q[0, 0] = 1.0;
                Q[0, 1] = 1.0;
                Q[1, 0] = UX - _x[KX-1];
                Q[1, 1] = UY - _y[KY-1];
                Q[2, 0] = Q[1, 0] * (UX - _x[KX]) * 0.5;
                Q[2, 1] = Q[1, 1] * (UY - _y[KY]) * 0.5;

                if (LP0 == 2) goto _61;
                Q[3, 0] = Q[2, 0] * (UX - _x[KX + 1]) / 3.0;
                Q[3, 1] = Q[2, 1] * (UY - _y[KY + 1]) / 3.0;

                _61:

                R10 = 0.0;
                KXM1 = KX - 1;
                KYM1 = KY - 1;

                for (IX = 1; IX <= LP1; IX++)
                {
                    OPER(ref IX, ref KX, ref _x, ref A);

                    for (IY = 1; IY <= LP1; IY++)
                    {
                        OPER(ref IY, ref KY, ref _y, ref B);

                        R0 = 0.0;

                        for (I = 1; I <= IX; I++)
                        {
                            IZ = KXM1 + I;

                            for (J = 1; J <= IY; J++)
                            {
                                JZ = KYM1 + J;

                                R0 = R0 + (Z_[IZ-1, JZ-1]) * A[I-1] * B[J-1];
                            }
                        }

                        R10 = R10 + R0 * Q[IX-1, 0] * Q[IY-1, 1];
                    }
                }

                W[N1-1] = R10;

                goto _30;

                _800:
                IX = KX;
                IY = KY;
                IX1 = IX + 1;
                IY1 = IY + 1;
                IXM1 = IX - 1;
                IYM1 = IY - 1;

                XIX = _x[IX-1];
                YIY = _y[IY-1];

                DX = _x[IX1-1] - XIX;
                DY = _y[IY1-1] - YIY;

                VX = (UX - XIX) / DX;
                VY = (UY - YIY) / DY;

                QX1 = VX * DX;
                QY1 = VY * DY;

                VDX2 = QX1 * QX1;
                VDY2 = QY1 * QY1;

                OX[1] = XIX;
                OX[2] = _x[IX1-1];
                OY[1] = YIY;
                OY[2] = _y[IY1-1];
                O[1, 1] = Z_[IX-1, IY-1];
                O[1, 2] = Z_[IX-1, IY1-1];
                O[2, 1] = Z_[IX1-1, IY-1];
                O[2, 2] = Z_[IX1-1, IY1-1];

                if (IX == 1) goto _901;
                if (IY == 1) goto _902;

                OX[0] = _x[IXM1-1];
                OY[0] = _y[IYM1-1];

                O[0, 0] = Z_[IXM1-1, IYM1-1];
                O[0, 1] = Z_[IXM1-1, IY-1];
                O[0, 2] = Z_[IXM1-1, IY1-1];
                O[1, 0] = Z_[IX-1, IYM1-1];
                O[2, 0] = Z_[IX1-1, IYM1-1];

                goto _710;

                _901:
                OX[0] = _x[0] - _x[2] + _x[1];
                TLOC = (OX[0] - _x[2]) / DX;

                if (IY == 1) goto _903;
                OY[0] = _y[IYM1-1];

                O[1, 0] = Z_[0, IYM1-1];
                O[2, 0] = Z_[1, IYM1-1];

                for (LOC = 1; LOC <= 3; LOC++)
                {
                    LOC1 = IYM1 - 1 + LOC;

                    O[0, LOC-1] = Z_[2, LOC1-1] + (Z_[1, LOC1-1] - Z_[0, LOC1-1]) * TLOC;
                }
                goto _710;

                _902:
                OX[0] = _x[IXM1-1];
                OY[0] = _y[0] - _y[2] + _y[1];
                O[0, 1] = Z_[IXM1-1, 0];
                O[0, 2] = Z_[IXM1-1, 1];

                TLOC = (OY[0] - _y[2]) / DY;

                for (LOC = 1; LOC <= 3; LOC++)
                {
                    LOC1 = IXM1 - 1 + LOC;

                    O[LOC-1, 0] = Z_[LOC1-1, 2] + (Z_[LOC1-1, 1] - Z_[LOC1-1, 0]) * TLOC;
                }

                goto _710;

                _903:
                OY[0] = _y[0] + _y[1] - _y[2];
                O[0, 1] = Z_[2, 0] + (Z_[1, 0] - Z_[0, 0]) * TLOC;
                O[0, 2] = Z_[2, 1] + (Z_[1, 1] - Z_[0, 1]) * TLOC;
                TLOC = (OY[0] - _y[2]) / DY;
                O[1, 0] = Z_[0, 2] + (Z_[0, 1] - Z_[0, 0]) * TLOC;
                O[2, 0] = Z_[1, 2] + (Z_[1, 1] - Z_[1, 0]) * TLOC;
                O[0, 0] = (O[0, 1] + O[1, 0]) * 0.5;

                _710:
                XM1 = (OX[0] - OX[1]) / DX;
                YM1 = (OY[0] - OY[1]) / DY;
                QX1 = (VX - XM1) * DX;
                QY1 = (VY - YM1) * DY;
                QX2 = 0.5 * (1.0 - XM1) * VDX2 * (2.0 - VX);
                QY2 = 0.5 * (1.0 - YM1) * VDY2 * (2.0 - VY);

                X10 = OX[1] - OX[0];
                Y10 = OY[1] - OY[0];

                S3 = QX2 / (OX[2] - OX[0]);
                S4 = QY2 / (OY[2] - OY[0]);
                S5 = S3 / (OX[2] - OX[1]);
                S6 = S4 / (OY[2] - OY[1]);

                S3 = S3 / X10;
                S4 = S4 / Y10;

                S1 = QX1 / X10;
                S2 = QY1 / Y10;

                S12 = S1 * S2;
                S14 = S1 * S4;

                S23 = S2 * S3;
                S34 = S3 * S4;
                S25 = S2 * S5;
                S45 = S4 * S5;

                S16 = S1 * S6;
                S36 = S3 * S6;

                Z21 = S25 + S25 - 4.0 * S45;
                Z12 = S16 + S16 - 4.0 * S36;

                Z20 = S5 + S5 - Z21;
                Z02 = S6 + S6 - Z12;

                Z22 = 4.0 * S5 * S6;
                Z21 = Z21 - Z22;
                Z00 = S12 - S14 - S14 - S23 - S23 + 4.0 * S34;
                Z001 = S2 - S4 - S4;
                Z002 = S1 - S3 - S3;
                Z01 = Z001 - Z00 - Z02;
                Z10 = Z002 - Z00 - Z20;
                Z11 = Z00 - Z21 - Z12;
                Z12 = Z12 - Z22;
                Z00 = Z00 - Z001 - Z002 + 1.0;

                W[N1-1] = Z00 * O[0, 0] + Z10 * O[1, 0] + Z01 * O[0, 1] + Z11 * O[1, 1] + Z22 * O[2, 2] + Z20 * O[2, 0] + Z02 * O[0, 2] + Z21 * O[2, 1] + Z12 * O[1, 2];

                _30:;
            }

            _510:
            return W.ToList();
        }

        /// <summary>
        /// Возвращает интерполируемое значение z сетки, в указанной точке Point.
        /// </summary>
        public double Interp(int LP, Vector2 point)
        {
            var p = new List<Vector2>() { point };

            return Interp(LP, p)[0];
        }

        private void OPER(ref int LRP1, ref int I, ref List<double> X, ref double[] RZ)
        {
            int LR, LRPI, K, K0, KP1, IPK0, L, L0, IPKL0, IPL0;

            LR = LRP1 - 1;

            if (LR < 0) goto _100;
            if (LR == 0) goto _200;
            if (LR > 0) goto _300;

            _200:
            RZ[0] = 1.0;
            _400:
            return;
            _300:
            LRPI = LR + I;
            RZ[0] = LR / (X[I-1] - X[LRPI-1]);
            RZ[1] = -RZ[0];

            if (LR == 1) goto _400;

            for (K = 2; K <= LR; K++)
            {
                K0 = 1 + LR - K;
                KP1 = 1 + K;
                IPK0 = K0 + I;
                for (L = 1; L <= KP1; L++)
                {
                    L0 = KP1 - L;
                    IPL0 = L0 + I;
                    IPKL0 = IPK0 + L0;

                    if (L0 == K) RZ[L0] = RZ[L0-1] * K0 / (X[IPKL0 - 2] - X[IPL0 - 2]);
                    if (L0 == 0) RZ[0] = RZ[0] * K0 / (X[I-1] - X[IPK0-1]);
                    if (L0 > 0 && L0 < K) RZ[L0] = (RZ[L0] / (X[IPL0-1] - X[IPKL0-1]) + RZ[L0-1] / (X[IPKL0 - 2] - X[IPL0 - 2])) * K0;
                }
            }
            _100:
            return;
        }
    }
}