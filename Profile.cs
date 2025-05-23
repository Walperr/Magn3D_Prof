﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using GRIDs;
using ploting;
using Vectors;

namespace Magn3D_Prof
{
    public partial class Profile : UserControl
    {
        private Vector2 Point0;
        private Vector2 Point1;

        private Vector3 Ox;
        private Vector3 Oy;
        private Vector3 ZeroPoint;
        private double _Angle;
        private double sko;

        internal List<Vector3> PointsH1;
        internal List<Vector3> PointsH2;
        internal List<Vector2> Relief;

        private List<int> BodiesToUpdate = new List<int>();
        internal List<Vector3> MeasureFields = new List<Vector3>();
        private bool AnomalFieldMode = true;

        public List<BodyControl> bodyControls = new List<BodyControl>();
        private static double _fieldOffset;

        private static double FieldOffset
        {
            get => _fieldOffset;
            set
            {
                if (Math.Abs(value - _fieldOffset) < 0.0001)
                    return;
                
                foreach (var profile in Global.Profiles)
                    profile.numeric1.SetValue(value);

                _fieldOffset = value;
            }
        }

        public Profile()
        {
            InitializeComponent();
            
            Point1X.SetValue(100);
        }

        public Profile(double X0,double X1, double Y0, double Y1)
        {
            InitializeComponent();

            Point0X.SetValue(X0);
            Point1X.SetValue(X1);

            Point0Y.SetValue(Y0);
            Point1Y.SetValue(Y1);
            
            UpdateAxis();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            Global.Profiles.Add(this);
            UpdateT0vals(); 
            if (Global.bodies != null)
            {
                foreach (var body in Global.bodies)
                {
                    body.AddProfile();
                    body.OnChangeParametrs += AddBodyToUpdate;

                    BodyControl control = new BodyControl { Dock = DockStyle.Top };
                    LeftSplit.Panel2.Controls.Add(control);
                    bodyControls.Add(control);
                }
            }

            Global.Setti.UpdateAll += UpdateNumerics;  
            UpdateNumerics(sender, e);

            Point0X.OnValueChanged += UpdateProfilePoints;
            Point0Y.OnValueChanged += UpdateProfilePoints;
            Point1X.OnValueChanged += UpdateProfilePoints;
            Point1Y.OnValueChanged += UpdateProfilePoints;
            numeric1.OnValueChanged += OffsetOnOnValueChanged;

            PointsCount.OnValueChanged += UpdatePointsCount;
            Hi1.OnValueChanged += UpdatePointsCount;
            Hi2.OnValueChanged += UpdatePointsCount;
            PointsCount.minimum = 1;
            PointsCount.Decimalplaces = 0;
            PointsCount.SetValue(10);

            Hi1.minimum = 0;
            Hi2.minimum = 0;

            Hmax.minimum = 1;
            Hmax.SetValue(30);
            Hmax.OnValueChanged += Draw;

            Hmin.minimum = -1000;
            Hmin.SetValue(-Global.Relief.Zmax);
            Hmin.OnValueChanged += Draw;

            if (Global.HeigthMaps != null)
            {
                Hi1.maximum = Global.HeigthMaps.Count-1;
                Hi2.maximum = Global.HeigthMaps.Count-1;
            }
            Hi1.Decimalplaces = 0;
            Hi2.Decimalplaces = 0;

            Top.DeferUpdate = true;
            float aspectRatio = (float)(Top.ClientRectangle.Width - Top.Padding.Horizontal) / (float)(Top.ClientRectangle.Height - Top.Padding.Vertical);
            Top.ViewHeight = (int)((Global.Relief.Xmax - Global.Relief.Xmin) / aspectRatio);
            Top.ViewXmin = (int)Global.Relief.Xmin;
            Top.ViewYmin = (int)Global.Relief.Ymin;
            Top.DeferUpdate = false;

            CurrentGrid = Global.Relief;
            Top.CPalette = ColorPalette.GetTerrain(CurrentGrid.Zmin,CurrentGrid.Zmax);
            
            UpdateProfilePoints(sender,e);
        }

        private void OffsetOnOnValueChanged(object sender, EventArgs e)
        {
            chart1_Click(this, EventArgs.Empty);
            FieldOffset = numeric1.GetValue();
        }

        private void UpdateProfilePoints(object sender, EventArgs e)
        {
            Point0 = new Vector2(Point0X.GetValue(), Point0Y.GetValue());
            Point1 = new Vector2(Point1X.GetValue(), Point1Y.GetValue());
            UpdateAxis();
        }
        private void UpdateAxis()
        {
            var P0X = Point0X.GetValue();
            var P0Y = Point0Y.GetValue();

            var P1X = Point1X.GetValue();
            var P1Y = Point1Y.GetValue();

            ZeroPoint = new Vector3(P0X, P0Y, 0);
            Ox = new Vector3(P1X - P0X, P1Y - P0Y, 0).Normalized();
            Oy = (new Vector3(0, 0, 1)) & Ox;
            _Angle = Math.Acos(Ox * (new Vector3(1, 0, 0))) * 180 / Math.PI;
            if ((Ox * new Vector3(0, 1, 0)) > 0)
                _Angle *= -1;

            foreach (var control in bodyControls)
                control.UpdateParameters();

            Top.DeferUpdate = true;
            Top.Start = new Vector2(P0X, P0Y);
            Top.DeferUpdate = false;
            Top.End = new Vector2(P1X, P1Y);

            RecalculatePoints();
        }
        public void RecalculatePoints()
        {
            PointsH1 = new List<Vector3>();
            PointsH2 = new List<Vector3>();
            Relief = new List<Vector2>();
            MeasureFields = new List<Vector3>();

            chart1.Series[5].Points.Clear();
            chart1.Series[6].Points.Clear();

            double Length = (Point1.x - Point0.x) * (Point1.x - Point0.x) + (Point1.y - Point0.y) * (Point1.y - Point0.y);
            Length = Math.Sqrt(Length);

            if (Length == 0) return;

            double dr = Length / PointsCount.GetValue();

            Vector3 r = ZeroPoint;

            for(int i = 0; i < PointsCount.GetValue(); i ++)
            {
                var Point = new Vector2(r.X, r.Y);
                var Z1 = Global.HeigthMaps[(int)Hi1.GetValue()].Interp(0, Point);
                var Z2 = Global.HeigthMaps[(int)Hi2.GetValue()].Interp(0, Point);
                var F1 = Global.MeasuredField[(int)Hi1.GetValue()].Interp(0, Point);
                var F2 = Global.MeasuredField[(int)Hi2.GetValue()].Interp(0, Point);
                var R = Global.Relief.Interp(0, Point);

                PointsH1.Add(new Vector3(r.X, r.Y, -Z1-R));
                PointsH2.Add(new Vector3(r.X, r.Y, -Z2-R));
                
                double dist = (r - ZeroPoint).Getlength();

                Relief.Add(new Vector2(dist, -R));

                MeasureFields.Add(new Vector3(dist, F1, F2));

                chart1.Series[5].Points.AddXY(MeasureFields[i].X, MeasureFields[i].Y);
                chart1.Series[6].Points.AddXY(MeasureFields[i].X, MeasureFields[i].Z);

                r = r + Ox * dr;
            }
            BodiesToUpdate = new List<int>();

            for (int i = 0; i < Global.bodies.Count; i++)
                BodiesToUpdate.Add(i);
            chart1_Click(this, new EventArgs());
        }
        private void UpdatePointsCount(object sender, EventArgs e)
        {
            RecalculatePoints();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart1.Series[4].Points.Clear();

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "F" + SettingsForm.decimals;
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "F" + SettingsForm.decimals;

            if (PointsH1.Count == 0) return;

            UpdateField();

            var index = Global.Profiles.IndexOf(this);

            if (index == -1) return;

            for(int i = 0; i < PointsCount.GetValue(); i++)
            {
                double FieldX1 = 0, FieldY1 = 0, FieldZ1 = 0, FieldX2 = 0, FieldY2 = 0, FieldZ2 = 0;
                double x = 0;
                foreach(var body in Global.bodies)
                {
                    x = body.GetField(index, (int)Hi1.GetValue())[i].W;
                    FieldX1 += body.GetField(index, (int)Hi1.GetValue())[i].X;
                    FieldY1 += body.GetField(index, (int)Hi1.GetValue())[i].Y;
                    FieldZ1 += body.GetField(index, (int)Hi1.GetValue())[i].Z;
                    FieldX2 += body.GetField(index, (int)Hi2.GetValue())[i].X;
                    FieldY2 += body.GetField(index, (int)Hi2.GetValue())[i].Y;
                    FieldZ2 += body.GetField(index, (int)Hi2.GetValue())[i].Z;
                }
                var T1 = new Vector3(FieldX1, FieldY1, FieldZ1) + Global.T0;

                double dT1 = T1.Getlength() - Global.T0.Getlength();

                var T2 = new Vector3(FieldX2, FieldY2, FieldZ2) + Global.T0;

                double dT2 = T2.Getlength() - Global.T0.Getlength();
                
                chart1.Series[0].Points.AddXY(x, dT1 - numeric1.GetValue());
                chart1.Series[1].Points.AddXY(x, dT2 - numeric1.GetValue());
                chart1.Series[2].Points.AddXY(x, FieldX1);
                chart1.Series[3].Points.AddXY(x, FieldY1);
                chart1.Series[4].Points.AddXY(x, FieldZ1);

            }
            if (chart1.Series[0].Points.Count > 0)
            {
                double summ = 0;
                int N = chart1.Series[0].Points.Count;
                for (int i = 0; i < N; i++)
                {
                    summ += (chart1.Series[0].Points[i].YValues[0] - chart1.Series[5].Points[i].YValues[0]) *
                        (chart1.Series[5].Points[i].YValues[0] - chart1.Series[5].Points[i].YValues[0]);
                }
                for (int i = 0; i < N; i++)
                {
                    summ += (chart1.Series[1].Points[i].YValues[0] - chart1.Series[6].Points[i].YValues[0]) *
                        (chart1.Series[1].Points[i].YValues[0] - chart1.Series[6].Points[i].YValues[0]);
                }
                sko = Math.Sqrt(summ / (2*N));
                SKOlabel.Text = "СКО = " + sko.ToString("F" + SettingsForm.decimals);
            }

        }
        public void UpdateField()
        {
            var i = Global.Profiles.IndexOf(this);

            if (Global.bodies.Count == 0) return;

            foreach(var j in BodiesToUpdate)
            {
                Global.bodies[j].UpdateField(i, (int)Hi1.GetValue(), PointsH1);
                Global.bodies[j].UpdateField(i, (int)Hi2.GetValue(), PointsH2);
            }
            BodiesToUpdate = new List<int>();
        }

        public void ShowField()
        {
            chart1_Click(this, new EventArgs());
        }

        public void AddBodyToUpdate(Prismbody sender, EventArgs e)
        {
            var ind = Global.bodies.IndexOf(sender);

            if (ind == -1) ind = Global.bodies.Count;

            if (!BodiesToUpdate.Contains(ind))
                BodiesToUpdate.Add(ind);

            Draw(sender, e);
        }

        public Vector2 GetStartPoint()
        {
            return Point0;
        }
        public double GetAngle()
        {
            return _Angle;
        }

        public Vector3[] GetAxis()
        {

            Vector3[] vectors = new Vector3[2];

            vectors[0] = Ox;
            vectors[1] = Oy;

            return vectors;
        }

        private void addbody_Click(object sender, EventArgs e)
        {
            var startPoint = Global.MeasuredField[0].GetXY(Global.MeasuredField[0].Zmax);

            if (Global.MeasuredField[0].Zmax == 0) startPoint = new Vector2(SettingsForm.minX, SettingsForm.minY);

            var Prism = new Prismbody(startPoint.x, startPoint.y, 5, 5, 5, 2, 2, 2, 0, 0, 90, 3, 0, 0, false);

            Global.bodies.Add(Prism);

            CreateControls();

            chart1_Click(this, new EventArgs());
        }
        private void CreateControls()
        {
            foreach (var profile in Global.Profiles)
            {
                BodyControl body = new BodyControl { Dock = DockStyle.Top };
                profile.LeftSplit.Panel2.Controls.Add(body);
                profile.bodyControls.Add(body);
            }
            Draw(this, new EventArgs());
        }
        public void DeleteControls(int index)
        {
            var control = bodyControls[index];

            LeftSplit.Panel2.Controls.Remove(control);

            bodyControls.Remove(control);

            for(int i = 0; i < BodiesToUpdate.Count; i++)
            {
                if (BodiesToUpdate[i] > index)
                    BodiesToUpdate[i] -= 1;
            }
            for (int i = 0; i < BodiesToUpdate.Count; i++)
            {
                if (i == index)
                {
                    BodiesToUpdate.RemoveAt(i);
                    break;
                }
            }
            Draw(this, new EventArgs());
        }

        private void rememberField_Click(object sender, EventArgs e)
        {
            chart1.Series[7].Points.Clear();

            foreach (var point in chart1.Series[0].Points)
            {
                chart1.Series[7].Points.Add(point);
            }
        }

        public void TurnToAnomalField()
        {
            AnomalFieldMode = true; // Включить режим аномального поля

            chart1.Series[0].Enabled = true;
            chart1.Series[1].Enabled = true;
            chart1.Series[2].Enabled = false;
            chart1.Series[3].Enabled = false;
            chart1.Series[4].Enabled = false;
            chart1.Series[5].Enabled = true;
            chart1.Series[6].Enabled = true;
            chart1.Series[7].Enabled = true;
        }

        /// <summary>
        /// Включает режим отображения поля по компонентам
        /// </summary>
        public void TurnToModelField()
        {
            AnomalFieldMode = false; // Выключить режим аномального поля

            chart1.Series[0].Enabled = false;
            chart1.Series[1].Enabled = false;
            chart1.Series[2].Enabled = true;
            chart1.Series[3].Enabled = true;
            chart1.Series[4].Enabled = true;
            chart1.Series[5].Enabled = false;
            chart1.Series[6].Enabled = false;
            chart1.Series[7].Enabled = false;
        }

        /// <summary>
        /// Возвращает режим отображения поля. 
        /// Аномальное поле - true
        /// Компоненты поля - false
        /// </summary>
        /// <returns></returns>
        public bool GetFieldMode()
        {
            return AnomalFieldMode; // Возвращаем режим отображения поля
        }

        public void UpdateNumerics(object sender, EventArgs e)
        {
            Point0X.maximum = Global.Relief.Xmax;
            Point0X.minimum = Global.Relief.Xmin;

            Point1X.maximum = Global.Relief.Xmax;
            Point1X.minimum = Global.Relief.Xmin;

            Point0Y.maximum = Global.Relief.Ymax;
            Point0Y.minimum = Global.Relief.Ymin;

            Point1Y.maximum = Global.Relief.Ymax;
            Point1Y.minimum = Global.Relief.Ymin;

            Point0X.Decimalplaces = (int)SettingsForm.decimals;
            Point0Y.Decimalplaces = (int)SettingsForm.decimals;

            Point1X.Decimalplaces = (int)SettingsForm.decimals;
            Point1Y.Decimalplaces = (int)SettingsForm.decimals;

            T0x.Decimalplaces = (int)SettingsForm.decimals;
            T0y.Decimalplaces = (int)SettingsForm.decimals;
            T0z.Decimalplaces = (int)SettingsForm.decimals;


            Hi1.maximum = Global.HeigthMaps.Count - 1;
            Hi2.maximum = Global.HeigthMaps.Count - 1;

            Hi1.SetValue(0);
            Hi2.SetValue(0);

            Hmax.Decimalplaces = (int)SettingsForm.decimals;
            Hmin.Decimalplaces = (int)SettingsForm.decimals;

            chooseGrid.Items.Clear();
            chooseGrid.Items.Add("рельеф");
            foreach (var name in Global.MeasFieldNames)
                chooseGrid.Items.Add(name.Substring(name.LastIndexOf("\\")+1));
            
            RecalculatePoints();

            Draw(sender, e);
        }

        public void UpdateT0vals()
        {
            T0x.SetValue(Global.T0.X);
            T0y.SetValue(Global.T0.Y);
            T0z.SetValue(Global.T0.Z);
        }

        public void LoadField(string filename)
        {
            string s; double x = 0, y = 0; int j; // Вспомагательные переменные

            chart1.Series[7].Points.Clear(); // Очищаем график
                using (StreamReader sr = new StreamReader(filename)) // Открываем файл filename
             {
                 // Создаем новый путь к файлу с таким же названием, но в папке проекта
                 string newpath = Global.ProjectPath + "\\" + filename.Remove(0, filename.LastIndexOf("\\"));
                 StreamWriter sw = new StreamWriter("l"); // Создаем пустой файл для записи
                 if (!File.Exists(newpath)) // Если файл в проекте не существует
                 {
                     sw.Close();
                     sw = new StreamWriter(newpath); // Создаем его
                     sw.WriteLine(sr.ReadLine()); // Переписываем первую строку
                 }
                 else sr.ReadLine(); // Если файл существует, переносим курсор на строку без перезаписи
                 while (!sr.EndOfStream) // Пока не дойдем до конца файла
                 {
                     s = sr.ReadLine(); // Считаем строку
                     sw.WriteLine(s); // Переписываем ее в новый файл
                     j = s.IndexOf(','); // Получаем индекс разделителя (запятая)
                     // Получаем координату х профиля
                     x = double.Parse(s.Substring(0, j).Replace('.', ','), System.Globalization.NumberStyles.Any);
                     // Получаем значение поля
                     y = double.Parse(s.Substring(j + 1).Replace('.', ','), System.Globalization.NumberStyles.Any);
                     chart1.Series[7].Points.AddXY(x, y); // Добавляем точки на график
                 }
                 sr.Close(); // Закрываем файл чтения
                 sw.Close(); // Закрываем файл записи
             }
        }

        private void PaintA(object sender, PaintEventArgs e)
        {
            // Если рисовать нечего прекращаем
            if (Slit.Series.Count == 0 || Slit.Series[0].Points.Count == 0 || Slit.Series.Count < Global.bodies.Count)
                return;

            var pen = new Pen(Color.Black, 1.0f); // Создаем кисть
            var ChartArea = Slit.ChartAreas[0]; // Объявляем ссылку на область отрисовки

            for (int j = 0; j < Global.bodies.Count; j++) // Перебираем все тела
            {
                pen.Color = Color.Black; // Задали кисти такой же цвет, как у вершин
                if (j == Global.SelectedBodyIndex)
                    pen.Color = Color.Blue;

                Vector2[] XYs = new Vector2[8]; // Массив с координатами точек

                // Перебираем все точки тела и добавляем их координаты в массив, переводя в координаты экрана
                for (int i = 0; i < 8; i++)
                {
                    XYs[i].x = Slit.Series[j].Points[i].XValue;
                    XYs[i].y = Slit.Series[j].Points[i].YValues[0];

                    XYs[i].x = ChartArea.AxisX.ValueToPixelPosition((float)XYs[i].x);
                    XYs[i].y = ChartArea.AxisY.ValueToPixelPosition((float)XYs[i].y);
                }

                Graphics g = e.Graphics;
                
                SolidBrush peg = new SolidBrush(Color.Gray);
                GraphicsPath gp = new GraphicsPath(FillMode.Winding);
                gp.AddPolygon(new Point[] { new Point((int)XYs[4].x, (int)XYs[4].y), new Point((int)XYs[5].x, (int)XYs[5].y), new Point((int)XYs[7].x, (int)XYs[7].y), new Point((int)XYs[6].x, (int)XYs[6].y) });
                g.FillPath(peg, gp);

                // Рисуем линии, соответствующие ребрам призмы (12 штук)
                g.DrawLine(pen, (float)XYs[0].x, (float)XYs[0].y, (float)XYs[1].x, (float)XYs[1].y);
                g.DrawLine(pen, (float)XYs[1].x, (float)XYs[1].y, (float)XYs[3].x, (float)XYs[3].y);
                g.DrawLine(pen, (float)XYs[3].x, (float)XYs[3].y, (float)XYs[2].x, (float)XYs[2].y);
                g.DrawLine(pen, (float)XYs[2].x, (float)XYs[2].y, (float)XYs[0].x, (float)XYs[0].y);
                
                g.DrawLine(pen, (float)XYs[4].x, (float)XYs[4].y, (float)XYs[5].x, (float)XYs[5].y);
                g.DrawLine(pen, (float)XYs[5].x, (float)XYs[5].y, (float)XYs[7].x, (float)XYs[7].y);
                g.DrawLine(pen, (float)XYs[7].x, (float)XYs[7].y, (float)XYs[6].x, (float)XYs[6].y);
                g.DrawLine(pen, (float)XYs[6].x, (float)XYs[6].y, (float)XYs[4].x, (float)XYs[4].y);
                
                g.DrawLine(pen, (float)XYs[0].x, (float)XYs[0].y, (float)XYs[4].x, (float)XYs[4].y);
                g.DrawLine(pen, (float)XYs[1].x, (float)XYs[1].y, (float)XYs[5].x, (float)XYs[5].y);
                g.DrawLine(pen, (float)XYs[3].x, (float)XYs[3].y, (float)XYs[7].x, (float)XYs[7].y);
                g.DrawLine(pen, (float)XYs[2].x, (float)XYs[2].y, (float)XYs[6].x, (float)XYs[6].y);
            }
        }
        public void Draw(object sender, EventArgs e)
        {
            if (Hmax.GetValue() == 0)
                Hmax.SetValue(1);
            
            Magn3D_Prof.Draw.DrawBodyPoints(Slit, Point0, Point1, chart1.ChartAreas[0].AxisX.Minimum, chart1.ChartAreas[0].AxisX.Maximum, Hmin.GetValue(), Hmax.GetValue());
           // Magn3D_Prof.Draw.DrawBodyPointsTOP(DrawPlace2, Point0, Point1, SettingsForm.minX, SettingsForm.maxX,
                                               //SettingsForm.minY, SettingsForm.maxY);
            
            Top.Grid = CurrentGrid;
        }

        public IGrid CurrentGrid { get; set; }

        private void resetView_Click(object sender, EventArgs e)
        {
            Top.ViewHeight = (int)(CurrentGrid.Ymax - CurrentGrid.Ymin);
            var ar = (float)(Top.ClientRectangle.Width - Top.Padding.Horizontal) /
                     (float)(Top.ClientRectangle.Height - Top.Padding.Vertical);
            Top.ViewXmin = (int) ((CurrentGrid.Xmax - CurrentGrid.Xmin - Top.ViewHeight * ar) / 2) +
                             (int)CurrentGrid.Xmin;
            Top.ViewYmin = (int) CurrentGrid.Ymin;
        }

        private void Slit_Click(object sender, EventArgs e)
        {
            Slit.Focus();
            Draw(sender,e);
        }
        private void chooseGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chooseGrid.SelectedIndex == 0)
            {
                CurrentGrid = Global.Relief;
                Top.CPalette = ColorPalette.GetTerrain(CurrentGrid.Zmin,CurrentGrid.Zmax);
                Top.Grid = CurrentGrid;
            }
            else
            {
                CurrentGrid = Global.MeasuredField[chooseGrid.SelectedIndex - 1];
                Top.CPalette = ColorPalette.GetRainbow(CurrentGrid.Zmin, CurrentGrid.Zmax);
                Top.Grid = CurrentGrid;
            }
            panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var pal = Top.CPalette;
            var rect = panel1.ClientRectangle;

            var hStep = (rect.Height - panel1.Padding.Vertical) / (pal.Colors.Length - 1);

            for (int i = 0; i < pal.Colors.Length - 1; i++)
            {
                var x1 = rect.X + panel1.Padding.Left;
                var x2 = rect.X + rect.Width - panel1.Padding.Right;
                var y = rect.Y + panel1.Padding.Top + i * hStep;
                
                var y2 = rect.Y + panel1.Padding.Top + (i+1) * hStep;
                
                var curRect = new Rectangle(x1, y, x2 - x1, y2 - y);
                e.Graphics.FillRectangle(new SolidBrush(pal.Colors[i]),curRect);
                e.Graphics.DrawRectangle(Pens.Black,curRect);
                e.Graphics.DrawString(pal.Values[i].ToString("F1"),Font,Brushes.Black,x2,y);
            }
        }
    }
}
