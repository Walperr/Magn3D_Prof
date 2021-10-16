using System;
using System.Collections;
using System.Drawing;
using System.Linq;

namespace OpenControls.Wpf.SurfacePlot.Model
{
    public class ColorBar : IColorBar
    {
        private Color[] _colors;
        private double[] _values;
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
        
        public double MaxValue
        {
            get => _values.Max();
            set => RecalculateValues(value,MinValue);
        }

        public double MinValue
        {
            get => _values.Min();
            set => RecalculateValues(MaxValue,value);
        }

        public ColorBar(double[] values, Color[] colors)
        {
            _values = values;
            _colors = colors;
        }

        public ColorBar(double maxValue, double minValue, Color maxColor, Color minColor)
        {
            _values = new[] {minValue, maxValue};
            _colors = new[] {minColor, maxColor};
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

            return Color.Transparent;
        }

        private void RecalculateValues(double maxValue, double minValue)
        {
            var lastMin = _values.Min();
            var lastMax = _values.Max();
            
            _values = _values.Select(value => minValue + (value - lastMin)/(lastMax - lastMin)*(maxValue-minValue)).ToArray();
        }
    }
}