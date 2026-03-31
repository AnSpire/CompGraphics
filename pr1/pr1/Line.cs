using System.Drawing;
using System.Drawing.Drawing2D;

namespace pr1
{
    /// <summary>
    /// Класс линии
    /// </summary>
    public class Line : Shape
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public LineCap StartCap { get; set; }
        public LineCap EndCap { get; set; }

        public Line() : base()
        {
            StartPoint = new Point(0, 0);
            EndPoint = new Point(100, 100);
            StartCap = LineCap.Flat;
            EndCap = LineCap.Flat;
        }

        public Line(Point start, Point end) : base()
        {
            StartPoint = start;
            EndPoint = end;
            StartCap = LineCap.Flat;
            EndCap = LineCap.Flat;
        }

        public override void Draw(Graphics g)
        {
            using var pen = CreatePen();
            pen.StartCap = StartCap;
            pen.EndCap = EndCap;
            g.DrawLine(pen, StartPoint, EndPoint);
        }

        public override void Erase(Graphics g)
        {
            using var pen = CreateErasePen();
            pen.StartCap = StartCap;
            pen.EndCap = EndCap;
            g.DrawLine(pen, StartPoint, EndPoint);
        }
    }
}
