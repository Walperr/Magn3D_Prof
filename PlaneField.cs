using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GRIDs;
using Vectors;

namespace Magn3D_Prof
{
    public partial class PlaneField : Form
    {
        public PlaneField()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;

            List<double> x = new List<double>(), y = new List<double>(), F = new List<double>();

            progressBar1.Value = 0;

            double Cx = 0, Cy = 0;

            Cx = Xminimum.GetValue();
            Cy = Yminimum.GetValue();
            x.Add(Cx);
            y.Add(Cy);
            while (Cx <= Xmaximum.GetValue())
            {
                Cx += dX.GetValue();
                x.Add(Cx);
            }
            while (Cy <= Ymaximum.GetValue())
            {
                Cy += dY.GetValue();
                y.Add(Cy);
            }

            double Fmin = double.PositiveInfinity, Fmax = double.NegativeInfinity;
            
            int N = y.Count * x.Count;

            foreach(var Y in y)
            {
                foreach (var X in x)
                {
                    double Fx = 0, Fy = 0, Fz = 0;

                    foreach(var body in Global.bodies)
                    {
                        Fx += body.Field(new Vector3(1, 0, 0), new Vector3(X, Y, -H.GetValue() - Global.Relief.Interp(0,new Vector2(X,Y))));
                        Fy += body.Field(new Vector3(0, 1, 0), new Vector3(X, Y, -H.GetValue() - Global.Relief.Interp(0, new Vector2(X, Y))));
                        Fz += body.Field(new Vector3(0, 0, 1), new Vector3(X, Y, -H.GetValue() - Global.Relief.Interp(0, new Vector2(X, Y))));
                    }
                    double dT = new Vector3(Fx + Global.T0.X, Fy + Global.T0.Y, Fz + Global.T0.Z).Getlength() - Global.T0.Getlength();

                    if (Fmin > dT) Fmin = dT;
                    if (Fmax < dT) Fmax = dT;

                    F.Add(dT);
                    progressBar1.Value = F.Count / N * 90;
                }
            }


            GRD field = new GRD(Fmin, Fmax, x.ToArray(), y.ToArray(), F.ToArray());

            if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.LastIndexOf('.')+1) == "grd") 
                field.SaveGRD(saveFileDialog1.FileName);
            if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.LastIndexOf('.') + 1) == "dat")
                field.SaveDAT(saveFileDialog1.FileName);

            MessageBox.Show(saveFileDialog1.FileName + " Сохранено", "Завершено");

            progressBar1.Value = 100;
        }

        private void PlaneField_Load(object sender, EventArgs e)
        {
            dX.minimum = 0.001;
            dY.minimum = 0.001;

            H.minimum = 0;
            listBox1.Items.Clear();
            foreach(var name in Global.HeightNames)
                listBox1.Items.Add(name + "\t mean = \t" + Global.HeigthMaps[listBox1.Items.Count].Zmean.ToString("F"+SettingsForm.decimals) + " м");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;

            var grid = Global.HeigthMaps[listBox1.SelectedIndex];

            double Fmin = double.PositiveInfinity, Fmax = double.NegativeInfinity;

            List<double> F = new List<double>();

            progressBar1.Value = 0;
            for (int j = 0; j < grid.Y.Count; j++)
            {
                for (int i = 0; i < grid.X.Count; i++)
                {
                    double Fx = 0, Fy = 0, Fz = 0;

                    var point = grid.GetCoordinates(i, j);

                    point.Z *= -1;

                    foreach (var body in Global.bodies)
                    {
                        Fx += body.Field(new Vector3(1, 0, 0), point);
                        Fy += body.Field(new Vector3(0, 1, 0), point);
                        Fz += body.Field(new Vector3(0, 0, 1), point);
                    }
                    double dT = new Vector3(Fx + Global.T0.X, Fy + Global.T0.Y, Fz + Global.T0.Z).Getlength() - Global.T0.Getlength();

                    if (Fmin > dT) Fmin = dT;
                    if (Fmax < dT) Fmax = dT;

                    F.Add(dT);
                    progressBar1.Value = F.Count / (grid.X.Count * grid.Y.Count) * 100;
                }
            }
            GRD field = new GRD(Fmin, Fmax, grid.X.ToArray(), grid.Y.ToArray(), F.ToArray());

            if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.LastIndexOf('.') + 1) == "grd")
                field.SaveGRD(saveFileDialog1.FileName);
            if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.LastIndexOf('.') + 1) == "dat")
                field.SaveDAT(saveFileDialog1.FileName);

            MessageBox.Show(saveFileDialog1.FileName + " Сохранено", "Завершено");
        }
    }
}
