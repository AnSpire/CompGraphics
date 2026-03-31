using System.Drawing;

namespace pr1
{
    /// <summary>
    /// Класс эллипса
    /// </summary>
    public class Ellipse : Shape
    {
        public Point Center { get; set; }
        public int RadiusX { get; set; }
        public int RadiusY { get; set; }
        public string? Text { get; set; }
        public Font? Font { get; set; }
        public bool ShowSizeLabel { get; set; }

        public Ellipse() : base()
        {
            Center = new Point(50, 50);
            RadiusX = 40;
            RadiusY = 30;
            Text = null;
            Font = new Font("Arial", 10);
            ShowSizeLabel = true;
        }

        public Ellipse(Point center, int radiusX, int radiusY) : base()
        {
            Center = center;
            RadiusX = radiusX;
            RadiusY = radiusY;
            Text = null;
            Font = new Font("Arial", 10);
            ShowSizeLabel = true;
        }

        public override void Draw(Graphics g)
        {
            int x = Center.X - RadiusX;
            int y = Center.Y - RadiusY;
            int width = 2 * RadiusX;
            int height = 2 * RadiusY;

            // Заливка
            if (FillColor != Color.Transparent)
            {
                using var brush = CreateBrush();
                g.FillEllipse(brush, x, y, width, height);
            }

            // Контур
            using var pen = CreatePen();
            g.DrawEllipse(pen, x, y, width, height);

            // Текст
            if (!string.IsNullOrEmpty(Text) && Font != null)
            {
                using var textBrush = new SolidBrush(Color);
                var textSize = g.MeasureString(Text, Font);
                var textX = Center.X - textSize.Width / 2;
                var textY = Center.Y - textSize.Height / 2;
                g.DrawString(Text, Font, textBrush, textX, textY);
            }

            // Подпись размеров
            if (ShowSizeLabel && Font != null)
            {
                using var textBrush = new SolidBrush(Color);
                string sizeLabel = $"{RadiusX}, {RadiusY}";
                var textSize = g.MeasureString(sizeLabel, Font);
                var textX = Center.X - textSize.Width / 2;
                var textY = Center.Y + RadiusY + 2; // чуть ниже эллипса
                g.DrawString(sizeLabel, Font, textBrush, textX, textY);
            }
        }

        public override void Erase(Graphics g)
        {
            int x = Center.X - RadiusX;
            int y = Center.Y - RadiusY;
            int width = 2 * RadiusX;
            int height = 2 * RadiusY;

            // Стираем заливку
            if (FillColor != Color.Transparent)
            {
                using var brush = new SolidBrush(BackgroundColor);
                g.FillEllipse(brush, x, y, width, height);
            }

            // Стираем контур
            using var pen = CreateErasePen();
            g.DrawEllipse(pen, x, y, width, height);

            // Стираем текст
            if (!string.IsNullOrEmpty(Text) && Font != null)
            {
                using var textBrush = new SolidBrush(BackgroundColor);
                var textSize = g.MeasureString(Text, Font);
                var textX = Center.X - textSize.Width / 2;
                var textY = Center.Y - textSize.Height / 2;
                g.DrawString(Text, Font, textBrush, textX, textY);
            }

            // Стираем подпись размеров
            if (ShowSizeLabel && Font != null)
            {
                string sizeLabel = $"{RadiusX}, {RadiusY}";
                var textSize = g.MeasureString(sizeLabel, Font);
                var textX = Center.X - textSize.Width / 2;
                var textY = Center.Y + RadiusY + 2;
                
                // Сначала заливаем область под текстом цветом фона
                using (var bgBrush = new SolidBrush(BackgroundColor))
                {
                    g.FillRectangle(bgBrush, textX, textY, textSize.Width, textSize.Height);
                }
                
                // Затем рисуем текст цветом фона для полного перекрытия
                using (var textBrush = new SolidBrush(BackgroundColor))
                {
                    g.DrawString(sizeLabel, Font, textBrush, textX, textY);
                }
            }
        }
    }
}
