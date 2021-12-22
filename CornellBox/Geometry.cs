using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CornellBox
{
    /// <summary>
    /// Тип координатной прямой (для поворотов)
    /// </summary>
    public enum AxisType { X, Y, Z };

    /// <summary>
    /// Типы фигур
    /// </summary>
    public enum ShapeType { Cube, Sphere };

    // точка
    public class Point
    {
        public double x, y, z;

        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Point(Point other)
        {
            if (other == null)
                return;
            this.x = other.x;
            this.y = other.y;
            this.z = other.z;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) 
                return false;
            Point objAsPart = obj as Point;
            return objAsPart == null ? false : Equals(objAsPart);
        }

        public bool Equals(Point other)
        {
            if (other == null) return false;
            return (this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z));
        }

        public float Length()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        public static double Scalar(Point p1, Point p2)
        {
            return p1.x * p2.x + p1.y * p2.y + p1.z * p2.z;
        }

        public static Point getNormal(Point p)
        {
            float len = p.Length();
            return len == 0 ? new Point(p) : new Point(p.x / len, p.y / len, p.z / len);
        }

        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);

        }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.x + p2.x, p1.y + p2.y, p1.z + p2.z);

        }

        public static Point operator *(Point p1, Point p2)
        {
            return new Point(p1.y * p2.z - p1.z * p2.y, p1.z * p2.x - p1.x * p2.z, p1.x * p2.y - p1.y * p2.x);
        }

        public static Point operator *(float a, Point p1)
        {
            return new Point(p1.x * a, p1.y * a, p1.z * a);
        }


        public static Point operator *(Point p1, float a)
        {
            return new Point(a*p1);
        }

        public static Point operator -(Point p1, float a)
        {
            return new Point(p1.x - a, p1.y - a, p1.z - a);
        }

        public static Point operator -(float a, Point p1)
        {
            return new Point(a - p1.x, a - p1.y, a - p1.z);
        }

        public static Point operator +(Point p1, float a)
        {
            return new Point(p1.x + a, p1.y + a, p1.z + a);
        }

        public static Point operator +(float a, Point p1)
        {
            return new Point(p1 + a);
        }

        public static Point operator /(Point p1, float a)
        {
            return new Point(p1.x / a, p1.y / a, p1.z / a);
        }

        public static Point operator /(float a, Point p1)
        {
            return new Point(a / p1.x, a / p1.y, a / p1.z);
        }
    }

    // грань
    public class Face
    {
        public Shape curShape = null;
        public List<int> Vertices = new List<int>();
        public Color color = Color.Black;
        public Point normVector;

        public Face(Shape shape = null)
        {
            curShape = shape;
        }

        public Face(Face face)
        {
            Vertices = new List<int>(face.Vertices);
            curShape = face.curShape;
            color = face.color;
            normVector = new Point(face.normVector);
        }

        public static Point getNormal(Face face)
        {

            return Point.getNormal((face[1] - face[0]) * (face[3] - face[0]));
        }

        public Point this[int ind]
        {
            get
            {
                return curShape.Vertices[Vertices[ind]];
            }

        }
    }

    // фигура
    public class Shape
    {
        public ShapeType shapeType;
        public List<Point> Vertices = new List<Point>(); 
        public List<Face> Faces = new List<Face>();        
        public Surface surface;

        //для шара
        public double diameter = 0;

        public Shape(ShapeType st) 
        {
            shapeType = st;
        }
        public Color color = Color.Red;

        public Shape(Shape shape)
        {
            shapeType = shape.shapeType;

            foreach (Point p in shape.Vertices)
                Vertices.Add(new Point(p));


            foreach (Face s in shape.Faces)
            {
                Face face = new Face(s);
                face.color = s.color;
                Faces.Add(face);
            }

            if (shape.shapeType == ShapeType.Sphere)
                diameter = shape.diameter;
        }

        // центр комнаты
        static public Point boxCenter(Shape shape)
        {
            return (shape.Faces[0][0] + shape.Faces[0][1] + shape.Faces[0][2] + shape.Faces[0][3]) / 4;
        }

        // цвет фигуры
        public void paint(Color c)
        {
            foreach (Face s in Faces)
                s.color = c;
        }


        /*           e4********f5
                    **        **
                   * *       * *
                  a0********b1 *
                  *  h7********g6
                  * *       * *
                  *         **
                  d3********c2
         */

        // создает комнату
        static public Shape createBox()
        {
            Shape shape = new Shape(ShapeType.Cube);
            Point a = new Point(10, 10, 10);
            Point b = new Point(-10, 10, 10);
            Point c = new Point(-10, 10, -10);
            Point d = new Point(10, 10, -10);
            Point e = new Point(10, -10, 10);
            Point f = new Point(-10, -10, 10);
            Point g = new Point(-10, -10, -10);
            Point h = new Point(10, -10, -10);

            shape.Vertices.AddRange(new Point[] { a, b, c, d, e, f, g, h });

            // передняя
            Face front = new Face(shape);
            front.color = Color.MediumPurple;
            front.Vertices.AddRange(new int[] { 3, 2, 1, 0 });
            // задняя
            Face back = new Face(shape);
            back.Vertices.AddRange(new int[] { 4, 5, 6, 7 });
            back.color = Color.IndianRed;
            // верхняя
            Face top = new Face(shape);
            top.Vertices.AddRange(new int[] { 1, 5, 4, 0 });
            top.color = Color.Gray;
            // нижняя
            Face bottom = new Face(shape);
            bottom.Vertices.AddRange(new int[] { 2, 3, 7, 6 });
            bottom.color = Color.HotPink;
            // правая
            Face right = new Face(shape);
            right.Vertices.AddRange(new int[] { 2, 6, 5, 1 });
            right.color = Color.DeepPink;
            // левая
            Face left = new Face(shape);
            left.Vertices.AddRange(new int[] { 0, 4, 7, 3 });
            left.color = Color.Purple;


            shape.Faces.Add(front);
            shape.Faces.Add(back);
            shape.Faces.Add(right);
            shape.Faces.Add(left);
            shape.Faces.Add(top);
            shape.Faces.Add(bottom);

            shape.surface = new Surface(0, 0, 0.03, 0.8, 1);

            return shape;
        }

        // создает куб
        static public Shape createCube(double size)
        {
            Shape cube = new Shape(ShapeType.Cube);
            Point a = new Point(size, size, size);
            Point b = new Point(-size, size, size);
            Point c = new Point(-size, size, -size);
            Point d = new Point(size, size, -size);
            Point e = new Point(size, -size, size);
            Point f = new Point(-size, -size, size);
            Point g = new Point(-size, -size, -size);
            Point h = new Point(size, -size, -size);

            cube.Vertices.AddRange(new Point[] { a, b, c, d, e, f, g, h });

            // передняя
            Face front = new Face(cube);
            front.Vertices.AddRange(new int[] { 3, 2, 1, 0 });
            // задняя
            Face back = new Face(cube);
            back.Vertices.AddRange(new int[] { 4, 5, 6, 7 });
            // верхняя
            Face top = new Face(cube);
            top.Vertices.AddRange(new int[] { 1, 5, 4, 0 });
            // нижняя
            Face bottom = new Face(cube);
            bottom.Vertices.AddRange(new int[] { 2, 3, 7, 6 });
            // правая
            Face right = new Face(cube);
            right.Vertices.AddRange(new int[] { 2, 6, 5, 1 });
            // левая
            Face left = new Face(cube);
            left.Vertices.AddRange(new int[] { 0, 4, 7, 3 });

            cube.Faces.Add(front);
            cube.Faces.Add(back);
            cube.Faces.Add(right);
            cube.Faces.Add(left);
            cube.Faces.Add(top);
            cube.Faces.Add(bottom);

            return cube;
        }

        // создает шар
        static public Shape createSphere(Point pos, double diam)
        {
            Shape sphere = new Shape(ShapeType.Sphere);
            //sphere.position = pos;
            Face front = new Face(sphere);
            front.Vertices.Add(0);
            sphere.Vertices.Add(pos);
            sphere.diameter = diam;
            return sphere;
        }


            /// <summary>
            /// Преобразует все точки в фигуре по заданной функции
            /// </summary>
            /// <param name="f"> Функция, преобразующая точку фигуры </param>
            public void transformPoints(Func<Point, Point> f)
        {
            this.Vertices = this.Vertices.Select(x => f(x)).ToList();
        }

        /// <summary>
        /// Повернуть фигуру на заданный угол вокруг заданной оси
        /// </summary>
        /// <param name="type"> Ось, вокруг которой поворачиваем </param>
        /// <param name="angle"> Угол поворота в градусах </param>
        public void rotate(double angle, AxisType type)
        {
            Matrix rotation = new Matrix(0, 0);

            switch (type)
            {
                case AxisType.X:
                    rotation = new Matrix(4, 4).fill(1, 0, 0, 0, 0, Geometry.Cos(angle), -Geometry.Sin(angle), 0, 0, Geometry.Sin(angle), Geometry.Cos(angle), 0, 0, 0, 0, 1);
                    break;
                case AxisType.Y:
                    rotation = new Matrix(4, 4).fill(Geometry.Cos(angle), 0, Geometry.Sin(angle), 0, 0, 1, 0, 0, -Geometry.Sin(angle), 0, Geometry.Cos(angle), 0, 0, 0, 0, 1);
                    break;
                case AxisType.Z:
                    rotation = new Matrix(4, 4).fill(Geometry.Cos(angle), -Geometry.Sin(angle), 0, 0, Geometry.Sin(angle), Geometry.Cos(angle), 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
                    break;
            }

            this.transformPoints((Point p) =>
            {
                var res = rotation * new Matrix(4, 1).fill(p.x, p.y, p.z, 1);
                return new Point(res[0, 0], res[1, 0], res[2, 0]);
            });
        }

        /// <summary>
        /// Сдвинуть фигуру на заданные расстояния
        /// </summary>
        /// <param name="shape"> Фигура </param>
        /// <param name="dx"> Сдвиг по оси X </param>
        /// <param name="dy"> Сдвиг по оси Y </param>
        /// <param name="dz"> Сдвиг по оси Z </param>
        public void shift(double dx, double dy, double dz)
        {
            Matrix shift = new Matrix(4, 4).fill(1, 0, 0, dx, 0, 1, 0, dy, 0, 0, 1, dz, 0, 0, 0, 1);
            this.transformPoints((Point p) =>
            {
                var res = shift * new Matrix(4, 1).fill(p.x, p.y, p.z, 1);
                return new Point(res[0, 0], res[1, 0], res[2, 0]);
            });
        }

        /// <summary>
        /// https://www.scratchapixel.com/lessons/3d-basic-rendering/ray-tracing-rendering-a-triangle/ray-triangle-intersection-geometric-solution
        /// </summary>
        /// <param name="r"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="intersectPixel"></param>
        /// <returns></returns>
        public bool rayIntersectsTriangle(Ray r, Point p0, Point p1, Point p2, out double intersectPixel)
        {
            intersectPixel = -1;
            // compute plane's normal
            Point edge1 = p1 - p0;
            Point edge2 = p2 - p0;
            Point h = r.direction * edge2;

            // check if ray and plane are parallel ?
            double a = Point.Scalar(edge1, h);
            if (Geometry.InRange(a, -0.0001f, 0.0001f))
                return false;

            // compute d parameter using equation 2
            double d = 1.0 / a;
            Point s = r.position - p0;

            // check if the triangle is in behind the ray
            double u = d * Point.Scalar(s, h);
            if (u < 0 || u > 1)
                return false;

            // compute the intersection point using equation 1
            Point q = s * edge1;
            double v = d * Point.Scalar(r.direction, q);
            // check if the triangle is in behind the ray
            if (v < 0 || u + v > 1)
                return false;
            double t = d * Point.Scalar(edge2, q);
            if (t > 0.0001)
            {
                intersectPixel = t;
                return true;
            }
            else  
                return false;
        }

        /// <summary>
        /// https://viclw17.github.io/2018/07/16/raytracing-ray-sphere-intersection/
        /// </summary>
        /// <param name="r"></param>
        /// <param name="pos"> Позиция центра шара </param>
        /// <param name="diam"> Диаметр шара </param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool raySphereIntersection(Ray r, Point pos, double diam, out double intersectPixel)
        {
            intersectPixel = 0;

            Point oc = r.position - pos;
            double a = Point.Scalar(r.direction, r.direction);
            double b = 2.0 * Point.Scalar(oc, r.direction);
            double c = Point.Scalar(oc, oc) - Math.Pow(diam/2,2);
            double discriminant = b * b - 4*a*c;

            if (discriminant >= 0)
            {
                double temp1 = (-b + Math.Sqrt(discriminant)) / 2.0 * a;
                double temp2 = (-b - Math.Sqrt(discriminant)) / 2.0 * a;

                intersectPixel = Math.Min(temp1, temp2); 

                if (intersectPixel <= 0.0001)
                    intersectPixel = Math.Max(temp1, temp2);

                return intersectPixel > 0.0001;
            }
            return false;
        }

        /// <summary>
        /// Пересекает ли луч фигуру
        /// </summary>
        /// <param name="r"></param>
        /// <param name="normal"></param>
        /// <param name="intersectPixel"></param>
        /// <returns></returns>
        public virtual bool rayIntersectsShape(Ray r, out Point normal, out double intersectPixel)
        {
            intersectPixel = 0;
            normal = null;
            if (this.shapeType == ShapeType.Cube)
            {
                Face face = null;
                foreach (Face curFace in Faces)
                {
                    if (rayIntersectsTriangle(r, curFace[0], curFace[1], curFace[3], out double t) || rayIntersectsTriangle(r, curFace[1], curFace[2], curFace[3], out t))
                    {
                        if (intersectPixel == 0 || t < intersectPixel) // ищем ближайшую фигуру
                        {
                            intersectPixel = t;
                            face = curFace;
                        }
                    }
                }
                // если луч пересекает фигуру
                if (intersectPixel != 0)
                {
                    normal = Face.getNormal(face);
                    surface.color = new Point(face.color.R, face.color.G, face.color.B) / 255;
                    return true;
                }
                return false;
            }
            else
            {
                if (raySphereIntersection(r, Vertices[0], diameter, out intersectPixel))
                {
                    if (intersectPixel > 0.0001)
                    {
                        normal = Point.getNormal((r.position + r.direction * (float)intersectPixel) - Vertices[0]);
                        return true;
                    }
                }
                return false;
            }
        }
    }

    public class Geometry
    {
        /// <summary>
        /// el находится в диапазоне между border1 и border2
        /// </summary>
        /// <param name="el"></param>
        /// <param name="border1"></param>
        /// <param name="border2"></param>
        /// <returns></returns>
        public static bool InRange(double el, float border1, float border2)
        {
            return el > border1 && el < border2;
        }

        /// <summary>
        /// Переводит угол из градусов в радианы
        /// </summary>
        /// <param name="angle"> Угол в градусах </param>
        ///
        public static double degreesToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static double radiansToDegrees(double angle)
        {
            return angle * 180 / Math.PI;
        }

        /// <summary>
        /// Косинус из угла в градусах, ограниченный 5 знаками после запятой
        /// </summary>
        /// <param name="angle"> Угол в градусах </param>
        /// <returns></returns>
        public static double Cos(double angle)
        {
            return Math.Cos(degreesToRadians(angle));
        }
        /// <summary>
        /// Синус из угла в градусах, ограниченный 5 знаками после запятой
        /// </summary>
        /// <param name="angle"> Угол в градусах </param>
        /// <returns></returns>
        public static double Sin(double angle)
        {
            return Math.Sin(degreesToRadians(angle));
        }
    }

    public class Vector
    {
        public double x, y, z;
        public Vector(double x, double y, double z, bool isVectorNeededToBeNormalized = false)
        {
            double normalization = isVectorNeededToBeNormalized ? Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2)) : 1;
            this.x = x / normalization;
            this.y = y / normalization;
            this.z = z / normalization;
        }

        public Vector(Point p, bool isVectorNeededToBeNormalized = false) : this(p.x, p.y, p.z, isVectorNeededToBeNormalized)
        {

        }

        public Vector normalize()
        {
            double normalization = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
            x = x / normalization;
            y = y / normalization;
            z = z / normalization;
            return this;
        }

        public int X { get => (int)x; set => x = value; }
        public int Y { get => (int)y; set => y = value; }
        public int Z { get => (int)z; set => z = value; }

        public double Xf { get => x; set => x = value; }
        public double Yf { get => y; set => y = value; }
        public double Zf { get => z; set => z = value; }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }
        public static Vector operator *(Vector v, float a)
        {
            return new Vector(v.x * a, v.y * a, v.z * a);
        }

        public static Vector operator *(double k, Vector b)
        {
            return new Vector(k * b.x, k * b.y, k * b.z);
        }
    }
}
