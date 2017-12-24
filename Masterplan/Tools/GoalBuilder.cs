using Masterplan.Data;
using System;
using System.Collections.Generic;
using Utils;

namespace Masterplan.Tools
{
	internal class GoalBuilder
	{
		public GoalBuilder()
		{
		}

		private static void add_links(List<Goal> goals, Dictionary<Guid, Pair<PlotPoint, PlotPoint>> map)
		{
			foreach (Goal goal in goals)
			{
				Pair<PlotPoint, PlotPoint> item = map[goal.ID];
				foreach (Goal prerequisite in goal.Prerequisites)
				{
					Pair<PlotPoint, PlotPoint> pair = map[prerequisite.ID];
					item.First.Links.Add(pair.First.ID);
					pair.Second.Links.Add(item.Second.ID);
				}
				if (goal.Prerequisites.Count == 0)
				{
					item.First.Links.Add(item.Second.ID);
				}
				GoalBuilder.add_links(goal.Prerequisites, map);
			}
		}

		private static void add_points(Plot plot, List<Goal> goals, Dictionary<Guid, Pair<PlotPoint, PlotPoint>> map)
		{
			foreach (Goal goal in goals)
			{
				PlotPoint plotPoint = new PlotPoint(string.Concat("Discover: ", goal.Name))
				{
					Details = goal.Details
				};
				PlotPoint plotPoint1 = new PlotPoint(string.Concat("Complete: ", goal.Name))
				{
					Details = goal.Details
				};
				plot.Points.Add(plotPoint);
				plot.Points.Add(plotPoint1);
				map[goal.ID] = new Pair<PlotPoint, PlotPoint>(plotPoint, plotPoint1);
				GoalBuilder.add_points(plot, goal.Prerequisites, map);
			}
		}

		public static void Build(Plot plot)
		{
			Dictionary<Guid, Pair<PlotPoint, PlotPoint>> guids = new Dictionary<Guid, Pair<PlotPoint, PlotPoint>>();
			GoalBuilder.add_points(plot, plot.Goals.Goals, guids);
			GoalBuilder.add_links(plot.Goals.Goals, guids);
		}
	}
}