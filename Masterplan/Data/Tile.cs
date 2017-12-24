using System;
using System.Drawing;

namespace Masterplan.Data
{
	[Serializable]
	public class Tile
	{
		private Guid fID = Guid.NewGuid();

		private TileCategory fCategory = TileCategory.Special;

		private System.Drawing.Size fSize = new System.Drawing.Size(2, 2);

		private System.Drawing.Image fImage;

		private Color fBlankColour = Color.White;

		private string fKeywords = "";

		public int Area
		{
			get
			{
				return this.fSize.Width * this.fSize.Height;
			}
		}

		public Color BlankColour
		{
			get
			{
				return this.fBlankColour;
			}
			set
			{
				this.fBlankColour = value;
			}
		}

		public System.Drawing.Image BlankImage
		{
			get
			{
				int num = 32;
				int width = this.fSize.Width * num + 1;
				int height = this.fSize.Height * num + 1;
				Bitmap bitmap = new Bitmap(width, height);
				for (int i = 0; i != width; i++)
				{
					for (int j = 0; j != height; j++)
					{
						Color darkGray = this.fBlankColour;
						if (i % num == 0 || j % num == 0)
						{
							darkGray = Color.DarkGray;
						}
						bitmap.SetPixel(i, j, darkGray);
					}
				}
				return bitmap;
			}
		}

		public TileCategory Category
		{
			get
			{
				return this.fCategory;
			}
			set
			{
				this.fCategory = value;
			}
		}

		public Guid ID
		{
			get
			{
				return this.fID;
			}
			set
			{
				this.fID = value;
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
			}
		}

		public string Keywords
		{
			get
			{
				return this.fKeywords;
			}
			set
			{
				this.fKeywords = value;
			}
		}

		public System.Drawing.Size Size
		{
			get
			{
				return this.fSize;
			}
			set
			{
				this.fSize = value;
			}
		}

		public Tile()
		{
		}

		public Tile Copy()
		{
			Tile tile = new Tile()
			{
				ID = this.fID,
				Category = this.fCategory,
				Size = new System.Drawing.Size(this.fSize.Width, this.fSize.Height),
				Image = this.fImage,
				Keywords = this.fKeywords
			};
			return tile;
		}

		public override string ToString()
		{
			return string.Concat(this.fSize.Width, " x ", this.fSize.Height);
		}
	}
}