using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class TilePanel : UserControl
	{
		private IContainer components;

		private Image fTileImage;

		private Color fTileColour = Color.White;

		private System.Drawing.Size fTileSize = new System.Drawing.Size(2, 2);

		private bool fShowGridlines = true;

		public bool ShowGridlines
		{
			get
			{
				return this.fShowGridlines;
			}
			set
			{
				this.fShowGridlines = value;
				base.Invalidate();
			}
		}

		public Color TileColour
		{
			get
			{
				return this.fTileColour;
			}
			set
			{
				this.fTileColour = value;
				base.Invalidate();
			}
		}

		public Image TileImage
		{
			get
			{
				return this.fTileImage;
			}
			set
			{
				this.fTileImage = value;
				base.Invalidate();
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
				base.Invalidate();
			}
		}

		public TilePanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
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
			this.components = new System.ComponentModel.Container();
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			e.Graphics.FillRectangle(new SolidBrush(this.BackColor), base.ClientRectangle);
			Rectangle clientRectangle = base.ClientRectangle;
			double width = (double)clientRectangle.Width / (double)this.fTileSize.Width;
			Rectangle rectangle = base.ClientRectangle;
			double height = (double)rectangle.Height / (double)this.fTileSize.Height;
			float single = (float)Math.Min(width, height);
			float width1 = single * (float)this.fTileSize.Width;
			float height1 = single * (float)this.fTileSize.Height;
			Rectangle clientRectangle1 = base.ClientRectangle;
			float single1 = ((float)clientRectangle1.Width - width1) / 2f;
			Rectangle rectangle1 = base.ClientRectangle;
			float height2 = ((float)rectangle1.Height - height1) / 2f;
			RectangleF rectangleF = new RectangleF(single1, height2, width1, height1);
			if (this.fTileImage == null)
			{
				using (Brush solidBrush = new SolidBrush(this.fTileColour))
				{
					e.Graphics.FillRectangle(solidBrush, rectangleF);
				}
				using (Pen pen = new Pen(Color.Black, 2f))
				{
					e.Graphics.DrawRectangle(pen, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
				}
			}
			else
			{
				e.Graphics.DrawImage(this.fTileImage, rectangleF);
			}
			if (this.fShowGridlines)
			{
				using (Pen pen1 = new Pen(Color.DarkGray))
				{
					for (int i = 1; i != this.fTileSize.Width; i++)
					{
						float single2 = single1 + (float)i * single;
						e.Graphics.DrawLine(pen1, single2, height2, single2, height2 + height1);
					}
					for (int j = 1; j != this.fTileSize.Height; j++)
					{
						float single3 = height2 + (float)j * single;
						e.Graphics.DrawLine(pen1, single1, single3, single1 + width1, single3);
					}
				}
			}
		}
	}
}