using System;
using System.Collections.Generic;
using System.Drawing;
using Masterplan.Data.Combat.Visibility;

namespace Masterplan.Data.Combat.Visibility
{
    public class VisibilitySystem
    {
        public List<VisibilityBlocker> Blockers = new List<VisibilityBlocker>();
        public void AddMapBlockers()
        {
            // Battle
            //Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(16f, 6f, 1f, 7f)));
            //Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(16f, 18f, 1f, 8f)));
            //Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(23f, 5f, 1f, 2f)));
            //Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(24f, 13f, 1f, 6f)));
            //Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(23f, 24f, 1f, 3f)));
            //Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(31f, 6f, 1f, 8f)));
            //Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(31f, 19f, 1f, 7f)));

            // Forest map
            Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(1f, 3f, 2f, 2f)));
            Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(3f, 2f, 1f, 1f)));
            Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(6f, 3f, 1f, 1f)));
            Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(1f, 7f, 1f, 1f)));
            Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(5f, 7f, 1f, 1f)));
            Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(8f, 6f, 1f, 1f)));
            Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(4f, 12f, 1f, 1f)));
            Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(7f, 10f, 1f, 1f)));
            Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(0f, 14f, 2f, 2f)));
            Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(7f, 15f, 1f, 1f)));
            Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(5f, 17f, 1f, 1f)));

            Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(4f, 14f, 2f, 2f), OcclusionLevel.Cover));
            Blockers.Add(new RectangleVisibilityBlocker(new RectangleF(1f, 8f, 4f, 3f), OcclusionLevel.Cover));
        }

        private static VisibilitySystem _instance;
        public static VisibilitySystem GetInstance()
        {
            if (_instance == null)
            {
                _instance = new VisibilitySystem();
            }
            return _instance;
        }

        public Point Min;
        public Point Max;

        public VisibilitySystem()
        {

        }

        public void SetSize(Point min, Point max)
        {
            this.Min = min;
            this.Max = max;
        }

        private enum CornerTestDirection
        {
            Up,
            Left,
            Right,
            Down
        }

        private void TestCorners(Point viewerPosition)
        {
            List<PointF> cornersToTest = new List<PointF>();

            if (this.VisibilityMap[viewerPosition.X-1, viewerPosition.Y - 1] != OcclusionLevel.Obscured &&
                 (this.VisibilityMap[viewerPosition.X, viewerPosition.Y-1] == OcclusionLevel.Obscured ||
                  this.VisibilityMap[viewerPosition.X-1, viewerPosition.Y] == OcclusionLevel.Obscured))
            {
                // test upper left corner, wall either above or left
                cornersToTest.Add(new PointF((float)viewerPosition.X, (float)viewerPosition.Y));
            }

            if (this.VisibilityMap[viewerPosition.X + 1, viewerPosition.Y - 1] != OcclusionLevel.Obscured &&
                 (this.VisibilityMap[viewerPosition.X, viewerPosition.Y - 1] == OcclusionLevel.Obscured ||
                  this.VisibilityMap[viewerPosition.X+1, viewerPosition.Y] == OcclusionLevel.Obscured))
            {
                // test upper right corner, wall either above or right
                cornersToTest.Add(new PointF((float)viewerPosition.X + 1, (float)viewerPosition.Y));
            }

            if (this.VisibilityMap[viewerPosition.X - 1, viewerPosition.Y + 1] != OcclusionLevel.Obscured &&
                 (this.VisibilityMap[viewerPosition.X, viewerPosition.Y + 1] == OcclusionLevel.Obscured ||
                  this.VisibilityMap[viewerPosition.X - 1, viewerPosition.Y] == OcclusionLevel.Obscured))
            {
                // test lower-left corner, wall either left or bottom
                cornersToTest.Add(new PointF((float)viewerPosition.X, (float)viewerPosition.Y + 1));
            }

            if (this.VisibilityMap[viewerPosition.X + 1, viewerPosition.Y + 1] != OcclusionLevel.Obscured &&
                 (this.VisibilityMap[viewerPosition.X, viewerPosition.Y + 1] == OcclusionLevel.Obscured ||
                  this.VisibilityMap[viewerPosition.X + 1, viewerPosition.Y] == OcclusionLevel.Obscured))
            {
                // test lower-right corner, wall either right or bottom
                cornersToTest.Add(new PointF((float)viewerPosition.X+1, (float)viewerPosition.Y+1));
            }

            // if we have corners to test, then run through the whole visibility map from that corner and union it to our result
            foreach(var point in cornersToTest)
            {
                VisibilityMap cornerMap = this.GenerateVisibilityMapForPosition(point);
                for(int x=0; x < cornerMap.Width; ++x)
                {
                    for(int y=0; y<cornerMap.Height;  ++y)
                    {
                        this.VisibilityMap[x, y] = this.VisibilityMap[x, y] == OcclusionLevel.Obscured ? OcclusionLevel.Obscured : cornerMap[x, y];
                    }
                }
            }
        }

        public VisibilityMap VisibilityMap
        {
            get;
            private set;
        }

        private OcclusionLevel TestSquare(int x, int y)
        {
            if (x < this.Min.X || x >= this.Max.X ||
                y < this.Min.Y || y >= this.Max.Y)
            {
                return OcclusionLevel.Obscured;
            }

            return this.VisibilityMap[x, y];
        }
        public void RecalculateFromPosition(Point viewerPosition)
        {
            // Test from each corner of source square to center of target square
            PointF[] cornersToTest = { new PointF(viewerPosition.X, viewerPosition.Y),
                                        new PointF(viewerPosition.X+1, viewerPosition.Y),
                                        new PointF(viewerPosition.X, viewerPosition.Y+1),
                                        new PointF(viewerPosition.X+1, viewerPosition.Y+1)};

            this.VisibilityMap = null;
            foreach (var point in cornersToTest)
            {
                VisibilityMap cornerMap = this.GenerateVisibilityMapForPosition(point);
                if (this.VisibilityMap == null)
                {
                    this.VisibilityMap = cornerMap;
                }
                else
                {
                    for (int x = 0; x < cornerMap.Width; ++x)
                    {
                        for (int y = 0; y < cornerMap.Height; ++y)
                        {
                            this.VisibilityMap[x, y] = (OcclusionLevel) Math.Min((int)this.VisibilityMap[x, y], (int)cornerMap[x, y]);
                        }
                    }
                }
            }

            // Calculate cover spots
            for (int x = 0; x < this.VisibilityMap.Width; ++x)
            {
                for (int y = 0; y < this.VisibilityMap.Height; ++y)
                {
                    if (this.VisibilityMap[x, y] == OcclusionLevel.Obscured)
                    {
                        if (TestSquare(x, y - 1) == OcclusionLevel.Visible ||
                            TestSquare(x, y + 1) == OcclusionLevel.Visible ||
                            TestSquare(x - 1, y) == OcclusionLevel.Visible ||
                            TestSquare(x + 1, y) == OcclusionLevel.Visible)
                        {
                            this.VisibilityMap[x, y] = OcclusionLevel.Cover;
                        }
                    }
                }
            }

            //PointF centerOfSquare = new PointF((float)viewerPosition.X + 0.5f, (float)viewerPosition.Y + 0.5f);
            //this.VisibilityMap = this.GenerateVisibilityMapForPosition(centerOfSquare);
            //this.TestCorners(viewerPosition);
        }

        private VisibilityMap GenerateVisibilityMapForPosition(PointF viewerLocation)
        {
            VisibilityMap visData = new VisibilityMap(this.Max.X - this.Min.X, this.Max.Y - this.Min.Y);

            for (int x = this.Min.X; x < this.Max.X; ++x)
            {
                for (int y = this.Min.Y; y < this.Max.Y; ++y)
                {
                    PointF targetLocation = new PointF((float)x + 0.5f, (float)y + 0.5f);
                    visData[x - this.Min.X, y - this.Min.Y] = this.GetOcclusion(viewerLocation, targetLocation);
                }
            }

            return visData;
        }

        public OcclusionLevel GetOcclusion(PointF fromPoint, PointF targetPoint)
        {
            MMath.LineSegment l = new MMath.LineSegment(fromPoint, targetPoint);
            OcclusionLevel result = OcclusionLevel.Visible;
            foreach (var blocker in Blockers)
            {
                if (blocker.Intersects(l))
                {
                    result = (OcclusionLevel)Math.Max((int)result, (int)blocker.OcclusionLevel);
                    if (result == OcclusionLevel.Obscured)
                    {
                        return result;
                    }
                }
            }
            return result;
        }
    }
}
