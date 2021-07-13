using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ploting
{
    public partial class plot : UserControl
    {
        private Rectangle _area = new Rectangle(-10, -10, 20, 20);

        private Rectangle _viewport;
        
        private int _viewXmin = 0;
        private int _viewYmin = 0;
        private int _viewHeight;
        private int _contoursCount = 10;

        private GRD _grid;
        

        public plot()
        {
            InitializeComponent();
        }


        public bool DeferUpdate { get; set; }
        public Rectangle Area
        {
            get { return _area; }
            set
            {
                _area = value;

                Invalidate();
            }
        }

        public int ContoursCount
        {
            get => _contoursCount;
            set
            {
                _contoursCount = value;
                UpdateViewport();
                Refresh();
            }
        }

        public int ViewHeight
        {
            get => _viewport.Height;
            set
            {
                _viewHeight = (int)MathF.Max(0,value);
                UpdateViewport();
                Refresh();
            }
        }

        public int ViewXmin
        {
            get => _viewXmin;
            set
            {
                _viewXmin = value;
                UpdateViewport();
                Refresh();
            }
        }

        public int ViewYmin
        {
            get => _viewYmin;
            set
            {
                _viewYmin = value;
                UpdateViewport();
                Refresh();
            }
        }



        public GRD Grid
        {
            get => _grid;
            set
            {
                _grid = value;
                UpdateViewport();
                Refresh();
            }
        }

        private void UpdateViewport()
        {
            float aspectRatio = (float)(ClientRectangle.Width - Padding.Horizontal) / (float)(ClientRectangle.Height - Padding.Vertical);
            _viewport = new Rectangle(_viewXmin, _viewYmin, (int)(aspectRatio * _viewHeight), _viewHeight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if(DeferUpdate) return;
            
            var graphics = e.Graphics;

            graphics.Clear(Color.WhiteSmoke);

            if (_grid != null && !DesignMode && Enabled)
            {
                 var Xs = _grid.X.Where(x =>
                 {
                     return (x > _viewXmin && x < _viewXmin + _viewport.Width);
                 }).OrderBy(x => x).ToArray();
                 var Ys = _grid.Y.Where(y =>
                 {
                     return (y > _viewYmin && y < _viewYmin + _viewport.Height);
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
                         float z_value = (float)_grid.GetCoordinates(i + offsetX, j + offsetY).Z;

                         var pt = new Point3F(this.Padding.All + i * step, this.Padding.All + j * step,z_value);

                         if (i == n - 1)
                             pt.X = this.ClientRectangle.Width - this.Padding.All;
                         if (j == m - 1)
                             pt.Y = this.ClientRectangle.Height - this.Padding.All;
                         pts[i, j] = pt;
                     }

                 }
                /*
                var step = (int)((ClientRectangle.Height - Padding.Vertical) * 0.01);

                var n = (ClientRectangle.Width - Padding.Horizontal) / step;
                var m = (ClientRectangle.Height - Padding.Vertical) / step;

                var pts = new Point3F[n, m];
                
                var dx = _viewport.Width / n;
                var dy = _viewport.Height / m;

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        float z_value = (float)_grid.Interp(0, new Vector2(_viewport.X + i * dx, _viewport.Y + j * dy));

                        var pt = new Point3F(this.Padding.All + i * step, this.Padding.All + j * step, z_value);

                        if (i == n - 1)
                            pt.X = this.ClientRectangle.Width - this.Padding.All;
                        if (j == m - 1)
                            pt.Y = this.ClientRectangle.Height - this.Padding.All;
                        pts[i, j] = pt;
                    }

                }*/

                var min = (float) _grid.GetzMin();
                var max = (float) _grid.GetzMax();

                DrawContour(graphics, pts, min, max, 15);
                DrawColor(graphics, pts, min, max);
                
              /*  var rect = this.ClientRectangle;

                rect.Inflate(-this.Padding.All + 1, -this.Padding.All + 1);

                ControlPaint.DrawBorder(graphics, rect, SystemColors.ControlDarkDark, ButtonBorderStyle.Dashed);

                int stepx = (this.ClientRectangle.Width - 2 * this.Padding.All) / _viewport.Width;
                int stepy = (this.ClientRectangle.Height - 2 * this.Padding.All) / _viewport.Height;

                for (int i = _viewport.X; i < _viewport.Width; i++)
                {
                    graphics.DrawString(i.ToString("0"), this.Font, Brushes.Black,
                        this.Padding.All + (i - _area.X) * stepx,
                        this.ClientRectangle.Height - this.Padding.All * 0.75f);

                }

                using (var format = new StringFormat(StringFormatFlags.DirectionRightToLeft)
                    {Alignment = StringAlignment.Far})
                {
                    // graphics.TranslateTransform(this.ClientRectangle.Width,0);
                    // graphics.RotateTransform(90);

                    for (int i = _area.Y; i < _area.Height; i++)
                    {
                        graphics.DrawString(i.ToString("0.0"), this.Font, Brushes.Black,
                            this.Padding.All * 0.25f - this.Font.Size * 0.5f
                            , this.ClientRectangle.Height - this.Padding.All - (i - _area.Y) * stepy, format);
                    }

                    graphics.ResetTransform();
                }*/
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
            using (var aPen = new Pen(Color.DimGray) {Width = 0.25f})
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

        private static void DrawColor(Graphics g, Point3F[,] pts, float zmin, float zmax)
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
                    using (var aBrush = new SolidBrush(Color.FromArgb(200, GetColor(pts[i, j].Z, zmin, zmax))))
                    {
                        g.FillPolygon(aBrush, pta);
                    }
                }
            }
        }

        private void plot_Load(object sender, EventArgs e)
        {
            SetStyle(
                ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);
        }
    }
}
