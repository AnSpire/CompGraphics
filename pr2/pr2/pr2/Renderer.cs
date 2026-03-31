using System;
using System.Drawing;
// Используем алиас для избежания конфликта имён
using GL = OpenTK.Graphics.OpenGL.GL;

namespace pr2
{
    /// <summary>
    /// Класс-рендерер, содержащий методы отрисовки различных фигур.
    /// Все методы используют старый добрый glBegin/glEnd для наглядности.
    /// </summary>
    public class Renderer
    {
        // Угол поворота для 3D куба
        public float RotationAngle { get; set; } = 0f;

        /// <summary>
        /// Пример 1: Треугольник в 3D (аналог GameWindow примера)
        /// Используется перспективная проекция и Vector3 для вершин и цветов.
        /// </summary>
        public void DrawTriangleExample1(int width, int height)
        {
            // Настройка проекции
            OpenTK.Matrix4 projection = OpenTK.Matrix4.CreatePerspectiveFieldOfView(
                (float)Math.PI / 4,
                width / (float)height,
                1.0f,
                64.0f
            );
            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            // Настройка вида (камера смотрит на сцену)
            OpenTK.Matrix4 modelview = OpenTK.Matrix4.LookAt(
                OpenTK.Vector3.Zero,           // позиция камеры
                OpenTK.Vector3.UnitZ,          // точка, куда смотрим
                OpenTK.Vector3.UnitY           // направление "вверх"
            );
            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            // Рисуем треугольник
            GL.Begin(OpenTK.Graphics.OpenGL.PrimitiveType.Triangles);

            // Используем Vector3 для цвета и вершин (требование задания)
            OpenTK.Vector3 clr = new OpenTK.Vector3(1.0f, 1.0f, 0.0f);  // жёлтый
            OpenTK.Vector3 p = new OpenTK.Vector3(-1.0f, -1.0f, 4.0f);
            GL.Color3(clr.X, clr.Y, clr.Z);
            GL.Vertex3(p.X, p.Y, p.Z);

            clr = new OpenTK.Vector3(1.0f, 0.0f, 0.0f);  // красный
            p = new OpenTK.Vector3(1.0f, -1.0f, 4.0f);
            GL.Color3(clr.X, clr.Y, clr.Z);
            GL.Vertex3(p.X, p.Y, p.Z);

            clr = new OpenTK.Vector3(0.2f, 0.9f, 1.0f);  // голубой
            p = new OpenTK.Vector3(0.0f, 1.0f, 4.0f);
            GL.Color3(clr.X, clr.Y, clr.Z);
            GL.Vertex3(p.X, p.Y, p.Z);

            GL.End();
        }

        /// <summary>
        /// Пример 2: Треугольник в 2D (аналог примера с обработчиками событий)
        /// Используется ортографическая проекция и System.Drawing.Color.
        /// </summary>
        public void DrawTriangleExample2()
        {
            // Ортографическая проекция для 2D
            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Begin(OpenTK.Graphics.OpenGL.PrimitiveType.Triangles);

            // Используем System.Drawing.Color (требование задания)
            GL.Color3(Color.MidnightBlue);
            GL.Vertex2(-1.0f, 1.0f);

            GL.Color3(Color.SpringGreen);
            GL.Vertex2(0.0f, -1.0f);

            GL.Color3(Color.Ivory);
            GL.Vertex2(1.0f, 1.0f);

            GL.End();
        }

        /// <summary>
        /// Рисует квадрат в 2D
        /// </summary>
        public void DrawSquare()
        {
            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Begin(OpenTK.Graphics.OpenGL.PrimitiveType.Quads);

            GL.Color3(Color.Green);
            GL.Vertex2(-0.5f, -0.5f);

            GL.Color3(Color.Green);
            GL.Vertex2(0.5f, -0.5f);

            GL.Color3(Color.Green);
            GL.Vertex2(0.5f, 0.5f);

            GL.Color3(Color.Green);
            GL.Vertex2(-0.5f, 0.5f);

            GL.End();
        }

        /// <summary>
        /// Рисует правильный многоугольник (шестиугольник)
        /// </summary>
        public void DrawPolygon()
        {
            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview);
            GL.LoadIdentity();

            int sides = 6;  // шестиугольник
            float radius = 0.6f;

            GL.Begin(OpenTK.Graphics.OpenGL.PrimitiveType.Polygon);

            for (int i = 0; i < sides; i++)
            {
                double angle = 2 * Math.PI * i / sides;
                float x = (float)(radius * Math.Cos(angle));
                float y = (float)(radius * Math.Sin(angle));

                // Разноцветные вершины для наглядности
                GL.Color3(
                    (byte)(255 * (i % 3 == 0 ? 1 : 0)),
                    (byte)(255 * (i % 3 == 1 ? 1 : 0)),
                    (byte)(255 * (i % 3 == 2 ? 1 : 0))
                );
                GL.Vertex2(x, y);
            }

            GL.End();
        }

        /// <summary>
        /// Рисует круг с помощью множества маленьких отрезков
        /// </summary>
        public void DrawCircle()
        {
            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview);
            GL.LoadIdentity();

            int segments = 36;  // количество сегментов для аппроксимации круга
            float radius = 0.6f;

            GL.Begin(OpenTK.Graphics.OpenGL.PrimitiveType.Polygon);

            for (int i = 0; i < segments; i++)
            {
                double angle = 2 * Math.PI * i / segments;
                float x = (float)(radius * Math.Cos(angle));
                float y = (float)(radius * Math.Sin(angle));

                GL.Color3(Color.OrangeRed);
                GL.Vertex2(x, y);
            }

            GL.End();

            // Рисуем обводку круга
            GL.Begin(OpenTK.Graphics.OpenGL.PrimitiveType.LineLoop);

            for (int i = 0; i < segments; i++)
            {
                double angle = 2 * Math.PI * i / segments;
                float x = (float)(radius * Math.Cos(angle));
                float y = (float)(radius * Math.Sin(angle));

                GL.Color3(Color.White);
                GL.Vertex2(x, y);
            }

            GL.End();
        }

        /// <summary>
        /// Рисует вращающийся 3D куб с разноцветными гранями
        /// </summary>
        public void Draw3DCube(int width, int height)
        {
            // Очистка буфера глубины обязательна для 3D
            GL.Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.DepthBufferBit);
            GL.Enable(OpenTK.Graphics.OpenGL.EnableCap.DepthTest);

            // Настройка перспективной проекции
            OpenTK.Matrix4 projection = OpenTK.Matrix4.CreatePerspectiveFieldOfView(
                (float)Math.PI / 4,
                width / (float)height,
                1.0f,
                64.0f
            );
            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            // Настройка камеры
            OpenTK.Matrix4 modelview = OpenTK.Matrix4.LookAt(
                new OpenTK.Vector3(0, 0, 5),    // камера на расстоянии 5 единиц
                OpenTK.Vector3.Zero,             // смотрим в центр
                OpenTK.Vector3.UnitY             // Y направлен вверх
            );

            // Добавляем вращение куба
            modelview = OpenTK.Matrix4.Mult(modelview, OpenTK.Matrix4.CreateRotationY(RotationAngle));
            modelview = OpenTK.Matrix4.Mult(modelview, OpenTK.Matrix4.CreateRotationX(RotationAngle * 0.5f));

            GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            // Рисуем куб из 6 граней
            GL.Begin(OpenTK.Graphics.OpenGL.PrimitiveType.Quads);

            // Передняя грань - красная
            GL.Color3(Color.Red);
            GL.Vertex3(-1, -1, 1);
            GL.Vertex3(1, -1, 1);
            GL.Vertex3(1, 1, 1);
            GL.Vertex3(-1, 1, 1);

            // Задняя грань - синяя
            GL.Color3(Color.Blue);
            GL.Vertex3(-1, -1, -1);
            GL.Vertex3(-1, 1, -1);
            GL.Vertex3(1, 1, -1);
            GL.Vertex3(1, -1, -1);

            // Левая грань - зелёная
            GL.Color3(Color.Green);
            GL.Vertex3(-1, -1, -1);
            GL.Vertex3(-1, -1, 1);
            GL.Vertex3(-1, 1, 1);
            GL.Vertex3(-1, 1, -1);

            // Правая грань - жёлтая
            GL.Color3(Color.Yellow);
            GL.Vertex3(1, -1, 1);
            GL.Vertex3(1, -1, -1);
            GL.Vertex3(1, 1, -1);
            GL.Vertex3(1, 1, 1);

            // Верхняя грань - белая
            GL.Color3(Color.White);
            GL.Vertex3(-1, 1, -1);
            GL.Vertex3(-1, 1, 1);
            GL.Vertex3(1, 1, 1);
            GL.Vertex3(1, 1, -1);

            // Нижняя грань - фиолетовая
            GL.Color3(Color.Purple);
            GL.Vertex3(-1, -1, -1);
            GL.Vertex3(1, -1, -1);
            GL.Vertex3(1, -1, 1);
            GL.Vertex3(-1, -1, 1);

            GL.End();

            // Увеличиваем угол поворота для анимации
            RotationAngle += 0.5f;
            if (RotationAngle > 360)
                RotationAngle = 0;
        }

        /// <summary>
        /// Инициализация OpenGL перед началом отрисовки
        /// </summary>
        public void Initialize()
        {
            GL.ClearColor(0.1f, 0.2f, 0.3f, 1.0f);  // тёмно-синий фон
            GL.Enable(OpenTK.Graphics.OpenGL.EnableCap.DepthTest);
            GL.DepthFunc(OpenTK.Graphics.OpenGL.DepthFunction.Less);
        }

        /// <summary>
        /// Обработка изменения размера OpenGL-области
        /// </summary>
        public void Resize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
        }
    }
}
