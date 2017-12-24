using Masterplan.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class DiceGraphPanel : UserControl
	{
		private List<int> fDice = new List<int>();

		private int fConstant;

		private string fTitle = "";

		private float fRange = 0.5f;

		private Dictionary<int, int> fDistribution;

		private StringFormat fCentred = new StringFormat();

		private IContainer components;

		public int Constant
		{
			get
			{
				return this.fConstant;
			}
			set
			{
				this.fConstant = value;
				this.fDistribution = null;
				base.Invalidate();
			}
		}

		public List<int> Dice
		{
			get
			{
				return this.fDice;
			}
			set
			{
				this.fDice = value;
				this.fDistribution = null;
				base.Invalidate();
			}
		}

		public string Title
		{
			get
			{
				return this.fTitle;
			}
			set
			{
				this.fTitle = value;
				base.Invalidate();
			}
		}

		public DiceGraphPanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
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

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			base.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			try
			{
				if (this.fDistribution == null)
				{
					this.fDistribution = DiceStatistics.Odds(this.fDice, this.fConstant);
				}
				if (this.fDistribution != null && this.fDistribution.Keys.Count != 0)
				{
					int width = base.Width / 10;
					int height = base.Height / 10;
					Rectangle rectangle = new Rectangle(width, 3 * height, base.Width - 2 * width, base.Height - 5 * height);
					if (this.fTitle != null && this.fTitle != "")
					{
						Rectangle rectangle1 = new Rectangle(rectangle.X, rectangle.Y - 2 * height, rectangle.Width, height);
						e.Graphics.FillRectangle(Brushes.White, rectangle1);
						e.Graphics.DrawRectangle(Pens.DarkGray, rectangle1);
						e.Graphics.DrawString(this.fTitle, new System.Drawing.Font(this.Font.FontFamily, (float)(height / 3)), Brushes.Black, rectangle1, this.fCentred);
					}
					int num = 2147483647;
					int num1 = -2147483648;
					int num2 = -2147483648;
					int item = 0;
					foreach (int key in this.fDistribution.Keys)
					{
						num = Math.Min(num, key);
						num1 = Math.Max(num1, key);
						num2 = Math.Max(num2, this.fDistribution[key]);
						item += this.fDistribution[key];
					}
					float single = (1f - this.fRange) / 2f;
					float single1 = 1f - single;
					Point client = base.PointToClient(System.Windows.Forms.Cursor.Position);
					int num3 = num1 - num + 1;
					float width1 = (float)rectangle.Width / (float)num3;
					float single2 = Math.Min(this.Font.Size, width1 / 2f);
					System.Drawing.Font font = new System.Drawing.Font(this.Font.FontFamily, single2);
					List<PointF> pointFs = new List<PointF>();
					int item1 = 0;
					foreach (int key1 in this.fDistribution.Keys)
					{
						float single3 = width1 * (float)(key1 - num);
						float height1 = (float)(rectangle.Height * (num2 - this.fDistribution[key1]) / num2);
						RectangleF rectangleF = new RectangleF(single3 + (float)rectangle.X, (float)rectangle.Y + height1, width1, (float)rectangle.Height - height1);
						item1 += this.fDistribution[key1];
						float single4 = (float)item1 / (float)item;
						bool flag = rectangleF.Contains(client);
						bool flag1 = (single4 < single ? false : single4 <= single1);
						flag1 = false;
						float x = single3 + (float)rectangle.X + width1 / 2f;
						float y = (float)rectangle.Y + height1;
						pointFs.Add(new PointF(x, y));
						Pen gray = Pens.Gray;
						if (flag1 || flag)
						{
							gray = Pens.Black;
						}
						e.Graphics.DrawLine(gray, x, (float)rectangle.Bottom, x, y);
						RectangleF rectangleF1 = new RectangleF(rectangleF.Left, rectangleF.Bottom, width1, (float)height);
						e.Graphics.DrawString(key1.ToString(), font, (flag ? Brushes.Black : Brushes.DarkGray), rectangleF1, this.fCentred);
					}
					e.Graphics.DrawLine(Pens.Black, rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
					for (int i = 1; i < pointFs.Count; i++)
					{
						e.Graphics.DrawLine(new Pen(Color.Red, 2f), pointFs[i - 1], pointFs[i]);
					}
				}
			}
			catch
			{
			}
		}
	}
}