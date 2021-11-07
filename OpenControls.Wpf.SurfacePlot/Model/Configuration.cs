using System.Drawing;

namespace OpenControls.Wpf.SurfacePlot.Model
{
    public class Configuration : IConfiguration
    {
        public Configuration()
        {
            Zoom = 100;
            ShadingAlgorithm = ShadingAlgorithm.FixedLevels;
            MinimumLevel = -64;
            MaximumLevel = 2048;
            Perspective = 1.0f;
            BackgroundColour = Color.Black;
            ShowScatterPlot = false;
            ShowShading = true;
            ShadingMethod = ShadingMethod.Interpolated;
            LabelFontSize = 15;
            LabelColour = Color.White;
            ShowLabels = true;
            XYLabelPosition = XYLabelPosition.Bottom;
            GridColour = Color.White;
            ShowGrid = true;
            FrameColour = Color.White;
            ShowAxes = true;
            ShowAxesTitles = true;
            ShowZBar = true;
            ShowFrame = true;
            Hold = false;
            HoldMaximum = true;
            ViewProjection = ViewProjection.ThreeDimensional;
        }

        #region IRawDataConfiguration

       public event ConfigurationChangedEventHandler ConfigurationChanged;

        private int _zoom;
        public int Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                // if (value > MaximumZoom)
                // {
                //     value = MaximumZoom;
                // }
                // else if (value < MinimumZoom)
                // {
                //     value = MinimumZoom;
                // }
                _zoom = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.Zoom);
            }
        }

        public int MaximumZoom
        {
            get
            {
                return 10000;
            }
        }

        public int MinimumZoom
        {
            get
            {
                return -1;
            }
        }

        private double _zScale = 0.02;
        public double ZScale
        {
            get
            {
                return _zScale;
            }
            set
            {
                _zScale = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.ZScale);
            }
        }

        private Color _backgroundColour;
        public Color BackgroundColour
        {
            get
            {
                return _backgroundColour;
            }
            set
            {
                _backgroundColour = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.BackgroundColour);
            }
        }

        private bool _showAxes;
        public bool ShowAxes
        {
            get
            {
                return _showAxes;
            }
            set
            {
                _showAxes = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.ShowAxes);
            }
        }

        private bool _showAxesTitles;
        public bool ShowAxesTitles
        {
            get
            {
                return _showAxesTitles;
            }
            set
            {
                _showAxesTitles = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.ShowAxesTitles);
            }
        }

        private bool _showZBar;
        public bool ShowZBar
        {
            get
            {
                return _showZBar;
            }
            set
            {
                _showZBar = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.ShowZBar);
            }
        }

        private bool _showFrame;
        public bool ShowFrame
        {
            get
            {
                return _showFrame;
            }
            set
            {
                _showFrame = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.ShowFrame);
            }
        }

        private Color _frameColour;
        public Color FrameColour
        {
            get
            {
                return _frameColour;
            }
            set
            {
                _frameColour = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.FrameColour);
            }
        }

        private bool _showLabels;
        public bool ShowLabels
        {
            get
            {
                return _showLabels;
            }
            set
            {
                _showLabels = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.ShowLabels);
            }
        }

        private Color _labelColour;
        public Color LabelColour
        {
            get
            {
                return _labelColour;
            }
            set
            {
                _labelColour = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.LabelColour);
            }
        }

        private int _labelFontSize;
        public int LabelFontSize
        {
            get
            {
                return _labelFontSize;
            }
            set
            {
                _labelFontSize = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.LabelFontSize);
            }
        }

        private int _labelAngleInDegrees = 90;
        public int LabelAngleInDegrees
        {
            get
            {
                return _labelAngleInDegrees;
            }
            set
            {
                _labelAngleInDegrees = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.LabelAngleInDegrees);
            }
        }

        private Model.XYLabelPosition _xyLabelPosition;
        public Model.XYLabelPosition XYLabelPosition
        {
            get
            {
                return _xyLabelPosition;
            }
            set
            {
                _xyLabelPosition = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.XYLabelPosition);
            }
        }

        private bool _transparentLabelBackground = true;
        public bool TransparentLabelBackground
        {
            get
            {
                return _transparentLabelBackground;
            }
            set
            {
                _transparentLabelBackground = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.TransparentLabelBackground);
            }
        }

        private float _perspective;
        public float Perspective
        {
            get
            {
                return _perspective;
            }
            set
            {
                _perspective = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.Perspective);
            }
        }

        private ViewProjection _viewProjection;
        public ViewProjection ViewProjection
        {
            get
            {
                return _viewProjection;
            }
            set
            {
                _viewProjection = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.ViewProjection);
            }
        }

        private bool _showGrid;
        public bool ShowGrid
        {
            get
            {
                return _showGrid;
            }
            set
            {
                _showGrid = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.ShowGrid);
            }
        }

        private Color _gridColour;
        public Color GridColour
        {
            get
            {
                return _gridColour;
            }
            set
            {
                _gridColour = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.GridColour);
            }
        }

        private bool _showScatterPlot;
        public bool ShowScatterPlot
        {
            get
            {
                return _showScatterPlot;
            }
            set
            {
                _showScatterPlot = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.ShowScatterPlot);
            }
        }

        private bool _showShading;
        public bool ShowShading
        {
            get
            {
                return _showShading;
            }
            set
            {
                _showShading = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.ShowShading);
            }
        }

        private ShadingMethod _shadingMethod;
        public ShadingMethod ShadingMethod
        {
            get
            {
                return _shadingMethod;
            }
            set
            {
                _shadingMethod = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.ShadingMethod);
            }
        }

        private ShadingAlgorithm _shadingAlgorithm;
        public ShadingAlgorithm ShadingAlgorithm
        {
            get => _shadingAlgorithm;
            set
            {
                _shadingAlgorithm = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.ShadingAlgorithm);
            }
        }

        private double _minimumLevel;
        public double MinimumLevel
        {
            get => _minimumLevel;
            set
            {
                _minimumLevel = value;
                
                if (_colorBar != null)
                    _colorBar.MinValue = value;
                
                ConfigurationChanged?.Invoke(ConfigurationItem.MinimumLevel);
            }
        }

        private double _maximumLevel;
        public double MaximumLevel
        {
            get => _maximumLevel;
            set
            {
                _maximumLevel = value;
                
                if (_colorBar != null)
                    _colorBar.MaxValue = value;
                
                ConfigurationChanged?.Invoke(ConfigurationItem.MaximumLevel);
            }
        }

        // If Hold is true, display the maximum value if HoldMaximum is true and the minimum otherwise

        private bool _hold;
        public bool Hold
        {
            get
            {
                return _hold;
            }
            set
            {
                _hold = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.Hold);
            }
        }

        private bool _holdMaximum;
        private IColorBar _colorBar = new ColorBar(2048, -64, Color.Red, Color.Blue);

        public bool HoldMaximum
        {
            get
            {
                return _holdMaximum;
            }
            set
            {
                _holdMaximum = value;
                ConfigurationChanged?.Invoke(ConfigurationItem.HoldMaximum);
            }
        }

        private ColorBarsLoader _colorBarsLoader = new ColorBarsLoader();
        private ColorBarsLoader.ColorBars _colorBarSelector;

        public ColorBarsLoader.ColorBars ColorBarSelector
        {
            get => _colorBarSelector;
            set
            {
                var oldValue = _colorBarSelector;
                
                if(oldValue == value) return;

                _colorBarsLoader.TryGetColorBarOrDefault(value, out _colorBar);

                _colorBar.MinValue = _minimumLevel;
                _colorBar.MaxValue = _maximumLevel;

                _colorBarSelector = value;
                
                ConfigurationChanged?.Invoke(ConfigurationItem.ColorBar);
            }
        }

        public IColorBar ColorBar
        {
            get
            {
                if (_colorBar != null) return _colorBar;

                _colorBarsLoader.TryGetColorBarOrDefault(ColorBarSelector, out _colorBar);

                _colorBar.MinValue = _minimumLevel;
                _colorBar.MaxValue = _maximumLevel;
                
                return _colorBar;
            }
        }

        public Color BodiesColor { get; set; }

        public Color BodiesEdges { get; set; }

        #endregion IRawDataConfiguration
    }

}
