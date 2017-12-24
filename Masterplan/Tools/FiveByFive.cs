using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan.Tools
{
	internal class FiveByFive
	{
		public FiveByFive()
		{
		}

		public static void Build(Plot plot)
		{
			foreach (FiveByFiveColumn column in plot.FiveByFive.Columns)
			{
				PlotPoint plotPoint = null;
				foreach (FiveByFiveItem item in column.Items)
				{
					PlotPoint plotPoint1 = new PlotPoint(item.Details)
					{
						Details = item.Details,
						Colour = column.Colour
					};
					plot.Points.Add(plotPoint1);
					if (plotPoint != null)
					{
						plotPoint.Links.Add(plotPoint1.ID);
					}
					plotPoint = plotPoint1;
				}
			}
		}
	}
}