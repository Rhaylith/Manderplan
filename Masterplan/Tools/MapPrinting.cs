using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Masterplan.Tools
{
	internal class MapPrinting
	{
		private static Map fMap;

		private static Rectangle fViewpoint;

		private static Encounter fEncounter;

		private static bool fShowGridlines;

		private static bool fPosterMode;

		private static List<Rectangle> fPages;

		static MapPrinting()
		{
			MapPrinting.fMap = null;
			MapPrinting.fViewpoint = Rectangle.Empty;
			MapPrinting.fEncounter = null;
			MapPrinting.fShowGridlines = false;
			MapPrinting.fPosterMode = false;
			MapPrinting.fPages = null;
		}

		public MapPrinting()
		{
		}

		private static List<Rectangle> get_pages(MapView ctrl, int square_count_h, int square_count_v)
		{
			int num = Math.Max(square_count_h, square_count_v);
			int num1 = Math.Min(square_count_h, square_count_v);
			List<Point> points = new List<Point>();
			for (int i = ctrl.LayoutData.MinX; i <= ctrl.LayoutData.MaxX; i++)
			{
				for (int j = ctrl.LayoutData.MinY; j <= ctrl.LayoutData.MaxY; j++)
				{
					Point point = new Point(i, j);
					if (ctrl.LayoutData.GetTileAtSquare(point) != null)
					{
						points.Add(point);
					}
				}
			}
			List<Rectangle> rectangles = new List<Rectangle>();
			for (int k = ctrl.LayoutData.MinX; k <= ctrl.LayoutData.MaxX; k += num)
			{
				for (int l = ctrl.LayoutData.MinY; l <= ctrl.LayoutData.MaxY; l += num1)
				{
					Rectangle rectangle = new Rectangle(k, l, num, num1);
					bool flag = false;
					foreach (Point point1 in points)
					{
						if (!rectangle.Contains(point1))
						{
							continue;
						}
						flag = true;
						break;
					}
					if (flag)
					{
						rectangles.Add(rectangle);
					}
				}
			}
			return rectangles;
		}

		public static void Print(MapView mapview, bool poster, PrinterSettings settings)
		{
			MapPrinting.fMap = mapview.Map;
			MapPrinting.fViewpoint = mapview.Viewpoint;
			MapPrinting.fEncounter = mapview.Encounter;
			MapPrinting.fShowGridlines = mapview.ShowGrid == MapGridMode.Overlay;
			MapPrinting.fPosterMode = poster;
			PrintDocument printDocument = new PrintDocument()
			{
				DocumentName = MapPrinting.fMap.Name,
				PrinterSettings = settings
			};
			MapPrinting.fPages = null;
			printDocument.PrintPage += new PrintPageEventHandler(MapPrinting.print_map_page);
			printDocument.Print();
		}

		private static void print_map_page(object sender, PrintPageEventArgs e)
		{
			MapView mapView = new MapView()
			{
				Map = MapPrinting.fMap,
				Viewpoint = MapPrinting.fViewpoint,
				Encounter = MapPrinting.fEncounter,
				LineOfSight = false,
				Mode = MapViewMode.Plain,
				Size = e.PageBounds.Size,
				BorderSize = 1
			};
			if (MapPrinting.fShowGridlines)
			{
				mapView.ShowGrid = MapGridMode.Overlay;
			}
			if (MapPrinting.fPages == null)
			{
				if (!MapPrinting.fPosterMode)
				{
					MapPrinting.fPages = new List<Rectangle>()
					{
						mapView.Viewpoint
					};
				}
				else
				{
					int width = e.PageSettings.PaperSize.Width / 100;
					int height = e.PageSettings.PaperSize.Height / 100;
					MapPrinting.fPages = MapPrinting.get_pages(mapView, width, height);
				}
			}
			mapView.Viewpoint = MapPrinting.fPages[0];
			MapPrinting.fPages.RemoveAt(0);
			bool flag = mapView.LayoutData.Width > mapView.LayoutData.Height;
			bool width1 = flag != e.PageBounds.Width > e.PageBounds.Height;
			if (width1)
			{
				mapView.Width = e.PageBounds.Height;
				mapView.Height = e.PageBounds.Width;
			}
			Bitmap bitmap = new Bitmap(mapView.Width, mapView.Height);
			mapView.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
			if (width1)
			{
				bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
			}
			e.Graphics.DrawImage(bitmap, e.PageBounds);
			e.HasMorePages = MapPrinting.fPages.Count != 0;
		}
	}
}