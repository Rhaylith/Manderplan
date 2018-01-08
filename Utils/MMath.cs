using System;
using System.Drawing;

namespace Utils
{
    public static class MMath
    {
        public static int CalcDistance(Point from, Point to)
        {
            int num = Math.Abs(from.X - to.X);
            int num1 = Math.Abs(from.Y - to.Y);
            return Math.Max(num, num1);
        }
    }
}
