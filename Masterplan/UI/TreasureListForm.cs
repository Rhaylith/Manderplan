using Masterplan;
using Masterplan.Data;
using Masterplan.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TreasureListForm : Form
	{
		private Plot fRootPlot;

		private Button CancelBtn;

		private SplitContainer Splitter;

		private TreeView PlotTree;

		private ListView ItemList;

		private ColumnHeader ItemHdr;

		private ToolStrip Toolbar;

		private ToolStripButton SelectAll;

		private ToolStripButton SelectNone;

		private Label InfoLbl;

		private Label PagesLbl;

		private Button ExportBtn;

		public MagicItem SelectedMagicItem
		{
			get
			{
				if (this.ItemList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.ItemList.SelectedItems[0].Tag as MagicItem;
			}
		}

		public Plot SelectedPlot
		{
			get
			{
				if (this.PlotTree.SelectedNode == null)
				{
					return null;
				}
				return this.PlotTree.SelectedNode.Tag as Plot;
			}
		}

		public TreasureListForm(Plot plot)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fRootPlot = plot;
			this.update_plot_tree();
			this.update_list();
		}

		private int add_nodes(TreeNodeCollection tnc, Plot p)
		{
			int num = 1;
			PlotPoint plotPoint = Session.Project.FindParent(p);
			TreeNode treeNode = tnc.Add((plotPoint != null ? plotPoint.Name : Session.Project.Name));
			treeNode.Tag = p;
			foreach (PlotPoint point in p.Points)
			{
				if (point.Subplot.Points.Count == 0)
				{
					continue;
				}
				num += this.add_nodes(treeNode.Nodes, point.Subplot);
			}
			return num;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.SelectAll.Enabled = this.ItemList.Items.Count != 0;
			this.SelectNone.Enabled = this.ItemList.Items.Count != 0;
			this.ExportBtn.Enabled = this.ItemList.CheckedItems.Count != 0;
			this.PagesLbl.Visible = this.ItemList.CheckedItems.Count > 9;
		}

		private void ExportBtn_Click(object sender, EventArgs e)
		{
			base.Close();
			int count = this.ItemList.CheckedItems.Count / 9;
			if (this.ItemList.CheckedItems.Count % 9 > 0)
			{
				count++;
			}
			for (int i = 0; i != count; i++)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					Filter = Program.HTMLFilter,
					FileName = string.Concat(Session.Project.Name, " Treasure"),
					Title = "Export"
				};
				if (count != 1)
				{
					SaveFileDialog saveFileDialog1 = saveFileDialog;
					object title = saveFileDialog1.Title;
					object[] objArray = new object[] { title, " (page ", i + 1, ")" };
					saveFileDialog1.Title = string.Concat(objArray);
				}
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					List<string> head = HTML.GetHead("Loot", "", DisplaySize.Small);
					head.Add("<BODY>");
					head.Add("<P>");
					head.Add("<TABLE class=clear height=100%>");
					for (int j = 0; j != 3; j++)
					{
						head.Add("<TR class=clear width=33% height=33%>");
						for (int k = 0; k != 3; k++)
						{
							head.Add("<TD width=33% height=33%>");
							int num = i * 9 + j * 3 + k;
							if (this.ItemList.CheckedItems.Count > num)
							{
								MagicItem tag = this.ItemList.CheckedItems[num].Tag as MagicItem;
								if (tag != null)
								{
									head.Add(HTML.MagicItem(tag, DisplaySize.Small, false, false));
								}
							}
							head.Add("</TD>");
						}
						head.Add("</TR>");
					}
					head.Add("</TABLE>");
					head.Add("</P>");
					head.Add("</BODY>");
					head.Add("</HTML>");
					string str = HTML.Concatenate(head);
					File.WriteAllText(saveFileDialog.FileName, str);
				}
			}
		}

		private List<PlotPoint> get_points(Plot p)
		{
			List<PlotPoint> plotPoints = new List<PlotPoint>();
			plotPoints.AddRange(p.Points);
			foreach (PlotPoint point in p.Points)
			{
				plotPoints.AddRange(this.get_points(point.Subplot));
			}
			return plotPoints;
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TreasureListForm));
			this.CancelBtn = new Button();
			this.Splitter = new SplitContainer();
			this.PlotTree = new TreeView();
			this.InfoLbl = new Label();
			this.ItemList = new ListView();
			this.ItemHdr = new ColumnHeader();
			this.PagesLbl = new Label();
			this.Toolbar = new ToolStrip();
			this.SelectAll = new ToolStripButton();
			this.SelectNone = new ToolStripButton();
			this.ExportBtn = new Button();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CancelBtn.Location = new Point(422, 396);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.Splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.PlotTree);
			this.Splitter.Panel1.Controls.Add(this.InfoLbl);
			this.Splitter.Panel2.Controls.Add(this.ItemList);
			this.Splitter.Panel2.Controls.Add(this.PagesLbl);
			this.Splitter.Panel2.Controls.Add(this.Toolbar);
			this.Splitter.Size = new System.Drawing.Size(485, 378);
			this.Splitter.SplitterDistance = 231;
			this.Splitter.TabIndex = 0;
			this.PlotTree.Dock = DockStyle.Fill;
			this.PlotTree.HideSelection = false;
			this.PlotTree.Location = new Point(0, 38);
			this.PlotTree.Name = "PlotTree";
			this.PlotTree.Size = new System.Drawing.Size(231, 340);
			this.PlotTree.TabIndex = 0;
			this.PlotTree.AfterSelect += new TreeViewEventHandler(this.PlotTree_AfterSelect);
			this.InfoLbl.Dock = DockStyle.Top;
			this.InfoLbl.Location = new Point(0, 0);
			this.InfoLbl.Name = "InfoLbl";
			this.InfoLbl.Size = new System.Drawing.Size(231, 38);
			this.InfoLbl.TabIndex = 1;
			this.InfoLbl.Text = "Select a plot point here to see treasure parcels from that subplot";
			this.InfoLbl.TextAlign = ContentAlignment.MiddleLeft;
			this.ItemList.CheckBoxes = true;
			this.ItemList.Columns.AddRange(new ColumnHeader[] { this.ItemHdr });
			this.ItemList.Dock = DockStyle.Fill;
			this.ItemList.FullRowSelect = true;
			this.ItemList.Location = new Point(0, 25);
			this.ItemList.Name = "ItemList";
			this.ItemList.Size = new System.Drawing.Size(250, 337);
			this.ItemList.TabIndex = 0;
			this.ItemList.UseCompatibleStateImageBehavior = false;
			this.ItemList.View = View.Details;
			this.ItemList.DoubleClick += new EventHandler(this.ItemList_DoubleClick);
			this.ItemHdr.Text = "Parcels";
			this.ItemHdr.Width = 220;
			this.PagesLbl.Dock = DockStyle.Bottom;
			this.PagesLbl.Location = new Point(0, 362);
			this.PagesLbl.Name = "PagesLbl";
			this.PagesLbl.Size = new System.Drawing.Size(250, 16);
			this.PagesLbl.TabIndex = 2;
			this.PagesLbl.Text = "Note that this will require multiple pages";
			this.PagesLbl.TextAlign = ContentAlignment.MiddleLeft;
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] selectAll = new ToolStripItem[] { this.SelectAll, this.SelectNone };
			items.AddRange(selectAll);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(250, 25);
			this.Toolbar.TabIndex = 1;
			this.Toolbar.Text = "toolStrip1";
			this.SelectAll.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SelectAll.Image = (Image)componentResourceManager.GetObject("SelectAll.Image");
			this.SelectAll.ImageTransparentColor = Color.Magenta;
			this.SelectAll.Name = "SelectAll";
			this.SelectAll.Size = new System.Drawing.Size(59, 22);
			this.SelectAll.Text = "Select All";
			this.SelectAll.Click += new EventHandler(this.SelectAll_Click);
			this.SelectNone.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.SelectNone.Image = (Image)componentResourceManager.GetObject("SelectNone.Image");
			this.SelectNone.ImageTransparentColor = Color.Magenta;
			this.SelectNone.Name = "SelectNone";
			this.SelectNone.Size = new System.Drawing.Size(74, 22);
			this.SelectNone.Text = "Select None";
			this.SelectNone.Click += new EventHandler(this.SelectNone_Click);
			this.ExportBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.ExportBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ExportBtn.Location = new Point(341, 396);
			this.ExportBtn.Name = "ExportBtn";
			this.ExportBtn.Size = new System.Drawing.Size(75, 23);
			this.ExportBtn.TabIndex = 1;
			this.ExportBtn.Text = "Export";
			this.ExportBtn.UseVisualStyleBackColor = true;
			this.ExportBtn.Click += new EventHandler(this.ExportBtn_Click);
			base.AcceptButton = this.ExportBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(509, 431);
			base.Controls.Add(this.ExportBtn);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.CancelBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TreasureListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Treasure List";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.Panel2.PerformLayout();
			this.Splitter.ResumeLayout(false);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void ItemList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedMagicItem != null)
			{
				(new MagicItemDetailsForm(this.SelectedMagicItem)).ShowDialog();
			}
		}

		private void PlotTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.update_list();
		}

		private void SelectAll_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in this.ItemList.Items)
			{
				item.Checked = true;
			}
		}

		private void SelectNone_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in this.ItemList.Items)
			{
				item.Checked = false;
			}
		}

		private void update_list()
		{
			List<MagicItem> magicItems = new List<MagicItem>();
			foreach (PlotPoint point in this.get_points(this.SelectedPlot))
			{
				foreach (Parcel parcel in point.Parcels)
				{
					if (parcel.MagicItemID == Guid.Empty)
					{
						continue;
					}
					MagicItem magicItem = Session.FindMagicItem(parcel.MagicItemID, SearchType.Global);
					if (magicItem == null || magicItems.Contains(magicItem))
					{
						continue;
					}
					magicItems.Add(magicItem);
				}
			}
			magicItems.Sort();
			this.ItemList.Items.Clear();
			foreach (MagicItem magicItem1 in magicItems)
			{
				ListViewItem listViewItem = this.ItemList.Items.Add(magicItem1.Name);
				listViewItem.Tag = magicItem1;
			}
		}

		private void update_plot_tree()
		{
			this.PlotTree.Nodes.Clear();
			int num = this.add_nodes(this.PlotTree.Nodes, this.fRootPlot);
			this.PlotTree.ExpandAll();
			this.PlotTree.SelectedNode = this.PlotTree.Nodes[0];
			this.Splitter.Panel1Collapsed = num == 1;
		}
	}
}