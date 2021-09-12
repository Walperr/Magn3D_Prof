using System;

namespace GRIDs.Parsers.Interfaces
{
    public interface IGridParser : IDisposable
    {
        GRD ReadGRD();
        void SkipFormat();
    }
}