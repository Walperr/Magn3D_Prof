using System.Globalization;
using System.IO;
using GRIDs.Parsers.Interfaces;

namespace GRIDs.Parsers
{
    public class DSBBGridParser : IGridParser
    {
        private readonly BinaryReader _reader;

        public DSBBGridParser(BinaryReader reader)
        {
            _reader = reader;
        }
        
        public GRD ReadGRD()
        {
            double[] z, x, y;
            int sNx, sNy, N;
            double xMin, xMax, yMin, yMax, zMin, zMax, dx, dy;
            
            sNx = _reader.ReadInt16();
            sNy = _reader.ReadInt16();

            N = sNx * sNy;

            z = new double[N];
            x = new double[sNx];
            y = new double[sNy];

            xMin = _reader.ReadDouble();
            xMax = _reader.ReadDouble();

            yMin = _reader.ReadDouble();
            yMax = _reader.ReadDouble();

            zMin = _reader.ReadDouble();
            zMax = _reader.ReadDouble();

            dx = (xMax - xMin) / (sNx-1);
            dy = (yMax - yMin) / (sNy-1);

            for(int i = 0; i < N; i++)
            {
                z[i] = _reader.ReadSingle();
                if (z[i].ToString(CultureInfo.InvariantCulture) == 1.70141e38d.ToString(CultureInfo.InvariantCulture))
                    z[i] = double.NaN;
                if (i < sNx)
                    x[i] = xMin + i * dx;
                if (i < sNy)
                    y[i] = yMin + i * dy;
            }
            _reader.Close();

            return new GRD(zMin, zMax, x, y, z);
        }

        public void SkipFormat()
        {
            _reader.ReadChars(4);
        }

        public void Dispose()
        {
            _reader?.Dispose();
        }
    }
}