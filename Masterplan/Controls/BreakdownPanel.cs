using Masterplan.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class BreakdownPanel : UserControl
	{
		private IContainer components;

		private List<Hero> fHeroes;

		private List<HeroRoleType> fRows;

		private List<string> fColumns;

		private Dictionary<Point, int> fCells;

		private Dictionary<int, int> fRowTotals;

		private Dictionary<int, int> fColumnTotals;

		private StringFormat fCentred = new StringFormat();

		public List<Hero> Heroes
		{
			get
			{
				return this.fHeroes;
			}
			set
			{
				this.fHeroes = value;
			}
		}

		public BreakdownPanel()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
		}

		private void analyse_party()
		{
			if (this.fHeroes == null)
			{
				return;
			}
			this.fRows = new List<HeroRoleType>();
			foreach (HeroRoleType value in Enum.GetValues(typeof(HeroRoleType)))
			{
				this.fRows.Add(value);
			}
			this.fColumns = new List<string>();
			foreach (Hero fHero in this.fHeroes)
			{
				if (this.fColumns.Contains(fHero.PowerSource))
				{
					continue;
				}
				this.fColumns.Add(fHero.PowerSource);
			}
			this.fColumns.Sort();
			this.fCells = new Dictionary<Point, int>();
			this.fRowTotals = new Dictionary<int, int>();
			this.fColumnTotals = new Dictionary<int, int>();
			for (int i = 0; i != this.fRows.Count; i++)
			{
				HeroRoleType item = this.fRows[i];
				if (!this.fRowTotals.ContainsKey(i))
				{
					this.fRowTotals[i] = 0;
				}
				for (int j = 0; j != this.fColumns.Count; j++)
				{
					string str = this.fColumns[j];
					int num = 0;
					foreach (Hero hero in this.fHeroes)
					{
						if (hero.Role != item || !(hero.PowerSource == str))
						{
							continue;
						}
						num++;
					}
					this.fCells[new Point(i, j)] = num;
					Dictionary<int, int> nums = this.fRowTotals;
					Dictionary<int, int> nums1 = nums;
					int num1 = i;
					nums[num1] = nums1[num1] + num;
					if (!this.fColumnTotals.ContainsKey(j))
					{
						this.fColumnTotals[j] = 0;
					}
					Dictionary<int, int> item1 = this.fColumnTotals;
					Dictionary<int, int> nums2 = item1;
					int num2 = j;
					item1[num2] = nums2[num2] + num;
				}
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

		private RectangleF get_rect(int x, int y)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			float width = (float)clientRectangle.Width / (float)(this.fColumns.Count + 2);
			Rectangle rectangle = base.ClientRectangle;
			float height = (float)rectangle.Height / (float)(this.fRows.Count + 2);
			return new RectangleF((float)x * width, (float)y * height, width, height);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.fHeroes == null)
			{
				e.Graphics.DrawString("(no heroes)", this.Font, SystemBrushes.WindowText, base.ClientRectangle, this.fCentred);
				return;
			}
			this.analyse_party();
			System.Drawing.Font font = new System.Drawing.Font(this.Font, FontStyle.Bold);
			for (int i = 0; i != this.fRows.Count + 1; i++)
			{
				string str = "Total";
				if (i != this.fRows.Count)
				{
					str = this.fRows[i].ToString();
				}
				RectangleF _rect = this.get_rect(0, i + 1);
				e.Graphics.DrawString(str, font, SystemBrushes.WindowText, _rect, this.fCentred);
			}
			for (int j = 0; j != this.fColumns.Count + 1; j++)
			{
				string item = "Total";
				if (j != this.fColumns.Count)
				{
					item = this.fColumns[j];
				}
				RectangleF rectangleF = this.get_rect(j + 1, 0);
				e.Graphics.DrawString(item, font, SystemBrushes.WindowText, rectangleF, this.fCentred);
			}
			for (int k = 0; k != this.fRows.Count; k++)
			{
				for (int l = 0; l != this.fColumns.Count; l++)
				{
					int num = this.fCells[new Point(k, l)];
					RectangleF _rect1 = this.get_rect(l + 1, k + 1);
					e.Graphics.DrawString(num.ToString(), this.Font, SystemBrushes.WindowText, _rect1, this.fCentred);
				}
			}
			for (int m = 0; m != this.fRows.Count; m++)
			{
				HeroRoleType heroRoleType = this.fRows[m];
				int item1 = this.fRowTotals[m];
				RectangleF rectangleF1 = this.get_rect(this.fColumns.Count + 1, m + 1);
				e.Graphics.DrawString(item1.ToString(), font, SystemBrushes.WindowText, rectangleF1, this.fCentred);
			}
			for (int n = 0; n != this.fColumns.Count; n++)
			{
				string str1 = this.fColumns[n];
				int num1 = this.fColumnTotals[n];
				RectangleF _rect2 = this.get_rect(n + 1, this.fRows.Count + 1);
				e.Graphics.DrawString(num1.ToString(), font, SystemBrushes.WindowText, _rect2, this.fCentred);
			}
			RectangleF rectangleF2 = this.get_rect(this.fColumns.Count + 1, this.fRows.Count + 1);
			Graphics graphics = e.Graphics;
			int count = this.fHeroes.Count;
			graphics.DrawString(count.ToString(), font, SystemBrushes.WindowText, rectangleF2, this.fCentred);
			Rectangle clientRectangle = base.ClientRectangle;
			float width = (float)clientRectangle.Width / (float)(this.fColumns.Count + 2);
			Rectangle rectangle = base.ClientRectangle;
			float height = (float)rectangle.Height / (float)(this.fRows.Count + 2);
			Pen pen = new Pen(SystemColors.ControlDark);
			for (int o = 0; o != this.fRows.Count + 1; o++)
			{
				float single = (float)(o + 1) * height;
				Graphics graphic = e.Graphics;
				PointF pointF = new PointF((float)base.ClientRectangle.Left, single);
				Rectangle clientRectangle1 = base.ClientRectangle;
				graphic.DrawLine(pen, pointF, new PointF((float)clientRectangle1.Right, single));
			}
			for (int p = 0; p != this.fColumns.Count + 1; p++)
			{
				float single1 = (float)(p + 1) * width;
				Graphics graphics1 = e.Graphics;
				PointF pointF1 = new PointF(single1, (float)base.ClientRectangle.Top);
				Rectangle rectangle1 = base.ClientRectangle;
				graphics1.DrawLine(pen, pointF1, new PointF(single1, (float)rectangle1.Bottom));
			}
		}
	}
}