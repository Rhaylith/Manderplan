using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using Masterplan.Tools;
using Masterplan.Tools.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Resources;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class MapListForm : Form
	{
		private static string FullMap;

		private IContainer components;

		private ListView MapList;

		private ColumnHeader MapHdr;

		private ToolStrip ListToolbar;

		private ToolStripButton RemoveBtn;

		private ToolStripButton EditBtn;

		private SplitContainer Splitter;

		private Masterplan.Controls.MapView MapView;

		private ToolStrip MapToolbar;

		private ToolStripComboBox AreaBox;

		private ToolStripLabel AreaLbl;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripDropDownButton ToolsBtn;

		private ToolStripMenuItem ToolsBreakdown;

		private ToolStripMenuItem ToolsScreenshot;

		private System.Windows.Forms.ContextMenuStrip ListContext;

		private ToolStripMenuItem ListContextAdd;

		private ToolStripMenuItem ListContextRemove;

		private ToolStripMenuItem ListContextEdit;

		private ToolStripSeparator toolStripMenuItem1;

		private ToolStripMenuItem ListContextCategory;

		private ToolStripSeparator toolStripMenuItem2;

		private ToolStripMenuItem ListContextDelve;

		private ToolStripMenuItem ListContextBreakdown;

		private ToolStripMenuItem ToolsCategory;

		private ToolStripDropDownButton PrintMenu;

		private ToolStripMenuItem PrintMap;

		private ToolStripMenuItem PrintBlank;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem ToolsPlayerView;

		private Button CloseBtn;

		private ToolStripDropDownButton DelveBtn;

		private ToolStripMenuItem DelveAutoBuild;

		private ToolStripMenuItem DelveAdvanced;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem DelveDeck;

		private ToolStripDropDownButton AddBtn;

		private ToolStripMenuItem AddBuild;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem AddImport;

		private ToolStripMenuItem AddImportProject;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem AddTile;

		public MapArea SelectedArea
		{
			get
			{
				return this.AreaBox.SelectedItem as MapArea;
			}
		}

		public Map SelectedMap
		{
			get
			{
				if (this.MapList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.MapList.SelectedItems[0].Tag as Map;
			}
			set
			{
				this.MapList.SelectedItems.Clear();
				foreach (ListViewItem item in this.MapList.Items)
				{
					if (item.Tag != value)
					{
						continue;
					}
					item.Selected = true;
				}
			}
		}

		static MapListForm()
		{
			MapListForm.FullMap = "(entire map)";
		}

		public MapListForm()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.update_maps();
		}

		private void AddBuild_Click(object sender, EventArgs e)
		{
			if (Session.Tiles.Count == 0)
			{
				string str = string.Concat("You have no libraries containing map tiles.", Environment.NewLine);
				str = string.Concat(str, "Map tiles are required for map building.");
				MessageBox.Show(str, "Masterplan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			Map map = new Map()
			{
				Name = "New Map"
			};
			MapBuilderForm mapBuilderForm = new MapBuilderForm(map, false);
			if (mapBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.Maps.Add(mapBuilderForm.Map);
				Session.Modified = true;
				this.update_maps();
				this.update_thumbnail();
				this.SelectedMap = mapBuilderForm.Map;
			}
		}

		private void AddImport_Click(object sender, EventArgs e)
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
				Image = image,
				Category = TileCategory.Map
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
			Map map = new Map()
			{
				Name = FileName.Name(openFileDialog.FileName)
			};
			map.Tiles.Add(tileDatum);
			Session.Project.Maps.Add(map);
			Session.Modified = true;
			this.update_maps();
		}

		private void AddImportProject_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = Program.ProjectFilter
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Project project = Serialisation<Project>.Load(openFileDialog.FileName, SerialisationMode.Binary);
				if (project != null)
				{
					MapSelectForm mapSelectForm = new MapSelectForm(project.Maps, null, true);
					if (mapSelectForm.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
					{
						return;
					}
					Session.Project.PopulateProjectLibrary();
					foreach (Map map in mapSelectForm.Maps)
					{
						Session.Project.Maps.Add(map);
						foreach (TileData tile in map.Tiles)
						{
							if (Session.FindTile(tile.TileID, SearchType.Global) != null)
							{
								continue;
							}
							Tile tile1 = project.Library.FindTile(tile.TileID);
							if (tile1 == null)
							{
								continue;
							}
							Session.Project.Library.Tiles.Add(tile1);
						}
					}
					Session.Project.SimplifyProjectLibrary();
					Session.Modified = true;
					this.update_maps();
				}
			}
		}

		private void AddTile_Click(object sender, EventArgs e)
		{
			TileSelectForm tileSelectForm = new TileSelectForm(System.Drawing.Size.Empty, TileCategory.Map);
			if (tileSelectForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
			{
				return;
			}
			TileData tileDatum = new TileData()
			{
				TileID = tileSelectForm.Tile.ID
			};
			Map map = new Map()
			{
				Name = FileName.Name("New Map")
			};
			map.Tiles.Add(tileDatum);
			Session.Project.Maps.Add(map);
			Session.Modified = true;
			this.update_maps();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.RemoveBtn.Enabled = this.SelectedMap != null;
			this.EditBtn.Enabled = this.SelectedMap != null;
			this.PrintMap.Enabled = this.SelectedMap != null;
			this.ToolsBtn.Enabled = this.SelectedMap != null;
			this.DelveBtn.Enabled = (this.SelectedMap == null ? false : this.SelectedMap.Areas.Count != 0);
			this.AreaLbl.Enabled = this.SelectedMap != null;
			this.AreaBox.Enabled = this.SelectedMap != null;
			this.ListContextAdd.Enabled = true;
			this.ListContextRemove.Enabled = this.RemoveBtn.Enabled;
			this.ListContextEdit.Enabled = this.EditBtn.Enabled;
			this.ListContextCategory.Enabled = this.SelectedMap != null;
			this.ListContextDelve.Enabled = this.DelveBtn.Enabled;
			this.ListContextBreakdown.Enabled = this.SelectedMap != null;
		}

		private void AreaBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.MapView.Viewpoint = (this.SelectedArea != null ? this.SelectedArea.Region : Rectangle.Empty);
		}

		private void autobuild_delve(bool advanced)
		{
			AutoBuildData autoBuildDatum = null;
			if (!advanced)
			{
				autoBuildDatum = new AutoBuildData();
			}
			else
			{
				AutoBuildForm autoBuildForm = new AutoBuildForm(AutoBuildForm.Mode.Delve);
				if (autoBuildForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				{
					return;
				}
				autoBuildDatum = autoBuildForm.Data;
			}
			PlotPoint plotPoint = DelveBuilder.AutoBuild(this.SelectedMap, autoBuildDatum);
			Session.Project.Plot.Points.Add(plotPoint);
			Session.Modified = true;
			base.Close();
		}

		private void DelveAdvanced_Click(object sender, EventArgs e)
		{
			this.autobuild_delve(true);
		}

		private void DelveBtn_Click(object sender, EventArgs e)
		{
			this.autobuild_delve(false);
		}

		private void DelveDeck_Click(object sender, EventArgs e)
		{
			EncounterDeck encounterDeck = new EncounterDeck()
			{
				Name = string.Concat(this.SelectedMap, " Deck")
			};
			DeckBuilderForm deckBuilderForm = new DeckBuilderForm(encounterDeck);
			if (deckBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				encounterDeck = deckBuilderForm.Deck;
				PlotPoint plotPoint = new PlotPoint(string.Concat(this.SelectedMap.Name, " Delve"))
				{
					Element = new MapElement(this.SelectedMap.ID, Guid.Empty)
				};
				encounterDeck.DrawDelve(plotPoint, this.SelectedMap);
				Session.Project.Plot.Points.Add(plotPoint);
				Session.Modified = true;
				base.Close();
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

		private void EditBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				int map = Session.Project.Maps.IndexOf(this.SelectedMap);
				MapBuilderForm mapBuilderForm = new MapBuilderForm(this.SelectedMap, false);
				if (mapBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Session.Project.Maps[map] = mapBuilderForm.Map;
					Session.Modified = true;
					this.update_maps();
					this.update_thumbnail();
					this.SelectedMap = mapBuilderForm.Map;
				}
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MapListForm));
			this.MapList = new ListView();
			this.MapHdr = new ColumnHeader();
			this.ListContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ListContextAdd = new ToolStripMenuItem();
			this.ListContextRemove = new ToolStripMenuItem();
			this.ListContextEdit = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.ListContextCategory = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripSeparator();
			this.ListContextDelve = new ToolStripMenuItem();
			this.ListContextBreakdown = new ToolStripMenuItem();
			this.ListToolbar = new ToolStrip();
			this.AddBtn = new ToolStripDropDownButton();
			this.AddBuild = new ToolStripMenuItem();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.AddImport = new ToolStripMenuItem();
			this.AddImportProject = new ToolStripMenuItem();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.AddTile = new ToolStripMenuItem();
			this.RemoveBtn = new ToolStripButton();
			this.EditBtn = new ToolStripButton();
			this.Splitter = new SplitContainer();
			this.MapView = new Masterplan.Controls.MapView();
			this.MapToolbar = new ToolStrip();
			this.PrintMenu = new ToolStripDropDownButton();
			this.PrintMap = new ToolStripMenuItem();
			this.PrintBlank = new ToolStripMenuItem();
			this.ToolsBtn = new ToolStripDropDownButton();
			this.ToolsCategory = new ToolStripMenuItem();
			this.ToolsBreakdown = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.ToolsScreenshot = new ToolStripMenuItem();
			this.ToolsPlayerView = new ToolStripMenuItem();
			this.DelveBtn = new ToolStripDropDownButton();
			this.DelveAutoBuild = new ToolStripMenuItem();
			this.DelveAdvanced = new ToolStripMenuItem();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.DelveDeck = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.AreaLbl = new ToolStripLabel();
			this.AreaBox = new ToolStripComboBox();
			this.CloseBtn = new Button();
			this.ListContext.SuspendLayout();
			this.ListToolbar.SuspendLayout();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.Panel2.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.MapToolbar.SuspendLayout();
			base.SuspendLayout();
			this.MapList.Columns.AddRange(new ColumnHeader[] { this.MapHdr });
			this.MapList.ContextMenuStrip = this.ListContext;
			this.MapList.Dock = DockStyle.Fill;
			this.MapList.FullRowSelect = true;
			this.MapList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.MapList.HideSelection = false;
			this.MapList.Location = new Point(0, 25);
			this.MapList.MultiSelect = false;
			this.MapList.Name = "MapList";
			this.MapList.Size = new System.Drawing.Size(199, 317);
			this.MapList.TabIndex = 1;
			this.MapList.UseCompatibleStateImageBehavior = false;
			this.MapList.View = View.Details;
			this.MapList.SelectedIndexChanged += new EventHandler(this.MapList_SelectedIndexChanged);
			this.MapList.DoubleClick += new EventHandler(this.EditBtn_Click);
			this.MapHdr.Text = "Map";
			this.MapHdr.Width = 172;
			ToolStripItemCollection items = this.ListContext.Items;
			ToolStripItem[] listContextAdd = new ToolStripItem[] { this.ListContextAdd, this.ListContextRemove, this.ListContextEdit, this.toolStripMenuItem1, this.ListContextCategory, this.toolStripMenuItem2, this.ListContextDelve, this.ListContextBreakdown };
			items.AddRange(listContextAdd);
			this.ListContext.Name = "ListContext";
			this.ListContext.Size = new System.Drawing.Size(160, 148);
			this.ListContextAdd.Name = "ListContextAdd";
			this.ListContextAdd.Size = new System.Drawing.Size(159, 22);
			this.ListContextAdd.Text = "Add...";
			this.ListContextRemove.Name = "ListContextRemove";
			this.ListContextRemove.Size = new System.Drawing.Size(159, 22);
			this.ListContextRemove.Text = "Remove";
			this.ListContextRemove.Click += new EventHandler(this.RemoveBtn_Click);
			this.ListContextEdit.Name = "ListContextEdit";
			this.ListContextEdit.Size = new System.Drawing.Size(159, 22);
			this.ListContextEdit.Text = "Edit";
			this.ListContextEdit.Click += new EventHandler(this.EditBtn_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(156, 6);
			this.ListContextCategory.Name = "ListContextCategory";
			this.ListContextCategory.Size = new System.Drawing.Size(159, 22);
			this.ListContextCategory.Text = "Set Category...";
			this.ListContextCategory.Click += new EventHandler(this.ToolsCategory_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(156, 6);
			this.ListContextDelve.Name = "ListContextDelve";
			this.ListContextDelve.Size = new System.Drawing.Size(159, 22);
			this.ListContextDelve.Text = "Delve AutoBuild";
			this.ListContextDelve.Click += new EventHandler(this.DelveBtn_Click);
			this.ListContextBreakdown.Name = "ListContextBreakdown";
			this.ListContextBreakdown.Size = new System.Drawing.Size(159, 22);
			this.ListContextBreakdown.Text = "Tile Breakdown";
			this.ListContextBreakdown.Click += new EventHandler(this.ToolsBreakdown_Click);
			ToolStripItemCollection toolStripItemCollections = this.ListToolbar.Items;
			ToolStripItem[] addBtn = new ToolStripItem[] { this.AddBtn, this.RemoveBtn, this.EditBtn };
			toolStripItemCollections.AddRange(addBtn);
			this.ListToolbar.Location = new Point(0, 0);
			this.ListToolbar.Name = "ListToolbar";
			this.ListToolbar.Size = new System.Drawing.Size(199, 25);
			this.ListToolbar.TabIndex = 0;
			this.ListToolbar.Text = "toolStrip1";
			this.AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems = this.AddBtn.DropDownItems;
			ToolStripItem[] addBuild = new ToolStripItem[] { this.AddBuild, this.toolStripSeparator5, this.AddImport, this.AddImportProject, this.toolStripSeparator3, this.AddTile };
			dropDownItems.AddRange(addBuild);
			this.AddBtn.Image = (Image)componentResourceManager.GetObject("AddBtn.Image");
			this.AddBtn.ImageTransparentColor = Color.Magenta;
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new System.Drawing.Size(42, 22);
			this.AddBtn.Text = "Add";
			this.AddBuild.Name = "AddBuild";
			this.AddBuild.Size = new System.Drawing.Size(209, 22);
			this.AddBuild.Text = "Build a Map...";
			this.AddBuild.Click += new EventHandler(this.AddBuild_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(206, 6);
			this.AddImport.Name = "AddImport";
			this.AddImport.Size = new System.Drawing.Size(209, 22);
			this.AddImport.Text = "Import Map Image...";
			this.AddImport.Click += new EventHandler(this.AddImport_Click);
			this.AddImportProject.Name = "AddImportProject";
			this.AddImportProject.Size = new System.Drawing.Size(209, 22);
			this.AddImportProject.Text = "Import from Project File...";
			this.AddImportProject.Click += new EventHandler(this.AddImportProject_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(206, 6);
			this.AddTile.Name = "AddTile";
			this.AddTile.Size = new System.Drawing.Size(209, 22);
			this.AddTile.Text = "Use Map Tile...";
			this.AddTile.Click += new EventHandler(this.AddTile_Click);
			this.RemoveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.RemoveBtn.Image = (Image)componentResourceManager.GetObject("RemoveBtn.Image");
			this.RemoveBtn.ImageTransparentColor = Color.Magenta;
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new System.Drawing.Size(54, 22);
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new EventHandler(this.RemoveBtn_Click);
			this.EditBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.EditBtn.Image = (Image)componentResourceManager.GetObject("EditBtn.Image");
			this.EditBtn.ImageTransparentColor = Color.Magenta;
			this.EditBtn.Name = "EditBtn";
			this.EditBtn.Size = new System.Drawing.Size(31, 22);
			this.EditBtn.Text = "Edit";
			this.EditBtn.Click += new EventHandler(this.EditBtn_Click);
			this.Splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.Splitter.Location = new Point(12, 12);
			this.Splitter.Name = "Splitter";
			this.Splitter.Panel1.Controls.Add(this.MapList);
			this.Splitter.Panel1.Controls.Add(this.ListToolbar);
			this.Splitter.Panel2.Controls.Add(this.MapView);
			this.Splitter.Panel2.Controls.Add(this.MapToolbar);
			this.Splitter.Size = new System.Drawing.Size(750, 342);
			this.Splitter.SplitterDistance = 199;
			this.Splitter.TabIndex = 11;
			this.MapView.AllowDrawing = false;
			this.MapView.AllowDrop = true;
			this.MapView.AllowLinkCreation = false;
			this.MapView.AllowScrolling = false;
			this.MapView.BackgroundMap = null;
			this.MapView.BorderSize = 1;
			this.MapView.BorderStyle = BorderStyle.FixedSingle;
			this.MapView.Caption = "";
			this.MapView.Cursor = Cursors.Default;
			this.MapView.Dock = DockStyle.Fill;
			this.MapView.Encounter = null;
			this.MapView.FrameType = MapDisplayType.Dimmed;
			this.MapView.HighlightAreas = false;
			this.MapView.HoverToken = null;
			this.MapView.LineOfSight = false;
			this.MapView.Location = new Point(0, 25);
			this.MapView.Map = null;
			this.MapView.Mode = MapViewMode.Thumbnail;
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
			this.MapView.ShowGrid = MapGridMode.None;
			this.MapView.ShowGridLabels = false;
			this.MapView.ShowHealthBars = false;
			this.MapView.ShowPictureTokens = true;
			this.MapView.Size = new System.Drawing.Size(547, 317);
			this.MapView.TabIndex = 0;
			this.MapView.Tactical = false;
			this.MapView.TokenLinks = null;
			this.MapView.Viewpoint = new Rectangle(0, 0, 0, 0);
			ToolStripItemCollection items1 = this.MapToolbar.Items;
			ToolStripItem[] printMenu = new ToolStripItem[] { this.PrintMenu, this.ToolsBtn, this.DelveBtn, this.toolStripSeparator2, this.AreaLbl, this.AreaBox };
			items1.AddRange(printMenu);
			this.MapToolbar.Location = new Point(0, 0);
			this.MapToolbar.Name = "MapToolbar";
			this.MapToolbar.Size = new System.Drawing.Size(547, 25);
			this.MapToolbar.TabIndex = 1;
			this.MapToolbar.Text = "toolStrip1";
			this.PrintMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems1 = this.PrintMenu.DropDownItems;
			ToolStripItem[] printMap = new ToolStripItem[] { this.PrintMap, this.PrintBlank };
			dropDownItems1.AddRange(printMap);
			this.PrintMenu.Image = (Image)componentResourceManager.GetObject("PrintMenu.Image");
			this.PrintMenu.ImageTransparentColor = Color.Magenta;
			this.PrintMenu.Name = "PrintMenu";
			this.PrintMenu.Size = new System.Drawing.Size(45, 22);
			this.PrintMenu.Text = "Print";
			this.PrintMap.Name = "PrintMap";
			this.PrintMap.Size = new System.Drawing.Size(156, 22);
			this.PrintMap.Text = "Print Map...";
			this.PrintMap.Click += new EventHandler(this.PrintMap_Click);
			this.PrintBlank.Name = "PrintBlank";
			this.PrintBlank.Size = new System.Drawing.Size(156, 22);
			this.PrintBlank.Text = "Print Blank Grid";
			this.PrintBlank.Click += new EventHandler(this.PrintBlank_Click);
			this.ToolsBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection toolStripItemCollections1 = this.ToolsBtn.DropDownItems;
			ToolStripItem[] toolsCategory = new ToolStripItem[] { this.ToolsCategory, this.ToolsBreakdown, this.toolStripSeparator1, this.ToolsScreenshot, this.ToolsPlayerView };
			toolStripItemCollections1.AddRange(toolsCategory);
			this.ToolsBtn.Image = (Image)componentResourceManager.GetObject("ToolsBtn.Image");
			this.ToolsBtn.ImageTransparentColor = Color.Magenta;
			this.ToolsBtn.Name = "ToolsBtn";
			this.ToolsBtn.Size = new System.Drawing.Size(49, 22);
			this.ToolsBtn.Text = "Tools";
			this.ToolsCategory.Name = "ToolsCategory";
			this.ToolsCategory.Size = new System.Drawing.Size(177, 22);
			this.ToolsCategory.Text = "Set Category...";
			this.ToolsCategory.Click += new EventHandler(this.ToolsCategory_Click);
			this.ToolsBreakdown.Name = "ToolsBreakdown";
			this.ToolsBreakdown.Size = new System.Drawing.Size(177, 22);
			this.ToolsBreakdown.Text = "Tile Breakdown";
			this.ToolsBreakdown.Click += new EventHandler(this.ToolsBreakdown_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(174, 6);
			this.ToolsScreenshot.Name = "ToolsScreenshot";
			this.ToolsScreenshot.Size = new System.Drawing.Size(177, 22);
			this.ToolsScreenshot.Text = "Export Map";
			this.ToolsScreenshot.Click += new EventHandler(this.ToolsExport_Click);
			this.ToolsPlayerView.Name = "ToolsPlayerView";
			this.ToolsPlayerView.Size = new System.Drawing.Size(177, 22);
			this.ToolsPlayerView.Text = "Send to Player View";
			this.ToolsPlayerView.Click += new EventHandler(this.ToolsPlayerView_Click);
			this.DelveBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems2 = this.DelveBtn.DropDownItems;
			ToolStripItem[] delveAutoBuild = new ToolStripItem[] { this.DelveAutoBuild, this.DelveAdvanced, this.toolStripSeparator4, this.DelveDeck };
			dropDownItems2.AddRange(delveAutoBuild);
			this.DelveBtn.Image = (Image)componentResourceManager.GetObject("DelveBtn.Image");
			this.DelveBtn.ImageTransparentColor = Color.Magenta;
			this.DelveBtn.Name = "DelveBtn";
			this.DelveBtn.Size = new System.Drawing.Size(105, 22);
			this.DelveBtn.Text = "Delve AutoBuild";
			this.DelveAutoBuild.Name = "DelveAutoBuild";
			this.DelveAutoBuild.Size = new System.Drawing.Size(245, 22);
			this.DelveAutoBuild.Text = "Build a Delve";
			this.DelveAutoBuild.Click += new EventHandler(this.DelveBtn_Click);
			this.DelveAdvanced.Name = "DelveAdvanced";
			this.DelveAdvanced.Size = new System.Drawing.Size(245, 22);
			this.DelveAdvanced.Text = "Advanced Options...";
			this.DelveAdvanced.Click += new EventHandler(this.DelveAdvanced_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(242, 6);
			this.DelveDeck.Name = "DelveDeck";
			this.DelveDeck.Size = new System.Drawing.Size(245, 22);
			this.DelveDeck.Text = "Build Using an Encounter Deck...";
			this.DelveDeck.Click += new EventHandler(this.DelveDeck_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.AreaLbl.Name = "AreaLbl";
			this.AreaLbl.Size = new System.Drawing.Size(39, 22);
			this.AreaLbl.Text = "Show:";
			this.AreaBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.AreaBox.Name = "AreaBox";
			this.AreaBox.Size = new System.Drawing.Size(121, 25);
			this.AreaBox.SelectedIndexChanged += new EventHandler(this.AreaBox_SelectedIndexChanged);
			this.CloseBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBtn.Location = new Point(687, 360);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(75, 23);
			this.CloseBtn.TabIndex = 12;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			base.AcceptButton = this.CloseBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(774, 395);
			base.Controls.Add(this.CloseBtn);
			base.Controls.Add(this.Splitter);
			base.MinimizeBox = false;
			base.Name = "MapListForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Tactical Maps";
			this.ListContext.ResumeLayout(false);
			this.ListToolbar.ResumeLayout(false);
			this.ListToolbar.PerformLayout();
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.Panel1.PerformLayout();
			this.Splitter.Panel2.ResumeLayout(false);
			this.Splitter.Panel2.PerformLayout();
			this.Splitter.ResumeLayout(false);
			this.MapToolbar.ResumeLayout(false);
			this.MapToolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void MapList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.update_thumbnail();
		}

		private void PrintBlank_Click(object sender, EventArgs e)
		{
			BlankMap.Print();
		}

		private void PrintMap_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				(new MapPrintingForm(this.MapView)).ShowDialog();
			}
		}

		private void RemoveBtn_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				if (MessageBox.Show("Are you sure you want to delete this map?", "Masterplan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
				{
					return;
				}
				Session.Project.Maps.Remove(this.SelectedMap);
				Session.Modified = true;
				this.update_maps();
				this.update_thumbnail();
			}
		}

		private void ToolsBreakdown_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				(new TileBreakdownForm(this.SelectedMap)).ShowDialog();
			}
		}

		private void ToolsCategory_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				List<string> strs = new List<string>();
				foreach (Map map in Session.Project.Maps)
				{
					if (map.Category == null || map.Category == "" || strs.Contains(map.Category))
					{
						continue;
					}
					strs.Add(map.Category);
				}
				CategoryForm categoryForm = new CategoryForm(strs, this.SelectedMap.Category);
				if (categoryForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.SelectedMap.Category = categoryForm.Category;
					Session.Modified = true;
					this.update_maps();
				}
			}
		}

		private void ToolsExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog()
				{
					FileName = this.SelectedMap.Name
				};
				if (this.SelectedArea != null)
				{
					SaveFileDialog saveFileDialog1 = saveFileDialog;
					saveFileDialog1.FileName = string.Concat(saveFileDialog1.FileName, " - ", this.SelectedArea.Name);
				}
				saveFileDialog.Filter = "Bitmap Image |*.bmp|JPEG Image|*.jpg|GIF Image|*.gif|PNG Image|*.png";
				if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					ImageFormat bmp = ImageFormat.Bmp;
					switch (saveFileDialog.FilterIndex)
					{
						case 1:
						{
							bmp = ImageFormat.Bmp;
							break;
						}
						case 2:
						{
							bmp = ImageFormat.Jpeg;
							break;
						}
						case 3:
						{
							bmp = ImageFormat.Gif;
							break;
						}
						case 4:
						{
							bmp = ImageFormat.Png;
							break;
						}
					}
					Bitmap bitmap = Screenshot.Map(this.MapView.Map, this.MapView.Viewpoint, null, null, null);
					bitmap.Save(saveFileDialog.FileName, bmp);
				}
			}
		}

		private void ToolsPlayerView_Click(object sender, EventArgs e)
		{
			if (this.SelectedMap != null)
			{
				if (Session.PlayerView == null)
				{
					Session.PlayerView = new PlayerViewForm(this);
				}
				Session.PlayerView.ShowTacticalMap(this.MapView, null);
			}
		}

		private void update_maps()
		{
			List<string> strs = new List<string>();
			foreach (Map map in Session.Project.Maps)
			{
				if (map.Category == null || map.Category == "" || strs.Contains(map.Category))
				{
					continue;
				}
				strs.Add(map.Category);
			}
			strs.Sort();
			strs.Add("Miscellaneous Maps");
			this.MapList.Groups.Clear();
			foreach (string str in strs)
			{
				this.MapList.Groups.Add(str, str);
			}
			this.MapList.ShowGroups = true;
			this.MapList.Items.Clear();
			foreach (Map map1 in Session.Project.Maps)
			{
				ListViewItem item = this.MapList.Items.Add(map1.Name);
				item.Tag = map1;
				if (map1.Category == null || !(map1.Category != ""))
				{
					item.Group = this.MapList.Groups["Miscellaneous Maps"];
				}
				else
				{
					item.Group = this.MapList.Groups[map1.Category];
				}
			}
			if (this.MapList.Items.Count == 0)
			{
				this.MapList.ShowGroups = false;
				ListViewItem grayText = this.MapList.Items.Add("(no maps)");
				grayText.ForeColor = SystemColors.GrayText;
			}
		}

		private void update_thumbnail()
		{
			if (this.MapView.Map != this.SelectedMap)
			{
				this.MapView.Map = this.SelectedMap;
				this.AreaBox.Enabled = this.MapView.Map != null;
				this.AreaBox.Items.Clear();
				if (this.SelectedMap != null)
				{
					this.AreaBox.Items.Add(MapListForm.FullMap);
					foreach (MapArea area in this.SelectedMap.Areas)
					{
						this.AreaBox.Items.Add(area);
					}
					this.AreaBox.SelectedIndex = 0;
				}
			}
		}
	}
}