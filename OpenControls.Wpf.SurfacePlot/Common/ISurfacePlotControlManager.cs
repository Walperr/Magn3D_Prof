using System;
using System.Drawing;
using Control = System.Windows.Forms.Control;

namespace OpenControls.Wpf.SurfacePlot.Common
{
    public interface ISurfacePlotControlManager : IDisposable
    {
        int LowQualitySurfaceResolution { get; set; }

        object OriginalSurface { get; set; }

        Color Background { get; }

        Control SurfacePlotParentControl { get; set; }

        Control PropertyGridParentControl { get; set; }
    }
}