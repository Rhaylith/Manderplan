using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masterplan.MMath
{
    public struct Point2F
    {
        public Point2F(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point2F(System.Drawing.Point point)
        {
            this.X = (float)point.X;
            this.Y = (float)point.Y;
        }

        public float X;
        public float Y;
    }
}
