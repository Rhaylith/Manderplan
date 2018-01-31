using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Events;
using Masterplan.Tools;
using Masterplan.Tools.Generators;
using Masterplan.Wizards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Utils;
using Utils.Wizards;

namespace Masterplan.UI
{
	internal class MapBuilderForm : Form
	{
		private IContainer components;

		private SplitContainer Splitter;

		private ListView TileList;

		private ColumnHeader TileHdr;

		private Masterplan.Controls.MapView MapView;

		private ToolStrip Toolbar;

		private ToolStripLabel NameLbl;

		private ToolStripTextBox NameBox;

		private ToolStripDropDownButton OrderingBtn;

		private ToolStripMenuItem OrderingFront;

		private ToolStripMenuItem OrderingBack;

		private ToolStripDropDownButton at;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStrip TileToolbar;

		private ToolStripMenuItem ToolsHighlightAreas;

		private TabControl Pages;

		private TabPage TilesPage;

		private TabPage AreasPage;

		private ToolStrip AreaToolbar;

		private ListView AreaList;

		private ToolStripButton AreaRemoveBtn;

		private ToolStripButton AreaEditBtn;

		private ColumnHeader AreaHdr;

		private ToolStripDropDownButton TilesViewBtn;

		private ToolStripMenuItem ViewGroupBy;

		private ToolStripMenuItem GroupByTileSet;

		private ToolStripMenuItem GroupBySize;

		private System.Windows.Forms.ContextMenuStrip MapContextMenu;

		private ToolStripMenuItem ContextCreate;

		private ToolStripMenuItem ContextClear;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem GroupByCategory;

		private ToolStripMenuItem ToolsAutoBuild;

		private ToolTip Tooltip;

		private ToolStripSeparator toolStripSeparator2;

		private TrackBar ZoomGauge;

		private ToolStripMenuItem ToolsNavigate;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem ToolsReset;

		private ToolStripMenuItem ContextSelect;

		private ToolStripSeparator toolStripMenuItem1;

		private System.Windows.Forms.ContextMenuStrip TileContextMenu;

		private ToolStripMenuItem TileContextRemove;

		private ToolStripMenuItem TileContextSwap;

		private ToolStripSeparator toolStripMenuItem2;

		private ToolStripMenuItem ToolsSelectBackground;

		private ToolStripMenuItem ToolsClearBackground;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton FullMapBtn;

		private Panel MainPanel;

		private Button OKBtn;

		private Button CancelBtn;

		private ToolStripMenuItem ToolsImportMap;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripMenuItem TileContextDuplicate;

		private ToolStripSplitButton RemoveBtn;

		private ToolStripSplitButton RotateLeftBtn;

		private ToolStripSplitButton RotateRightBtn;

		private ToolStripMenuItem clearMapToolStripMenuItem;

		private ToolStripMenuItem rotateMapLeftToolStripMenuItem;

		private ToolStripMenuItem rotateMapRightToolStripMenuItem;

		private Panel MapFilterPanel;

		private ToolStripMenuItem ViewSize;

		private ToolStripMenuItem SizeSmall;

		private ToolStripMenuItem SizeMedium;

		private ToolStripMenuItem SizeLarge;

		private ToolStripButton FilterBtn;

		private Button SelectLibrariesBtn;

		private TextBox SearchBox;

		private Label SearchLbl;

		private Masterplan.Data.Map fMap;

		private Dictionary<Guid, bool> fTileSets = new Dictionary<Guid, bool>();

		public Masterplan.Data.Map Map
		{
			get
			{
				return this.fMap;
			}
		}

		public MapArea SelectedArea
		{
			get
			{
				if (this.AreaList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.AreaList.SelectedItems[0].Tag as MapArea;
			}
		}

		public Tile SelectedTile
		{
			get
			{
				if (this.TileList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.TileList.SelectedItems[0].Tag as Tile;
			}
		}

		public MapBuilderForm(Masterplan.Data.Map m, bool autobuild)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			List<Library> libraries = new List<Library>();
			libraries.AddRange(Session.Libraries);
			if (Session.Project != null)
			{
				libraries.Add(Session.Project.Library);
			}
			int count = 0;
			foreach (Library library in libraries)
			{
				if (Session.Preferences.TileLibraries == null)
				{
					this.fTileSets[library.ID] = true;
				}
				else
				{
					this.fTileSets[library.ID] = Session.Preferences.TileLibraries.Contains(library.ID);
				}
				if (!this.fTileSets[library.ID])
				{
					continue;
				}
				count = library.Tiles.Count;
			}
			if (count == 0)
			{
				foreach (Library library1 in libraries)
				{
					this.fTileSets[library1.ID] = true;
				}
			}
			this.MapFilterPanel.Visible = false;
			this.populate_tiles();
			this.fMap = m.Copy();
			this.MapView.Map = this.fMap;
			this.NameBox.Text = this.fMap.Name;
			if (autobuild)
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				this.ToolsAutoBuild_Click(null, null);
				foreach (MapArea area in this.fMap.Areas)
				{
					area.Name = RoomBuilder.Name();
					area.Details = RoomBuilder.Details();
				}
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
			this.update_areas();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = (this.MapView.SelectedTiles == null ? false : this.MapView.SelectedTiles.Count != 0);
			this.RotateLeftBtn.Enabled = (this.MapView.SelectedTiles == null ? false : this.MapView.SelectedTiles.Count != 0);
			this.RotateRightBtn.Enabled = (this.MapView.SelectedTiles == null ? false : this.MapView.SelectedTiles.Count != 0);
			this.OrderingBtn.Enabled = (this.MapView.SelectedTiles == null ? false : this.MapView.SelectedTiles.Count == 1);
			this.ToolsHighlightAreas.Checked = this.MapView.HighlightAreas;
			this.ToolsNavigate.Checked = this.MapView.AllowScrolling;
			this.ToolsClearBackground.Enabled = this.MapView.BackgroundMap != null;
			this.AreaRemoveBtn.Enabled = this.SelectedArea != null;
			this.AreaEditBtn.Enabled = this.SelectedArea != null;
			this.FullMapBtn.Enabled = this.MapView.Viewpoint != Rectangle.Empty;
			this.GroupByTileSet.Checked = Session.Preferences.TileView == TileView.Library;
			this.GroupBySize.Checked = Session.Preferences.TileView == TileView.Size;
			this.SizeSmall.Checked = Session.Preferences.TileSize == TileSize.Small;
			this.SizeMedium.Checked = Session.Preferences.TileSize == TileSize.Medium;
			this.SizeLarge.Checked = Session.Preferences.TileSize == TileSize.Large;
			this.FilterBtn.Checked = this.MapFilterPanel.Visible;
			this.OKBtn.Enabled = (this.fMap.Name == "" ? false : this.fMap.Tiles.Count != 0);
		}

		private void AreaEditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedArea != null)
			{
				int area = this.fMap.Areas.IndexOf(this.SelectedArea);
				MapAreaForm mapAreaForm = new MapAreaForm(this.SelectedArea, this.fMap);
				if (mapAreaForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.fMap.Areas[area] = mapAreaForm.Area;
					this.update_areas();
					this.MapView.Viewpoint = this.fMap.Areas[area].Region;
				}
			}
		}

		private void AreaList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.SelectedArea == null)
			{
				this.MapView.Viewpoint = Rectangle.Empty;
				return;
			}
			this.MapView.Viewpoint = this.SelectedArea.Region;
		}

		private void AreaRemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedArea != null)
			{
				this.fMap.Areas.Remove(this.SelectedArea);
				this.update_areas();
				this.MapView.Viewpoint = Rectangle.Empty;
			}
		}

		private void ContextClear_Click(object sender, EventArgs e)
		{
			try
			{
				List<TileData> tileDatas = new List<TileData>();
				foreach (TileData tile in this.fMap.Tiles)
				{
					if (!this.MapView.LayoutData.TileSquares.ContainsKey(tile))
					{
						continue;
					}
					Rectangle item = this.MapView.LayoutData.TileSquares[tile];
					if (!this.MapView.Selection.IntersectsWith(item))
					{
						continue;
					}
					tileDatas.Add(tile);
				}
				foreach (TileData tileData in tileDatas)
				{
					this.fMap.Tiles.Remove(tileData);
				}
				this.MapView.Selection = Rectangle.Empty;
				this.MapView.MapChanged();
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ContextCreate_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.MapView.Selection != Rectangle.Empty)
				{
					MapArea mapArea = new MapArea()
					{
						Name = "New Area",
						Region = this.MapView.Selection
					};
					MapAreaForm mapAreaForm = new MapAreaForm(mapArea, this.fMap);
					if (mapAreaForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						this.fMap.Areas.Add(mapAreaForm.Area);
						this.update_areas();
						this.MapView.Selection = Rectangle.Empty;
					}
				}
			}
			catch (Exception exception)
			{
				LogSystem.Trace(exception);
			}
		}

		private void ContextSelect_Click(object sender, EventArgs e)
		{
			try
			{
				this.MapView.SelectedTiles.Clear();
				foreach (TileData tile in this.fMap.Tiles)
				{
					if (!this.MapView.LayoutData.TileSquares.ContainsKey(tile))
					{
						continue;
					}
					Rectangle item = this.MapView.LayoutData.TileSquares[tile];
					if (!this.MapView.Selection.IntersectsWith(item))
					{
						continue;
					}
					this.MapView.SelectedTiles.Add(tile);
				}
				this.MapView.Selection = Rectangle.Empty;
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

		private void FilterBtn_Click(object sender, EventArgs e)
		{
			this.MapFilterPanel.Visible = !this.MapFilterPanel.Visible;
		}

		private void FullMapBtn_Click(object sender, EventArgs e)
		{
			this.MapView.Viewpoint = Rectangle.Empty;
		}

		private void GroupByCategory_Click(object sender, EventArgs e)
		{
			Session.Preferences.TileView = TileView.Category;
			this.populate_tiles();
		}

		private void GroupBySize_Click(object sender, EventArgs e)
		{
			Session.Preferences.TileView = TileView.Size;
			this.populate_tiles();
		}

		private void GroupByTileSet_Click(object sender, EventArgs e)
		{
			Session.Preferences.TileView = TileView.Library;
			this.populate_tiles();
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MapBuilderForm));
			this.Splitter = new SplitContainer();
			this.MapView = new Masterplan.Controls.MapView();
			this.Toolbar = new ToolStrip();
			this.RemoveBtn = new ToolStripSplitButton();
			this.clearMapToolStripMenuItem = new ToolStripMenuItem();
			this.RotateLeftBtn = new ToolStripSplitButton();
			this.rotateMapLeftToolStripMenuItem = new ToolStripMenuItem();
			this.RotateRightBtn = new ToolStripSplitButton();
			this.rotateMapRightToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.OrderingBtn = new ToolStripDropDownButton();
			this.OrderingFront = new ToolStripMenuItem();
			this.OrderingBack = new ToolStripMenuItem();
			this.at = new ToolStripDropDownButton();
			this.ToolsNavigate = new ToolStripMenuItem();
			this.ToolsReset = new ToolStripMenuItem();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.ToolsHighlightAreas = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.ToolsSelectBackground = new ToolStripMenuItem();
			this.ToolsClearBackground = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripSeparator();
			this.ToolsImportMap = new ToolStripMenuItem();
			this.toolStripSeparator8 = new ToolStripSeparator();
			this.ToolsAutoBuild = new ToolStripMenuItem();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.NameLbl = new ToolStripLabel();
			this.NameBox = new ToolStripTextBox();
			this.ZoomGauge = new TrackBar();
			this.Pages = new TabControl();
			this.TilesPage = new TabPage();
			this.TileList = new ListView();
			this.TileHdr = new ColumnHeader();
			this.MapFilterPanel = new Panel();
			this.SelectLibrariesBtn = new Button();
			this.SearchBox = new TextBox();
			this.SearchLbl = new Label();
			this.TileToolbar = new ToolStrip();
			this.TilesViewBtn = new ToolStripDropDownButton();
			this.ViewGroupBy = new ToolStripMenuItem();
			this.GroupByTileSet = new ToolStripMenuItem();
			this.GroupBySize = new ToolStripMenuItem();
			this.GroupByCategory = new ToolStripMenuItem();
			this.ViewSize = new ToolStripMenuItem();
			this.SizeSmall = new ToolStripMenuItem();
			this.SizeMedium = new ToolStripMenuItem();
			this.SizeLarge = new ToolStripMenuItem();
			this.FilterBtn = new ToolStripButton();
			this.AreasPage = new TabPage();
			this.AreaList = new ListView();
			this.AreaHdr = new ColumnHeader();
			this.AreaToolbar = new ToolStrip();
			this.AreaRemoveBtn = new ToolStripButton();
			this.AreaEditBtn = new ToolStripButton();
			this.toolStripSeparator7 = new ToolStripSeparator();
			this.FullMapBtn = new ToolStripButton();
			this.MapContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ContextSelect = new ToolStripMenuItem();
			this.ContextClear = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.ContextCreate = new ToolStripMenuItem();
			this.Tooltip = new ToolTip(this.components);
			this.TileContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TileContextRemove = new ToolStripMenuItem();
			this.TileContextSwap = new ToolStripMenuItem();
			this.TileContextDuplicate = new ToolStripMenuItem();
			this.MainPanel = new Panel();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.Toolbar.SuspendLayout();
			((ISupportInitialize)this.ZoomGauge).BeginInit();
			this.Pages.SuspendLayout();
			this.TilesPage.SuspendLayout();
			this.MapFilterPanel.SuspendLayout();
			this.TileToolbar.SuspendLayout();
			this.AreasPage.SuspendLayout();
			this.AreaToolbar.SuspendLayout();
			this.MapContextMenu.SuspendLayout();
			this.TileContextMenu.SuspendLayout();
			this.MainPanel.SuspendLayout();
			base.SuspendLayout();
			this.Splitter.Dock = DockStyle.Fill;
			this.Splitter.FixedPanel = FixedPanel.Panel2;
			this.Splitter.Location = new Point(0, 0);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.MapView);
			this.Splitter.Panel1.Controls.Add(this.Toolbar);
			this.Splitter.Panel1.Controls.Add(this.ZoomGauge);
			this.Splitter.Panel2.Controls.Add(this.Pages);
			this.Splitter.Size = new System.Drawing.Size(882, 401);
			this.Splitter.SplitterDistance = 675;
			this.Splitter.TabIndex = 0;
			this.MapView.AllowDrawing = false;
			this.MapView.AllowDrop = true;
			this.MapView.AllowLinkCreation = false;
			this.MapView.AllowScrolling = false;
			this.MapView.BackgroundMap = null;
			this.MapView.BorderSize = 3;
			this.MapView.Caption = "";
			this.MapView.Cursor = Cursors.Default;
			this.MapView.Dock = DockStyle.Fill;
			this.MapView.Encounter = null;
			this.MapView.FrameType = MapDisplayType.Dimmed;
			this.MapView.HighlightAreas = true;
			this.MapView.HoverToken = null;
			this.MapView.LineOfSight = false;
			this.MapView.Location = new Point(0, 25);
			this.MapView.Map = null;
			this.MapView.Mode = MapViewMode.Normal;
			this.MapView.Name = "MapView";
			this.MapView.Plot = null;
			this.MapView.ScalingFactor = 1;
			this.MapView.SelectedArea = null;
			this.MapView.SelectedTiles = null;
			this.MapView.Selection = new Rectangle(0, 0, 0, 0);
			this.MapView.ShowAuras = true;
			this.MapView.ShowConditions = true;
			this.MapView.ShowCreatureLabels = true;
			this.MapView.ShowCreatures = CreatureViewMode.All;
			this.MapView.ShowGrid = MapGridMode.Behind;
			this.MapView.ShowGridLabels = false;
			this.MapView.ShowHealthBars = false;
			this.MapView.ShowPictureTokens = true;
			this.MapView.Size = new System.Drawing.Size(675, 331);
			this.MapView.TabIndex = 0;
			this.MapView.Tactical = false;
			this.MapView.TokenLinks = null;
			this.MapView.Viewpoint = new Rectangle(0, 0, 0, 0);
			this.MapView.ItemDropped += new ItemDroppedEventHandler(this.MapView_ItemDropped);
			this.MapView.RegionSelected += new EventHandler(this.MapView_RegionSelected);
			this.MapView.HighlightedAreaChanged += new EventHandler(this.MapView_HoverAreaChanged);
			this.MapView.TileContext += new EventHandler(this.MapView_TileContext);
			this.MapView.MouseZoomed += new MouseEventHandler(this.MapView_MouseZoomed);
			this.MapView.ItemRemoved += new EventHandler(this.MapView_ItemRemoved);
			this.MapView.AreaActivated += new MapAreaEventHandler(this.MapView_AreaActivated);
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] removeBtn = new ToolStripItem[] { this.RemoveBtn, this.RotateLeftBtn, this.RotateRightBtn, this.toolStripSeparator1, this.OrderingBtn, this.at, this.toolStripSeparator3, this.NameLbl, this.NameBox };
			items.AddRange(removeBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(675, 25);
			this.Toolbar.TabIndex = 1;
			this.Toolbar.Text = "toolStrip1";
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.DropDownItems.AddRange(new ToolStripItem[] { this.clearMapToolStripMenuItem });
			this.RemoveBtn.Image = (Image)componentResourceManager.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new System.Drawing.Size(66, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.ButtonClick += new EventHandler(this.RemoveBtn_Click);
			this.clearMapToolStripMenuItem.Name = "clearMapToolStripMenuItem";
			this.clearMapToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.clearMapToolStripMenuItem.Text = "Clear All Tiles";
			this.clearMapToolStripMenuItem.Click += new EventHandler(this.ToolsClearAll_Click);
			this.RotateLeftBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RotateLeftBtn.DropDownItems.AddRange(new ToolStripItem[] { this.rotateMapLeftToolStripMenuItem });
			this.RotateLeftBtn.Image = (Image)componentResourceManager.GetObject("RotateLeftBtn.Image");
			this.RotateLeftBtn.ImageTransparentColor = Color.Magenta;
			this.RotateLeftBtn.Name = "RotateLeftBtn";
			this.RotateLeftBtn.Size = new System.Drawing.Size(80, 22);
			this.RotateLeftBtn.Text = "Rotate Left";
			this.RotateLeftBtn.ButtonClick += new EventHandler(this.RotateLeftBtn_Click);
			this.rotateMapLeftToolStripMenuItem.Name = "rotateMapLeftToolStripMenuItem";
			this.rotateMapLeftToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.rotateMapLeftToolStripMenuItem.Text = "Rotate Map Left";
			this.rotateMapLeftToolStripMenuItem.Click += new EventHandler(this.RotateMapLeft_Click);
			this.RotateRightBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RotateRightBtn.DropDownItems.AddRange(new ToolStripItem[] { this.rotateMapRightToolStripMenuItem });
			this.RotateRightBtn.Image = (Image)componentResourceManager.GetObject("RotateRightBtn.Image");
			this.RotateRightBtn.ImageTransparentColor = Color.Magenta;
			this.RotateRightBtn.Name = "RotateRightBtn";
			this.RotateRightBtn.Size = new System.Drawing.Size(88, 22);
			this.RotateRightBtn.Text = "Rotate Right";
			this.RotateRightBtn.ButtonClick += new EventHandler(this.RotateRightBtn_Click);
			this.rotateMapRightToolStripMenuItem.Name = "rotateMapRightToolStripMenuItem";
			this.rotateMapRightToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.rotateMapRightToolStripMenuItem.Text = "Rotate Map Right";
			this.rotateMapRightToolStripMenuItem.Click += new EventHandler(this.RotateMapRight_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.OrderingBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems = this.OrderingBtn.DropDownItems;
			ToolStripItem[] orderingFront = new ToolStripItem[] { this.OrderingFront, this.OrderingBack };
			dropDownItems.AddRange(orderingFront);
			this.OrderingBtn.Image = (Image)componentResourceManager.GetObject("OrderingBtn.Image");
			this.OrderingBtn.ImageTransparentColor = Color.Magenta;
			this.OrderingBtn.Name = "OrderingBtn";
			this.OrderingBtn.Size = new System.Drawing.Size(67, 22);
			this.OrderingBtn.Text = "Ordering";
			this.OrderingFront.Name = "OrderingFront";
			this.OrderingFront.Size = new System.Drawing.Size(147, 22);
			this.OrderingFront.Text = "Bring to Front";
			this.OrderingFront.Click += new EventHandler(this.OrderingFront_Click);
			this.OrderingBack.Name = "OrderingBack";
			this.OrderingBack.Size = new System.Drawing.Size(147, 22);
			this.OrderingBack.Text = "Send to Back";
			this.OrderingBack.Click += new EventHandler(this.OrderingBack_Click);
			this.at.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection toolStripItemCollections = this.at.DropDownItems;
			ToolStripItem[] toolsNavigate = new ToolStripItem[] { this.ToolsNavigate, this.ToolsReset, this.toolStripSeparator6, this.ToolsHighlightAreas, this.toolStripSeparator2, this.ToolsSelectBackground, this.ToolsClearBackground, this.toolStripMenuItem2, this.ToolsImportMap, this.toolStripSeparator8, this.ToolsAutoBuild };
			toolStripItemCollections.AddRange(toolsNavigate);
			this.at.Image = (Image)componentResourceManager.GetObject("at.Image");
			this.at.ImageTransparentColor = Color.Magenta;
			this.at.Name = "at";
			this.at.Size = new System.Drawing.Size(49, 22);
			this.at.Text = "Tools";
			this.ToolsNavigate.Name = "ToolsNavigate";
			this.ToolsNavigate.Size = new System.Drawing.Size(208, 22);
			this.ToolsNavigate.Text = "Scroll and Zoom";
			this.ToolsNavigate.Click += new EventHandler(this.ToolsNavigate_Click);
			this.ToolsReset.Name = "ToolsReset";
			this.ToolsReset.Size = new System.Drawing.Size(208, 22);
			this.ToolsReset.Text = "Reset View";
			this.ToolsReset.Click += new EventHandler(this.ToolsReset_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(205, 6);
			this.ToolsHighlightAreas.Name = "ToolsHighlightAreas";
			this.ToolsHighlightAreas.Size = new System.Drawing.Size(208, 22);
			this.ToolsHighlightAreas.Text = "Highlight Areas";
			this.ToolsHighlightAreas.Click += new EventHandler(this.ToolsHighlightAreas_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
			this.ToolsSelectBackground.Name = "ToolsSelectBackground";
			this.ToolsSelectBackground.Size = new System.Drawing.Size(208, 22);
			this.ToolsSelectBackground.Text = "Select Background Map...";
			this.ToolsSelectBackground.Click += new EventHandler(this.ToolsSelectBackground_Click);
			this.ToolsClearBackground.Name = "ToolsClearBackground";
			this.ToolsClearBackground.Size = new System.Drawing.Size(208, 22);
			this.ToolsClearBackground.Text = "Clear Background Map";
			this.ToolsClearBackground.Click += new EventHandler(this.ToolsClearBackground_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(205, 6);
			this.ToolsImportMap.Name = "ToolsImportMap";
			this.ToolsImportMap.Size = new System.Drawing.Size(208, 22);
			this.ToolsImportMap.Text = "Import Map Image...";
			this.ToolsImportMap.Click += new EventHandler(this.ToolsImportMap_Click);
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(205, 6);
			this.ToolsAutoBuild.Name = "ToolsAutoBuild";
			this.ToolsAutoBuild.Size = new System.Drawing.Size(208, 22);
			this.ToolsAutoBuild.Text = "AutoBuild...";
			this.ToolsAutoBuild.Click += new EventHandler(this.ToolsAutoBuild_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			this.NameLbl.Name = "NameLbl";
			this.NameLbl.Size = new System.Drawing.Size(69, 22);
			this.NameLbl.Text = "Map Name:";
			this.NameBox.BorderStyle = BorderStyle.FixedSingle;
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(200, 25);
			this.NameBox.TextChanged += new EventHandler(this.NameBox_TextChanged);
			this.ZoomGauge.Dock = DockStyle.Bottom;
			this.ZoomGauge.Location = new Point(0, 356);
			this.ZoomGauge.Maximum = 100;
			this.ZoomGauge.Name = "ZoomGauge";
			this.ZoomGauge.Size = new System.Drawing.Size(675, 45);
			this.ZoomGauge.TabIndex = 2;
			this.ZoomGauge.TickFrequency = 10;
			this.ZoomGauge.TickStyle = TickStyle.Both;
			this.ZoomGauge.Value = 50;
			this.ZoomGauge.Visible = false;
			this.ZoomGauge.Scroll += new EventHandler(this.ZoomGauge_Scroll);
			this.Pages.Controls.Add(this.TilesPage);
			this.Pages.Controls.Add(this.AreasPage);
			this.Pages.Dock = DockStyle.Fill;
			this.Pages.Location = new Point(0, 0);
			this.Pages.Name = "Pages";
			this.Pages.SelectedIndex = 0;
			this.Pages.Size = new System.Drawing.Size(203, 401);
			this.Pages.TabIndex = 3;
			this.TilesPage.Controls.Add(this.TileList);
			this.TilesPage.Controls.Add(this.MapFilterPanel);
			this.TilesPage.Controls.Add(this.TileToolbar);
			this.TilesPage.Location = new Point(4, 22);
			this.TilesPage.Name = "TilesPage";
			this.TilesPage.Padding = new System.Windows.Forms.Padding(3);
			this.TilesPage.Size = new System.Drawing.Size(195, 375);
			this.TilesPage.TabIndex = 0;
			this.TilesPage.Text = "Tiles";
			this.TilesPage.UseVisualStyleBackColor = true;
			this.TileList.Columns.AddRange(new ColumnHeader[] { this.TileHdr });
			this.TileList.Dock = DockStyle.Fill;
			this.TileList.FullRowSelect = true;
			this.TileList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.TileList.HideSelection = false;
			this.TileList.Location = new Point(3, 87);
			this.TileList.MultiSelect = false;
			this.TileList.Name = "TileList";
			this.TileList.Size = new System.Drawing.Size(189, 285);
			this.TileList.TabIndex = 1;
			this.TileList.UseCompatibleStateImageBehavior = false;
			this.TileList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.TileList.ItemDrag += new ItemDragEventHandler(this.TileList_ItemDrag);
			this.TileHdr.Text = "Tile";
			this.TileHdr.Width = 120;
			this.MapFilterPanel.Controls.Add(this.SelectLibrariesBtn);
			this.MapFilterPanel.Controls.Add(this.SearchBox);
			this.MapFilterPanel.Controls.Add(this.SearchLbl);
			this.MapFilterPanel.Dock = DockStyle.Top;
			this.MapFilterPanel.Location = new Point(3, 28);
			this.MapFilterPanel.Name = "MapFilterPanel";
			this.MapFilterPanel.Size = new System.Drawing.Size(189, 59);
			this.MapFilterPanel.TabIndex = 3;
			this.SelectLibrariesBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.SelectLibrariesBtn.Location = new Point(53, 29);
			this.SelectLibrariesBtn.Name = "SelectLibrariesBtn";
			this.SelectLibrariesBtn.Size = new System.Drawing.Size(133, 23);
			this.SelectLibrariesBtn.TabIndex = 2;
			this.SelectLibrariesBtn.Text = "Select Libraries";
			this.SelectLibrariesBtn.UseVisualStyleBackColor = true;
			this.SelectLibrariesBtn.Click += new EventHandler(this.ViewSelectLibraries_Click);
			this.SearchBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.SearchBox.Location = new Point(53, 3);
			this.SearchBox.Name = "SearchBox";
			this.SearchBox.Size = new System.Drawing.Size(133, 20);
			this.SearchBox.TabIndex = 1;
			this.SearchBox.TextChanged += new EventHandler(this.SearchBox_TextChanged);
			this.SearchLbl.AutoSize = true;
			this.SearchLbl.Location = new Point(3, 6);
			this.SearchLbl.Name = "SearchLbl";
			this.SearchLbl.Size = new System.Drawing.Size(44, 13);
			this.SearchLbl.TabIndex = 0;
			this.SearchLbl.Text = "Search:";
			ToolStripItemCollection items1 = this.TileToolbar.Items;
			ToolStripItem[] tilesViewBtn = new ToolStripItem[] { this.TilesViewBtn, this.FilterBtn };
			items1.AddRange(tilesViewBtn);
			this.TileToolbar.Location = new Point(3, 3);
			this.TileToolbar.Name = "TileToolbar";
			this.TileToolbar.Size = new System.Drawing.Size(189, 25);
			this.TileToolbar.TabIndex = 2;
			this.TileToolbar.Text = "toolStrip1";
			this.TilesViewBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems1 = this.TilesViewBtn.DropDownItems;
			ToolStripItem[] viewGroupBy = new ToolStripItem[] { this.ViewGroupBy, this.ViewSize };
			dropDownItems1.AddRange(viewGroupBy);
			this.TilesViewBtn.Image = (Image)componentResourceManager.GetObject("TilesViewBtn.Image");
			this.TilesViewBtn.ImageTransparentColor = Color.Magenta;
			this.TilesViewBtn.Name = "TilesViewBtn";
			this.TilesViewBtn.Size = new System.Drawing.Size(45, 22);
			this.TilesViewBtn.Text = "View";
			ToolStripItemCollection toolStripItemCollections1 = this.ViewGroupBy.DropDownItems;
			ToolStripItem[] groupByTileSet = new ToolStripItem[] { this.GroupByTileSet, this.GroupBySize, this.GroupByCategory };
			toolStripItemCollections1.AddRange(groupByTileSet);
			this.ViewGroupBy.Name = "ViewGroupBy";
			this.ViewGroupBy.Size = new System.Drawing.Size(152, 22);
			this.ViewGroupBy.Text = "Group By";
			this.GroupByTileSet.Name = "GroupByTileSet";
			this.GroupByTileSet.Size = new System.Drawing.Size(152, 22);
			this.GroupByTileSet.Text = "Library";
			this.GroupByTileSet.Click += new EventHandler(this.GroupByTileSet_Click);
			this.GroupBySize.Name = "GroupBySize";
			this.GroupBySize.Size = new System.Drawing.Size(152, 22);
			this.GroupBySize.Text = "Tile Size";
			this.GroupBySize.Click += new EventHandler(this.GroupBySize_Click);
			this.GroupByCategory.Name = "GroupByCategory";
			this.GroupByCategory.Size = new System.Drawing.Size(152, 22);
			this.GroupByCategory.Text = "Tile Category";
			this.GroupByCategory.Click += new EventHandler(this.GroupByCategory_Click);
			ToolStripItemCollection dropDownItems2 = this.ViewSize.DropDownItems;
			ToolStripItem[] sizeSmall = new ToolStripItem[] { this.SizeSmall, this.SizeMedium, this.SizeLarge };
			dropDownItems2.AddRange(sizeSmall);
			this.ViewSize.Name = "ViewSize";
			this.ViewSize.Size = new System.Drawing.Size(152, 22);
			this.ViewSize.Text = "Size";
			this.SizeSmall.Name = "SizeSmall";
			this.SizeSmall.Size = new System.Drawing.Size(119, 22);
			this.SizeSmall.Text = "Small";
			this.SizeSmall.Click += new EventHandler(this.SizeSmall_Click);
			this.SizeMedium.Name = "SizeMedium";
			this.SizeMedium.Size = new System.Drawing.Size(119, 22);
			this.SizeMedium.Text = "Medium";
			this.SizeMedium.Click += new EventHandler(this.SizeMedium_Click);
			this.SizeLarge.Name = "SizeLarge";
			this.SizeLarge.Size = new System.Drawing.Size(119, 22);
			this.SizeLarge.Text = "Large";
			this.SizeLarge.Click += new EventHandler(this.SizeLarge_Click);
			this.FilterBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FilterBtn.Image = (Image)componentResourceManager.GetObject("FilterBtn.Image");
			this.FilterBtn.ImageTransparentColor = Color.Magenta;
			this.FilterBtn.Name = "FilterBtn";
			this.FilterBtn.Size = new System.Drawing.Size(83, 22);
			this.FilterBtn.Text = "Filter This List";
			this.FilterBtn.Click += new EventHandler(this.FilterBtn_Click);
			this.AreasPage.Controls.Add(this.AreaList);
			this.AreasPage.Controls.Add(this.AreaToolbar);
			this.AreasPage.Location = new Point(4, 22);
			this.AreasPage.Name = "AreasPage";
			this.AreasPage.Padding = new System.Windows.Forms.Padding(3);
			this.AreasPage.Size = new System.Drawing.Size(195, 375);
			this.AreasPage.TabIndex = 1;
			this.AreasPage.Text = "Map Areas";
			this.AreasPage.UseVisualStyleBackColor = true;
			this.AreaList.Columns.AddRange(new ColumnHeader[] { this.AreaHdr });
			this.AreaList.Dock = DockStyle.Fill;
			this.AreaList.FullRowSelect = true;
			this.AreaList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.AreaList.HideSelection = false;
			this.AreaList.Location = new Point(3, 28);
			this.AreaList.MultiSelect = false;
			this.AreaList.Name = "AreaList";
			this.AreaList.Size = new System.Drawing.Size(189, 344);
			this.AreaList.TabIndex = 1;
			this.AreaList.UseCompatibleStateImageBehavior = false;
			this.AreaList.View = View.Details;
			this.AreaList.SelectedIndexChanged += new EventHandler(this.AreaList_SelectedIndexChanged);
			this.AreaList.DoubleClick += new EventHandler(this.AreaEditBtn_Click);
			this.AreaHdr.Text = "Area Name";
			this.AreaHdr.Width = 150;
			ToolStripItemCollection items2 = this.AreaToolbar.Items;
			ToolStripItem[] areaRemoveBtn = new ToolStripItem[] { this.AreaRemoveBtn, this.AreaEditBtn, this.toolStripSeparator7, this.FullMapBtn };
			items2.AddRange(areaRemoveBtn);
			this.AreaToolbar.Location = new Point(3, 3);
			this.AreaToolbar.Name = "AreaToolbar";
			this.AreaToolbar.Size = new System.Drawing.Size(189, 25);
			this.AreaToolbar.TabIndex = 0;
			this.AreaToolbar.Text = "toolStrip1";
			this.AreaRemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AreaRemoveBtn.Image = (Image)componentResourceManager.GetObject("AreaRemoveBtn.Image");
			this.AreaRemoveBtn.ImageTransparentColor = Color.Magenta;
			this.AreaRemoveBtn.Name = "AreaRemoveBtn";
			this.AreaRemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.AreaRemoveBtn.Text = "Remove";
			this.AreaRemoveBtn.Click += new EventHandler(this.AreaRemoveBtn_Click);
			this.AreaEditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.AreaEditBtn.Image = (Image)componentResourceManager.GetObject("AreaEditBtn.Image");
			this.AreaEditBtn.ImageTransparentColor = Color.Magenta;
			this.AreaEditBtn.Name = "AreaEditBtn";
			this.AreaEditBtn.Size = new System.Drawing.Size(31, 22);
			this.AreaEditBtn.Text = "Edit";
			this.AreaEditBtn.Click += new EventHandler(this.AreaEditBtn_Click);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
			this.FullMapBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.FullMapBtn.Image = (Image)componentResourceManager.GetObject("FullMapBtn.Image");
			this.FullMapBtn.ImageTransparentColor = Color.Magenta;
			this.FullMapBtn.Name = "FullMapBtn";
			this.FullMapBtn.Size = new System.Drawing.Size(57, 22);
			this.FullMapBtn.Text = "Full Map";
			this.FullMapBtn.Click += new EventHandler(this.FullMapBtn_Click);
			ToolStripItemCollection toolStripItemCollections2 = this.MapContextMenu.Items;
			ToolStripItem[] contextSelect = new ToolStripItem[] { this.ContextSelect, this.ContextClear, this.toolStripMenuItem1, this.ContextCreate };
			toolStripItemCollections2.AddRange(contextSelect);
			this.MapContextMenu.Name = "MapContextMenu";
			this.MapContextMenu.Size = new System.Drawing.Size(172, 76);
			this.ContextSelect.Name = "ContextSelect";
			this.ContextSelect.Size = new System.Drawing.Size(171, 22);
			this.ContextSelect.Text = "Select Tiles";
			this.ContextSelect.Click += new EventHandler(this.ContextSelect_Click);
			this.ContextClear.Name = "ContextClear";
			this.ContextClear.Size = new System.Drawing.Size(171, 22);
			this.ContextClear.Text = "Clear Tiles";
			this.ContextClear.Click += new EventHandler(this.ContextClear_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 6);
			this.ContextCreate.Name = "ContextCreate";
			this.ContextCreate.Size = new System.Drawing.Size(171, 22);
			this.ContextCreate.Text = "Create Map Area...";
			this.ContextCreate.Click += new EventHandler(this.ContextCreate_Click);
			ToolStripItemCollection items3 = this.TileContextMenu.Items;
			ToolStripItem[] tileContextRemove = new ToolStripItem[] { this.TileContextRemove, this.TileContextSwap, this.TileContextDuplicate };
			items3.AddRange(tileContextRemove);
			this.TileContextMenu.Name = "TileContextMenu";
			this.TileContextMenu.Size = new System.Drawing.Size(147, 70);
			this.TileContextRemove.Name = "TileContextRemove";
			this.TileContextRemove.Size = new System.Drawing.Size(146, 22);
			this.TileContextRemove.Text = "Remove Tile";
			this.TileContextRemove.Click += new EventHandler(this.RemoveBtn_Click);
			this.TileContextSwap.Name = "TileContextSwap";
			this.TileContextSwap.Size = new System.Drawing.Size(146, 22);
			this.TileContextSwap.Text = "Swap Tile";
			this.TileContextSwap.Click += new EventHandler(this.TileContextSwap_Click);
			this.TileContextDuplicate.Name = "TileContextDuplicate";
			this.TileContextDuplicate.Size = new System.Drawing.Size(146, 22);
			this.TileContextDuplicate.Text = "Duplicate Tile";
			this.TileContextDuplicate.Click += new EventHandler(this.TileContextDuplicate_Click);
			this.MainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.MainPanel.Controls.Add(this.Splitter);
			this.MainPanel.Location = new Point(12, 12);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(882, 401);
			this.MainPanel.TabIndex = 3;
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(738, 419);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 4;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(819, 419);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 5;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(906, 454);
			base.Controls.Add(this.MainPanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MinimizeBox = false;
			base.Name = "MapForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Map Editor";
			base.FormClosed += new FormClosedEventHandler(this.MapForm_FormClosed);
			base.FormClosing += new FormClosingEventHandler(this.MapForm_FormClosing);
			base.KeyDown += new KeyEventHandler(this.MapForm_KeyDown);
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel1.PerformLayout();
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			((ISupportInitialize)this.ZoomGauge).EndInit();
			this.Pages.ResumeLayout(false);
			this.TilesPage.ResumeLayout(false);
			this.TilesPage.PerformLayout();
			this.MapFilterPanel.ResumeLayout(false);
			this.MapFilterPanel.PerformLayout();
			this.TileToolbar.ResumeLayout(false);
			this.TileToolbar.PerformLayout();
			this.AreasPage.ResumeLayout(false);
			this.AreasPage.PerformLayout();
			this.AreaToolbar.ResumeLayout(false);
			this.AreaToolbar.PerformLayout();
			this.MapContextMenu.ResumeLayout(false);
			this.TileContextMenu.ResumeLayout(false);
			this.MainPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		protected override bool IsInputKey(Keys key)
		{
			if (base.IsInputKey(key))
			{
				return true;
			}
			return this.MapView.HandleKey(key);
		}

		private void MapForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Session.Preferences.TileLibraries = new List<Guid>();
			foreach (Guid key in this.fTileSets.Keys)
			{
				if (!this.fTileSets[key])
				{
					continue;
				}
				Session.Preferences.TileLibraries.Add(key);
			}
		}

		private void MapForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == System.Windows.Forms.DialogResult.OK)
			{
				return;
			}
			if (this.fMap.Tiles.Count == 0)
			{
				return;
			}
			if (Session.Project.FindTacticalMap(this.fMap.ID) != null)
			{
				return;
			}
			if (MessageBox.Show("Do you want to save this new map?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
		}

		private void MapForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				this.RemoveBtn_Click(sender, e);
			}
			Keys keyCode = e.KeyCode;
			switch (keyCode)
			{
				case Keys.Left:
				case Keys.Up:
				case Keys.Right:
				case Keys.Down:
				{
					this.MapView.Nudge(e);
					return;
				}
				default:
				{
					if (keyCode == Keys.Delete)
					{
						break;
					}
					else
					{
						return;
					}
				}
			}
			this.RemoveBtn_Click(sender, e);
		}

		private void MapView_AreaActivated(object sender, MapAreaEventArgs e)
		{
			int area = this.fMap.Areas.IndexOf(e.MapArea);
			MapAreaForm mapAreaForm = new MapAreaForm(e.MapArea, this.fMap);
			if (mapAreaForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.fMap.Areas[area] = mapAreaForm.Area;
				this.update_areas();
			}
		}

		private void MapView_HoverAreaChanged(object sender, EventArgs e)
		{
			string name = "";
			string str = "";
			if (this.MapView.HighlightedArea != null)
			{
				name = this.MapView.HighlightedArea.Name;
				str = TextHelper.Wrap(this.MapView.HighlightedArea.Details);
				if (str != "")
				{
					str = string.Concat(str, Environment.NewLine);
				}
				object obj = str;
				object[] width = new object[] { obj, null, null, null, null };
				Rectangle region = this.MapView.HighlightedArea.Region;
				width[1] = region.Width;
				width[2] = " sq x ";
				Rectangle rectangle = this.MapView.HighlightedArea.Region;
				width[3] = rectangle.Height;
				width[4] = " sq";
				str = string.Concat(width);
			}
			this.Tooltip.ToolTipTitle = name;
			this.Tooltip.ToolTipIcon = ToolTipIcon.Info;
			this.Tooltip.SetToolTip(this.MapView, str);
		}

		private void MapView_ItemDropped(CombatData data, Point location)
		{
            data.Location = location;
		}

		private void MapView_ItemRemoved(object sender, EventArgs e)
		{
		}

		private void MapView_MouseZoomed(object sender, MouseEventArgs e)
		{
			this.ZoomGauge.Visible = true;
			TrackBar zoomGauge = this.ZoomGauge;
			zoomGauge.Value = zoomGauge.Value - Math.Sign(e.Delta);
			this.ZoomGauge_Scroll(sender, e);
		}

		private void MapView_RegionSelected(object sender, EventArgs e)
		{
			Point client = this.MapView.PointToClient(System.Windows.Forms.Cursor.Position);
			this.MapContextMenu.Show(this.MapView, client);
		}

		private void MapView_TileContext(object sender, EventArgs e)
		{
			Point client = this.MapView.PointToClient(System.Windows.Forms.Cursor.Position);
			this.TileContextMenu.Show(this.MapView, client);
		}

		private bool match(Tile t, string query)
		{
			string[] strArrays = query.ToLower().Split(new char[0]);
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				if (!this.match_token(t, strArrays[i]))
				{
					return false;
				}
			}
			return true;
		}

		private bool match_token(Tile t, string token)
		{
			return t.Keywords.ToLower().Contains(token);
		}

		private void NameBox_TextChanged(object sender, EventArgs e)
		{
			this.fMap.Name = this.NameBox.Text;
		}

		private void OnAutoBuild(object sender, EventArgs e)
		{
			this.MapView.MapChanged();
			Application.DoEvents();
		}

		private void OrderingBack_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTiles.Count == 1)
			{
				TileData item = this.MapView.SelectedTiles[0];
				this.fMap.Tiles.Remove(item);
				this.fMap.Tiles.Insert(0, item);
				this.MapView.MapChanged();
			}
		}

		private void OrderingFront_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTiles.Count == 1)
			{
				TileData item = this.MapView.SelectedTiles[0];
				this.fMap.Tiles.Remove(item);
				this.fMap.Tiles.Add(item);
				this.MapView.MapChanged();
			}
		}

		private void populate_tiles()
		{
			List<Library> libraries = new List<Library>();
			libraries.AddRange(Session.Libraries);
			libraries.Add(Session.Project.Library);
			List<string> strs = new List<string>();
			switch (Session.Preferences.TileView)
			{
				case TileView.Library:
				{
					foreach (Library library in libraries)
					{
						if (!this.fTileSets[library.ID] || strs.Contains(library.Name))
						{
							continue;
						}
						strs.Add(library.Name);
					}
					strs.Sort();
					break;
				}
				case TileView.Size:
				{
					List<int> nums = new List<int>();
					foreach (Library library1 in libraries)
					{
						foreach (Tile tile in library1.Tiles)
						{
							if (nums.Contains(tile.Area))
							{
								continue;
							}
							nums.Add(tile.Area);
						}
					}
					nums.Sort();
					List<int>.Enumerator enumerator = nums.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							int current = enumerator.Current;
							strs.Add(string.Concat("Size: ", current));
						}
						break;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				case TileView.Category:
				{
					IEnumerator enumerator1 = Enum.GetValues(typeof(TileCategory)).GetEnumerator();
#if MANDER
                        try
					{
						while (enumerator1.MoveNext())
						{
							strs.Add((TileCategory)enumerator1.Current.ToString());
						}
						break;
					}
					finally
					{
						IDisposable disposable = enumerator1 as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
#endif
					break;
				}
			}
			int num = 32;
			switch (Session.Preferences.TileSize)
			{
				case TileSize.Small:
				{
					num = 16;
					break;
				}
				case TileSize.Medium:
				{
					num = 32;
					break;
				}
				case TileSize.Large:
				{
					num = 64;
					break;
				}
			}
			this.TileList.BeginUpdate();
			this.TileList.Groups.Clear();
			foreach (string str in strs)
			{
				this.TileList.Groups.Add(str, str);
			}
			this.TileList.ShowGroups = this.TileList.Groups.Count != 0;
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			List<Image> images = new List<Image>();
			foreach (Library library2 in libraries)
			{
				if (!this.fTileSets[library2.ID])
				{
					continue;
				}
				foreach (Tile tile1 in library2.Tiles)
				{
					if (!this.match(tile1, this.SearchBox.Text))
					{
						continue;
					}
					ListViewItem listViewItem = new ListViewItem(tile1.ToString())
					{
						Tag = tile1
					};
					switch (Session.Preferences.TileView)
					{
						case TileView.Library:
						{
							listViewItem.Group = this.TileList.Groups[library2.Name];
							break;
						}
						case TileView.Size:
						{
							listViewItem.Group = this.TileList.Groups[string.Concat("Size: ", tile1.Area)];
							break;
						}
						case TileView.Category:
						{
							listViewItem.Group = this.TileList.Groups[tile1.Category.ToString()];
							break;
						}
					}
					Image image = (tile1.Image != null ? tile1.Image : tile1.BlankImage);
					if (image == null)
					{
						continue;
					}
					try
					{
						Bitmap bitmap = new Bitmap(num, num);
						if (tile1.Size.Width <= tile1.Size.Height)
						{
							System.Drawing.Size size = tile1.Size;
							System.Drawing.Size size1 = tile1.Size;
							int width = size.Width * num / size1.Height;
							Rectangle rectangle = new Rectangle((num - width) / 2, 0, width, num);
							Graphics.FromImage(bitmap).DrawImage(image, rectangle);
						}
						else
						{
							System.Drawing.Size size2 = tile1.Size;
							System.Drawing.Size size3 = tile1.Size;
							int height = size2.Height * num / size3.Width;
							Rectangle rectangle1 = new Rectangle(0, (num - height) / 2, num, height);
							Graphics.FromImage(bitmap).DrawImage(image, rectangle1);
						}
						images.Add(bitmap);
						listViewItem.ImageIndex = images.Count - 1;
						listViewItems.Add(listViewItem);
					}
					catch (Exception exception)
					{
						LogSystem.Trace(exception);
					}
				}
			}
			this.TileList.LargeImageList = new ImageList()
			{
				ColorDepth = ColorDepth.Depth32Bit,
				ImageSize = new System.Drawing.Size(num, num)
			};
			this.TileList.LargeImageList.Images.AddRange(images.ToArray());
			this.TileList.Items.Clear();
			this.TileList.Items.AddRange(listViewItems.ToArray());
			if (this.TileList.Items.Count == 0)
			{
				ListViewItem grayText = this.TileList.Items.Add("(no tiles)");
				grayText.ForeColor = SystemColors.GrayText;
			}
			this.TileList.EndUpdate();
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTiles.Count != 0)
			{
				foreach (TileData selectedTile in this.MapView.SelectedTiles)
				{
					this.fMap.Tiles.Remove(selectedTile);
				}
				this.MapView.SelectedTiles.Clear();
				this.MapView.MapChanged();
			}
		}

		private void RotateLeftBtn_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTiles.Count != 0)
			{
				foreach (TileData selectedTile in this.MapView.SelectedTiles)
				{
					TileData rotations = selectedTile;
					rotations.Rotations = rotations.Rotations - 1;
				}
				this.MapView.MapChanged();
			}
		}

		private void RotateMapLeft_Click(object sender, EventArgs e)
		{
			foreach (TileData tile in this.fMap.Tiles)
			{
				if (!this.MapView.LayoutData.Tiles.ContainsKey(tile))
				{
					continue;
				}
				Point location = tile.Location;
				int x = location.X - this.MapView.LayoutData.MinX;
				Point point = tile.Location;
				int y = point.Y - this.MapView.LayoutData.MinY;
				Tile item = this.MapView.LayoutData.Tiles[tile];
				int num = (tile.Rotations % 2 == 0 ? item.Size.Width : item.Size.Height);
				tile.Location = new Point(y, this.MapView.LayoutData.Width - x - num + 1);
				TileData rotations = tile;
				rotations.Rotations = rotations.Rotations - 1;
			}
			foreach (MapArea area in this.fMap.Areas)
			{
				Rectangle region = area.Region;
				int x1 = region.X - this.MapView.LayoutData.MinX;
				Rectangle rectangle = area.Region;
				int y1 = rectangle.Y - this.MapView.LayoutData.MinY;
				Rectangle region1 = area.Region;
				Point point1 = new Point(y1, this.MapView.LayoutData.Width - x1 - region1.Width + 1);
				System.Drawing.Size size = new System.Drawing.Size(area.Region.Height, area.Region.Width);
				area.Region = new Rectangle(point1, size);
			}
			if (this.SelectedArea != null)
			{
				this.MapView.Viewpoint = this.SelectedArea.Region;
			}
			this.MapView.MapChanged();
		}

		private void RotateMapRight_Click(object sender, EventArgs e)
		{
			foreach (TileData tile in this.fMap.Tiles)
			{
				if (!this.MapView.LayoutData.Tiles.ContainsKey(tile))
				{
					continue;
				}
				Point location = tile.Location;
				int x = location.X - this.MapView.LayoutData.MinX;
				Point point = tile.Location;
				int y = point.Y - this.MapView.LayoutData.MinY;
				Tile item = this.MapView.LayoutData.Tiles[tile];
				int num = (tile.Rotations % 2 == 0 ? item.Size.Height : item.Size.Width);
				tile.Location = new Point(this.MapView.LayoutData.Height - y - num + 1, x);
				TileData rotations = tile;
				rotations.Rotations = rotations.Rotations + 1;
			}
			foreach (MapArea area in this.fMap.Areas)
			{
				Rectangle region = area.Region;
				int x1 = region.X - this.MapView.LayoutData.MinX;
				Rectangle rectangle = area.Region;
				int y1 = rectangle.Y - this.MapView.LayoutData.MinY;
				Rectangle region1 = area.Region;
				Point point1 = new Point(this.MapView.LayoutData.Height - y1 - region1.Height + 1, x1);
				System.Drawing.Size size = new System.Drawing.Size(area.Region.Height, area.Region.Width);
				area.Region = new Rectangle(point1, size);
			}
			if (this.SelectedArea != null)
			{
				this.MapView.Viewpoint = this.SelectedArea.Region;
			}
			this.MapView.MapChanged();
		}

		private void RotateRightBtn_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTiles.Count != 0)
			{
				foreach (TileData selectedTile in this.MapView.SelectedTiles)
				{
					TileData rotations = selectedTile;
					rotations.Rotations = rotations.Rotations + 1;
				}
				this.MapView.MapChanged();
			}
		}

		private void SearchBox_TextChanged(object sender, EventArgs e)
		{
			this.populate_tiles();
		}

		private void SizeLarge_Click(object sender, EventArgs e)
		{
			Session.Preferences.TileSize = TileSize.Large;
			this.populate_tiles();
		}

		private void SizeMedium_Click(object sender, EventArgs e)
		{
			Session.Preferences.TileSize = TileSize.Medium;
			this.populate_tiles();
		}

		private void SizeSmall_Click(object sender, EventArgs e)
		{
			Session.Preferences.TileSize = TileSize.Small;
			this.populate_tiles();
		}

		private void TileContextDuplicate_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTiles.Count != 1)
			{
				return;
			}
			TileData item = this.MapView.SelectedTiles[0];
			item = item.Copy();
			item.ID = Guid.NewGuid();
			Point location = item.Location;
			Point point = item.Location;
			item.Location = new Point(location.X + 1, point.Y + 1);
			this.fMap.Tiles.Add(item);
			this.MapView.MapChanged();
		}

		private void TileContextSwap_Click(object sender, EventArgs e)
		{
			if (this.MapView.SelectedTiles.Count != 1)
			{
				return;
			}
			TileData item = this.MapView.SelectedTiles[0];
			Tile tile = Session.FindTile(item.TileID, SearchType.Global);
			TileSelectForm tileSelectForm = new TileSelectForm(tile.Size, tile.Category);
			if (tileSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				int num = (item.Rotations % 2 == 0 ? tile.Size.Width : tile.Size.Height);
				int num1 = (item.Rotations % 2 == 0 ? tile.Size.Height : tile.Size.Width);
				int num2 = 0;
				if (tileSelectForm.Tile.Size.Width != num || tileSelectForm.Tile.Size.Height != num1)
				{
					num2 = 1;
				}
				item.TileID = tileSelectForm.Tile.ID;
				item.Rotations = num2;
				this.MapView.MapChanged();
			}
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedTile != null)
			{
				Library tile = Session.FindLibrary(this.SelectedTile);
				int num = tile.Tiles.IndexOf(this.SelectedTile);
				TileForm tileForm = new TileForm(this.SelectedTile);
				if (tileForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					tile.Tiles[num] = tileForm.Tile;
					this.populate_tiles();
				}
			}
		}

		private void TileList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (this.SelectedTile != null)
			{
				base.DoDragDrop(this.SelectedTile, DragDropEffects.All);
			}
		}

		private void TileSet_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			Library tag = item.Tag as Library;
			this.fTileSets[tag.ID] = !this.fTileSets[tag.ID];
			item.Checked = this.fTileSets[tag.ID];
			this.populate_tiles();
		}

		private void ToolsAutoBuild_Click(object sender, EventArgs e)
		{
			MapWizard mapWizard = new MapWizard(sender == null);
			if (sender == null)
			{
				MapBuilderData data = mapWizard.Data as MapBuilderData;
				data.MaxAreaCount = 3;
				data.MinAreaCount = 3;
			}
			if (mapWizard.Show() == System.Windows.Forms.DialogResult.OK)
			{
				MapBuilderData mapBuilderDatum = mapWizard.Data as MapBuilderData;
				this.MapView.Viewpoint = Rectangle.Empty;
				int num = 0;
				do
				{
					if (num == 20)
					{
						break;
					}
					num++;
					MapBuilder.BuildMap(mapBuilderDatum, this.fMap, new EventHandler(this.OnAutoBuild));
				}
				while (mapBuilderDatum.Type != MapAutoBuildType.FilledArea && mapBuilderDatum.Type != MapAutoBuildType.Freeform && this.fMap.Areas.Count < mapBuilderDatum.MinAreaCount);
				if (mapBuilderDatum.Type == MapAutoBuildType.Warren && this.MapView.LayoutData.Height > this.MapView.LayoutData.Width)
				{
					this.RotateMapLeft_Click(null, null);
				}
				this.MapView.MapChanged();
				this.update_areas();
			}
		}

		private void ToolsClearAll_Click(object sender, EventArgs e)
		{
			this.fMap.Tiles.Clear();
			this.MapView.MapChanged();
		}

		private void ToolsClearBackground_Click(object sender, EventArgs e)
		{
			this.MapView.BackgroundMap = null;
		}

		private void ToolsHighlightAreas_Click(object sender, EventArgs e)
		{
			this.MapView.HighlightAreas = !this.MapView.HighlightAreas;
		}

		private void ToolsImportMap_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = Program.ImageFilter
			};
			if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
			{
				return;
			}
			Image image = Image.FromFile(openFileDialog.FileName);
			if (image == null)
			{
				return;
			}
			Tile tile = new Tile()
			{
				Image = image
			};
			TileForm tileForm = new TileForm(tile);
			if (tileForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
			{
				return;
			}
			Session.Project.Library.Tiles.Add(tileForm.Tile);
			TileData tileDatum = new TileData()
			{
				TileID = tile.ID
			};
			this.fMap.Tiles.Add(tileDatum);
			this.MapView.MapChanged();
		}

		private void ToolsNavigate_Click(object sender, EventArgs e)
		{
			this.MapView.AllowScrolling = !this.MapView.AllowScrolling;
			this.ZoomGauge.Visible = this.MapView.AllowScrolling;
		}

		private void ToolsReset_Click(object sender, EventArgs e)
		{
			this.ZoomGauge.Value = 50;
			this.MapView.ScalingFactor = 1;
			this.MapView.Viewpoint = Rectangle.Empty;
			this.MapView.MapChanged();
		}

		private void ToolsSelectBackground_Click(object sender, EventArgs e)
		{
			List<Guid> guids = new List<Guid>()
			{
				this.fMap.ID
			};
			MapSelectForm mapSelectForm = new MapSelectForm(Session.Project.Maps, guids, false);
			if (mapSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.MapView.BackgroundMap = mapSelectForm.Map;
			}
		}

		private void update_areas()
		{
			this.AreaList.Items.Clear();
			foreach (MapArea area in this.fMap.Areas)
			{
				ListViewItem listViewItem = this.AreaList.Items.Add(area.Name);
				listViewItem.Tag = area;
			}
			if (this.AreaList.Items.Count == 0)
			{
				ListViewItem grayText = this.AreaList.Items.Add("(no areas defined)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}

		private void ViewSelectLibraries_Click(object sender, EventArgs e)
		{
			List<Library> libraries = new List<Library>();
			libraries.AddRange(Session.Libraries);
			libraries.Add(Session.Project.Library);
			List<Library> libraries1 = new List<Library>();
			foreach (Library library in libraries)
			{
				if (!this.fTileSets[library.ID])
				{
					continue;
				}
				libraries1.Add(library);
			}
			TileLibrarySelectForm tileLibrarySelectForm = new TileLibrarySelectForm(libraries1);
			if (tileLibrarySelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				foreach (Library library1 in libraries)
				{
					this.fTileSets[library1.ID] = tileLibrarySelectForm.Libraries.Contains(library1);
				}
				this.populate_tiles();
			}
		}

		private void ZoomGauge_Scroll(object sender, EventArgs e)
		{
			double num = 10;
			double num1 = 1;
			double num2 = 0.1;
			double value = (double)(this.ZoomGauge.Value - this.ZoomGauge.Minimum) / (double)(this.ZoomGauge.Maximum - this.ZoomGauge.Minimum);
			if (value < 0.5)
			{
				value *= 2;
				this.MapView.ScalingFactor = num2 + value * (num1 - num2);
			}
			else
			{
				value -= 0.5;
				value *= 2;
				this.MapView.ScalingFactor = num1 + value * (num - num1);
			}
			this.MapView.MapChanged();
		}
	}
}