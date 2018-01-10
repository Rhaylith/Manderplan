using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class EncounterReportForm : Form
	{
		private EncounterReport fReport;

		private Encounter fEncounter;

		private ReportType fReportType;

		private BreakdownType fBreakdownType;

		private WebBrowser Browser;

		private ToolStrip Toolbar;

		private ToolStripButton ExportBtn;

		private ToolStripDropDownButton ReportBtn;

		private ToolStripDropDownButton BreakdownBtn;

		private ToolStripMenuItem ReportTime;

		private ToolStripMenuItem ReportDamageEnemies;

		private ToolStripMenuItem ReportDamageAllies;

		private ToolStripMenuItem ReportMovement;

		private ToolStripMenuItem BreakdownIndividually;

		private ToolStripMenuItem BreakdownByController;

		private ToolStripMenuItem BreakdownByFaction;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton PlayerViewBtn;

		private SplitContainer Splitter;

		private DemographicsPanel Graph;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripLabel MVPLbl;

		public EncounterReportForm(EncounterLog log, Encounter enc)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fReport = log.CreateReport(enc, true);
			this.fEncounter = enc;
			if (this.fEncounter.MapID == Guid.Empty)
			{
				this.ReportBtn.DropDownItems.Remove(this.ReportMovement);
			}
			this.update_report();
			this.update_mvp();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ReportTime.Checked = this.fReportType == ReportType.Time;
			this.ReportDamageEnemies.Checked = this.fReportType == ReportType.DamageToEnemies;
			this.ReportDamageAllies.Checked = this.fReportType == ReportType.DamageToAllies;
			this.ReportMovement.Checked = this.fReportType == ReportType.Movement;
			this.BreakdownIndividually.Checked = this.fBreakdownType == BreakdownType.Individual;
			this.BreakdownByController.Checked = this.fBreakdownType == BreakdownType.Controller;
			this.BreakdownByFaction.Checked = this.fBreakdownType == BreakdownType.Faction;
		}

		private void BreakdownByController_Click(object sender, EventArgs e)
		{
			this.fBreakdownType = BreakdownType.Controller;
			this.update_report();
		}

		private void BreakdownByFaction_Click(object sender, EventArgs e)
		{
			this.fBreakdownType = BreakdownType.Faction;
			this.update_report();
		}

		private void BreakdownIndividually_Click(object sender, EventArgs e)
		{
			this.fBreakdownType = BreakdownType.Individual;
			this.update_report();
		}

		private void ExportBtn_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				FileName = "Encounter Report",
				Filter = Program.HTMLFilter
			};
			if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				File.WriteAllText(saveFileDialog.FileName, this.Browser.DocumentText);
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(EncounterReportForm));
			this.Browser = new WebBrowser();
			this.Toolbar = new ToolStrip();
			this.ReportBtn = new ToolStripDropDownButton();
			this.ReportTime = new ToolStripMenuItem();
			this.ReportDamageEnemies = new ToolStripMenuItem();
			this.ReportDamageAllies = new ToolStripMenuItem();
			this.ReportMovement = new ToolStripMenuItem();
			this.BreakdownBtn = new ToolStripDropDownButton();
			this.BreakdownIndividually = new ToolStripMenuItem();
			this.BreakdownByController = new ToolStripMenuItem();
			this.BreakdownByFaction = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.PlayerViewBtn = new ToolStripButton();
			this.ExportBtn = new ToolStripButton();
			this.Splitter = new SplitContainer();
			this.Graph = new DemographicsPanel();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.MVPLbl = new ToolStripLabel();
			this.Toolbar.SuspendLayout();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			base.SuspendLayout();
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.IsWebBrowserContextMenuEnabled = false;
			this.Browser.Location = new Point(0, 0);
			this.Browser.Name = "Browser";
			this.Browser.ScriptErrorsSuppressed = true;
			this.Browser.Size = new System.Drawing.Size(404, 266);
			this.Browser.TabIndex = 2;
			this.Browser.WebBrowserShortcutsEnabled = false;
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] reportBtn = new ToolStripItem[] { this.ReportBtn, this.BreakdownBtn, this.toolStripSeparator1, this.PlayerViewBtn, this.ExportBtn, this.toolStripSeparator2, this.MVPLbl };
			items.AddRange(reportBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(811, 25);
			this.Toolbar.TabIndex = 3;
			this.Toolbar.Text = "toolStrip1";
			this.ReportBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems = this.ReportBtn.DropDownItems;
			ToolStripItem[] reportTime = new ToolStripItem[] { this.ReportTime, this.ReportDamageEnemies, this.ReportDamageAllies, this.ReportMovement };
			dropDownItems.AddRange(reportTime);
			this.ReportBtn.Image = (Image)componentResourceManager.GetObject("ReportBtn.Image");
			this.ReportBtn.ImageTransparentColor = Color.Magenta;
			this.ReportBtn.Name = "ReportBtn";
			this.ReportBtn.Size = new System.Drawing.Size(84, 22);
			this.ReportBtn.Text = "Report Type";
			this.ReportTime.Name = "ReportTime";
			this.ReportTime.Size = new System.Drawing.Size(218, 22);
			this.ReportTime.Text = "Time Taken";
			this.ReportTime.Click += new EventHandler(this.ReportTime_Click);
			this.ReportDamageEnemies.Name = "ReportDamageEnemies";
			this.ReportDamageEnemies.Size = new System.Drawing.Size(218, 22);
			this.ReportDamageEnemies.Text = "Damage Done (to enemies)";
			this.ReportDamageEnemies.Click += new EventHandler(this.ReportDamageEnemies_Click);
			this.ReportDamageAllies.Name = "ReportDamageAllies";
			this.ReportDamageAllies.Size = new System.Drawing.Size(218, 22);
			this.ReportDamageAllies.Text = "Damage Done (to allies)";
			this.ReportDamageAllies.Click += new EventHandler(this.ReportDamageAllies_Click);
			this.ReportMovement.Name = "ReportMovement";
			this.ReportMovement.Size = new System.Drawing.Size(218, 22);
			this.ReportMovement.Text = "Movement";
			this.ReportMovement.Click += new EventHandler(this.ReportMovement_Click);
			this.BreakdownBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection toolStripItemCollections = this.BreakdownBtn.DropDownItems;
			ToolStripItem[] breakdownIndividually = new ToolStripItem[] { this.BreakdownIndividually, this.BreakdownByController, this.BreakdownByFaction };
			toolStripItemCollections.AddRange(breakdownIndividually);
			this.BreakdownBtn.Image = (Image)componentResourceManager.GetObject("BreakdownBtn.Image");
			this.BreakdownBtn.ImageTransparentColor = Color.Magenta;
			this.BreakdownBtn.Name = "BreakdownBtn";
			this.BreakdownBtn.Size = new System.Drawing.Size(70, 22);
			this.BreakdownBtn.Text = "Grouping";
			this.BreakdownIndividually.Name = "BreakdownIndividually";
			this.BreakdownIndividually.Size = new System.Drawing.Size(183, 22);
			this.BreakdownIndividually.Text = "Individually (default)";
			this.BreakdownIndividually.Click += new EventHandler(this.BreakdownIndividually_Click);
			this.BreakdownByController.Name = "BreakdownByController";
			this.BreakdownByController.Size = new System.Drawing.Size(183, 22);
			this.BreakdownByController.Text = "By Controller";
			this.BreakdownByController.Click += new EventHandler(this.BreakdownByController_Click);
			this.BreakdownByFaction.Name = "BreakdownByFaction";
			this.BreakdownByFaction.Size = new System.Drawing.Size(183, 22);
			this.BreakdownByFaction.Text = "By Faction";
			this.BreakdownByFaction.Click += new EventHandler(this.BreakdownByFaction_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.PlayerViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.PlayerViewBtn.Image = (Image)componentResourceManager.GetObject("PlayerViewBtn.Image");
			this.PlayerViewBtn.ImageTransparentColor = Color.Magenta;
			this.PlayerViewBtn.Name = "PlayerViewBtn";
			this.PlayerViewBtn.Size = new System.Drawing.Size(114, 22);
			this.PlayerViewBtn.Text = "Send to Player View";
			this.PlayerViewBtn.Click += new EventHandler(this.PlayerViewBtn_Click);
			this.ExportBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.ExportBtn.Image = (Image)componentResourceManager.GetObject("ExportBtn.Image");
			this.ExportBtn.ImageTransparentColor = Color.Magenta;
			this.ExportBtn.Name = "ExportBtn";
			this.ExportBtn.Size = new System.Drawing.Size(44, 22);
			this.ExportBtn.Text = "Export";
			this.ExportBtn.Click += new EventHandler(this.ExportBtn_Click);
			this.Splitter.Dock = DockStyle.Fill;
			this.Splitter.Location = new Point(0, 25);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.Browser);
			this.Splitter.Panel2.Controls.Add(this.Graph);
			this.Splitter.Size = new System.Drawing.Size(811, 266);
			this.Splitter.SplitterDistance = 404;
			this.Splitter.TabIndex = 4;
			this.Graph.Dock = DockStyle.Fill;
			this.Graph.Library = null;
			this.Graph.Location = new Point(0, 0);
			this.Graph.Mode = DemographicsMode.Level;
			this.Graph.Name = "Graph";
			this.Graph.Size = new System.Drawing.Size(403, 266);
			this.Graph.Source = DemographicsSource.Creatures;
			this.Graph.TabIndex = 0;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.MVPLbl.Name = "MVPLbl";
			this.MVPLbl.Size = new System.Drawing.Size(39, 22);
			this.MVPLbl.Text = "[mvp]";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(811, 291);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.Toolbar);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EncounterReportForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Encounter Report";
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void PlayerViewBtn_Click(object sender, EventArgs e)
		{
			if (Session.PlayerView == null)
			{
				Session.PlayerView = new PlayerViewForm(this);
			}
			ReportTable reportTable = this.fReport.CreateTable(this.fReportType, this.fBreakdownType, this.fEncounter);
			Session.PlayerView.ShowEncounterReportTable(reportTable);
		}

		private void ReportDamageAllies_Click(object sender, EventArgs e)
		{
			this.fReportType = ReportType.DamageToAllies;
			this.update_report();
		}

		private void ReportDamageEnemies_Click(object sender, EventArgs e)
		{
			this.fReportType = ReportType.DamageToEnemies;
			this.update_report();
		}

		private void ReportMovement_Click(object sender, EventArgs e)
		{
			this.fReportType = ReportType.Movement;
			this.update_report();
		}

		private void ReportTime_Click(object sender, EventArgs e)
		{
			this.fReportType = ReportType.Time;
			this.update_report();
		}

		private void update_mvp()
		{
			List<Guid> guids = this.fReport.MVPs(this.fEncounter);
			string str = "";
			foreach (Guid guid in guids)
			{
				Hero hero = Session.Project.FindHero(guid);
				if (hero == null)
				{
					continue;
				}
				if (str != "")
				{
					str = string.Concat(str, ", ");
				}
				str = string.Concat(str, hero.Name);
			}
			if (str != "")
			{
				this.MVPLbl.Text = string.Concat("MVP: ", str);
				return;
			}
			this.MVPLbl.Text = "(no MVP for this encounter)";
			this.MVPLbl.Enabled = false;
		}

		private void update_report()
		{
			ReportTable reportTable = this.fReport.CreateTable(this.fReportType, this.fBreakdownType, this.fEncounter);
			this.Browser.DocumentText = HTML.EncounterReportTable(reportTable, DisplaySize.Small);
			this.Graph.ShowTable(reportTable);
		}
	}
}