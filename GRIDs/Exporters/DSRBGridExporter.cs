using System.IO;
using System.Linq;
using GRIDs.Exporters.Interfaces;

namespace GRIDs.Exporters
{
    public class DSRBGridExporter : IGridExporter
    {
        private readonly BinaryWriter _writer;
        private readonly IGrid _grid;

        public DSRBGridExporter(BinaryWriter writer, IGrid grid)
        {
            _writer = writer;
            _grid = grid;
        }
        
        public void Dispose()
        {
            _writer?.Dispose();
        }

        public void SaveGrid()
        {
            _writer.Write(new []{'D', 'S', 'R', 'B'});
            
            _writer.Write(new byte[] {4, 0, 0, 0, 2, 0, 0, 0});
            
            _writer.Write(new []{'G', 'R', 'I', 'D'});
            
            _writer.Write(72);
            
            _writer.Write(_grid.X.Count);
            _writer.Write(_grid.Y.Count);
            
            _writer.Write(_grid.Xmin);
            _writer.Write(_grid.Ymin);
            
            _writer.Write(_grid.dX);
            _writer.Write(_grid.dY);
            
            _writer.Write(_grid.Zmin);
            _writer.Write(_grid.Zmax);
            
            _writer.Write(Enumerable.Repeat(byte.MinValue,8).ToArray());
            
            _writer.Write(1.70141E38);
            
            _writer.Write(new []{'D', 'A', 'T', 'A'});
            
            _writer.Write(_grid.Z.Count * 8);

            foreach (var z in _grid.Z)
                if(double.IsNaN(z)) _writer.Write(1.70141E38);
                else _writer.Write(z);
            
            _writer.Close();
        }
    }
}