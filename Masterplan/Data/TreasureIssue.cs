using Masterplan;
using Masterplan.Tools;
using System;
using System.Collections.Generic;

namespace Masterplan.Data
{
	[Serializable]
	public class TreasureIssue : IIssue
	{
		private string fName = "";

		private Masterplan.Data.Plot fPlot;

		public Masterplan.Data.Plot Plot
		{
			get
			{
				return this.fPlot;
			}
		}

		public string PlotName
		{
			get
			{
				return this.fName;
			}
		}

		public string Reason
		{
			get
			{
				int xP = 0;
				int count = 0;
				foreach (PlotPoint point in this.fPlot.Points)
				{
					xP += point.GetXP();
					foreach (PlotPoint subtree in point.Subtree)
					{
						count += subtree.Parcels.Count;
					}
				}
				int heroXP = Experience.GetHeroXP(Session.Project.Party.Level);
				heroXP = heroXP + xP / Session.Project.Party.Size;
				int heroLevel = Experience.GetHeroLevel(heroXP);
				int level = heroLevel - Session.Project.Party.Level;
				int num = heroXP - Experience.GetHeroXP(heroLevel);
				int heroXP1 = Experience.GetHeroXP(heroLevel + 1) - Experience.GetHeroXP(heroLevel);
				if (heroXP1 == 0)
				{
					return "";
				}
				int size = 10 + (Session.Project.Party.Size - 5);
				int num1 = size * level;
				num1 = num1 + num * size / heroXP1;
				int num2 = (int)((double)num1 * 0.3);
				int num3 = num1 + num2;
				int num4 = num1 - num2;
				string str = "";
				if (count < num4)
				{
					str = "Too few treasure parcels are available, compared to the amount of XP given.";
				}
				if (count > num3)
				{
					str = "Too many treasure parcels are available, compared to the amount of XP given.";
				}
				if (str != "")
				{
					bool flag = false;
					foreach (PlotPoint plotPoint in this.fPlot.Points)
					{
						if (plotPoint.Subplot.Points.Count == 0)
						{
							continue;
						}
						flag = true;
						break;
					}
					str = string.Concat(str, Environment.NewLine);
					str = string.Concat(str, "This plot");
					if (flag)
					{
						str = string.Concat(str, " (and its subplots)");
					}
					str = string.Concat(str, " should contain ");
					if (num4 != num3)
					{
						object obj = str;
						object[] objArray = new object[] { obj, num4, " - ", num3 };
						str = string.Concat(objArray);
					}
					else
					{
						str = string.Concat(str, num3.ToString());
					}
					object obj1 = str;
					object[] objArray1 = new object[] { obj1, " parcels; currently ", count, " are available." };
					str = string.Concat(objArray1);
				}
				return str;
			}
		}

		public TreasureIssue(string name, Masterplan.Data.Plot plot)
		{
			this.fName = name;
			this.fPlot = plot;
		}

		public override string ToString()
		{
			return string.Concat(this.fName, ": ", this.Reason);
		}
	}
}