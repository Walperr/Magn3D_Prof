using System.Globalization;
using System.IO;

namespace GRIDs
{
    public class DSAAGridParser : IGRDParser
    {
        private readonly StreamReader _reader;

        public DSAAGridParser(StreamReader reader)
        {
            _reader = reader;
        }

        public void Dispose()
        {
            _reader?.Dispose();
        }

        public GRD ReadGRD()
        {
            double[] z, x, y;
            int sNx, sNy, N;
            double xMin, xMax, yMin, yMax, zMin, zMax, dx, dy;

            
            _reader.ReadLine();

            string s = _reader.ReadLine();

            int j = s.IndexOf(' ');

            sNx = int.Parse(s.Substring(0, j));

            sNy = int.Parse(s.Substring(j + 1));

            N = sNx * sNy;

            z = new double[N];
            x = new double[sNx];
            y = new double[sNy];

            s = _reader.ReadLine();
            j = s.IndexOf(' '); // Получаем индекс разделителя (пробел)

            xMin = double.Parse(s.Substring(0, j).Replace('.', ','), System.Globalization.NumberStyles.Any);
            xMax = double.Parse(s.Substring(j + 1).Replace('.', ','), System.Globalization.NumberStyles.Any);

            s = _reader.ReadLine();
            j = s.IndexOf(' '); // Получаем индекс разделителя (пробел)

            yMin = double.Parse(s.Substring(0, j).Replace('.', ','), System.Globalization.NumberStyles.Any);
            yMax = double.Parse(s.Substring(j + 1).Replace('.', ','), System.Globalization.NumberStyles.Any);

            s = _reader.ReadLine();
            j = s.IndexOf(' '); // Получаем индекс разделителя (пробел)

            zMin = double.Parse(s.Substring(0, j).Replace('.', ','), System.Globalization.NumberStyles.Any);
            zMax = double.Parse(s.Substring(j + 1).Replace('.', ','), System.Globalization.NumberStyles.Any);

            dx = (xMax - xMin) / sNx;
            dy = (yMax - yMin) / sNy;
            int i = 0;
            while (!_reader.EndOfStream)
            {
                s = _reader.ReadLine();
                if (s == "") continue;
                var m = GetCharCount(s, ' ');
                for (int g = 0; g < m; g++)
                {
                    j = s.IndexOf(' ');
                    z[i] = double.Parse(s.Substring(0, j).Replace('.', ','), System.Globalization.NumberStyles.Any);
                    if (z[i].ToString(CultureInfo.InvariantCulture) ==
                        1.70141e38d.ToString(CultureInfo.InvariantCulture))
                        z[i] = double.NaN;
                    if (i < sNx)
                        x[i] = xMin + i * dx;
                    if (i < sNy)
                        y[i] = yMin + i * dy;
                    s = s.Remove(0, j + 1);
                    i++;
                }
            }

            _reader.Close(); // Закрываем файл чтения
        
            return new GRD(zMin, zMax, x, y, z);
        }
        
        private static int GetCharCount(string s, char c)
        {
            int count = 0;

            foreach (var i in s)
            {
                if (i == c) count++;
            }

            return count;
        }
    }
}