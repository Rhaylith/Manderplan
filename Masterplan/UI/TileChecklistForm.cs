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
	internal class TileChecklistForm : Form
	{
		private Button OKBtn;

		private ListView TileList;

		private TreeView PlotTree;

		private SplitContainer Splitter;

		public TileChecklistForm()
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

		private void InitializeComponent()
		{
			this.OKBtn = new Button();
			this.TileList = new ListView();
			this.PlotTree = new TreeView();
			this.Splitter = new SplitContainer();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(458, 376);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.TileList.Dock = DockStyle.Fill;
			this.TileList.FullRowSelect = true;
			this.TileList.HideSelection = false;
			this.TileList.Location = new Point(0, 0);
			this.TileList.MultiSelect = false;
			this.TileList.Name = "TileList";
			this.TileList.Size = new System.Drawing.Size(521, 249);
			this.TileList.TabIndex = 1;
			this.TileList.UseCompatibleStateImageBehavior = false;
			this.PlotTree.Dock = DockStyle.Fill;
			this.PlotTree.Location = new Point(0, 0);
			this.PlotTree.Name = "PlotTree";
			this.PlotTree.Size = new System.Drawing.Size(521, 105);
			this.PlotTree.TabIndex = 2;
			this.PlotTree.AfterSelect += new TreeViewEventHandler(this.PlotTree_AfterSelect);
			this.Splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Orientation = Orientation.Horizontal;
			this.Splitter.Panel1.Controls.Add(this.PlotTree);
			this.Splitter.Panel2.Controls.Add(this.TileList);
			this.Splitter.Size = new System.Drawing.Size(521, 358);
			this.Splitter.SplitterDistance = 105;
			this.Splitter.TabIndex = 3;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(545, 411);
			base.Controls.Add(this.Splitter);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TileChecklistForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Map Tile Checklist";
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
			List<Map> maps = new List<Map>();
			List<PlotPoint> allPlotPoints = plot.AllPlotPoints;
			List<Guid> guids = new List<Guid>();
			foreach (PlotPoint allPlotPoint in allPlotPoints)
			{
				Encounter element = allPlotPoint.Element as Encounter;
				if (element != null && element.MapID != Guid.Empty)
				{
					guids.Add(element.MapID);
				}
				MapElement mapElement = allPlotPoint.Element as MapElement;
				if (mapElement == null)
				{
					continue;
				}
				guids.Add(mapElement.MapID);
			}
			foreach (Guid guid in guids)
			{
				Map map = Session.Project.FindTacticalMap(guid);
				if (map == null)
				{
					continue;
				}
				maps.Add(map);
			}
			Dictionary<Guid, int> item = new Dictionary<Guid, int>();
			foreach (Map map1 in maps)
			{
				Dictionary<Guid, int> guids1 = new Dictionary<Guid, int>();
				foreach (TileData tile in map1.Tiles)
				{
					if (!guids1.ContainsKey(tile.TileID))
					{
						guids1[tile.TileID] = 0;
					}
					Dictionary<Guid, int> item1 = guids1;
					Dictionary<Guid, int> guids2 = item1;
					Guid tileID = tile.TileID;
					item1[tileID] = guids2[tileID] + 1;
				}
				foreach (Guid key in guids1.Keys)
				{
					if (!item.ContainsKey(key))
					{
						item[key] = 0;
					}
					if (guids1[key] <= item[key])
					{
						continue;
					}
					item[key] = guids1[key];
				}
			}
			List<string> strs = new List<string>();
			foreach (Guid key1 in item.Keys)
			{
				Library library = Session.FindLibrary(Session.FindTile(key1, SearchType.Global));
				if (strs.Contains(library.Name))
				{
					continue;
				}
				strs.Add(library.Name);
			}
			strs.Sort();
			this.TileList.Groups.Clear();
			foreach (string str in strs)
			{
				this.TileList.Groups.Add(str, str);
			}
			this.TileList.LargeImageList = new ImageList()
			{
				ImageSize = new System.Drawing.Size(64, 64)
			};
			this.TileList.Items.Clear();
			foreach (Guid guid1 in item.Keys)
			{
				Tile tile1 = Session.FindTile(guid1, SearchType.Global);
				Library library1 = Session.FindLibrary(tile1);
				ListViewItem count = this.TileList.Items.Add(string.Concat("x ", item[guid1]));
				count.Tag = tile1;
				count.Group = this.TileList.Groups[library1.Name];
				Image image = (tile1.Image != null ? tile1.Image : tile1.BlankImage);
				Bitmap bitmap = new Bitmap(64, 64);
				if (tile1.Size.Width <= tile1.Size.Height)
				{
					System.Drawing.Size size = tile1.Size;
					System.Drawing.Size size1 = tile1.Size;
					int width = size.Width * 64 / size1.Height;
					Rectangle rectangle = new Rectangle((64 - width) / 2, 0, width, 64);
					Graphics.FromImage(bitmap).DrawImage(image, rectangle);
				}
				else
				{
					System.Drawing.Size size2 = tile1.Size;
					System.Drawing.Size size3 = tile1.Size;
					int height = size2.Height * 64 / size3.Width;
					Rectangle rectangle1 = new Rectangle(0, (64 - height) / 2, 64, height);
					Graphics.FromImage(bitmap).DrawImage(image, rectangle1);
				}
				this.TileList.LargeImageList.Images.Add(bitmap);
				count.ImageIndex = this.TileList.LargeImageList.Images.Count - 1;
			}
		}

		private void update_tree()
		{
			this.add_navigation_node(null, null);
			this.PlotTree.ExpandAll();
		}
	}
}