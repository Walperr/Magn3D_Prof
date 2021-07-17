using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magn3D_Prof
{
   public class Prismbody
    {
        private const double k = 1000.0d / (4 * Math.PI) * 1256.6370621219d; // Коэффициент перевода из Эрстед в наноТесла

        public double X, Y, b, d, L; // Все линейные размеры получаем в метрах
        public double h1, h2, h3;
        public double alpha, beta, fi;
        public double kappa, I, D;
        public bool hle;

        public delegate void PrismEvent(Prismbody sender, EventArgs e);

        public event PrismEvent OnChangeParametrs;

        private Vector3 J_vector; // Вектор намагниченности

        public List<Vector3> Verticles; // Координаты вершин призмы

        private List<List<List<Vector4>>> Fields = new List<List<List<Vector4>>>(); // Fields[i][j][k] i - индекс профиля, j  - индекс карты высот, k - индекс точки наблюдения X {X, Fx, Fy, Fz}

        public Prismbody(double x, double y, double b, double d, double L, double h1, double h2, double h3,
                                   double fi, double alpha, double beta, double kappa, double I, double D, bool hle)
        {
            this.X = x;
            this.Y = y;
            this.b = b;
            this.d = d;
            this.L = L;
            this.h1 = h1;
            this.h2 = h2;
            this.h3 = h3;
            this.alpha = fi;
            this.beta = alpha;
            this.fi = beta;
            this.kappa = kappa;
            this.I = I;
            this.D = D;
            this.hle = hle;

            OnChangeParametrs = (Prismbody sender, EventArgs e) => { };
            if(Global.Profiles != null)
                foreach(var profile in Global.Profiles)
                {
                    AddProfile();
                    OnChangeParametrs += profile.AddBodyToUpdate;
                }

            UpdateBody();
        }
        /// <summary>
        /// Обновляет тело в соответствии с параметрами
        /// </summary>
        public void UpdateBody()
        {
            Verticles = new List<Vector3>();

            Vector3 bias = new Vector3(X, Y, 0); // Смещение верхней грани в плоскости x0y относительно начала координат

            List<Vector2> points = new List<Vector2>
        {
            new Vector2(-b / 2 + X, -d / 2 + Y),
            new Vector2(b / 2+ X, -d /2 + Y),
            new Vector2(-b /2+ X, d / 2 + Y),
            new Vector2(b / 2+ X, d / 2 + Y)
        };

            List<double> Zs = new List<double>();

            for (int i = 0; i < 4; i++)
                Zs.Add(0);

            if (Global.Relief != null)
                Zs = Global.Relief.Interp(0, points);

            for(int i = 0; i <  4; i++)
                if (double.IsNaN(Zs[i]))
                    Zs[i] = Global.Relief.GetzMean();
            
            Verticles.Add(new Vector3(-b / 2, -d / 2, h1 - Zs[0])); // 1 Точка грани
            Verticles.Add(new Vector3(b / 2, -d / 2, h2 - Zs[1])); // 2 Точка грани
            Verticles.Add(new Vector3(-b / 2, d / 2, h3 - Zs[2])); // 3 Точка грани
            Verticles.Add(new Vector3(b / 2, d / 2, h2 - h1 + h3 - Zs[3])); // 4 Точка грани (координата z рассчитывается на основе первых 3 точек)

            // Поворачиваем основание вокруг оси Z на угол ф
            Verticles[0] = RotateZaxis(Verticles[0], alpha * Math.PI / 180) + bias;
            Verticles[1] = RotateZaxis(Verticles[1], alpha * Math.PI / 180) + bias;
            Verticles[2] = RotateZaxis(Verticles[2], alpha * Math.PI / 180) + bias;
            Verticles[3] = RotateZaxis(Verticles[3], alpha * Math.PI / 180) + bias;

            // Рассчитываем координаты вектора оси призмы (вектор с началом в центре верхнего основания и концом в центре нижнего)
            double Lx = L * Math.Cos(fi * Math.PI / 180) * Math.Cos(beta * Math.PI / 180);
            double Ly = L * Math.Cos(fi * Math.PI / 180) * Math.Sin(beta * Math.PI / 180);
            double Lz = L * Math.Sin(fi * Math.PI / 180);

            Vector3 L_vec = new Vector3(Lx, Ly, Lz); // Собираем вектор

            // Считаем координаты нижнего основания
            if (hle) // Если оно горизонтально
            {
                double h = (h3 - h2) / 2 + h2; // Считаем его координату z как проекцию на горизонтальную плоскость

                // Считаем координаты нижнего основания, добавляя смещение вдоль оси призмы
                Verticles.Add(new Vector3(Verticles[0].X, Verticles[0].Y, h)+ L_vec);
                Verticles.Add(new Vector3(Verticles[1].X, Verticles[1].Y, h)+ L_vec);
                Verticles.Add(new Vector3(Verticles[2].X, Verticles[2].Y, h)+ L_vec);
                Verticles.Add(new Vector3(Verticles[3].X, Verticles[3].Y, h)+ L_vec);
            }
            else
            { // Иначе

                // Копируем верхнее основание и смещаем на L вдоль оси призмы
                Verticles.Add(Verticles[0] + L_vec);
                Verticles.Add(Verticles[1] + L_vec);
                Verticles.Add(Verticles[2] + L_vec);
                Verticles.Add(Verticles[3] + L_vec);

            }

            double J = kappa * Global.T0.Getlength() / (1256.6370621219d * 1000) * 4 * Math.PI / 1000;

            J_vector = new Vector3(J * Math.Cos(I * Math.PI / 180) * Math.Cos(D * Math.PI / 180),
                                J * Math.Cos(I * Math.PI / 180) * Math.Sin(D * Math.PI / 180), J * Math.Sin(I * Math.PI / 180));

            OnChangeParametrs(this, new EventArgs());
        }

        // Поворот точки вокруг начала координат на угол ang
        private Vector3 RotateZaxis(Vector3 vec, double ang)
        {
            double x = vec.X * Math.Cos(ang) + vec.Y * Math.Sin(ang);
            double y = -vec.X * Math.Sin(ang) + vec.Y * Math.Cos(ang);

            return new Vector3(x, y, vec.Z);
        }

        /// <summary>
        /// Возвращает проекцию вектора магнитной индукции на направление M в указанной точке в наноТесла
        /// </summary>
        /// <param name="M">Направление</param>
        /// <param name="point">Точка наблюдения</param>
        /// <returns></returns>
        public double Field(Vector3 M, Vector3 point)
        {
            Vector3 temp = new Vector3(); // Переменная хранящая в себе результатирующее поле

            point *= 100; // Переводим метры в сантиметры (СГС)

            // Добавляем 12 слагаемых поля от каждого треугольника (в прямоугольной призме их 12)
            // Координаты каждого треугольника пересчитываются индивидуальной для каждого в системе координат (см. Статью)
            temp = temp + IntegralTerm(M, ChangeSystem(new List<Vector3> { point, Verticles[0] * 100, Verticles[2] * 100, Verticles[1] * 100 }, Verticles[4] * 100));
            temp = temp + IntegralTerm(M, ChangeSystem(new List<Vector3> { point, Verticles[1] * 100, Verticles[2] * 100, Verticles[3] * 100 }, Verticles[4] * 100));

            temp = temp + IntegralTerm(M, ChangeSystem(new List<Vector3> { point, Verticles[4] * 100, Verticles[5] * 100, Verticles[6] * 100 }, Verticles[0] * 100));
            temp = temp + IntegralTerm(M, ChangeSystem(new List<Vector3> { point, Verticles[5] * 100, Verticles[7] * 100, Verticles[6] * 100 }, Verticles[0] * 100));

            temp = temp + IntegralTerm(M, ChangeSystem(new List<Vector3> { point, Verticles[0] * 100, Verticles[4] * 100, Verticles[2] * 100 }, Verticles[1] * 100));
            temp = temp + IntegralTerm(M, ChangeSystem(new List<Vector3> { point, Verticles[2] * 100, Verticles[4] * 100, Verticles[6] * 100 }, Verticles[1] * 100));

            temp = temp + IntegralTerm(M, ChangeSystem(new List<Vector3> { point, Verticles[1] * 100, Verticles[3] * 100, Verticles[5] * 100 }, Verticles[0] * 100));
            temp = temp + IntegralTerm(M, ChangeSystem(new List<Vector3> { point, Verticles[3] * 100, Verticles[7] * 100, Verticles[5] * 100 }, Verticles[0] * 100));

            temp = temp + IntegralTerm(M, ChangeSystem(new List<Vector3> { point, Verticles[0] * 100, Verticles[1] * 100, Verticles[4] * 100 }, Verticles[2] * 100));
            temp = temp + IntegralTerm(M, ChangeSystem(new List<Vector3> { point, Verticles[1] * 100, Verticles[5] * 100, Verticles[4] * 100 }, Verticles[2] * 100));

            temp = temp + IntegralTerm(M, ChangeSystem(new List<Vector3> { point, Verticles[2] * 100, Verticles[6] * 100, Verticles[3] * 100 }, Verticles[0] * 100));
            temp = temp + IntegralTerm(M, ChangeSystem(new List<Vector3> { point, Verticles[3] * 100, Verticles[6] * 100, Verticles[7] * 100 }, Verticles[0] * 100));

            return J_vector * temp * k; // Домножаем на вектор намагниченности и возвращаем результат, переводя в наноТесла
        }

        // Пересчет вершин треугольника в новых координатах
        private List<Vector3> ChangeSystem(List<Vector3> points, Vector3 orientation)
        {
            // Получаем точку наблюдения (x,y,z) - points[0], вершины треугольника и дополнительную точку для рассчета нормали
            // В новой системе координат один вектор направлени вдоль грани треугольника, другой вдоль его внешней нормали, третий дополняет их до левого ОНБ
            Vector3 l = (points[3] - points[1]).Normalized(); // Строим первый вектор как направляющий вдоль грани 13
            Vector3 n = (l & (points[2] - points[1])).Normalized(); // Находим единичную нормаль к треугольнику

            double f = n * (orientation - points[1]); // Проверяем если нормаль внешняя скалярным произведением

            if (f > 0) n = -n; // Если дополнительный вектор и нормаль образуют острый угол, разворачиваем нормаль

            Vector3 m = l & n; // Дополняем l и n до базиса так, чтобы тройка l,m,n была левой
            Vector3 point = Vector3.RotateCoordinates(l, m, n, points[0]); // Пересчитываем точку наблюдения в новом базисе

            List<Vector3> output = new List<Vector3>(6); // Объявляем массив для вывода

            for (int i = 1; i <= 3; i++)// Пересчитываем вершины треугольника в базисе l,m,n
                output.Add(Vector3.RotateCoordinates(l, m, n, points[i]) - point);

            // Добавляем в вывод вектора l,m,n
            output.Add(l);
            output.Add(m);
            output.Add(n);

            return output; // Возвращаем массив, как указатель на первый элемент
        }

        // Слагаемое, дающее вклад одного треугольника
        private Vector3 IntegralTerm(Vector3 M, List<Vector3> points)
        {
            // Обозначим константы
            double A = M * points[3]; // A = (M,l)
            double B = M * points[4]; // B = (M,m)
            double C = M * points[5]; // C = (M,n)

            double p1 = points[0].X, p2 = points[1].X, p3 = points[2].X; // (p,q,r) - координаты вершин треугольника
            double q1 = points[0].Y, q2 = points[1].Y, q3 = points[2].Y;
            double r1 = points[0].Z, r2 = points[1].Z, r3 = points[2].Z;

            double g1 = (p2 - p1) / (q2 - q1); // Некоторые параметры, связанные с системой координат
            double g2 = (p2 - p3) / (q2 - q1);

            double h1 = (p1 * q2 - p2 * q1) / (q2 - q1);
            double h2 = (p3 * q2 - p2 * q1) / (q2 - q1);

            double alpha1 = Math.Atan(g1);
            double alpha2 = Math.Atan(g2);

            double R1 = points[0].Getlength(); // Расстояния от точки наблюдения к каждой вершине
            double R2 = points[1].Getlength();
            double R3 = points[2].Getlength();

            double I = 0, f1, f2, f3, f4; // Для рассчетов

            // Считаем первое слагаемое в выражении 20 (статья Барнетта), обходя особенности логарифма

            if (p1 * Math.Sin(alpha1) + q1 * Math.Cos(alpha1) >= 0)
                f1 = p1 * Math.Sin(alpha1) + q1 * Math.Cos(alpha1) + R1;
            else
            {
                double t = (p1 * Math.Cos(alpha1) - q1 * Math.Sin(alpha1)) * (p1 * Math.Cos(alpha1) - q1 * Math.Sin(alpha1)) + r1 * r1;
                double y = R1 - (p1 * Math.Sin(alpha1) + q1 * Math.Cos(alpha1));

                f1 = t / y;
            }

            if (p2 * Math.Sin(alpha1) + q2 * Math.Cos(alpha1) >= 0)
                f2 = p2 * Math.Sin(alpha1) + q2 * Math.Cos(alpha1) + R2;
            else
            {
                double t = (p2 * Math.Cos(alpha1) - q2 * Math.Sin(alpha1)) * (p2 * Math.Cos(alpha1) - q2 * Math.Sin(alpha1)) + r2 * r2;
                double y = R2 - (p2 * Math.Sin(alpha1) + q2 * Math.Cos(alpha1));

                f2 = t / y;
            }

            I += (A * Math.Cos(alpha1) - B * Math.Sin(alpha1)) * Math.Log(f1 / f2); // Добавили первое слагаемое

            // Считаем второе слагаемое в выражении 20 (статья Барнетта), обходя особенности логарифма

            if (p3 * Math.Sin(alpha2) + q1 * Math.Cos(alpha2) >= 0)
                f1 = p3 * Math.Sin(alpha2) + q1 * Math.Cos(alpha2) + R3; // Если не работает поменяй q1 на q3
            else
            {
                double t = (p3 * Math.Cos(alpha2) - q1 * Math.Sin(alpha2)) * (p3 * Math.Cos(alpha2) - q1 * Math.Sin(alpha2)) + r3 * r3;
                double y = R3 - (p3 * Math.Sin(alpha2) + q1 * Math.Cos(alpha2));

                f1 = t / y;
            }

            if (p2 * Math.Sin(alpha2) + q2 * Math.Cos(alpha2) >= 0)
                f2 = p2 * Math.Sin(alpha2) + q2 * Math.Cos(alpha2) + R2;
            else
            {
                double t = (p2 * Math.Cos(alpha2) - q2 * Math.Sin(alpha2)) * (p2 * Math.Cos(alpha2) - q2 * Math.Sin(alpha2)) + r2 * r2;
                double y = R2 - (p2 * Math.Sin(alpha2) + q2 * Math.Cos(alpha2));

                f2 = t / y;
            }

            I -= (A * Math.Cos(alpha2) - B * Math.Sin(alpha2)) * Math.Log(f1 / f2); // Добавили второе слагаемое (оно со знаком минус)

            // Считаем третье слагаемое в выражении 20 (статья Барнетта), обходя особенности логарифма

            if (p1 >= 0)
                f1 = p1 + R1;
            else f1 = (q1 * q1 + r1 * r1) / (R1 - p1);

            if (-p3 >= 0)
                f2 = p3 - R3;
            else f2 = -(q3 * q3 + r3 * r3) / (R3 + p3);

            if (-p1 >= 0)
                f3 = p1 - R1;
            else f3 = -(q1 * q1 + r1 * r1) / (R1 + p1);

            if (p3 >= 0)
                f4 = p3 + R3;
            else f4 = (q3 * q3 + r3 * r3) / (R3 - p3);

            I += (B / 2) * Math.Log((f1 * f2) / (f3 * f4)); // Добавили третье слагаемое

            I += C * (Atan2(g1 * r1 * r1 - h1 * q1, r1 * R1)
                            - Atan2(g1 * r1 * r1 - h1 * q2, r1 * R2)
                            - Atan2(g2 * r1 * r1 - h2 * q1, r1 * R3)
                            + Atan2(g2 * r1 * r1 - h2 * q2, r1 * R2)); // Добавили третье слагаемое


            return (I * points[5]); // Задали направление вдоль внешней нормали треугольника
        }

        // Переопределение функции Atan2 но с пределами как Atan
        private double Atan2(double y, double x)
        {
            if (x > 0.0d)
            {
                return Math.Atan(y / x);
            }
            if (x < 0.0d && y >= 0.0d)
            {
                return Math.Atan(y / x);
            }
            if (x < 0.0d && y < 0.0d)
            {
                return Math.Atan(y / x);
            }
            if (x == 0.0d && y > 0.0d)
            {
                return Math.PI / 2;
            }
            if (x == 0.0d && y < 0.0d)
            {
                return 3 * Math.PI / 2;
            }
            return 1;
            throw new ArgumentException("invalid atan2 arguments");
        }

        /// <summary>
        /// Обновляет значения поля на указанном профиле, на указанной высоте
        /// </summary>
        /// <param name="ProfIndex">Индекс профиля</param>
        /// <param name="HeigthIndex">Индексы высоты</param>
        /// <param name="points">Точка наблюдения</param>
        public void UpdateField(int ProfIndex, int HeigthIndex, List<Vector3> points)
        {
            if (ProfIndex == -1 | HeigthIndex == -1) return;

            var output = new List<Vector4>();

            foreach(var point in points)
            {
                double FieldX, FieldY, FieldZ;

                FieldX = Field(new Vector3(1, 0, 0), point);
                FieldY = Field(new Vector3(0, 1, 0), point);
                FieldZ = Field(new Vector3(0, 0, 1), point);

                Vector2 ProfileStart = Global.Profiles[ProfIndex].GetStartPoint();
                double dist = Math.Sqrt( (point.X - ProfileStart.x) * (point.X - ProfileStart.x) + (point.Y - ProfileStart.y) * (point.Y - ProfileStart.y));

                output.Add(new Vector4(FieldX, FieldY, FieldZ, dist));
            }
            Fields[ProfIndex][HeigthIndex] = output;
        }

        /// <summary>
        /// Добавляет новый профиль в массив полей
        /// </summary>
        public void AddProfile()
        {
            var Profile = new List<List<Vector4>>();

            for(int i = 0; i < Global.HeigthMaps.Count; i++)
            {
                Profile.Add(new List<Vector4>());
            }

            Fields.Add(Profile);
        }

        public void RemoveProfileFields(int index)
        {
            Fields.RemoveAt(index);
        }

        public List<Vector4> GetField(int ProfileIndex, int HeightIndex)
        {
             return Fields[ProfileIndex][HeightIndex];
        }
        public void UpdateFieldList()
        {
            Fields = new List<List<List<Vector4>>>();

            for(int i = 0; i < Global.Profiles.Count; i++)
            {
                AddProfile();
            }
        }

        public void UpdateParameters()
        {
            double x1 = Verticles[0].X, x2 = Verticles[1].X, x3 = Verticles[2].X, x4 = Verticles[3].X;
            double y1 = Verticles[0].Y, y2 = Verticles[1].Y, y3 = Verticles[2].Y, y4 = Verticles[3].Y;

            var y = (x2 - x1) * (y4 - y1) * (y3 - y2) + (x4 - x1) * (y3 - y2) * y1 - (x3 - x2) * (y4 - y1) * y2;
            
            y /= (x4 - x1) * (y3 - y2) - (x3 - x2) * (y4 - y1);

            var x = (y - y1) * (x4 - x1) / (y4 - y1) + x1;

            X = x;
            Y = y;

            b = (Verticles[1] - Verticles[0]).Getlength();

            d = (Verticles[2] - Verticles[0]).Getlength();

            var L_vec = (Verticles[4] - Verticles[0]);

            L = L_vec.Getlength();

            List<Vector2> points = new List<Vector2>
            {
                new Vector2(-b / 2, -d / 2),
                new Vector2(b / 2, -d / 2),
                new Vector2(-b / 2, d / 2)
            };

            List<double> Zs = new List<double>();

            for (int i = 0; i < 4; i++)
                Zs.Add(0);

            if (Global.Relief != null)
                Zs = Global.Relief.Interp(0, points);

            h1 = Verticles[0].Z + Zs[0];
            h2 = Verticles[1].Z + Zs[1];
            h3 = Verticles[2].Z + Zs[2];

            fi = Math.Asin(L_vec.Z / L) * 180 / Math.PI + 90;

            beta = Math.Asin(L_vec.Y / L / Math.Cos(fi * Math.PI / 180)) * 180 / Math.PI;
        }
    }
}