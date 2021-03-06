using Masterplan;
using Masterplan.Controls;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Masterplan.UI
{
	internal class MapAreaSelectForm : Form
	{
		private Label MapLbl;

		private ComboBox MapBox;

		private Label AreaLbl;

		private ComboBox AreaBox;

		private Button OKBtn;

		private Button CancelBtn;

		private Masterplan.Controls.MapView MapView;

		private Button NewBtn;

		private Button ImportBtn;

		private Button UseTileBtn;

		public Masterplan.Data.Map Map
		{
			get
			{
				return this.MapBox.SelectedItem as Masterplan.Data.Map;
			}
		}

		public Masterplan.Data.MapArea MapArea
		{
			get
			{
				return this.AreaBox.SelectedItem as Masterplan.Data.MapArea;
			}
		}

		public MapAreaSelectForm(Guid map_id, Guid map_area_id)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.UseTileBtn.Visible = this.map_tiles_exist();
			this.MapBox.Items.Add("(no map)");
			foreach (Masterplan.Data.Map map in Session.Project.Maps)
			{
				this.MapBox.Items.Add(map);
			}
			Masterplan.Data.Map map1 = Session.Project.FindTacticalMap(map_id);
			if (map1 == null)
			{
				this.MapBox.SelectedIndex = 0;
				this.AreaBox.Items.Add("(no map)");
				this.AreaBox.SelectedIndex = 0;
				return;
			}
			this.MapBox.SelectedItem = map1;
			Masterplan.Data.MapArea mapArea = map1.FindArea(map_area_id);
			if (mapArea != null)
			{
				this.AreaBox.SelectedItem = mapArea;
				return;
			}
			this.AreaBox.SelectedIndex = 0;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.MapLbl.Enabled = Session.Project.Maps.Count != 0;
			this.MapBox.Enabled = Session.Project.Maps.Count != 0;
			Masterplan.Data.Map selectedItem = this.MapBox.SelectedItem as Masterplan.Data.Map;
			bool flag = (selectedItem == null ? false : selectedItem.Areas.Count != 0);
			this.AreaLbl.Enabled = flag;
			this.AreaBox.Enabled = flag;
		}

		private void AreaBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.show_map();
		}

		private void ImportBtn_Click(object sender, EventArgs e)
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
			Masterplan.Data.Map map = new Masterplan.Data.Map()
			{
				Name = FileName.Name(openFileDialog.FileName)
			};
			map.Tiles.Add(tileDatum);
			Session.Project.Maps.Add(map);
			Session.Modified = true;
			this.MapBox.Items.Add(map);
			this.MapBox.SelectedItem = map;
		}

		private void InitializeComponent()
		{
			this.MapLbl = new Label();
			this.MapBox = new ComboBox();
			this.AreaLbl = new Label();
			this.AreaBox = new ComboBox();
			this.OKBtn = new Button();
			this.CancelBtn = new Button();
			this.MapView = new Masterplan.Controls.MapView();
			this.NewBtn = new Button();
			this.ImportBtn = new Button();
			this.UseTileBtn = new Button();
			base.SuspendLayout();
			this.MapLbl.AutoSize = true;
			this.MapLbl.Location = new Point(12, 15);
			this.MapLbl.Name = "MapLbl";
			this.MapLbl.Size = new System.Drawing.Size(31, 13);
			this.MapLbl.TabIndex = 0;
			this.MapLbl.Text = "Map:";
			this.MapBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.MapBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.MapBox.FormattingEnabled = true;
			this.MapBox.Location = new Point(50, 12);
			this.MapBox.Name = "MapBox";
			this.MapBox.Size = new System.Drawing.Size(497, 21);
			this.MapBox.Sorted = true;
			this.MapBox.TabIndex = 1;
			this.MapBox.SelectedIndexChanged += new EventHandler(this.MapBox_SelectedIndexChanged);
			this.AreaLbl.AutoSize = true;
			this.AreaLbl.Location = new Point(12, 42);
			this.AreaLbl.Name = "AreaLbl";
			this.AreaLbl.Size = new System.Drawing.Size(32, 13);
			this.AreaLbl.TabIndex = 2;
			this.AreaLbl.Text = "Area:";
			this.AreaBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.AreaBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.AreaBox.FormattingEnabled = true;
			this.AreaBox.Location = new Point(50, 39);
			this.AreaBox.Name = "AreaBox";
			this.AreaBox.Size = new System.Drawing.Size(497, 21);
			this.AreaBox.Sorted = true;
			this.AreaBox.TabIndex = 3;
			this.AreaBox.SelectedIndexChanged += new EventHandler(this.AreaBox_SelectedIndexChanged);
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(391, 357);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 8;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(472, 357);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 9;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.MapView.AllowDrawing = false;
			this.MapView.AllowDrop = true;
			this.MapView.AllowLinkCreation = false;
			this.MapView.AllowScrolling = false;
			this.MapView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.MapView.BackgroundMap = null;
			this.MapView.BorderSize = 0;
			this.MapView.BorderStyle = BorderStyle.FixedSingle;
			this.MapView.Cursor = Cursors.Default;
			this.MapView.Encounter = null;
			this.MapView.FrameType = MapDisplayType.Dimmed;
			this.MapView.HighlightAreas = false;
			this.MapView.HoverToken = null;
			this.MapView.LineOfSight = false;
			this.MapView.Location = new Point(12, 66);
			this.MapView.Map = null;
			this.MapView.Mode = MapViewMode.Plain;
			this.MapView.Name = "MapView";
			this.MapView.ScalingFactor = 1;
			this.MapView.SelectedArea = null;
			this.MapView.SelectedTiles = null;
			this.MapView.Selection = new Rectangle(0, 0, 0, 0);
			this.MapView.ShowAuras = true;
			this.MapView.ShowCreatureLabels = true;
			this.MapView.ShowCreatures = CreatureViewMode.All;
			this.MapView.ShowGrid = MapGridMode.None;
			this.MapView.ShowGridLabels = false;
			this.MapView.ShowHealthBars = false;
			this.MapView.ShowPictureTokens = true;
			this.MapView.Size = new System.Drawing.Size(535, 285);
			this.MapView.TabIndex = 4;
			this.MapView.Tactical = false;
			this.MapView.TokenLinks = null;
			this.MapView.Viewpoint = new Rectangle(0, 0, 0, 0);
			this.NewBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.NewBtn.Location = new Point(12, 357);
			this.NewBtn.Name = "NewBtn";
			this.NewBtn.Size = new System.Drawing.Size(100, 23);
			this.NewBtn.TabIndex = 5;
			this.NewBtn.Text = "Create New Map";
			this.NewBtn.UseVisualStyleBackColor = true;
			this.NewBtn.Click += new EventHandler(this.NewBtn_Click);
			this.ImportBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.ImportBtn.Location = new Point(118, 357);
			this.ImportBtn.Name = "ImportBtn";
			this.ImportBtn.Size = new System.Drawing.Size(100, 23);
			this.ImportBtn.TabIndex = 6;
			this.ImportBtn.Text = "Import Map File";
			this.ImportBtn.UseVisualStyleBackColor = true;
			this.ImportBtn.Click += new EventHandler(this.ImportBtn_Click);
			this.UseTileBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.UseTileBtn.Location = new Point(224, 357);
			this.UseTileBtn.Name = "UseTileBtn";
			this.UseTileBtn.Size = new System.Drawing.Size(100, 23);
			this.UseTileBtn.TabIndex = 7;
			this.UseTileBtn.Text = "Use Map Tile";
			this.UseTileBtn.UseVisualStyleBackColor = true;
			this.UseTileBtn.Click += new EventHandler(this.UseTileBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(559, 392);
			base.Controls.Add(this.UseTileBtn);
			base.Controls.Add(this.ImportBtn);
			base.Controls.Add(this.NewBtn);
			base.Controls.Add(this.MapView);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.Controls.Add(this.AreaBox);
			base.Controls.Add(this.AreaLbl);
			base.Controls.Add(this.MapBox);
			base.Controls.Add(this.MapLbl);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MapAreaSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Location";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private bool map_tiles_exist()
		{
			bool flag;
			List<Library> libraries = new List<Library>();
			libraries.AddRange(Session.Libraries);
			if (Session.Project != null)
			{
				libraries.Add(Session.Project.Library);
			}
			List<Library>.Enumerator enumerator = libraries.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					List<Tile>.Enumerator enumerator1 = enumerator.Current.Tiles.GetEnumerator();
					try
					{
						while (enumerator1.MoveNext())
						{
							if (enumerator1.Current.Category != TileCategory.Map)
							{
								continue;
							}
							flag = true;
							return flag;
						}
					}
					finally
					{
						((IDisposable)enumerator1).Dispose();
					}
				}
				return false;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		private void MapBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.AreaBox.Items.Clear();
			Masterplan.Data.Map selectedItem = this.MapBox.SelectedItem as Masterplan.Data.Map;
			if (selectedItem != null)
			{
				this.AreaBox.Items.Add("(entire map)");
				foreach (Masterplan.Data.MapArea area in selectedItem.Areas)
				{
					this.AreaBox.Items.Add(area);
				}
				this.AreaBox.SelectedIndex = 0;
			}
			this.show_map();
		}

		private void NewBtn_Click(object sender, EventArgs e)
		{
			Masterplan.Data.Map map = new Masterplan.Data.Map()
			{
				Name = "New Map"
			};
			MapBuilderForm mapBuilderForm = new MapBuilderForm(map, false);
			if (mapBuilderForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Session.Project.Maps.Add(mapBuilderForm.Map);
				Session.Modified = true;
				this.MapBox.Items.Add(mapBuilderForm.Map);
				this.MapBox.SelectedItem = mapBuilderForm.Map;
			}
		}

		private void show_map()
		{
			Masterplan.Data.Map selectedItem = this.MapBox.SelectedItem as Masterplan.Data.Map;
			if (selectedItem == null)
			{
				this.MapView.Map = null;
				return;
			}
			this.MapView.Map = selectedItem;
			Masterplan.Data.MapArea mapArea = this.AreaBox.SelectedItem as Masterplan.Data.MapArea;
			if (mapArea == null)
			{
				this.MapView.Viewpoint = Rectangle.Empty;
				return;
			}
			this.MapView.Viewpoint = mapArea.Region;
		}

		private void UseTileBtn_Click(object sender, EventArgs e)
		{
			TileSelectForm tileSelectForm = new TileSelectForm(System.Drawing.Size.Empty, TileCategory.Map);
			if (tileSelectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				TileData tileDatum = new TileData()
				{
					TileID = tileSelectForm.Tile.ID
				};
				Masterplan.Data.Map map = new Masterplan.Data.Map()
				{
					Name = FileName.Name("New Map")
				};
				map.Tiles.Add(tileDatum);
				Session.Project.Maps.Add(map);
				Session.Modified = true;
				this.MapBox.Items.Add(map);
				this.MapBox.SelectedItem = map;
			}
		}
	}
}