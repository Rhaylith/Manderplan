using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class MapData
	{
		public Dictionary<TileData, Tile> Tiles = new Dictionary<TileData, Tile>();

		public Dictionary<TileData, Rectangle> TileSquares = new Dictionary<TileData, Rectangle>();

		public Dictionary<TileData, RectangleF> TileRegions = new Dictionary<TileData, RectangleF>();

		public double ScalingFactor;

		public int SquareSize;

		public Size MapOffset = new Size();

		public int MinX = 2147483647;

		public int MinY = 2147483647;

		public int MaxX = -2147483648;

		public int MaxY = -2147483648;

		public int Height
		{
			get
			{
				return this.MaxY - this.MinY + 1;
			}
		}

		public int Width
		{
			get
			{
				return this.MaxX - this.MinX + 1;
			}
		}

		public MapData(MapView mapview, double scaling_factor)
		{
			Rectangle rectangle;
			this.ScalingFactor = scaling_factor;
			if ((mapview.Map == null || mapview.Map.Tiles.Count == 0) && (mapview.BackgroundMap == null || mapview.BackgroundMap.Tiles.Count == 0))
			{
				this.MinX = 0;
				this.MinY = 0;
				this.MaxX = 0;
				this.MaxY = 0;
			}
			else
			{
				List<TileData> tileDatas = new List<TileData>();
				tileDatas.AddRange(mapview.Map.Tiles);
				if (mapview.BackgroundMap != null)
				{
					tileDatas.AddRange(mapview.BackgroundMap.Tiles);
				}
				foreach (TileData tileData in tileDatas)
				{
					Tile tile = Session.FindTile(tileData.TileID, SearchType.Global);
					if (tile == null)
					{
						continue;
					}
					this.Tiles[tileData] = tile;
					if (tileData.Rotations % 2 != 0)
					{
						int x = tileData.Location.X;
						int y = tileData.Location.Y;
						int height = tile.Size.Height;
						Size size = tile.Size;
						rectangle = new Rectangle(x, y, height, size.Width);
					}
					else
					{
						int num = tileData.Location.X;
						int y1 = tileData.Location.Y;
						int width = tile.Size.Width;
						Size size1 = tile.Size;
						rectangle = new Rectangle(num, y1, width, size1.Height);
					}
					this.TileSquares[tileData] = rectangle;
					if (rectangle.X < this.MinX)
					{
						this.MinX = rectangle.X;
					}
					if (rectangle.Y < this.MinY)
					{
						this.MinY = rectangle.Y;
					}
					int x1 = rectangle.X + rectangle.Width;
					if (x1 > this.MaxX)
					{
						this.MaxX = x1;
					}
					int num1 = rectangle.Y + rectangle.Height;
					if (num1 <= this.MaxY)
					{
						continue;
					}
					this.MaxY = num1;
				}
			}
			if (mapview.Map == null || !(mapview.Viewpoint != Rectangle.Empty))
			{
				this.MinX -= mapview.BorderSize;
				this.MinY -= mapview.BorderSize;
				this.MaxX += mapview.BorderSize;
				this.MaxY += mapview.BorderSize;
			}
			else
			{
				this.MinX = mapview.Viewpoint.X;
				this.MinY = mapview.Viewpoint.Y;
				int x2 = mapview.Viewpoint.X;
				Rectangle viewpoint = mapview.Viewpoint;
				this.MaxX = x2 + viewpoint.Width - 1;
				int y2 = mapview.Viewpoint.Y;
				Rectangle viewpoint1 = mapview.Viewpoint;
				this.MaxY = y2 + viewpoint1.Height - 1;
			}
			Rectangle clientRectangle = mapview.ClientRectangle;
			int single = clientRectangle.Width / this.Width;
			Rectangle clientRectangle1 = mapview.ClientRectangle;
			int height1 = clientRectangle1.Height / this.Height;
			this.SquareSize = (int) Math.Min(single, height1);
			this.SquareSize *= (int)this.ScalingFactor;
			int width1 = this.Width * this.SquareSize;
			int single1 = this.Height * this.SquareSize;
			int width2 = mapview.ClientRectangle.Width - width1;
			int height2 = mapview.ClientRectangle.Height - single1;
			this.MapOffset = new Size(width2 / 2, height2 / 2);
			if (mapview.Map != null)
			{
				foreach (TileData key in this.Tiles.Keys)
				{
					Rectangle item = this.TileSquares[key];
					this.TileRegions[key] = this.GetRegion(item.Location, item.Size);
				}
			}
		}

		~MapData()
		{
			this.Tiles.Clear();
			this.TileSquares.Clear();
			this.TileRegions.Clear();
		}

		public RectangleF GetRegion(Point square, Size size)
		{
			float x = (float)(square.X - this.MinX) * this.SquareSize + this.MapOffset.Width;
			float y = (float)(square.Y - this.MinY) * this.SquareSize + this.MapOffset.Height;
			float width = (float)size.Width * this.SquareSize;
			float height = (float)size.Height * this.SquareSize;
			return new RectangleF(x, y, width, height);
		}

		public Point GetSquareAtPoint(Point pt)
		{
			int x = (int)((float)pt.X - this.MapOffset.Width);
			int y = (int)((float)pt.Y - this.MapOffset.Height);
			x = (int)((float)x / this.SquareSize);
			y = (int)((float)y / this.SquareSize);
			return new Point(x + this.MinX, y + this.MinY);
		}

		public TileData GetTileAtSquare(Point square)
		{
			TileData tileDatum = null;
			foreach (TileData key in this.TileSquares.Keys)
			{
				if (!this.TileSquares[key].Contains(square))
				{
					continue;
				}
				tileDatum = key;
			}
			return tileDatum;
		}
	}
}