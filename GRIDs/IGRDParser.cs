using System;

namespace GRIDs
{
    public interface IGRDParser : IDisposable
    {
        GRD ReadGRD();
    }
}