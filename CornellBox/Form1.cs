using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CornellBox
{
    public partial class Form1 : Form
    {
        Shape box;
        public List<Shape> scene = new List<Shape>();
        public Point camera;
        public List<Light> lights = new List<Light>();                     
        public Point[,] pixels;
        public Color[,] colors;

        public int height, width;

        public Form1()
        {
            InitializeComponent();
            height = cornellBox.Height;
            width = cornellBox.Width;
            cornellBox.Image = new Bitmap(width, height);

            colors = new Color[width, height];
            pixels = new Point[width, height];

            // комната
            box = Shape.createBox();

            // камера
            camera = Shape.boxCenter(box) + Face.getNormal(box.Faces[0]) * 20;

            // свет
            Light l = new Light(new Point(0, 0, 9.8), new Point(1, 1, 1));
            lights.Add(l);

            // большой куб слева
            Shape cube1 = Shape.createCube(3.5);
            cube1.shift(3, -2, -7);
            cube1.rotate(40, AxisType.Z);
            cube1.paint(Color.Pink);
            cube1.surface = new Surface(0, 0, 0.1, 0.8, 1);

            // маленький куб справа
            Shape cube2 = Shape.createCube(2);
            cube2.shift(3, -2, 0);
            cube2.rotate(50, AxisType.Z);
            cube2.rotate(-15, AxisType.X);
            cube2.paint(Color.Purple);
            cube2.surface = new Surface(0, 0.5, 0.15, 0.6, 1);

            // добавляем все в комнату
            scene.AddRange(new List<Shape> { box, cube1, cube2 });

            backwardRayTracing();
        }

        public void backwardRayTracing()
        {
            // шаг по ширине
            Point widthRatio = (box.Faces[0][1] - box.Faces[0][0]) / width;
            // шаг по высоте
            Point heightRatio = (box.Faces[0][0] - box.Faces[0][3]) / height;
            // начальная точка
            Point start = new Point(box.Faces[0][3]);

            // идем по пикселям видового окна
            for (int i = 0; i < width; i++)
            {
                Point curPixel = new Point(start);
                for (int j = 0; j < height; j++)
                {
                    // пиксель сцены
                    pixels[i, j] = curPixel;

                    Ray ray = new Ray(camera, curPixel);
                    // т.к. рейтрейсинг обратный - луч идет из пикселя
                    ray.position = new Point(curPixel);

                    Point color = Ray.RayTracing(scene, lights, ray, 10, 1) * 255;

                    if (color.x > 255 || color.y > 255 || color.z > 255)
                        color = Point.getNormal(color) * 255;

                    // цвет пикселя после рейтрейсинга
                    colors[i, j] = Color.FromArgb((int)color.x, (int)color.y, (int)color.z);

                    curPixel += heightRatio;
                }
                start += widthRatio;
            }

            // выводим сцену на экран
            for (int i = 0; i < width; ++i)
                for (int j = 0; j < height; ++j)
                    (cornellBox.Image as Bitmap).SetPixel(i, j, colors[i, j]);
        }

    }
}

