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
        public Shape curShape;

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
            Light light1 = new Light(new Point(0, 0, 9.8), new Point(1, 1, 1)); // центр комнаты
            Light light2 = new Light(new Point(9.8, -9.8, 9.8), new Point(1, 1, 1)); // дальний левый угол
            Light light3 = new Light(new Point(0, 5, 9.8), new Point(1, 1, 1)); // центр ближе к камере
            lights.AddRange(new List<Light> { /*light1, light2,*/ light3 });

            // большой куб слева
            Shape cube1 = Shape.createCube(3.5);
            cube1.shift(3, -2, -7);
            cube1.rotate(40, AxisType.Z);
            cube1.paint(Color.Plum);
            cube1.surface = new Surface(0, 0, 0.1, 0.8, 1);

            // маленький куб справа
            Shape cube2 = Shape.createCube(2);
            cube2.shift(3, -2, 0);
            cube2.rotate(50, AxisType.Z);
            cube2.rotate(-15, AxisType.X);
            cube2.paint(Color.Purple);
            cube2.surface = new Surface(0, 0, 0.15, 0.6, 1);

            // шар большой дальний
            Shape sphere1 = Shape.createSphere(new Point(-7, -2, -7), 6);
            sphere1.surface = new Surface(0, 0, 0, 0.6, 1, new Point(Color.MediumOrchid.R, Color.MediumOrchid.G, Color.MediumOrchid.B));

            // шар маленький ближний
            Shape sphere2 = Shape.createSphere(new Point(0, 5, -8), 3);
            sphere2.surface = new Surface(0.9, 0, 0, 0.6, 1);

            // добавляем все в комнату
            scene.AddRange(new List<Shape> { box, cube1, cube2, sphere1, sphere2 });

            backwardRayTracing();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (listBox1.SelectedItem.ToString())
            {
                case "Большой куб":
                    curShape = scene[1];
                    break;
                case "Маленький куб":
                    curShape = scene[2];
                    break;
                case "Большая сфера":
                    curShape = scene[3];
                    break;
                case "Маленькая сфера":
                    curShape = scene[4];
                    break;
                default:                 
                    break;
            }

            buttonMatt.Enabled = true;
            buttonMirror.Enabled = true;
        }

        private void buttonMirror_Click(object sender, EventArgs e)
        {
            if (curShape == scene[3])
                scene[3].surface = new Surface(0.9, 0, 0, 0.1, 1);
            else if (curShape == scene[4])
                scene[4].surface = new Surface(0.9, 0, 0, 0.1, 1);
            else if (curShape == scene[1])
                scene[1].surface.reflection = 1;
            else if (curShape == scene[2])
                scene[2].surface.reflection = 1;

            backwardRayTracing();
        }

        private void buttonMatt_Click(object sender, EventArgs e)
        {
            if (curShape == scene[3])
            {
                scene[3].surface.reflection = 0;
                scene[3].surface.color = new Point(Color.MediumOrchid.R, Color.MediumOrchid.G, Color.MediumOrchid.B);
            }
            else if (curShape == scene[4])
            {
                scene[4].surface.reflection = 0;
                scene[4].surface.color = new Point(Color.PaleVioletRed.R, Color.PaleVioletRed.G, Color.PaleVioletRed.B);
            }
            else if (curShape == scene[1])
                scene[1].surface.reflection = 0;
            else if(curShape == scene[2])
                scene[2].surface.reflection = 0;

            backwardRayTracing();
        }

        private void checkBoxCenter_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCenter.Checked)
                lights.Add(new Light(new Point(0, 0, 9.8), new Point(1, 1, 1))); // центр комнаты
            else
                lights.Remove(lights.Find(x => x.position.Equals(new Point(0, 0, 9.8))));
        }

        private void checkBoxCenterCloser_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCenterCloser.Checked)
                lights.Add(new Light(new Point(0, 5, 9.8), new Point(1, 1, 1))); // центр ближе к камере
            else
                lights.Remove(lights.Find(x => x.position.Equals(new Point(0, 5, 9.8))));
        }

        private void checkBoxLeftBack_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLeftBack.Checked)
                lights.Add(new Light(new Point(9.8, -9.8, 9.8), new Point(1, 1, 1))); // дальний левый угол
            else
                lights.Remove(lights.Find(x => x.position.Equals(new Point(9.8, -9.8, 9.8))));
        }

        private void checkBoxRightBack_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRightBack.Checked)
                lights.Add(new Light(new Point(-9.8, -9.8, 9.8), new Point(1, 1, 1))); // дальний правый угол
            else
                lights.Remove(lights.Find(x => x.position.Equals(new Point(-9.8, -9.8, 9.8))));
        }

        private void checkBoxLeftFront_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLeftFront.Checked)
                lights.Add(new Light(new Point(9.8, 9.8, 9.8), new Point(1, 1, 1))); // ближний левый угол
            else
                lights.Remove(lights.Find(x => x.position.Equals(new Point(9.8, 9.8, 9.8))));
        }

        private void checkBoxRightFront_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRightFront.Checked)
                lights.Add(new Light(new Point(-9.8, 9.8, 9.8), new Point(1, 1, 1))); // ближний правый угол
            else
                lights.Remove(lights.Find(x => x.position.Equals(new Point(-9.8, 9.8, 9.8))));
        }

        // изменить свет
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            backwardRayTracing();
        }

        public void backwardRayTracing()
        {
            cornellBox.Image = new Bitmap(width, height);

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

