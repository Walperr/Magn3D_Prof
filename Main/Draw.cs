using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using Vectors;

namespace Magn3D_Prof.Main
{
    class Draw
    {
        public static void DrawBodyPoints(Chart BodiesDraw, Vector2 p0_v2, Vector2 p1_v2, double Xmin, double Xmax, double Ymin, double Ymax)
        {
            BodiesDraw.Series.Clear(); // Очищаем область

            BodiesDraw.ChartAreas[0].AxisX.Minimum = Xmin;
            BodiesDraw.ChartAreas[0].AxisX.Maximum = Xmax;

            BodiesDraw.ChartAreas[0].AxisY.Minimum = Ymin;
            BodiesDraw.ChartAreas[0].AxisY.Maximum = Ymax;

            BodiesDraw.ChartAreas[0].AxisX.LabelStyle.Format = "F" + SettingsForm.decimals;
            BodiesDraw.ChartAreas[0].AxisY.LabelStyle.Format = "F" + SettingsForm.decimals;

            for (int i = 0; i < Global.bodies.Count; i++) // Перебираем все тела
            {
                BodiesDraw.Series.Add("Body" + i); // Добавляем новую коллекцию элементов графика и задаем ей точечный тип
                BodiesDraw.Series[i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

                var p0 = new Vector3(p0_v2.x, p0_v2.y, 0);
                var p1 = new Vector3(p1_v2.x, p1_v2.y, 0);

                var l = (p1 - p0).Normalized();
                var n = new Vector3(0, 0, 1);
                var m = n & l;

                for (int j = 0; j < 8; j++)
                {
                    var point = Global.bodies[i].Vertices[j];
                    point = Vector3.RotateCoordinates(l, m, n, point - p0);
                    // Добавляем точки на график как проекции вершин тела
                    double x = point.X;
                    double y = point.Z;

                    BodiesDraw.Series[i].Points.AddXY(x, y);
                }
            }

            BodiesDraw.Series.Add("relief");
            BodiesDraw.Series["relief"].ChartType = SeriesChartType.Spline;
            BodiesDraw.Series["relief"].Color = Color.Green;
            
            var Profile = BodiesDraw.Parent.Parent.Parent.Parent.Parent.Parent.Parent as Profile;

            if(Profile.Relief != null)
            foreach (var point in Profile.Relief)
                BodiesDraw.Series["relief"].Points.AddXY(point.x, point.y);
        }
        public static void DrawBodyPointsTOP(Chart BodiesDraw,Vector2 P0, Vector2 P1, double Xmin, double Xmax, double Ymin, double Ymax)
        {
            BodiesDraw.Series.Clear(); // Очищаем область

            BodiesDraw.ChartAreas[0].AxisY.Minimum = Ymin;
            BodiesDraw.ChartAreas[0].AxisY.Maximum = Ymax;
            
            BodiesDraw.ChartAreas[0].AxisX.LabelStyle.Format = "F" + SettingsForm.decimals;
            BodiesDraw.ChartAreas[0].AxisY.LabelStyle.Format = "F" + SettingsForm.decimals;

            ChartArea ca = BodiesDraw.ChartAreas[0];

            // store the original value:
            var ipp0 = ca.InnerPlotPosition;

            // get the current chart area :
            ElementPosition cap = ca.Position;

            // get both area sizes in pixels:
            Size CaSize = new Size((int)(cap.Width * BodiesDraw.ClientSize.Width / 100f),
                (int)(cap.Height * BodiesDraw.ClientSize.Height / 100f));

            Size IppSize = new Size((int)(ipp0.Width * CaSize.Width / 100f),
                (int)(ipp0.Height * CaSize.Height / 100f));

            var ar = 1;

            if (IppSize.Height != 0)
            ar = IppSize.Width / IppSize.Height;

            var wid = (Xmax - Xmin) * ar;

            var centre = Xmax / 2 + Xmin / 2;

            BodiesDraw.ChartAreas[0].AxisX.Minimum = centre - wid/2;
            BodiesDraw.ChartAreas[0].AxisX.Maximum = centre + wid/2;

            BodiesDraw.Series.Add("Profile");
            BodiesDraw.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            BodiesDraw.Series[0].Points.AddXY(P0.x, P0.y);
            BodiesDraw.Series[0].Points.AddXY(P1.x, P1.y);
            
            for (int i = 0; i < Global.bodies.Count; i++) // Перебираем все тела
            {
                BodiesDraw.Series.Add("Body" + i); // Добавляем новую коллекцию элементов графика и задаем ей точечный тип
                BodiesDraw.Series[i+1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

                for (int j = 0; j < 8; j++)
                {
                    var point = Global.bodies[i].Vertices[j];
                    
                    // Добавляем точки на график как проекции вершин тела
                    double x = point.X;
                    double y = point.Y;

                    BodiesDraw.Series[i+1].Points.AddXY(x, y);
                }
            }
        }
    }
}
