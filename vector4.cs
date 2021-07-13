public struct Vector4
{
    public double X { get; set; } //Координаты
    public double Y { get; set; }
    public double Z { get; set; }
    public double W { get; set; }

    public Vector4(double x, double y, double z, double w)//конструктор
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }
}
