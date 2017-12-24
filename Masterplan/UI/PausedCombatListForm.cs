using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class PausedCombatListForm : Form
	{
		private IContainer components;

		private SplitContainer Splitter;

		private ListView EncounterList;

		private ColumnHeader EncounterHdr;

		private ToolStrip Toolbar;

		private Masterplan.Controls.MapView MapView;

		private ToolStripButton RunBtn;

		private ToolStripButton RemoveBtn;

		private Button CloseBtn;

		public CombatState SelectedCombat
		{
			get
			{
				if (this.EncounterList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.EncounterList.SelectedItems[0].Tag as CombatState;
			}
		}

		public PausedCombatListForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_list();
			this.set_map();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RunBtn.Enabled = this.SelectedCombat != null;
			this.RemoveBtn.Enabled = this.SelectedCombat != null;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void EncounterList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.set_map();
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PausedCombatListForm));
			this.Splitter = new SplitContainer();
			this.EncounterList = new ListView();
			this.EncounterHdr = new ColumnHeader();
			this.Toolbar = new ToolStrip();
			this.RunBtn = new ToolStripButton();
			this.RemoveBtn = new ToolStripButton();
			this.MapView = new Masterplan.Controls.MapView();
			this.CloseBtn = new Button();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.Splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Splitter.FixedPanel = FixedPanel.Panel1;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.EncounterList);
			this.Splitter.Panel1.Controls.Add(this.Toolbar);
			this.Splitter.Panel2.Controls.Add(this.MapView);
			this.Splitter.Size = new System.Drawing.Size(461, 239);
			this.Splitter.SplitterDistance = 180;
			this.Splitter.TabIndex = 0;
			this.EncounterList.Columns.AddRange(new ColumnHeader[] { this.EncounterHdr });
			this.EncounterList.Dock = DockStyle.Fill;
			this.EncounterList.FullRowSelect = true;
			this.EncounterList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.EncounterList.HideSelection = false;
			this.EncounterList.Location = new Point(0, 25);
			this.EncounterList.MultiSelect = false;
			this.EncounterList.Name = "EncounterList";
			this.EncounterList.Size = new System.Drawing.Size(180, 214);
			this.EncounterList.TabIndex = 1;
			this.EncounterList.UseCompatibleStateImageBehavior = false;
			this.EncounterList.View = View.Details;
			this.EncounterList.SelectedIndexChanged += new EventHandler(this.EncounterList_SelectedIndexChanged);
			this.EncounterList.DoubleClick += new EventHandler(this.RunBtn_Click);
			this.EncounterHdr.Text = "Encounters";
			this.EncounterHdr.Width = 150;
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] runBtn = new ToolStripItem[] { this.RunBtn, this.RemoveBtn };
			items.AddRange(runBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(180, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.RunBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RunBtn.Image = (Image)componentResourceManager.GetObject("RunBtn.Image");
			this.RunBtn.ImageTransparentColor = Color.Magenta;
			this.RunBtn.Name = "RunBtn";
			this.RunBtn.Size = new System.Drawing.Size(32, 22);
			this.RunBtn.Text = "Run";
			this.RunBtn.Click += new EventHandler(this.RunBtn_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)componentResourceManager.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.MapView.AllowDrop = true;
			this.MapView.AllowLinkCreation = false;
			this.MapView.AllowScrolling = false;
			this.MapView.BackgroundMap = null;
			this.MapView.BorderSize = 0;
			this.MapView.BorderStyle = BorderStyle.FixedSingle;
			this.MapView.Dock = DockStyle.Fill;
			this.MapView.Encounter = null;
			this.MapView.FrameType = MapDisplayType.Dimmed;
			this.MapView.HighlightAreas = false;
			this.MapView.LineOfSight = false;
			this.MapView.Location = new Point(0, 0);
			this.MapView.Map = null;
			this.MapView.Mode = MapViewMode.Thumbnail;
			this.MapView.Name = "MapView";
			this.MapView.ScalingFactor = 1;
			this.MapView.SelectedTiles = null;
			this.MapView.Selection = new Rectangle(0, 0, 0, 0);
			this.MapView.ShowCreatureLabels = true;
			this.MapView.ShowCreatures = CreatureViewMode.All;
			this.MapView.ShowGrid = MapGridMode.None;
			this.MapView.ShowHealthBars = false;
			this.MapView.Size = new System.Drawing.Size(277, 239);
			this.MapView.TabIndex = 0;
			this.MapView.Tactical = false;
			this.MapView.TokenLinks = null;
			this.MapView.Viewpoint = new Rectangle(0, 0, 0, 0);
			this.CloseBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBtn.Location = new Point(398, 257);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(75, 23);
			this.CloseBtn.TabIndex = 1;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(485, 292);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.Splitter);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PausedCombatListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Paused Encounters";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel1.PerformLayout();
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCombat != null)
			{
				Session.Project.SavedCombats.Remove(this.SelectedCombat);
				Session.Modified = true;
				this.update_list();
				this.set_map();
			}
		}

		private void RunBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedCombat != null)
			{
				Session.Project.SavedCombats.Remove(this.SelectedCombat);
				Session.Modified = true;
				base.Close();
				(new CombatForm(this.SelectedCombat)).Show();
			}
		}

		private void set_map()
		{
			if (this.SelectedCombat == null)
			{
				this.MapView.Map = null;
			}
			else
			{
				Map map = Session.Project.FindTacticalMap(this.SelectedCombat.Encounter.MapID);
				this.MapView.Map = map;
				this.MapView.Viewpoint = this.SelectedCombat.Viewpoint;
				this.MapView.Encounter = this.SelectedCombat.Encounter;
				this.MapView.TokenLinks = this.SelectedCombat.TokenLinks;
				this.MapView.Sketches.Clear();
				foreach (MapSketch sketch in this.SelectedCombat.Sketches)
				{
					this.MapView.Sketches.Add(sketch.Copy());
				}
			}
		}

		private void update_list()
		{
			this.EncounterList.Items.Clear();
			foreach (CombatState savedCombat in Session.Project.SavedCombats)
			{
				ListViewItem listViewItem = this.EncounterList.Items.Add(savedCombat.ToString());
				listViewItem.Tag = savedCombat;
			}
			if (Session.Project.SavedCombats.Count == 0)
			{
				ListViewItem grayText = this.EncounterList.Items.Add("(none)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}

		public void UpdateEncounters()
		{
			this.update_list();
			this.set_map();
		}
	}
}