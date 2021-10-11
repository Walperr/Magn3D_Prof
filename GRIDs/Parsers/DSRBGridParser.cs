using System.Globalization;
using System.IO;
using GRIDs.Parsers.Interfaces;

namespace GRIDs.Parsers
{
    public class DSRBGridParser : IGridParser
    {
        private readonly BinaryReader _reader;

        public DSRBGridParser(BinaryReader reader)
        {
            _reader = reader;
        }
        
        public IGrid ReadGRD()
        {
            double[] z, x, y;
            int sNx, sNy, N;
            double xMin, yMin, zMin, zMax, dx, dy;

            var t1 = _reader.ReadInt32();
            var t2 = _reader.ReadInt32();

            var marker1 = _reader.ReadChars(4);
            var gridBytesCount = _reader.ReadInt32();

            sNy = _reader.ReadInt32();//rows
            sNx = _reader.ReadInt32();//cols

            N = sNx * sNy;

            z = new double[N];
            x = new double[sNx];
            y = new double[sNy];

            xMin = _reader.ReadDouble();//xmin
            yMin = _reader.ReadDouble();//ymin
            dx = _reader.ReadDouble();//dx
            dy = _reader.ReadDouble();//dy
            zMin = _reader.ReadDouble();//zmin
            zMax = _reader.ReadDouble();//zmax
            var rotation = _reader.ReadDouble();//rotation
            var nodataValues = _reader.ReadDouble().ToString(CultureInfo.InvariantCulture);//Nodatavalues
            var marker2 = _reader.ReadChars(4);
            var dataBytesCount = _reader.ReadInt32();

            for (int i = 0; i < N; i++)
            {
                z[i] = _reader.ReadDouble();
                if (z[i].ToString(CultureInfo.InvariantCulture) == nodataValues)
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