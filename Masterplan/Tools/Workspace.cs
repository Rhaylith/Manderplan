using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;

namespace Masterplan.Tools
{
	internal class Workspace
	{
		public Workspace()
		{
		}

		public static List<List<PlotPoint>> FindLayers(Plot plot)
		{
			List<List<PlotPoint>> lists = new List<List<PlotPoint>>();
			List<PlotPoint> plotPoints = new List<PlotPoint>(plot.Points);
			while (plotPoints.Count > 0)
			{
				List<PlotPoint> plotPoints1 = new List<PlotPoint>();
				foreach (PlotPoint plotPoint in plotPoints)
				{
					bool flag = true;
					foreach (PlotPoint plotPoint1 in plotPoints)
					{
						if (plotPoint1 == plotPoint || !plotPoint1.Links.Contains(plotPoint.ID))
						{
							continue;
						}
						flag = false;
						break;
					}
					if (!flag)
					{
						continue;
					}
					plotPoints1.Add(plotPoint);
				}
				if (plotPoints1.Count == 0)
				{
					plotPoints1.AddRange(plotPoints);
				}
				lists.Add(plotPoints1);
				foreach (PlotPoint plotPoint2 in plotPoints1)
				{
					plotPoints.Remove(plotPoint2);
				}
			}
			return lists;
		}

		public static int GetLayerXP(List<PlotPoint> layer)
		{
			int xP = 0;
			int num = 0;
			int num1 = 0;
			foreach (PlotPoint plotPoint in layer)
			{
				if (plotPoint == null)
				{
					continue;
				}
				switch (plotPoint.State)
				{
					case PlotPointState.Normal:
					{
						num += plotPoint.GetXP();
						num1++;
						continue;
					}
					case PlotPointState.Completed:
					{
						xP += plotPoint.GetXP();
						continue;
					}
					default:
					{
						continue;
					}
				}
			}
			int num2 = num;
			if (!Session.Preferences.AllXP)
			{
				num2 = (num1 != 0 ? num / num1 : 0);
			}
			return xP + num2;
		}

		public static int GetPartyLevel(PlotPoint pp)
		{
			int totalXP = Workspace.GetTotalXP(pp);
			int size = totalXP / Session.Project.Party.Size;
			return Experience.GetHeroLevel(size);
		}

		public static int GetTotalXP(PlotPoint pp)
		{
			int xP = Session.Project.Party.XP * Session.Project.Party.Size;
			do
			{
				Plot plot = Session.Project.FindParent(pp);
				if (plot == null)
				{
					break;
				}
				foreach (List<PlotPoint> plotPoints in Workspace.FindLayers(plot))
				{
					bool flag = false;
					foreach (PlotPoint plotPoint in plotPoints)
					{
						if (plotPoint.ID != pp.ID)
						{
							continue;
						}
						flag = true;
						break;
					}
					if (flag)
					{
						break;
					}
					xP += Workspace.GetLayerXP(plotPoints);
				}
				pp = Session.Project.FindParent(plot);
			}
			while (pp != null);
			return xP;
		}
	}
}