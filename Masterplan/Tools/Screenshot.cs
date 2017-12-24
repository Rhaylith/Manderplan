using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Tools
{
	internal class Screenshot
	{
		public Screenshot()
		{
		}

		public static Bitmap Calendar(Calendar calendar, int month_index, int year, Size size)
		{
			CalendarPanel calendarPanel = new CalendarPanel()
			{
				Calendar = calendar,
				MonthIndex = month_index,
				Year = year,
				Size = size
			};
			Bitmap bitmap = new Bitmap(calendarPanel.Width, calendarPanel.Height);
			calendarPanel.DrawToBitmap(bitmap, calendarPanel.ClientRectangle);
			return bitmap;
		}

		public static Bitmap Map(Map map, Rectangle view, Encounter enc, Dictionary<Guid, CombatData> heroes, List<TokenLink> tokens)
		{
			MapView mapView = new MapView()
			{
				Map = map,
				Viewpoint = view,
				Mode = MapViewMode.Plain,
				LineOfSight = false,
				Encounter = enc,
				TokenLinks = tokens
			};
			return Screenshot.Map(mapView);
		}

		public static Bitmap Map(MapView mapview)
		{
			if (mapview.Viewpoint == Rectangle.Empty)
			{
				mapview.Size = new Size(mapview.LayoutData.Width * 64, mapview.LayoutData.Height * 64);
			}
			else
			{
				Rectangle viewpoint = mapview.Viewpoint;
				Rectangle rectangle = mapview.Viewpoint;
				mapview.Size = new Size(viewpoint.Width * 64, rectangle.Height * 64);
			}
			Bitmap bitmap = new Bitmap(mapview.Width, mapview.Height);
			mapview.DrawToBitmap(bitmap, mapview.ClientRectangle);
			return bitmap;
		}

		public static Bitmap Plot(Plot plot, Size size)
		{
			PlotView plotView = new PlotView()
			{
				Plot = plot,
				Mode = PlotViewMode.Plain,
				Size = size
			};
			Bitmap bitmap = new Bitmap(plotView.Width, plotView.Height);
			plotView.DrawToBitmap(bitmap, plotView.ClientRectangle);
			return bitmap;
		}
	}
}