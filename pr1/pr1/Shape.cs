using System.Drawing;
using System.Drawing.Drawing2D;

namespace pr1
{
    /// <summary>
    /// Базовый абстрактный класс для всех фигур
    /// </summary>
    public abstract class Shape
    {
        public Color Color { get; set; }
        public Color FillColor { get; set; }
        public Color BackgroundColor { get; set; }
        public float PenWidth { get; set; }
        public DashStyle DashStyle { get; set; }

        protected Shape()
        {
            Color = Color.Black;
            FillColor = Color.Transparent;
            BackgroundColor = Color.White;
            PenWidth = 1f;
            DashStyle = DashStyle.Solid;
        }

        /// <summary>
        /// Нарисовать фигуру
        /// </summary>
        public abstract void Draw(Graphics g);

        /// <summary>
        /// Стереть фигуру (рисование цветом фона)
        /// </summary>
        public abstract void Erase(Graphics g);

        /// <summary>
        /// Создать Pen с текущими настройками
        /// </summary>
        protected Pen CreatePen()
        {
            var pen = new Pen(Color, PenWidth);
            pen.DashStyle = DashStyle;
            return pen;
        }

        /// <summary>
        /// Создать Pen для стирания
        /// </summary>
        protected Pen CreateErasePen()
        {
            var pen = new Pen(BackgroundColor, PenWidth);
            pen.DashStyle = DashStyle;
            return pen;
        }

        /// <summary>
        /// Создать SolidBrush с текущим цветом заливки
        /// </summary>
        protected SolidBrush CreateBrush()
        {
            return new SolidBrush(FillColor);
        }
    }
}
