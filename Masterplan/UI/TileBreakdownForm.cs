using Masterplan;
using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal class TileBreakdownForm : Form
	{
		private Button OKBtn;

		private ListView TileList;

		public TileBreakdownForm(Map map)
		{
			this.InitializeComponent();
			Dictionary<Guid, int> guids = new Dictionary<Guid, int>();
			Dictionary<Guid, int> guids1 = new Dictionary<Guid, int>();
			foreach (TileData tile in map.Tiles)
			{
				if (!guids1.ContainsKey(tile.TileID))
				{
					guids1[tile.TileID] = 0;
				}
				Dictionary<Guid, int> item = guids1;
				Dictionary<Guid, int> guids2 = item;
				Guid tileID = tile.TileID;
				item[tileID] = guids2[tileID] + 1;
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
			List<string> strs = new List<string>();
			foreach (Guid guid in guids.Keys)
			{
				Library library = Session.FindLibrary(Session.FindTile(guid, SearchType.Global));
				if (strs.Contains(library.Name))
				{
					continue;
				}
				strs.Add(library.Name);
			}
			strs.Sort();
			foreach (string str in strs)
			{
				this.TileList.Groups.Add(str, str);
			}
			this.TileList.LargeImageList = new ImageList()
			{
				ImageSize = new System.Drawing.Size(64, 64)
			};
			foreach (Guid key1 in guids.Keys)
			{
				Tile tile1 = Session.FindTile(key1, SearchType.Global);
				Library library1 = Session.FindLibrary(tile1);
				ListViewItem count = this.TileList.Items.Add(string.Concat("x ", guids[key1]));
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

		private void InitializeComponent()
		{
			this.OKBtn = new Button();
			this.TileList = new ListView();
			base.SuspendLayout();
			this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKBtn.Location = new Point(330, 376);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(75, 23);
			this.OKBtn.TabIndex = 0;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.TileList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.TileList.FullRowSelect = true;
			this.TileList.HideSelection = false;
			this.TileList.Location = new Point(12, 12);
			this.TileList.MultiSelect = false;
			this.TileList.Name = "TileList";
			this.TileList.Size = new System.Drawing.Size(393, 358);
			this.TileList.TabIndex = 1;
			this.TileList.UseCompatibleStateImageBehavior = false;
			base.AcceptButton = this.OKBtn;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(417, 411);
			base.Controls.Add(this.TileList);
			base.Controls.Add(this.OKBtn);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TileBreakdownForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Map Tiles Used";
			base.ResumeLayout(false);
		}
	}
}