# Задача для агента

Сделай **Windows Forms приложение на C#** для демонстрации простого рисования через **OpenGL / OpenTK** на форме.

## Контекст

Есть учебное задание по OpenGL на C#. Изначально примеры даны для консольного приложения с `GameWindow`, но мне нужно именно **WinForms-приложение**, где рисование происходит **внутри формы**, желательно через `OpenTK.GLControl`.

Важно:  
- **папка с dll OpenTK уже лежит в директории проекта**, обычно что-то вроде `lib/`  
- dll **не надо скачивать заново**, нужно только **подключить ссылки на них в проект**
- приложение нужно сделать так, чтобы оно **демонстрировало оба примера**, приведённых ниже, но уже в формате **WinForms**
- код должен быть **понятным, учебным и аккуратно структурированным**

---

## Что нужно сделать

### 1. Тип проекта
Создай **Windows Forms App** на C#.

### 2. Подключение библиотек
Используй **OpenTK** из уже существующей папки `lib` в проекте.  
Нужно:
- подключить dll как references
- не предлагать скачивать NuGet-пакеты, если это не требуется
- учитывать, что в задании уже есть локальные dll

### 3. Главная цель
Сделай WinForms-приложение, которое показывает работу OpenGL через форму.  
Желательно использовать:
- `GLControl` на форме
- таймер или стандартный механизм перерисовки
- обработку инициализации OpenGL, ресайза, рендера

### 4. Что именно должно быть в приложении
В приложении должны быть демонстрации:

1. **Пример 1** — треугольник, аналогичный примеру с `Game : GameWindow`
2. **Пример 2** — треугольник, аналогичный примеру с обработчиками `Load`, `Resize`, `UpdateFrame`, `RenderFrame`
3. Дополнительно:
   - квадрат
   - многоугольник
   - круг
   - простой 3D-пример (например, цветной куб или другая базовая 3D-фигура)

### 5. Интерфейс
Сделай простой и понятный интерфейс. Например:
- `ComboBox` или `ListBox` для выбора сцены:
  - Triangle Example 1
  - Triangle Example 2
  - Square
  - Polygon
  - Circle
  - 3D Cube
- область OpenGL-отрисовки на форме
- кнопку `Перерисовать` или автоматическую перерисовку
- по желанию — подпись с названием текущего примера

### 6. Код
Код нужен:
- понятный
- с комментариями
- без лишней магии
- пригодный для учебного разбора

Раздели логику хотя бы минимально:
- `Program.cs`
- `MainForm.cs`
- при необходимости отдельный класс для OpenGL-рендера/сцен

---

## Важные технические требования

### 1. Обработка конфликтов библиотек
В исходном задании сказано, что:
- иногда возникает конфликт библиотек в `using`
- в таком случае нужно просто убрать лишний конфликтующий `using`

Учти это и исправь аккуратно.

### 2. System.Drawing
Для второго примера может понадобиться `System.Drawing`.  
Если это действительно нужно:
- подключи ссылку на `System.Drawing`
- либо перепиши цвета так, чтобы пример работал без проблем

### 3. Использование Vector3
Покажи в коде хотя бы один пример, где цвет и вершина задаются не только литералами, но и через `Vector3`, например:
- `Vector3 clr = ...`
- `Vector3 p = ...`
- `GL.Color3(clr);`
- `GL.Vertex3(p);`

### 4. Ресайз окна
При изменении размеров формы OpenGL-область должна корректно обновляться:
- `GL.Viewport(...)`
- пересчёт матриц, если нужно


---

## Исходные учебные примеры, которые нужно адаптировать под WinForms

### Пример 1

```csharp
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterKit
{
    class Game : GameWindow
    {
        public Game()
            : base(800, 600, GraphicsMode.Default, "OpenTK Quick Start Sample")
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                (float)Math.PI / 4,
                Width / (float)Height,
                1.0f,
                64.0f
            );

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
                Exit();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            GL.Begin(BeginMode.Triangles);
            GL.Color3(1.0f, 1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 4.0f);
            GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 4.0f);
            GL.Color3(0.2f, 0.9f, 1.0f); GL.Vertex3(0.0f, 1.0f, 4.0f);
            GL.End();

            SwapBuffers();
        }

        [STAThread]
        static void Main()
        {
            using (Game game = new Game())
            {
                game.Run(30.0);
            }
        }
    }
}
```


### Пример 2
```csharp
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example
{
    class MyApplication
    {
        [STAThread]
        public static void Main()
        {
            using (var game = new GameWindow())
            {
                game.Load += (sender, e) =>
                {
                    game.VSync = VSyncMode.On;
                };

                game.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, game.Width, game.Height);
                };

                game.UpdateFrame += (sender, e) =>
                {
                    if (game.Keyboard[Key.Escape])
                    {
                        game.Exit();
                    }
                };

                game.RenderFrame += (sender, e) =>
                {
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadIdentity();
                    GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

                    GL.Begin(PrimitiveType.Triangles);

                    GL.Color3(Color.MidnightBlue);
                    GL.Vertex2(-1.0f, 1.0f);
                    GL.Color3(Color.SpringGreen);
                    GL.Vertex2(0.0f, -1.0f);
                    GL.Color3(Color.Ivory);
                    GL.Vertex2(1.0f, 1.0f);

                    GL.End();
                    game.SwapBuffers();
                };

                game.Run(60.0);
            }
        }
    }
}
```