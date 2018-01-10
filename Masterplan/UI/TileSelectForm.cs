using Masterplan;
using Masterplan.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TileSelectForm : Form
	{
		private Button OKBtn;

		private ListView TileList;

		private Button CancelBtn;

		private Panel TilePanel;

		private ToolStrip Toolbar;

		private ToolStripButton LibraryBtn;

		private ToolStripButton CategoryBtn;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton MatchCatBtn;

		private System.Drawing.Size fTileSize = System.Drawing.Size.Empty;

		private TileCategory fCategory = TileCategory.Map;

		private TileSelectForm.GroupBy fGroupBy;

		private bool fMatchCategory = true;

		public Masterplan.Data.Tile Tile
		{
			get
			{
				if (this.TileList.SelectedItems.Count == 0)
				{
					return null;
				}
				return this.TileList.SelectedItems[0].Tag as Masterplan.Data.Tile;
			}
		}

		public TileSelectForm(System.Drawing.Size tilesize, TileCategory category)
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
			this.fTileSize = tilesize;
			this.fCategory = category;
			this.MatchCatBtn.Text = string.Concat("Show only tiles in category: ", this.fCategory);
			this.update_tiles();
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.OKBtn.Enabled = this.Tile != null;
			this.LibraryBtn.Checked = this.fGroupBy == TileSelectForm.GroupBy.Library;
			this.CategoryBtn.Checked = this.fGroupBy == TileSelectForm.GroupBy.Category;
			this.MatchCatBtn.Checked = this.fMatchCategory;
		}

		private void CategoryBtn_Click(object sender, EventArgs e)
		{
			this.fGroupBy = TileSelectForm.GroupBy.Category;
			this.update_tiles();
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TileSelectForm));
			this.OKBtn = new Button();
			this.TileList = new ListView();
			this.CancelBtn = new Button();
			this.TilePanel = new Panel();
			this.Toolbar = new ToolStrip();
			this.LibraryBtn = new ToolStripButton();
			this.CategoryBtn = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.MatchCatBtn = new ToolStripButton();
			this.TilePanel.SuspendLayout();
			this.Toolbar.SuspendLayout();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(438, 307);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 1;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.TileList.Dock = DockStyle.Fill;
			this.TileList.FullRowSelect = true;
			this.TileList.HideSelection = false;
			this.TileList.Location = new Point(0, 25);
			this.TileList.MultiSelect = false;
			this.TileList.Name = "TileList";
			this.TileList.Size = new System.Drawing.Size(582, 264);
			this.TileList.TabIndex = 0;
			this.TileList.UseCompatibleStateImageBehavior = false;
			this.TileList.DoubleClick += new EventHandler(this.TileList_DoubleClick);
			this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new Point(519, 307);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.TilePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.TilePanel.Controls.Add(this.TileList);
			this.TilePanel.Controls.Add(this.Toolbar);
			this.TilePanel.Location = new Point(12, 12);
			this.TilePanel.Name = "TilePanel";
			this.TilePanel.Size = new System.Drawing.Size(582, 289);
			this.TilePanel.TabIndex = 3;
			ToolStripItemCollection items = this.Toolbar.Items;
			ToolStripItem[] libraryBtn = new ToolStripItem[] { this.LibraryBtn, this.CategoryBtn, this.toolStripSeparator1, this.MatchCatBtn };
			items.AddRange(libraryBtn);
			this.Toolbar.Location = new Point(0, 0);
			this.Toolbar.Name = "Toolbar";
			this.Toolbar.Size = new System.Drawing.Size(582, 25);
			this.Toolbar.TabIndex = 0;
			this.Toolbar.Text = "toolStrip1";
			this.LibraryBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.LibraryBtn.Image = (Image)componentResourceManager.GetObject("LibraryBtn.Image");
			this.LibraryBtn.ImageTransparentColor = Color.Magenta;
			this.LibraryBtn.Name = "LibraryBtn";
			this.LibraryBtn.Size = new System.Drawing.Size(108, 22);
			this.LibraryBtn.Text = "Arrange by Library";
			this.LibraryBtn.Click += new EventHandler(this.LibraryBtn_Click);
			this.CategoryBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.CategoryBtn.Image = (Image)componentResourceManager.GetObject("CategoryBtn.Image");
			this.CategoryBtn.ImageTransparentColor = Color.Magenta;
			this.CategoryBtn.Name = "CategoryBtn";
			this.CategoryBtn.Size = new System.Drawing.Size(120, 22);
			this.CategoryBtn.Text = "Arrange by Category";
			this.CategoryBtn.Click += new EventHandler(this.CategoryBtn_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.MatchCatBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.MatchCatBtn.Image = (Image)componentResourceManager.GetObject("MatchCatBtn.Image");
			this.MatchCatBtn.ImageTransparentColor = Color.Magenta;
			this.MatchCatBtn.Name = "MatchCatBtn";
			this.MatchCatBtn.Size = new System.Drawing.Size(96, 22);
			this.MatchCatBtn.Text = "Match Category";
			this.MatchCatBtn.Click += new EventHandler(this.MatchCatBtn_Click);
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.CancelBtn;
			base.ClientSize = new System.Drawing.Size(606, 342);
			base.Controls.Add(this.TilePanel);
			base.Controls.Add(this.CancelBtn);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TileSelectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select Tile";
			this.TilePanel.ResumeLayout(false);
			this.TilePanel.PerformLayout();
			this.Toolbar.ResumeLayout(false);
			this.Toolbar.PerformLayout();
			base.ResumeLayout(false);
		}

		private void LibraryBtn_Click(object sender, EventArgs e)
		{
			this.fGroupBy = TileSelectForm.GroupBy.Library;
			this.update_tiles();
		}

		private void MatchCatBtn_Click(object sender, EventArgs e)
		{
			this.fMatchCategory = !this.fMatchCategory;
			this.update_tiles();
		}

		private void TileList_DoubleClick(object sender, EventArgs e)
		{
			if (this.Tile != null)
			{
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
				base.Close();
			}
		}

		private void update_tiles()
		{
			List<Masterplan.Data.Tile> tiles = new List<Masterplan.Data.Tile>();
			foreach (Library library in Session.Libraries)
			{
				foreach (Masterplan.Data.Tile tile in library.Tiles)
				{
					if (this.fMatchCategory && this.fCategory != tile.Category)
					{
						continue;
					}
					bool flag = false;
					if (this.fTileSize == System.Drawing.Size.Empty)
					{
						flag = true;
					}
					else
					{
						if (tile.Size.Width == this.fTileSize.Width && tile.Size.Height == this.fTileSize.Height)
						{
							flag = true;
						}
						if (tile.Size.Width == this.fTileSize.Height && tile.Size.Height == this.fTileSize.Width)
						{
							flag = true;
						}
					}
					if (!flag)
					{
						continue;
					}
					tiles.Add(tile);
				}
			}
			this.TileList.Groups.Clear();
			switch (this.fGroupBy)
			{
				case TileSelectForm.GroupBy.Library:
				{
					List<Library>.Enumerator enumerator = Session.Libraries.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							Library current = enumerator.Current;
							this.TileList.Groups.Add(current.Name, current.Name);
						}
						break;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				case TileSelectForm.GroupBy.Category:
				{
					IEnumerator enumerator1 = Enum.GetValues(typeof(TileCategory)).GetEnumerator();
					try
					{
						while (enumerator1.MoveNext())
						{
							TileCategory tileCategory = (TileCategory)enumerator1.Current;
							this.TileList.Groups.Add(tileCategory.ToString(), tileCategory.ToString());
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
				}
			}
			this.TileList.BeginUpdate();
			this.TileList.LargeImageList = new ImageList()
			{
				ColorDepth = ColorDepth.Depth32Bit,
				ImageSize = new System.Drawing.Size(64, 64)
			};
			List<ListViewItem> listViewItems = new List<ListViewItem>();
			foreach (Masterplan.Data.Tile tile1 in tiles)
			{
				ListViewItem listViewItem = new ListViewItem(tile1.ToString())
				{
					Tag = tile1
				};
				switch (this.fGroupBy)
				{
					case TileSelectForm.GroupBy.Library:
					{
						Library library1 = Session.FindLibrary(tile1);
						listViewItem.Group = this.TileList.Groups[library1.Name];
						break;
					}
					case TileSelectForm.GroupBy.Category:
					{
						listViewItem.Group = this.TileList.Groups[tile1.Category.ToString()];
						break;
					}
				}
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
				listViewItem.ImageIndex = this.TileList.LargeImageList.Images.Count - 1;
				listViewItems.Add(listViewItem);
			}
			this.TileList.Items.Clear();
			this.TileList.Items.AddRange(listViewItems.ToArray());
			this.TileList.EndUpdate();
		}

		private enum GroupBy
		{
			Library,
			Category
		}
	}
}