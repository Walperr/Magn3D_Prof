using System.Collections.Generic;
using Vectors;

namespace GRIDs
{
    public interface IGrid
    {
        List<double> X { get; }
        List<double> Y { get; }
        List<double> Z { get; }
        double Xmin { get; }
        double Xmax { get; }

        double Ymin { get; }
        double Ymax { get; }

        double Zmin { get; }
        double Zmax { get; }
        double Zmean { get; }
        double dX { get; }
        double dY { get; }

        double Interp(int LP, Vector2 point);
        List<double> Interp(int LP, List<Vector2> point);
        Vector2 GetXY(double Z);
        Vector3 GetCoordinates(int i, int j);
    }
}