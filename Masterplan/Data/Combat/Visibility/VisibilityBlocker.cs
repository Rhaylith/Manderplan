using System;
using System.Drawing;
using Masterplan.MMath;

namespace Masterplan.Data.Combat
{
    public abstract class VisibilityBlocker
    {
        public enum BlockerType
        {
            BlocksLookingInto,
            BlocksLookingThrough
        }

        public BlockerType Type = BlockerType.BlocksLookingInto;
        public Visibility.OcclusionLevel OcclusionLevel = Visibility.OcclusionLevel.Obscured;
        public abstract bool Intersects(MMath.LineSegment sightLine);

        public VisibilityBlocker(Visibility.OcclusionLevel occlusionLevel = Visibility.OcclusionLevel.Obscured)
        {
            this.OcclusionLevel = occlusionLevel;
        }
    }

    public class RectangleVisibilityBlocker : VisibilityBlocker
    {
        public RectangleF Rect;
        public RectangleVisibilityBlocker(RectangleF rect, Visibility.OcclusionLevel occlusionLevel = Visibility.OcclusionLevel.Obscured) : base(occlusionLevel)
        {
            this.Rect = rect;
        }

        public override bool Intersects(MMath.LineSegment sightLine)
        {
            //if (MMath.Rectangle.RectContainsPoint(this.Rect, sightLine.Start))
            if (this.Rect.X != sightLine.Start.X && this.Rect.Y != sightLine.Start.Y && this.Rect.Contains(sightLine.Start))
            {
                return false;
            }

            // If end point is inside this rectangle then we know based on the type what it is
            if (MMath.Rectangle.RectContainsPoint(this.Rect, sightLine.End))
            {
                return (this.Type == BlockerType.BlocksLookingInto) ? true : false;
            }
      
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

        public LineVisibilityBlocker(MMath.LineSegment line, Visibility.OcclusionLevel occlusionLevel = Visibility.OcclusionLevel.Obscured) : base(occlusionLevel)
        {
            this.Line = line;
        }
    }
}
