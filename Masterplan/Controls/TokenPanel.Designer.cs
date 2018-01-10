using Masterplan;
using Masterplan.Data;
using Masterplan.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class TokenPanel : UserControl
	{
		private PictureBox ImageBox;

		private ToolStrip PictureToolbar;

		private ToolStripDropDownButton toolStripButton1;

		private ToolStripMenuItem ImageSelectFile;

		private ToolStripMenuItem ImageSelectTile;

		private ToolStripMenuItem ImageSelectColour;

		private ToolStripLabel ImageClear;

		private System.Drawing.Size fTileSize = new System.Drawing.Size(2, 2);

		private System.Drawing.Image fImage;

		private Color fColour = Color.Blue;

		public Color Colour
		{
			get
			{
				return this.fColour;
			}
			set
			{
				this.fColour = value;
				this.update_picture();
			}
		}

		public System.Drawing.Image Image
		{
			get
			{
				return this.fImage;
			}
			set
			{
				this.fImage = value;
				this.update_picture();
			}
		}

		public System.Drawing.Size TileSize
		{
			get
			{
				return this.fTileSize;
			}
			set
			{
				this.fTileSize = value;
			}
		}

		public TokenPanel()
		{
			this.InitializeComponent();
			Application.Idle += new EventHandler(this.Application_Idle);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			this.ImageClear.Enabled = this.fImage != null;
		}

		private void ImageClear_Click(object sender, EventArgs e)
		{
			this.fImage = null;
			this.update_picture();
		}

		private void ImageSelectColour_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog()
			{
				AllowFullOpen = true,
				Color = this.ImageBox.BackColor
			};
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				this.fImage = null;
				this.fColour = colorDialog.Color;
				this.update_picture();
			}
		}

		private void ImageSelectFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = Program.ImageFilter
			};
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.fImage = System.Drawing.Image.FromFile(openFileDialog.FileName);
				this.update_picture();
			}
		}

		private void ImageSelectTile_Click(object sender, EventArgs e)
		{
			TileSelectForm tileSelectForm = new TileSelectForm(this.fTileSize, TileCategory.Feature);
			if (tileSelectForm.ShowDialog() == DialogResult.OK)
			{
				this.fImage = tileSelectForm.Tile.Image;
				if (tileSelectForm.Tile.Size.Width != this.fTileSize.Width || tileSelectForm.Tile.Size.Height != this.fTileSize.Height)
				{
					this.fImage = new Bitmap(this.fImage);
					this.fImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
				}
				this.update_picture();
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TokenPanel));
			this.ImageBox = new PictureBox();
			this.PictureToolbar = new ToolStrip();
			this.toolStripButton1 = new ToolStripDropDownButton();
			this.ImageSelectFile = new ToolStripMenuItem();
			this.ImageSelectTile = new ToolStripMenuItem();
			this.ImageSelectColour = new ToolStripMenuItem();
			this.ImageClear = new ToolStripLabel();
			((ISupportInitialize)this.ImageBox).BeginInit();
			this.PictureToolbar.SuspendLayout();
			base.SuspendLayout();
			this.ImageBox.BackColor = Color.Transparent;
			this.ImageBox.Dock = DockStyle.Fill;
			this.ImageBox.Location = new Point(0, 25);
			this.ImageBox.Name = "ImageBox";
			this.ImageBox.Size = new System.Drawing.Size(237, 205);
			this.ImageBox.SizeMode = PictureBoxSizeMode.Zoom;
			this.ImageBox.TabIndex = 3;
			this.ImageBox.TabStop = false;
			ToolStripItemCollection items = this.PictureToolbar.Items;
			ToolStripItem[] imageClear = new ToolStripItem[] { this.toolStripButton1, this.ImageClear };
			items.AddRange(imageClear);
			this.PictureToolbar.Location = new Point(0, 0);
			this.PictureToolbar.Name = "PictureToolbar";
			this.PictureToolbar.Size = new System.Drawing.Size(237, 25);
			this.PictureToolbar.TabIndex = 2;
			this.PictureToolbar.Text = "toolStrip1";
			this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
			ToolStripItemCollection dropDownItems = this.toolStripButton1.DropDownItems;
			ToolStripItem[] imageSelectFile = new ToolStripItem[] { this.ImageSelectFile, this.ImageSelectTile, this.ImageSelectColour };
			dropDownItems.AddRange(imageSelectFile);
			this.toolStripButton1.Image = (System.Drawing.Image)componentResourceManager.GetObject("toolStripButton1.Image");
			this.toolStripButton1.ImageTransparentColor = Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(51, 22);
			this.toolStripButton1.Text = "Select";
			this.ImageSelectFile.Name = "ImageSelectFile";
			this.ImageSelectFile.Size = new System.Drawing.Size(160, 22);
			this.ImageSelectFile.Text = "From File...";
			this.ImageSelectFile.Click += new EventHandler(this.ImageSelectFile_Click);
			this.ImageSelectTile.Name = "ImageSelectTile";
			this.ImageSelectTile.Size = new System.Drawing.Size(160, 22);
			this.ImageSelectTile.Text = "From Map Tile...";
			this.ImageSelectTile.Click += new EventHandler(this.ImageSelectTile_Click);
			this.ImageSelectColour.Name = "ImageSelectColour";
			this.ImageSelectColour.Size = new System.Drawing.Size(160, 22);
			this.ImageSelectColour.Text = "Plain Colour...";
			this.ImageSelectColour.Click += new EventHandler(this.ImageSelectColour_Click);
			this.ImageClear.IsLink = true;
			this.ImageClear.Name = "ImageClear";
			this.ImageClear.Size = new System.Drawing.Size(34, 22);
			this.ImageClear.Text = "Clear";
			this.ImageClear.Click += new EventHandler(this.ImageClear_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.ImageBox);
			base.Controls.Add(this.PictureToolbar);
			base.Name = "TilePanel";
			base.Size = new System.Drawing.Size(237, 230);
			((ISupportInitialize)this.ImageBox).EndInit();
			this.PictureToolbar.ResumeLayout(false);
			this.PictureToolbar.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void update_picture()
		{
			if (this.fImage != null)
			{
				this.ImageBox.BackColor = Color.Transparent;
				this.ImageBox.Image = this.fImage;
				return;
			}
			this.ImageBox.BackColor = this.fColour;
			this.ImageBox.Image = null;
		}
	}
}