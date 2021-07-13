using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using Magn3D_Prof;

namespace ploting
{
    public partial class plot : UserControl
    {
        private Rectangle _viewport;
        
        private int _viewXmin = 0;
        private int _viewYmin = 0;
        private int _viewHeight;
        private int _contoursCount = 23;
        private int _labelsCountX;
        private int _labelsCountY;
        private ColorPalette _palette;


        private int _Xlast;
        private int _ylast;

        private Vector2 _start;
        private Vector2 _end;

        private GRD _grid;
        

        public plot()
        {
            InitializeComponent();
        }


        public bool DeferUpdate { get; set; }
        
        public int ContoursCount
        {
            get => _contoursCount;
            set
            {
                _contoursCount = value;
                UpdateViewport();
            }
        }

        public int ViewHeight
        {
            get => _viewport.Height;
            set
            {
                _viewHeight = (int)Math.Max(0,value);
                UpdateViewport();
            }
        }

        public int ViewXmin
        {
            get => _viewXmin;
            set
            {
                _viewXmin = value;
                UpdateViewport();
            }
        }

        public int ViewYmin
        {
            get => _viewYmin;
            set
            {
                _viewYmin = value;
                UpdateViewport();
            }
        }



        public GRD Grid
        {
            get => _grid;
            set
            {
                _grid = value;
                if(value != null)
                UpdateViewport();
            }
        }

        public int LabelsCountX
        {
            get => _labelsCountX;
            set
            {
                _labelsCountX = value;
                Refresh();
            }

        }

        public int LabelsCountY
        {
            get => _labelsCountY;
            set
            {
                _labelsCountY = value; 
                Refresh();
            }
        }

        public Vector2 Start
        {
            get => _start;
            set
            {
                _start = value; 
                Refresh();
            }
        }

        public Vector2 End
        {
            get => _end;
            set
            {
                _end = value;
                Refresh();
            }
        }

        public ColorPalette CPalette
        {
            get => _palette;
            set => _palette = value;
        }

        private void UpdateViewport()
        {
            float aspectRatio = (float)(ClientRectangle.Width - Padding.Horizontal) / (float)(ClientRectangle.Height - Padding.Vertical);
            _viewport = new Rectangle(_viewXmin, _viewYmin, (int)(aspectRatio * _viewHeight), _viewHeight);
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if(DeferUpdate) return;
            
            var graphics = e.Graphics;

            graphics.Clear(Color.Gray);

            if (_grid != null && !DesignMode && Enabled)
            {
                var dx = _grid.X[1] - _grid.X[0];
                var dy = _grid.Y[1] - _grid.Y[0];

                var Xs = _grid.X.Where(x =>
                 {
                     return (x > _viewXmin - dx && x < dx + _viewXmin + _viewport.Width);
                 }).OrderBy(x => x).ToArray();
                 var Ys = _grid.Y.Where(y =>
                 {
                     return (y > _viewYmin - dy && y < dy + _viewYmin + _viewport.Height);
                 }).OrderBy(y => y).ToArray();

                 int n = Xs.Length;
                 int m = Ys.Length;

                 if(n == 0 || m == 0) return;

                 int offsetX = _grid.X.IndexOf(Xs[0]);
                 int offsetY = _grid.Y.IndexOf(Ys[0]);

                 var step = (float)(ClientRectangle.Height - Padding.Vertical) / m;

                 var pts = new Point3F[n, m];

                 for (int i = 0; i < n; i++)
                 {
                     for (int j = 0; j < m; j++)
                     {
                         var point =  _grid.GetCoordinates(i + offsetX, j + offsetY);

                         float z_value = (float)point.Z;

                         var X1 = (float)(point.X - _viewport.X) / _viewport.Width * ClientRectangle.Width - Padding.Left;
                         var Y1 = (float)(point.Y - _viewport.Y) / _viewport.Height * ClientRectangle.Height - Padding.Top;

                        var pt = new Point3F(X1,Y1,z_value);
                        
                        pts[i, j] = pt;
                     }

                 }

                var min = (float) _grid.GetzMin();
                var max = (float) _grid.GetzMax();

                _palette = ColorPalette.GetTerrain(min,max);

               
                DrawContour(graphics, pts, min, max, _contoursCount);
                DrawColor(graphics, pts, min, max);
                DrawBodies(graphics);
                /*
                int stepx = (this.ClientRectangle.Width - 2 * this.Padding.All) / _viewport.Width;
                int stepy = (this.ClientRectangle.Height - 2 * this.Padding.All) / _viewport.Height;

                for (int i = _viewport.X; i < _viewport.Width; i++)
                {
                    graphics.DrawString(i.ToString("0"), this.Font, Brushes.Black,
                        this.Padding.All + (i - _viewport.X) * stepx,
                        this.ClientRectangle.Height - this.Padding.All * 0.75f);

                }

                using (var format = new StringFormat(StringFormatFlags.DirectionRightToLeft)
                    {Alignment = StringAlignment.Far})
                {
                    // graphics.TranslateTransform(this.ClientRectangle.Width,0);
                    // graphics.RotateTransform(90);

                    for (int i = _viewport.Y; i < _viewport.Height; i++)
                    {
                        graphics.DrawString(i.ToString("0.0"), this.Font, Brushes.Black,
                            this.Padding.All * 0.25f - this.Font.Size * 0.5f
                            , this.ClientRectangle.Height - this.Padding.All - (i - _viewport.Y) * stepy, format);
                    }

                    graphics.ResetTransform();
                }*/

                var edges = new PointF[4];

                var Xmin = (_grid.GetxMin() - _viewport.X) * ClientRectangle.Width / _viewport.Width - Padding.Left;
                var Xmax = (_grid.GetxMax() - _viewport.X) * ClientRectangle.Width / _viewport.Width - Padding.Left;

                var Ymin = (_grid.GetyMin() - _viewport.Y) * ClientRectangle.Height / _viewport.Height - Padding.Top;
                var Ymax = (_grid.GetyMax() - _viewport.Y) * ClientRectangle.Height / _viewport.Height - Padding.Top;

                edges[0] = new PointF((float)Xmin, (float)Ymin);
                edges[1] = new PointF((float)Xmin, (float)Ymax);
                edges[2] = new PointF((float)Xmax, (float)Ymax);
                edges[3] = new PointF((float)Xmax, (float)Ymin);

                var pen = new Pen(Color.Black, 1.5f);

                graphics.DrawPolygon(pen,edges);

                float dX = 0, dY = 0;

                if(_labelsCountX > 0)
                    dX = (float)((Xmax - Xmin) / _labelsCountX);
                if(_labelsCountY > 0)
                    dY = (float)((Ymax - Ymin) / _labelsCountY);

                var lenX = _grid.GetxMax() - _grid.GetxMin();
                var lenY = _grid.GetyMax() - _grid.GetyMin();

                for (int i = 0; i <= _labelsCountX; i++)
                {
                    var down = new PointF((float) Xmin + i * dX, (float) Ymax + dY / 5);
                    var up = new PointF((float)Xmin + i * dX, (float)Ymin - dY / 5);

                    var x = _grid.GetxMin() + i * lenX / _labelsCountX;

                    graphics.DrawString(x.ToString("0.0"), this.Font, Brushes.Black, up.X,up.Y - this.Font.Height);
                    graphics.DrawString(x.ToString("0.0"), this.Font, Brushes.Black, down);
                    graphics.DrawLine(pen,up,down);
                }

                for (int i = 0; i <= _labelsCountY; i++)
                {
                    var left = new PointF((float)Xmin - dX / 5, (float)Ymin + i * dY);
                    var right = new PointF((float)Xmax + dX / 5, (float)Ymin + i * dY);

                    var y = _grid.GetyMin() + i * lenY / _labelsCountY;

                    var format = new StringFormat(StringFormatFlags.DirectionRightToLeft);

                    graphics.DrawString(y.ToString("0.0"), this.Font, Brushes.Black, left.X - this.Font.Size, left.Y, format);
                    graphics.DrawString(y.ToString("0.0"), this.Font, Brushes.Black, right);

                    graphics.DrawLine(pen, left, right);
                    DrawProfile(graphics);
                }
            }

            ControlPaint.DrawBorder(graphics, ClientRectangle, SystemColors.ActiveBorder, ButtonBorderStyle.Solid);
            
            base.OnPaint(e);
        }

        private struct Point3F
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }


            public Point3F(float x, float y, float z) : this()
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }

        }

        private static void DrawContour(Graphics g, Point3F[,] pts, float zmin, float zmax, int ncount)
        {
            using (var aPen = new Pen(Color.DarkGray) {Width = 0.25f})
            {
                var pta = new PointF[2];


                var zlevels = new float[ncount];
                for (int i = 0; i < ncount; i++)
                {
                    zlevels[i] = zmin + i * (zmax - zmin) / (ncount - 1);
                }

                int i0, i1, i2, j0, j1, j2;
                float zratio = 1; // Draw contour on the XY plane:
                for (int i = 0; i < pts.GetLength(0) - 1; i++)
                {
                    for (int j = 0; j < pts.GetLength(1) - 1; j++)
                    {
                        if (float.IsNaN(pts[i, j].Z)) continue;

                        for (int k = 0; k < ncount; k++)
                        {
                            // Left triangle:
                            i0 = i;
                            j0 = j;
                            i1 = i;
                            j1 = j + 1;
                            i2 = i + 1;
                            j2 = j + 1;
                            if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i1, j1].Z ||
                                 zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i1, j1].Z) &&
                                (zlevels[k] >= pts[i1, j1].Z && zlevels[k] < pts[i2, j2].Z ||
                                 zlevels[k] < pts[i1, j1].Z && zlevels[k] >= pts[i2, j2].Z))
                            {
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i1, j1].Z - pts[i0, j0].Z);
                                pta[0] =
                                    new PointF(pts[i0, j0].X, (1 - zratio) * pts[i0, j0].Y + zratio * pts[i1, j1].Y);
                                zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                                pta[1] =
                                    new PointF((1 - zratio) * pts[i1, j1].X + zratio * pts[i2, j2].X, pts[i1, j1].Y);
                                g.DrawLine(aPen, pta[0], pta[1]);
                            }
                            else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i2, j2].Z ||
                                      zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i2, j2].Z) &&
                                     (zlevels[k] >= pts[i1, j1].Z && zlevels[k] < pts[i2, j2].Z ||
                                      zlevels[k] < pts[i1, j1].Z && zlevels[k] >= pts[i2, j2].Z))
                            {
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i2, j2].Z - pts[i0, j0].Z);
                                pta[0] =
                                    new PointF((1 - zratio) * pts[i0, j0].X + zratio * pts[i2, j2].X,
                                        (1 - zratio) * pts[i0, j0].Y + zratio * pts[i2, j2].Y);
                                zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                                pta[1] =
                                    new PointF((1 - zratio) * pts[i1, j1].X + zratio * pts[i2, j2].X, pts[i1, j1].Y);
                                g.DrawLine(aPen, pta[0], pta[1]);
                            }
                            else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i1, j1].Z ||
                                      zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i1, j1].Z) &&
                                     (zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i2, j2].Z ||
                                      zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i2, j2].Z))
                            {
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i1, j1].Z - pts[i0, j0].Z);
                                pta[0] =
                                    new PointF(pts[i0, j0].X, (1 - zratio) * pts[i0, j0].Y + zratio * pts[i1, j1].Y)
                                    ;
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i2, j2].Z - pts[i0, j0].Z);
                                pta[1] =
                                    new PointF(pts[i0, j0].X * (1 - zratio) + pts[i2, j2].X * zratio,
                                        pts[i0, j0].Y * (1 - zratio) + pts[i2, j2].Y * zratio);
                                g.DrawLine(aPen, pta[0], pta[1]);
                            } // right triangle:

                            i0 = i;
                            j0 = j;
                            i1 = i + 1;
                            j1 = j;
                            i2 = i + 1;
                            j2 = j + 1;
                            if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i1, j1].Z ||
                                 zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i1, j1].Z) &&
                                (zlevels[k] >= pts[i1, j1].Z && zlevels[k] < pts[i2, j2].Z ||
                                 zlevels[k] < pts[i1, j1].Z && zlevels[k] >= pts[i2, j2].Z))
                            {
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i1, j1].Z - pts[i0, j0].Z);
                                pta[0] =
                                    new PointF(pts[i0, j0].X * (1 - zratio) + pts[i1, j1].X * zratio, pts[i0, j0].Y);
                                zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                                pta[1] =
                                    new PointF(pts[i1, j1].X, pts[i1, j1].Y * (1 - zratio) + pts[i2, j2].Y * zratio);
                                g.DrawLine(aPen, pta[0], pta[1]);
                            }
                            else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i2, j2].Z ||
                                      zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i2, j2].Z) &&
                                     (zlevels[k] >= pts[i1, j1].Z && zlevels[k] < pts[i2, j2].Z ||
                                      zlevels[k] < pts[i1, j1].Z && zlevels[k] >= pts[i2, j2].Z))
                            {
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i2, j2].Z - pts[i0, j0].Z);
                                pta[0] =
                                    new PointF(pts[i0, j0].X * (1 - zratio) + pts[i2, j2].X * zratio,
                                        pts[i0, j0].Y * (1 - zratio) + pts[i2, j2].Y * zratio);
                                zratio = (zlevels[k] - pts[i1, j1].Z) / (pts[i2, j2].Z - pts[i1, j1].Z);
                                pta[1] =
                                    new PointF(pts[i1, j1].X, pts[i1, j1].Y * (1 - zratio) + pts[i2, j2].Y * zratio);
                                g.DrawLine(aPen, pta[0], pta[1]);
                            }
                            else if ((zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i1, j1].Z ||
                                      zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i1, j1].Z) &&
                                     (zlevels[k] >= pts[i0, j0].Z && zlevels[k] < pts[i2, j2].Z ||
                                      zlevels[k] < pts[i0, j0].Z && zlevels[k] >= pts[i2, j2].Z))
                            {
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i1, j1].Z - pts[i0, j0].Z);
                                pta[0] =
                                    new PointF(pts[i0, j0].X * (1 - zratio) + pts[i1, j1].X * zratio, pts[i0, j0].Y);
                                zratio = (zlevels[k] - pts[i0, j0].Z) / (pts[i2, j2].Z - pts[i0, j0].Z);
                                pta[1] =
                                    new PointF(pts[i0, j0].X * (1 - zratio) + pts[i2, j2].X * zratio,
                                        pts[i0, j0].Y * (1 - zratio) + pts[i2, j2].Y * zratio);
                                g.DrawLine(aPen, pta[0], pta[1]);
                            }
                        }
                    }
                }
            }
        }

        private static Color GetColor(float value, float maxValue, float minValue)
        {
            if (float.IsNaN(value))
            {
                return Color.WhiteSmoke;
            }

            // Convert into a value between 0 and 1023.
            int int_value = (int) (1023 * (value - maxValue) / (minValue - maxValue));
            if (int_value < 0 || int_value > 1023) int_value = 1023 / 2;
            // Map different color bands.
            if (int_value < 256)
            {
                // Red to yellow. (255, 0, 0) to (255, 255, 0).
                return Color.FromArgb(255, int_value, 0);
            }
            else if (int_value < 512)
            {
                // Yellow to green. (255, 255, 0) to (0, 255, 0).
                int_value -= 256;
                return Color.FromArgb(255 - int_value, 255, 0);
            }
            else if (int_value < 768)
            {
                // Green to aqua. (0, 255, 0) to (0, 255, 255).
                int_value -= 512;
                return Color.FromArgb(0, 255, int_value);
            }
            else
            {
                // Aqua to blue. (0, 255, 255) to (0, 0, 255).
                int_value -= 768;
                return Color.FromArgb(0, 255 - int_value, 255);
            }
        }

        private void DrawColor(Graphics g, Point3F[,] pts, float zmin, float zmax)
        {
            var pta = new PointF[4];

            for (int i = 0; i < pts.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < pts.GetLength(1) - 1; j++)
                {
                    pta[0] = new PointF(pts[i, j].X, pts[i, j].Y);
                    pta[1] = new PointF(pts[i, j + 1].X, pts[i, j + 1].Y);
                    pta[2] = new PointF(pts[i + 1, j + 1].X, pts[i + 1, j + 1].Y);
                    pta[3] = new PointF(pts[i + 1, j].X, pts[i + 1, j].Y);
                    using (var aBrush = new SolidBrush(Color.FromArgb(200, _palette.GetColor(pts[i, j].Z))))
                    {
                        g.FillPolygon(aBrush, pta);
                    }
                }
            }
        }

        private void DrawBodies(Graphics g)
        {
            PointF[] points = new PointF[8];

            foreach (var body in Global.bodies)
            {
                for (int i = 0; i < 8; i++)
                {
                    var nX = (float)((body.Verticles[i].X - _viewport.X) / _viewport.Width * ClientRectangle.Width -
                             Padding.Left);
                    var nY = (float)((body.Verticles[i].Y - _viewport.Y) / _viewport.Height * ClientRectangle.Height -
                                     Padding.Top);

                    points[i] = new PointF(nX , nY);
                }

                using (var Pen = new Pen(Color.Black, 1.5f))
                {
                    if(Global.bodies.IndexOf(body) == Global.SelectedBodyIndex)
                        Pen.Color = Color.Blue;;

                    SolidBrush brush = new SolidBrush(Color.FromArgb(127,127,127,127));

                    g.FillPolygon(brush, new PointF[] { points[4], points[5], points[7], points[6] });
                    
                    g.DrawPolygon(Pen, new PointF[] { points[4], points[5], points[7], points[6] });

                    g.DrawPolygon(Pen, new PointF[] {points[0], points[1], points[3], points[2]});

                    
                    

                    g.DrawLine(Pen, points[0], points[4]);
                    g.DrawLine(Pen, points[1], points[5]);
                    g.DrawLine(Pen, points[2], points[6]);
                    g.DrawLine(Pen, points[3], points[7]);
                }
            }
        }

        private void DrawProfile(Graphics g)
        {

            var p1 = new PointF(
                (float) ((_start.x - _viewport.X) * ClientRectangle.Width / _viewport.Width - Padding.Left), (float)((_start.y - _viewport.Y) * ClientRectangle.Height / _viewport.Height - Padding.Top));
            var p2 = new PointF(
                (float)((_end.x - _viewport.X) * ClientRectangle.Width / _viewport.Width - Padding.Left), (float)((_end.y - _viewport.Y) * ClientRectangle.Height / _viewport.Height - Padding.Top));

            Pen pen = new Pen(Color.Red, 2.5f);

            g.DrawLine(pen, p1,p2);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            var h_min = (float)(_grid.Y[1] - _grid.Y[0])*4;
            
            ViewHeight -= (int)(e.Delta / Math.Abs(e.Delta) * h_min);

            base.OnMouseWheel(e);
        }

        private void plot_Load(object sender, EventArgs e)
        {
            SetStyle(
                ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);
            //_palette = new ColorPalette(new [] {Color.BlueViolet, Color.Red },new []{250d,230d});
        }

        private void plot_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _viewXmin -= (e.X - _Xlast)/2;
                _viewYmin -= (e.Y - _ylast)/2;
            }
            UpdateViewport();

            _Xlast = e.X;
            _ylast = e.Y;
        }
    }

    public struct ColorPalette
    {
        private Color[] _colors;

        public Color[] Colors
        {
            get => _colors;
            private set => _colors = value;
        }

        public double[] Values
        {
            get => _values;
            private set => _values = value;
        }

        private double[] _values;

        public ColorPalette(Color[] colors, double[] values)
        {
            if (colors.Length != values.Length)
                throw new ArgumentException("Wrong arrays: the lengths of both arrays must be equal");

            _colors = colors;
            _values = values.OrderBy(x => x).ToArray();
        }

        public Color GetColor(double value)
        {
            for (int i = 0; i < Values.Length-1; i++)
            {
                if (value > Values[i] && value < Values[i + 1])
                {
                    var k = 1 / (Values[i + 1] - Values[i]) * (value - Values[i]);

                    Color output = Color.FromArgb(255,
                        (int) ((Colors[i + 1].R - Colors[i].R) * k  + Colors[i].R ),
                        (int)Math.Abs((Colors[i + 1].G - Colors[i].G) * k + Colors[i].G),
                        (int)Math.Abs((Colors[i + 1].B - Colors[i].B) * k + Colors[i].B));

                    return output;
                }

                if (value <= Values[0])
                    return Colors[0];
                if (value >= Values[Values.Length-1])
                    return Colors[Values.Length - 1];
            }


            return Color.Black;
        }

        public static ColorPalette GetRainbow(double min, double max)
        {
            var values = new double[6];

            for (int i = 0; i < values.Length; i++)
                values[i] = min + (max - min) / (values.Length - 1) * i;

            return new ColorPalette(
                new[] { Color.MediumPurple, Color.Blue, Color.LimeGreen, Color.Yellow, Color.Orange, Color.Red },
                values);
        }

        public static ColorPalette GetTerrain(double min, double max)
        {
            var values = new double[21];

            for (int i = 0; i < values.Length; i++)
                values[i] = min + (max - min) / (values.Length - 1) * i;

            return new ColorPalette(
                new[]
                {
                    Color.FromArgb(255,200,215,133), Color.FromArgb(255, 171, 217, 177), Color.FromArgb(255, 124, 196, 120), Color.FromArgb(255, 117, 193, 120),
                    Color.FromArgb(255,175,204,166), Color.FromArgb(255,219,208,78), Color.FromArgb(255,241,207,14), Color.FromArgb(255,242,167,0),
                    Color.FromArgb(255,192,159,13), Color.FromArgb(255,211,192,112), Color.FromArgb(255,240,219,161), Color.FromArgb(255,252,236,192),
                    Color.FromArgb(255,248,250,234), Color.FromArgb(255,229,254,250), Color.FromArgb(255,219,255,253),Color.FromArgb(255,214,251,252),
                    Color.FromArgb(255,183,244,247),Color.FromArgb(255,115,224,241),Color.FromArgb(255,29,188,239), Color.FromArgb(255,0,137,245),
                    Color.FromArgb(255,0,80,250)},
                values);
        }
    }

}
