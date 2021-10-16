using System;
using System.Drawing;
using System.IO;
using OpenControls.Wpf.SurfacePlot.Common;

namespace OpenControls.Wpf.SurfacePlot.Model
{
    public class ColorBarsLoader
    {
        public enum ColorBars
        { GrayScale,
            ChromaDepth,
            Terrain,
            Rainbow,
            Rainbow2,
            Rainbow3,
            Rainbow4,
            Rainbow5,
            YellowHigh,
            Geology,
            Geology2,
            Gravity,
            Gravity2,
            HighPoints,
            HighPoints2,
            Exploration,
            Exploration2,
            Forecast,
            Ice,
            Soil,
            Sea2,
            Land,
            Land2,
            LandArid,
            Desert,
            Blues1,
            Blues3,
            BlueSteel,
            BlueRed1,
            BlueRed2,
            BrownBlue,
            BrownGreen,
            Accents,
            PurpleOrange2,
            YellowJacket
        }

        private static IColorBar[] ColorBarObjects = new IColorBar[35];

        public IColorBar GetColorBar(ColorBars colorBar)
        {
            var result = ColorBarObjects[(int) colorBar];

            if (result == null)
            
                using (StreamReader reader = new StreamReader("ColorBars\\" + colorBar + ".clr"))
                {
                    result = new ColorBarParser(reader).Parse();
                    ColorBarObjects[(int) colorBar] = result;
                }
            
            return result;
        }

        public bool TryGetColorBarOrDefault(ColorBars colorBar, out IColorBar result)
        {
            result = new ColorBar(0, 100, Color.Blue, Color.Red);
            try
            {
                result = GetColorBar(colorBar);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}