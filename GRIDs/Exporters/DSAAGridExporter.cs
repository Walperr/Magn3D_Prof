using System;
using System.IO;
using GRIDs.Exporters.Interfaces;
using Microsoft.SqlServer.Server;

namespace GRIDs.Exporters
{
    public class DSAAGridExporter : IGridExporter
    {
        private readonly StreamWriter _writer;
        private readonly GRD _grid;

        public DSAAGridExporter(StreamWriter writer, GRD grid)
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
            _writer.WriteLine("DSAA");
            
            _writer.WriteLine(_grid.SNx + " " + _grid.SNy);
            
            _writer.WriteLine((_grid.Xmin + " " + _grid.Xmax).Replace(',', '.'));
            
            _writer.WriteLine((_grid.Ymin + " " + _grid.Ymax).Replace(',', '.'));
            
            _writer.WriteLine((_grid.Zmin + " " + _grid.Zmax).Replace(',', '.'));

            for (int i = 0; i < _grid.Z.Count; i+=10)
            {
                var line = "";
                
                for (int j = 0; j <= 10; j++)
                {
                    if(i+j >= _grid.Z.Count)
                        break;
                    if (double.IsNaN(_grid.Z[i + j])) line += "1.70141e38 ";
                    else line += _grid.Z[i + j].ToString("F16") + " ";
                }
                
                _writer.WriteLine(line.Replace(',', '.'));
            }
            
            _writer.Close();
        }
    }
}