using System;
using System.Drawing;
using Masterplan.MMath;

namespace Masterplan.Data.Combat
{
    public abstract class VisibilityBlocker
    {
        public Visibility.OcclusionLevel OcclusionLevel = Visibility.OcclusionLevel.Obscured;
        public abstract bool Intersects(MMath.LineSegment sightLine);
    }

    public class RectangleVisibilityBlocker : VisibilityBlocker
    {
        public RectangleF Rect;
        public RectangleVisibilityBlocker(RectangleF rect)
        {
            this.Rect = rect;
        }

        public override bool Intersects(MMath.LineSegment sightLine)
        {
            return sightLine.Intersects(this.Rect);
        }
    }

    public class LineVisibilityBlocker : VisibilityBlocker
    {
        public MMath.LineSegment Line;
        public override bool Intersects(LineSegment sightLine)
        {
            return sightLine.Intersects(this.Line.Start, this.Line.End);
        }
    }
}
