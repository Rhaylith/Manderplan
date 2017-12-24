using Masterplan.Data;
using System;
using System.Drawing;

namespace Masterplan.Tools.Generators
{
	internal class Endpoint
	{
		public TileCategory Category;

		public Masterplan.Tools.Generators.Direction Direction;

		public Point TopLeft = Point.Empty;

		public Point BottomRight = Point.Empty;

		public Masterplan.Tools.Generators.Orientation Orientation
		{
			get
			{
				if (this.TopLeft.X == this.BottomRight.X)
				{
					return Masterplan.Tools.Generators.Orientation.NorthSouth;
				}
				return Masterplan.Tools.Generators.Orientation.EastWest;
			}
		}

		public int Size
		{
			get
			{
				int x = this.BottomRight.X - this.TopLeft.X;
				int y = this.BottomRight.Y - this.TopLeft.Y;
				return Math.Max(x, y);
			}
		}

		public Endpoint()
		{
		}
	}
}