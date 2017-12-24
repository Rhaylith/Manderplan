using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class MiniChecklistForm : Form
	{
		private IContainer components;

		private Button OKBtn;

		private ListView TileList;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private SplitContainer Splitter;

		private TreeView PlotTree;

		public MiniChecklistForm()
		{
			this.InitializeComponent();
			this.update_tree();
			if (this.PlotTree.Nodes[0].Nodes.Count == 0)
			{
				this.Splitter.Panel1Collapsed = true;
			}
			this.PlotTree.SelectedNode = this.PlotTree.Nodes[0];
		}

		private void add_navigation_node(PlotPoint pp, TreeNode parent)
		{
			try
			{
				string str = (pp != null ? pp.Name : Session.Project.Name);
				TreeNode treeNode = ((parent != null ? parent.Nodes : this.PlotTree.Nodes)).Add(str);
				treeNode.Tag = (pp != null ? pp.Subplot : Session.Project.Plot);
				foreach (PlotPoint plotPoint in (pp != null ? pp.Subplot.Points : Session.Project.Plot.Points))
				{
					if (plotPoint.Subplot.Points.Count == 0)
					{
						continue;
					}
					this.add_navigation_node(plotPoint, treeNode);
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
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
			this.OKBtn = new Button();
			this.TileList = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.Splitter = new SplitContainer();
			this.PlotTree = new TreeView();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(454, 324);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			ListView.ColumnHeaderCollection columns = this.TileList.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3 };
			columns.AddRange(columnHeaderArray);
			this.TileList.Dock = DockStyle.Fill;
			this.TileList.FullRowSelect = true;
			this.TileList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TileList.HideSelection = false;
			this.TileList.Location = new Point(0, 0);
			this.TileList.MultiSelect = false;
			this.TileList.Name = "TileList";
			this.TileList.Size = new System.Drawing.Size(517, 213);
			this.TileList.TabIndex = 1;
			this.TileList.UseCompatibleStateImageBehavior = false;
			this.TileList.View = View.Details;
			this.columnHeader1.Text = "Creature";
			this.columnHeader1.Width = 148;
			this.columnHeader2.Text = "Info";
			this.columnHeader2.Width = 280;
			this.columnHeader3.Text = "Count";
			this.columnHeader3.TextAlign = HorizontalAlignment.Right;
			this.Splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Orientation = Orientation.Horizontal;
			this.Splitter.Panel1.Controls.Add(this.PlotTree);
			this.Splitter.Panel2.Controls.Add(this.TileList);
			this.Splitter.Size = new System.Drawing.Size(517, 306);
			this.Splitter.SplitterDistance = 89;
			this.Splitter.TabIndex = 2;
			this.PlotTree.Dock = DockStyle.Fill;
			this.PlotTree.Location = new Point(0, 0);
			this.PlotTree.Name = "PlotTree";
			this.PlotTree.Size = new System.Drawing.Size(517, 89);
			this.PlotTree.TabIndex = 0;
			this.PlotTree.AfterSelect += new TreeViewEventHandler(this.PlotTree_AfterSelect);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(541, 359);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MiniChecklistForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Miniature Checklist";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void PlotTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			Plot tag = e.Node.Tag as Plot;
			if (tag != null)
			{
				this.update_list(tag);
			}
		}

		private void update_list(Plot plot)
		{
			List<Encounter> encounters = new List<Encounter>();
			foreach (PlotPoint allPlotPoint in plot.AllPlotPoints)
			{
				Encounter element = allPlotPoint.Element as Encounter;
				if (element == null)
				{
					continue;
				}
				encounters.Add(element);
			}
			Dictionary<Guid, int> guids = new Dictionary<Guid, int>();
			foreach (Encounter encounter in encounters)
			{
				Dictionary<Guid, int> guids1 = new Dictionary<Guid, int>();
				foreach (EncounterSlot slot in encounter.Slots)
				{
					if (!guids1.ContainsKey(slot.Card.CreatureID))
					{
						guids1[slot.Card.CreatureID] = 0;
					}
					Dictionary<Guid, int> item = guids1;
					Dictionary<Guid, int> guids2 = item;
					Guid creatureID = slot.Card.CreatureID;
					Guid guid = creatureID;
					item[creatureID] = guids2[guid] + slot.CombatData.Count;
				}
				foreach (Guid key in guids1.Keys)
				{
					if (!guids.ContainsKey(key))
					{
						guids[key] = 0;
					}
					if (guids1[key] <= guids[key])
					{
						continue;
					}
					guids[key] = guids1[key];
				}
			}
			this.TileList.Items.Clear();
			foreach (Guid key1 in guids.Keys)
			{
				ICreature creature = Session.FindCreature(key1, SearchType.Global);
				int num = guids[key1];
				if (creature == null)
				{
					continue;
				}
				ListViewItem listViewItem = this.TileList.Items.Add(creature.Name);
				string str = string.Concat(creature.Size, " size");
				if (creature.Keywords != "")
				{
					str = string.Concat(str, ", ", creature.Keywords);
				}
				foreach (CreaturePower creaturePower in creature.CreaturePowers)
				{
					str = string.Concat(str, ", ", creaturePower.Name);
				}
				listViewItem.SubItems.Add(str);
				if (num <= 1)
				{
					listViewItem.SubItems.Add("");
				}
				else
				{
					listViewItem.SubItems.Add(string.Concat("x", num));
				}
			}
		}

		private void update_tree()
		{
			this.add_navigation_node(null, null);
			this.PlotTree.ExpandAll();
		}
	}
}