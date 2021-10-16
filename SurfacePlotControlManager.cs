using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GRIDs;
using OpenControls.Wpf.SurfacePlot.Model;
using OpenControls.Wpf.SurfacePlot;
using Vectors;

namespace test
{
    public class SurfacePlotControlManager : IDisposable
    {
        private IGrid _lowQualitySurface;
        private IGrid _originalSurface;
        
        private int _lowQualitySurfaceResolutionX = 100;
        private int _lowQualitySurfaceResolutionY = 100;
        
        private SurfacePlotControl _surfacePlotControl = new SurfacePlotControl();
        private PropertyGrid _propertyGrid = new PropertyGrid() {Dock = DockStyle.Fill};
        
        private IConfiguration _configuration;
        private IConfiguration _configurationInfo;
        private Control _surfacePlotParentControl;
        private Control _propertyGridParentControl;

        public int LowQualitySurfaceResolutionX
        {
            get => _lowQualitySurfaceResolutionX;
            set
            {
                _lowQualitySurfaceResolutionX = value;
                
                RefreshDisplaySurface();
            }
        }
        
        public int LowQualitySurfaceResolutionY
        {
            get => _lowQualitySurfaceResolutionY;
            set
            {
                _lowQualitySurfaceResolutionY = value;
                
                RefreshDisplaySurface();
            }
        }

        public IGrid OriginalSurface
        {
            get => _originalSurface;
            set
            {
                _originalSurface = value;
                
                RefreshDisplaySurface();
            }
        }

        public Control SurfacePlotParentControl
        {
            get => _surfacePlotParentControl;
            set
            {
                var oldValue = _surfacePlotParentControl;

                if(oldValue != null)
                    oldValue.ClientSizeChanged -= SurfacePlotParentControlOnClientSizeChanged;

                oldValue?.Controls.Remove(_surfacePlotControl);
                
                _surfacePlotParentControl = value;
                
                if(_surfacePlotParentControl == null) return;
                
                _surfacePlotParentControl.Controls.Add(_surfacePlotControl);
                _surfacePlotParentControl.ClientSizeChanged += SurfacePlotParentControlOnClientSizeChanged;
                
                _surfacePlotControl.SetBounds(
                    _surfacePlotParentControl.ClientRectangle.X,
                    _surfacePlotParentControl.ClientRectangle.Y,
                    _surfacePlotParentControl.ClientRectangle.Width,
                    _surfacePlotParentControl.ClientRectangle.Height);
            }
        }

        public Control PropertyGridParentControl
        {
            get => _propertyGridParentControl;
            set
            {
                {
                    _propertyGridParentControl?.Controls.Remove(_propertyGrid);
                    _propertyGridParentControl = value;
                    _propertyGridParentControl.Controls.Add(_propertyGrid);
                }
            }
        }

        public SurfacePlotControlManager(Control parentControl)
        {
            SurfacePlotParentControl = parentControl;
            
            //Initialize configuration
            
            _configuration = new Configuration();
            
            _configuration.BackgroundColour = Color.DarkGray;
            _configuration.TransparentLabelBackground = true;
            _configuration.ShowGrid = false;
            _configuration.ColorBarSelector = ColorBarsLoader.ColorBars.Terrain;
            _configuration.ShadingAlgorithm = ShadingAlgorithm.ColorBar;
            
            _surfacePlotControl.Initialise(_configuration);
            
            //Initialize propertyGrid
            
            _configurationInfo = new ConfigurationPropertiesInfo(_configuration);

            _propertyGrid.PropertySort = PropertySort.Categorized;
            _propertyGrid.SelectedObject = _configurationInfo;
        }

        public void Dispose()
        {
            _surfacePlotControl.Parent.ClientSizeChanged -= SurfacePlotParentControlOnClientSizeChanged;
            _surfacePlotControl?.Dispose();
            _propertyGrid?.Dispose();
        }

        private void SurfacePlotParentControlOnClientSizeChanged(object sender, EventArgs e)
        {
            _surfacePlotControl.SetBounds(
                ((Control)sender).ClientRectangle.X,
                ((Control)sender).ClientRectangle.Y,
                ((Control)sender).ClientRectangle.Width,
                ((Control)sender).ClientRectangle.Height);
        }

        private void RefreshDisplaySurface()
        {
            if (_originalSurface == null) return;

            double zMax;
            double zMin;
            
            if (_originalSurface.X.Count * _originalSurface.Y.Count <=
                _lowQualitySurfaceResolutionX * _lowQualitySurfaceResolutionY)
            {
                _lowQualitySurface = _originalSurface;
            }
            else
            {
                var dx = (_originalSurface.Xmax - _originalSurface.Xmin) / (_lowQualitySurfaceResolutionX + 1);
                var dy = (_originalSurface.Ymax - _originalSurface.Ymin) / (_lowQualitySurfaceResolutionY + 1);

                List<double> xValues = Enumerable.Repeat(_originalSurface.Xmin, _lowQualitySurfaceResolutionX)
                    .Select((e, i) => e + (i) * dx).ToList();
                List<double> yValues = Enumerable.Repeat(_originalSurface.Ymin, _lowQualitySurfaceResolutionY)
                    .Select((e, i) => e + (i) * dy).ToList();
                List<Vector2> points = new List<Vector2>();

                for (int i = 0; i < _lowQualitySurfaceResolutionX; i++)
                for (int j = 0; j < _lowQualitySurfaceResolutionY; j++)
                    points.Add(new Vector2(xValues[i], yValues[j]));

                var zValues = _originalSurface.Interp(0, points);

                zMax = zValues.Max();
                zMin = zValues.Min();

                _lowQualitySurface = new GRD(zMin, zMax, xValues.ToArray(), yValues.ToArray(), zValues.ToArray());
            }

            var xCount = Math.Min(_lowQualitySurfaceResolutionX, _lowQualitySurface.X.Count);
            var yCount = Math.Min(_lowQualitySurfaceResolutionY, _lowQualitySurface.Y.Count);

            List<List<float>> drawData = new List<List<float>>();
            
            for (int i = 0; i < xCount; ++i)
            {
                List<float> list = new List<float>();
                drawData.Add(list);
                for (int j = 0; j < yCount; ++j)
                {
                    list.Add((float)_lowQualitySurface.GetCoordinates(i,j).Z);
                }
            }

            zMin = _lowQualitySurface.Z.Min();
            zMax = _lowQualitySurface.Z.Max();

            float xMin = (float) _lowQualitySurface.Xmin;
            float yMin = (float) _lowQualitySurface.Ymin;
            
            float xMax = (float) _lowQualitySurface.Xmax;
            float yMax = (float) _lowQualitySurface.Ymax;

            _configuration.MaximumLevel = (short)zMax;
            _configuration.MinimumLevel = (short)zMin;
            
            _surfacePlotControl.SetData(drawData, xMin, xMax, 21, yMin, yMax, 21, (float)zMin, (float)zMax, 21);
        }
    }
}