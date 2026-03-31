using System;
using System.Drawing;
using System.Windows.Forms;

namespace pr2
{
    /// <summary>
    /// Главная форма приложения, демонстрирующая работу OpenGL через GLControl.
    /// </summary>
    public partial class Form1 : Form
    {
        // Рендерер, содержащий все методы отрисовки
        private readonly Renderer _renderer;

        // Таймер для анимации 3D куба
        private readonly System.Windows.Forms.Timer _animationTimer;

        // Перечисление доступных сцен
        private enum SceneType
        {
            Triangle1,      // Пример 1 - 3D треугольник
            Triangle2,      // Пример 2 - 2D треугольник
            Square,         // Квадрат
            Polygon,        // Многоугольник
            Circle,         // Круг
            Cube3D          // 3D куб
        }

        // Текущая выбранная сцена
        private SceneType _currentScene = SceneType.Triangle1;

        // Соответствие индексов ComboBox и сцен
        private readonly string[] _sceneNames = new string[]
        {
            "Triangle Example 1 (3D)",
            "Triangle Example 2 (2D)",
            "Square",
            "Polygon",
            "Circle",
            "3D Cube"
        };

        // Флаг инициализации OpenGL
        private bool _isInitialized = false;

        public Form1()
        {
            InitializeComponent();

            // Инициализируем рендерер
            _renderer = new Renderer();

            // Заполняем ComboBox названиями сцен
            comboBoxScenes.Items.AddRange(_sceneNames);
            comboBoxScenes.SelectedIndex = 0;

            // Настраиваем таймер для анимации 3D куба
            _animationTimer = new System.Windows.Forms.Timer();
            _animationTimer.Interval = 16; // ~60 FPS
            _animationTimer.Tick += (sender, e) => AnimationTimer_Tick(sender!, e);
            _animationTimer.Start();
        }

        /// <summary>
        /// Обработчик загрузки GLControl.
        /// Здесь происходит инициализация OpenGL.
        /// </summary>
        private void glControl_Load(object sender, EventArgs e)
        {
            // Инициализация OpenGL
            _renderer.Initialize();
            _isInitialized = true;

            // Принудительная перерисовка
            glControl.Invalidate();
        }

        /// <summary>
        /// Обработчик отрисовки GLControl.
        /// Вызывается при каждом обновлении контрола.
        /// </summary>
        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            // Проверяем, готов ли контекст OpenGL
            if (!_isInitialized)
                return;

            // Делаем контекст OpenGL текущим
            glControl.MakeCurrent();

            // Очистка буферов цвета и глубины
            OpenTK.Graphics.OpenGL.GL.Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.ColorBufferBit | OpenTK.Graphics.OpenGL.ClearBufferMask.DepthBufferBit);

            // Отрисовка текущей сцены
            switch (_currentScene)
            {
                case SceneType.Triangle1:
                    _renderer.DrawTriangleExample1(glControl.Width, glControl.Height);
                    break;

                case SceneType.Triangle2:
                    _renderer.DrawTriangleExample2();
                    break;

                case SceneType.Square:
                    _renderer.DrawSquare();
                    break;

                case SceneType.Polygon:
                    _renderer.DrawPolygon();
                    break;

                case SceneType.Circle:
                    _renderer.DrawCircle();
                    break;

                case SceneType.Cube3D:
                    _renderer.Draw3DCube(glControl.Width, glControl.Height);
                    break;
            }

            // Завершение отрисовки
            glControl.SwapBuffers();
        }

        /// <summary>
        /// Обработчик изменения размера GLControl.
        /// Необходим для корректного отображения при ресайзе формы.
        /// </summary>
        private void glControl_Resize(object sender, EventArgs e)
        {
            if (!_isInitialized)
                return;

            // Обновляем viewport
            _renderer.Resize(glControl.Width, glControl.Height);

            // Перерисовываем
            glControl.Invalidate();
        }

        /// <summary>
        /// Обработчик выбора сцены в ComboBox.
        /// </summary>
        private void comboBoxScenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Обновляем текущую сцену
            _currentScene = (SceneType)comboBoxScenes.SelectedIndex;

            // Обновляем подпись
            labelCurrentScene.Text = _sceneNames[comboBoxScenes.SelectedIndex];

            // Сбрасываем угол вращения для 3D куба при переключении
            if (_currentScene == SceneType.Cube3D)
            {
                _renderer.RotationAngle = 0f;
            }

            // Перерисовываем
            glControl.Invalidate();
        }

        /// <summary>
        /// Обработчик кнопки "Перерисовать".
        /// </summary>
        private void buttonRedraw_Click(object sender, EventArgs e)
        {
            glControl.Invalidate();
        }

        /// <summary>
        /// Обработчик таймера анимации.
        /// Вызывается примерно 60 раз в секунду.
        /// </summary>
        private void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            // Анимация нужна только для 3D куба
            if (_currentScene == SceneType.Cube3D)
            {
                glControl.Invalidate();
            }
        }

        /// <summary>
        /// Обработчик закрытия формы.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Останавливаем таймер
            _animationTimer.Stop();
            _animationTimer.Dispose();

            base.OnFormClosing(e);
        }
    }
}
