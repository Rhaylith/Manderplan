using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Utils.Graphics
{
	public abstract class RoundedRectangle
	{
		protected RoundedRectangle()
		{
		}

		public static GraphicsPath Create(float x, float y, float width, float height, float radius, RoundedRectangle.RectangleCorners corners)
		{
			float single = x + width;
			float single1 = y + height;
			float single2 = single - radius;
			float single3 = single1 - radius;
			float single4 = x + radius;
			float single5 = y + radius;
			float single6 = radius * 2f;
			float single7 = single - single6;
			float single8 = single1 - single6;
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.StartFigure();
			if ((RoundedRectangle.RectangleCorners.TopLeft & corners) != RoundedRectangle.RectangleCorners.TopLeft)
			{
				graphicsPath.AddLine(x, single5, x, y);
				graphicsPath.AddLine(x, y, single4, y);
			}
			else
			{
				graphicsPath.AddArc(x, y, single6, single6, 180f, 90f);
			}
			graphicsPath.AddLine(single4, y, single2, y);
			if ((RoundedRectangle.RectangleCorners.TopRight & corners) != RoundedRectangle.RectangleCorners.TopRight)
			{
				graphicsPath.AddLine(single2, y, single, y);
				graphicsPath.AddLine(single, y, single, single5);
			}
			else
			{
				graphicsPath.AddArc(single7, y, single6, single6, 270f, 90f);
			}
			graphicsPath.AddLine(single, single5, single, single3);
			if ((RoundedRectangle.RectangleCorners.BottomRight & corners) != RoundedRectangle.RectangleCorners.BottomRight)
			{
				graphicsPath.AddLine(single, single3, single, single1);
				graphicsPath.AddLine(single, single1, single2, single1);
			}
			else
			{
				graphicsPath.AddArc(single7, single8, single6, single6, 0f, 90f);
			}
			graphicsPath.AddLine(single2, single1, single4, single1);
			if ((RoundedRectangle.RectangleCorners.BottomLeft & corners) != RoundedRectangle.RectangleCorners.BottomLeft)
			{
				graphicsPath.AddLine(single4, single1, x, single1);
				graphicsPath.AddLine(x, single1, x, single3);
			}
			else
			{
				graphicsPath.AddArc(x, single8, single6, single6, 90f, 90f);
			}
			graphicsPath.AddLine(x, single3, x, single5);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		public static GraphicsPath Create(RectangleF rect, float radius, RoundedRectangle.RectangleCorners corners)
		{
			return RoundedRectangle.Create(rect.X, rect.Y, rect.Width, rect.Height, radius, corners);
		}

		public static GraphicsPath Create(float x, float y, float width, float height, float radius)
		{
			return RoundedRectangle.Create(x, y, width, height, radius, RoundedRectangle.RectangleCorners.All);
		}

		public static GraphicsPath Create(RectangleF rect, float radius)
		{
			return RoundedRectangle.Create(rect.X, rect.Y, rect.Width, rect.Height, radius);
		}

		public enum RectangleCorners
		{
			None = 0,
			TopLeft = 1,
			TopRight = 2,
			BottomLeft = 4,
			BottomRight = 8,
			All = 15
		}
	}
}