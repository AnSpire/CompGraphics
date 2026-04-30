# LR6 — 3D Shapes (OpenTK)

WinForms-приложение (.NET 8) с компонентом `GLControl`, рисующее 3D-фигуры через legacy OpenGL (стиль `GL.Begin/GL.End`). Учебный мини-проект.

## Зависимости

Три DLL лежат в `lib\` прямо в репозитории — NuGet не нужен:

| Файл | Что содержит |
|---|---|
| `lib\OpenTK.dll` | Ядро: `GL`, `Vector3`, `Matrix4`, `MathHelper` |
| `lib\OpenTK.GLControl.dll` | Компонент `OpenTK.GLControl` для WinForms |
| `lib\OpenTK.Compatibility.dll` | Совместимость (не используется напрямую) |

Подключены через `<Reference HintPath="lib\...">` в `lr6.csproj`. При переносе проекта на другое устройство ничего менять не нужно — пути относительные.

## Ключевые неймспейсы

```csharp
using OpenTK;                    // Vector3, Matrix4, MathHelper
using OpenTK.Graphics.OpenGL;    // GL, PrimitiveType, MatrixMode, EnableCap, ...
// System.Drawing.Color передаётся напрямую в GL.Color3(Color)
```

## Архитектура

- **`Form1.Designer.cs`** — разметка: `GLControl` (Fill) + `Panel` сверху с двумя `RadioButton`
- **`Form1.cs`** — вся логика:
  - `GlControl_Load` — инициализация OpenGL (цвет фона, depth test)
  - `GlControl_Paint` — основной render loop: очистка → камера → вызовы функций рисования → SwapBuffers
  - `GlControl_Resize` — пересчёт viewport и проекции
  - `SetupCamera()` — перспективная проекция (55°, near=0.1, far=100) + LookAt (0,3,12) → (0,0,0)
  - `RadioWireframe_CheckedChanged` — переключает `_mode` между `PrimitiveType.LineLoop` и `PrimitiveType.Polygon`

## Реализованные фигуры

| Функция | Параметры | Алгоритм |
|---|---|---|
| `draw_parallelepiped` | `pos, w, h, d, color, mode` | 8 вершин, 6 граней по индексам |
| `draw_cone` | `pos, radius, height, segs, color, mode` | основание-полигон + `segs` боковых треугольников |
| `draw_sphere` | `pos, radius, stacks, slices, color, mode` | `stacks×slices` четырёхугольных патчей по φ/θ |
| `draw_tetrahedron` | `pos, size, color, mode` | 4 вершины вычислены из длины ребра, 4 треугольные грани |
| `draw_pyramid` | `pos, baseSize, height, color, mode` | квадратное основание + 4 боковых треугольника к вершине |
| `draw_frustum` | `pos, baseSize, topSize, height, color, mode` | усечённая пирамида: 2 квадрата (низ/верх) + 4 трапециевидных грани |

Все фигуры отображаются одновременно в `GlControl_Paint`, каждая со своим смещением `pos`.

## Как добавить новую фигуру

1. Добавить `private static void draw_<name>(Vector3 pos, /* параметры формы */, Color color, PrimitiveType mode)` в `Form1.cs`.

2. Внутри: вычислить вершины относительно `pos`, вызвать `Col(color)`, затем для каждой грани:
   ```csharp
   GL.Begin(mode);      // mode — LineLoop или Polygon, приходит снаружи
   Vtx(v[i]);           // GL.Vertex3 обёртка
   // ...
   GL.End();
   ```

3. Добавить вызов в `GlControl_Paint` с новым смещением `pos`, чтобы фигура не перекрывала существующие.

### Пример — цилиндр

```csharp
private static void draw_cylinder(Vector3 pos, float radius, float height, int segs, Color color, PrimitiveType mode)
{
    Col(color);
    float top = height;

    // нижнее основание
    GL.Begin(mode);
    for (int i = 0; i < segs; i++)
    {
        float a = MathHelper.TwoPi * i / segs;
        Vtx(pos + new Vector3(radius * (float)Math.Cos(a), 0f, radius * (float)Math.Sin(a)));
    }
    GL.End();

    // верхнее основание
    GL.Begin(mode);
    for (int i = 0; i < segs; i++)
    {
        float a = MathHelper.TwoPi * i / segs;
        Vtx(pos + new Vector3(radius * (float)Math.Cos(a), top, radius * (float)Math.Sin(a)));
    }
    GL.End();

    // боковые грани (четырёхугольники)
    for (int i = 0; i < segs; i++)
    {
        float a0 = MathHelper.TwoPi * i       / segs;
        float a1 = MathHelper.TwoPi * (i + 1) / segs;
        GL.Begin(mode);
        Vtx(pos + new Vector3(radius * (float)Math.Cos(a0), 0f,  radius * (float)Math.Sin(a0)));
        Vtx(pos + new Vector3(radius * (float)Math.Cos(a1), 0f,  radius * (float)Math.Sin(a1)));
        Vtx(pos + new Vector3(radius * (float)Math.Cos(a1), top, radius * (float)Math.Sin(a1)));
        Vtx(pos + new Vector3(radius * (float)Math.Cos(a0), top, radius * (float)Math.Sin(a0)));
        GL.End();
    }
}
```

## Сборка и запуск

```
dotnet build
dotnet run
```

Требуется только .NET 8 SDK — DLL уже в репозитории.
