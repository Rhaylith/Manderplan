using System.Drawing;

namespace Masterplan.MMath
{
    public struct LineSegment
    {
        public PointF Start;
        public PointF End;

        public LineSegment(float x1, float y1, float x2, float y2) : this(new PointF(x1, y1), new PointF(x2,y2))
        {

        }

        public LineSegment(PointF start, PointF end)
        {
            this.Start = start;
            this.End = end;
        }

        public bool Intersects(PointF p1, PointF q1)
        {
            return LineSegment.Intersects(p1, q1, Start, End);
        }

        public bool Intersects(RectangleF region)
        {
            // top
            // left
            // right
            // bottom
            return this.Intersects(new PointF(region.X, region.Y), new PointF(region.X + region.Width, region.Y))
                || this.Intersects(new PointF(region.X, region.Y), new PointF(region.X, region.Y + region.Height))
                || this.Intersects(new PointF(region.X + region.Width, region.Y), new PointF(region.X + region.Width, region.Y + region.Height))
                || this.Intersects(new PointF(region.X, region.Y + region.Height), new PointF(region.X + region.Width, region.Y + region.Height));
        }

        public static bool Intersects(PointF p1, PointF q1, PointF p2, PointF q2)
        {
            // Find the four orientations needed for general and
            // special cases
            int o1 = LineSegment.Orientation(p1, q1, p2);
            int o2 = LineSegment.Orientation(p1, q1, q2);
            int o3 = LineSegment.Orientation(p2, q2, p1);
            int o4 = LineSegment.Orientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
                return true;

#if false
            // Special Cases
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1
            if (o1 == 0 && onSegment(p1, p2, q1)) return true;

            // p1, q1 and p2 are colinear and q2 lies on segment p1q1
            if (o2 == 0 && onSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2
            if (o3 == 0 && onSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2
            if (o4 == 0 && onSegment(p2, q1, q2)) return true;
#endif
            return false; // Doesn't fall in any of the above cases
        }

        // To find orientation of ordered triplet (p, q, r).
        // The function returns following values
        // 0 --> p, q and r are colinear
        // 1 --> Clockwise
        // 2 --> Counterclockwise
        private static int Orientation(PointF p, PointF q, PointF r)
        {
            // See https://www.geeksforgeeks.org/orientation-3-ordered-points/
            // for details of below formula.
            float val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

            if (System.Math.Abs(val) < float.Epsilon) return 0;  // colinear

            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }
    }
}
