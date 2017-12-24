using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class IssuesForm : Form
	{
		private IContainer components;

		private WebBrowser Browser;

		public IssuesForm(Plot plot)
		{
			this.InitializeComponent();
			List<PlotPoint> allPlotPoints = plot.AllPlotPoints;
			List<string> strs = new List<string>();
			strs.AddRange(HTML.GetHead("Plot Design Issues", "", DisplaySize.Small));
			strs.Add("<BODY>");
			List<DifficultyIssue> difficultyIssues = new List<DifficultyIssue>();
			foreach (PlotPoint allPlotPoint in allPlotPoints)
			{
				DifficultyIssue difficultyIssue = new DifficultyIssue(allPlotPoint);
				if (difficultyIssue.Reason == "")
				{
					continue;
				}
				difficultyIssues.Add(difficultyIssue);
			}
			strs.Add("<H4>Difficulty Issues</H4>");
			if (difficultyIssues.Count == 0)
			{
				strs.Add("<P class=instruction>");
				strs.Add("(none)");
				strs.Add("</P>");
			}
			else
			{
				foreach (DifficultyIssue difficultyIssue1 in difficultyIssues)
				{
					strs.Add("<P>");
					object[] point = new object[] { "<B>", difficultyIssue1.Point, "</B>: ", difficultyIssue1.Reason };
					strs.Add(string.Concat(point));
					strs.Add("</P>");
				}
			}
			strs.Add("<HR>");
			List<CreatureIssue> creatureIssues = new List<CreatureIssue>();
			foreach (PlotPoint plotPoint in allPlotPoints)
			{
				if (!(plotPoint.Element is Encounter))
				{
					continue;
				}
				CreatureIssue creatureIssue = new CreatureIssue(plotPoint);
				if (creatureIssue.Reason == "")
				{
					continue;
				}
				creatureIssues.Add(creatureIssue);
			}
			strs.Add("<H4>Creature Choice Issues</H4>");
			if (difficultyIssues.Count == 0)
			{
				strs.Add("<P class=instruction>");
				strs.Add("(none)");
				strs.Add("</P>");
			}
			else
			{
				foreach (CreatureIssue creatureIssue1 in creatureIssues)
				{
					strs.Add("<P>");
					object[] objArray = new object[] { "<B>", creatureIssue1.Point, "</B>: ", creatureIssue1.Reason };
					strs.Add(string.Concat(objArray));
					strs.Add("</P>");
				}
			}
			strs.Add("<HR>");
			List<SkillIssue> skillIssues = new List<SkillIssue>();
			foreach (PlotPoint allPlotPoint1 in allPlotPoints)
			{
				if (!(allPlotPoint1.Element is SkillChallenge))
				{
					continue;
				}
				SkillIssue skillIssue = new SkillIssue(allPlotPoint1);
				if (skillIssue.Reason == "")
				{
					continue;
				}
				skillIssues.Add(skillIssue);
			}
			strs.Add("<H4>Undefined Skill Challenges</H4>");
			if (skillIssues.Count == 0)
			{
				strs.Add("<P class=instruction>");
				strs.Add("(none)");
				strs.Add("</P>");
			}
			else
			{
				foreach (SkillIssue skillIssue1 in skillIssues)
				{
					strs.Add("<P>");
					object[] point1 = new object[] { "<B>", skillIssue1.Point, "</B>: ", skillIssue1.Reason };
					strs.Add(string.Concat(point1));
					strs.Add("</P>");
				}
			}
			strs.Add("<HR>");
			List<ParcelIssue> parcelIssues = new List<ParcelIssue>();
			foreach (PlotPoint plotPoint1 in allPlotPoints)
			{
				foreach (Parcel parcel in plotPoint1.Parcels)
				{
					if (parcel.Name != "")
					{
						continue;
					}
					parcelIssues.Add(new ParcelIssue(parcel, plotPoint1));
				}
			}
			strs.Add("<H4>Undefined Treasure Parcels</H4>");
			if (parcelIssues.Count == 0)
			{
				strs.Add("<P class=instruction>");
				strs.Add("(none)");
				strs.Add("</P>");
			}
			else
			{
				foreach (ParcelIssue parcelIssue in parcelIssues)
				{
					strs.Add("<P>");
					object[] objArray1 = new object[] { "<B>", parcelIssue.Point, "</B>: ", parcelIssue.Reason };
					strs.Add(string.Concat(objArray1));
					strs.Add("</P>");
				}
			}
			strs.Add("<HR>");
			List<TreasureIssue> treasureIssues = new List<TreasureIssue>();
			PlotPoint plotPoint2 = Session.Project.FindParent(plot);
			this.add_treasure_issues((plotPoint2 != null ? plotPoint2.Name : Session.Project.Name), plot, treasureIssues);
			strs.Add("<H4>Treasure Allocation Issues</H4>");
			if (treasureIssues.Count == 0)
			{
				strs.Add("<P class=instruction>");
				strs.Add("(none)");
				strs.Add("</P>");
			}
			else
			{
				foreach (TreasureIssue treasureIssue in treasureIssues)
				{
					strs.Add("<P>");
					strs.Add(string.Concat("<B>", treasureIssue.PlotName, "</B>: ", treasureIssue.Reason));
					strs.Add("</P>");
				}
			}
			strs.Add("</BODY>");
			strs.Add("</HTML>");
			this.Browser.DocumentText = HTML.Concatenate(strs);
		}

		private void add_treasure_issues(string plotname, Plot plot, List<TreasureIssue> treasure_issues)
		{
			TreasureIssue treasureIssue = new TreasureIssue(plotname, plot);
			if (treasureIssue.Reason != "")
			{
				treasure_issues.Add(treasureIssue);
				foreach (PlotPoint point in plot.Points)
				{
					this.add_treasure_issues(point.Name, point.Subplot, treasure_issues);
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.Browser = new WebBrowser();
			base.SuspendLayout();
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 0);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new System.Drawing.Size(468, 291);
			this.Browser.TabIndex = 2;
			this.Browser.WebBrowserShortcutsEnabled = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(468, 291);
			base.Controls.Add(this.Browser);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "IssuesForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Plot Design Issues";
			base.ResumeLayout(false);
		}
	}
}