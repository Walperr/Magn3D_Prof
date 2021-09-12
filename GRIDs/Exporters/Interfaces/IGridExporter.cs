using System;

namespace GRIDs.Exporters.Interfaces
{
    public interface IGridExporter : IDisposable
    {
        void SaveGrid();
    }
}