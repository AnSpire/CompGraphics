using System.Drawing;

namespace pr1
{
    /// <summary>
    /// Класс многоугольника
    /// Поддерживает произвольный многоугольник (по точкам) и правильный n-угольник
    /// </summary>
    public class Polygon : Shape
    {
        public PointF[]? Points { get; set; }
        public Point Center { get; set; }
        public int Sides { get; set; }
        public int Radius { get; set; }
        public bool IsRegular { get; set; }

        public Polygon() : base()
        {
            Points = null;
            Center = new Point(100, 100);
            Sides = 6;
            Radius = 50;
            IsRegular = true;
        }

        /// <summary>
        /// Создать произвольный многоугольник по точкам
        /// </summary>
        public Polygon(PointF[] points) : base()
        {
            Points = points;
            IsRegular = false;
        }

        /// <summary>
        /// Создать правильный многоугольник
        /// </summary>
        public Polygon(Point center, int sides, int radius) : base()
        {
            Center = center;
            Sides = sides;
            Radius = radius;
            IsRegular = true;
            GenerateRegularPolygon();
        }

        /// <summary>
        /// Сгенерировать точки правильного многоугольника
        /// </summary>
        private void GenerateRegularPolygon()
        {
            Points = new PointF[Sides];
            double angleStep = 2 * Math.PI / Sides;

            for (int i = 0; i < Sides; i++)
            {
                double angle = angleStep * i;
                Points[i].X = Center.X + (float)(Radius * Math.Cos(angle));
                Points[i].Y = Center.Y + (float)(Radius * Math.Sin(angle));
            }
        }

        public override void Draw(Graphics g)
        {
            if (Points == null || Points.Length < 3)
                return;

            // Заливка
            if (FillColor != Color.Transparent)
            {
                using var brush = CreateBrush();
                g.FillPolygon(brush, Points);
            }

            // Контур
            using var pen = CreatePen();
            g.DrawPolygon(pen, Points);
        }

        public override void Erase(Graphics g)
        {
            if (Points == null || Points.Length < 3)
                return;

            // Стираем заливку
            if (FillColor != Color.Transparent)
            {
                using var brush = new SolidBrush(BackgroundColor);
                g.FillPolygon(brush, Points);
            }

            // Стираем контур
            using var pen = CreateErasePen();
            g.DrawPolygon(pen, Points);
        }
    }
}
