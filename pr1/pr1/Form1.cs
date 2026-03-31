using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace pr1
{
    public partial class Form1 : Form
    {
        private Graphics? g;
        private readonly List<Shape> shapes = new();
        private ShapeType currentShapeType = ShapeType.Line;
        private bool isRegularPolygon = true;

        private enum ShapeType
        {
            Line,
            Ellipse,
            Polygon
        }

        public Form1()
        {
            InitializeComponent();
            InitializeComboBox();
            UpdateParamsHint();
            UpdatePolygonControls();
        }

        private void InitializeComboBox()
        {
            comboBoxShapeType.Items.Add("Линия");
            comboBoxShapeType.Items.Add("Эллипс");
            comboBoxShapeType.Items.Add("Многоугольник");
            comboBoxShapeType.SelectedIndex = 0;
            comboBoxShapeType.SelectedIndexChanged += ComboBoxShapeType_SelectedIndexChanged;
        }

        private void ComboBoxShapeType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            currentShapeType = (ShapeType)comboBoxShapeType.SelectedIndex;
            UpdateParamsHint();
            UpdatePolygonControls();
        }

        private void RadioPolygonType_CheckedChanged(object? sender, EventArgs e)
        {
            isRegularPolygon = radioRegularPolygon.Checked;
            UpdateParamsHint();
            UpdatePolygonControls();
        }

        private void UpdatePolygonControls()
        {
            bool isPolygon = currentShapeType == ShapeType.Polygon;
            bool showPointControls = isPolygon && !isRegularPolygon;

            listBoxPoints.Visible = showPointControls;
            buttonAddPoint.Visible = showPointControls;
            buttonRemovePoint.Visible = showPointControls;
            labelPoints.Visible = showPointControls;

            if (isPolygon && isRegularPolygon)
            {
                listBoxPoints.Items.Clear();
            }
        }

        private void UpdateParamsHint()
        {
            string hint = currentShapeType switch
            {
                ShapeType.Line => "x1,y1,x2,y2\nцвет\nтолщина\n\nПример:\n0,0,100,100\nred\n2",
                ShapeType.Ellipse => "x,y,radX,radY\nцвет\nтолщина\nтекст\n\nПример:\n100,100,50,30\nblue\n2\nТекст",
                ShapeType.Polygon => isRegularPolygon
                    ? "x,y,стороны,радиус\nцвет\nтолщина\n\nПример:\n150,150,6,50\ngreen\n3"
                    : "Точки: x1,y1,x2,y2,...\nцвет\nтолщина\n\nПример:\n50,50,100,50,100,100\nred\n2",
                _ => ""
            };
            labelParams.Text = "Параметры:\n" + hint;
        }

        private void buttonEnable_Click(object? sender, EventArgs e)
        {
            g = panelCanvas.CreateGraphics();
            // Очистка области при инициализации
            ClearCanvas();
            buttonEnable.Enabled = false;
            labelStatus.Text = "Графика включена. Выберите фигуру и введите параметры.";
        }

        private void buttonDraw_Click(object? sender, EventArgs e)
        {
            if (g == null)
            {
                labelStatus.Text = "Сначала включите графику!";
                return;
            }

            try
            {
                Shape? shape = CreateShapeFromParams();
                if (shape != null)
                {
                    shape.BackgroundColor = panelCanvas.BackColor;
                    shape.Draw(g);
                    shapes.Add(shape);
                    labelStatus.Text = $"Нарисовано: {GetShapeName(currentShapeType)}";
                }
                else
                {
                    labelStatus.Text = "Ошибка: неверные параметры";
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"Ошибка рисования: {ex.Message}";
            }
        }

        private void buttonErase_Click(object? sender, EventArgs e)
        {
            if (g == null)
            {
                labelStatus.Text = "Сначала включите графику!";
                return;
            }

            if (shapes.Count == 0)
            {
                labelStatus.Text = "Нет фигур для стирания";
                return;
            }

            // Стираем последнюю фигуру
            var lastShape = shapes[^1];
            lastShape.Erase(g);
            shapes.RemoveAt(shapes.Count - 1);
            labelStatus.Text = "Фигура стёрта";
        }

        private void buttonClearAll_Click(object? sender, EventArgs e)
        {
            if (g == null)
            {
                labelStatus.Text = "Сначала включите графику!";
                return;
            }

            ClearCanvas();
            shapes.Clear();
            labelStatus.Text = "Все фигуры удалены";
        }

        private void ClearCanvas()
        {
            if (g == null) return;
            g.FillRectangle(new SolidBrush(panelCanvas.BackColor), 0, 0, panelCanvas.Width, panelCanvas.Height);
        }

        private Shape? CreateShapeFromParams()
        {
            string[] lines = textBoxParams.Text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            System.Diagnostics.Debug.WriteLine(textBoxParams.Text);
            if (lines.Length == 0) return null;
            System.Diagnostics.Debug.WriteLine(lines);
            string[] coords = lines[0].Split(',', StringSplitOptions.RemoveEmptyEntries);

            return currentShapeType switch
            {
                ShapeType.Line => CreateLine(coords, lines),
                ShapeType.Ellipse => CreateEllipse(coords, lines),
                ShapeType.Polygon => CreatePolygon(coords, lines),
                _ => null
            };
        }

        private Line? CreateLine(string[] coords, string[] lines)
        {
            if (coords.Length < 4) return null;

            var line = new Line(
                new Point(ParseInt(coords[0]), ParseInt(coords[1])),
                new Point(ParseInt(coords[2]), ParseInt(coords[3]))
            );

            ApplyCommonSettings(line, lines);
            return line;
        }

        private Ellipse? CreateEllipse(string[] coords, string[] lines)
        {
            if (coords.Length < 4) return null;

            var ellipse = new Ellipse(
                new Point(ParseInt(coords[0]), ParseInt(coords[1])),
                ParseInt(coords[2]),
                ParseInt(coords[3])
            );

            ApplyCommonSettings(ellipse, lines);

            if (lines.Length > 3)
                ellipse.Text = lines[3].Trim();

            return ellipse;
        }

        private Polygon? CreatePolygon(string[] coords, string[] lines)
        {
            if (isRegularPolygon)
            {
                // Правильный многоугольник
                if (coords.Length < 4) return null;

                var polygon = new Polygon(
                    new Point(ParseInt(coords[0]), ParseInt(coords[1])),
                    ParseInt(coords[2]),
                    ParseInt(coords[3])
                );

                ApplyCommonSettings(polygon, lines);
                return polygon;
            }
            else
            {
                // Произвольный многоугольник по точкам из ListBox
                if (listBoxPoints.Items.Count < 3)
                {
                    labelStatus.Text = "Добавьте минимум 3 точки";
                    return null;
                }

                var points = new PointF[listBoxPoints.Items.Count];
                for (int i = 0; i < listBoxPoints.Items.Count; i++)
                {
                    string[] pointCoords = listBoxPoints.Items[i].ToString()!.Split(',');
                    points[i] = new PointF(ParseInt(pointCoords[0]), ParseInt(pointCoords[1]));
                }

                var polygon = new Polygon(points);
                
                // Для произвольного многоугольника:
                // lines[0] - точки (уже обработаны в listBoxPoints)
                // lines[1] - цвет (или lines[0] если точки не в lines)
                // lines[2] - толщина
                // Парсим цвет и толщину из textBoxParams (после точек)
                string[] paramLines = textBoxParams.Text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                
                if (paramLines.Length >= 1 && !string.IsNullOrWhiteSpace(paramLines[0]))
                    polygon.Color = ParseColor(paramLines[0].Trim());

                if (paramLines.Length >= 2 && int.TryParse(paramLines[1].Trim(), out int width))
                    polygon.PenWidth = width;

                // Заливка (опционально, 3-я строка)
                if (paramLines.Length >= 3 && !string.IsNullOrWhiteSpace(paramLines[2]))
                {
                    polygon.FillColor = ParseColor(paramLines[2].Trim());
                }
                
                return polygon;
            }
        }

        private void ApplyCommonSettings(Shape shape, string[] lines)
        {
            if (lines.Length > 1 && !string.IsNullOrWhiteSpace(lines[1]))
                shape.Color = ParseColor(lines[1].Trim());

            if (lines.Length > 2 && int.TryParse(lines[2].Trim(), out int width))
                shape.PenWidth = width;

            // Заливка для многоугольника (опционально, 4-я строка)
            if (currentShapeType == ShapeType.Polygon && lines.Length > 3 && !string.IsNullOrWhiteSpace(lines[3]))
            {
                shape.FillColor = ParseColor(lines[3].Trim());
            }
        }

        private int ParseInt(string s)
        {
            return int.TryParse(s.Trim(), out int result) ? result : 0;
        }

        private Color ParseColor(string s)
        {
            return s.ToLower() switch
            {
                "red" => Color.Red,
                "green" => Color.Green,
                "blue" => Color.Blue,
                "yellow" => Color.Yellow,
                "cyan" => Color.Cyan,
                "magenta" => Color.Magenta,
                "white" => Color.White,
                "black" => Color.Black,
                "orange" => Color.Orange,
                "purple" => Color.Purple,
                "pink" => Color.Pink,
                "gray" => Color.Gray,
                _ => Color.Black
            };
        }

        private string GetShapeName(ShapeType type)
        {
            return type switch
            {
                ShapeType.Line => "Линия",
                ShapeType.Ellipse => "Эллипс",
                ShapeType.Polygon => "Многоугольник",
                _ => "Фигура"
            };
        }

        private void textBoxParams_TextChanged(object sender, EventArgs e)
        {

        }

        private void ButtonAddPoint_Click(object? sender, EventArgs e)
        {
            try
            {
                // Получаем первую строку (координаты точек)
                string firstLine = textBoxParams.Text.Split('\n')[0].Trim();
                if (string.IsNullOrEmpty(firstLine))
                {
                    labelStatus.Text = "Введите координаты точек: x1,y1,x2,y2,...";
                    return;
                }

                string[] coords = firstLine.Split(',', StringSplitOptions.RemoveEmptyEntries);
                
                // Проверяем, что есть хотя бы одна пара координат
                if (coords.Length < 2)
                {
                    labelStatus.Text = "Введите координаты: x,y";
                    return;
                }

                // Парсим все точки (поддержка формата x1,y1,x2,y2,...)
                int pointsAdded = 0;
                for (int i = 0; i < coords.Length - 1; i += 2)
                {
                    int x = ParseInt(coords[i]);
                    int y = ParseInt(coords[i + 1]);
                    listBoxPoints.Items.Add($"{x},{y}");
                    pointsAdded++;
                }

                textBoxParams.Clear();
                labelStatus.Text = $"Добавлено точек: {pointsAdded}";
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void ButtonRemovePoint_Click(object? sender, EventArgs e)
        {
            if (listBoxPoints.SelectedIndex >= 0)
            {
                listBoxPoints.Items.RemoveAt(listBoxPoints.SelectedIndex);
                labelStatus.Text = "Точка удалена";
            }
            else if (listBoxPoints.Items.Count > 0)
            {
                listBoxPoints.Items.RemoveAt(listBoxPoints.Items.Count - 1);
                labelStatus.Text = "Последняя точка удалена";
            }
            else
            {
                labelStatus.Text = "Нет точек для удаления";
            }
        }
    }
}
