using System;

namespace GRIDs.Parsers.Interfaces
{
    public interface IGridParser : IDisposable
    {
        IGrid ReadGRD();
        void SkipFormat();
    }
}