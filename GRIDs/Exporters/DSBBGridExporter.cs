using System;
using System.IO;
using GRIDs.Exporters.Interfaces;

namespace GRIDs.Exporters
{
    public class DSBBGridExporter : IGridExporter
    {
        private readonly BinaryWriter _writer;
        private readonly GRD _grid;

        public DSBBGridExporter(BinaryWriter writer, GRD grid)
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
            _writer.Write(new []{'D','S','B', 'B'});
            
            _writer.Write(Convert.ToInt16(_grid.SNx));
            _writer.Write(Convert.ToInt16(_grid.SNy));
            
            _writer.Write(_grid.Xmin);
            _writer.Write(_grid.Xmax);
            
            _writer.Write(_grid.Ymin);
            _writer.Write(_grid.Ymax);
            
            _writer.Write(_grid.Zmin);
            _writer.Write(_grid.Zmax);

            foreach (var z in _grid.Z)
                if(double.IsNaN(z))_writer.Write(1.70141e38f);
                else _writer.Write(Convert.ToSingle(z));
            
            _writer.Close();
        }
    }
}