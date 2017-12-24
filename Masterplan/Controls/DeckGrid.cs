using Masterplan.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Masterplan.Controls
{
	internal class DeckGrid : UserControl
	{
		private IContainer components;

		private EncounterDeck fDeck;

		private List<CardCategory> fRows;

		private List<Difficulty> fColumns;

		private Dictionary<int, int> fRowTotals;

		private Dictionary<int, int> fColumnTotals;

		private Dictionary<Point, int> fCells;

		private StringFormat fCentred = new StringFormat();

		private Point fHoverCell = Point.Empty;

		private Point fSelectedCell = Point.Empty;

		public EncounterDeck Deck
		{
			get
			{
				return this.fDeck;
			}
			set
			{
				this.fDeck = value;
				this.fSelectedCell = Point.Empty;
				base.Invalidate();
			}
		}

		public bool IsCellSelected
		{
			get
			{
				return this.fSelectedCell != Point.Empty;
			}
		}

		public DeckGrid()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.fCentred.Alignment = StringAlignment.Center;
			this.fCentred.LineAlignment = StringAlignment.Center;
			this.fCentred.Trimming = StringTrimming.EllipsisWord;
		}

		private void analyse_deck()
		{
			if (this.fDeck == null)
			{
				return;
			}
			this.fRows = new List<CardCategory>();
			foreach (CardCategory value in Enum.GetValues(typeof(CardCategory)))
			{
				this.fRows.Add(value);
			}
			this.fColumns = new List<Difficulty>();
			foreach (Difficulty difficulty in Enum.GetValues(typeof(Difficulty)))
			{
				if (difficulty == Difficulty.Trivial && this.fDeck.Level < 3 || difficulty == Difficulty.Random)
				{
					continue;
				}
				this.fColumns.Add(difficulty);
			}
			this.fCells = new Dictionary<Point, int>();
			this.fRowTotals = new Dictionary<int, int>();
			this.fColumnTotals = new Dictionary<int, int>();
			for (int i = 0; i != this.fRows.Count; i++)
			{
				CardCategory item = this.fRows[i];
				for (int j = 0; j != this.fColumns.Count; j++)
				{
					Difficulty item1 = this.fColumns[j];
					int num = 0;
					foreach (EncounterCard card in this.fDeck.Cards)
					{
						if (card.Category != item || card.GetDifficulty(this.fDeck.Level) != item1)
						{
							continue;
						}
						num++;
					}
					this.fCells[new Point(i, j)] = num;
					if (!this.fRowTotals.ContainsKey(i))
					{
						this.fRowTotals[i] = 0;
					}
					Dictionary<int, int> nums = this.fRowTotals;
					Dictionary<int, int> nums1 = nums;
					int num1 = i;
					nums[num1] = nums1[num1] + num;
					if (!this.fColumnTotals.ContainsKey(j))
					{
						this.fColumnTotals[j] = 0;
					}
					Dictionary<int, int> item2 = this.fColumnTotals;
					Dictionary<int, int> nums2 = item2;
					int num2 = j;
					item2[num2] = nums2[num2] + num;
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

		public bool InSelectedCell(EncounterCard card)
		{
			if (this.fSelectedCell == Point.Empty)
			{
				return false;
			}
			int x = this.fSelectedCell.X - 1;
			Difficulty item = this.fColumns[x];
			int y = this.fSelectedCell.Y - 1;
			CardCategory cardCategory = this.fRows[y];
			if (card.Category != cardCategory)
			{
				return false;
			}
			return card.GetDifficulty(this.fDeck.Level) == item;
		}

		protected void OnCellActivated()
		{
			if (this.CellActivated != null)
			{
				this.CellActivated(this, new EventArgs());
			}
		}

		protected override void OnClick(EventArgs e)
		{
			this.fSelectedCell = this.fHoverCell;
			if (this.fSelectedCell.X > this.fColumns.Count || this.fSelectedCell.Y > this.fRows.Count)
			{
				this.fSelectedCell = Point.Empty;
			}
			base.Invalidate();
			this.OnSelectedCellChanged();
		}

		protected override void OnDoubleClick(EventArgs e)
		{
			this.OnCellActivated();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.fHoverCell = Point.Empty;
			base.Invalidate();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this.fColumns == null || this.fRows == null)
			{
				return;
			}
			Rectangle clientRectangle = base.ClientRectangle;
			float width = (float)clientRectangle.Width / (float)(this.fColumns.Count + 2);
			Rectangle rectangle = base.ClientRectangle;
			float height = (float)rectangle.Height / (float)(this.fRows.Count + 2);
			Point client = base.PointToClient(System.Windows.Forms.Cursor.Position);
			int x = (int)((float)client.X / width);
			int y = (int)((float)client.Y / height);
			if (x == 0 || y == 0)
			{
				this.fHoverCell = Point.Empty;
				base.Invalidate();
				return;
			}
			if (x != this.fHoverCell.X || y != this.fHoverCell.Y)
			{
				this.fHoverCell = new Point(x, y);
				base.Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Brushes.White, base.ClientRectangle);
			if (this.fDeck == null)
			{
				e.Graphics.DrawString("(no deck)", this.Font, SystemBrushes.WindowText, base.ClientRectangle, this.fCentred);
				return;
			}
			this.analyse_deck();
			Rectangle clientRectangle = base.ClientRectangle;
			float width = (float)clientRectangle.Width / (float)(this.fColumns.Count + 2);
			Rectangle rectangle = base.ClientRectangle;
			float height = (float)rectangle.Height / (float)(this.fRows.Count + 2);
			using (Pen pen = new Pen(SystemColors.ControlDark))
			{
				for (int i = 0; i != this.fRows.Count + 1; i++)
				{
					float single = (float)(i + 1) * height;
					Graphics graphics = e.Graphics;
					PointF pointF = new PointF((float)base.ClientRectangle.Left, single);
					Rectangle clientRectangle1 = base.ClientRectangle;
					graphics.DrawLine(pen, pointF, new PointF((float)clientRectangle1.Right, single));
				}
				for (int j = 0; j != this.fColumns.Count + 1; j++)
				{
					float single1 = (float)(j + 1) * width;
					Graphics graphic = e.Graphics;
					PointF pointF1 = new PointF(single1, (float)base.ClientRectangle.Top);
					Rectangle rectangle1 = base.ClientRectangle;
					graphic.DrawLine(pen, pointF1, new PointF(single1, (float)rectangle1.Bottom));
				}
			}
			e.Graphics.FillRectangle(Brushes.Black, this.get_rect(0, 0));
			for (int k = 1; k != this.fColumns.Count + 2; k++)
			{
				RectangleF _rect = this.get_rect(k, 0);
				e.Graphics.FillRectangle(Brushes.Black, _rect);
			}
			for (int l = 1; l != this.fRows.Count + 2; l++)
			{
				RectangleF rectangleF = this.get_rect(0, l);
				e.Graphics.FillRectangle(Brushes.Black, rectangleF);
			}
			using (Brush solidBrush = new SolidBrush(Color.FromArgb(30, Color.Gray)))
			{
				for (int m = 1; m != this.fColumns.Count + 1; m++)
				{
					RectangleF _rect1 = this.get_rect(m, this.fRows.Count + 1);
					e.Graphics.FillRectangle(solidBrush, _rect1);
				}
				for (int n = 1; n != this.fRows.Count + 1; n++)
				{
					RectangleF rectangleF1 = this.get_rect(this.fColumns.Count + 1, n);
					e.Graphics.FillRectangle(solidBrush, rectangleF1);
				}
			}
			if (this.fHoverCell != Point.Empty && this.fHoverCell.X <= this.fColumns.Count && this.fHoverCell.Y <= this.fRows.Count)
			{
				RectangleF _rect2 = this.get_rect(this.fHoverCell.X, this.fHoverCell.Y);
				e.Graphics.DrawRectangle(SystemPens.Highlight, _rect2.X, _rect2.Y, _rect2.Width, _rect2.Height);
				using (Brush brush = new SolidBrush(Color.FromArgb(30, SystemColors.Highlight)))
				{
					e.Graphics.FillRectangle(brush, _rect2);
				}
			}
			if (this.fSelectedCell != Point.Empty)
			{
				RectangleF rectangleF2 = this.get_rect(this.fSelectedCell.X, this.fSelectedCell.Y);
				using (Brush solidBrush1 = new SolidBrush(Color.FromArgb(100, SystemColors.Highlight)))
				{
					e.Graphics.FillRectangle(solidBrush1, rectangleF2);
				}
			}
			System.Drawing.Font font = new System.Drawing.Font(this.Font, FontStyle.Bold);
			for (int o = 0; o != this.fRows.Count + 1; o++)
			{
				string str = "Total";
				if (o != this.fRows.Count)
				{
					CardCategory item = this.fRows[o];
					str = item.ToString();
					if (item == CardCategory.SoldierBrute)
					{
						str = "Sldr / Brute";
					}
				}
				RectangleF _rect3 = this.get_rect(0, o + 1);
				e.Graphics.DrawString(str, font, Brushes.White, _rect3, this.fCentred);
			}
			for (int p = 0; p != this.fColumns.Count + 1; p++)
			{
				string str1 = "Total";
				if (p != this.fColumns.Count)
				{
					switch (this.fColumns[p])
					{
						case Difficulty.Trivial:
						{
							str1 = "Lower";
							break;
						}
						case Difficulty.Easy:
						{
							int num = Math.Max(1, this.fDeck.Level - 1);
							object[] level = new object[] { "Lvl ", num, " to ", this.fDeck.Level + 1 };
							str1 = string.Concat(level);
							break;
						}
						case Difficulty.Moderate:
						{
							object[] objArray = new object[] { "Lvl ", this.fDeck.Level + 2, " to ", this.fDeck.Level + 3 };
							str1 = string.Concat(objArray);
							break;
						}
						case Difficulty.Hard:
						{
							object[] level1 = new object[] { "Lvl ", this.fDeck.Level + 4, " to ", this.fDeck.Level + 5 };
							str1 = string.Concat(level1);
							break;
						}
						case Difficulty.Extreme:
						{
							str1 = "Higher";
							break;
						}
					}
				}
				RectangleF rectangleF3 = this.get_rect(p + 1, 0);
				e.Graphics.DrawString(str1, font, Brushes.White, rectangleF3, this.fCentred);
			}
			for (int q = 0; q != this.fRows.Count; q++)
			{
				for (int r = 0; r != this.fColumns.Count; r++)
				{
					Point point = new Point(q, r);
					int item1 = this.fCells[point];
					if (item1 != 0)
					{
						RectangleF _rect4 = this.get_rect(r + 1, q + 1);
						e.Graphics.DrawString(item1.ToString(), this.Font, SystemBrushes.WindowText, _rect4, this.fCentred);
					}
				}
			}
			for (int s = 0; s != this.fRows.Count; s++)
			{
				CardCategory cardCategory = this.fRows[s];
				int num1 = this.fRowTotals[s];
				int num2 = 0;
				switch (cardCategory)
				{
					case CardCategory.Artillery:
					{
						num2 = 5;
						break;
					}
					case CardCategory.Controller:
					{
						num2 = 5;
						break;
					}
					case CardCategory.Lurker:
					{
						num2 = 2;
						break;
					}
					case CardCategory.Skirmisher:
					{
						num2 = 14;
						break;
					}
					case CardCategory.SoldierBrute:
					{
						num2 = 18;
						break;
					}
					case CardCategory.Minion:
					{
						num2 = 5;
						break;
					}
					case CardCategory.Solo:
					{
						num2 = 1;
						break;
					}
				}
				RectangleF rectangleF4 = this.get_rect(this.fColumns.Count + 1, s + 1);
				Graphics graphics1 = e.Graphics;
				object[] objArray1 = new object[] { num1, " (", num2, ")" };
				graphics1.DrawString(string.Concat(objArray1), font, SystemBrushes.WindowText, rectangleF4, this.fCentred);
			}
			for (int t = 0; t != this.fColumns.Count; t++)
			{
				Difficulty difficulty = this.fColumns[t];
				int item2 = this.fColumnTotals[t];
				RectangleF _rect5 = this.get_rect(t + 1, this.fRows.Count + 1);
				e.Graphics.DrawString(item2.ToString(), font, SystemBrushes.WindowText, _rect5, this.fCentred);
			}
			RectangleF rectangleF5 = this.get_rect(this.fColumns.Count + 1, this.fRows.Count + 1);
			e.Graphics.DrawString(string.Concat(this.fDeck.Cards.Count, " cards"), font, SystemBrushes.WindowText, rectangleF5, this.fCentred);
		}

		protected void OnSelectedCellChanged()
		{
			if (this.SelectedCellChanged != null)
			{
				this.SelectedCellChanged(this, new EventArgs());
			}
		}

		public event EventHandler CellActivated;

		public event EventHandler SelectedCellChanged;
	}
}