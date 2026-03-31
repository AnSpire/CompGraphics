using NCalc;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;

namespace pr3
{
    public partial class Form1 : Form
    {
        // Цвета для разных функций
        private readonly Color[] _seriesColors =
        {
            Color.Blue,
            Color.Red,
            Color.Green,
            Color.Purple,
            Color.Orange,
            Color.Brown
        };

        // Стили маркеров для разных функций
        private readonly MarkerStyle[] _markerStyles =
        {
            MarkerStyle.Circle,
            MarkerStyle.Square,
            MarkerStyle.Triangle,
            MarkerStyle.Diamond,
            MarkerStyle.Star4,
            MarkerStyle.Cross
        };

        // Шаг дискретизации для построения графика
        private const double Step = 0.05;

        public Form1()
        {
            InitializeComponent();
            InitializeChart();
            cmbChartType.SelectedIndex = 0; // По умолчанию линейный график
        }

        /// <summary>
        /// Инициализация настроек Chart
        /// </summary>
        private void InitializeChart()
        {
            chartFunctions.ChartAreas[0].AxisX.Title = "X";
            chartFunctions.ChartAreas[0].AxisY.Title = "Y";
            chartFunctions.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chartFunctions.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            chartFunctions.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartFunctions.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartFunctions.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartFunctions.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
        }

        /// <summary>
        /// Преобразование формулы в формат NCalc
        /// </summary>
        private string PreprocessFormula(string formula)
        {
            // Простая замена x^N на Pow(x,N)
            return Regex.Replace(formula, @"(\w+)\s*\^\s*(\d+)", m => $"Pow({m.Groups[1].Value}, {m.Groups[2].Value})");
        }

        /// <summary>
        /// Вычисление выражения с поддержкой математических функций
        /// </summary>
        private double EvaluateExpression(string formula, double x)
        {
            try
            {
                var expr = new Expression(formula);

                // Регистрируем математические функции
                expr.EvaluateFunction += (name, args) =>
                {
                    var funcName = name.ToLower();
                    try
                    {
                        switch (funcName)
                        {
                            case "sin":
                                args.Result = Math.Sin(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                break;
                            case "cos":
                                args.Result = Math.Cos(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                break;
                            case "tan":
                                args.Result = Math.Tan(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                break;
                            case "abs":
                                args.Result = Math.Abs(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                break;
                            case "sqrt":
                                args.Result = Math.Sqrt(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                break;
                            case "log":
                                args.Result = Math.Log(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                break;
                            case "log10":
                                args.Result = Math.Log10(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                break;
                            case "exp":
                                args.Result = Math.Exp(Convert.ToDouble(args.Parameters[0].Evaluate()));
                                break;
                            case "pow":
                            case "Pow":
                            case "POWER":
                            case "power":
                                args.Result = Math.Pow(
                                    Convert.ToDouble(args.Parameters[0].Evaluate()),
                                    Convert.ToDouble(args.Parameters[1].Evaluate())
                                );
                                break;
                            case "min":
                                args.Result = Math.Min(
                                    Convert.ToDouble(args.Parameters[0].Evaluate()),
                                    Convert.ToDouble(args.Parameters[1].Evaluate())
                                );
                                break;
                            case "max":
                                args.Result = Math.Max(
                                    Convert.ToDouble(args.Parameters[0].Evaluate()),
                                    Convert.ToDouble(args.Parameters[1].Evaluate())
                                );
                                break;
                            default:
                                throw new ArgumentException($"Неизвестная функция: {name}");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Ошибка при вычислении функции {name}: {ex.Message}");
                    }
                };

                // Регистрируем константы
                expr.EvaluateParameter += (name, args) =>
                {
                    switch (name.ToLower())
                    {
                        case "pi":
                            args.Result = Math.PI;
                            break;
                        case "e":
                            args.Result = Math.E;
                            break;
                    }
                };

                // Устанавливаем параметр x
                expr.Parameters["x"] = x;

                var result = expr.Evaluate();
                return Convert.ToDouble(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка вычисления '{formula}' при x={x}: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик кнопки построения графиков
        /// </summary>
        private void btnPlot_Click(object? sender, EventArgs e)
        {
            try
            {
                lblError.Text = string.Empty;
                chartFunctions.Series.Clear();

                double xMin = (double)nudXMin.Value;
                double xMax = (double)nudXMax.Value;

                if (xMin >= xMax)
                {
                    lblError.Text = "Ошибка: минимальное значение X должно быть меньше максимального!";
                    return;
                }

                // Собираем все функции из TextBox
                var textBoxes = new[] { txtFunction1, txtFunction2, txtFunction3 };
                int seriesIndex = 0;

                foreach (var textBox in textBoxes)
                {
                    string formula = textBox.Text.Trim();
                    if (string.IsNullOrEmpty(formula))
                        continue;

                    // Преобразуем формулу (заменяем ^ на Pow)
                    string ncalcFormula = PreprocessFormula(formula);

                    // Проверяем корректность формулы
                    if (!IsValidFormula(ncalcFormula))
                    {
                        lblError.Text = $"Ошибка в формуле: {formula}\nПосле обработки: {ncalcFormula}";
                        return;
                    }

                    CreateSeries(formula, ncalcFormula, xMin, xMax, seriesIndex);
                    seriesIndex++;
                }

                if (seriesIndex == 0)
                {
                    lblError.Text = "Введите хотя бы одну функцию!";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = $"Ошибка: {ex.Message}\n{ex.InnerException?.Message}";
            }
        }

        /// <summary>
        /// Проверка корректности формулы
        /// </summary>
        private bool IsValidFormula(string formula)
        {
            try
            {
                // Проверяем на x=0 и x=1
                var y0 = EvaluateExpression(formula, 0);
                var y1 = EvaluateExpression(formula, 1);
                return true;
            }
            catch (Exception ex)
            {
                // Для отладки
                System.Diagnostics.Debug.WriteLine($"IsValidFormula error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Создание серии данных для функции
        /// </summary>
        private void CreateSeries(string originalFormula, string ncalcFormula, double xMin, double xMax, int index)
        {
            var series = new Series
            {
                Name = $"Функция {index + 1}: {originalFormula}",
                ChartType = GetChartType(),
                Color = _seriesColors[index % _seriesColors.Length],
                MarkerStyle = MarkerStyle.None,
                MarkerSize = 8,
                BorderWidth = 2
            };

            // Генерация точек для графика
            for (double x = xMin; x <= xMax; x += Step)
            {
                try
                {
                    double y = EvaluateExpression(ncalcFormula, x);

                    if (!double.IsNaN(y) && !double.IsInfinity(y))
                    {
                        series.Points.AddXY(x, y);
                    }
                }
                catch
                {
                    // Пропускаем точки, где функция не вычисляется
                }
            }

            chartFunctions.Series.Add(series);
        }

        /// <summary>
        /// Получение типа графика из ComboBox
        /// </summary>
        private SeriesChartType GetChartType()
        {
            return cmbChartType.SelectedIndex switch
            {
                0 => SeriesChartType.Line,          // Линейный
                1 => SeriesChartType.Point,         // Точечный
                2 => SeriesChartType.Line,          // Линии с маркерами
                _ => SeriesChartType.Line
            };
        }

        /// <summary>
        /// Обработчик изменения типа графика
        /// </summary>
        private void cmbChartType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Обновляем тип графика и маркеры для всех серий
            foreach (Series series in chartFunctions.Series)
            {
                series.ChartType = GetChartType();

                // Для режима "Линии с маркерами" включаем маркеры
                bool showMarkers = cmbChartType.SelectedIndex == 2;
                int index = chartFunctions.Series.IndexOf(series);
                series.MarkerStyle = showMarkers
                    ? _markerStyles[index % _markerStyles.Length]
                    : MarkerStyle.None;
            }
        }
    }
}
