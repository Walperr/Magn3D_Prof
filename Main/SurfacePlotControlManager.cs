using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GRIDs;
using OpenControls.Wpf.SurfacePlot;
using OpenControls.Wpf.SurfacePlot.Common;
using OpenControls.Wpf.SurfacePlot.Model;
using OpenTK;
using Configuration = OpenControls.Wpf.SurfacePlot.Model.Configuration;
using Vector2 = Vectors.Vector2;

namespace Magn3D_Prof.Main
{
    public class SurfacePlotControlManager : ISurfacePlotControlManager
    {
        private IGrid _lowQualitySurface;
        private IGrid _originalSurface;

        private int _lowQualitySurfaceResolution = 40000;

        private readonly SurfacePlotControl _surfacePlotControl = new SurfacePlotControl();
        private readonly PropertyGrid _propertyGrid = new PropertyGrid() {Dock = DockStyle.Fill};

        private IConfiguration _configuration;
        private IConfiguration _configurationInfo;
        private Control _surfacePlotParentControl;
        private Control _propertyGridParentControl;

        public int LowQualitySurfaceResolution
        {
            get => _lowQualitySurfaceResolution;
            set
            {
                _lowQualitySurfaceResolution = value;

                RefreshDisplaySurface();
            }
        }

        public object OriginalSurface
        {
            get => _originalSurface;
            set
            {
                if(value is not IGrid surface)
                    return;
                
                _originalSurface = surface;

                RefreshDisplaySurface();
            }
        }

        public ObservableCollection<Prismbody> Bodies { get; } = new ObservableCollection<Prismbody>();

        public Color Background => _configuration.BackgroundColour;

        public Control SurfacePlotParentControl
        {
            get => _surfacePlotParentControl;
            set
            {
                var oldValue = _surfacePlotParentControl;

                if (oldValue != null)
                    oldValue.ClientSizeChanged -= SurfacePlotParentControlOnClientSizeChanged;

                oldValue?.Controls.Remove(_surfacePlotControl);

                _surfacePlotParentControl = value;

                if (_surfacePlotParentControl == null) return;

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
                    _configuration.ConfigurationChanged -= ConfigurationInfoOnConfigurationChanged; 
                    _propertyGridParentControl = value;
                    _configuration.ConfigurationChanged += ConfigurationInfoOnConfigurationChanged;
                    _propertyGridParentControl.Controls.Add(_propertyGrid);
                }
            }
        }

        private void ConfigurationInfoOnConfigurationChanged(ConfigurationItem configurationitem)
        {
            _propertyGrid.Refresh();
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
            _configuration.ViewProjection = ViewProjection.Top;

            _surfacePlotControl.Initialise(_configuration);

            //Initialize propertyGrid

            _configurationInfo = new ConfigurationPropertiesInfo(_configuration);

            _propertyGrid.PropertySort = PropertySort.Categorized;
            _propertyGrid.SelectedObject = _configurationInfo;
            _propertyGrid.HelpVisible = false;
            _propertyGrid.ToolbarVisible = false;
            _propertyGrid.CommandsVisibleIfAvailable = false;
            
            //Initialize ObservaibleCollection

            Bodies.CollectionChanged += (sender, args) =>
            {
                ApplyBodiesToControl();
            };
        }

        public void Dispose()
        {
            _surfacePlotControl.Parent.ClientSizeChanged -= SurfacePlotParentControlOnClientSizeChanged;
            _surfacePlotControl?.Dispose();
            _propertyGrid?.Dispose();
            _configuration = null;
            _configurationInfo = null;
        }

        private void SurfacePlotParentControlOnClientSizeChanged(object sender, EventArgs e)
        {
            _surfacePlotControl.SetBounds(
                ((Control) sender).ClientRectangle.X,
                ((Control) sender).ClientRectangle.Y,
                ((Control) sender).ClientRectangle.Width,
                ((Control) sender).ClientRectangle.Height);
        }

        private void RefreshDisplaySurface()
        {
            if (_originalSurface == null) return;

            if (_originalSurface.Z.Count <= _lowQualitySurfaceResolution)
            {
                _lowQualitySurface = new GRD(_originalSurface.Zmin, _originalSurface.Zmax, _originalSurface.X.ToArray(),
                    _originalSurface.Y.ToArray(), _originalSurface.Z.ToArray());
            }
            else
            {
                var xCount = (int)Math.Sqrt(_lowQualitySurfaceResolution * _originalSurface.X.Count /
                                             (float)_originalSurface.Y.Count);
                
                var yCount = (int)Math.Sqrt(_lowQualitySurfaceResolution * _originalSurface.Y.Count /
                                            (float)_originalSurface.X.Count);

                var dx = (_originalSurface.Xmax - _originalSurface.Xmin) / (xCount - 1);
                var dy = (_originalSurface.Ymax - _originalSurface.Ymin) / (yCount - 1);

                List<double> xValues = Enumerable.Repeat(_originalSurface.Xmin, xCount)
                    .Select((e, i) => e + (i) * dx).ToList();
                List<double> yValues = Enumerable.Repeat(_originalSurface.Ymin, yCount)
                    .Select((e, i) => e + (i) * dy).ToList();
                List<Vector2> points = new List<Vector2>();

                for (int j = 0; j < yCount; j++)
                for (int i = 0; i < xCount; i++)
                    points.Add(new Vector2(xValues[i], yValues[j]));

                var zValues = _originalSurface.Interp(0, points).Select(x => x / 1000);

                var zMax = zValues.Where(z => !double.IsNaN(z)).Max();
                var zMin = zValues.Where(z => !double.IsNaN(z)).Min();

                _lowQualitySurface = new GRD(zMin, zMax, xValues.ToArray(), yValues.ToArray(), zValues.ToArray());
            }
         
            ApplySurfaceDataToControl();
        }

        private void ApplySurfaceDataToControl()
        {
            List<List<float>> drawData = new List<List<float>>();

            for (int i = 0; i < _lowQualitySurface.X.Count; ++i)
            {
                List<float> list = new List<float>();
                drawData.Add(list);
                for (int j = 0; j < _lowQualitySurface.Y.Count; ++j)
                {
                    list.Add((float) _lowQualitySurface.GetCoordinates(i, j).Z);
                }
            }

            float zMin = (float) _lowQualitySurface.Z.Where(z => !double.IsNaN(z)).Min();
            float zMax = (float) _lowQualitySurface.Z.Where(z => !double.IsNaN(z)).Max();

            float xMin = (float) _lowQualitySurface.Xmin;
            float yMin = (float) _lowQualitySurface.Ymin;

            float xMax = (float) _lowQualitySurface.Xmax;
            float yMax = (float) _lowQualitySurface.Ymax;

            _configurationInfo.MaximumLevel = (short) zMax;
            _configurationInfo.MinimumLevel = (short) zMin;
            
            _surfacePlotControl.SetData(drawData, xMin, xMax, 21, yMin, yMax, 21, zMin, zMax, 21);
        }

        private void ApplyBodiesToControl()
        {
            _surfacePlotControl.ClearBodies();

            foreach (var prismbody in Bodies)
            {
                var body = prismbody.Vertices.Select(
                    vert => new Vector3((float) vert.X, (float) vert.Y, (float) vert.Z)).ToArray();

                _surfacePlotControl.AddBodies(new[] {body});
            }
        }
    }
}