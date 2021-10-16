using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using OpenControls.Wpf.SurfacePlot.Model;

namespace OpenControls.Wpf.SurfacePlot.Common
{
    public class ColorBarParser : IDisposable
    {
        private readonly StreamReader _reader;

        public ColorBarParser(StreamReader reader)
        {
            _reader = reader;
        }
        
        public void Dispose()
        {
            _reader?.Dispose();
        }

        public IColorBar Parse()
        {
            _reader.ReadLine();

            var lines = new List<string>();

            while (!_reader.EndOfStream)
                lines.Add(_reader.ReadLine());
            
            try
            {
                var data = lines.Select(line =>
                {
                    var splited = line.Split(' ').Where(item => item != "").ToArray();

                    return new
                    {
                        value = double.Parse(splited[0].Replace('.',',')), R = int.Parse(splited[1]), G = int.Parse(splited[2]),
                        B = int.Parse(splited[3]),
                        A = int.Parse(splited[4])
                    };
                }).ToArray();

                var values = data.Select(x => x.value).ToArray();
                var colors = data.Select(x => Color.FromArgb(x.A, x.R, x.G, x.B)).ToArray();

                return new ColorBar(values, colors);
            }
            catch (Exception e)
            {
                throw new FileFormatException("Wrong file format");
            }
        }
    }
}