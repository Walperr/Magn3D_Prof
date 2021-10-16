using System.Drawing;

namespace OpenControls.Wpf.SurfacePlot.Model
{
    public interface IColorBar
    {
        Color GetColor(double value);
        
        double MaxValue { get; set; }
        double MinValue { get; set; }
    }
}