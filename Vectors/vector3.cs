using System;

namespace Vectors
{
    public struct Vector3
    {
        public double X { get; set; } //Координаты
        public double Y { get; set; }
        public double Z { get; set; }

        private readonly double length;

        public Vector3(double x_, double y_, double z_)//конструктор
        {
            X = x_;
            Y = y_;
            Z = z_;
            length = Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public Vector3 Normalized()
        {
            if (Getlength() == 0)
                return new Vector3();
            return new Vector3(X / Getlength(), Y / Getlength(), Z / Getlength());
        }

        public double Getlength()
        {
            return length;
        }
        // возвращает расстояние между двумя точками с радиус векторами a и b
        public static double Distance(Vector3 a, Vector3 b)
        {
            return (a - b).Getlength();
        }

        // пересчитывает координаты вектора в заданном нормированном базисе l,m,n
        public static Vector3 RotateCoordinates(Vector3 l, Vector3 m, Vector3 n, Vector3 vector)
        {
            double u, v, w; //новые координаты

            u = l.X * vector.X + l.Y * vector.Y + l.Z * vector.Z; //проекции исходного вектора на заданные базисные вектора
            v = m.X * vector.X + m.Y * vector.Y + m.Z * vector.Z; //проекции исходного вектора на заданные базисные вектора
            w = n.X * vector.X + n.Y * vector.Y + n.Z * vector.Z; //проекции исходного вектора на заданные базисные вектора

            return new Vector3(u,v,w); //строим вектор из новых координат
        }
        /// <summary>
        /// Создает проекцию точки на вертикальную плоскость, заданную точками p1, p2
        /// </summary>
        /// <param name="p">Проектируемая точка</param>
        /// <param name="p1">Начало прямой</param>
        /// <param name="p2">Конец прямой</param>
        /// <returns></returns>
        public static double GetProjection(Vector3 p, Vector3 p1, Vector3 p2)
        {
            double Denominator = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
            if (Denominator == 0) // p1 and p2 are the same
                return 0;

            double t = (p.X * (p2.X - p1.X) - (p2.X - p1.X) * p1.X + p.Y * (p2.Y - p1.Y) - (p2.Y - p1.Y) * p1.Y) / Denominator;

            return t;
        }

        //арифметические операции
        public static Vector3 operator +(Vector3 a, Vector3 b) 
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.X,-a.Y,-a.Z);
        }
        public static double operator *(Vector3 a, Vector3 b)
        {
            return (a.X * b.X + a.Y * b.Y + a.Z * b.Z);
        }
        public static Vector3 operator &(Vector3 a, Vector3 b)
        {
            return new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
        }
        public static Vector3 operator *(double a, Vector3 b)
        {
            return new Vector3(a * b.X, a * b.Y, a * b.Z);
        }
        public static Vector3 operator *(Vector3 b, double a)
        {
            return new Vector3(a * b.X, a * b.Y, a * b.Z);
        }
        public static Vector3 operator /(Vector3 b, double a)
        {
            return new Vector3(b.X / a,b.Y / a,b.Z / a);
        }
    }
}