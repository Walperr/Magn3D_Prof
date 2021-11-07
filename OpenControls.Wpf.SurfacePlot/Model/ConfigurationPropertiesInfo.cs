using System;
using System.ComponentModel;
using System.Drawing;

namespace OpenControls.Wpf.SurfacePlot.Model
{
    public class ConfigurationPropertiesInfo : IConfiguration, IDisposable
    {
        private IConfiguration _configuration;
        private ShowModes _showMode = ShowModes.Shading;

        public event ConfigurationChangedEventHandler ConfigurationChanged;
  
        public ConfigurationPropertiesInfo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Browsable(false)]
        public int Zoom
        {
            get => _configuration.Zoom;
            set => _configuration.Zoom = value;
        }

        [Browsable(false)]
        public int MaximumZoom => _configuration.MaximumZoom;
        
        [Browsable(false)]
        public int MinimumZoom => _configuration.MinimumZoom;
        
        [Category("General")]
        public Color BackgroundColour
        {
            get => _configuration.BackgroundColour;
            set => _configuration.BackgroundColour = value;
        }
        
        [Category("General")]
        public bool ShowGrid
        {
            get => _configuration.ShowGrid;
            set => _configuration.ShowGrid = value;
        }
        
        [Category("General")]
        public Color GridColour
        {
            get => _configuration.GridColour;
            set => _configuration.GridColour = value;
        }
        
        [Category("Axes")]
        public bool ShowAxes
        {
            get => _configuration.ShowAxes;
            set => _configuration.ShowAxes = value;
        }

        [Category("Axes")]
        public bool ShowAxesTitles
        {
            get => _configuration.ShowAxesTitles;
            set => _configuration.ShowAxesTitles = value;
        }

        [Category("Levels")]
        public bool ShowZBar
        {
            get => _configuration.ShowZBar;
            set => _configuration.ShowZBar = value;
        }

        [Category("General")]
        public bool ShowFrame
        {
            get => _configuration.ShowFrame;
            set => _configuration.ShowFrame = value;
        }

        [Category("General")]
        public Color FrameColour
        {
            get => _configuration.FrameColour;
            set => _configuration.FrameColour = value;
        }

        [Category("Axes")]
        public bool ShowLabels
        {
            get => _configuration.ShowLabels;
            set => _configuration.ShowLabels = value;
        }

        [Category("Axes")]
        public Color LabelColour
        {
            get => _configuration.LabelColour;
            set => _configuration.LabelColour = value;
        }

        [Category("Axes")]
        public int LabelFontSize
        {
            get => _configuration.LabelFontSize;
            set => _configuration.LabelFontSize = value;
        }
        
        [Category("Axes")]
        public double ZScale
        {
            get => _configuration.ZScale;
            set => _configuration.ZScale = value;
        }

        [Browsable(false)]
        public int LabelAngleInDegrees
        {
            get => _configuration.LabelAngleInDegrees;
            set => _configuration.LabelAngleInDegrees = value;
        }

        [Browsable(false)]
        public bool TransparentLabelBackground
        {
            get => _configuration.TransparentLabelBackground;
            set => _configuration.TransparentLabelBackground = value;
        }

        [Category("Axes")]
        public XYLabelPosition XYLabelPosition
        {
            get => _configuration.XYLabelPosition;
            set => _configuration.XYLabelPosition = value;
        }

        [Browsable(false)]
        public float Perspective
        {
            get => _configuration.Perspective;
            set => _configuration.Perspective = value;
        }

        [Category("General")]
        public ViewProjection ViewProjection
        {
            get => _configuration.ViewProjection;
            set => _configuration.ViewProjection = value;
        }

        [Category("General")]
        public ShadingMethod ShadingMethod
        {
            get => _configuration.ShadingMethod;
            set => _configuration.ShadingMethod = value;
        }
        
        
        [Browsable(false)]
        public bool ShowScatterPlot { get; set; }
        [Browsable(false)]
        public bool ShowShading { get; set; }
        
        public enum ShowModes
        {
            Shading,
            ScatterPlot
        }

        [Category("Shading")]
        public ShowModes ShowMode
        {
            get => _showMode;
            set
            {
                var oldValue = _showMode;
                
                if(oldValue == value) return;

                switch (value)
                {
                    case ShowModes.Shading:
                        _configuration.ShowShading = true;
                        _configuration.ShowScatterPlot = false;
                        break;
                    case ShowModes.ScatterPlot:
                        _configuration.ShowShading = false;
                        _configuration.ShowScatterPlot = true;
                        break;
                    default:
                        break;
                }
                
                _showMode = value;
            }
        }

        [Category("Shading")]
        public ShadingAlgorithm ShadingAlgorithm
        {
            get => _configuration.ShadingAlgorithm;
            set => _configuration.ShadingAlgorithm = value;
        }

        [Category("Levels")]
        public double MinimumLevel
        {
            get => _configuration.MinimumLevel;
            set => _configuration.MinimumLevel = value;
        }

        [Category("Levels")]
        public double MaximumLevel
        {
            get => _configuration.MaximumLevel;
            set => _configuration.MaximumLevel = value;
        }

        [Browsable(false)]
        public bool Hold { get; set; }
        
        [Browsable(false)]
        public bool HoldMaximum { get; set; }

        public ColorBarsLoader.ColorBars ColorBarSelector
        {
            get => _configuration.ColorBarSelector;
            set => _configuration.ColorBarSelector = value;
        }
        
        [Browsable(false)]
        public IColorBar ColorBar { get; }
        
        [Category("Body")]
        public Color BodiesColor
        {
            get => _configuration.BodiesColor;
            set => _configuration.BodiesColor = value;
        }
        [Category("Body")]
        public Color BodiesEdges
        {
            get => _configuration.BodiesEdges;
            set => _configuration.BodiesEdges = value;
        }

        public void Dispose()
        {
            if(_configuration is IDisposable disposable)
                disposable.Dispose();
            
            _configuration = null;
        }
    }
}