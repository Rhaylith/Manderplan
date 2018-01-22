using System.Drawing;
namespace Masterplan.MMath
{
    public static class Rectangle
    {
        public static bool RectContainsPoint(RectangleF rect, PointF point)
        {
            return point.X >= rect.X && point.X <= (rect.X + rect.Width) &&
                   point.Y >= rect.Y && point.Y <= (rect.Y + rect.Height);
        }
    }
}
