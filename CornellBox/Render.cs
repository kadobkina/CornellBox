using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CornellBox
{
    // свет
    public class Light
    {
        public Point position; // позиция света
        public Point intensity; // интенсивность света

        public Light(Point p, Point i)
        {
            position = new Point(p);
            intensity = new Point(i);
        }
    }

    // поверхность
    public class Surface
    {
        public double reflection;    // отражение
        public double refraction;    // преломление
        public double ambient;       // фоновое освещение
        public double diffuse;       // диффузное (локальное) освещение
        public double environment;   // преломление среды

        public Point color;       // цвет покрытия

        public Surface(double reflect, double refract, double amb, double diff, double env, Point col = null)
        {
            reflection = reflect;
            refraction = refract;
            ambient = amb;
            diffuse = diff;
            environment = env;
            color = col;
        }

        public Surface(Surface s)
        {
            reflection = s.reflection;
            refraction = s.refraction;
            ambient = s.ambient;
            diffuse = s.diffuse;
            environment = s.environment;
            color = new Point(s.color);
        }
    }

    // луч
    public class Ray
    {
        public Point position; // основание луча
        public Point direction; // направление луча

        public Ray(Point from, Point to)
        {
            position = new Point(from);
            direction = Point.getNormal(to - from);
        }

        public Ray(Ray r)
        {
            position = r.position;
            direction = r.direction;
        }

        // Направление отраженного луча определяется по закону:
        // R = I - 2N(N*I), где (N*I) - скалярное произведение, R - отраженный луч, I- падающий первичный луч, N - вектор нормали
        public Ray reflectRay(Point pixel, Point normal)
        {
            Point reflectDirection = direction - 2 * normal * (float)Point.Scalar(direction, normal); // direction - I, normal - N
            return new Ray(pixel, pixel + reflectDirection);
        }

        // Направление для преломлённого луча определяется следующим образом: 
        // T = (n1/n2)*I - (cost + (n1/n2)*(N*I))*N, где (N*I) - скалярное произведение, T - преломленный луч, I - падающий первичный луч, N - вектор нормали
        // n1 и n2 – коэффициенты рефракции для первой среды (в которой распространяется первичный луч) и второй среды прозрачного объекта, t - угол между N и T
        public Ray refractRay(Point pixel, Point normal, double refractionCoef1, double refractionCoef2)
        {
            /*            double NIscalar = Point.Scalar(normal, direction); // (N*I), direction - I, normal - N
                        double n1divn2 = refractionCoef1 / refractionCoef2; // n1/n2, refractionCoef1 - n1, refractionCoef2 - n2
                        double cost = Math.Sqrt(1 - n1divn2 * n1divn2 * (1 - NIscalar * NIscalar)); // cost, t - угол между N и T

                        return new Ray(pixel, Point.getNormal((float)n1divn2 * direction - (float)(cost + n1divn2 * NIscalar) * normal)); // (n1/n2)*I - (cost + (n1/n2)*(N*I))*N*/

            Ray res_ray;
            double sclr = Point.Scalar(normal, direction);
            /*
             Если луч падает,то он проходит прямо,не преломляясь
             */
            double n1n2div = refractionCoef1 / refractionCoef2;
            double theta_formula = 1 - n1n2div * n1n2div * (1 - sclr * sclr);
            if (theta_formula >= 0)
            {
                float cos_theta = (float)Math.Sqrt(theta_formula);
                res_ray = new Ray(new Point(pixel), Point.getNormal(direction * (float)n1n2div - (float)(cos_theta + n1n2div * sclr) * normal));
                return res_ray;
            }
            else
                return null;
        }

        /// <summary>
        /// Видимость из источника света точки пересечения луча с фигурой
        /// </summary>
        /// <param name="scene"> Сцена с фигурами </param>
        /// <param name="pixel"> Точка пересечения луча с фигурой </param>
        /// <param name="light"> Луч света </param>
        /// <returns></returns>
        public static bool pixelsVisibleFromLightSource(List<Shape> scene, Point pixel, Point light)
        {
            Ray r = new Ray(pixel, light); // луч из пикселя в источник света

            foreach (Shape shape in scene)
            {
                if (shape.rayIntersectsShape(r, out Point normal, out double intersectPixel)) // если луч пересекает какую-то фигуру на сцене
                    if (Geometry.InRange(intersectPixel, 0.0001f, (light - pixel).Length())) // если луч не выходит за пределы видимой из камеры области сцены
                        return false; // точка пересечения заслонена
            }
            return true;
        }

        /// <summary>
        /// Локальная модель освещения
        /// </summary>
        /// <param name="light"> Луч света </param>
        /// <param name="pixel"> Пиксель грани, с которым пересекается луч </param>
        /// <param name="normal"> Нормаль грани, с которой пересекается луч </param>
        /// <param name="serfColor"> Цвет поверхности </param>
        /// <param name="diffCoef"> Коэффициент диффузного освещения </param>
        /// <returns></returns>
        public static Point localLight(Light light, Point pixel, Point normal, Point serfColor, double diffCoef)
        {
            // если угол между лучом, идущим в текущий пиксель, и нормалью тупой, то умножаем на 0, т.к. локальное освещение нулевое
            Point diffuse = (float)diffCoef * (float)Math.Max(Point.Scalar(normal, Point.getNormal(light.position - pixel)), 0) * light.intensity;
            // применяем локальное освещение к текущему цвету поверхности фигуры
            return new Point(diffuse.x * serfColor.x, diffuse.y * serfColor.y, diffuse.z * serfColor.z);
        }

        public static Point RayTracing(List<Shape> scene, List<Light> lights, Ray r, int iteration, double envir)
        {
            if (iteration <= 0)
                return new Point(0, 0, 0);

            // итоговый цвет пикселя после трассировки лучей
            Point newColor = new Point(0, 0, 0);

            // пиксель пересечения луча и фигуры
            double intersectionPixel = 0;
            // нормаль грани, пересеченной лучом
            Point normal = null;

            // поверхность грани
            Surface surface = null;

            foreach (Shape shape in scene)
            {
                if (shape.rayIntersectsShape(r, out Point norm, out double t)) // если луч пересекает какую-то фигуру на сцене
                    if (t < intersectionPixel || intersectionPixel == 0) // ищем ближайшую фигуру
                    {
                        intersectionPixel = t;
                        normal = norm;
                        surface = new Surface(shape.surface);
                    }
            }

            // текущий пиксель пересечения луча и фигурой
            Point curPixel = r.position + r.direction * (float)intersectionPixel;

            if (intersectionPixel == 0) // если луч не пересекает фигуру, то он уходит в свободное пространство
                return new Point(0, 0, 0);

            // определение среды
            if (Point.Scalar(r.direction, normal) > 0)
                normal *= -1;

            foreach (Light light in lights)
            {
                // фоновый ambient цвет 
                Point ambCoef = light.intensity * (float)surface.ambient;
                ambCoef = new Point(ambCoef.x * surface.color.x, ambCoef.y * surface.color.y, ambCoef.z * surface.color.z);
                newColor += ambCoef; // Если точка закрыта от освещения всеми источниками сцены, ей присваивается фоновый ambient цвет

                if (pixelsVisibleFromLightSource(scene, curPixel, light.position))
                    newColor += localLight(light, curPixel, normal, surface.color, surface.diffuse);
            }

            // если фигура зеркальная
            if (surface.reflection !=0)
                newColor += (float)surface.reflection * RayTracing(scene, lights, r.reflectRay(curPixel, normal), iteration - 1, envir);

            return newColor;
        }
    }

}
