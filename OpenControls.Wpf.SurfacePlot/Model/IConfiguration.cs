using System.Drawing;

namespace OpenControls.Wpf.SurfacePlot.Model
{
    public interface IConfiguration
    {
        // Signalled whenever a configuration setting is changed
        event Model.ConfigurationChangedEventHandler ConfigurationChanged;

        int Zoom { get; set; }
        int MaximumZoom { get; }
        int MinimumZoom { get; }
        double ZScale { get; set; }
        Color BackgroundColour { get; set; }
        bool ShowAxes { get; set; }
        bool ShowAxesTitles { get; set; }
        bool ShowZBar { get; set; }
        bool ShowFrame { get; set; }
        Color FrameColour { get; set; }
        bool ShowLabels { get; set; }
        Color LabelColour { get; set; }
        int LabelFontSize { get; set; }
        int LabelAngleInDegrees { get; set; }
        bool TransparentLabelBackground { get; set; }
        XYLabelPosition XYLabelPosition { get; set; }
        float Perspective { get; set; }
        ViewProjection ViewProjection { get; set; }
        ShadingMethod ShadingMethod { get; set; }
        bool ShowGrid { get; set; }
        Color GridColour { get; set; }
        bool ShowScatterPlot { get; set; }
        bool ShowShading { get; set; }
        ShadingAlgorithm ShadingAlgorithm { get; set; }
        short MinimumLevel { get; set; }
        short MaximumLevel { get; set; }
        bool Hold { get; set; }
        bool HoldMaximum { get; set; }

        ColorBarsLoader.ColorBars ColorBarSelector { get; set; }
        IColorBar ColorBar { get; }
    }
}
